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

        public override async void StartModProcess()
        {
            string bdPath = ConsolePipeline.ExtractedPath;
            TwinsFile.FileType rmType = TwinsFile.FileType.RMX;
            TwinsFile.FileType smType = TwinsFile.FileType.SMX;

            #region Extract BD
            // Extract BD (PS2 only)
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                UpdateProcessMessage("Extracting CRASH.BD...", 0);
                bdPath = System.IO.Path.Combine(ConsolePipeline.ExtractedPath, @"CRASH6\CRASH\");
                rmType = TwinsFile.FileType.RM2;
                smType = TwinsFile.FileType.SM2;

                FindArchives(new Pipeline_BD(this));
                await StartPipelines(PipelinePass.Extract);
            }
            #endregion

            UpdateProcessMessage("Patching executable...", 4);
            PatchEXE(bdPath, ConsolePipeline.Metadata.Console, GameRegion.Region);

            //Mods
            FindFiles(new Parser_RM(this, rmType), new Parser_SM(this, smType));
            await StartNewPass();

            Twins_Data.cachedGameObjects.Clear();

            #region Build BD
            // Build BD
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                UpdateProcessMessage("Building CRASH.BD...", 95);
                await StartPipelines(PipelinePass.Build);
            }
            #endregion

            ProcessBusy = false;
        }

        public override async void StartPreload()
        {
            #region Extract BD
            // Extract BD (PS2 only)
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                UpdateProcessMessage("Extracting CRASH.BD...", 0);

                FindArchives(new Pipeline_BD(this));
                await StartPipelines(PipelinePass.Extract);
            }
            #endregion

            FindFiles();
            await StartPreloadPass();

            ProcessBusy = false;
        }

        void PatchEXE(string bdPath, ConsoleMode console, RegionType region)
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
