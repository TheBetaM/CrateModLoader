using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CTTR;
using Pure3D;
//CTTR API by NeoKesha
//Pure3D API by handsomematt (https://github.com/handsomematt/Pure3D) with modifications by BetaM
//Version number, seed and options are displayed in the Credits accessible from the main menu.

namespace CrateModLoader
{
    class Modder_CTTR
    {

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
        private string basePath = "";

        public Random randState = new Random();
        public string[] modOptions = {
            "Randomize Playable & Starting Characters",
            "Randomize Hub Entrances",
            "Randomize Track Entrances",
            "Randomize Minigames",
            "Randomize Missions",
            "Randomize Car Stats",
            "Randomize Race Laps",
            "Randomize Battle KO's",
            "Randomize Crashinator",
            "Randomize Run & Gun",
            "Add Unused Cutscenes",
            "Prevent Sequence Breaks" };
        public bool CTTR_rand_hubs = false; // todo: gem keys in missionobjectives_x and platforming_objects, unlock failure message, key missions
        public bool CTTR_rand_tracks = false; // todo: arenas
        public bool CTTR_rand_minigames = false; // todo: minigame challenges aswell
        public bool CTTR_rand_missions = false; // todo, genericobjectives, missionobjectives_x, level NIS+NPC
        public bool CTTR_rand_characters = false; // todo: idle animation
        public bool CTTR_rand_carstats = false; // todo: vehicles, levels/common for speed tier values
        public bool CTTR_rand_racelaps = false;
        public bool CTTR_rand_battlekos = false;
        public bool CTTR_rand_crashinator = false; // todo: kamikaze
        public bool CTTR_rand_runandgun = false; // todo: railshooter
        public bool CTTR_rand_stuntarena = false; //todo: permament_objects, stunt_objects
        public bool CTTR_rand_surfaceparams = false; //todo: car_effect_objects
        public bool CTTR_rand_powerupdistribution = false; // todo: driving_objects
        public bool CTTR_rand_npclocations = false; // todo: NPC - locator list
        public bool CTTR_rand_powerupeffects = false; //todo: driving_objects
        public bool CTTR_rand_weapons = false; // todo: turretmotifs
        public bool CTTR_rand_killrewards = false; //todo: levels/common

        public bool CTTR_replace_crashinator_with_coneattack = false; //todo
        public bool CTTR_add_powerups_in_timetrial = false; // todo: timetrial/props, see: bonus11
        public bool CTTR_add_unused_cutscenes = false; // todo, NIS + an objective?
        public bool CTTR_add_sequence_break_checks = false; // todo, genericobjectives
        public enum CTTR_Options
        {
            RandomizeCharacters = 0,
            RandomizeHubs = 1,
            RandomizeTracks = 2,
            RandomizeMinigames = 4,
            RandomizeMissions = 5,
            RandomizeCarStats = 6,
            RandomizeRaceLaps = 7,
            RandomizeBattleKOs = 8,
            RandomizeCrashinator = 9,
            RandomizeRunAndGun = 10,
            AddUnusedCutscenes = 11,
            PreventSequenceBreaks = 12,
        }

        public void OptionChanged(int option,bool value)
        {
            switch ((CTTR_Options)option)
            {
                case CTTR_Options.RandomizeCharacters:
                    CTTR_rand_characters = value;
                    break;
                case CTTR_Options.AddUnusedCutscenes:
                    CTTR_add_unused_cutscenes = value;
                    break;
                case CTTR_Options.PreventSequenceBreaks:
                    CTTR_add_sequence_break_checks = value;
                    break;
                case CTTR_Options.RandomizeHubs:
                    CTTR_rand_hubs = value;
                    break;
                case CTTR_Options.RandomizeMinigames:
                    CTTR_rand_minigames = value;
                    break;
                case CTTR_Options.RandomizeMissions:
                    CTTR_rand_missions = value;
                    break;
                case CTTR_Options.RandomizeTracks:
                    CTTR_rand_tracks = value;
                    break;
                case CTTR_Options.RandomizeCarStats:
                    CTTR_rand_carstats = value;
                    break;
                case CTTR_Options.RandomizeBattleKOs:
                    CTTR_rand_battlekos = value;
                    break;
                case CTTR_Options.RandomizeRaceLaps:
                    CTTR_rand_racelaps = value;
                    break;
                case CTTR_Options.RandomizeCrashinator:
                    CTTR_rand_crashinator = value;
                    break;
                case CTTR_Options.RandomizeRunAndGun:
                    CTTR_rand_runandgun = value;
                    break;
            }
        }

