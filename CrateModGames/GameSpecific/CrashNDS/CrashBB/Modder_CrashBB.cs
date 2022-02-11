//CBB API by BetaM.
/* 
 * Mod Layers:
 * 1: .BIN contents
 * Mod Passes:
 */
namespace CrateModLoader.GameSpecific.CrashBB
{
    public sealed class Modder_CrashBB : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                FindArchives(new Pipeline_BIN(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles();
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
