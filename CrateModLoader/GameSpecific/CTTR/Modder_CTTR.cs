using System;
using System.Collections.Generic;
using System.IO;
using Pure3D;
using Pure3D.Chunks;
using CrateModLoader.GameSpecific.CTTR;
//RCF API by NeoKesha
//Pure3D API by BetaM (based on https://github.com/handsomematt/Pure3D)
//Version number, seed and options are displayed in the Credits accessible from the main menu.
/* Mod Layers:
 * 1: All .RCF file contents (only replace files)
 */

namespace CrateModLoader
{
    public sealed class Modder_CTTR : Modder
    {
        internal string path_RCF_default = "";
        internal string path_RCF_common = "";
        internal string path_RCF_frontend = "";
        internal string path_executable = "";
        internal string path_RCF_onfoot0 = "";
        internal string path_RCF_onfoot1 = "";
        internal string path_RCF_onfoot2 = "";
        internal string path_RCF_onfoot3 = "";
        internal string path_RCF_onfoot4 = ""; // PSP
        internal string path_RCF_onfoot5 = "";
        internal string path_RCF_onfoot6 = "";
        internal string path_RCF_onfoot7 = ""; // GC
        internal string path_RCF_advent1 = "";
        internal string path_RCF_advent2 = "";
        internal string path_RCF_advent3 = "";
        internal string path_RCF_adventa = "";
        internal string path_RCF_dino1 = "";
        internal string path_RCF_dino2 = "";
        internal string path_RCF_dino3 = "";
        internal string path_RCF_dinoa = "";
        internal string path_RCF_egypt1 = "";
        internal string path_RCF_egypt2 = "";
        internal string path_RCF_egypt3 = "";
        internal string path_RCF_egypta = ""; // PSP/PS2
        internal string path_RCF_fairy1 = "";
        internal string path_RCF_fairy2 = "";
        internal string path_RCF_fairy3 = "";
        internal string path_RCF_fairys = "";
        internal string path_RCF_solar1 = "";
        internal string path_RCF_solar2 = "";
        internal string path_RCF_solar3 = "";
        internal string path_RCF_solars = "";
        internal string path_RCF_0 = ""; // XBOX
        internal string path_RCF_1 = ""; // XBOX
        internal string path_RCF_2 = ""; // XBOX
        internal string path_RCF_3 = ""; // XBOX
        internal string path_RCF_4 = ""; // XBOX
        internal string path_RCF_5 = ""; // XBOX
        internal string path_RCF_6 = ""; // XBOX
        internal string path_RCF_sound = "";
        internal string path_RCF_english = "";
        internal string basePath = "";

        internal const int RandomizeCharacters          = 0;
        internal const int RandomizeHubs                = 1;
        internal const int RandomizeTracks              = 2;
        internal const int RandomizeMinigames           = 3;
        internal const int RandomizeMissions            = 4;
        internal const int RandomizeCarStats            = 5;
        internal const int RandomizeRaceLaps            = 6;
        internal const int RandomizeBattleKOs           = 7;
        internal const int RandomizeCrashinator         = 8;
        internal const int RandomizeRunAndGun           = 9;
        internal const int RandomizeStuntArena          = 10;
        internal const int RandomizeSurfaceParams       = 11;
        internal const int RandomizePowerupDistribution = 12;
        internal const int RandomizePowerupEffects      = 13;
        internal const int RandomizeWeapons             = 14;
        internal const int AddUnusedCutscenes           = 15;
        internal const int PreventSequenceBreaks        = 16;
        internal const int RandomizeNPCs                = 17;
        internal const int AddPowerupsTimeTrial         = 18;
        internal const int ReplaceCrashinatorConeAttack = 19;

