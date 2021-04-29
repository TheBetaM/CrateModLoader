using System;
using System.Collections.Generic;
using System.IO;
using CTRFramework.Big;
using CTRFramework.Shared;
using CTRFramework;
using CTRFramework.Vram;
//CTR API by DCxDemo (https://github.com/DCxDemo/CTR-tools) 
/* Mod Layers:
 * 1: BIGFILE.BIG contents
 * Mod Passes:
 * LNG -> language files
 * CTR -> model files
 * LEV -> level files
 * string -> BIGFILE contents path
 */

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public sealed class Modder_CTR : Modder
    {
        public override void StartModProcess()
        {
            ProcessBusy = true;
            ModProcess();
        }

        public async void ModProcess()
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
                big = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 6);
            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            // Mods
            FindFiles(new Parser_LNG(this), new Parser_LEV(this), new Parser_CTR(this));
            await StartNewPass();

            File.Delete(Path.Combine(basePath, path_Bigfile));
            File.Move(ModLoaderGlobals.BaseDirectory + ".txt", Path.Combine(basePath, path_txt));
            Directory.Move(path_extr, Path.Combine(ModLoaderGlobals.BaseDirectory, @"BIGFILE\"));

            UpdateProcessMessage("Building BIGFILE.BIG...", 95);

            try
            {
                BigFile big = BigFile.FromFile(Path.Combine(basePath, path_txt));
                big.Save(Path.Combine(basePath, path_Bigfile));
                big = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            UpdateProcessMessage("Removing temporary files...", 98);
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

            ProcessBusy = false;
        }


        void Recursive_ExtractMPKs(DirectoryInfo di, string bigpath)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_ExtractMPKs(dir, bigpath);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Extension.ToLower() == ".mpk")
                    Extract_MPK(file, bigpath);
            }
        }

        private void Extract_MPK(FileInfo file, string bigPath)
        {
            string vrampath = Path.ChangeExtension(file.FullName, "vrm");
            if (!File.Exists(vrampath))
            {
                vrampath = bigPath + @"packs\shared.vrm";
                if (!File.Exists(vrampath))
                {
                    Console.WriteLine("Warning! No vram file found.\r\nPlease put shared.vrm file with mpk you want to extract.");
                    vrampath = "";
                }
            }

            ModelPack mpk = ModelPack.FromFile(file.FullName);
            mpk.Extract(file.FullName, CtrVrm.FromFile(vrampath));
        }
    }
}
