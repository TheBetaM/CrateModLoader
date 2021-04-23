using System;
using System.Collections.Generic;
using System.IO;
using CTRFramework.Big;
using CTRFramework.Shared;

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
            string path_txt = "BIGFILE.TXT";
            string basePath = ConsolePipeline.ExtractedPath;
            string path_extr = ConsolePipeline.ExtractedPath + @"BIGFILE\";

            UpdateProcessMessage("Extracting BIGFILE.BIG...", 5);

            try
            {
                BigFile big = BigFile.FromFile(Path.Combine(basePath, path_Bigfile));
                big.Extract(path_extr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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

            File.Delete(Path.Combine(basePath, path_Bigfile));
            File.Move(ModLoaderGlobals.BaseDirectory + ".txt", Path.Combine(basePath, path_txt));
            Directory.Move(path_extr, Path.Combine(ModLoaderGlobals.BaseDirectory, @"BIGFILE\"));

            UpdateProcessMessage("Building BIGFILE.BIG...", 91);

            try
            {
                BigFile big = BigFile.FromFile(Path.Combine(basePath, path_txt));
                big.Save(Path.Combine(basePath, path_Bigfile));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            UpdateProcessMessage("Removing temporary files...", 95);
            Directory.Move(Path.Combine(ModLoaderGlobals.BaseDirectory, @"BIGFILE\"), path_extr);
            File.Delete(Path.Combine(basePath, path_txt));

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
        }
    }
}
