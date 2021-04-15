using System;
using System.IO;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public sealed class Modder_TWOC : Modder
    {

        public override Game Game => new Game()
        {
            Name = "Crash Bandicoot: The Wrath of Cortex",
            ShortName = "CrashTWOC",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    //ConsoleMode.GCN,
                    //ConsoleMode.XBOX,
                },
            API_Credit = string.Empty,
            API_Link = string.Empty,
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS2] = new RegionCode[]
                {
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
                [ConsoleMode.GCN] = new RegionCode[]
                {
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
                [ConsoleMode.XBOX] = new RegionCode[]
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
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [0] = "Options",
            },
        };

        public static ModPropOption Option_RandCrates = new ModPropOption("Randomize Wooden Crates", "The types of wooden crates are randomized.");
        public static ModPropOption Option_RandCratesRemoved = new ModPropOption("Random Crates Removed", "Wooden crates are randomly removed in each level. The box counter is adjusted accordingly.")
        { Hidden = true, };
        public static ModPropOption Option_RandEnemyPaths = new ModPropOption("Randomize Enemy Paths", "Reverses paths of random enemies.");
        public static ModPropOption Option_RandEnemiesRemoved = new ModPropOption("Random Enemies Removed", "Enemies are randomly removed in each level.");
        [ModAllowedConsoles(ConsoleMode.GCN, ConsoleMode.XBOX)]
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "Music tracks are shuffled around."); //works on xbox

        public static ModPropOption Option_RandWumpaCrates = new ModPropOption("Random Wumpa Are Random Crates", "Wumpas are randomly turned into crates in each level. The box counter is adjusted accordingly.")
        { Hidden = true, }; //todo: new box positions are off
        public static ModPropOption Option_RandEnemyCrates = new ModPropOption("Random Enemies Are Random Crates", "Enemies are randomly turned into random cratesin each level. The box counter is adjusted accordingly.")
        { Hidden = true, }; //todo
        [ModMenuOnly]
        public static ModPropOption Option_AllEnemyCrates = new ModPropOption("All Enemies Are Random Crates", "The box counter is adjusted accordingly.")
        { Hidden = true, }; //todo
        public static ModPropOption Option_RandLevelOrder = new ModPropOption("Randomize Level Order", "") //todo, unbeatable levels, enemies not spawning, etc.
        { Hidden = true, };
        public static ModPropOption Option_SphereLevelsOnFoot = new ModPropOption("Atlasphere Levels On Foot", "") //todo: camera!!!
        { Hidden = true, };
        

        public Modder_TWOC()
        {

        }

        public override void StartModProcess()
        {
            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                // rebuilding the GC version makes it not boot for some reason...
                return;
            }

            ModProcess();
        }

        void ModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            if (Option_RandCrates.Enabled)
                Rand_RandomizeCrates(rand);
            if (Option_RandWumpaCrates.Enabled)
                Rand_WumpaCrates(rand);
            if (Option_RandCratesRemoved.Enabled)
                Rand_CratesRemoved(rand);
            if (Option_RandLevelOrder.Enabled)
                Mod_RandomizeLevelOrder(rand);
            if (Option_RandEnemiesRemoved.Enabled)
                Rand_EnemiesRemoved(rand);
            if (Option_RandEnemyPaths.Enabled)
                Rand_EnemyPaths(rand);
            if (Option_SphereLevelsOnFoot.Enabled)
            {
                Mod_ReplaceLevel(TWOC_Levels.L03_Bamboozled, TWOC_Levels.L14_EskimoRoll);
            }
            if (Option_RandMusic.Enabled)
                Mod_RandomizeMusic(rand);
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

        public List<TWOC_Levels> LevelList_Onfoot = new List<TWOC_Levels>()
        {
            TWOC_Levels.L01_ArcticAntics,
            TWOC_Levels.L04_WizardsAndLizards,
            TWOC_Levels.L05_CompactorReactor,
            TWOC_Levels.L06_JungleRumble,
            TWOC_Levels.L08_BanzaiBonzai,
            TWOC_Levels.L11_TheGauntlet,
            TWOC_Levels.L12_Tsunami,
            TWOC_Levels.L15_FahrenheitFrenzy,
            TWOC_Levels.L16_Avalanche,
            TWOC_Levels.L17_DroidVoid,
            TWOC_Levels.L20_WeatheringHeights,
            TWOC_Levels.L21_CrashAndBurn,
            TWOC_Levels.L22_GoldRush,
            TWOC_Levels.L25_CortexVortex,
            TWOC_Levels.L26_KnightTime,
            TWOC_Levels.L30_ForceOfNature,
        };

        public List<TWOC_Levels> LevelList_Sphere = new List<TWOC_Levels>()
        {
            TWOC_Levels.L03_Bamboozled,
            TWOC_Levels.L14_EskimoRoll,
            TWOC_Levels.L23_MedievalMadness,
            TWOC_Levels.L29_SolarBowler,
        };

        public List<TWOC_Levels> LevelList_Underwater = new List<TWOC_Levels>()
        {
            TWOC_Levels.L07_SeaShellShenanigans,
            TWOC_Levels.L10_H2OhNo,
            TWOC_Levels.L19_CoralCanyon,
        };

        public List<TWOC_Levels> LevelList_Flying = new List<TWOC_Levels>()
        {
            TWOC_Levels.L02_TornadoAlley,
            TWOC_Levels.L09_ThatSinkingFeeling,
            TWOC_Levels.L18_Crashteroids,
            TWOC_Levels.L24_CrateBallsOfFire,
            TWOC_Levels.L28_IceStationBandicoot,
        };

        public List<TWOC_Levels> LevelList_Racing = new List<TWOC_Levels>()
        {
            TWOC_Levels.L13_SmokeyAndTheBandicoot,
            TWOC_Levels.L27_GhostTown,
        };

        void Mod_RandomizeLevelOrder(Random rand)
        {
            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
            }
            int maxLevel = LevelNames.Length; // - 5;

            List<TWOC_Levels> LevelsNoRand = new List<TWOC_Levels>()
            {
                TWOC_Levels.L03_Bamboozled,
                TWOC_Levels.L14_EskimoRoll,
                TWOC_Levels.L23_MedievalMadness,
                TWOC_Levels.L29_SolarBowler,

                TWOC_Levels.L07_SeaShellShenanigans,
                TWOC_Levels.L10_H2OhNo,
                TWOC_Levels.L19_CoralCanyon,

                TWOC_Levels.L02_TornadoAlley,
                TWOC_Levels.L09_ThatSinkingFeeling,
                TWOC_Levels.L18_Crashteroids,
                TWOC_Levels.L24_CrateBallsOfFire,
                TWOC_Levels.L28_IceStationBandicoot,

                TWOC_Levels.L13_SmokeyAndTheBandicoot,
                TWOC_Levels.L27_GhostTown,

                TWOC_Levels.B01_Earth,
                TWOC_Levels.B02_Water,
                TWOC_Levels.B03_Fire,
                TWOC_Levels.B04_Air,
                TWOC_Levels.B05_Cortex,
            };

            List<int> LevelsToRand = new List<int>();
            for (int i = 0; i < maxLevel; i++)
            {
                if (i < 25)
                {
                    Directory.Move(ConsolePipeline.ExtractedPath + LevelsPathA + LevelNames[i], ConsolePipeline.ExtractedPath + LevelsPathA + "LEVEL" + i);
                }
                else
                {
                    Directory.Move(ConsolePipeline.ExtractedPath + LevelsPathC + LevelNames[i], ConsolePipeline.ExtractedPath + LevelsPathC + "LEVEL" + i);
                }
                
                if (!LevelsNoRand.Contains((TWOC_Levels)i))
                {
                    LevelsToRand.Add(i);
                }
            }

            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < maxLevel; i++)
            {
                if (LevelsNoRand.Contains((TWOC_Levels)i))
                {
                    LevelsRand.Add(i);
                }
                else
                {
                    int r = rand.Next(LevelsToRand.Count);
                    LevelsRand.Add(LevelsToRand[r]);
                    LevelsToRand.RemoveAt(r);
                }
            }

            for (int i = 0; i < maxLevel; i++)
            {
                string LevelPathIn = LevelsPathA;
                string LevelPathOut = LevelsPathA;
                if (i > 24)
                {
                    LevelPathIn = LevelsPathC;
                }
                if (LevelsRand[i] > 24)
                {
                    LevelPathOut = LevelsPathC;
                }

                Directory.Move(ConsolePipeline.ExtractedPath + LevelPathIn + "LEVEL" + i, ConsolePipeline.ExtractedPath + LevelPathOut + LevelNames[LevelsRand[i]]);

                if (i != LevelsRand[i])
                {
                    DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath + LevelPathOut + LevelNames[LevelsRand[i]]);
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        if (file.Name.ToUpper().Contains(FileNames[i]))
                        {
                            file.MoveTo(di.FullName + @"\" + FileNames[LevelsRand[i]] + file.Extension);
                        }
                    }
                }
            }
        }

        void Mod_ReplaceLevel(TWOC_Levels Level1, TWOC_Levels Level2)
        {
            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
            }
            int maxLevel = LevelNames.Length;

            string LevelPathIn = LevelsPathA;
            string LevelPathOut = LevelsPathA;
            if ((int)Level1 > 24)
            {
                LevelPathIn = LevelsPathC;
            }
            if ((int)Level2 > 24)
            {
                LevelPathOut = LevelsPathC;
            }

            Directory.Move(ConsolePipeline.ExtractedPath + LevelPathIn + LevelNames[(int)Level1], ConsolePipeline.ExtractedPath + LevelPathIn + "LEVEL");
            Directory.Move(ConsolePipeline.ExtractedPath + LevelPathOut + LevelNames[(int)Level2], ConsolePipeline.ExtractedPath + LevelPathIn + LevelNames[(int)Level1]);
            Directory.Move(ConsolePipeline.ExtractedPath + LevelPathIn + "LEVEL", ConsolePipeline.ExtractedPath + LevelPathOut + LevelNames[(int)Level2]);

            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath + LevelPathOut + LevelNames[(int)Level2]);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Name.ToUpper().Contains(FileNames[(int)Level1]))
                {
                    file.MoveTo(di.FullName + @"\" + FileNames[(int)Level2] + file.Extension);
                }
            }

            di = new DirectoryInfo(ConsolePipeline.ExtractedPath + LevelPathIn + LevelNames[(int)Level1]);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Name.ToUpper().Contains(FileNames[(int)Level2]))
                {
                    file.MoveTo(di.FullName + @"\" + FileNames[(int)Level1] + file.Extension);
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
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                return;
            }
            string musicPath;
            string ext;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                musicPath = ConsolePipeline.ExtractedPath + @"sfx\music\";
                ext = ".adp";
                if (!MusicNames.Contains(Music_GC_Extra))
                {
                    MusicNames.Add(Music_GC_Extra);
                }
            }
            else
            {
                musicPath = ConsolePipeline.ExtractedPath + @"Crashdat\sfx\Music\";
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

        public List<TWOC_File_CRT.CrateType> CratesToChange = new List<TWOC_File_CRT.CrateType>()
        {
            TWOC_File_CRT.CrateType.Aku,
            TWOC_File_CRT.CrateType.Blank,
            //TWOC_File_CRT.CrateType.Bounce,
            TWOC_File_CRT.CrateType.Fruit,
            TWOC_File_CRT.CrateType.Invisibility,
            TWOC_File_CRT.CrateType.Life,
            TWOC_File_CRT.CrateType.Pickup,
            TWOC_File_CRT.CrateType.Proximity,
            //TWOC_File_CRT.CrateType.TNT,
        };
        public List<TWOC_File_CRT.CrateType> CratesToInsert = new List<TWOC_File_CRT.CrateType>()
        {
            TWOC_File_CRT.CrateType.Aku,
            TWOC_File_CRT.CrateType.Blank,
            TWOC_File_CRT.CrateType.Bounce,
            TWOC_File_CRT.CrateType.Fruit,
            TWOC_File_CRT.CrateType.Invisibility,
            TWOC_File_CRT.CrateType.Life,
            TWOC_File_CRT.CrateType.Pickup,
            TWOC_File_CRT.CrateType.Proximity,
        };

        public void Rand_RandomizeCrates(Random rand)
        {
            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            string Ext = ".CRT";
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
                Ext = Ext.ToLower();
            }
            else
            {
                Ext += ";1";
            }

            for (int i = 0; i < LevelNames.Length; i++)
            {
                string path = ConsolePipeline.ExtractedPath + LevelsPathA + LevelNames[i] + @"\" + FileNames[i] + Ext;
                if (i > 24)
                {
                    path = ConsolePipeline.ExtractedPath + LevelsPathC + LevelNames[i] + @"\" + FileNames[i] + Ext;
                }
                if (File.Exists(path))
                {
                    TWOC_File_CRT CrateFile = new TWOC_File_CRT(path, false);

                    foreach (TWOC_File_CRT.CrateGroup Group in CrateFile.CrateGroups)
                    {
                        foreach (TWOC_File_CRT.Crate Crate in Group.Crates)
                        {
                            if (CratesToChange.Contains(Crate.Type))
                            {
                                int r = rand.Next(CratesToInsert.Count);
                                Crate.Type = CratesToInsert[r];
                            }
                        }
                    }

                    CrateFile.Save(path);
                }
            }

        }

        public List<TWOC_File_CRT.CrateType> CratesToRemove = new List<TWOC_File_CRT.CrateType>()
        {
            TWOC_File_CRT.CrateType.Aku,
            TWOC_File_CRT.CrateType.Blank,
            //TWOC_File_CRT.CrateType.Bounce,
            TWOC_File_CRT.CrateType.Fruit,
            TWOC_File_CRT.CrateType.Invisibility,
            TWOC_File_CRT.CrateType.Life,
            TWOC_File_CRT.CrateType.Pickup,
            TWOC_File_CRT.CrateType.Proximity,
            //TWOC_File_CRT.CrateType.Nitro,
            TWOC_File_CRT.CrateType.Reinforced,
            TWOC_File_CRT.CrateType.Checkpoint,
            TWOC_File_CRT.CrateType.Slot,
        };

        public void Rand_CratesRemoved(Random rand)
        {
            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            string Ext = ".CRT";
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
                Ext = Ext.ToLower();
            }
            else
            {
                Ext += ";1";
            }

            for (int i = 0; i < LevelNames.Length; i++)
            {
                string path = ConsolePipeline.ExtractedPath + LevelsPathA + LevelNames[i] + @"\" + FileNames[i] + Ext;
                if (i > 24)
                {
                    path = ConsolePipeline.ExtractedPath + LevelsPathC + LevelNames[i] + @"\" + FileNames[i] + Ext;
                }
                if (File.Exists(path))
                {
                    TWOC_File_CRT CrateFile = new TWOC_File_CRT(path, false);

                    for (int g = 0; g < CrateFile.CrateGroups.Count; g++)
                    {
                        for (int c = 0; c < CrateFile.CrateGroups[g].Crates.Count; c++)
                        {
                            if (CratesToRemove.Contains(CrateFile.CrateGroups[g].Crates[c].Type) && rand.Next(2) == 0)
                            {
                                CrateFile.CrateGroups[g].Crates.RemoveAt(c);
                                c--;
                            }
                        }
                        if (CrateFile.CrateGroups[g].Crates.Count == 0)
                        {
                            CrateFile.CrateGroups.RemoveAt(g);
                            g--;
                        }
                    }

                    CrateFile.Save(path);
                }
            }

        }

        List<TWOC_File_CRT.CrateType> TimeCrates = new List<TWOC_File_CRT.CrateType>()
        {
            TWOC_File_CRT.CrateType.Time1,
            TWOC_File_CRT.CrateType.Time2,
            TWOC_File_CRT.CrateType.Time3,
        };

        public void Rand_WumpaCrates(Random rand)
        {
            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            string ExtWMP = ".WMP";
            string ExtCRT = ".CRT";
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
                ExtWMP = ExtWMP.ToLower();
                ExtCRT = ExtCRT.ToLower();
            }
            else
            {
                ExtCRT += ";1";
                ExtWMP += ";1";
            }

            for (int i = 0; i < LevelNames.Length - 5; i++)
            {
                string path = ConsolePipeline.ExtractedPath + LevelsPathA + LevelNames[i] + @"\" + FileNames[i] + ExtWMP;
                if (i > 24)
                {
                    path = ConsolePipeline.ExtractedPath + LevelsPathC + LevelNames[i] + @"\" + FileNames[i] + ExtWMP;
                }
                if (File.Exists(path))
                {
                    TWOC_File_WMP WumpaFile = new TWOC_File_WMP(path, false);

                    List<TWOC_Vector3> WumpaPos = new List<TWOC_Vector3>();

                    for (int w = 0; w < WumpaFile.Wumpas.Count; w++)
                    {
                        if (rand.Next(0,5) == 0)
                        {
                            WumpaPos.Add(WumpaFile.Wumpas[w]);
                            WumpaFile.Wumpas.RemoveAt(w);
                            w--;
                        }
                    }

                    WumpaFile.Save(path);

                    path = ConsolePipeline.ExtractedPath + LevelsPathA + LevelNames[i] + @"\" + FileNames[i] + ExtCRT;
                    if (i > 24)
                    {
                        path = ConsolePipeline.ExtractedPath + LevelsPathC + LevelNames[i] + @"\" + FileNames[i] + ExtCRT;
                    }

                    if (File.Exists(path))
                    {
                        TWOC_File_CRT CrateFile = new TWOC_File_CRT(path, false);

                        ushort CrateCount = CrateFile.GetCrateCount();

                        for (int w = 0; w < WumpaPos.Count; w++)
                        {
                            TWOC_File_CRT.CrateGroup Group = new TWOC_File_CRT.CrateGroup();
                            Group.Crates = new List<TWOC_File_CRT.Crate>();
                            TWOC_File_CRT.Crate BaseCrate = new TWOC_File_CRT.Crate();
                            BaseCrate.Pos = new TWOC_Vector3(WumpaPos[w].X, WumpaPos[w].Y, WumpaPos[w].Z);
                            Group.ID = CrateCount;
                            CrateCount++;
                            Group.unkFlags = 0; // ??
                            if (rand.Next(0,2) == 0)
                                Group.Rot = new TWOC_Vector3((float)rand.NextDouble() * 180f, 0, 0);
                            else
                                Group.Rot = new TWOC_Vector3((float)rand.NextDouble() * -180f, 0, 0);
                            
                            BaseCrate.unkFlags = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
                            int r = rand.Next(CratesToInsert.Count);
                            BaseCrate.Type = CratesToInsert[r];
                            if (rand.Next(0,3) == 0)
                            {
                                r = rand.Next(TimeCrates.Count);
                                BaseCrate.TypeTT = TimeCrates[r];
                            }
                            else
                            {
                                BaseCrate.TypeTT = BaseCrate.Type;
                            }
                            BaseCrate.Type3 = TWOC_File_CRT.CrateType.NULL;
                            BaseCrate.Type4 = TWOC_File_CRT.CrateType.NULL;
                            BaseCrate.unkFlags2 = new byte[14] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, };
                            Group.Crates.Add(BaseCrate);
                            CrateFile.CrateGroups.Add(Group);
                        }

                        CrateFile.Save(path);
                    }
                }
            }

        }

        List<string> BannedEnemies = new List<string>()
            {
                "crystal",
                "clock",
                "probe",
                "crate gem",
                "green gem",
                "bonus gem",
                "flying clock",
                "super slam",
                "flying crystal",
                "flying crategem",
                "flying probe",
                "blue gem",
                "double jump",
                "red gem",
                "flying bonusgem",
                "tiptoe",
                "super spin",
                "sprint",
                "space cortex",
                "space crunch",
                "water crunch",
                "yellow gem",
                "purple gem",
                "bazooka",
                "coco",
                "space lo-lo",
                "earth crunch",
                "space rok-ko",
                "space wa-wa",
                "space py-ro",
                "atlas crunch",
                "turtle",

            };


        public void Rand_EnemiesRemoved(Random rand)
        {
            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            string Ext = ".AI";
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
                Ext = Ext.ToLower();
            }
            else
            {
                Ext += ";1";
            }

            for (int i = 0; i < LevelNames.Length; i++)
            {
                string path = ConsolePipeline.ExtractedPath + LevelsPathA + LevelNames[i] + @"\" + FileNames[i] + Ext;
                if (i > 24)
                {
                    path = ConsolePipeline.ExtractedPath + LevelsPathC + LevelNames[i] + @"\" + FileNames[i] + Ext;
                }
                if (File.Exists(path))
                {
                    TWOC_File_AI AIFile = new TWOC_File_AI(path, false);

                    for (int a = 0; a < AIFile.AI.Count; a++)
                    {
                        if (!BannedEnemies.Contains(AIFile.AI[a].Name) && rand.Next(2) == 0)
                        {
                            AIFile.AI.RemoveAt(a);
                            a--;
                        }
                    }

                    AIFile.Save(path);
                }
            }

        }

        public void Rand_EnemyPaths(Random rand)
        {
            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            string Ext = ".AI";
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
                Ext = Ext.ToLower();
            }
            else
            {
                Ext += ";1";
            }

            for (int i = 0; i < LevelNames.Length; i++)
            {
                string path = ConsolePipeline.ExtractedPath + LevelsPathA + LevelNames[i] + @"\" + FileNames[i] + Ext;
                if (i > 24)
                {
                    path = ConsolePipeline.ExtractedPath + LevelsPathC + LevelNames[i] + @"\" + FileNames[i] + Ext;
                }
                if (File.Exists(path))
                {
                    TWOC_File_AI AIFile = new TWOC_File_AI(path, false);

                    for (int a = 0; a < AIFile.AI.Count; a++)
                    {
                        if (!BannedEnemies.Contains(AIFile.AI[a].Name) && rand.Next(2) == 0)
                        {
                            AIFile.AI[a].Pos.Reverse();
                        }
                    }

                    AIFile.Save(path);
                }
            }

        }

    }

    public class TWOC_Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public TWOC_Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
