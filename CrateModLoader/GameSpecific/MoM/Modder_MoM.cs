using RadcoreCementFile;
using System;
using System.Collections.Generic;
using System.IO;
//RCF API by NeoKesha
//Version number, seed and options are displayed in the Credits accessible from the main menu.

namespace CrateModLoader
{
    public sealed class Modder_MoM : Modder
    {
        public Modder_MoM()
        {
            Game = new Game()
            {
                Name = "Crash Mind Over Mutant",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.PSP,
                    ConsoleMode.WII,
                    ConsoleMode.XBOX360,
                },
                API_Credit = "API by NeoKesha",
                Icon = Properties.Resources.icon_titans,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_217.28;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_217.28",
                    CodeName = "SLUS_21728", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_552.04;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_552.04",
                    CodeName = "SLES_55204", },
                },
                RegionID_PSP = new RegionCode[] {
                    new RegionCode() {
                    Name = "ULUS-10377",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-01171",
                    Region = RegionType.PAL },
                },
                RegionID_WII = new RegionCode[] {
                    new RegionCode() {
                    Name = "RC8E7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "RC8P7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "RC8X7D",
                    Region = RegionType.PAL },
                }
            };
        }

        internal string basePath = "";

        public override void StartModProcess()
        {
            string path_RCF_frontend = "DEFAULT.RCF";

            basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\";
            if (Program.ModProgram.isoType == ConsoleMode.WII)
            {
                path_RCF_frontend = "default.rcf";
                basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\DATA\files\";
            }
            else if (Program.ModProgram.isoType == ConsoleMode.PSP)
            {
                path_RCF_frontend = "default.rcf";
                basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\PSP_GAME\USRDIR\";
            }

            //Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + path_RCF_frontend + ";1", Program.ModProgram.extractedPath + path_RCF_frontend);
            RCF rcf_frontend = new RCF();
            rcf_frontend.OpenRCF(basePath + path_RCF_frontend);

            //Warning: The RCF API only likes paths with \ backslashes
            string path_extr = AppDomain.CurrentDomain.BaseDirectory + @"temp\cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_frontend.ExtractRCF(path_extr);

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
            rcf_frontend.Pack(basePath + path_RCF_frontend + "1");

            // Extraction cleanup
            File.Delete(basePath + path_RCF_frontend);
            File.Move(basePath + path_RCF_frontend + "1", basePath + path_RCF_frontend);
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
