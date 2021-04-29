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

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public sealed class Modder_Titans : Modder
    {
        private int CurrentPass = 0;
        private float PassPercentMod = 49f;
        private int PassPercentAdd = 1;

        public override void StartModProcess()
        {
            ProcessBusy = true;
            ModProcess();
        }

        public async void ModProcess()
        {
            PassIterator = 0;
            PassCount = 1;

            string path_RCF_frontend = "DEFAULT.RCF";
            string basePath = ConsolePipeline.ExtractedPath;
            RCF_Manager rcf = new RCF_Manager(this, basePath + path_RCF_frontend);

            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
                path_RCF_frontend = "default.rcf";

            string path_extr = basePath + @"default_ex\";
            UpdateProcessMessage("Extracting DEFAULT.RCF...", 5);

            PassBusy = true;
            await rcf.ExtractAsync(this, basePath + path_RCF_frontend, path_extr);
            PassBusy = false;
            int FileCount = PassCount;

            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 26);
            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1, true);

            UpdateProcessMessage("Cache Pass", 27);

            BeforeCachePass();

            StartCachePass(path_extr);

            UpdateProcessMessage("Mod Pass", 50);

            BeforeModPass();

            StartModPass(path_extr);

            UpdateProcessMessage("Packing DEFAULT.RCF...", 75);

            PassIterator = 0;
            PassCount = FileCount;
            PassBusy = true;
            await rcf.PackAsync(basePath + path_RCF_frontend, path_extr);
            PassBusy = false;


            ProcessBusy = false;
        }

        
    }
}
