using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity;
using CrateModLoader.GameSpecific.CrashTS.Mods;
using System.Threading.Tasks;
//Twinsanity API by NeoKesha, Smartkin, ManDude, BetaM and Marko (https://github.com/Smartkin/twinsanity-editor)
/* 
 * Mod Layers:
 * 1: Extracted BD/BH archive files (PS2 only, same as layer 0 on XBOX)
 * Mod Passes:
 * ChunkInfoRM -> All RM files
 * ChunkInfoSM -> All SM files
 * ChunkInfoFull -> All RM/SM file pairs (not yet implemented)
 * ExecutableInfo -> Executable file paths and associated metadata
 */

namespace CrateModLoader.GameSpecific.CrashTS
{

    public enum ModProps : int
    {
        Options = 0,
        Misc = 1,
        Character = 2,
        Textures = 3,
        Galleries = 4,
        Text = 5,
    }

    public sealed class Modder_Twins : Modder
    {
        public override bool CanPreloadGame => true;
        public override List<ConsoleMode> PreloadConsoles => new List<ConsoleMode>() { ConsoleMode.PS2, };
        public override bool AsyncProcess => true;

        public Modder_Twins() { }

        public string bdPath = "";
        internal string extensionMod = "2";
        internal TwinsFile.FileType rmType = TwinsFile.FileType.RM2;
        internal TwinsFile.FileType smType = TwinsFile.FileType.SM2;

        private int CurrentPass = 0;
        private float PassPercentMod = 39f;
        private int PassPercentAdd = 10;
        private bool EditingRM = false;
        private bool EditingSM = false;
        private bool MainBusy = false;

        public override void StartModProcess()
        {
            ProcessBusy = true;

            AsyncStart();
        }

        public async void AsyncStart()
        {
            UpdateProcessMessage("Extracting CRASH.BD...", 0);

            // Extract BD (PS2 only)
            SetupBD();

            // Mod files
            ModProcess();

            while (MainBusy || PassBusy)
            {
                await Task.Delay(100);
            }

            UpdateProcessMessage("Building CRASH.BD...", 95);

            // Build BD
            BuildBD();

            ProcessBusy = false;
        }

        private async void ModProcess()
        {
            MainBusy = true;

            //Start Modding
            EditingRM = CheckModsForRM();
            EditingSM = CheckModsForSM();

            // Discovering files
            Dictionary<string, bool> Paths = new Dictionary<string, bool>();

            if (EditingRM)
            {
                Paths.Add(bdPath + @"Startup\Default.rm" + extensionMod, false);
            }

            DirectoryInfo di = new DirectoryInfo(bdPath + "/Levels/");
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LoadLevels(dir, ref Paths);
            }
            PassCount = Paths.Count;

            UpdateProcessMessage("Patching executable...", 4);
            PatchEXE(ConsolePipeline.Metadata.Console, GameRegion.Region);

            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 5);
            ModCrates.InstallLayerMods(EnabledModCrates, bdPath, 1);

            //could be better...
            foreach (ModPropertyBase prop in Props)
            {
                if (prop.HasChanged)
                {
                    if (prop.Category == (int)ModProps.Character)
                    {
                        TS_Props_Main.Option_RandCharacterParams.Value = 1;
                        TS_Props_Main.Option_RandCharacterParams.HasChanged = true;
                        TS_Rand_CharParams PMod = (TS_Rand_CharParams)TS_Props_Main.Option_RandCharacterParams.TargetMod;
                        PMod.isSet = true;
                    }
                }
            }

            bool NeedsCache = NeedsCachePass();
            CurrentPass = 0;
            if (!NeedsCache)
            {
                PassPercentMod = 83f;
                CurrentPass++;
            }

            while (CurrentPass < 2)
            {
                PassIterator = 0;
                PassBusy = true;
                if (CurrentPass == 0)
                {
                    PassPercentMod = 39f;
                    PassPercentAdd = 10;
                    UpdateProcessMessage("Cache Pass", 10);
                    BeforeCachePass();
                }
                else if (CurrentPass == 1)
                {
                    if (NeedsCache)
                    {
                        PassPercentMod = 43f;
                        PassPercentAdd = 50;
                        UpdateProcessMessage("Mod Pass", 50);
                    }
                    else
                    {
                        PassPercentMod = 83f;
                        UpdateProcessMessage("Mod Pass", 10);
                    }
                    
                    BeforeModPass();
                }

                IList<Task> editTaskList = new List<Task>();

                foreach (KeyValuePair<string, bool> Path in Paths)
                {
                    editTaskList.Add(EditLevel(Path.Key, Path.Value));
                }

                await Task.WhenAll(editTaskList);

                CurrentPass++;
                PassBusy = false;
            }

            Twins_Data.cachedGameObjects.Clear();

            UpdateProcessMessage("Modding textures...", 94);
            Twins_Data_Textures.Textures_Mod(bdPath, GameRegion.Region);

