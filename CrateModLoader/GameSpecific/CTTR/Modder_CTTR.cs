using System;
using System.Collections.Generic;
using System.IO;
using RadcoreCementFile;
using Pure3D;
using Pure3D.Chunks;
using CrateModLoader.GameSpecific.CTTR;
//RCF API by NeoKesha
//Pure3D API by BetaM (based on https://github.com/handsomematt/Pure3D)
//Version number, seed and options are displayed in the Credits accessible from the main menu.

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

            Options.Add(RandomizeCharacters, new ModOption("Randomize Platforming Character")); // todo: change missions to unlock crash and cortex if they're not in the starting pool
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
            //Options.Add(AddUnusedCutscenes, new ModOption("Add Unused Cutscenes")); // todo, NIS + an objective?
            //Options.Add(AddPowerupsTimeTrial, new ModOption("Add Powerups in Time Trial")); // todo: timetrial/props, see: bonus11
            Options.Add(PreventSequenceBreaks, new ModOption("Prevent Sequence Breaks"));
            //Options.Add(ReplaceCrashinatorConeAttack, new ModOption("Replace Crashinator with Cone Attack")); // todo
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

            randState = new Random(Program.ModProgram.randoSeed);


            bool Editing_Credits = true;
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

            if (Editing_Credits)
            {
                Mod_EditCredits();
            }

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

            if (path_RCF_default != "")
            {
                Modify_RCF(path_RCF_default);
            }
            if (path_RCF_common != "")
            {
                Modify_RCF(path_RCF_common);
            }
            if (path_RCF_frontend != "")
            {
                Modify_RCF(path_RCF_frontend);
            }
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
            RCF rcf_default = new RCF();
            rcf_default.OpenRCF(basePath + path);
            string path_extr = basePath + @"cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_default.ExtractRCF(path_extr);

            if (Options[RandomizeCharacters].Enabled)
            {
                Randomize_Characters(path_extr, rcf_default, randChars);
            }
            /*
            if (Options[RandomizeHubs].Enabled)
            {
                Randomize_Hubs(path_extr, rcf_default, randHubs, randGems);
            }
            */
            if (Options[RandomizeTracks].Enabled)
            {
                Randomize_Tracks(path_extr, rcf_default, randTracks);
            }
            if (Options[RandomizeMinigames].Enabled)
            {
                Randomize_Minigames(path_extr, rcf_default, randMinigames);
            }
            if (Options[RandomizeRaceLaps].Enabled)
            {
                Randomize_Race_Laps(path_extr, rcf_default, randLaps);
            }
            /*
            if (Options[RandomizeBattleKOs].Enabled)
            {
                Randomize_Battle_KOs(path_extr, rcf_default, randKOs);
            }
            */
            if (Options[PreventSequenceBreaks].Enabled)
            {
                Mod_PreventSequenceBreaks(path_extr, rcf_default);
            }

            rcf_default.Recalculate();
            rcf_default.Pack(basePath + path + "1");


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

        void Randomize_Characters(string path_extr, RCF rcf_file, List<int> randChars)
        {
            /* TODO later, because it requires mission logic to unlock Crash/Cortex
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
                if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "UnlockDefaults", ref characterList_Start, ref characterList_End, DefaultUnlocks))
                {
                    DefaultUnlocks.Clear();
                    DefaultUnlocks.Add("this.SetName(\"UnlockDefaults\")");
                    for (int i = 0; i < randChars.Count; i++)
                    {
                        DefaultUnlocks.Add("this.AddAction_UnlockCar(\"" + CTTR_Data.DriverNames[randChars[i]] + "\",1)");
                    }
                }
                CTTR_Data.LUA_SaveObject(LineList, "Objective", "UnlockDefaults", DefaultUnlocks);

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
            */
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
                if (CTTR_Data.LUA_LoadObject(LineList, "Skin", "CrashDefault", ref skin_Start, ref skin_End, SkinObj))
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
                CTTR_Data.LUA_SaveObject(LineList, "Skin", "CrashDefault", SkinObj);

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
                Pure3D.File targetCharAnim = new Pure3D.File();
                if (System.IO.File.Exists(path_extr + @"art\animation\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d"))
                {
                    targetCharAnim.Load(path_extr + @"art\animation\\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d");
                }
                else if (System.IO.File.Exists(path_extr + @"art\animation\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d"))
                {
                    targetCharAnim.Load(path_extr + @"art\animation\\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d");
                }
                else
                {
                    return;
                }

                Pure3D.Chunk targetIdleAnim;
                if (targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                {
                    targetIdleAnim = targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_idle");
                }
                else if (targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_talk_bored") != null) // Nina doesn't have an idle animation
                {
                    Animation targetIdleAnimAnim;
                    targetIdleAnimAnim = targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_talk_bored");
                    targetIdleAnimAnim.Name = "onfoot_idle";
                    targetIdleAnim = (Pure3D.Chunk)targetIdleAnimAnim;
                }
                else
                {
                    return;
                }

                if (System.IO.File.Exists(path_extr + @"art\animation\crash_onfoot_animations.p3d"))
                {
                    Pure3D.File CrashOnfootAnim = new Pure3D.File();
                    CrashOnfootAnim.Load(path_extr + @"art\animation\crash_onfoot_animations.p3d");

                    if (CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                    {
                        int animIndex = CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.GetChildIndexByName<Animation>("onfoot_idle");
                        CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.Children[animIndex] = targetIdleAnim;
                    }

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

                    if (CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                    {
                        int animIndex = CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.GetChildIndexByName<Animation>("onfoot_idle");
                        CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.Children[animIndex] = targetIdleAnim;
                    }

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
        void Randomize_Hubs(string path_extr, RCF rcf_file, List<int> randHubs, List<int> randGems)
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
                for (int i = 0; i < 5; i++)
                {
                    string targetHub = CTTR_Data.HubNamesSimple[i + 1];
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayTo" + targetHub, ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_ChangeLevel(\"" + CTTR_Data.HubNames[randHubs[i]] + "\",\"StartLocationFromMidway\")";
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayTo" + targetHub, ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevel" + CTTR_Data.HubNamesSimple[randHubs[i]] + "ToMidway", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_ChangeLevel(\"onfoot_midway\",\"StartLocationFrom" + targetHub + "\")";
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevel" + CTTR_Data.HubNamesSimple[randHubs[i]] + "ToMidway", ChangeHubObjective);
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
                        if (CTTR_Data.LUA_LoadObject(LineList, "Objective", CTTR_Data.MissionObjectiveHubNamesSimple[i] + "KeyCollection", ref List_Start, ref List_End, ChangeHubObjective))
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
                            CTTR_Data.LUA_SaveObject(LineList, "Objective", CTTR_Data.MissionObjectiveHubNamesSimple[i] + "KeyCollection", ChangeHubObjective);
                        }
                        ChangeHubObjective.Clear();
                        if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate", ref List_Start, ref List_End, ChangeHubObjective))
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
                            CTTR_Data.LUA_SaveObject(LineList, "Objective", "Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate", ChangeHubObjective);
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
        void Randomize_Tracks(string path_extr, RCF rcf_file, List<int> randTracks)
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
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "StartRace" + CTTR_Data.TrackNamesSimple[i], ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\")";
                        ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\",\"ReturnFromRace" + CTTR_Data.TrackNamesSimple[i] + "\")";
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "StartRace" + CTTR_Data.TrackNamesSimple[i], ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }
                if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "StartRaceFromMidway2", ref List_Start, ref List_End, ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\",\"ReturnFromMidwayRaces\")";
                    CTTR_Data.LUA_SaveObject(LineList, "Objective", "StartRaceFromMidway2", ChangeHubObjective);
                }
                ChangeHubObjective.Clear();
                if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "StartRaceFromMidway3", ref List_Start, ref List_End, ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\",\"ReturnFromMidwayRaces2\")";
                    CTTR_Data.LUA_SaveObject(LineList, "Objective", "StartRaceFromMidway3", ChangeHubObjective);
                }
                ChangeHubObjective.Clear();
                if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "BuyRaceTicketWithTrack", ref List_Start, ref List_End, ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\")";
                    CTTR_Data.LUA_SaveObject(LineList, "Objective", "BuyRaceTicketWithTrack", ChangeHubObjective);
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
                        if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "UnlockRace" + CTTR_Data.TrackNamesSimple[i], ref List_Start, ref List_End, ChangeHubObjective))
                        {
                            ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\")";
                            ChangeHubObjective[ChangeHubObjective.Count - 1] = "this.AddAction_DisplayMessage(\"" + CTTR_Data.TrackGateNames[randTracks[i]] +"\",1.0,6.0)";
                            CTTR_Data.LUA_SaveObject(LineList, "Objective", "UnlockRace" + CTTR_Data.TrackNamesSimple[i], ChangeHubObjective);
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
        void Randomize_Minigames(string path_extr, RCF rcf_file, List<int> randMinigames)
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
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", CTTR_Data.MinigameObjectiveTypes[i], ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_UnlockMiniGame(\"OFMiniGames/" + CTTR_Data.MinigameTypeNames[randMinigames[i]] + "\")";
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", CTTR_Data.MinigameObjectiveTypes[i], ChangeHubObjective);
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
        void Randomize_Race_Laps(string path_extr, RCF rcf_file, List<int> randLaps)
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
        void Randomize_Battle_KOs(string path_extr, RCF rcf_file, List<int> randKOs)
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

        void Mod_PreventSequenceBreaks(string path_extr, RCF rcf_file)
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
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToFairy", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_fairy\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToFairy", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToDino", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_dino\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToDino", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToEgypt", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_egypt\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToEgypt", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToSolar", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_solar\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToSolar", ChangeHubObjective);
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
            if (System.IO.File.Exists(path_extr + @"design\permanent\missionobjectives_fairy.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_fairy.god");
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
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "DinoKeyCollection", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"WeenieUnlocked_fairy\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "DinoKeyCollection", ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_fairy.god", objective_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\permanent\missionobjectives_fairy.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\missionobjectives_fairy.god";
                        break;
                    }
                }
            }
        }

        void Mod_EditCredits()
        {
            RCF rcf_frontend = new RCF();
            rcf_frontend.OpenRCF(basePath + path_RCF_frontend);
            //Warning: The RCF API only likes paths with \ backslashes
            string path_extr = basePath + @"cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_frontend.ExtractRCF(path_extr);

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
            rcf_frontend.Pack(basePath + path_RCF_frontend + "1");


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

        public override void OpenModMenu()
        {

            //Pure3D.File targetCharAnim = new Pure3D.File();
            //targetCharAnim.Load(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ngin_onfoot_animations.p3d");
            //Pure3D.Chunk targetIdleAnim = targetCharAnim.RootChunk.Children[0];

            /*
            string path_extr = "";
            basePath = AppDomain.CurrentDomain.BaseDirectory + @"\Tools\";
            RCF rcf_frontend = new RCF();
            rcf_frontend.OpenRCF(basePath + @"default.rcf");
            path_extr = basePath + @"cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_frontend.ExtractRCF(path_extr);

            rcf_frontend.Recalculate();
            rcf_frontend.Pack(basePath + @"default.rcf1");
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

            /*
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

        void PrintHierarchy(Chunk chunk, int indent)
        {
            Console.WriteLine("{1}{0}", chunk.ToString(), new string('\t', indent));

            foreach (var child in chunk.Children)
                PrintHierarchy(child, indent + 1);
        }

    }
}
