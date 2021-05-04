//CNK Tools/API by BetaM, ManDude and eezstreet.
/* 
 * Mod Layers:
 * 1: ASSETS.GOB contents
 * Mod Passes:
 * CSV -> CSV table data
 * IGB -> to be implemented
 */
namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public sealed class Modder_CNK : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                FindArchives(new Pipeline_GOB(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_CSV(this), new Parser_PNG(this, ConsolePipeline.Metadata.Console, ConsolePipeline.ExtractedPath, ModderIsPreloading));
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
