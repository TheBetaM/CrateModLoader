using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity;
using CrateModLoader.GameSpecific.CrashTS.Mods;
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

        public string bdPath = "";
        internal string extensionMod = "2";
        internal TwinsFile.FileType rmType = TwinsFile.FileType.RM2;
        internal TwinsFile.FileType smType = TwinsFile.FileType.SM2;

        public override void StartModProcess()
        {
            ProcessBusy = true;
            ModProcess();
        }

        private async void ModProcess()
        {
            // Extract BD (PS2 only)
            UpdateProcessMessage("Extracting CRASH.BD...", 0);
            SetupBD();

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

            //Mods
            FindFiles(new Parser_RM(this, rmType), new Parser_SM(this, smType));
            await StartNewPass();

            Twins_Data.cachedGameObjects.Clear();

            UpdateProcessMessage("Modding textures...", 94);
            Twins_Data_Textures.Textures_Mod(bdPath, GameRegion.Region);

            // Build BD
            UpdateProcessMessage("Building CRASH.BD...", 95);
            BuildBD();

            ProcessBusy = false;
        }

        public override void StartPreload()
        {
            SetupBD();
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                Twins_Data_Textures.Textures_Preload(bdPath, GameRegion.Region);
            }
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


    }
}
