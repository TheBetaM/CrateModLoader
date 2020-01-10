using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RadcoreCementFile;
//Titans API by NeoKesha

namespace CrateModLoader
{
    class Modder_Titans
    {
        public string[] modOptions = { "No options available" };

        public void OptionChanged(int option, bool value)
        {
            //TODO
        }

        public void UpdateModOptions()
        {
            Program.ModProgram.PrepareOptionsList(modOptions);
        }

        public void StartModProcess()
        {

            if (Program.ModProgram.isoType == ModLoader.ConsoleMode.WII || Program.ModProgram.isoType == ModLoader.ConsoleMode.XBOX360)
            {
                // API doesn't work yet for WII/360 because the archive's too big
                return;
            }

            string path_RCF_frontend = "DEFAULT.RCF";
            //Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + path_RCF_frontend + ";1", Program.ModProgram.extractedPath + path_RCF_frontend);

            //Warning: The RCF API only likes paths with \ backslashes
            string path_extr = "";
            RCF rcf_frontend = new RCF();
            rcf_frontend.OpenRCF(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend);
            path_extr = AppDomain.CurrentDomain.BaseDirectory + @"temp\cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_frontend.ExtractRCF(path_extr);

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

            for (int i = 0; i < rcf_frontend.Header.T2File.Length; i++)
            {
                if (rcf_frontend.Header.T2File[i].Name == @"levels\L1_E1\cameraoverrides.blua")
                {
                    rcf_frontend.Header.T2File[i].External = path_extr + @"levels\L1_E1\cameraoverrides.blua";
                    //Console.WriteLine("external " + rcf_frontend.Header.T2File[i].External);
                    break;
                }
            }
            

            rcf_frontend.Recalculate();
            rcf_frontend.Pack(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend + "1");

            // Extraction cleanup
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend);
            File.Move(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend + "1", AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend);
            if (Directory.Exists(path_extr))
            {
                DirectoryInfo di = new DirectoryInfo(path_extr);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_extr);
            }


            //Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + path_RCF_frontend, Program.ModProgram.extractedPath + path_RCF_frontend + ";1");
        }

        int GetPositionBeforeMatch(byte[] data, byte[] pattern)
        {
            for (int i = 0; i < data.Length - pattern.Length; i++)
            {
                bool match = true;
                for (int k = 0; k < pattern.Length; k++)
                {
                    if (data[i + k] != pattern[k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return i - pattern.Length;
                }
            }
            return -1;
        }
    }
}
