using System;
using System.Collections.Generic;
using System.IO;
using CTRFramework;
using CTRFramework.Shared;
using bigtool;
//CTR API by DCxDemo (https://github.com/DCxDemo/CTR-tools) 

namespace CrateModLoader
{
    public sealed class Modder_CTR : Modder
    {
        internal const int RandomizeAdvCharacters = 0;

        public Modder_CTR()
        {
            Game = new Game()
            {
                Name = "Crash Team Racing",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
                API_Credit = "API by DCxDemo",
                API_Link = "https://github.com/DCxDemo/CTR-tools",
                Icon = null,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCUS_944.26;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_944.26",
                    CodeName = "SCUS_94426", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCES_021.05;1",
                    Region = RegionType.PAL,
                    ExecName = "SCES_021.05",
                    CodeName = "SCES_02105", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCPS_101.18;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_101.18",
                    CodeName = "SCPS_10118", },
                },
            };

            //Options.Add(RandomizeAdvCharacters, new ModOption("Randomize adventure mode characters"));
        }

        private string basePath = "";

        public override void StartModProcess()
        {

            string path_Bigfile = "BIGFILE.BIG";

            basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\";

            BIG big = new BIG(basePath + path_Bigfile);
            big.Export();

            ModProcess();
        }

        protected override void ModProcess()
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

        protected override void EndModProcess()
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
