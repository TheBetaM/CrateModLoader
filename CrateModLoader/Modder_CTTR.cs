using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTTR;
//CTTR API by NeoKesha

namespace CrateModLoader
{
    class Modder_CTTR
    {

        //CTTR specific
        public string path_RCF_default = "";
        public string path_RCF_common = "";
        public string path_RCF_frontend = "";
        public string path_executable = "";
        public string path_RCF_onfoot0 = "";
        public string path_RCF_onfoot1 = "";
        public string path_RCF_onfoot2 = "";
        public string path_RCF_onfoot3 = "";
        public string path_RCF_onfoot4 = ""; // PSP
        public string path_RCF_onfoot5 = "";
        public string path_RCF_onfoot6 = "";
        public string path_RCF_onfoot7 = ""; // GC
        public string path_RCF_advent1 = "";
        public string path_RCF_advent2 = "";
        public string path_RCF_advent3 = "";
        public string path_RCF_adventa = "";
        public string path_RCF_dino1 = "";
        public string path_RCF_dino2 = "";
        public string path_RCF_dino3 = "";
        public string path_RCF_dinoa = "";
        public string path_RCF_egypt1 = "";
        public string path_RCF_egypt2 = "";
        public string path_RCF_egypt3 = "";
        public string path_RCF_egypta = ""; // PSP/PS2
        public string path_RCF_fairy1 = "";
        public string path_RCF_fairy2 = "";
        public string path_RCF_fairy3 = "";
        public string path_RCF_fairys = "";
        public string path_RCF_solar1 = "";
        public string path_RCF_solar2 = "";
        public string path_RCF_solar3 = "";
        public string path_RCF_solars = "";
        public string path_RCF_0 = ""; // XBOX
        public string path_RCF_1 = ""; // XBOX
        public string path_RCF_2 = ""; // XBOX
        public string path_RCF_3 = ""; // XBOX
        public string path_RCF_4 = ""; // XBOX
        public string path_RCF_5 = ""; // XBOX
        public string path_RCF_6 = ""; // XBOX
        public string path_RCF_sound = "";
        public string path_RCF_english = "";

        public string[] modOptions_CTTR = { "Randomize hubs", "Randomize tracks", "Randomize minigames", "Randomize missions", "Add unused cutscenes", "Prevent sequence breaks" };
        public bool CTTR_rand_hubs = false;
        public bool CTTR_rand_tracks = false;
        public bool CTTR_rand_minigames = false;
        public bool CTTR_rand_missions = false;
        public bool CTTR_add_unused_cutscenes = false;
        public bool CTTR_add_sequence_break_checks = false;
        public enum CTTR_Options
        {
            RandomizeHubs = 0,
            RandomizeTracks = 1,
            RandomizeMinigames = 2,
            RandomizeMissions = 3,
            AddUnusedCutscenes = 4,
            PreventSequenceBreaks = 5,
        }

        public void StartModProcess()
        {
            string feedbacktest = "";
            RCF newrcf = new RCF();
            newrcf.OpenRCF(""); // Path to target RCF
            object testItem = newrcf.ExtractItem(1, ""); // Extract if needed
            newrcf.Header.T2File[0].External = ""; // Path to external replacement
            newrcf.Recalculate(); //Maybe needs this?
            newrcf.Pack("", ref feedbacktest); // Pack the RCF
        }

        public void OptionChanged(int option,bool value)
        {
            if (option == (int)CTTR_Options.RandomizeHubs)
            {
                CTTR_rand_hubs = value;
            }
            else if (option == (int)CTTR_Options.RandomizeTracks)
            {
                CTTR_rand_tracks = value;
            }
            else if (option == (int)CTTR_Options.RandomizeMinigames)
            {
                CTTR_rand_minigames = value;
            }
            else if (option == (int)CTTR_Options.RandomizeMissions)
            {
                CTTR_rand_missions = value;
            }
            else if (option == (int)CTTR_Options.AddUnusedCutscenes)
            {
                CTTR_add_unused_cutscenes = value;
            }
            else if (option == (int)CTTR_Options.PreventSequenceBreaks)
            {
                CTTR_add_sequence_break_checks = value;
            }
        }

