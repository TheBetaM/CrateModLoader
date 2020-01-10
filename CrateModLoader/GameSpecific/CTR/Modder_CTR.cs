using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CTRFramework;
using CTRFramework.Shared;
using bigtool;
//CTR API by DCxDemo

namespace CrateModLoader
{
    class Modder_CTR
    {
        public string[] modOptions = { "No options available" };

        public enum CTR_Options
        {
            RandomizeAdvCharacters = 0,
        }

        public bool CTR_Rand_1PCharacters = false;

        public void OptionChanged(int option, bool value)
        {
            if (option == (int)CTR_Options.RandomizeAdvCharacters)
            {
                CTR_Rand_1PCharacters = value;
            }
        }

        public void UpdateModOptions()
        {
            Program.ModProgram.PrepareOptionsList(modOptions);
        }

        private string basePath = "";

        public void StartModProcess()
        {

            string path_Bigfile = "BIGFILE.BIG";

            basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\";

            BIG big = new BIG(basePath + path_Bigfile);
            big.Export();

            ModProcess();
        }

        public void ModProcess()
        {

            string path_extr = AppDomain.CurrentDomain.BaseDirectory + @"temp\BIGFILE\";

            // Inserting CML metadata, doesn't work yet

            /*
            LNG lng = new LNG(path_extr + @"lang\en.lng");
            string[] lang_lines = File.ReadAllLines(path_extr + @"lang\en.txt");
            for (int i = 0; i < lang_lines.Length; i++)
            {
                if (lang_lines[i] == "LOADING...")
                {
                    lang_lines[i] = "CML " + Program.ModProgram.releaseVersionString + "..";
                }
            }
            File.WriteAllLines(path_extr + @"lang\en.txt", lang_lines);
            lng.ConvertTXT(path_extr + @"lang\en.txt");
            File.Delete(path_extr + @"lang\en.txt");

            if (File.Exists(path_extr + @"lang\en2.lng"))
            {
                LNG lng1 = new LNG(path_extr + @"lang\en2.lng");
                string[] lang_lines1 = File.ReadAllLines(path_extr + @"lang\en2.txt");
                for (int i = 0; i < lang_lines1.Length; i++)
                {
                    if (lang_lines[i] == "LOADING...")
                    {
                        lang_lines[i] = "CML " + Program.ModProgram.releaseVersionString + "..";
                    }
                }
                File.WriteAllLines(path_extr + @"lang\en2.txt", lang_lines1);
                lng1.ConvertTXT(path_extr + @"lang\en2.txt");
                File.Delete(path_extr + @"lang\en2.txt");
            }
            */
            

            EndModProcess();
        }

        public void EndModProcess()
        {
            string path_Bigfile = "BIGFILE.TXT";

            File.Move(AppDomain.CurrentDomain.BaseDirectory + path_Bigfile, basePath + path_Bigfile);

            BIG big = new BIG();
            big.Build(basePath + path_Bigfile);

            // Extraction cleanup
            if (Directory.Exists(basePath + @"BIGFILE\"))
            {
                DirectoryInfo di = new DirectoryInfo(basePath + @"BIGFILE\");

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(basePath + @"BIGFILE\");
            }
            if (File.Exists(basePath + path_Bigfile))
            {
                File.Delete(basePath + path_Bigfile);
            }


        }
    }
}