        public Modder_CTTR()
        {
            Game = new Game()
            {
                Name = "Crash Tag Team Racing",
                ShortName = "CrashTTR",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.PSP,
                    ConsoleMode.XBOX,
                },
                API_Credit = "APIs by NeoKesha and BetaM",
                API_Link = string.Empty,
                Icon = Properties.Resources.icon_crash,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_211.91;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_211.91",
                    CodeName = "SLUS_21191", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_534.39;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_534.39",
                    CodeName = "SLES_53439", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLPM_660.90;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_660.90",
                    CodeName = "SLPM_66090", },
                },
                RegionID_GCN = new RegionCode[] {
                    new RegionCode() {
                    Name = "G9RE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "G9RH7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RJ7D",
                    Region = RegionType.NTSC_J },
                    new RegionCode() {
                    Name = "G9RD7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RF7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RP7D",
                    Region = RegionType.PAL },
                },
                RegionID_PSP = new RegionCode[] {
                    new RegionCode() {
                    Name = "ULUS-10044",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULJM-05036",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "ULES-00168",
                    Region = RegionType.NTSC_J },
                },
                RegionID_XBOX = new RegionCode[] {
                    new RegionCode() {
                    Name = "Crash Tag Team Racing",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7, },
                    new RegionCode() {
                    Name = "Crash Tag Team Racing",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
            };

            Options.Add(RandomizeCharacters, new ModOption("Randomize Platforming Character (Unstable)")); // todo: change missions to unlock crash and cortex if they're not in the starting pool
            //Options.Add(RandomizeHubs, new ModOption("Randomize Hub Entrances")); // todo: gem keys in missionobjectives_x and platforming_objects, unlock failure message, key missions
            Options.Add(RandomizeTracks, new ModOption("Randomize Track Entrances")); // todo: arenas
            Options.Add(RandomizeMinigames, new ModOption("Randomize Minigames")); // todo: minigame challenges aswell
            //Options.Add(RandomizeMissions, new ModOption("Randomize Missions"));// todo, genericobjectives, missionobjectives_x, level NIS+NPC
            //Options.Add(RandomizeCarStats, new ModOption("Randomize Car Stats")); // todo: vehicles, levels/common for speed tier values
            Options.Add(RandomizeRaceLaps, new ModOption("Randomize Race Laps"));
            //Options.Add(RandomizeBattleKOs, new ModOption("Randomize Battle KO's")); // doesn't work?
            //Options.Add(RandomizeCrashinator, new ModOption("Randomize Crashinator")); // todo: kamikaze
            //Options.Add(RandomizeRunAndGun, new ModOption("Randomize Run & Gun")); // todo: railshooter
            //Options.Add(RandomizeStuntArena, new ModOption("Randomize Stunt Arena")); //todo: permament_objects, stunt_objects
            //Options.Add(RandomizeSurfaceParams, new ModOption("Randomize Surface Parameters")); //todo: car_effect_objects
            //Options.Add(RandomizePowerupDistribution, new ModOption("Randomize Powerup Distribution")); // todo: driving_objects
            //Options.Add(RandomizePowerupEffects, new ModOption("Randomize Powerup Effects")); //todo: driving_objects
            //Options.Add(RandomizeWeapons, new ModOption("Randomize Weapons")); // todo: turretmotifs
            //Options.Add(RandomizeNPCs, new ModOption("Randomize NPC Locations")); // todo: NPC - locator list
            Options.Add(PreventSequenceBreaks, new ModOption("Prevent Sequence Breaks"));
        }

        public Random randState = new Random();

        private List<int> randChars = new List<int>();
        private List<int> randHubs = new List<int>();
        private List<int> randTracks = new List<int>();
        private List<int> randMinigames = new List<int>();
        private List<int> randLaps = new List<int>();
        private List<int> randKOs = new List<int>();
        private List<int> randGems = new List<int>();