            MainBusy = false;
        }

        public override void StartPreload()
        {
            SetupBD();
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                Twins_Data_Textures.Textures_Preload(bdPath, GameRegion.Region);
            }
        }

        private async Task EditLevel(string path, bool isSM)
        {
            //Console.WriteLine("Editing: " + path);
            TwinsFile.FileType FileType = rmType;
            if (isSM) FileType = smType;
            
            await Task.Run(
            () => 
            {
                TwinsFile Archive = new TwinsFile();
                Archive.LoadFile(path, FileType);

                if (!isSM)
                {
                    ChunkInfoRM chunk = new ChunkInfoRM(Archive, ChunkPathToType(path));

                    switch (CurrentPass)
                    {
                        case 0:
                            StartCachePass(chunk);
                            break;
                        default:
                        case 1:
                            StartModPass(chunk);
                            break;
                    }
                }
                else
                {
                    ChunkInfoSM chunk = new ChunkInfoSM(Archive, ChunkPathToType(path));

                    switch (CurrentPass)
                    {
                        case 0:
                            StartCachePass(chunk);
                            break;
                        default:
                        case 1:
                            StartModPass(chunk);
                            break;
                    }
                }

                    Archive.SaveFile(path);
                }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }

        void Recursive_LoadLevels(DirectoryInfo di, ref Dictionary<string, bool> Paths)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LoadLevels(dir, ref Paths);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (EditingRM && (file.Extension.ToLower() == ".rm2" || file.Extension.ToLower() == ".rmx"))
                {
                    Paths.Add(file.FullName, false);
                }
                else if (EditingSM && (file.Extension.ToLower() == ".sm2" || file.Extension.ToLower() == ".smx"))
                {
                    Paths.Add(file.FullName, true);
                }
            }
        }

        bool CheckModsForRM()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<ChunkInfoRM>)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckModsForSM()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<ChunkInfoSM>)
                {
                    return true;
                }
            }
            return false;
        }

        void SetupBD()
        {
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                bdPath = System.IO.Path.Combine(ConsolePipeline.ExtractedPath,  @"cml_extr\");
                extensionMod = "2";
                rmType = TwinsFile.FileType.RM2;
                smType = TwinsFile.FileType.SM2;

                Directory.CreateDirectory(bdPath);

                BDArchive.ExtractAll(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH"), bdPath);

                File.Delete(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH.BD"));
                File.Delete(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH.BH"));
            }
            else
            {
                bdPath = ConsolePipeline.ExtractedPath;
                extensionMod = "x";
                rmType = TwinsFile.FileType.RMX;
                smType = TwinsFile.FileType.SMX;
            }
        }

        void BuildBD()
        {
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                BDArchive.CompileAll(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH"), bdPath);

                // Get rid of extracted files
                if (Directory.Exists(bdPath))
                {
                    DirectoryInfo di = new DirectoryInfo(bdPath);

                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }

                    Directory.Delete(bdPath);
                }
            }
        }

        void PatchEXE(ConsoleMode console, RegionType region)
        {
            string filePath = System.IO.Path.Combine(ConsolePipeline.ExtractedPath, GameRegion.ExecName);

            ExecutableIndex index = ExecutableIndex.Invalid;
            if (console == ConsoleMode.XBOX)
            {
                if (region == RegionType.PAL)
                    index = ExecutableIndex.XBOX_PAL;
                else if (region == RegionType.NTSC_U)
                    index = ExecutableIndex.XBOX_NTSC;
                else
                    return;
            }
            else
            {
                if (region == RegionType.NTSC_U)
                {
                    using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
                    {
                        reader.BaseStream.Position = 0x1ECB10;
                        char ch = reader.ReadChar();

                        if (ch == 'C')
                            index = ExecutableIndex.NTSCU;
                        else
                            index = ExecutableIndex.NTSCU2;
                    }
                }
                else if (region == RegionType.PAL)
                    index = ExecutableIndex.PAL;
                else if (region == RegionType.NTSC_J)
                    index = ExecutableIndex.NTSCJ;
                else
                    return;
            }

            StartModPass(new ExecutableInfo(filePath, index, bdPath));
        }

        private ChunkType ChunkPathToType(string path)
        {
            ChunkType type = ChunkType.Invalid;

            for (int i = 0; i < Twins_Data.All_Chunks.Count; i++)
            {
                if (path.ToLower().Contains(Twins_Data.All_Chunks[i].Path.ToLower()))
                {
                    type = Twins_Data.All_Chunks[i].Chunk;
                    break;
                }
            }

            if (type == ChunkType.Invalid)
            {
                Console.WriteLine("invalid Chunk");
                Console.WriteLine("any chunk path: " + Twins_Data.All_Chunks[0].Path.ToLower());
                Console.WriteLine("file path: " + path.ToLower());
            }

            return type;
        }
    }
}
