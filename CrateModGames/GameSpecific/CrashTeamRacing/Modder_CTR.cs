using System;
using System.Collections.Generic;
using System.IO;
using bigtool;

//CTR API by DCxDemo (https://github.com/DCxDemo/CTR-tools) 
/* Mod Layers:
 * 1: BIGFILE.BIG contents
 * Mod Passes:
 * string -> BIGFILE contents path
 */

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public sealed class Modder_CTR : Modder
    {
        public Modder_CTR() { }

        public override void StartModProcess()
        {
            string path_Bigfile = "BIGFILE.BIG";
            string basePath = ConsolePipeline.ExtractedPath;
            string path_extr = ConsolePipeline.ExtractedPath + @"BIGFILE\";
            BIG big;

            UpdateProcessMessage("Extracting BIGFILE.BIG...", 5);

            switch (GameRegion.Region)
            {
                case RegionType.NTSC_U:
                    big = new BIG(basePath + path_Bigfile, false);
                    big.Export();
                    break;
                case RegionType.NTSC_J:
                    big = new BIG(basePath + path_Bigfile, false);
                    big.Export();
                    break;
                case RegionType.PAL:
                    big = new BIG(basePath + path_Bigfile, true);
                    big.Export();
                    break;
            }
            
            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 6);
            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            UpdateProcessMessage("Cache Pass", 25);

            BeforeCachePass();

            StartCachePass(path_extr);

            UpdateProcessMessage("Mod Pass", 50);

            BeforeModPass();

            StartModPass(path_extr);

            UpdateProcessMessage("Building BIGFILE.BIG...", 90);

            File.Move(ModLoaderGlobals.BaseDirectory + path_Bigfile, basePath + path_Bigfile);

            BIG big1 = new BIG();
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
    }
}