        public void SetPaths(ConsoleMode console, string exec_name = "")
        {
            path_RCF_default = "";
            path_RCF_common = "";
            path_RCF_frontend = "";
            path_executable = "";
            path_RCF_onfoot0 = "";
            path_RCF_onfoot1 = "";
            path_RCF_onfoot2 = "";
            path_RCF_onfoot3 = "";
            path_RCF_onfoot4 = "";
            path_RCF_onfoot5 = "";
            path_RCF_onfoot6 = "";
            path_RCF_onfoot7 = "";
            path_RCF_advent1 = "";
            path_RCF_advent2 = "";
            path_RCF_advent3 = "";
            path_RCF_adventa = "";
            path_RCF_dino1 = "";
            path_RCF_dino2 = "";
            path_RCF_dino3 = "";
            path_RCF_dinoa = "";
            path_RCF_egypt1 = "";
            path_RCF_egypt2 = "";
            path_RCF_egypt3 = "";
            path_RCF_egypta = "";
            path_RCF_fairy1 = "";
            path_RCF_fairy2 = "";
            path_RCF_fairy3 = "";
            path_RCF_fairys = "";
            path_RCF_solar1 = "";
            path_RCF_solar2 = "";
            path_RCF_solar3 = "";
            path_RCF_solars = "";
            path_RCF_0 = "";
            path_RCF_1 = "";
            path_RCF_2 = "";
            path_RCF_3 = "";
            path_RCF_4 = "";
            path_RCF_5 = "";
            path_RCF_6 = ""; 
            path_RCF_sound = "";
            path_RCF_english = "";

            if (console == ConsoleMode.PS2)
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
            else if (console == ConsoleMode.PSP)
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
            else if (console == ConsoleMode.GCN)
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
            else
            {
                path_executable = @"default.xbe";
                path_RCF_advent1 = @"advent\advent1.rcf";
                path_RCF_advent2 = @"advent\advent2.rcf";
                path_RCF_advent3 = @"advent\advent3.rcf";
                path_RCF_adventa = @"advent\adventa.rcf";
                path_RCF_common = @"common\common.rcf";
                path_RCF_dino1 = @"dino\dino1.rcf";
                path_RCF_dino2 = @"dino\dino2.rcf";
                path_RCF_dino3 = @"dino\dino3.rcf";
                path_RCF_dinoa = @"dino\dinoa.rcf";
                path_RCF_egypt1 = @"egypt\egypt1.rcf";
                path_RCF_egypt2 = @"egypt\egypt2.rcf";
                path_RCF_egypt3 = @"egypt\egypt3.rcf";
                path_RCF_fairy1 = @"fairy\fairy1.rcf";
                path_RCF_fairy2 = @"fairy\fairy2.rcf";
                path_RCF_fairy3 = @"fairy\fairy3.rcf";
                path_RCF_fairys = @"fairy\fairys.rcf";
                path_RCF_frontend = @"common\frontend.rcf";
                path_RCF_solar1 = @"solar\solar1.rcf";
                path_RCF_solar2 = @"solar\solar2.rcf";
                path_RCF_solar3 = @"solar\solar3.rcf";
                path_RCF_solars = @"solar\solars.rcf";
                path_RCF_onfoot0 = @"onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"onfoot\onfoot3.rcf";
                path_RCF_onfoot5 = @"onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"onfoot\onfoot6.rcf";
                path_RCF_0 = @"0\0.rcf";
                path_RCF_1 = @"1\1.rcf";
                path_RCF_2 = @"2\2.rcf";
                path_RCF_3 = @"3\3.rcf";
                path_RCF_4 = @"4\4.rcf";
                path_RCF_5 = @"5\5.rcf";
                path_RCF_6 = @"6\6.rcf";
                path_RCF_sound = @"sound\sound.rcf";
            }
        }

        public override void StartModProcess()
        {
            SetPaths(Program.ModProgram.isoType, Program.ModProgram.PS2_executable_name);
            basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\";
            if (Program.ModProgram.isoType == ConsoleMode.GCN)
            {
                basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\P-" + Program.ModProgram.ProductCode.Substring(0, 4) + @"\";
            }

            RCF_Manager.cachedRCF = null;

            randState = new Random(Program.ModProgram.randoSeed);

            bool Editing_DefaultCommon = false;

            if (Options[RandomizeCharacters].Enabled ||
                //Options[RandomizeHubs].Enabled ||
                Options[RandomizeTracks].Enabled ||
                Options[RandomizeMinigames].Enabled ||
                Options[PreventSequenceBreaks].Enabled ||
                //Options[RandomizeMissions].Enabled ||
                //Options[RandomizeCarStats].Enabled ||
                Options[RandomizeRaceLaps].Enabled )
                //Options[RandomizeBattleKOs].Enabled ||
                //Options[RandomizeCrashinator].Enabled ||
                //Options[RandomizeRunAndGun].Enabled)
            {
                Editing_DefaultCommon = true;
            }

            if (Editing_DefaultCommon)
            {
                EditDefaultAndCommon();
            }

            CTTR_Randomizers.targetCharAnim = null;
            CTTR_Randomizers.targetIdleAnim = null;

        }

