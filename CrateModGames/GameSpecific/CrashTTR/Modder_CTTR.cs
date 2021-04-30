using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
//RCF API by NeoKesha and BetaM
//Pure3D API by BetaM (based on https://github.com/handsomematt/Pure3D)
/* 
 * Mod Layers:
 * 1: All .RCF file contents (only replace files)
 * Mod Passes:
 * god -> LUA scripts (plain text)
 * mid -> roads (not yet implemented)
 * p3d -> Pure3D files
 * fig -> fight trees (plain text) (not yet implemented)
 */

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public sealed class Modder_CTTR : Modder
    {
        public override async void StartModProcess()
        {
            string basePath = ConsolePipeline.ExtractedPath;

            UpdateProcessMessage("Extracting all RCF archives...", 5);
            FindArchives(new Pipeline_RCF(this));
            await StartPipelines(PipelinePass.Extract);

            // Mod all RCF
            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 25);
            ModCrates.InstallLayerMods(EnabledModCrates, basePath, 1, true);
            /*
            for (int i = 0; i < RCF_Paths.Count; i++)
            {
                // Only allowing overwrite at the moment
                ModCrates.InstallLayerMods(EnabledModCrates, RCF_Paths[i].Substring(0, RCF_Paths[i].Length - 4) + @"\", 1, true);
            }
            */

            //Mods
            FindFiles(new Parser_GOD(this), new Parser_P3D(this));
            await StartNewPass();

            UpdateProcessMessage("Packing all RCF archives...", 75);
            await StartPipelines(PipelinePass.Build);

            ProcessBusy = false;
        }
    }
}
