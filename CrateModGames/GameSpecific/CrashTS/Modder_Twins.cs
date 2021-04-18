using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;
using System.Threading.Tasks;
//Twinsanity API by NeoKesha, Smartkin, ManDude, BetaM and Marko (https://github.com/Smartkin/twinsanity-editor)
/* 
 * Mod Layers:
 * 1: Extracted BD/BH archive files (PS2 only, same as layer 0 on XBOX)
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

        public override Game Game => new Game()
        {
            Name = Twins_Text.GameTitle,
            ShortName = "CrashTS",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.XBOX,
                },
            API_Credit = Twins_Text.API_Credit,
            API_Link = "https://github.com/Smartkin/twinsanity-editor",
            TextClass = typeof(Twins_Text),
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_209.09",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_209.09",
                    CodeName = "SLUS_20909", },
                    new RegionCode() {
                    Name = @"SLES_525.68",
                    Region = RegionType.PAL,
                    ExecName = "SLES_525.68",
                    CodeName = "SLES_52568", },
                    new RegionCode() {
                    Name = @"SLPM_658.01",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_658.01",
                    CodeName = "SLPM_65801", },
                },
                [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Crash Twinsanity",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7,
                    ExecName = "default.xbe" },
                    new RegionCode() {
                    Name = "Crash Twinsanity",
                    Region = RegionType.PAL,
                    RegionNumber = 4,
                    ExecName = "default.xbe" },
                },
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [(int)ModProps.Options] = "Options",
                [(int)ModProps.Misc] = "Misc.",
                [(int)ModProps.Character] = "Character",
                [(int)ModProps.Textures] = "Textures",
                [(int)ModProps.Galleries] = "Galleries",
            }
        };
        public override bool CanPreloadGame => true;
        public override List<ConsoleMode> PreloadConsoles => new List<ConsoleMode>() { ConsoleMode.PS2, };
        public override bool AsyncProcess => true;

        public Modder_Twins() { }

        public string bdPath = "";
        internal string extensionMod = "2";
        internal TwinsFile.FileType rmType = TwinsFile.FileType.RM2;
        internal TwinsFile.FileType smType = TwinsFile.FileType.SM2;

        internal Random randState = new Random();
        private int CurrentPass = 0;
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
            UpdateProcessMessage("Extracting CRASH.BD...");

            // Extract BD (PS2 only)
            SetupBD();

            // Mod files
            ModProcess();

            while (MainBusy || PassBusy)
            {
                await Task.Delay(100);
            }

            UpdateProcessMessage("Building CRASH.BD...");

            // Build BD
            BuildBD();

            ProcessBusy = false;
        }

        private async void ModProcess()
        {
            MainBusy = true;

            //Start Modding
            randState = new Random(ModLoaderGlobals.RandomizerSeed);


            LoadActiveProps();
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

            UpdateProcessMessage("Patching executable...");
            PatchEXE(ConsolePipeline.Metadata.Console, GameRegion.Region);

            UpdateProcessMessage("Installing Mod Crates...");
            ModCrates.InstallLayerMods(EnabledModCrates, bdPath, 1);

            //todo
            foreach (ModPropertyBase prop in Props)
            {
                if (prop.HasChanged)
                {
                    if (prop.Category == (int)ModProps.Character)
                    {
                        bool Edit_AllCharacters = true;
                    }
                }
            }

            CurrentPass = 0;
            if (!NeedsCachePass())
                CurrentPass++;

            while (CurrentPass < 2)
            {
                PassIterator = 0;
                PassBusy = true;
                if (CurrentPass == 0)
                {
                    UpdateProcessMessage("Cache Pass");
                    BeforeCachePass();
                }
                else if (CurrentPass == 1)
                {
                    UpdateProcessMessage("Mod Pass");
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

            UpdateProcessMessage("Modding textures...");
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

            TwinsFile RM_Archive = new TwinsFile();
            if (!isSM)
            {
                RM_Archive.LoadFile(path, rmType);
                ChunkInfoRM chunk = new ChunkInfoRM(RM_Archive, ChunkPathToType(path));

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
                RM_Archive.LoadFile(path, smType);
                ChunkInfoSM chunk = new ChunkInfoSM(RM_Archive, ChunkPathToType(path));

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

            RM_Archive.SaveFile(path);

            PassIterator++;
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