        void EditDefaultAndCommon()
        {
            randChars = new List<int>();
            if (Options[RandomizeCharacters].Enabled)
            {
                int maxPlayableCharacters = 2;

                List<int> possibleChars = new List<int>();
                
                for (int i = 0; i < 8; i++)
                {
                    possibleChars.Add(i);
                }

                for (int i = 0; i < maxPlayableCharacters; i++)
                {
                    int targetChar = possibleChars[randState.Next(0, possibleChars.Count)];
                    randChars.Add(targetChar);
                    possibleChars.Remove(targetChar);
                }
            }
            randHubs = new List<int>();
            randGems = new List<int>();
            /*
            if (Options[RandomizeHubs].Enabled)
            {
                List<int> possibleHubs = new List<int>();

                for (int i = 1; i < 6; i++)
                {
                    possibleHubs.Add(i);
                }

                for (int i = 0; i < 5; i++)
                {
                    int targetHub = possibleHubs[randState.Next(0, possibleHubs.Count)];
                    randHubs.Add(targetHub);
                    possibleHubs.Remove(targetHub);
                }
                List<int> possibleGems = new List<int>();

                possibleGems.Add(0);
                possibleGems.Add(1);
                possibleGems.Add(2);
                possibleGems.Add(4);
                possibleGems.Add(5);
                for (int i = 0; i < 5; i++)
                {
                    int targetGem = possibleGems[randState.Next(0, possibleGems.Count)];
                    randGems.Add(targetGem);
                    possibleGems.Remove(targetGem);
                    if (i == 2)
                    {
                        randGems.Add(3);
                    }
                }
            }
            */
            randTracks = new List<int>();
            randMinigames = new List<int>();
            if (Options[RandomizeTracks].Enabled)
            {
                List<int> possibleTracks = new List<int>();

                for (int i = 0; i < 15; i++)
                {
                    possibleTracks.Add(i);
                }

                for (int i = 0; i < 15; i++)
                {
                    int targetTrack = possibleTracks[randState.Next(0, possibleTracks.Count)];
                    randTracks.Add(targetTrack);
                    possibleTracks.Remove(targetTrack);
                }
                List<int> possibleMinigames = new List<int>();

                for (int i = 0; i < 8; i++)
                {
                    possibleMinigames.Add(i);
                }

                for (int i = 0; i < 8; i++)
                {
                    int targetMinigame = possibleMinigames[randState.Next(0, possibleMinigames.Count)];
                    randMinigames.Add(targetMinigame);
                    possibleMinigames.Remove(targetMinigame);
                }
            }
            randLaps = new List<int>();
            if (Options[RandomizeRaceLaps].Enabled)
            {
                for (int i = 0; i < 15; i++)
                {
                    randLaps.Add(randState.Next(1,7));
                }
            }
            randKOs = new List<int>();
            /*
            if (Options[RandomizeBattleKOs].Enabled)
            {
                for (int i = 0; i < 5; i++)
                {
                    randKOs.Add(randState.Next(5, 20));
                }
            }
            */

            string[] all_RCF = new string[] {
                path_RCF_default,
                path_RCF_common,
                path_RCF_frontend,
                path_RCF_onfoot0,
                path_RCF_onfoot1,
                path_RCF_onfoot2,
                path_RCF_onfoot3,
                path_RCF_onfoot4,
                path_RCF_onfoot5,
                path_RCF_onfoot6,
                path_RCF_onfoot7,
                path_RCF_advent1,
                path_RCF_advent2,
                path_RCF_advent3,
                path_RCF_adventa,
                path_RCF_dino1,
                path_RCF_dino2,
                path_RCF_dino3,
                path_RCF_dinoa,
                path_RCF_egypt1,
                path_RCF_egypt2,
                path_RCF_egypt3,
                path_RCF_egypta,
                path_RCF_fairy1,
                path_RCF_fairy2,
                path_RCF_fairy3,
                path_RCF_fairys,
                path_RCF_solar1,
                path_RCF_solar2,
                path_RCF_solar3,
                path_RCF_solars,
                path_RCF_0,
                path_RCF_1,
                path_RCF_2,
                path_RCF_3,
                path_RCF_4,
                path_RCF_5,
                path_RCF_6,
                path_RCF_sound,
                path_RCF_english,
            };

            for (int i = 0; i < all_RCF.Length; i++)
            {
                if (all_RCF[i] != "")
                {
                    Modify_RCF(all_RCF[i]);
                }
            }

        }

