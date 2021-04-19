using System;
using System.Collections.Generic;
using System.IO;
using CTRFramework;
using CTRFramework.Lang;
using CTRFramework.Shared;
using bigtool;
//CTR API by DCxDemo (https://github.com/DCxDemo/CTR-tools) 
/* Mod Layers:
 * 1: BIGFILE.BIG contents
 */

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public sealed class Modder_CTR : Modder
    {

        public Modder_CTR()
        {

        }

        private string basePath = "";

        public override void StartModProcess()
        {

            string path_Bigfile = "BIGFILE.BIG";

            basePath = ConsolePipeline.ExtractedPath;

            bigtool.BIG big;

            UpdateProcessMessage("Extracting BIGFILE.BIG...", 5);

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

            UpdateProcessMessage("Building BIGFILE.BIG...", 90);

            File.Move(ModLoaderGlobals.BaseDirectory + path_Bigfile, basePath + path_Bigfile);

            bigtool.BIG big1 = new bigtool.BIG();
            big1.Build(basePath, basePath + path_Bigfile);

            UpdateProcessMessage("Removing temporary files...", 95);

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

        void ModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            string path_extr = ConsolePipeline.ExtractedPath + @"BIGFILE\";
            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 6);
            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            UpdateProcessMessage("Mod Pass", 50);

            if ((CTR_Props_Main.Option_RandTracks.Enabled || CTR_Props_Main.Option_RandTracks101.Enabled) && Directory.Exists(path_extr + @"levels\tracks\island1"))
            {
                List<int> LevelInd = new List<int>();
                List<int> LevelRand = new List<int>();
                int maxLevel = TrackFolderNames.Count;
                if (CTR_Props_Main.Option_RandTracks101.Enabled)
                {
                    maxLevel -= 2;
                }

                for (int i = 0; i < maxLevel; i++)
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

            #region Mod_Metadata
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
            #endregion

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
            "sewer1",
            "space",
            "temple1",
            "temple2",
            "tube1",

            "secret1",
            "secret2",
        };
    }
}
