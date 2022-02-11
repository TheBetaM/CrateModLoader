//Twinsanity API by NeoKesha, Smartkin, ManDude, BetaM and Marko (https://github.com/Smartkin/twinsanity-editor)
/* 
 * Mod Layers:
 * 1: Extracted BD/BH archive files (PS2/PSP only)
 * Mod Passes:
 * ChunkInfoRM -> All RM files
 * ChunkInfoSM -> All SM files
 * ChunkInfoFull -> All RM/SM file pairs (not yet implemented)
 * XML -> All XML files
 */
namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public sealed class Modder_MonkeyBallAdv : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded && ConsolePipeline.Metadata.Console != ConsoleMode.GCN)
            {
                FindArchives(new CrashTS.Pipeline_BD(this));
                await StartPipelines(PipelinePass.Extract);
            }

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PSP) //PS2 level editing not functional yet
                FindFiles(new Parser_XML(this), 
                    new Parser_RM(this, ConsolePipeline.Metadata.Console)
                    ,new Parser_SM(this, ConsolePipeline.Metadata.Console));
            else
                FindFiles(new Parser_XML(this));
            await StartNewPass();

            if (!ModderIsPreloading && ConsolePipeline.Metadata.Console != ConsoleMode.GCN)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
