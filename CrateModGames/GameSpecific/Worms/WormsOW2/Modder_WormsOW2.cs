/* API by BetaM.
 * Mod Layers:
 * 1: zip.zip contents (PSP)
 * Mod Passes:
 * xom/bdl/kev -> data containers (PSP)
 * laf -> packed xom files (PSP)
 * mss -> mission setup script (plain text)
   oss -> object setup script (plain text)
   sch -> scheme setup (plain text)
   tss -> team setup script (plain text)
   txt -> misc. mappings (plain text)
   bin -> localization text packs
 */
using CrateModLoader.GameSpecific.WormsForts;
namespace CrateModLoader.GameSpecific.WormsOW2
{
    public sealed class Modder_WormsOW2 : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                XOM_TYPE.GetSupported();
                if (ConsolePipeline.Metadata.Console == ConsoleMode.PSP)
                {
                    FindArchives(new Pipeline_ZIP(this));
                    await StartPipelines(PipelinePass.Extract);
                }
            }

            FindFiles(new Parser_XOM(this, WormsGame.OW2));
            await StartNewPass();

            if (!ModderIsPreloading && ConsolePipeline.Metadata.Console == ConsoleMode.PSP)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
