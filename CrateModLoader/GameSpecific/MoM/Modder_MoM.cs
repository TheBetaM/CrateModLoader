using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CTTR;
//MoM API by NeoKesha
//Version number, seed and options are displayed in the Credits accessible from the main menu.

namespace CrateModLoader
{
    class Modder_MoM
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

            //Warning: The CTTR API only likes paths with \ backslashes
            string feedback = "";
            string path_extr = "";
            RCF rcf_frontend = new RCF();
            rcf_frontend.OpenRCF(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend);
            path_extr = AppDomain.CurrentDomain.BaseDirectory + @"temp\cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_frontend.ExtractRCF(ref feedback, path_extr);

            // Proof of concept mod replacing credits text
            string[] credits_lines = File.ReadAllLines(path_extr + @"script\CreditsList.txt");
            //credits_lines[3] = "false       \"Modded MoM\"                                               false           true    kforbes";

            List<string> credits_LineList = new List<string>();
            credits_LineList.Add(credits_lines[0]);

            credits_LineList.Add("false        \"Crate Mod Loader " + Program.ModProgram.releaseVersionString + "\"                 false           false");
            credits_LineList.Add("false        \"Seed: " + Program.ModProgram.randoSeed + "\"                 false           false");
            credits_LineList.Add("false        \"Options: " + Program.ModProgram.optionsSelectedString + "\"                 false           false");

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

            for (int i = 0; i < rcf_frontend.Header.T2File.Length; i++)
            {
                if (rcf_frontend.Header.T2File[i].Name == @"script\CreditsList.txt")
                {
                    rcf_frontend.Header.T2File[i].External = path_extr + @"script\CreditsList.txt";
                    //Console.WriteLine("external " + rcf_frontend.Header.T2File[i].External);
                    break;
                }
            }

            rcf_frontend.Recalculate();
            rcf_frontend.Pack(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend + "1", ref feedback);

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
    }
}