        public void UpdateModOptions()
        {
            Program.ModProgram.PrepareOptionsList(modOptions);
        }

        private List<int> randChars = new List<int>();
        private List<int> randHubs = new List<int>();
        private List<int> randTracks = new List<int>();
        private List<int> randMinigames = new List<int>();
        private List<int> randLaps = new List<int>();
        private List<int> randKOs = new List<int>();
        private List<int> randGems = new List<int>();

        public void SetPaths(ModLoader.ConsoleMode console, string exec_name = "")
        {
            if (console == ModLoader.ConsoleMode.PS2)
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
            else if (console == ModLoader.ConsoleMode.PSP)
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
            else if (console == ModLoader.ConsoleMode.GCN)
            {
                path_executable = @"sys\main.dol";
                path_RCF_default = @"files\adefault\default.rcf";
                path_RCF_advent1 = @"files\advent\advent1.rcf";
                path_RCF_advent2 = @"files\advent\advent2.rcf";
                path_RCF_advent3 = @"files\advent\advent3.rcf";
                path_RCF_adventa = @"files\advent\adventa.rcf";
                path_RCF_common = @"files\common\common.rcf";
                path_RCF_dino1 = @"files\dino\dino1.rcf";
                path_RCF_dino2 = @"files\dino\dino2.rcf";
                path_RCF_dino3 = @"files\dino\dino3.rcf";
                path_RCF_dinoa = @"files\dino\dinoa.rcf";
                path_RCF_egypt1 = @"files\egypt\egypt1.rcf";
                path_RCF_egypt2 = @"files\egypt\egypt2.rcf";
                path_RCF_egypt3 = @"files\egypt\egypt3.rcf";
                path_RCF_english = @"files\english.rcf";
                path_RCF_fairy1 = @"files\fairy\fairy1.rcf";
                path_RCF_fairy2 = @"files\fairy\fairy2.rcf";
                path_RCF_fairy3 = @"files\fairy\fairy3.rcf";
                path_RCF_fairys = @"files\fairy\fairys.rcf";
                path_RCF_frontend = @"files\common\frontend.rcf";
                path_RCF_solar1 = @"files\solar\solar1.rcf";
                path_RCF_solar2 = @"files\solar\solar2.rcf";
                path_RCF_solar3 = @"files\solar\solar3.rcf";
                path_RCF_solars = @"files\solar\solars.rcf";
                path_RCF_onfoot0 = @"files\onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"files\onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"files\onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"files\onfoot\onfoot3.rcf";
                path_RCF_onfoot5 = @"files\onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"files\onfoot\onfoot6.rcf";
                path_RCF_onfoot7 = @"files\onfoot\onfoot7.rcf";
            }
        }

        public void StartModProcess()
        {
            SetPaths(Program.ModProgram.isoType, Program.ModProgram.PS2_executable_name);
            basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\";
            if (Program.ModProgram.isoType == ModLoader.ConsoleMode.GCN)
            {
                basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\P-" + Program.ModProgram.PS2_game_code_name.Substring(0, 4) + @"\";
                // The CTTR API does not work with the GCN version yet
                return;
            }


            randState = new Random(Program.ModProgram.randoSeed);

            //Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + path_RCF_frontend + ";1", Program.ModProgram.extractedPath + path_RCF_frontend);

            bool Editing_Credits = true;
            bool Editing_DefaultCommon = false;

            if (CTTR_rand_characters || CTTR_rand_hubs || CTTR_rand_tracks || CTTR_rand_minigames || CTTR_rand_missions || CTTR_rand_carstats || CTTR_rand_racelaps || CTTR_rand_battlekos || CTTR_rand_crashinator || CTTR_rand_runandgun)
            {
                Editing_DefaultCommon = true;
            }

            if (Editing_DefaultCommon)
            {
                EditDefaultAndCommon();
            }

            if (Editing_Credits)
            {
                Mod_EditCredits();
            }
            

            //Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + path_RCF_frontend, Program.ModProgram.extractedPath + path_RCF_frontend + ";1");
        }

