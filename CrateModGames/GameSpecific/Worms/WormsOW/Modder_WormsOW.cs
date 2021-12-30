/* API by BetaM.
 * Mod Layers:
 * 1: zip.zip contents
 * Mod Passes:
 * xom -> data containers
 * laf -> packed xom files
 */
using CrateModLoader.GameSpecific.WormsForts;
namespace CrateModLoader.GameSpecific.WormsOW
{
    public sealed class Modder_WormsOW : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                XOM_TYPE.GetSupported();
                FindArchives(new Pipeline_ZIP(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_XOM(this, WormsGame.OW));
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
