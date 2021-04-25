using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
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

        private int CurrentPass = 0;
        private float PassPercentMod = 39f;
        private int PassPercentAdd = 10;
        private bool EditingRM = false;
        private bool EditingSM = false;
        private bool MainBusy = false;

        public Modder_CNK() { }

        public override void StartModProcess()
        {
            ProcessBusy = true;

            AsyncStart();
        }

        public async void AsyncStart()
        {
            // Mod files
            ModProcess();

            while (MainBusy || PassBusy)
            {
                await Task.Delay(100);
            }

            ProcessBusy = false;
        }

        public async void ModProcess()
        {
            MainBusy = true;

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

            List<FileInfo> Files = new List<FileInfo>();
            DirectoryInfo adi = new DirectoryInfo(path_gob_extracted);
            Recursive_LoadCSV(adi, ref Files);
            PassCount = Files.Count;

            //Generic mods
            CNK_GenericMod generic = new CNK_GenericMod(path_gob_extracted, ConsolePipeline.ExtractedPath, ConsolePipeline.Metadata.Console);
            BeforeModPass();
            StartModPass(generic);

            bool NeedsCache = NeedsCachePass();
            CurrentPass = 0;
            if (!NeedsCache)
            {
                PassPercentMod = 83f;
                CurrentPass++;
            }

            while (CurrentPass < 2)
            {
                PassIterator = 0;
                PassBusy = true;
                if (CurrentPass == 0)
                {
                    PassPercentMod = 39f;
                    PassPercentAdd = 10;
                    UpdateProcessMessage("Cache Pass", 10);
                    BeforeCachePass();
                }
                else if (CurrentPass == 1)
                {
                    if (NeedsCache)
                    {
                        PassPercentMod = 43f;
                        PassPercentAdd = 50;
                        UpdateProcessMessage("Mod Pass", 50);
                    }
                    else
                    {
                        PassPercentMod = 83f;
                        UpdateProcessMessage("Mod Pass", 10);
                    }

                    BeforeModPass();
                }

                IList<Task> editTaskList = new List<Task>();

                foreach (FileInfo file in Files)
                {
                    editTaskList.Add(EditCSV(file));
                }

                await Task.WhenAll(editTaskList);

                CurrentPass++;
                PassBusy = false;
            }

            UpdateProcessMessage("Handling custom textures...", 95);

            HandleTextures(path_gob_extracted);

            UpdateProcessMessage("Building ASSETS.GOB...", 97);

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

            MainBusy = false;
        }

        void Recursive_LoadCSV(DirectoryInfo di, ref List<FileInfo> Files)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LoadCSV(dir, ref Files);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Extension.ToLower() == ".csv")
                {
                    Files.Add(file);
                }
            }
        }

        private async Task EditCSV(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);

            await Task.Run(
            () =>
            {
                CSV table = new CSV(file);

                switch (CurrentPass)
                {
                    case 0:
                        StartCachePass(table);
                        break;
                    default:
                    case 1:
                        StartModPass(table);
                        break;
                }

                table.Write();
            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
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
