using System;
using System.Collections.Generic;
using System.IO;
using CTRFramework;
using CTRFramework.Lang;
using CTRFramework.Shared;
using bigtool;
using CrateModLoader.ModProperties;
//CTR API by DCxDemo (https://github.com/DCxDemo/CTR-tools) 
/* Mod Layers:
 * 1: BIGFILE.BIG contents
 */

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public sealed class Modder_CTR : Modder
    {
        internal const int RandomizeAdvCharacters = 0;

        public override Game Game => new Game()
        {
            Name = "Crash Team Racing",
            ShortName = "CrashTR",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
            API_Credit = "API by DCxDemo",
            API_Link = "https://github.com/DCxDemo/CTR-tools",
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS1] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SCUS_944.26",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_944.26",
                    CodeName = "SCUS_94426", },
                    new RegionCode() {
                    Name = @"SCES_021.05",
                    Region = RegionType.PAL,
                    ExecName = "SCES_021.05",
                    CodeName = "SCES_02105", },
                    new RegionCode() {
                    Name = @"SCPS_101.18",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_101.18",
                    CodeName = "SCPS_10118", },
                },
            },
        };

        public Modder_CTR()
        {

        }

        public static ModPropOption Option_RandCharacters = new ModPropOption("Randomize Drivers", "") { Hidden = true, };
        public static ModPropOption Option_RandTracks = new ModPropOption("Randomize Track Order", "Shuffles tracks around.");
        public static ModPropOption Option_RandTracksWithDupes = new ModPropOption("Randomize Tracks (With Duplicates)", "Shuffles tracks around, which can repeat.") { Hidden = true, };

        private string basePath = "";

        public override void StartModProcess()
        {

            string path_Bigfile = "BIGFILE.BIG";

            basePath = ConsolePipeline.ExtractedPath;

            bigtool.BIG big;

            switch (GameRegion.Region)
            {
                case RegionType.NTSC_U:
                    big = new bigtool.BIG(basePath + path_Bigfile, false);
                    big.Export();
                    break;
                case RegionType.NTSC_J:
                    big = new bigtool.BIG(basePath + path_Bigfile, false);
                    big.Export();
                    break;
                case RegionType.PAL:
                    big = new bigtool.BIG(basePath + path_Bigfile, true);
                    big.Export();
                    break;
            }

            ModProcess();
        }

        void ModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            string path_extr = ConsolePipeline.ExtractedPath + @"BIGFILE\";

            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            if (Option_RandTracks.Enabled && Directory.Exists(path_extr + @"levels\tracks\island1"))
            {
                List<int> LevelInd = new List<int>();
                List<int> LevelRand = new List<int>();
                for (int i = 0; i < TrackFolderNames.Count; i++)
                {
                    Directory.Move(path_extr + @"levels\tracks\" + TrackFolderNames[i], path_extr + @"levels\tracks\level" + i);
                    LevelInd.Add(i);
                }

                while (LevelInd.Count > 0)
                {
                    int r = rand.Next(LevelInd.Count);
                    LevelRand.Add(LevelInd[r]);
                    LevelInd.RemoveAt(r);
                }

                for (int i = 0; i < LevelRand.Count; i++)
                {
                    Directory.Move(path_extr + @"levels\tracks\level" + i, path_extr + @"levels\tracks\" + TrackFolderNames[LevelRand[i]]);
                }
            }
            
            LNG lng = new LNG(path_extr + @"lang\en.lng");
            string[] lang_lines = File.ReadAllLines(path_extr + @"lang\en.txt", System.Text.Encoding.Default);
            for (int i = 0; i < lang_lines.Length; i++)
            {
                if (lang_lines[i].Contains("LOADING.."))
                {
                    lang_lines[i] = "CML " + ModLoaderGlobals.ProgramVersion + "|" + "SEED: " + ModLoaderGlobals.RandomizerSeed;
                }
            }
            File.WriteAllLines(path_extr + @"lang\en.txt", lang_lines, System.Text.Encoding.Default);
            lng.ConvertTXT(path_extr + @"lang\en.txt");
            File.Delete(path_extr + @"lang\en.txt");

            if (File.Exists(path_extr + @"lang\en2.lng"))
            {
                LNG lng1 = new LNG(path_extr + @"lang\en2.lng");
                string[] lang_lines1 = File.ReadAllLines(path_extr + @"lang\en2.txt", System.Text.Encoding.Default);
                for (int i = 0; i < lang_lines1.Length; i++)
                {
                    if (lang_lines[i].Contains("LOADING.."))
                    {
                        lang_lines[i] = "CML " + ModLoaderGlobals.ProgramVersion + "|" + "SEED: " + ModLoaderGlobals.RandomizerSeed;
                    }
                }
                File.WriteAllLines(path_extr + @"lang\en2.txt", lang_lines1, System.Text.Encoding.Default);
                lng1.ConvertTXT(path_extr + @"lang\en2.txt");
                File.Delete(path_extr + @"lang\en2.txt");
            }

            EndModProcess();
        }

        void EndModProcess()
        {
            string path_Bigfile = "BIGFILE.TXT";

            File.Move(ModLoaderGlobals.BaseDirectory + path_Bigfile, basePath + path_Bigfile);

            bigtool.BIG big = new bigtool.BIG();
            big.Build(basePath, basePath + path_Bigfile);

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

        public List<string> TrackFolderNames = new List<string>()
        {
            "arena2",
            "blimp1",
            "castle1",
            "cave1",
            "coco1",
            "desert2",
            "ice1",
            "island1",
            "labs1",
            "proto8",
            "proto9",
            "secret1",
            "secret2",
            "sewer1",
            "space",
            "temple1",
            "temple2",
            "tube1",
        };
    }
}
