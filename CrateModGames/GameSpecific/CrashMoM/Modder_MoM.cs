using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CrateModLoader.GameSpecific.CrashTTR;
//RCF API by NeoKesha and BetaM
/* Mod Layers:
 * 1: Default.RCF contents (only replace files)
 * Mod Passes:
 * string -> extraction path
 */

namespace CrateModLoader.GameSpecific.CrashMoM
{
    public sealed class Modder_MoM : Modder
    {
        public override async void StartModProcess()
        {
            UpdateProcessMessage("Extracting DEFAULT.RCF...", 5);
            FindArchives(new Pipeline_RCF(this) { SecondaryList = new List<string>() { "default.rcf", "DEFAULT.RCF" }, SecondarySkip = false, });
            await StartPipelines(PipelinePass.Extract);

            string path_extr = ConsolePipeline.ExtractedPath + @"default\";
            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 26);
            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1, true);
            UpdateProcessMessage("Cache Pass", 27);
            BeforeCachePass();
            StartCachePass(path_extr);
            UpdateProcessMessage("Mod Pass", 50);
            BeforeModPass();
            StartModPass(path_extr);

            UpdateProcessMessage("Packing DEFAULT.RCF...", 75);
            await StartPipelines(PipelinePass.Build);

            ProcessBusy = false;
        }
    }
}