        void EditDefaultAndCommon()
        {
            randChars = new List<int>();
            if (CTTR_rand_characters)
            {
                int maxPlayableCharacters = 2;

                List<int> possibleChars = new List<int>();
                
                for (int i = 0; i < 8; i++)
                {
                    possibleChars.Add(i);
                }
                int targetChar = -1;
                for (int i = 0; i < maxPlayableCharacters; i++)
                {
                    targetChar = possibleChars[randState.Next(0, possibleChars.Count)];
                    randChars.Add(targetChar);
                    possibleChars.Remove(targetChar);
                }
            }
            randHubs = new List<int>();
            if (CTTR_rand_hubs)
            {
                List<int> possibleHubs = new List<int>();

                for (int i = 1; i < 6; i++)
                {
                    possibleHubs.Add(i);
                }
                int targetHub = -1;
                for (int i = 0; i < 5; i++)
                {
                    targetHub = possibleHubs[randState.Next(0, possibleHubs.Count)];
                    randHubs.Add(targetHub);
                    possibleHubs.Remove(targetHub);
                }
            }
            randTracks = new List<int>();
            if (CTTR_rand_tracks)
            {
                List<int> possibleTracks = new List<int>();

                for (int i = 0; i < 15; i++)
                {
                    possibleTracks.Add(i);
                }
                int targetTrack = -1;
                for (int i = 0; i < 15; i++)
                {
                    targetTrack = possibleTracks[randState.Next(0, possibleTracks.Count)];
                    randTracks.Add(targetTrack);
                    possibleTracks.Remove(targetTrack);
                }
            }
            randMinigames = new List<int>();
            if (CTTR_rand_tracks)
            {
                List<int> possibleMinigames = new List<int>();

                for (int i = 0; i < 8; i++)
                {
                    possibleMinigames.Add(i);
                }
                int targetMinigame = -1;
                for (int i = 0; i < 8; i++)
                {
                    targetMinigame = possibleMinigames[randState.Next(0, possibleMinigames.Count)];
                    randMinigames.Add(targetMinigame);
                    possibleMinigames.Remove(targetMinigame);
                }
            }
            randLaps = new List<int>();
            if (CTTR_rand_racelaps)
            {
                for (int i = 0; i < 15; i++)
                {
                    randLaps.Add(randState.Next(1,10));
                }
            }
            randKOs = new List<int>();
            if (CTTR_rand_battlekos)
            {
                for (int i = 0; i < 5; i++)
                {
                    randKOs.Add(randState.Next(5, 20));
                }
            }
            randGems = new List<int>();
            if (CTTR_rand_hubs)
            {
                List<int> possibleGems = new List<int>();

                possibleGems.Add(0);
                possibleGems.Add(1);
                possibleGems.Add(2);
                possibleGems.Add(4);
                possibleGems.Add(5);
                int targetGem = -1;
                for (int i = 0; i < 5; i++)
                {
                    targetGem = possibleGems[randState.Next(0, possibleGems.Count)];
                    randGems.Add(targetGem);
                    possibleGems.Remove(targetGem);
                    if (i == 2)
                    {
                        randGems.Add(3);
                    }
                }
            }

            Modify_RCF(path_RCF_default);
            Modify_RCF(path_RCF_common);
            if (path_RCF_onfoot0 != "")
            {
                Modify_RCF(path_RCF_onfoot0);
            }
            if (path_RCF_onfoot1 != "")
            {
                Modify_RCF(path_RCF_onfoot1);
            }
            if (path_RCF_onfoot2 != "")
            {
                Modify_RCF(path_RCF_onfoot2);
            }
            if (path_RCF_onfoot3 != "")
            {
                Modify_RCF(path_RCF_onfoot3);
            }
            if (path_RCF_onfoot4 != "")
            {
                Modify_RCF(path_RCF_onfoot4);
            }
            if (path_RCF_onfoot5 != "")
            {
                Modify_RCF(path_RCF_onfoot5);
            }
            if (path_RCF_onfoot6 != "")
            {
                Modify_RCF(path_RCF_onfoot6);
            }
            if (path_RCF_onfoot7 != "")
            {
                Modify_RCF(path_RCF_onfoot7);
            }

        }

