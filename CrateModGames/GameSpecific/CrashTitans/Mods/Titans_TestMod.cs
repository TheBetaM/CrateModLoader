using System;
using System.Collections.Generic;
using System.IO;
using Pure3D;
using Pure3D.Chunks;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public class Titans_TestMod : ModStruct<string>
    {
        public override void ModPass(string path_extr)
        {
            // Proof of concept mod increasing gameplay FOV in Episode 1
            /*
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
            */

            /*
            List<string> lines = new List<string>(File.ReadAllLines(path_extr + @"hashdictionary.txt"));

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == "cinematics/characters_crash_nis_01.p3d c6b664d5")
                {
                    lines[i] = "cinematics/characters_crash_nis_01.p3d bf7e0d25";
                }
                else if (lines[i] == "cinematics/characters_crash_parafox_nis_01.p3d bf7e0d25")
                {
                    lines[i] = "cinematics/characters_crash_parafox_nis_01.p3d c6b664d5";
                }
            }

            File.WriteAllLines(path_extr + @"hashdictionary.txt", lines.ToArray());
            */

            /*
            string file1 = path_extr + "package/344a15f9.p3d";
            string file2 = path_extr + "package/b06ecd92.p3d"; //orig
            System.IO.File.Move(file2, file1 + "1");
            System.IO.File.Move(file1, file2 + "1");
            System.IO.File.Move(file1 + "1", file1);
            System.IO.File.Move(file2 + "1", file2);
            */

            /*
            bool skip = false;
            string fileName = path_extr + "package/cdd70a8c.p3d";
            Pure3D.File file = new Pure3D.File();
            try
            {
                file.Load(fileName);
            }
            catch
            {
                Console.WriteLine("Failed to load");
                skip = true;
            }

            if (file.RootChunk.GetChildByName<FrontendTextBible>("frontend") != null)
            {
                foreach (Chunk chunk in file.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children)
                {
                    FrontendLanguage lang = (FrontendLanguage)chunk;
                    lang.TextStrings[45] = "Really long loading text for testing!";
                    for (int i = 0; i < lang.TextStrings.Count; i++)
                    {
                        if (lang.TextStrings[i] == "RADICAL ENTERTAINMENT" || lang.TextStrings[i] == "DEVELOPED BY SUPERVILLAIN STUDIOS" || lang.TextStrings[i] == "SENIOR PRODUCER")
                        {
                            lang.TextStrings[i] = "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + " Seed: " + ModLoaderGlobals.RandomizerSeed;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to find text");
            }


            if (!skip)
            {
                file.Save(fileName);
            }
            */
        }
    }
}
