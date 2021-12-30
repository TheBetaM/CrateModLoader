/* API by BetaM.
 * Mod Layers:
 * 1: first.zip/frontend.zip/game.zip contents
 * Mod Passes:
 * xom/bdl/kev -> data containers
 * mss -> mission setup script (plain text)
   oss -> object setup script (plain text)
   sch -> scheme setup (plain text)
   tss -> team setup script (plain text)
   txt -> misc. mappings (plain text)
   bin -> localization text packs
 */
using CrateModLoader.GameSpecific.WormsForts;
namespace CrateModLoader.GameSpecific.WormsBI
{
    public sealed class Modder_WormsBI : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                XOM_TYPE.GetSupported();
                FindArchives(new Pipeline_ZIP(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_XOM(this, WormsGame.BattleIslands));
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
