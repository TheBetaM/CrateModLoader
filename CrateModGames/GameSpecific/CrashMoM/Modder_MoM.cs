using RadcoreCementFile;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.CrashTTR;
//RCF API by NeoKesha
/* Mod Layers:
 * 1: Default.RCF contents (only replace files)
 */

namespace CrateModLoader.GameSpecific.CrashMoM
{
    public sealed class Modder_MoM : Modder
    {

        public Modder_MoM()
        {
            
        }

        internal string basePath = "";

        public override void StartModProcess()
        {
            string path_RCF_frontend = "DEFAULT.RCF";
            basePath = ConsolePipeline.ExtractedPath;
            RCF_Manager.cachedRCF = null;

            if (ConsolePipeline.Metadata.Console == ConsoleMode.WII)
                path_RCF_frontend = "default.rcf";
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.PSP)
                path_RCF_frontend = "default.rcf";
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX360)
                path_RCF_frontend = "default.rcf";

            string path_extr = basePath + @"cml_extr\";
            UpdateProcessMessage("Extracting DEFAULT.RCF...", 5);
            RCF_Manager.Extract(basePath + path_RCF_frontend, path_extr);

            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 6);
            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            UpdateProcessMessage("Mod Pass", 50);

            #region Mod_Metadata
            // Proof of concept mod replacing credits text
            string[] credits_lines = File.ReadAllLines(path_extr + @"script\CreditsList.txt");

            List<string> credits_LineList = new List<string>();
            credits_LineList.Add(credits_lines[0]);

            credits_LineList.Add("false        \"Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + "\"                 false           false");
            credits_LineList.Add("false        \"Seed: " + ModLoaderGlobals.RandomizerSeed + "\"                 false           false");

            for (int i = 1; i < credits_lines.Length; i++)
            {
                credits_LineList.Add(credits_lines[i]);
            }

            credits_lines = new string[credits_LineList.Count];
            for (int i = 0; i < credits_LineList.Count; i++)
            {
                credits_lines[i] = credits_LineList[i];
            }
            File.WriteAllLines(path_extr + @"script\CreditsList.txt", credits_lines);
            #endregion

            UpdateProcessMessage("Building DEFAULT.RCF...", 95);
            RCF_Manager.Pack(basePath + path_RCF_frontend, path_extr);

        }
    }
}
