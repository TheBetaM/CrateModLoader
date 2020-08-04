using System;
using System.IO;
using System.Collections.Generic;
using CrateModLoader.GameSpecific.CrashTWOC;

namespace CrateModLoader
{
    public sealed class Modder_TWOC : Modder
    {

        internal const int RandomizeLevelOrder = 0;
        internal const int RandomizeCrates = 1;
        internal const int RandomizeMusic = 2;

        public Modder_TWOC()
        {
            Game = new Game()
            {
                Name = "Crash Bandicoot: The Wrath of Cortex",
                ShortName = "CrashTWOC",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    //ConsoleMode.GCN,
                    ConsoleMode.XBOX,
                },
                API_Credit = string.Empty,
                API_Link = string.Empty,
                Icon = Properties.Resources.icon_crashtwoc,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"SLUS_202.38",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_202.38",
                    CodeName = "SLUS_20238", },
                    new RegionCode() {
                    Name = @"SLES_503.86",
                    Region = RegionType.PAL,
                    ExecName = "SLES_503.86",
                    CodeName = "SLES_50386", },
                    new RegionCode() {
                    Name = @"SLPM_740.03",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_740.03",
                    CodeName = "SLPM_74003", },
                },
                RegionID_GCN = new RegionCode[] {
                    new RegionCode() {
                    Name = "GCBE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GCBP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "GCBJA4",
                    Region = RegionType.NTSC_J },
                },
                RegionID_XBOX = new RegionCode[]
                {
                    new RegionCode() {
                        Name = "Crash Bandicoot: tWoC",
                        Region = RegionType.NTSC_U,
                        RegionNumber = 1, },
                    new RegionCode() {
                        Name = "Crash Bandicoot: tWoC",
                        Region = RegionType.NTSC_U,
                        RegionNumber = 7, },
                    new RegionCode() {
                        Name = "Crash Bandicoot: tWoC",
                        Region = RegionType.PAL,
                        RegionNumber = 4, },
                },
            };

            //AddOption(RandomizeCrates, new ModOption("Randomize Crates"));
            //AddOption(RandomizeLevelOrder, new ModOption("Randomize Level Order")); //doesn't work yet
            //AddOption(RandomizeMusic, new ModOption("Randomize Music", new List<ConsoleMode>() { ConsoleMode.GCN, ConsoleMode.XBOX })); //not tested
            
        }

        public override void StartModProcess()
        {
            if (ModLoaderGlobals.Console == ConsoleMode.GCN)
            {
                // rebuilding the GC version makes it not boot for some reason...
                return;
            }

            ModProcess();
        }

        protected override void ModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            if (GetOption(RandomizeCrates))
            {
                Mod_RandomizeCrates(rand);
            }
            if (GetOption(RandomizeLevelOrder))
            {
                Mod_RandomizeLevelOrder(rand);
            }
            if (GetOption(RandomizeMusic))
            {
                Mod_RandomizeMusic(rand);
            }
        }

        public enum TWOC_Levels
        {
            L01_ArcticAntics = 0, //SNOW_M
            L02_TornadoAlley = 1, //FARM
            L03_Bamboozled = 2, //JUNGLE_A
            L04_WizardsAndLizards = 3, //CASTLE_C
            L05_CompactorReactor = 4, //EARTH_R
            L06_JungleRumble = 5, //JUNGLE_R
            L07_SeaShellShenanigans = 6, //SEASHELL
            L08_BanzaiBonzai = 7, //GARDEN
            L09_ThatSinkingFeeling = 8, //FIRE_FLY
            L10_H2OhNo = 9, //WATER_R
            L11_TheGauntlet = 10, //CASTLE
            L12_Tsunami = 11, //TSUNAMI
            L13_SmokeyAndTheBandicoot = 12, //WEST_A
            L14_EskimoRoll = 13, //COLD_A
            L15_FahrenheitFrenzy = 14, //FIRE_R
            L16_Avalanche = 15, //AVALANCH
            L17_DroidVoid = 16, //DROID
            L18_Crashteroids = 17, //SPACE_A
            L19_CoralCanyon = 18, //CORAL_C
            L20_WeatheringHeights = 19, //WEATH_R
            L21_CrashAndBurn = 20, //VOLCANO
            L22_GoldRush = 21, //WESTERN
            L23_MedievalMadness = 22, //CASTLE_A
            L24_CrateBallsOfFire = 23, //FIREBALL
            L25_CortexVortex = 24, //SPACE_R
            L26_KnightTime = 25, //CAST_BUG
            L27_GhostTown = 26, //WEST_B
            L28_IceStationBandicoot = 27, //COLD_B
            L29_SolarBowler = 28, //S_BALLS
            L30_ForceOfNature = 29, //SNOW_B
            B01_Earth = 30, //EARTH_B
            B02_Water = 31, //WATER_B
            B03_Fire = 32, //FIRE_B
            B04_Air = 33, //WEATH_B
            B05_Cortex = 34, //SPACE_B
        }

        string[] LevelNames = new string[]
        {
            "SNOW_M",
            "FARM",
            "JUNGLE_A",
            "CASTLE_C",
            "EARTH_R",
            "JUNGLE_R",
            "SEASHELL",
            "GARDEN",
            "FIRE_FLY",
            "WATER_R",
            "CASTLE",
            "TSUNAMI",
            "WEST_A",
            "COLD_A",
            "FIRE_R",
            "AVALANCH",
            "DROID",
            "SPACE_A",
            "CORAL_C",
            "WEATH_R",
            "VOLCANO",
            "WESTERN",
            "CASTLE_A",
            "FIREBALL",
            "SPACE_R",
            "CAST_BUG",
            "WEST_B",
            "COLD_B",
            "S_BALLS",
            "SNOW_B",
            "EARTH_B",
            "WATER_B",
            "FIRE_B",
            "WEATH_B",
            "SPACE_B",
        };

        string[] FileNames = new string[]
        {
            "SNOW",
            "FARM",
            "JUNGLE_A",
            "CASTLE_C",
            "EARTH",
            "JUNGLE",
            "SEASHELL",
            "GARDEN",
            "FIRE_FLY",
            "WATER",
            "CASTLE",
            "TOONARMY",
            "WEST_A",
            "COLD_A",
            "FIRE_R",
            "AVALANCH",
            "DROID",
            "SPACE_A",
            "CORAL",
            "WEATHER",
            "VOLCANO",
            "WESTERN",
            "CASTLE_A",
            "BALLSOF",
            "SPACE_R",
            "CAST_BUG",
            "WEST_B",
            "COLD_B",
            "S_BALLS",
            "SNOW_B",
            "EARTH_B",
            "WATER_B",
            "FIRE_B",
            "WEATH_B",
            "SPACE_B",
        };

        void Mod_RandomizeCrates(Random rand)
        {

        }

        void Mod_RandomizeLevelOrder(Random rand)
        {
            string LevelsPath = @"LEVELS\A\";
            if (ModLoaderGlobals.Console != ConsoleMode.PS2)
            {
                LevelsPath = LevelsPath.ToLower();
            }
            int maxLevel = LevelNames.Length - 10;

            List<int> LevelsToRand = new List<int>();
            for (int i = 0; i < maxLevel; i++)
            {
                Directory.Move(ModLoaderGlobals.ExtractedPath + LevelsPath + LevelNames[i], ModLoaderGlobals.ExtractedPath + LevelsPath + "LEVEL" + i);
                LevelsToRand.Add(i);
            }

            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < maxLevel; i++)
            {
                int r = rand.Next(LevelsToRand.Count);
                LevelsRand.Add(LevelsToRand[r]);
                LevelsToRand.RemoveAt(r);
            }

            for (int i = 0; i < maxLevel; i++)
            {
                Directory.Move(ModLoaderGlobals.ExtractedPath + LevelsPath + "LEVEL" + i, ModLoaderGlobals.ExtractedPath + LevelsPath + LevelNames[LevelsRand[i]]);

                if (i != LevelsRand[i])
                {
                    DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.ExtractedPath + LevelsPath + LevelNames[LevelsRand[i]]);
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        if (file.Name.Contains(FileNames[i]))
                        {
                            file.MoveTo(di.FullName + @"\" + FileNames[LevelsRand[i]] + "." + file.Extension);
                        }
                    }
                }
            }



        }

        List<string> MusicNames = new List<string>()
        {
            "ArcticAL",
            "AtmosphL",
            "AvalancL",
            "BanzaiBL",
            "CAndBurL",
            "CompactL",
            "CoralCaL",
            "CortexVL",
            "CraAsheL",
            "CrashteL",
            "CrateBaL",
            "CrTimeL",
            "DrainDaL",
            "DroidVoL",
            "EskimoRL",
            "FarenheL",
            "ForceL",
            "GauntleL",
            "GhostToL",
            "GoldRusL",
            "H2OhNoL",
            "IceStatL",
            "JuRumblL",
            "KnightTL",
            "RokNRolL",
            "RokRumbL",
            "SeashelL",
            "SinkingL",
            "SmokeyL",
            "SolarBoL",
            "ThemeL",
            "TornadoL",
            "TsunamiL",
            "WeatheiL",
            "WizardsL",
        };
        string Music_GC_Extra = "Gauntl2";

        void Mod_RandomizeMusic(Random rand)
        {
            if (ModLoaderGlobals.Console == ConsoleMode.PS2)
            {
                return;
            }
            string musicPath;
            string ext;
            if (ModLoaderGlobals.Console == ConsoleMode.GCN)
            {
                musicPath = ModLoaderGlobals.ExtractedPath + @"sfx\music\";
                ext = ".adp";
                if (!MusicNames.Contains(Music_GC_Extra))
                {
                    MusicNames.Add(Music_GC_Extra);
                }
            }
            else
            {
                musicPath = ModLoaderGlobals.ExtractedPath + @"sfx\Music\";
                ext = ".wav";
                if (MusicNames.Contains(Music_GC_Extra))
                {
                    MusicNames.Remove(Music_GC_Extra);
                }
            }

            int maxCount = MusicNames.Count;

            List<int> MusicToRand = new List<int>();
            for (int i = 0; i < maxCount; i++)
            {
                File.Move(musicPath + MusicNames[i] + ext, musicPath + "mus" + i);
                MusicToRand.Add(i);
            }

            List<int> MusicRand = new List<int>();
            for (int i = 0; i < maxCount; i++)
            {
                int r = rand.Next(MusicToRand.Count);
                MusicRand.Add(MusicToRand[r]);
                MusicToRand.RemoveAt(r);
            }

            for (int i = 0; i < maxCount; i++)
            {
                File.Move(musicPath + "mus" + i, musicPath + MusicNames[MusicRand[i]] + ext);
            }

        }

    }
}