        void Modify_RCF(string path)
        {
            string path_extr = basePath + @"cml_extr\";
            RCF_Manager.Extract(basePath + path);

            ModCrates.InstallLayerMods(path_extr, 1);

            if (Options[RandomizeCharacters].Enabled)
            {
                CTTR_Randomizers.Randomize_Characters(path_extr, randChars);
            }
            /*
            if (Options[RandomizeHubs].Enabled)
            {
                CTTR_Randomizers.Randomize_Hubs(path_extr, randHubs, randGems);
            }
            */
            if (Options[RandomizeTracks].Enabled)
            {
                CTTR_Randomizers.Randomize_Tracks(path_extr, randTracks);
            }
            if (Options[RandomizeMinigames].Enabled)
            {
                CTTR_Randomizers.Randomize_Minigames(path_extr, randMinigames);
            }
            if (Options[RandomizeRaceLaps].Enabled)
            {
                CTTR_Randomizers.Randomize_Race_Laps(path_extr, randLaps);
            }
            /*
            if (Options[RandomizeBattleKOs].Enabled)
            {
                CTTR_Randomizers.Randomize_Battle_KOs(path_extr, randKOs);
            }
            */
            if (Options[PreventSequenceBreaks].Enabled)
            {
                CTTR_Mods.Mod_PreventSequenceBreaks(path_extr);
            }
            CTTR_Mods.Mod_EditCredits(basePath);

            RCF_Manager.Pack(basePath + path);
        }

        public override void OpenModMenu()
        {

            /*
            basePath = AppDomain.CurrentDomain.BaseDirectory + @"\Tools\";
            RCF_Manager.Extract(basePath + @"default.rcf");

            RCF_Manager.Pack(basePath + @"default.rcf1");
            */

            /*
            Pure3D.File CrashOnfootAnim1 = new Pure3D.File();
            CrashOnfootAnim1.Load(AppDomain.CurrentDomain.BaseDirectory + "/Tools/file.p3d");
            PrintHierarchy(CrashOnfootAnim1.RootChunk, 0);

            GameSpecific.CTTR.Pure3D.ModelExporter.AddSkinnedModelWithAnimations(ref CrashOnfootAnim1.RootChunk.GetChildren<Skin>()[0], ref CrashOnfootAnim1.RootChunk.GetChildren<SkeletonCTTR>()[0], ref shaders);
            GameSpecific.CTTR.Pure3D.ModelExporter.ExportModel(AppDomain.CurrentDomain.BaseDirectory + "/Tools/out.dae");

            Console.WriteLine("\nNow saving...\n");
            CrashOnfootAnim1.Save(AppDomain.CurrentDomain.BaseDirectory + "/Tools/file1.p3d");
            */

        }

        void PrintHierarchy(Chunk chunk, int indent)
        {
            Console.WriteLine("{1}{0}", chunk.ToString(), new string('\t', indent));

            foreach (var child in chunk.Children)
                PrintHierarchy(child, indent + 1);
        }

    }
}
