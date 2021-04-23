using System;
using System.Collections.Generic;
using Crash;

namespace CrateModLoader.GameSpecific.Crash1
{
    public enum Crash1_Levels
    {
        Unknown = -1,
        L01_NSanityBeach = 4,
        L02_JungleRollers = 5,
        L03_GreatGate = 9,
        L04_Boulders = 6,
        L05_Upstream = 7,
        L06_RollingStones = 12,
        L07_HogWild = 8,
        L08_NativeFortress = 15,
        L09_UpTheCreek = 14,
        L10_LostCity = 19,
        L11_TempleRuins = 16,
        L12_RoadToNowhere = 11,
        L13_BoulderDash = 10,
        L14_WholeHog = 18,
        L15_SunsetVista = 21,
        L16_HeavyMachinery = 2,
        L17_CortexPower = 0,
        L18_GeneratorRoom = 1,
        L19_ToxicWaste = 3,
        L20_HighRoad = 13,
        L21_SlipperyClimb = 26,
        L22_LightsOut = 22,
        L23_FumblingInTheDark = 24,
        L24_JawsOfDarkness = 17,
        L25_CastleMachinery = 27,
        L26_TheLab = 23,
        L27_GreatHall = 25,
        L28_StormyAscent = 20,
        B01_PapuPapu = 28,
        B02_RipperRoo = 29,
        B03_KoalaKong = 30,
        B04_Pinstripe = 31,
        B05_NBrio = 32,
        B06_Cortex = 33,
        MapMainMenu = 34,
        L29_Cavern = 35,
        Bonus_TawnaShort = 36,
        Bonus_Brio = 37,
        Bonus_TawnaLong = 38,
        Bonus_Cortex = 39,
    }

    public enum CrateSubTypes
    {
        TNT = 0,
        Blank = 2,
        WoodSpring = 3,
        Checkpoint = 4,
        Iron = 5,
        Fruit = 6, //Multiple bounce
        IronSwitch = 7,
        Life = 8,
        Aku = 9,
        Pickup = 10,
        Pow = 11, // same as TNT outside cavern
        Outline = 13,
        IronSpring = 15,
        AutoPickup = 17,
        Nitro = 18,
        AutoBlank = 20,
        Blank2 = 21,
        Steel = 23,
        Slot = 25,
    }

    public enum CrateContentTypes : short
    {
        Wumpa_1 = 0,
        Wumpa_10 = 77,
        Wumpa_9 = 79,
        Wumpa_8 = 81,
        Wumpa_7 = 83,
        Wumpa_6 = 85,
        Wumpa_5 = 87,
        Wumpa_4 = 89,
        Wumpa_3 = 91,
        Wumpa_2 = 93,
        Wumpa_1_Anim = 96,
        Life = 97,
        Rand1 = 100,
        Rand2 = 101,
        Mask = 102,
        Token_Cortex = 103,
        Token_Brio = 104,
        Token_Tawna = 105,
    }

    public enum BonusLevels
    {
        Unk1 = 0,
        Unk2 = 1,
        Unk3 = 2,
        Unk4 = 3,
        Unk5 = 4,
        Unk6 = 5,
        Unk7 = 6,
        Unk8 = 7,
        Unk9 = 8,
        Unk10 = 9,
        Unk11 = 10,
        Unk12 = 11,
        Unk13 = 12,
        Unk14 = 13,
        Unk15 = 14,
        Unk16 = 15,
        Unk17 = 16,
        Unk18 = 17,
        Unk19 = 18,
        Unk20 = 19,
        Unk21 = 20,
        Unk22 = 21,
    }


    public static class Crash1_Common
    {
        public static List<Crash1_Levels> ChaseLevelsList = new List<Crash1_Levels>()
        {
            Crash1_Levels.L04_Boulders,
            Crash1_Levels.L13_BoulderDash,
        };
        public static List<Crash1_Levels> VehicleLevelsList = new List<Crash1_Levels>()
        {
            Crash1_Levels.L07_HogWild,
            Crash1_Levels.L14_WholeHog,
        };
        public static List<Crash1_Levels> BossLevelsList = new List<Crash1_Levels>()
        {
            //Crash1_Levels.B01_PapuPapu,
            Crash1_Levels.B02_RipperRoo,
            //Crash1_Levels.B03_KoalaKong,
            Crash1_Levels.B04_Pinstripe,
            //Crash1_Levels.B05_NBrio,
            Crash1_Levels.B06_Cortex,
        };

        public static List<Crash1_Levels> BonusLevelsList = new List<Crash1_Levels>()
        {
            Crash1_Levels.Bonus_TawnaShort,
            Crash1_Levels.Bonus_TawnaLong,
            Crash1_Levels.Bonus_Cortex,
            Crash1_Levels.Bonus_Brio,
        };

        public static string[] Crash1_LevelFileNames = new string[]
        {
            "03",
            "05",
            "06",
            "07",
            "09",
            "0C",
            "0E",
            "0F",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "18",
            "1A",
            "1C",
            "1D",
            "1E",
            "20",
            "22",
            "23",
            "28",
            "29",
            "2A",
            "2C",
            "2E",
            "37",
            //Bosses
            "0A",
            "17",
            "21",
            "08",
            "1B",
            "1F",
            //Other
            "19",
            "04",
            //Bonus
            "24",
            "25",
            "33",
            "34",
        };

        public static void CreateEntity(short id, int type, int subtype, short x, short y, short z, OldZoneEntry zone)
        {
            // Loading an entity we just made, for the error checking.
            OldEntity newentity = OldEntity.Load(new OldEntity(0x18, 3, 0, id, 0, 0, 0, (byte)type, (byte)subtype, new List<EntityPosition>() { new EntityPosition(x, y, z) }, 0).Save());

            zone.Entities.Add(newentity);
        }

    }

}
