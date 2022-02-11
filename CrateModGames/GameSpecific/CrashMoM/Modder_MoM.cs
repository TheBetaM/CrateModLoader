using System.Collections.Generic;
using CrateModLoader.GameSpecific.CrashTTR;
//RCF API by NeoKesha and BetaM
/* Mod Layers:
 * 1: Default.RCF contents (only replace files)
 * Mod Passes:
 * 
 */
namespace CrateModLoader.GameSpecific.CrashMoM
{
    public sealed class Modder_MoM : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                FindArchives(new Pipeline_RCF(this) { SecondaryList = new List<string>() { "default.rcf", "DEFAULT.RCF" }, SecondarySkip = false, });
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
