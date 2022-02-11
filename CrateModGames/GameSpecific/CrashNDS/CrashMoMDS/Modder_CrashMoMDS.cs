//MoMDS API by BetaM.
/* 
 * Mod Layers:
 * 1: .BIN contents
 * Mod Passes:
 * MXB - language files
 */
namespace CrateModLoader.GameSpecific.CrashMoMDS
{
    public sealed class Modder_CrashMoMDS : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                FindArchives(new Pipeline_BIN(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_MXB(this));
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}