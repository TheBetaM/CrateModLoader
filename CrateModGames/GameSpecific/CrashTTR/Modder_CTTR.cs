using System;
using System.Collections.Generic;
using System.IO;
using Pure3D;
using Pure3D.Chunks;
//RCF API by NeoKesha
//Pure3D API by BetaM (based on https://github.com/handsomematt/Pure3D)
/* 
 * Mod Layers:
 * 1: All .RCF file contents (only replace files)
 */

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public sealed class Modder_CTTR : Modder
    {
        internal string path_RCF_default = "";
        internal string path_RCF_common = "";
        internal string path_RCF_frontend = "";
        //internal string path_executable = "";
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
        internal string path_RCF_movies = "";
        internal string basePath = "";

        public Modder_CTTR()
        {

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
            //path_executable = "";
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
            path_RCF_movies = "";

            if (console == ConsoleMode.PS2)
            {
                //path_executable = exec_name;
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
                path_RCF_movies = @"MOVIES.RCF";
            }
            else if (console == ConsoleMode.PSP)
            {
                //path_executable = @"PSP_GAME\SYSDIR\BOOT.BIN";
                path_RCF_default = @"adefault\default.rcf";
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
                path_RCF_egypta = @"egypt\egypta.rcf";
                path_RCF_english = @"english.rcf";
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
                path_RCF_onfoot4 = @"onfoot\onfoot4.rcf";
                path_RCF_onfoot5 = @"onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"onfoot\onfoot6.rcf";
                path_RCF_movies = @"movies.rcf";
            }
            else if (console == ConsoleMode.GCN)
            {
                // path_executable = @"sys\main.dol";
                path_RCF_default = @"adefault\default.rcf";
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
                path_RCF_english = @"english.rcf";
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
                path_RCF_onfoot7 = @"onfoot\onfoot7.rcf";
                path_RCF_movies = @"movies.rcf";
            }
            else
            {
                //path_executable = @"default.xbe";
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
            SetPaths(ConsolePipeline.Metadata.Console, GameRegion.ExecName);
            basePath = ConsolePipeline.ExtractedPath;

            RCF_Manager.cachedRCF = null;

            randState = new Random(ModLoaderGlobals.RandomizerSeed);

            EditDefaultAndCommon();

            CTTR_Randomizers.targetCharAnim = null;
            CTTR_Randomizers.targetIdleAnim = null;

        }

        void EditDefaultAndCommon()
        {
            randChars = new List<int>();
            if (CTTR_Props_Main.Option_RandCharacters.Enabled)
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
            if (GetOption(RandomizeHubs))
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
            if (CTTR_Props_Main.Option_RandTrackEntrances.Enabled)
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
            if (CTTR_Props_Main.Option_RandRaceLaps.Enabled)
            {
                for (int i = 0; i < 15; i++)
                {
                    if (i == 12) // Rings of Uranus
                    {
                        randLaps.Add(randState.Next(3, 13));
                    }
                    else
                    {
                        randLaps.Add(randState.Next(1, 7));
                    }
                }
            }
            randKOs = new List<int>();
            /*
            if (GetOption(RandomizeBattleKOs))
            {
                for (int i = 0; i < 5; i++)
                {
                    randKOs.Add(randState.Next(5, 20));
                }
            }
            */

            List<string> all_RCF = new List<string> {
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
                //path_RCF_sound,
                //path_RCF_english,
                //path_RCF_movies,
            };

            if (ModCrates.HasLayerModsActive(EnabledModCrates, 1)) // these take forever, so only if they're needed
            {
                all_RCF.Add(path_RCF_movies);
                all_RCF.Add(path_RCF_english);
                all_RCF.Add(path_RCF_sound);
            }

            for (int i = 0; i < all_RCF.Count; i++)
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
            RCF_Manager.Extract(basePath + path, path_extr);

            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            ParseSettings(path_extr);

            if (CTTR_Props_Main.Option_RandCharacters.Enabled)
            {
                CTTR_Randomizers.Randomize_Characters(path_extr, randChars);
            }
            /*
            if (GetOption(RandomizeHubs))
            {
                CTTR_Randomizers.Randomize_Hubs(path_extr, randHubs, randGems);
            }
            */
            if (CTTR_Props_Main.Option_RandTrackEntrances.Enabled)
            {
                CTTR_Randomizers.Randomize_Tracks(path_extr, randTracks);
            }
            if (CTTR_Props_Main.Option_RandMinigames.Enabled)
            {
                CTTR_Randomizers.Randomize_Minigames(path_extr, randMinigames);
            }
            if (CTTR_Props_Main.Option_RandRaceLaps.Enabled)
            {
                CTTR_Randomizers.Randomize_Race_Laps(path_extr, randLaps);
            }
            /*
            if (GetOption(RandomizeBattleKOs))
            {
                CTTR_Randomizers.Randomize_Battle_KOs(path_extr, randKOs);
            }
            */
            if (CTTR_Props_Main.Option_NoSequenceBreaks.Enabled)
            {
                CTTR_Mods.Mod_PreventSequenceBreaks(path_extr);
            }
            CTTR_Mods.Mod_EditCredits(basePath);

            RCF_Manager.Pack(basePath + path, path_extr);
        }

        public void OpenModMenu()
        {

            /*
            basePath = ModLoaderGlobals.ToolsPath;
            RCF_Manager.Extract(basePath + @"frontend.rcf", basePath + @"temp\");

            RCF_Manager.Pack(basePath + @"exfrontend.rcf", basePath + @"temp\");
            */


            
            Pure3D.File CrashOnfootAnim1 = new Pure3D.File();
            CrashOnfootAnim1.Load(ModLoaderGlobals.ToolsPath + "file.p3d");
            PrintHierarchy(CrashOnfootAnim1.RootChunk, 0);

            Shader[] shaders = CrashOnfootAnim1.RootChunk.GetChildren<Shader>();
            //ModelExporter.AddSkinnedModelWithAnimations(ref CrashOnfootAnim1.RootChunk.GetChildren<Skin>()[0], ref CrashOnfootAnim1.RootChunk.GetChildren<SkeletonCTTR>()[0], ref shaders);
            //ModelExporter.ExportModel(ModLoaderGlobals.ToolsPath + "out.dae");

            /*
            Console.WriteLine("\nNow saving...\n");
            CrashOnfootAnim1.Save(ModLoaderGlobals.ToolsPath + "file1.p3d");
            */
            

        }

        void PrintHierarchy(Chunk chunk, int indent)
        {
            Console.WriteLine("{1}{0}", chunk.ToString(), new string('\t', indent));

            foreach (var child in chunk.Children)
                PrintHierarchy(child, indent + 1);
        }

        void ParseSettings(string path_extr)
        {
            // example setting
            if (CTTR_Props_Misc.RaceLaps.HasChanged)
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
                    LineList[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[0] + ",true},";
                    LineList[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[1] + ",true},";
                    LineList[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[2] + ",true},";
                    LineList[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[3] + ",true},";
                    LineList[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[4] + ",true},";
                    LineList[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[5] + ",true},";
                    LineList[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[6] + ",true},";
                    LineList[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[7] + ",true},";
                    LineList[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[8] + ",true},";
                    LineList[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[9] + ",true},";
                    LineList[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[10] + ",true},";
                    LineList[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[11] + ",true},";
                    LineList[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[12] + ",true},";
                    LineList[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[13] + ",true},";
                    LineList[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[14] + ",true},";

                    startup_lines = new string[LineList.Count];
                    for (int i = 0; i < LineList.Count; i++)
                    {
                        startup_lines[i] = LineList[i];
                    }

                    System.IO.File.WriteAllLines(path_extr + @"design\startup.god", startup_lines);

                }
            }
        }
    }
}