        public void SetPaths(Randomizer.ConsoleMode console, string exec_name = "")
        {
            if (console == Randomizer.ConsoleMode.PS2)
            {
                path_executable = exec_name;
                path_RCF_default = @"ADEFAULT\DEFAULT.RCF";
                path_RCF_advent1 = @"ADVENT\ADVENT1.RCF";
                path_RCF_advent2 = @"ADVENT\ADVENT2.RCF";
                path_RCF_advent3 = @"ADVENT\ADVENT3.RCF";
                path_RCF_adventa = @"ADVENT\ADVENTA.RCF";
                path_RCF_common = @"COMMON\COMMON.RCF";
                path_RCF_dino1 = @"DINO\DINO1.RCF";
                path_RCF_dino2 = @"DINO\DINO2.RCF";
                path_RCF_dino3 = @"DINO\DINO3.RCF";
                path_RCF_dinoa = @"DINO\DINOA.RCF";
                path_RCF_egypt1 = @"EGYPT\EGYPT1.RCF";
                path_RCF_egypt2 = @"EGYPT\EGYPT2.RCF";
                path_RCF_egypt3 = @"EGYPT\EGYPT3.RCF";
                path_RCF_egypta = @"EGYPT\EGYPTA.RCF";
                path_RCF_english = @"ENGLISH.RCF";
                path_RCF_fairy1 = @"FAIRY\FAIRY1.RCF";
                path_RCF_fairy2 = @"FAIRY\FAIRY2.RCF";
                path_RCF_fairy3 = @"FAIRY\FAIRY3.RCF";
                path_RCF_fairys = @"FAIRY\FAIRYS.RCF";
                path_RCF_frontend = @"COMMON\FRONTEND.RCF";
                path_RCF_solar1 = @"SOLAR\SOLAR1.RCF";
                path_RCF_solar2 = @"SOLAR\SOLAR2.RCF";
                path_RCF_solar3 = @"SOLAR\SOLAR3.RCF";
                path_RCF_solars = @"SOLAR\SOLARS.RCF";
                path_RCF_onfoot0 = @"ONFOOT\ONFOOT.RCF";
                path_RCF_onfoot1 = @"ONFOOT\ONFOOT1.RCF";
                path_RCF_onfoot2 = @"ONFOOT\ONFOOT2.RCF";
                path_RCF_onfoot3 = @"ONFOOT\ONFOOT3.RCF";
                path_RCF_onfoot5 = @"ONFOOT\ONFOOT5.RCF";
                path_RCF_onfoot6 = @"ONFOOT\ONFOOT6.RCF";
            }
            else if (console == Randomizer.ConsoleMode.PSP)
            {
                path_executable = @"PSP_GAME\SYSDIR\BOOT.BIN";
                path_RCF_default = @"PSP_GAME\USRDIR\adefault\default.rcf";
                path_RCF_advent1 = @"PSP_GAME\USRDIR\advent\advent1.rcf";
                path_RCF_advent2 = @"PSP_GAME\USRDIR\advent\advent2.rcf";
                path_RCF_advent3 = @"PSP_GAME\USRDIR\advent\advent3.rcf";
                path_RCF_adventa = @"PSP_GAME\USRDIR\advent\adventa.rcf";
                path_RCF_common = @"PSP_GAME\USRDIR\common\common.rcf";
                path_RCF_dino1 = @"PSP_GAME\USRDIR\dino\dino1.rcf";
                path_RCF_dino2 = @"PSP_GAME\USRDIR\dino\dino2.rcf";
                path_RCF_dino3 = @"PSP_GAME\USRDIR\dino\dino3.rcf";
                path_RCF_dinoa = @"PSP_GAME\USRDIR\dino\dinoa.rcf";
                path_RCF_egypt1 = @"PSP_GAME\USRDIR\egypt\egypt1.rcf";
                path_RCF_egypt2 = @"PSP_GAME\USRDIR\egypt\egypt2.rcf";
                path_RCF_egypt3 = @"PSP_GAME\USRDIR\egypt\egypt3.rcf";
                path_RCF_egypta = @"PSP_GAME\USRDIR\egypt\egypta.rcf";
                path_RCF_english = @"PSP_GAME\USRDIR\english.rcf";
                path_RCF_fairy1 = @"PSP_GAME\USRDIR\fairy\fairy1.rcf";
                path_RCF_fairy2 = @"PSP_GAME\USRDIR\fairy\fairy2.rcf";
                path_RCF_fairy3 = @"PSP_GAME\USRDIR\fairy\fairy3.rcf";
                path_RCF_fairys = @"PSP_GAME\USRDIR\fairy\fairys.rcf";
                path_RCF_frontend = @"PSP_GAME\USRDIR\common\frontend.rcf";
                path_RCF_solar1 = @"PSP_GAME\USRDIR\solar\solar1.rcf";
                path_RCF_solar2 = @"PSP_GAME\USRDIR\solar\solar2.rcf";
                path_RCF_solar3 = @"PSP_GAME\USRDIR\solar\solar3.rcf";
                path_RCF_solars = @"PSP_GAME\USRDIR\solar\solars.rcf";
                path_RCF_onfoot0 = @"PSP_GAME\USRDIR\onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"PSP_GAME\USRDIR\onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"PSP_GAME\USRDIR\onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"PSP_GAME\USRDIR\onfoot\onfoot3.rcf";
                path_RCF_onfoot4 = @"PSP_GAME\USRDIR\onfoot\onfoot4.rcf";
                path_RCF_onfoot5 = @"PSP_GAME\USRDIR\onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"PSP_GAME\USRDIR\onfoot\onfoot6.rcf";
            }
            else if (console == Randomizer.ConsoleMode.GCN)
            {
                path_executable = @"sys\main.dol";
                path_RCF_default = @"root\adefault\default.rcf";
                path_RCF_advent1 = @"root\advent\advent1.rcf";
                path_RCF_advent2 = @"root\advent\advent2.rcf";
                path_RCF_advent3 = @"root\advent\advent3.rcf";
                path_RCF_adventa = @"root\advent\adventa.rcf";
                path_RCF_common = @"root\common\common.rcf";
                path_RCF_dino1 = @"root\dino\dino1.rcf";
                path_RCF_dino2 = @"root\dino\dino2.rcf";
                path_RCF_dino3 = @"root\dino\dino3.rcf";
                path_RCF_dinoa = @"root\dino\dinoa.rcf";
                path_RCF_egypt1 = @"root\egypt\egypt1.rcf";
                path_RCF_egypt2 = @"root\egypt\egypt2.rcf";
                path_RCF_egypt3 = @"root\egypt\egypt3.rcf";
                path_RCF_english = @"root\english.rcf";
                path_RCF_fairy1 = @"root\fairy\fairy1.rcf";
                path_RCF_fairy2 = @"root\fairy\fairy2.rcf";
                path_RCF_fairy3 = @"root\fairy\fairy3.rcf";
                path_RCF_fairys = @"root\fairy\fairys.rcf";
                path_RCF_frontend = @"root\common\frontend.rcf";
                path_RCF_solar1 = @"root\solar\solar1.rcf";
                path_RCF_solar2 = @"root\solar\solar2.rcf";
                path_RCF_solar3 = @"root\solar\solar3.rcf";
                path_RCF_solars = @"root\solar\solars.rcf";
                path_RCF_onfoot0 = @"root\onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"root\onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"root\onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"root\onfoot\onfoot3.rcf";
                path_RCF_onfoot5 = @"root\onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"root\onfoot\onfoot6.rcf";
                path_RCF_onfoot7 = @"root\onfoot\onfoot7.rcf";
            }
        }
    }

    class Modder_Titans
    {

    }

    class Modder_MoM
    {

    }
}
