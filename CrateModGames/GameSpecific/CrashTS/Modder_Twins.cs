using System.Collections.Generic;
//Twinsanity API by NeoKesha, Smartkin, ManDude, BetaM and Marko (https://github.com/Smartkin/twinsanity-editor)
/* 
 * Mod Layers:
 * 1: Extracted BD/BH archive files (PS2 only, same as layer 0 on XBOX)
 * Mod Passes:
 * ChunkInfoRM -> All RM files
 * ChunkInfoSM -> All SM files
 * ChunkInfoFull -> All RM/SM file pairs (not yet implemented)
 */
namespace CrateModLoader.GameSpecific.CrashTS
{
    public sealed class Modder_Twins : Modder
    {
        public override bool CanPreloadGame => true;
        public override List<ConsoleMode> PreloadConsoles => new List<ConsoleMode>() { ConsoleMode.PS2, };

        public override async void StartModProcess()
        {
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                FindArchives(new Pipeline_BD(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_RM(this, ConsolePipeline.Metadata.Console), new Parser_SM(this, ConsolePipeline.Metadata.Console));
            await StartNewPass();

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }

        public override async void StartPreload()
        {
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                FindArchives(new Pipeline_BD(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles();
            await StartPreloadPass();

            ProcessBusy = false;
        }

    }
}
