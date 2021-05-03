using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//CNK Tools/API by BetaM, ManDude and eezstreet.
/* 
 * Mod Layers:
 * 1: ASSETS.GOB contents
 * Mod Passes:
 * CNK_GenericMod -> extraction paths, console metadata
 * CSV -> CSV table data
 * IGB -> to be implemented
 */

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public enum ModProps : int
    {
        KartStats = 1,
        DriverStats = 2,
        Surfaces = 3,
        Powerups = 4,
        Adventure = 5,
        Textures = 6,
    }

    public sealed class Modder_CNK : Modder
    {
        public override bool CanPreloadGame => true;

        public override async void StartModProcess()
        {
            string path_gob_extracted = ConsolePipeline.ExtractedPath + @"\ASSETS\";

            UpdateProcessMessage("Extracting ASSETS.GOB...", 5);
            FindArchives(new Pipeline_GOB(this));
            await StartPipelines(PipelinePass.Extract);

            FindFiles(new Parser_CSV(this));
            await StartNewPass();

            UpdateProcessMessage("Building ASSETS.GOB...", 97);
            await StartPipelines(PipelinePass.Build);

            // Extraction cleanup
            UpdateProcessMessage("Removing temporary files...", 99);
            if (Directory.Exists(path_gob_extracted))
            {
                DirectoryInfo di = new DirectoryInfo(path_gob_extracted);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                try
                {
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                    Directory.Delete(path_gob_extracted);
                }
                catch
                {

                }

            }

            ProcessBusy = false;
        }

        public override async void StartPreload()
        {
            UpdateProcessMessage("Extracting ASSETS.GOB...", 5);
            FindArchives(new Pipeline_GOB(this));
            await StartPipelines(PipelinePass.Extract);

            FindFiles();
            await StartPreloadPass();

            ProcessBusy = false;
        }

        
    }
}