        void Modify_RCF(string path)
        {
            string feedback = "";
            string path_extr = "";
            RCF rcf_default = new RCF();
            rcf_default.OpenRCF(basePath + path);
            path_extr = basePath + @"cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_default.ExtractRCF(ref feedback, path_extr);

            if (CTTR_rand_characters)
            {
                Randomize_Characters(path_extr, ref rcf_default, ref randChars);
            }
            if (CTTR_rand_hubs)
            {
                Randomize_Hubs(path_extr, ref rcf_default, ref randHubs, ref randGems);
            }
            if (CTTR_rand_tracks)
            {
                Randomize_Tracks(path_extr, ref rcf_default, ref randTracks);
            }
            if (CTTR_rand_minigames)
            {
                Randomize_Minigames(path_extr, ref rcf_default, ref randMinigames);
            }
            if (CTTR_rand_racelaps)
            {
                Randomize_Race_Laps(path_extr, ref rcf_default, ref randLaps);
            }
            if (CTTR_rand_battlekos)
            {
                Randomize_Battle_KOs(path_extr, ref rcf_default, ref randKOs);
            }

            rcf_default.Recalculate();
            rcf_default.Pack(basePath + path + "1", ref feedback);

            // Extraction cleanup
            System.IO.File.Delete(basePath + path);
            System.IO.File.Move(basePath + path + "1", basePath + path);
            if (Directory.Exists(path_extr))
            {
                DirectoryInfo di = new DirectoryInfo(path_extr);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_extr);
            }
        }

        void Randomize_Characters(string path_extr, ref RCF rcf_file, ref List<int> randChars)
        {
            

            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < startup_lines.Length; i++)
                {
                    LineList.Add(startup_lines[i]);
                }

