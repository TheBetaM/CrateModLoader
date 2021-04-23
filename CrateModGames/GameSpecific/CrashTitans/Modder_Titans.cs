using RadcoreCementFile;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.CrashTTR;
//RCF API by NeoKesha
/* Mod Layers:
 * 1: Default.RCF contents (only replace files)
 * Mod Passes:
 * string -> extraction path
 */

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public sealed class Modder_Titans : Modder
    {
        public Modder_Titans() { }

        private string basePath = "";

        public override void StartModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            string path_RCF_frontend = "DEFAULT.RCF";
            basePath = ConsolePipeline.ExtractedPath;
            RCF_Manager.cachedRCF = null;

            if (ConsolePipeline.Metadata.Console == ConsoleMode.WII)
                path_RCF_frontend = "default.rcf";
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.PSP)
                path_RCF_frontend = "default.rcf";
            else if  (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX360)
                path_RCF_frontend = "default.rcf";

            string path_extr = basePath + @"cml_extr\";

            UpdateProcessMessage("Extracting DEFAULT.RCF...", 5);
            RCF_Manager.Extract(basePath + path_RCF_frontend, path_extr);

            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 6);
            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            UpdateProcessMessage("Cache Pass", 25);
            BeforeCachePass();

            StartCachePass(path_extr);

            UpdateProcessMessage("Mod Pass", 50);
            BeforeModPass();

            StartModPass(path_extr);

            UpdateProcessMessage("Building DEFAULT.RCF...", 95);

            RCF_Manager.Pack(basePath + path_RCF_frontend, path_extr);
        }

        
    }
}
