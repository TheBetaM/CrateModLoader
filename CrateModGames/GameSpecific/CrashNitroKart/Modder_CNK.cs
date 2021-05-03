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
        public override bool CanPreloadGame => true;

        public override async void StartModProcess()
        {
            FindArchives(new Pipeline_GOB(this));
            await StartPipelines(PipelinePass.Extract);

            FindFiles(new Parser_CSV(this));
            await StartNewPass();

            await StartPipelines(PipelinePass.Build);

            ProcessBusy = false;
        }

        public override async void StartPreload()
        {
            FindArchives(new Pipeline_GOB(this));
            await StartPipelines(PipelinePass.Extract);

            FindFiles();
            await StartPreloadPass();

            ProcessBusy = false;
        }
    }
}
