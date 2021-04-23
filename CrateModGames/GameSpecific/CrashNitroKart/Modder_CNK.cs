using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//CNK Tools/API by BetaM, ManDude and eezstreet.
/* 
 * Mod Layers:
 * 1: ASSETS.GOB contents
 * Mod Passes:
 * string -> GOB extraction path
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

        public Modder_CNK() { }

        public override void StartModProcess()
        {
            string path_gob_extracted = "";
            string relativePath = ConsolePipeline.ProcessPath;
            string extrPath = ConsolePipeline.ExtractedPath;

            UpdateProcessMessage("Extracting ASSETS.GOB...", 5);

            #region Extract GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                GobExtract.StartInfo.Arguments = relativePath + "assets.gob" + " " + relativePath + "cml_extr";
            }
            else
            {
                GobExtract.StartInfo.Arguments = relativePath + "ASSETS.GOB" + " " + relativePath + "cml_extr";
            }
            GobExtract.Start();
            GobExtract.WaitForExit();

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                File.Delete(extrPath + "ASSETS.GFC");
                File.Delete(extrPath + "ASSETS.GOB");
            }
            else
            {
                File.Delete(extrPath + "assets.gfc");
                File.Delete(extrPath + "assets.gob");
            }
            #endregion

            path_gob_extracted = extrPath + @"\cml_extr\";

            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 6);
            ModCrates.InstallLayerMods(EnabledModCrates, path_gob_extracted, 1);

            //todo improve
            foreach (ModPropertyBase mod in Props)
            {
                if (mod.HasChanged)
                {
                    if (mod.Category == (int)ModProps.KartStats)
                    {
                        CNK_Props_Main.Option_RandPowerupDistribution.Value = 1;
                        CNK_Props_Main.Option_RandPowerupDistribution.HasChanged = true;
                    }
                    else if (mod.Category == (int)ModProps.DriverStats)
                    {
                        CNK_Props_Main.Option_RandCharStats.Value = 1;
                        CNK_Props_Main.Option_RandCharStats.HasChanged = true;
                    }
                    else if (mod.Category == (int)ModProps.Powerups)
                    {
                        CNK_Props_Main.Option_RandWeaponEffects.Value = 1;
                        CNK_Props_Main.Option_RandWeaponEffects.HasChanged = true;
                    }
                    else if (mod.Category == (int)ModProps.Surfaces)
                    {
                        CNK_Props_Main.Option_RandSurfParams.Value = 1;
                        CNK_Props_Main.Option_RandSurfParams.HasChanged = true;
                    }
                }
            }

            UpdateProcessMessage("Mod Pass", 25);
            BeforeModPass();

            StartModPass(path_gob_extracted);

            UpdateProcessMessage("Handling custom textures...", 70);

            HandleTextures(path_gob_extracted);

            UpdateProcessMessage("Building ASSETS.GOB...", 90);

            #region Build GOB
            GobExtract = new Process();
            GobExtract.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                GobExtract.StartInfo.Arguments = relativePath + "ASSETS.GOB" + " " + relativePath + "cml_extr" + " -create 1";
            }
            else
            {
                GobExtract.StartInfo.Arguments = relativePath + "assets.gob" + " " + relativePath + "cml_extr" + " -create 1";
            }
            GobExtract.Start();
            GobExtract.WaitForExit();
            #endregion

            // Extraction cleanup
            UpdateProcessMessage("Removing temporary files...", 95);
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
        }

        void HandleTextures(string path_gob_extracted)
        {
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "ps2/gfx/hud/8-bit_hud.png");
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "gcn/gfx/hud/8-bit_hud.png");
            }
            else
            {
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "xbox/gfx/hud/8-bit_hud.png");
            }

            CNK_Data_Textures.Texture_Font.ResourceToFile(path_gob_extracted + "common/fonts/all_fonts.png");

            CNK_Data_Textures.Texture_Load_Arena1.ResourceToFile(path_gob_extracted + "common/load/arena1.png");
            CNK_Data_Textures.Texture_Load_Arena2.ResourceToFile(path_gob_extracted + "common/load/arena2.png");
            CNK_Data_Textures.Texture_Load_Arena3.ResourceToFile(path_gob_extracted + "common/load/arena3.png");
            CNK_Data_Textures.Texture_Load_Arena4.ResourceToFile(path_gob_extracted + "common/load/arena4.png");
            CNK_Data_Textures.Texture_Load_Arena5.ResourceToFile(path_gob_extracted + "common/load/arena5.png");
            CNK_Data_Textures.Texture_Load_Barin1.ResourceToFile(path_gob_extracted + "common/load/barin1.png");
            CNK_Data_Textures.Texture_Load_Barin2.ResourceToFile(path_gob_extracted + "common/load/barin2.png");
            CNK_Data_Textures.Texture_Load_Barin3.ResourceToFile(path_gob_extracted + "common/load/barin3.png");
            CNK_Data_Textures.Texture_Load_Citadel.ResourceToFile(path_gob_extracted + "common/load/citadel.png");
            CNK_Data_Textures.Texture_Load_Earth1.ResourceToFile(path_gob_extracted + "common/load/earth1.png");
            CNK_Data_Textures.Texture_Load_Earth2.ResourceToFile(path_gob_extracted + "common/load/earth2.png");
            CNK_Data_Textures.Texture_Load_Earth3.ResourceToFile(path_gob_extracted + "common/load/earth3.png");
            CNK_Data_Textures.Texture_Load_Fenom1.ResourceToFile(path_gob_extracted + "common/load/fenom1.png");
            CNK_Data_Textures.Texture_Load_Fenom2.ResourceToFile(path_gob_extracted + "common/load/fenom2.png");
            CNK_Data_Textures.Texture_Load_Fenom3.ResourceToFile(path_gob_extracted + "common/load/fenom3.png");
            CNK_Data_Textures.Texture_Load_Hub1.ResourceToFile(path_gob_extracted + "common/load/hub1.png");
            CNK_Data_Textures.Texture_Load_Hub2.ResourceToFile(path_gob_extracted + "common/load/hub2.png");
            CNK_Data_Textures.Texture_Load_Hub3.ResourceToFile(path_gob_extracted + "common/load/hub3.png");
            CNK_Data_Textures.Texture_Load_Hub4.ResourceToFile(path_gob_extracted + "common/load/hub4.png");
            CNK_Data_Textures.Texture_Load_Hub5.ResourceToFile(path_gob_extracted + "common/load/hub5.png");
            CNK_Data_Textures.Texture_Load_Teknee1.ResourceToFile(path_gob_extracted + "common/load/teknee1.png");
            CNK_Data_Textures.Texture_Load_Teknee2.ResourceToFile(path_gob_extracted + "common/load/teknee2.png");
            CNK_Data_Textures.Texture_Load_Teknee3.ResourceToFile(path_gob_extracted + "common/load/teknee3.png");
            CNK_Data_Textures.Texture_Load_Trophy.ResourceToFile(path_gob_extracted + "common/load/trophy.png");
            CNK_Data_Textures.Texture_Load_Velorace.ResourceToFile(path_gob_extracted + "common/load/velorace.png");
            CNK_Data_Textures.Texture_Load_MainMenu01.ResourceToFile(path_gob_extracted + "common/load/mainmenu01.png");
            CNK_Data_Textures.Texture_Load_MainMenu02.ResourceToFile(path_gob_extracted + "common/load/mainmenu02.png");
            CNK_Data_Textures.Texture_Load_MainMenu03.ResourceToFile(path_gob_extracted + "common/load/mainmenu03.png");
            CNK_Data_Textures.Texture_Load_MainMenu04.ResourceToFile(path_gob_extracted + "common/load/mainmenu04.png");
            CNK_Data_Textures.Texture_Load_MainMenu05.ResourceToFile(path_gob_extracted + "common/load/mainmenu05.png");
            CNK_Data_Textures.Texture_Load_MainMenu06.ResourceToFile(path_gob_extracted + "common/load/mainmenu06.png");
            CNK_Data_Textures.Texture_Load_MainMenu07.ResourceToFile(path_gob_extracted + "common/load/mainmenu07.png");
            CNK_Data_Textures.Texture_Load_MainMenu08.ResourceToFile(path_gob_extracted + "common/load/mainmenu08.png");
            CNK_Data_Textures.Texture_Load_MainMenu09.ResourceToFile(path_gob_extracted + "common/load/mainmenu09.png");
            CNK_Data_Textures.Texture_Load_MainMenu10.ResourceToFile(path_gob_extracted + "common/load/mainmenu10.png");
            CNK_Data_Textures.Texture_Load_MainMenu11.ResourceToFile(path_gob_extracted + "common/load/mainmenu11.png");
            CNK_Data_Textures.Texture_Load_MainMenu12.ResourceToFile(path_gob_extracted + "common/load/mainmenu12.png");
            CNK_Data_Textures.Texture_Load_MainMenu13.ResourceToFile(path_gob_extracted + "common/load/mainmenu13.png");
        }

        public override void StartPreload()
        {
            // Extract GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                GobExtract.StartInfo.Arguments = ConsolePipeline.ProcessPath + "assets.gob" + " " + ConsolePipeline.ProcessPath + "cml_extr";
            }
            else
            {
                GobExtract.StartInfo.Arguments = ConsolePipeline.ProcessPath + "ASSETS.GOB" + " " + ConsolePipeline.ProcessPath + "cml_extr";
            }
            GobExtract.Start();
            GobExtract.WaitForExit();

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                File.Delete(ConsolePipeline.ExtractedPath + "ASSETS.GFC");
                File.Delete(ConsolePipeline.ExtractedPath + "ASSETS.GOB");
            }
            else
            {
                File.Delete(ConsolePipeline.ExtractedPath + "assets.gfc");
                File.Delete(ConsolePipeline.ExtractedPath + "assets.gob");
            }
            string path_gob_extracted = ConsolePipeline.ExtractedPath + @"\cml_extr\";

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "ps2/gfx/hud/8-bit_hud.png");
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "gcn/gfx/hud/8-bit_hud.png");
            }
            else
            {
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "xbox/gfx/hud/8-bit_hud.png");
            }

            CNK_Data_Textures.Texture_Font.FileToResource(path_gob_extracted + "common/fonts/all_fonts.png");

            CNK_Data_Textures.Texture_Load_Arena1.FileToResource(path_gob_extracted + "common/load/arena1.png");
            CNK_Data_Textures.Texture_Load_Arena2.FileToResource(path_gob_extracted + "common/load/arena2.png");
            CNK_Data_Textures.Texture_Load_Arena3.FileToResource(path_gob_extracted + "common/load/arena3.png");
            CNK_Data_Textures.Texture_Load_Arena4.FileToResource(path_gob_extracted + "common/load/arena4.png");
            CNK_Data_Textures.Texture_Load_Arena5.FileToResource(path_gob_extracted + "common/load/arena5.png");
            CNK_Data_Textures.Texture_Load_Barin1.FileToResource(path_gob_extracted + "common/load/barin1.png");
            CNK_Data_Textures.Texture_Load_Barin2.FileToResource(path_gob_extracted + "common/load/barin2.png");
            CNK_Data_Textures.Texture_Load_Barin3.FileToResource(path_gob_extracted + "common/load/barin3.png");
            CNK_Data_Textures.Texture_Load_Citadel.FileToResource(path_gob_extracted + "common/load/citadel.png");
            CNK_Data_Textures.Texture_Load_Earth1.FileToResource(path_gob_extracted + "common/load/earth1.png");
            CNK_Data_Textures.Texture_Load_Earth2.FileToResource(path_gob_extracted + "common/load/earth2.png");
            CNK_Data_Textures.Texture_Load_Earth3.FileToResource(path_gob_extracted + "common/load/earth3.png");
            CNK_Data_Textures.Texture_Load_Fenom1.FileToResource(path_gob_extracted + "common/load/fenom1.png");
            CNK_Data_Textures.Texture_Load_Fenom2.FileToResource(path_gob_extracted + "common/load/fenom2.png");
            CNK_Data_Textures.Texture_Load_Fenom3.FileToResource(path_gob_extracted + "common/load/fenom3.png");
            CNK_Data_Textures.Texture_Load_Hub1.FileToResource(path_gob_extracted + "common/load/hub1.png");
            CNK_Data_Textures.Texture_Load_Hub2.FileToResource(path_gob_extracted + "common/load/hub2.png");
            CNK_Data_Textures.Texture_Load_Hub3.FileToResource(path_gob_extracted + "common/load/hub3.png");
            CNK_Data_Textures.Texture_Load_Hub4.FileToResource(path_gob_extracted + "common/load/hub4.png");
            CNK_Data_Textures.Texture_Load_Hub5.FileToResource(path_gob_extracted + "common/load/hub5.png");
            CNK_Data_Textures.Texture_Load_Teknee1.FileToResource(path_gob_extracted + "common/load/teknee1.png");
            CNK_Data_Textures.Texture_Load_Teknee2.FileToResource(path_gob_extracted + "common/load/teknee2.png");
            CNK_Data_Textures.Texture_Load_Teknee3.FileToResource(path_gob_extracted + "common/load/teknee3.png");
            CNK_Data_Textures.Texture_Load_Trophy.FileToResource(path_gob_extracted + "common/load/trophy.png");
            CNK_Data_Textures.Texture_Load_Velorace.FileToResource(path_gob_extracted + "common/load/velorace.png");
            CNK_Data_Textures.Texture_Load_MainMenu01.FileToResource(path_gob_extracted + "common/load/mainmenu01.png");
            CNK_Data_Textures.Texture_Load_MainMenu02.FileToResource(path_gob_extracted + "common/load/mainmenu02.png");
            CNK_Data_Textures.Texture_Load_MainMenu03.FileToResource(path_gob_extracted + "common/load/mainmenu03.png");
            CNK_Data_Textures.Texture_Load_MainMenu04.FileToResource(path_gob_extracted + "common/load/mainmenu04.png");
            CNK_Data_Textures.Texture_Load_MainMenu05.FileToResource(path_gob_extracted + "common/load/mainmenu05.png");
            CNK_Data_Textures.Texture_Load_MainMenu06.FileToResource(path_gob_extracted + "common/load/mainmenu06.png");
            CNK_Data_Textures.Texture_Load_MainMenu07.FileToResource(path_gob_extracted + "common/load/mainmenu07.png");
            CNK_Data_Textures.Texture_Load_MainMenu08.FileToResource(path_gob_extracted + "common/load/mainmenu08.png");
            CNK_Data_Textures.Texture_Load_MainMenu09.FileToResource(path_gob_extracted + "common/load/mainmenu09.png");
            CNK_Data_Textures.Texture_Load_MainMenu10.FileToResource(path_gob_extracted + "common/load/mainmenu10.png");
            CNK_Data_Textures.Texture_Load_MainMenu11.FileToResource(path_gob_extracted + "common/load/mainmenu11.png");
            CNK_Data_Textures.Texture_Load_MainMenu12.FileToResource(path_gob_extracted + "common/load/mainmenu12.png");
            CNK_Data_Textures.Texture_Load_MainMenu13.FileToResource(path_gob_extracted + "common/load/mainmenu13.png");


        }

        
    }
}
