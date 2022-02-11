//Titans DS API by BetaM.
/* 
 * Mod Layers:
 * 1: ROM.BIN contents 
 * Mod Passes:
 */
namespace CrateModLoader.GameSpecific.CrashTitansDS
{
    public sealed class Modder_CrashTitansDS : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                FindArchives(new Pipeline_BIN_Rom(this));
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