                int characterList_Start = 0;
                int characterList_End = 0;
                List<string> DefaultUnlocks = new List<string>();
                if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "UnlockDefaults", ref characterList_Start, ref characterList_End, ref DefaultUnlocks))
                {
                    DefaultUnlocks.Clear();
                    DefaultUnlocks.Add("this.SetName(\"UnlockDefaults\")");
                    for (int i = 0; i < randChars.Count; i++)
                    {
                        DefaultUnlocks.Add("this.AddAction_UnlockCar(\"" + CTTR_Data.DriverNames[randChars[i]] + "\",1)");
                    }
                }
                CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "UnlockDefaults", ref DefaultUnlocks);

                startup_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    startup_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", startup_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\permanent\genericobjectives.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\genericobjectives.god";
                        break;
                    }
                }
            }
            if (randChars[0] != (int)CTTR_Data.DriverID.Crash && System.IO.File.Exists(path_extr + @"design\permanent\skins.god"))
            {
                string[] skins_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\skins.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < skins_lines.Length; i++)
                {
                    LineList.Add(skins_lines[i]);
                }

                int skin_Start = 0;
                int skin_End = 0;
                List<string> SkinObj = new List<string>();
                if (CTTR_Data.LUA_LoadObject(ref LineList, "Skin", "CrashDefault", ref skin_Start, ref skin_End, ref SkinObj))
                {
                    for (int i = 0; i < SkinObj.Count; i++)
                    {
                        if (SkinObj[i] == "this.SetOnfootSkinFilename(\"crash_onfoot_model\")")
                        {
                            SkinObj[i] = "this.SetOnfootSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                        else if (SkinObj[i] == "this.SetSpinSkinFilename(\"crash_spin_model\")")
                        {
                            SkinObj[i] = "this.SetSpinSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                    }
                }
                CTTR_Data.LUA_SaveObject(ref LineList, "Skin", "CrashDefault", ref SkinObj);

                skins_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    skins_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\skins.god", skins_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\permanent\skins.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\skins.god";
                        break;
                    }
                }
            }
            
            // Swapping idle animation for platforming character
            if (randChars[0] != (int)CTTR_Data.DriverID.Crash)
            {
                int[] targetAnimPos;
                Pure3D.File targetCharAnim = new Pure3D.File();
                if (System.IO.File.Exists(path_extr + @"art\animation\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d"))
                {
                    targetAnimPos = new int[] { 0, 4, 0, 0, 4, 0, 0, 3 };
                    targetCharAnim.Load(path_extr + @"art\animation\\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d");
                }
                else if (System.IO.File.Exists(path_extr + @"art\animation\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d"))
                {
                    targetAnimPos = new int[] { 0, 3, 0, 0, 1, 0, 13, 3 };
                    targetCharAnim.Load(path_extr + @"art\animation\\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d");
                }
                else
                {
                    return;
                }
                
                Pure3D.Chunk targetIdleAnim = targetCharAnim.RootChunk.Children[targetAnimPos[randChars[0]]];

                if (System.IO.File.Exists(path_extr + @"art\animation\crash_onfoot_animations.p3d"))
                {
                    Pure3D.File CrashOnfootAnim = new Pure3D.File();
                    CrashOnfootAnim.Load(path_extr + @"art\animation\crash_onfoot_animations.p3d");

                    CrashOnfootAnim.RootChunk.Children[0] = targetIdleAnim;

                    CrashOnfootAnim.Save(path_extr + @"art\animation\crash_onfoot_animations1.p3d");
                    System.IO.File.Delete(path_extr + @"art\animation\crash_onfoot_animations.p3d");
                    System.IO.File.Move(path_extr + @"art\animation\crash_onfoot_animations1.p3d", path_extr + @"art\animation\crash_onfoot_animations.p3d");

                    for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                    {
                        if (rcf_file.Header.T2File[i].Name == @"art\animation\crash_onfoot_animations.p3d")
                        {
                            rcf_file.Header.T2File[i].External = path_extr + @"art\animation\crash_onfoot_animations.p3d";
                            break;
                        }
                    }
                }
                if (System.IO.File.Exists(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d"))
                {
                    Pure3D.File CrashOnfootMidwayAnim = new Pure3D.File();
                    CrashOnfootMidwayAnim.Load(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");

                    CrashOnfootMidwayAnim.RootChunk.Children[0] = targetIdleAnim;

                    CrashOnfootMidwayAnim.Save(path_extr + @"art\animation\crash_onfoot_midway_animations1.p3d");
                    System.IO.File.Delete(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");
                    System.IO.File.Move(path_extr + @"art\animation\crash_onfoot_midway_animations1.p3d", path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");

                    for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                    {
                        if (rcf_file.Header.T2File[i].Name == @"art\animation\crash_onfoot_midway_animations.p3d")
                        {
                            rcf_file.Header.T2File[i].External = path_extr + @"art\animation\crash_onfoot_midway_animations.p3d";
                            break;
                        }
                    }
                }
            }

        }
        void Randomize_Hubs(string path_extr, ref RCF rcf_file, ref List<int> randHubs, ref List<int> randGems)
        {
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                string targetHub = "";
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    targetHub = CTTR_Data.HubNamesSimple[i + 1];
                    if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "ChangeLevelMidwayTo" + targetHub, ref List_Start, ref List_End, ref ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_ChangeLevel(\"" + CTTR_Data.HubNames[randHubs[i]] + "\",\"StartLocationFromMidway\")";
                        CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "ChangeLevelMidwayTo" + targetHub, ref ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                    if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "ChangeLevel" + CTTR_Data.HubNamesSimple[randHubs[i]] + "ToMidway", ref List_Start, ref List_End, ref ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_ChangeLevel(\"onfoot_midway\",\"StartLocationFrom" + targetHub + "\")";
                        CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "ChangeLevel" + CTTR_Data.HubNamesSimple[randHubs[i]] + "ToMidway", ref ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\permanent\genericobjectives.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\genericobjectives.god";
                        break;
                    }
                }
            }
            /* TODO: Gem Key randomization?
            for (int obj = 0; obj < CTTR_Data.MissionObjectiveTypes.Length; obj++)
            {
                if (System.IO.File.Exists(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god"))
                {
                    string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god");
                    List<string> LineList = new List<string>();
                    for (int i = 0; i < objective_lines.Length; i++)
                    {
                        LineList.Add(objective_lines[i]);
                    }

                    int List_Start = 0;
                    int List_End = 0;
                    List<string> ChangeHubObjective = new List<string>();
                    for (int i = 0; i < CTTR_Data.MissionObjectiveTypes.Length; i++)
                    {
                        if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", CTTR_Data.MissionObjectiveHubNamesSimple[i] + "KeyCollection", ref List_Start, ref List_End, ref ChangeHubObjective))
                        {
                            for (int a = 0; a < ChangeHubObjective.Count; a++)
                            {
                                if (ChangeHubObjective[a] == "this.AddAction_SetNamedFlag(\"KeyAcquired_" + CTTR_Data.MissionObjectiveTypes[i] + "\",true)")
                                {
                                    ChangeHubObjective[a] = "this.AddAction_SetNamedFlag(\"KeyAcquired_" + CTTR_Data.MissionObjectiveTypes[randGems[i]] + "\",true)";
                                }
                                else if (ChangeHubObjective[a] == "this.AddAction_SetRadarNavPoint(\"Nav" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate\")")
                                {
                                    ChangeHubObjective[a] = "this.AddAction_SetRadarNavPoint(\"Nav" + CTTR_Data.MissionObjectiveHubNamesSimple[randGems[i]] + "Gate\")";
                                }
                                else if (ChangeHubObjective[a] == "this.AddRequirement_ObjectiveComplete(\"Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate\")")
                                {
                                    ChangeHubObjective[a] = "this.AddRequirement_ObjectiveComplete(\"Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[randGems[i]] + "Gate\")";
                                }
                            }
                            CTTR_Data.LUA_SaveObject(ref LineList, "Objective", CTTR_Data.MissionObjectiveHubNamesSimple[i] + "KeyCollection", ref ChangeHubObjective);
                        }
                        ChangeHubObjective.Clear();
                        if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate", ref List_Start, ref List_End, ref ChangeHubObjective))
                        {
                            for (int a = 0; a < ChangeHubObjective.Count; a++)
                            {
                                if (ChangeHubObjective[a] == "this.AddRequirement_CheckNamedFlag(\"KeyAcquired_" + CTTR_Data.MissionObjectiveTypes[i] + "\",true)")
                                {
                                    ChangeHubObjective[a] = "this.AddRequirement_CheckNamedFlag(\"KeyAcquired_" + CTTR_Data.MissionObjectiveTypes[randGems[i]] + "\",true)";
                                }
                                else if (ChangeHubObjective[a] == "this.AddAction_SetNamedFlag(\"GateUnlocked_" + CTTR_Data.MissionObjectiveTypes[i] + "\",true)")
                                {
                                    ChangeHubObjective[a] = "this.AddAction_SetNamedFlag(\"GateUnlocked_" + CTTR_Data.MissionObjectiveTypes[randGems[i]] + "\",true)";
                                }
                                else if (ChangeHubObjective[a] == "this.AddAction_SetRadarNavPoint(\"Nav" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Weenie\")")
                                {
                                    ChangeHubObjective[a] = "this.AddAction_SetRadarNavPoint(\"Nav" + CTTR_Data.MissionObjectiveHubNamesSimple[randGems[i]] + "Weenie\")";
                                }
                            }
                            CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate", ref ChangeHubObjective);
                        }
                        ChangeHubObjective.Clear();
                    }

                    objective_lines = new string[LineList.Count];
                    for (int i = 0; i < LineList.Count; i++)
                    {
                        objective_lines[i] = LineList[i];
                    }

                    System.IO.File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god", objective_lines);

                    for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                    {
                        if (rcf_file.Header.T2File[i].Name == @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god")
                        {
                            rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god";
                            break;
                        }
                    }
                }
            }
            */
        }
        void Randomize_Tracks(string path_extr, ref RCF rcf_file, ref List<int> randTracks)
        {
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 15; i++)
                {
                    if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "StartRace" + CTTR_Data.TrackNamesSimple[i], ref List_Start, ref List_End, ref ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\")";
                        ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\",\"ReturnFromRace" + CTTR_Data.TrackNamesSimple[i] + "\")";
                        CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "StartRace" + CTTR_Data.TrackNamesSimple[i], ref ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }
                if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "StartRaceFromMidway2", ref List_Start, ref List_End, ref ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\",\"ReturnFromMidwayRaces\")";
                    CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "StartRaceFromMidway2", ref ChangeHubObjective);
                }
                ChangeHubObjective.Clear();
                if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "StartRaceFromMidway3", ref List_Start, ref List_End, ref ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\",\"ReturnFromMidwayRaces2\")";
                    CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "StartRaceFromMidway3", ref ChangeHubObjective);
                }
                ChangeHubObjective.Clear();
                if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "BuyRaceTicketWithTrack", ref List_Start, ref List_End, ref ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\")";
                    CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "BuyRaceTicketWithTrack", ref ChangeHubObjective);
                }
                ChangeHubObjective.Clear();

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\permanent\genericobjectives.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\genericobjectives.god";
                        break;
                    }
                }
            }
            for (int obj = 0; obj < CTTR_Data.MissionObjectiveTypes.Length; obj++)
            {
                if (System.IO.File.Exists(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god"))
                {
                    string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god");
                    List<string> LineList = new List<string>();
                    for (int i = 0; i < objective_lines.Length; i++)
                    {
                        LineList.Add(objective_lines[i]);
                    }

                    int List_Start = 0;
                    int List_End = 0;
                    List<string> ChangeHubObjective = new List<string>();
                    for (int i = 0; i < 15; i++)
                    {
                        if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "UnlockRace" + CTTR_Data.TrackNamesSimple[i], ref List_Start, ref List_End, ref ChangeHubObjective))
                        {
                            ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\")";
                            ChangeHubObjective[ChangeHubObjective.Count - 1] = "this.AddAction_DisplayMessage(\"" + CTTR_Data.TrackGateNames[randTracks[i]] +"\",1.0,6.0)";
                            CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "UnlockRace" + CTTR_Data.TrackNamesSimple[i], ref ChangeHubObjective);
                        }
                        ChangeHubObjective.Clear();
                    }

                    objective_lines = new string[LineList.Count];
                    for (int i = 0; i < LineList.Count; i++)
                    {
                        objective_lines[i] = LineList[i];
                    }

                    System.IO.File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god", objective_lines);

                    for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                    {
                        if (rcf_file.Header.T2File[i].Name == @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god")
                        {
                            rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god";
                            break;
                        }
                    }
                }
            }
        }
        void Randomize_Minigames(string path_extr, ref RCF rcf_file, ref List<int> randMinigames)
        {
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 8; i++)
                {
                    if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", CTTR_Data.MinigameObjectiveTypes[i], ref List_Start, ref List_End, ref ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_UnlockMiniGame(\"OFMiniGames/" + CTTR_Data.MinigameTypeNames[randMinigames[i]] + "\")";
                        CTTR_Data.LUA_SaveObject(ref LineList, "Objective", CTTR_Data.MinigameObjectiveTypes[i], ref ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\permanent\genericobjectives.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\genericobjectives.god";
                        break;
                    }
                }
            }
        }
        void Randomize_Race_Laps(string path_extr, ref RCF rcf_file, ref List<int> randLaps)
        {
            if (System.IO.File.Exists(path_extr + @"design\startup.god"))
            {
                string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\startup.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < startup_lines.Length; i++)
                {
                    LineList.Add(startup_lines[i]);
                }

                int LevelListStart = 0;
                for (int i = 0; i < LineList.Count; i++)
                {
                    if (LineList[i] == "function GetLevelList()")
                    {
                        LevelListStart = i + 2;
                        break;
                    }
                }
                LineList[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + randLaps[0] + ",true},";
                LineList[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + randLaps[1] + ",true},";
                LineList[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + randLaps[2] + ",true},";
                LineList[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + randLaps[3] + ",true},";
                LineList[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + randLaps[4] + ",true},";
                LineList[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + randLaps[5] + ",true},";
                LineList[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + randLaps[6] + ",true},";
                LineList[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + randLaps[7] + ",true},";
                LineList[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + randLaps[8] + ",true},";
                LineList[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + randLaps[9] + ",true},";
                LineList[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + randLaps[10] + ",true},";
                LineList[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + randLaps[11] + ",true},";
                LineList[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + randLaps[12] + ",true},";
                LineList[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + randLaps[13] + ",true},";
                LineList[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + randLaps[14] + ",true},";

                startup_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    startup_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\startup.god", startup_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\startup.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\startup.god";
                        break;
                    }
                }
            }
        }
        void Randomize_Battle_KOs(string path_extr, ref RCF rcf_file, ref List<int> randKOs)
        {
            if (System.IO.File.Exists(path_extr + @"design\startup.god"))
            {
                string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\startup.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < startup_lines.Length; i++)
                {
                    LineList.Add(startup_lines[i]);
                }

                int LevelListStart = 0;
                for (int i = 0; i < LineList.Count; i++)
                {
                    if (LineList[i] == "function GetLevelList()")
                    {
                        LevelListStart = i + 2;
                        break;
                    }
                }
                LineList[LevelListStart + 3] = "{\"adventure_arena\",ThemeAdventure,TypeBattle," + randKOs[0] + ",true},";
                LineList[LevelListStart + 8] = "{\"bonus1_arena\",ThemeFairy,TypeBattle," + randKOs[1] + ",true},";
                LineList[LevelListStart + 12] = "{\"dino_arena\",ThemeDino,TypeBattle," + randKOs[2] + ",true},";
                LineList[LevelListStart + 16] = "{\"egypt_arena\",ThemeEgypt,TypeBattle," + randKOs[3] + ",true},";

                startup_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    startup_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\startup.god", startup_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\startup.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\startup.god";
                        break;
                    }
                }
            }
        }

        void Mod_EditCredits()
        {
            //Warning: The CTTR API only likes paths with \ backslashes
            string feedback = "";
            string path_extr = "";
            RCF rcf_frontend = new RCF();
            rcf_frontend.OpenRCF(basePath + path_RCF_frontend);
            path_extr = basePath + @"cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_frontend.ExtractRCF(ref feedback, path_extr);

            string[] frontend_lines = System.IO.File.ReadAllLines(path_extr + @"design\levels\common\frontend.god");

            // Editing credits to add CML metadata
            for (int i = 0; i < frontend_lines.Length; i++)
            {
                if (frontend_lines[i] == "screen.AddLine(\"\",0,\"\")")
                {
                    frontend_lines[i + 1] = "screen.AddLine(\"Crate Mod Loader " + Program.ModProgram.releaseVersionString + "\",0,\"\")";
                    frontend_lines[i + 2] = "screen.AddLine(\"Seed: " + Program.ModProgram.randoSeed + "\",0,\"\")";
                    frontend_lines[i + 3] = "screen.AddLine(\"Options: " + Program.ModProgram.optionsSelectedString + "\",0,\"\")";
                    frontend_lines[i + 4] = "screen.AddLineSpecial(\"creditscttr\",0,104,104,255,1.2,true)";
                    break;
                }
            }

            System.IO.File.WriteAllLines(path_extr + @"design\levels\common\frontend.god", frontend_lines);

            for (int i = 0; i < rcf_frontend.Header.T2File.Length; i++)
            {
                if (rcf_frontend.Header.T2File[i].Name == @"design\levels\common\frontend.god")
                {
                    rcf_frontend.Header.T2File[i].External = path_extr + @"design\levels\common\frontend.god";
                    //Console.WriteLine("external " + rcf_frontend.Header.T2File[i].External);
                    break;
                }
            }

            rcf_frontend.Recalculate();
            rcf_frontend.Pack(basePath + path_RCF_frontend + "1", ref feedback);

            // Extraction cleanup
            System.IO.File.Delete(basePath + path_RCF_frontend);
            System.IO.File.Move(basePath + path_RCF_frontend + "1", basePath + path_RCF_frontend);
            if (Directory.Exists(path_extr))
            {
                DirectoryInfo di = new DirectoryInfo(path_extr);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_extr);
            }
        }

        public void OpenModMenu()
        {
            /*
            Pure3D.File targetCharAnim = new Pure3D.File();
            targetCharAnim.Load(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ngin_onfoot_animations.p3d");
            Pure3D.Chunk targetIdleAnim = targetCharAnim.RootChunk.Children[0];

            Pure3D.File CrashOnfootAnim1 = new Pure3D.File();
            CrashOnfootAnim1.Load(AppDomain.CurrentDomain.BaseDirectory + "/Tools/crash_onfoot_animations.p3d");
            CrashOnfootAnim1.Save(AppDomain.CurrentDomain.BaseDirectory + "/Tools/crash_onfoot_animations_norm.p3d");

            Pure3D.File CrashOnfootAnim = new Pure3D.File();
            CrashOnfootAnim.Load(AppDomain.CurrentDomain.BaseDirectory + "/Tools/crash_onfoot_animations.p3d");

            CrashOnfootAnim.RootChunk.Children[0] = targetIdleAnim;

            CrashOnfootAnim.Save(AppDomain.CurrentDomain.BaseDirectory + "/Tools/crash_onfoot_animations1.p3d");

            Pure3D.File CrashOnfootMidwayAnim1 = new Pure3D.File();
            CrashOnfootMidwayAnim1.Load(AppDomain.CurrentDomain.BaseDirectory + "/Tools/crash_onfoot_midway_animations.p3d");
            CrashOnfootMidwayAnim1.Save(AppDomain.CurrentDomain.BaseDirectory + "/Tools/crash_onfoot_midway_animations_norm.p3d");

            Pure3D.File CrashOnfootMidwayAnim = new Pure3D.File();
            CrashOnfootMidwayAnim.Load(AppDomain.CurrentDomain.BaseDirectory + "/Tools/crash_onfoot_midway_animations.p3d");

            CrashOnfootMidwayAnim.RootChunk.Children[0] = targetIdleAnim;

            CrashOnfootMidwayAnim.Save(AppDomain.CurrentDomain.BaseDirectory + "/Tools/crash_onfoot_midway_animations1.p3d");
            */
        }
    }
}
