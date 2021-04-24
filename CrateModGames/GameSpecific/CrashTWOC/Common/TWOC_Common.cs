using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
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

    public class TWOC_GenericMod
    {
        public string mainPath;
        public ConsoleMode console;

        public TWOC_GenericMod(string main, ConsoleMode cons)
        {
            mainPath = main;
            console = cons;
        }
    }

    public static class TWOC_Common
    {
        

        public static string[] LevelNames = new string[]
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

        public static string[] FileNames = new string[]
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

        public static List<TWOC_Levels> LevelList_Onfoot = new List<TWOC_Levels>()
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

        public static List<TWOC_Levels> LevelList_Sphere = new List<TWOC_Levels>()
        {
            TWOC_Levels.L03_Bamboozled,
            TWOC_Levels.L14_EskimoRoll,
            TWOC_Levels.L23_MedievalMadness,
            TWOC_Levels.L29_SolarBowler,
        };

        public static List<TWOC_Levels> LevelList_Underwater = new List<TWOC_Levels>()
        {
            TWOC_Levels.L07_SeaShellShenanigans,
            TWOC_Levels.L10_H2OhNo,
            TWOC_Levels.L19_CoralCanyon,
        };

        public static List<TWOC_Levels> LevelList_Flying = new List<TWOC_Levels>()
        {
            TWOC_Levels.L02_TornadoAlley,
            TWOC_Levels.L09_ThatSinkingFeeling,
            TWOC_Levels.L18_Crashteroids,
            TWOC_Levels.L24_CrateBallsOfFire,
            TWOC_Levels.L28_IceStationBandicoot,
        };

        public static List<TWOC_Levels> LevelList_Racing = new List<TWOC_Levels>()
        {
            TWOC_Levels.L13_SmokeyAndTheBandicoot,
            TWOC_Levels.L27_GhostTown,
        };

    }
}
