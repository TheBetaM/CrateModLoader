using System.Collections.Generic;
using CrateModLoader.GameSpecific.CrashTTR;
//RCF API by NeoKesha and BetaM
/* Mod Layers:
 * 1: Default.RCF contents (only replace files)
 * Mod Passes:
 * 
 */
namespace CrateModLoader.GameSpecific.CrashTitans
{
    public sealed class Modder_Titans : Modder
    {
        public override async void StartModProcess()
        {
            FindArchives(new Pipeline_RCF(this) { SecondaryList = new List<string>() { "default.rcf", "DEFAULT.RCF" } , SecondarySkip = false, });
            await StartPipelines(PipelinePass.Extract);

            await StartNewPass();

            await StartPipelines(PipelinePass.Build);

            ProcessBusy = false;
        }
    }
}
