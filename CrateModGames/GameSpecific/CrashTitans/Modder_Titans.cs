using RadcoreCementFile;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.CrashTTR;
//RCF API by NeoKesha
/* Mod Layers:
 * 1: Default.RCF contents (only replace files)
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

            UpdateProcessMessage("Mod Pass", 50);

            if (Titans_Props_Main.Option_TestMod.Enabled)
            {
                // Proof of concept mod increasing gameplay FOV in Episode 1
                string[] frontend_lines = File.ReadAllLines(path_extr + @"levels\L1_E1\cameraoverrides.blua");
                frontend_lines[6] = "cameraManager:SetCameraVolumeFOV( 0, 90.000000 )";
                frontend_lines[11] = "cameraManager:SetCameraVolumeFOV( 2, 90.000000 )";
                frontend_lines[14] = "cameraManager:SetCameraVolumeFOV( 3, 90.000000 )";
                frontend_lines[68] = "cameraManager:SetCameraVolumeFOV( 78, 90.000000 )";
                frontend_lines[71] = "cameraManager:SetCameraVolumeFOV( 79, 90.000000 )";
                frontend_lines[73] = "cameraManager:SetCameraVolumeFOV( 80, 90.000000 )";
                frontend_lines[76] = "cameraManager:SetCameraVolumeFOV( 81, 90.000000 )";
                frontend_lines[79] = "cameraManager:SetCameraVolumeFOV( 82, 90.000000 )";
                frontend_lines[81] = "cameraManager:SetCameraVolumeFOV( 83, 90.000000 )";
                frontend_lines[83] = "cameraManager:SetCameraVolumeFOV( 84, 90.000000 )";
                frontend_lines[85] = "cameraManager:SetCameraVolumeFOV( 85, 90.000000 )";
                frontend_lines[87] = "cameraManager:SetCameraVolumeFOV( 86, 90.000000 )";
                frontend_lines[89] = "cameraManager:SetCameraVolumeFOV( 87, 90.000000 )";
                frontend_lines[91] = "cameraManager:SetCameraVolumeFOV( 88, 90.000000 )";
                frontend_lines[94] = "cameraManager:SetCameraVolumeFOV( 89, 90.000000 )";
                frontend_lines[97] = "cameraManager:SetCameraVolumeFOV( 90, 90.000000 )";
                File.WriteAllLines(path_extr + @"levels\L1_E1\cameraoverrides.blua", frontend_lines);
            }

            if (Titans_Props_Main.Option_RandEpisodeOrder.Enabled)
            {
                List<int> LevelsToRand = new List<int>();
                for (int i = 0; i < EpisodeFolderNames.Count; i++)
                {
                    LevelsToRand.Add(i);
                    Directory.Move(path_extr + @"levels\" + EpisodeFolderNames[i], path_extr + @"levels\" + "level" + i);
                }

                List<int> LevelsRand = new List<int>();
                for (int i = 0; i < EpisodeFolderNames.Count; i++)
                {
                    int r = rand.Next(LevelsToRand.Count);
                    LevelsRand.Add(LevelsToRand[r]);
                    LevelsToRand.RemoveAt(r);
                }

                for (int i = 0; i < EpisodeFolderNames.Count; i++)
                {
                    Directory.Move(path_extr + @"levels\" + "level" + i, path_extr + @"levels\" + EpisodeFolderNames[LevelsRand[i]]);
                }
            }

            UpdateProcessMessage("Building DEFAULT.RCF...", 95);

            RCF_Manager.Pack(basePath + path_RCF_frontend, path_extr);
        }

        List<string> EpisodeFolderNames = new List<string>()
        {
            "L1_E1",
            "L1_E2",
            "L1_E3",
            "L1_E4",
            "L2_E1",
            "L2_E2",
            "L2_E4",
            "L3_E1",
            "L3_E1A",
            "L3_E1B",
            "L3_E2",
            "L3_E3",
            "L3_E4",
            "L4_E1",
            "L4_E2",
            "L4_E3",
            "L4_E4",
            "L5_E1",
            "L5_E2",
            "L5_E3",
        };
    }
}
