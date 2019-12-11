using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
//CNK Tools/API by BetaM, ManDude and eezstreet.

namespace CrateModLoader
{
    class Modder_CNK
    {
        public string gameName = "CNK";
        public string apiCredit = "Tools/API by BetaM, ManDude and eezstreet";
        public System.Drawing.Image gameIcon = Properties.Resources.icon_cnk;
        public string[] modOptions = { "Randomize Adventure Hub Warp Pads", "Randomize Character Stats", "Randomize Kart Stats", "Randomize AI Kart Stats", "Randomize Wumpa Crate", "Disable Fadeout Overlay", "Speed Up Mask Hints" };

        public bool Randomize_Hub_Pads = false;
        public bool Randomize_Character_Stats = false;
        public bool Randomize_Kart_Stats = false;
        public bool Randomize_AI_Kart_Stats = false;
        public bool Randomize_Wumpa_Crate = false;
        public bool Mod_SpeedUp_Mask_Hints = false;
        public bool Mod_Disable_Fadeout = false;
        private string path_gob_extracted = "";

        public enum CNK_Options
        {
            RandomizeHubPads = 0,
            RandomizeCharacterStats = 1,
            RandomizeKartStats = 2,
            RandomizeAIKartStats = 3,
            RandomizeWumpaCrate = 4,
            DisableFadeout = 5,
            SpeedUpMaskHints = 6,
        }

        public void OptionChanged(int option, bool value)
        {
            if (option == (int)CNK_Options.RandomizeHubPads)
            {
                Randomize_Hub_Pads = value;
            }
            else if (option == (int)CNK_Options.RandomizeCharacterStats)
            {
                Randomize_Character_Stats = value;
            }
            else if (option == (int)CNK_Options.DisableFadeout)
            {
                Mod_Disable_Fadeout = value;
            }
            else if (option == (int)CNK_Options.RandomizeKartStats)
            {
                Randomize_Kart_Stats = value;   
            }
            else if (option == (int)CNK_Options.RandomizeAIKartStats)
            {
                Randomize_AI_Kart_Stats = value;
            }
            else if (option == (int)CNK_Options.RandomizeWumpaCrate)
            {
                Randomize_Wumpa_Crate = value;
            }
            else if (option == (int)CNK_Options.SpeedUpMaskHints)
            {
                Mod_SpeedUp_Mask_Hints = value;
            }
        }

        public void StartModProcess()
        {
            // Fixes names for PS2, and moves the archive for convenience
            File.Move(Program.ModProgram.extractedPath + "/ASSETS.GFC;1", AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC");
            File.Move(Program.ModProgram.extractedPath + "/ASSETS.GOB;1", AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB");

            // Extract GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract_in.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            GobExtract.StartInfo.Arguments = "Tools/ASSETS.GOB" + " " + "Tools/cml_extr";
            GobExtract.Start();
            GobExtract.WaitForExit();
            path_gob_extracted = AppDomain.CurrentDomain.BaseDirectory + "/Tools/cml_extr/";

            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB");

            ModProcess();
        }

        void ModProcess()
        {
            // Proof of concept hiscores mod
            string[] hiscores_lines = File.ReadAllLines(path_gob_extracted + "common/gameprogression/hiscores.csv");
            hiscores_lines[5] = "Modded,crash,69,# Modded         ";
            File.WriteAllLines(path_gob_extracted + "common/gameprogression/hiscores.csv", hiscores_lines);

            EndModProcess();
        }

        public void EndModProcess()
        {
            // Build GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract_out.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            GobExtract.StartInfo.Arguments = "Tools/ASSETS.GOB" + " " + "Tools/cml_extr" + " -create";
            GobExtract.Start();
            GobExtract.WaitForExit();

            // Fixes names for PS2, and moves the archive for convenience
            File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC", Program.ModProgram.extractedPath + "/ASSETS.GFC;1");
            File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB", Program.ModProgram.extractedPath + "/ASSETS.GOB;1");
            
            // Extraction cleanup
            if (Directory.Exists(path_gob_extracted))
            {
                DirectoryInfo di = new DirectoryInfo(path_gob_extracted);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_gob_extracted);
            }
            
        }
    }
}
