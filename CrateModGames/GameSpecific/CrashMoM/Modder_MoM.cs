﻿using RadcoreCementFile;
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

        public override Game Game => new Game()
        {
            Name = "Crash Mind Over Mutant",
            ShortName = "CrashMOM",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.PSP,
                    ConsoleMode.WII,
                    ConsoleMode.XBOX360,
                },
            API_Credit = "API by NeoKesha and BetaM",
            API_Link = string.Empty,
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_217.28",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_217.28",
                    CodeName = "SLUS_21728", },
                    new RegionCode() {
                    Name = @"SLES_552.04",
                    Region = RegionType.PAL,
                    ExecName = "SLES_552.04",
                    CodeName = "SLES_55204", },
                },
                [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10377",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-01171",
                    Region = RegionType.PAL },
                },
                [ConsoleMode.WII] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "RC8E7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "RC8P7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "RC8X7D",
                    Region = RegionType.PAL },
                },
                [ConsoleMode.XBOX360] = new RegionCode[]
                {
                    new RegionCode() {
                        Name = "Crash Mind Over Mutant",
                        Region = RegionType.Global, }
                },
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [0] = "Options",
            },
        };

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
            {
                path_RCF_frontend = "default.rcf";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.PSP)
            {
                path_RCF_frontend = "default.rcf";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX360)
            {
                path_RCF_frontend = "default.rcf";
            }

            string path_extr = basePath + @"cml_extr\";
            RCF_Manager.Extract(basePath + path_RCF_frontend, path_extr);

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

            RCF_Manager.Pack(basePath + path_RCF_frontend, path_extr);

        }
    }
}
