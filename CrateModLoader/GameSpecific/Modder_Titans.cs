using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CTTR;
//Titans API by NeoKesha

namespace CrateModLoader
{
    class Modder_Titans
    {
        public string gameName = "Titans";
        public string apiCredit = "API by NeoKesha";
        public System.Drawing.Image gameIcon = Properties.Resources.icon_titans;
        public string[] modOptions = { "No options available" };


        public void OptionChanged(int option, bool value)
        {
            //TODO
        }

        public void StartModProcess()
        {
            string path_RCF_frontend = "DEFAULT.RCF";
            //Fixes names for PS2
            File.Move(Program.ModProgram.extractedPath + path_RCF_frontend + ";1", Program.ModProgram.extractedPath + path_RCF_frontend);

            //Warning: The CTTR API only likes paths with \ backslashes
            string feedback = "";
            string path_extr = "";
            RCF rcf_frontend = new RCF();
            rcf_frontend.OpenRCF(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend);
            path_extr = AppDomain.CurrentDomain.BaseDirectory + @"temp\cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_frontend.ExtractRCF(ref feedback, path_extr);

            // Proof of concept mod TEMP
            /*
            string[] frontend_lines = File.ReadAllLines(path_extr + @"script\CreditsList.txt");
            frontend_lines[3] = "false       \"Modded Titans\"                                               false           true    kforbes";
            File.WriteAllLines(path_extr + @"script\CreditsList.txt", frontend_lines);

            for (int i = 0; i < rcf_frontend.Header.T2File.Length; i++)
            {
                if (rcf_frontend.Header.T2File[i].Name == @"script\CreditsList.txt")
                {
                    rcf_frontend.Header.T2File[i].External = path_extr + @"script\CreditsList.txt";
                    //Console.WriteLine("external " + rcf_frontend.Header.T2File[i].External);
                    break;
                }
            }
            */

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
            File.Move(Program.ModProgram.extractedPath + path_RCF_frontend, Program.ModProgram.extractedPath + path_RCF_frontend + ";1");
        }
    }
}
