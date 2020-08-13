using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{

    public enum PowerupTypes
    {
        EXPLOSIVE_CRATE = 0,
        FREEZING_MINE = 1,
        HOMING_MISSLE = 2,
        BOWLING_BOMB = 3,
        TORNADO = 4,
        STATIC_SHOCK = 5,
        POWER_SHIELD = 6,
        INVINCIBILITY_MASK = 7,
        INVISIBILITY = 8,
        VOODOO_DOLL = 9, //Dummied out in retail :(
        TROPY_CLOCKS = 10,
        TURBO_BOOSTS = 11,
        SUPER_ENGINE = 12,
        REDEYE = 13,
        HOMING_MISSLE_X3 = 14,
        BOWLING_BOMB_X3 = 15,
        TURBO_BOOST_X3 = 16,
        EXPCRATE_X3 = 17,
        FREEZEMINE_X3 = 18,
        STATICSHOCK_X3 = 19,
    }

    [ModCategory((int)ModProps.Powerups)]
    static class CNK_Data_Powerups
    {

        
        public enum WeaponSelectionRows
        {
            Track_Earth_1 = 11,
            Track_Earth_2 = 12,
            Track_Earth_3 = 13,
            Track_Barin_1 = 14,
            Track_Barin_2 = 15,
            Track_Barin_3 = 16,
            Track_Fenom_1 = 17,
            Track_Fenom_2 = 18,
            Track_Fenom_3 = 19,
            Track_Teknee_1 = 20,
            Track_Teknee_2 = 21,
            Track_Teknee_3 = 22,
            Track_VeloRace = 23,
            Track_Arena_1 = 24,
            Track_Arena_2 = 25,
            Track_Arena_3 = 26,
            Track_Arena_4 = 27,
            Track_Arena_5 = 28,
            Track_Arena_6 = 29,
            Track_Arena_7 = 30,
            Track_Lobby = 31,
            Mode_Adv_Trophy = 36,
            Mode_Adv_CNK = 37,
            Mode_Adv_Gem = 38,
            Mode_Adv_Boss = 39,
            Mode_Adv_Crystal = 40,
            Mode_Arcade = 41,
            Mode_Versus = 42,
            Mode_CrystalRace = 43,
            Mode_Battle_Point = 44,
            Mode_Battle_Time = 45,
            Mode_Battle_Domination = 46,
            Mode_Battle_CTF = 47,
            Mode_Battle_KOTR = 48,
            Mode_Battle_Crystal = 49,
            Mode_Lobby = 50,
            Rank_1st = 56,
            Rank_2nd = 57,
            Rank_3rd = 58,
            Rank_4th = 59,
            Rank_5th = 60,
            Rank_6th = 61,
            Rank_7th = 62,
            Rank_8th = 63,
            Progress_0 = 71,
            Progress_5 = 72,
            Progress_10 = 73,
            Progress_15 = 74,
            Progress_20 = 75,
            Progress_25 = 76,
            Progress_30 = 77,
            Progress_35 = 78,
            Progress_40 = 79,
            Progress_45 = 80,
            Progress_50 = 81,
            Progress_55 = 82,
            Progress_60 = 83,
            Progress_65 = 84,
            Progress_70 = 85,
            Progress_75 = 86,
            Progress_80 = 87,
            Progress_85 = 88,
            Progress_90 = 89,
            Progress_95 = 90,
            ActiveWep_EXPLOSIVE_CRATE = 101,
            ActiveWep_FREEZING_MINE = 102,
            ActiveWep_HOMING_MISSLE = 103,
            ActiveWep_BOWLING_BOMB = 104,
            ActiveWep_TORNADO = 105,
            ActiveWep_STATIC_SHOCK = 106,
            ActiveWep_POWER_SHIELD = 107,
            ActiveWep_INVINCIBILITY_MASKS = 108,
            ActiveWep_INVISIBILITY = 109,
            ActiveWep_VOODOO_DOLL = 110,
            ActiveWep_TROPY_CLOCK = 111,
            ActiveWep_TURBO_BOOSTS = 112,
            ActiveWep_SUPER_ENGINE = 113,
            ActiveWep_REDEYE = 114,
            ActiveWep_HOMING_MISSLE_X3 = 115,
            ActiveWep_BOWLING_BOMB_X3 = 116,
            ActiveWep_TURBO_BOOST_X3 = 117,
            ActiveWep_EXPCRATE_X3 = 118,
            ActiveWep_FREEZEMINE_X3 = 119,
            ActiveWep_STATICSHOCK_X3 = 120,
            ActivePower_POWER_SHIELD = 131,
            ActivePower_TURBO_BOOSTS = 132,
            ActivePower_INVINCIBILITY_MASKS = 133,
            ActivePower_TROPY_CLOCKS = 134,
            ActivePower_INVISIBILITY = 135,
            ActivePower_SUPER_ENGINE = 136,
            ActivePower_EXPLOSIVE_CRATE = 137,
            ActivePower_RESETTING = 138,
            ActivePower_CURSED = 139,
            ActivePower_GRACED = 140,
            ActivePower_ICED = 141,
            ActivePower_STATICSHOCKED = 142,
            ActivePower_SPIKYFRUIT = 143,
            ActivePower_MIMECUBE = 144,
            ActivePower_TIMEBUBBLE = 145,
            ActivePower_CLEANINGFLUID = 146,
            ActivePower_ROLLINGBRUSH = 147,
            ActivePower_WINDUPJAW = 148,
            ActivePower_TEETHSTRIP = 149,
            ActivePower_INVULNERABLE = 150,
            ActivePower_TEAMINVULNERABLE = 151,
            ActivePower_POWERSHIELD_ZAPPED = 152,
            KartsInFront_DensityRadius = 158,
            KartsInFront_0 = 161,
            KartsInFront_1 = 162,
            KartsInFront_2 = 163,
            KartsInFront_3 = 164,
            KartsInFront_4 = 165,
            KartsInFront_5 = 166,
            KartsInFront_6 = 167,
            KartsInFront_7 = 168,
            KartsInFront_8 = 169,
            KartsBehind_DensityRadius = 178,
            KartsBehind_0 = 181,
            KartsBehind_1 = 182,
            KartsBehind_2 = 183,
            KartsBehind_3 = 184,
            KartsBehind_4 = 185,
            KartsBehind_5 = 186,
            KartsBehind_6 = 187,
            KartsBehind_7 = 188,
            KartsBehind_8 = 189,
            Difficulty_Easiest = 201,
            Difficulty_Hardest = 202,
            Buddy_Range = 210,
            Buddy_Ahead = 211,
            Buddy_Behind = 212,
            Buddy_InRange = 213,
        }

        //These are Normal values, not battle/boss/AI
        public static float[] WeaponSelection_Track_Earth_1 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Earth_2 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Earth_3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Barin_1 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Barin_2 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Barin_3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Fenom_1 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Fenom_2 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Fenom_3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Teknee_1 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Teknee_2 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Teknee_3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_VeloRace = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Arena_1 = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static float[] WeaponSelection_Track_Arena_2 = new float[] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 };
        public static float[] WeaponSelection_Track_Arena_3 = new float[] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 };
        public static float[] WeaponSelection_Track_Arena_4 = new float[] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 };
        public static float[] WeaponSelection_Track_Arena_5 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Arena_6 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Arena_7 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Track_Lobby = new float[] { 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Mode_Adv_Trophy = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Mode_Adv_CNK = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Mode_Adv_Gem = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Mode_Adv_Boss = new float[] { 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1 }; // only used in _Boss.csv
        public static float[] WeaponSelection_Mode_Adv_Crystal = new float[] { 0, 0, 0, 1, 0, 0, 0, 0.1f, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0 };
        public static float[] WeaponSelection_Mode_Arcade = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Mode_Versus = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Mode_CrystalRace = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Mode_Battle_Point = new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 }; // only used in _Battle.csv
        public static float[] WeaponSelection_Mode_Battle_Time = new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 }; // only used in _Battle.csv
        public static float[] WeaponSelection_Mode_Battle_Domination = new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 }; // only used in _Battle.csv
        public static float[] WeaponSelection_Mode_Battle_CTF = new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 }; // only used in _Battle.csv
        public static float[] WeaponSelection_Mode_Battle_KOTR = new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 }; // only used in _Battle.csv
        public static float[] WeaponSelection_Mode_Battle_Crystal = new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 }; // only used in _Battle.csv
        public static float[] WeaponSelection_Mode_Lobby = new float[] { 1, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Rank_1st = new float[] { 1.5f, 1.5f, 0.01f, 0.6f, 0.01f, 1.4f, 1, 0.01f, 0, 0, 0.01f, 0.1f, 0, 0, 0.01f, 0.6f, 0.1f, 1.5f, 1.5f, 1.4f };
        public static float[] WeaponSelection_Rank_2nd = new float[] { 1.3f, 1.3f, 1.5f, 1.5f, 0.01f, 1.2f, 1, 0.4f, 0, 0, 0.01f, 0.1f, 0, 0, 1.5f, 1.5f, 0.1f, 1.3f, 1.3f, 1.2f };
        public static float[] WeaponSelection_Rank_3rd = new float[] { 1, 1, 1.5f, 1.5f, 0.01f, 1, 1, 0.6f, 0, 0, 0.01f, 1, 0, 0, 1.5f, 1.5f, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Rank_4th = new float[] { 0.8f, 0.8f, 1.5f, 1.5f, 0.01f, 0.8f, 0.8f, 0.8f, 0, 0, 0.01f, 1, 0, 0, 1.5f, 1.5f, 1, 0.8f, 0.8f, 0.8f };
        public static float[] WeaponSelection_Rank_5th = new float[] { 0.6f, 0.6f, 1.5f, 1, 0.5f, 0.6f, 0.8f, 1.3f, 0, 0, 0.5f, 0.8f, 0, 0, 1.5f, 1, 0.8f, 0.6f, 0.6f, 0.6f };
        public static float[] WeaponSelection_Rank_6th = new float[] { 0.4f, 0.4f, 1.3f, 0.8f, 0.8f, 0.4f, 0.6f, 1.3f, 0, 0, 0.8f, 0.8f, 0, 0, 1.3f, 0.8f, 0.8f, 0.4f, 0.4f, 0.4f };
        public static float[] WeaponSelection_Rank_7th = new float[] { 0.01f, 0.01f, 1.1f, 0.6f, 1.1f, 0.01f, 0.01f, 1, 0, 0, 1.1f, 0.6f, 0, 0, 1.1f, 0.6f, 0.6f, 0.01f, 0.01f, 0.01f };
        public static float[] WeaponSelection_Rank_8th = new float[] { 0.01f, 0.01f, 1.1f, 0.6f, 1.3f, 0.01f, 0.01f, 1, 0, 0, 1.3f, 0.6f, 0, 0, 1.1f, 0.6f, 0.6f, 0.01f, 0.01f, 0.01f };
        public static float[] WeaponSelection_Progress_0 = new float[] { 0.7f, 0.7f, 1, 1, 0.1f, 1, 1, 0.4f, 0, 0, 0.1f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_5 = new float[] { 0.7f, 0.7f, 1, 1, 0.2f, 1, 1, 0.4f, 0, 0, 0.2f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_10 = new float[] { 0.7f, 0.7f, 1, 1, 0.3f, 1, 1, 0.5f, 0, 0, 0.3f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_15 = new float[] { 0.7f, 0.7f, 1, 1, 0.4f, 1, 1, 0.5f, 0, 0, 0.4f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_20 = new float[] { 0.7f, 0.7f, 1, 1, 0.5f, 1, 1, 0.6f, 0, 0, 0.5f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_25 = new float[] { 0.7f, 0.7f, 1, 1, 0.6f, 1, 1, 0.7f, 0, 0, 0.6f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_30 = new float[] { 0.8f, 0.8f, 1, 1, 0.7f, 1, 1, 0.8f, 0, 0, 0.7f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_35 = new float[] { 0.8f, 0.8f, 1, 1, 0.8f, 1, 1, 0.9f, 0, 0, 0.8f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_40 = new float[] { 0.8f, 0.8f, 1, 1, 0.9f, 1, 1, 1, 0, 0, 0.9f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_45 = new float[] { 0.8f, 0.8f, 1, 1, 0.9f, 1, 1, 1, 0, 0, 0.9f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_50 = new float[] { 0.8f, 0.8f, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_55 = new float[] { 0.9f, 0.9f, 1, 1, 1, 1, 1, 0.9f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_60 = new float[] { 0.9f, 0.9f, 1, 1, 0.9f, 1, 1, 0.9f, 0, 0, 0.9f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_65 = new float[] { 0.9f, 0.9f, 1, 1, 0.9f, 1, 1, 0.9f, 0, 0, 0.9f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_70 = new float[] { 0.9f, 0.9f, 1, 1, 0.8f, 1, 1, 0.9f, 0, 0, 0.8f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_75 = new float[] { 0.9f, 0.9f, 1, 1, 0.8f, 1, 1, 0.9f, 0, 0, 0.8f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_80 = new float[] { 1, 1, 1, 1, 0.8f, 1, 1, 0.8f, 0, 0, 0.7f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_85 = new float[] { 1, 1, 1, 1, 0.8f, 1, 1, 0.8f, 0, 0, 0.7f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_90 = new float[] { 1, 1, 1, 1, 0.8f, 1, 1, 0.8f, 0, 0, 0.7f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Progress_95 = new float[] { 1, 1, 1, 1, 0.8f, 1, 1, 0.8f, 0, 0, 0.7f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_EXPLOSIVE_CRATE = new float[] { 0.5f, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_FREEZING_MINE = new float[] { 1, 0.5f, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_HOMING_MISSLE = new float[] { 1, 1, 0.75f, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_BOWLING_BOMB = new float[] { 1, 1, 1, 0.75f, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_TORNADO = new float[] { 1, 1, 1, 1, 0.01f, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_STATIC_SHOCK = new float[] { 1, 1, 1, 1, 1, 0.5f, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_POWER_SHIELD = new float[] { 1, 1, 1, 1, 1, 1, 0.1f, 0.01f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_INVINCIBILITY_MASKS = new float[] { 1, 1, 1, 1, 1, 1, 0.01f, 0.01f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_INVISIBILITY = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_VOODOO_DOLL = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_TROPY_CLOCK = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0.01f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_TURBO_BOOSTS = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0.5f, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_SUPER_ENGINE = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_REDEYE = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_HOMING_MISSLE_X3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0.75f, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_BOWLING_BOMB_X3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0.75f, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_TURBO_BOOST_X3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_EXPCRATE_X3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0.1f, 1, 1 };
        public static float[] WeaponSelection_ActiveWep_FREEZEMINE_X3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0.1f, 1 };
        public static float[] WeaponSelection_ActiveWep_STATICSHOCK_X3 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0.1f };
        public static float[] WeaponSelection_ActivePower_POWER_SHIELD = new float[] { 1, 1, 1, 1, 1, 1, 0.1f, 0.01f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_TURBO_BOOSTS = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0.5f, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_INVINCIBILITY_MASKS = new float[] { 1, 1, 1, 1, 1, 1, 0.01f, 0.01f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_TROPY_CLOCKS = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0.01f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_INVISIBILITY = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_SUPER_ENGINE = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_EXPLOSIVE_CRATE = new float[] { 0.5f, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0.5f, 1, 1 };
        public static float[] WeaponSelection_ActivePower_RESETTING = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_CURSED = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_GRACED = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static float[] WeaponSelection_ActivePower_ICED = new float[] { 1, 0.5f, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0.5f, 1 };
        public static float[] WeaponSelection_ActivePower_STATICSHOCKED = new float[] { 1, 1, 1, 1, 1, 0.5f, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0.5f };
        public static float[] WeaponSelection_ActivePower_SPIKYFRUIT = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_MIMECUBE = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_TIMEBUBBLE = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_CLEANINGFLUID = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_ROLLINGBRUSH = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_WINDUPJAW = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_TEETHSTRIP = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_INVULNERABLE = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_TEAMINVULNERABLE = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_ActivePower_POWERSHIELD_ZAPPED = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_0 = new float[] { 1, 1, 1, 1, 2, 1, 1, 2, 0, 0, 2, 2, 0, 0, 1, 1, 2, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_1 = new float[] { 1, 1, 2, 2, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 2, 2, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_2 = new float[] { 1, 1, 1.5f, 1.5f, 1, 1, 1, 1.5f, 0, 0, 1, 1, 0, 0, 1.5f, 1.5f, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_3 = new float[] { 1, 1, 1, 1, 1.5f, 1, 1, 1.5f, 0, 0, 1.5f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_4 = new float[] { 1, 1, 1, 1, 1.5f, 1, 1, 1.5f, 0, 0, 1.5f, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_5 = new float[] { 1, 1, 1, 1, 2, 1, 1, 2, 0, 0, 2, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_6 = new float[] { 1, 1, 1, 1, 2, 1, 1, 2, 0, 0, 2, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_7 = new float[] { 1, 1, 1, 1, 2, 1, 1, 2, 0, 0, 2, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsInFront_8 = new float[] { 1, 1, 1, 1, 2, 1, 1, 2, 0, 0, 2, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsBehind_0 = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsBehind_1 = new float[] { 1.4f, 1.4f, 1, 1, 1, 1.4f, 1.4f, 1, 0, 0, 1, 1.2f, 0, 0, 1, 1, 1.2f, 1.4f, 1.4f, 1.4f };
        public static float[] WeaponSelection_KartsBehind_2 = new float[] { 1.4f, 1.4f, 1, 1, 1, 1.4f, 1.4f, 1.2f, 0, 0, 1, 1.2f, 0, 0, 1, 1, 1.2f, 1.4f, 1.4f, 1.4f };
        public static float[] WeaponSelection_KartsBehind_3 = new float[] { 1.2f, 1.2f, 1, 1, 1, 1.2f, 1.2f, 1.2f, 0, 0, 1, 1.1f, 0, 0, 1, 1, 1.1f, 1.2f, 1.2f, 1.2f };
        public static float[] WeaponSelection_KartsBehind_4 = new float[] { 1.2f, 1.2f, 1, 1, 1, 1.2f, 1.2f, 1.4f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1.2f, 1.2f, 1.2f };
        public static float[] WeaponSelection_KartsBehind_5 = new float[] { 1, 1, 1, 1, 1, 1, 1.2f, 1.4f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsBehind_6 = new float[] { 1, 1, 1, 1, 1, 1, 1.2f, 1.4f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsBehind_7 = new float[] { 1, 1, 1, 1, 1, 1, 1.2f, 1.4f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_KartsBehind_8 = new float[] { 1, 1, 1, 1, 1, 1, 1.2f, 1.4f, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Difficulty_Easiest = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Difficulty_Hardest = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Buddy_Ahead = new float[] { 1, 1, 1, 0.5f, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0.5f, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Buddy_Behind = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };
        public static float[] WeaponSelection_Buddy_InRange = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 };


        public static float PowerShield_m_Time = 8000f;
        public static float PowerShield_m_RangeForZapping = 7.5f; //15, 10
        public static float PowerShield_m_ZapSpeed = 9f;
        public static float[] PowerShield_m_ColorNonJuiced = new float[] { 0.309f, 0.616f, 0.318f };
        public static float[] PowerShield_m_ColorJuiced = new float[] { 0.322f, 0.31f, 0.616f };
        public static float PowerShield_m_ColRadius = 1.75f;


        /// <summary> How long effect lasts when not juiced </summary>
        public static int StaticShock_m_NormalTime = 3000;
        /// <summary> How long effect lasts when it is juiced | 3000 </summary>
        public static int StaticShock_m_JuicedTime = 6000;
        /// <summary> How much wumpa you loose when hit </summary>
        public static int StaticShock_m_NormalWumpaLoss = 2;
        /// <summary> How much wumpa you loose when hit </summary>
        public static int StaticShock_m_JuicedWumpaLoss = 2;
        /// <summary> How fast does it move when its homing in on someone | 12 </summary>
        public static float StaticShock_m_HomingSpeed = 12f;
        /// <summary> How close to kart, before start homing </summary>
        public static float StaticShock_m_DistanceForHome = 12f;

        /// <summary> How long effect lasts </summary>
        public static float TurboBoost_m_NormalTime = 8000f;
        /// <summary> How long effect lasts when juiced </summary>
        public static float TurboBoost_m_JuicedTime = 12000f;


        /// <summary> How long effect lasts </summary>
        public static int TNT_m_Time = 4300;
        /// <summary> How long before the character hides </summary>
        public static int TNT_m_TimeBeforeHiddenChar = 2500;
        /// <summary> Time when character comes out from hiding </summary>
        public static int TNT_m_TimeHiddenChar = 4300;
        /// <summary> How much wumpa victim looses </summary>
        public static int TNT_m_NormalWumpaLoss = 3;
        /// <summary> How much wumpa victim looses </summary>
        public static int TNT_m_JuicedWumpaLoss = 3;
        /// <summary> Blast Radius of Explosion </summary>
        public static float TNT_m_ExplosionBlastRadius = 5f;
        /// <summary> Scale of explosion normal </summary>
        public static float TNT_m_ExplScale = 0.714f;
        /// <summary> Scale of explosion juiced(nitro crate) </summary>
        public static float TNT_m_ExplScaleJuiced = 0.714f;

        /// <summary> How long effect lasts with a normal mine | 1500 </summary>
        public static int FreezingMine_m_NormalFreezeTime = 3000;
        /// <summary> How long effect lasts with a juiced mine </summary>
        public static int FreezingMine_m_JuicedFreezeTime = 12000;
        /// <summary> How much wumpa fruit victim looses </summary>
        public static int FreezingMine_m_NormalWumpaFruitLost = 1;
        /// <summary> How much wumpa fruit victim looses(juiced) </summary>
        public static int FreezingMine_m_JuicedWumpaFruitLost = 1;
        /// <summary> How far to throw freeze mine </summary>
        public static float FreezingMine_m_ThrowDistance = 24;
        /// <summary> This number is multiplied by currentspeed and added to the throw distance </summary>
        public static float FreezingMine_m_ThrowSpeedFactor = 1f;
        /// <summary> 2 </summary>
        public static float FreezingMine_m_BlastRadius = 3f;
        /// <summary> 3 </summary>
        public static float FreezingMine_m_BlastRadiusJuiced = 5f;
        /// <summary> 1=7m </summary>
        public static float FreezingMine_m_ExplScale = 0.429f;
        /// <summary> 1=7m </summary>
        public static float FreezingMine_m_ExplScaleJuiced = 0.714f;

        /// <summary> 45 </summary>
        public static float RedEye_Acceleration = 50f;
        public static float RedEye_Deceleration = 15f;
        public static float RedEye_MaxSpeed = 60f;
        public static float RedEye_MinSpeed = 28f;
        /// <summary> 20, 13 </summary>
        public static float RedEye_TurnSpeed = 10f;
        public static float RedEye_Explosion_Radius = 3f;
        /// <summary> 30, 13.5 </summary>
        public static float RedEye_TurnSpeedJuiced = 12f;
        public static float RedEye_Explosion_Radius_Juiced = 7f;
        public static float RedEye_Expl_Scale = 0.429f;
        public static float RedEye_Expl_Scale_Juiced = 1f;
        /// <summary> (at min speed drop turn angle by 0, at full speed drop turn angle by this number - and it interpolates in between) </summary>
        public static float RedEye_FullSpeedTurnSlowdown = 4f;

        /// <summary> How long effect lasts </summary>
        public static int InvincMask_m_NormalTime = 8000;
        /// <summary> How long effect lasts when juiced </summary>
        public static int InvincMask_m_JuicedTime = 12000;
        /// <summary> When Teamed </summary>
        public static int InvincMask_m_NormalTimeTeamed = 12000;
        /// <summary> When Teamed and Juiced </summary>
        public static int InvincMask_m_JuicedTimeTeamed = 16000;
        /// <summary> How much wumpa fruit lost by victim </summary>
        public static int InvincMask_m_NormalWumpaLoss = 3;
        /// <summary> How much wumpa fruit lost by victim </summary>
        public static int InvincMask_m_JuicedWumpaLoss = 3;
        /// <summary> How fast mask travels to buddy from buddy </summary>
        public static float InvincMask_m_TeamSpeed = 15f;
        /// <summary> How big explosion is for team effect </summary>
        public static float InvincMask_m_TeamBlastRange = 40f;
        /// <summary> How full does the buddy meter need to be before explosion </summary>
        public static float InvincMask_m_TeamMeterFull = 5f;
        /// <summary> Scale of explosion normal </summary>
        public static float InvincMask_m_ExplScale = 1f;
        /// <summary> Scale of explosion juiced </summary>
        public static float InvincMask_m_ExplScaleJuiced = 1.5f;
        /// <summary> Collision Radius </summary>
        public static float InvincMask_m_ColRadius = 2f;

        /// <summary> Speed of the bowling bomb </summary>
        public static float BowlingBomb_m_Speed = 65f;
        /// <summary> Acceleration </summary>
        public static float BowlingBomb_m_Acceleration = 80f;
        /// <summary> Acceleration </summary>
        public static float BowlingBomb_m_AccelerationJuiced = 90f;
        /// <summary> Mass of bomb </summary>
        public static float BowlingBomb_m_Mass = 2500f;
        /// <summary> Radius of bomb </summary>
        public static float BowlingBomb_m_Radius = 1f;
        /// <summary> Gravity in the air normally | 8 </summary>
        public static float BowlingBomb_m_AirGravity = 12f;
        /// <summary> Gravity on the ground normally | 1.25 </summary>
        public static float BowlingBomb_m_GroundGravity = 4f;
        /// <summary> Gravity in the air on maglev surfaces </summary>
        public static float BowlingBomb_m_AirGravityMaglev = 17f;
        /// <summary> Gravity on the ground on maglev surfaces </summary>
        public static float BowlingBomb_m_GroundGravityMaglev = 15f;
        /// <summary> How fast it turns | 0.25 </summary>
        public static float BowlingBomb_m_TurnSpeed = 0.9f;
        /// <summary> How fast does it turn(juiced) | 0.25 </summary>
        public static float BowlingBomb_m_TurnSpeedJuiced = 0.9f;
        /// <summary> Targeting range target must be in | (7.5 degrees) </summary>
        public static float BowlingBomb_m_ViewRange = 0.993f;
        /// <summary> Targeting range target must be in | 130, 100 </summary>
        public static float BowlingBomb_m_RangeInFront = 150f;
        /// <summary> How much wumpa fruit victim looses </summary>
        public static int BowlingBomb_m_NormalWumpaLoss = 3;
        /// <summary> How much wumpa fruit victim looses </summary>
        public static int BowlingBomb_m_JuicedWumpaLoss = 3;
        /// <summary> Blast Radius of Explosion | 3 </summary>
        public static float BowlingBomb_m_ExplosionBlastRadius = 5f;
        /// <summary> Blast Radius of Explosion Juiced | 6 </summary>
        public static float BowlingBomb_m_ExplosionBlastRadiusJuiced = 8f;
        /// <summary> How much drag from the "wind"</summary>
        public static float BowlingBomb_m_DragCoef = 0.00125f;
        /// <summary> Friction that helps when turning | 30 </summary>
        public static float BowlingBomb_m_EasyLatFriction = 30f;
        /// <summary> Friction that slows the missile down </summary>
        public static float BowlingBomb_m_EasyLongFriction = 1f;
        /// <summary> Friction that helps when turning | 50 </summary>
        public static float BowlingBomb_m_HardLatFriction = 30f;
        /// <summary> Friction that slows the missile down </summary>
        public static float BowlingBomb_m_HardLongFriction = 1f;
        /// <summary> Speed of the bowling bomb when shot backwards </summary>
        public static float BowlingBomb_m_BackSpeed = 40f;
        /// <summary> Scale of explosion normal | (1=7m) </summary>
        public static float BowlingBomb_m_ExplScale = 0.714f;
        /// <summary> Scale of explosion juiced | (1=7m) </summary>
        public static float BowlingBomb_m_ExplScaleJuiced = 1.143f;

        /// <summary> How man times farther will the missile target someone who is in front of them, rather than in back </summary>
        public static float HomingMissle_m_TrackingFrontDistance = 50f;
        /// <summary> Max speed of homing missile | 60 </summary>
        public static float HomingMissle_m_MaxSpeed = 60f;
        /// <summary> Max speed of homing missile(juiced) | 70 </summary>
        public static float HomingMissle_m_MaxSpeedJuiced = 70f;
        /// <summary> How long will missile last in ms </summary>
        public static int HomingMissle_m_TimeLimit = 15000;
        /// <summary> Gravity in the air </summary>
        public static float HomingMissle_m_AirGravityNormal = 8f;
        /// <summary> Gravity on the ground, </summary>
        public static float HomingMissle_m_GroundGravityNormal = 1.25f;
        /// <summary> Gravity in the air, when on maglev </summary>
        public static float HomingMissle_m_AirGravityMaglev = 8f;
        /// <summary> Gravity on the ground, when on maglev, should be high(there are sharp changes in curvature of ground in maglev) </summary>
        public static float HomingMissle_m_GroundGravityMaglev = 8f;
        /// <summary> How fast it accelerates, note the reason this is so high is because of the long. friction | 40 </summary>
        public static float HomingMissle_m_Acceleration = 45f;
        /// <summary> How fast it accelerates juiced, note the reason this is so high is because of the long. friction | 50 </summary>
        public static float HomingMissle_m_AccelerationJuiced = 55f;
        /// <summary> How fast homing missile turns (radians / sec) | 4 </summary>
        public static float HomingMissle_m_TurnSpeed = 5f;
        /// <summary> How fast homing missile turns (radians / sec)(juiced) | 8 </summary>
        public static float HomingMissle_m_TurnSpeedJuiced = 8f;
        /// <summary> Mass of missile </summary>
        public static float HomingMissle_m_Mass = 1000f;
        /// <summary> Radius of missile(for collision) </summary>
        public static float HomingMissle_m_Radius = 1f;
        /// <summary> Delay between updates of tracking </summary>
        public static int HomingMissle_m_DelayTrackingUpdate = 100;
        /// <summary> Range of view the homing missile targets, if 0, then targets everything infront of him, 1 is exactly in front of him </summary>
        public static float HomingMissle_m_ViewRange = 0.2f;
        /// <summary> How far will the missile track in front of user kart </summary>
        public static float HomingMissle_m_RangeInFront = 140;
        /// <summary> How far to the back will the missile track of the user kart </summary>
        public static float HomingMissle_m_RangeInBack = 0f;
        /// <summary> How much wumpa fruit victim losses </summary>
        public static int HomingMissle_m_NormalWumpaLoss = 3;
        /// <summary> How much wumpa fruit victim losses </summary>
        public static int HomingMissle_m_JuicedWumpaLoss = 3;
        /// <summary> Blast Radius of Explosion </summary>
        public static float HomingMissle_m_ExplosionBlastRadius = 1f;
        /// <summary> Blast Radius of Explosion Juiced </summary>
        public static float HomingMissle_m_ExplosionBlastRadiusJuiced = 1f;
        /// <summary> How much drag force applied to missile </summary>
        public static float HomingMissle_m_DragCoef = 0.00125f;
        /// <summary> Lateral friction, helps when turning(higher the better you turn) </summary>
        public static float HomingMissle_m_EasyLatFriction = 15f;
        /// <summary> Long. Friction, friction working against missile, this force the acceleration to be much higher than normal </summary>
        public static float HomingMissle_m_EasyLongFriction = 1f;
        /// <summary> Same as above, except when making a tight turn </summary>
        public static float HomingMissle_m_HardLatFriction = 55f;
        /// <summary> Same as above, except when making a tight turn </summary>
        public static float HomingMissle_m_HardLongFriction = 1f;
        /// <summary> How long before decay starts</summary>
        public static int HomingMissle_m_DecayTime = 5000;
        /// <summary> How fast velocity decays(in meteres/sec) </summary>
        public static float HomingMissle_m_DecaySpeed = 2f;
        /// <summary> Min value for velocity | 40 </summary>
        public static float HomingMissle_m_DecayMin = 40f;
        /// <summary> Scale of explosion normal | 0.45 </summary>
        public static float HomingMissle_m_ExplScale = 0.45f;
        /// <summary> Scale of explosion juiced | 0.45 </summary>
        public static float HomingMissle_m_ExplScaleJuiced = 0.45f;

        /// <summary> NO USE | How man times farther will the missile target someone who is in front of them, rather than in back </summary>
        public static float Tornado_m_TrackingFrontDistance = 35f;
        /// <summary> Max speed of homing missile | 35 </summary>
        public static float Tornado_m_MaxSpeed = 55f;
        /// <summary> Max speed of homing missile(juiced) | 48 </summary>
        public static float Tornado_m_MaxSpeedJuiced = 55f;
        /// <summary> Max speed, when tornado has picked up a kart</summary>
        public static float Tornado_m_MaxSpeedWithKart = 10f;
        /// <summary> How long will missile last in ms | 20000 </summary>
        public static int Tornado_m_TimeLimit = 30000;
        /// <summary> Gravity in the air </summary>
        public static float Tornado_m_AirGravity = 6f;
        /// <summary> Gravity on the ground </summary>
        public static float Tornado_m_GroundGravity = 1.5f;
        /// <summary> Gravity in the air on maglev surface </summary>
        public static float Tornado_m_AirGravityMaglev = 6f;
        /// <summary> Gravity on the ground on maglev surface </summary>
        public static float Tornado_m_GroundGravityMaglev = 8f;
        /// <summary> How fast it accelerates(20/s/s) | 50 </summary>
        public static float Tornado_m_Acceleration = 50f;
        /// <summary> How fast it accelerates(20/s/s) | 50 </summary>
        public static float Tornado_m_AccelerationJuiced = 50f;
        /// <summary> How fast homing missile turns (radians / sec) | 6 </summary>
        public static float Tornado_m_TurnSpeed = 8f;
        /// <summary> How fast homing missile turns (radians / sec)(juiced) | 11 </summary>
        public static float Tornado_m_TurnSpeedJuiced = 8f;
        /// <summary> Mass of missile </summary>
        public static float Tornado_m_Mass = 50f;
        /// <summary> Radius of missile(for collision) | 0.75 </summary>
        public static float Tornado_m_Radius = 2.5f;
        /// <summary> NO USE | Delay between updates of tracking </summary>
        public static int Tornado_m_DelayTrackingUpdate = 100;
        /// <summary> DO NOT USE | Range of view the homing missile targets, if 0, then targets everything infront of him, 1 is exactly in front of him </summary>
        public static float Tornado_m_ViewRange = 0f;
        /// <summary> DO NOT USE | How far will the missile track in front of user kart </summary>
        public static float Tornado_m_RangeInFront = 0f;
        /// <summary> DO NOT USE | How far to the back will the missile track of the user kart </summary>
        public static float Tornado_m_RangeInBack = 0f;
        /// <summary> How long to lift a caught player | 1500</summary>
        public static int Tornado_m_LiftTime = 3000;
        /// <summary> How much force upon lifting the player </summary>
        public static float Tornado_m_LiftForce = 30f;
        /// <summary> How long before it fizzles once its messed with its final target </summary>
        public static int Tornado_m_FizzleTime = 1000;
        /// <summary> How much wumpa fruit victim looses </summary>
        public static int Tornado_m_NormalWumpaLoss = 5;
        /// <summary> How much wumpa fruit victim looses </summary>
        public static int Tornado_m_JuicedWumpaLoss = 5;
        /// <summary> How much drag (from "wind") </summary>
        public static float Tornado_m_DragCoef = 0.01f;
        /// <summary> Friction that helps with turning </summary>
        public static float Tornado_m_EasyLatFriction = 30f;
        /// <summary> Friction that slows the missile down </summary>
        public static float Tornado_m_EasyLongFriction = 1f;
        /// <summary> Friction that helps with turning </summary>
        public static float Tornado_m_HardLatFriction = 50f;
        /// <summary> Friction that slows the missile down </summary>
        public static float Tornado_m_HardLongFriction = 1f;
        /// <summary> If juiced, what is the distance a kart needs to be from tornado for it to start targeting it on its way to victim | 15 </summary>
        public static float Tornado_m_TargetAllDistance = 18f;
        /// <summary> If juiced, what range does target kart have to be in for tornado to target it on the way to victim | 0.15 </summary>
        public static float Tornado_m_ViewRangleOfTarget = 0.707f;


        public static void CNK_Randomize_StaticShock(Random randState)
        {
            StaticShock_m_NormalTime = randState.Next(15, 45) * 100;
            StaticShock_m_JuicedTime = StaticShock_m_NormalTime * 2;
            StaticShock_m_NormalWumpaLoss = randState.Next(1, 5);
            StaticShock_m_JuicedWumpaLoss = StaticShock_m_NormalWumpaLoss + randState.Next(0, 2);
            StaticShock_m_HomingSpeed = randState.Next(9, 15);
            StaticShock_m_DistanceForHome = randState.Next(9, 15);
        }

        public static void CNK_Randomize_PowerShield(Random randState)
        {
            PowerShield_m_Time = randState.Next(4, 16) * 1000f;
            PowerShield_m_ZapSpeed = randState.Next(6, 12);
            //Sadly, it doesn't seem like this actually changes the shield's color
            PowerShield_m_ColorNonJuiced[0] = (float)randState.NextDouble();
            PowerShield_m_ColorNonJuiced[1] = (float)randState.NextDouble();
            PowerShield_m_ColorNonJuiced[2] = (float)randState.NextDouble();
            PowerShield_m_ColorJuiced[0] = (float)randState.NextDouble();
            PowerShield_m_ColorJuiced[1] = (float)randState.NextDouble();
            PowerShield_m_ColorJuiced[2] = (float)randState.NextDouble();
            PowerShield_m_RangeForZapping = randState.Next(50, 100) / 10f;
            PowerShield_m_ColRadius = PowerShield_m_RangeForZapping / 4.3f;
        }

        public static void CNK_Randomize_TurboBoost(Random randState)
        {
            TurboBoost_m_NormalTime = randState.Next(60, 120) * 100f;
            TurboBoost_m_JuicedTime = TurboBoost_m_NormalTime * 1.5f;
        }

        public static void CNK_Randomize_TNTCrate(Random randState)
        {
            TNT_m_Time = 4300;
            TNT_m_TimeBeforeHiddenChar = 2500;
            TNT_m_TimeHiddenChar = 4300;
            TNT_m_NormalWumpaLoss = randState.Next(1, 5);
            TNT_m_JuicedWumpaLoss = TNT_m_JuicedWumpaLoss + randState.Next(0, 2);
            TNT_m_ExplosionBlastRadius = randState.Next(300, 800) / 100f;
            TNT_m_ExplScale = (float)randState.NextDouble() + 0.25f;
            TNT_m_ExplScaleJuiced = TNT_m_ExplScale + 0.1f;
        }

        public static void CNK_Randomize_FreezingMine(Random randState)
        {
            FreezingMine_m_NormalFreezeTime = randState.Next(15, 45) * 100;
            FreezingMine_m_JuicedFreezeTime = randState.Next(9, 15) * 1000;
            FreezingMine_m_NormalWumpaFruitLost = randState.Next(1, 5);
            FreezingMine_m_JuicedWumpaFruitLost = FreezingMine_m_NormalWumpaFruitLost + randState.Next(0, 2);
            FreezingMine_m_ThrowDistance = randState.Next(16, 32);
            FreezingMine_m_ThrowSpeedFactor = (float)randState.NextDouble() + 0.75f;
            FreezingMine_m_BlastRadius = (float)randState.NextDouble() + randState.Next(1, 5);
            FreezingMine_m_BlastRadiusJuiced = FreezingMine_m_BlastRadius * 2f;
            FreezingMine_m_ExplScale = (float)randState.NextDouble() + 0.1f;
            FreezingMine_m_ExplScaleJuiced = FreezingMine_m_ExplScale * 2f;
        }

        public static void CNK_Randomize_RedEye(Random randState)
        {
            RedEye_Acceleration = randState.Next(25, 75) + (float)randState.NextDouble();
            RedEye_Deceleration = randState.Next(10, 20) + (float)randState.NextDouble();
            RedEye_MaxSpeed = randState.Next(45, 75);
            RedEye_MinSpeed = randState.Next(24, 32);
            RedEye_TurnSpeed = randState.Next(8, 12) + (float)randState.NextDouble();
            RedEye_Explosion_Radius = randState.Next(2, 5) + (float)randState.NextDouble();
            RedEye_TurnSpeedJuiced = RedEye_TurnSpeed + 2f;
            RedEye_Explosion_Radius_Juiced = RedEye_Explosion_Radius + 4f;
            RedEye_Expl_Scale = (float)randState.NextDouble() + 0.1f;
            RedEye_Expl_Scale_Juiced = RedEye_Expl_Scale * 2f;
            RedEye_FullSpeedTurnSlowdown = 4f;
        }

        public static void CNK_Randomize_InvincMask(Random randState)
        {
            InvincMask_m_NormalTime = randState.Next(60, 100) * 100;
            InvincMask_m_JuicedTime = InvincMask_m_NormalTime + ((int)Math.Ceiling(InvincMask_m_NormalTime / 2f));
            InvincMask_m_NormalTimeTeamed = randState.Next(80, 160) * 100;
            InvincMask_m_JuicedTimeTeamed = InvincMask_m_NormalTimeTeamed + ((int)Math.Ceiling(InvincMask_m_NormalTimeTeamed / 3f));
            InvincMask_m_NormalWumpaLoss = randState.Next(1, 5);
            InvincMask_m_JuicedWumpaLoss = InvincMask_m_NormalWumpaLoss + randState.Next(0, 2);
            InvincMask_m_TeamSpeed = 15f;
            InvincMask_m_TeamBlastRange = 40f;
            InvincMask_m_TeamMeterFull = 5f;
            InvincMask_m_ExplScale = (float)randState.NextDouble() + 0.5f;
            InvincMask_m_ExplScaleJuiced = 1.5f * InvincMask_m_ExplScale;
            InvincMask_m_ColRadius = randState.Next(1, 3) + (float)randState.NextDouble();
        }

        public static void CNK_Randomize_BowlingBomb(Random randState)
        {
            BowlingBomb_m_Speed = randState.Next(50, 80);
            BowlingBomb_m_Acceleration = randState.Next(70, 90);
            BowlingBomb_m_AccelerationJuiced = BowlingBomb_m_Acceleration * 1.125f;
            BowlingBomb_m_Mass = 2500f;
            BowlingBomb_m_Radius = 1f;
            BowlingBomb_m_AirGravity = 12f;
            BowlingBomb_m_GroundGravity = 4f;
            BowlingBomb_m_AirGravityMaglev = 17f;
            BowlingBomb_m_GroundGravityMaglev = 15f;
            BowlingBomb_m_TurnSpeed = 0.9f;
            BowlingBomb_m_TurnSpeedJuiced = BowlingBomb_m_TurnSpeed;
            BowlingBomb_m_ViewRange = 0.993f;
            BowlingBomb_m_RangeInFront = 150f;
            BowlingBomb_m_NormalWumpaLoss = randState.Next(1, 5);
            BowlingBomb_m_JuicedWumpaLoss = BowlingBomb_m_NormalWumpaLoss + randState.Next(0, 2);
            BowlingBomb_m_ExplosionBlastRadius = randState.Next(25, 75) / 10f;
            BowlingBomb_m_ExplosionBlastRadiusJuiced = BowlingBomb_m_ExplosionBlastRadius * 1.6f;
            BowlingBomb_m_DragCoef = 0.00125f;
            BowlingBomb_m_EasyLatFriction = 30f;
            BowlingBomb_m_EasyLongFriction = 1f;
            BowlingBomb_m_HardLatFriction = 30f;
            BowlingBomb_m_HardLongFriction = 1f;
            BowlingBomb_m_BackSpeed = randState.Next(30, 60);
            BowlingBomb_m_ExplScale = (float)randState.NextDouble() + 0.5f;
            BowlingBomb_m_ExplScaleJuiced = BowlingBomb_m_ExplScale * 1.5f;
        }

        public static void CNK_Randomize_HomingMissle(Random randState)
        {
            HomingMissle_m_TrackingFrontDistance = 50f;
            HomingMissle_m_MaxSpeed = randState.Next(40, 80);
            HomingMissle_m_MaxSpeedJuiced = HomingMissle_m_MaxSpeed * (70f / 60f);
            HomingMissle_m_TimeLimit = 15000;
            HomingMissle_m_AirGravityNormal = 8f;
            HomingMissle_m_GroundGravityNormal = 1.25f;
            HomingMissle_m_AirGravityMaglev = 8f;
            HomingMissle_m_GroundGravityMaglev = HomingMissle_m_AirGravityMaglev;
            HomingMissle_m_Acceleration = randState.Next(35, 55);
            HomingMissle_m_AccelerationJuiced = HomingMissle_m_Acceleration * (55f / 45f);
            HomingMissle_m_TurnSpeed = 5f;
            HomingMissle_m_TurnSpeedJuiced = HomingMissle_m_TurnSpeed * (8f / 5f);
            HomingMissle_m_Mass = 1000f;
            HomingMissle_m_Radius = 1f;
            HomingMissle_m_DelayTrackingUpdate = 100;
            HomingMissle_m_ViewRange = 0.2f;
            HomingMissle_m_RangeInFront = 140;
            HomingMissle_m_RangeInBack = 0f;
            HomingMissle_m_NormalWumpaLoss = randState.Next(1, 5);
            HomingMissle_m_JuicedWumpaLoss = HomingMissle_m_NormalWumpaLoss + randState.Next(0, 2);
            HomingMissle_m_ExplosionBlastRadius = 1f;
            HomingMissle_m_ExplosionBlastRadiusJuiced = HomingMissle_m_ExplosionBlastRadius;
            HomingMissle_m_DragCoef = 0.00125f;
            HomingMissle_m_EasyLatFriction = 15f;
            HomingMissle_m_EasyLongFriction = 1f;
            HomingMissle_m_HardLatFriction = 55f;
            HomingMissle_m_HardLongFriction = 1f;
            HomingMissle_m_DecayTime = 5000;
            HomingMissle_m_DecaySpeed = 2f;
            HomingMissle_m_DecayMin = 40f;
            HomingMissle_m_ExplScale = 0.45f;
            HomingMissle_m_ExplScaleJuiced = HomingMissle_m_ExplScale;
        }

        public static void CNK_Randomize_Tornado(Random randState)
        {
            Tornado_m_TrackingFrontDistance = 35f;
            Tornado_m_MaxSpeed = randState.Next(45, 60);
            Tornado_m_MaxSpeedJuiced = Tornado_m_MaxSpeed;
            Tornado_m_MaxSpeedWithKart = 10f;
            Tornado_m_TimeLimit = 30000;
            Tornado_m_AirGravity = 6f;
            Tornado_m_GroundGravity = 1.5f;
            Tornado_m_AirGravityMaglev = Tornado_m_AirGravity;
            Tornado_m_GroundGravityMaglev = 8f;
            Tornado_m_Acceleration = randState.Next(40, 60);
            Tornado_m_AccelerationJuiced = Tornado_m_Acceleration;
            Tornado_m_TurnSpeed = randState.Next(6, 10);
            Tornado_m_TurnSpeedJuiced = Tornado_m_TurnSpeed;
            Tornado_m_Mass = 50f;
            Tornado_m_Radius = 2.5f;
            Tornado_m_DelayTrackingUpdate = 100;
            Tornado_m_ViewRange = 0f;
            Tornado_m_RangeInFront = 0f;
            Tornado_m_RangeInBack = 0f;
            Tornado_m_LiftTime = 3000;
            Tornado_m_LiftForce = 30f;
            Tornado_m_FizzleTime = 1000;
            Tornado_m_NormalWumpaLoss = randState.Next(3, 7);
            Tornado_m_JuicedWumpaLoss = Tornado_m_NormalWumpaLoss + randState.Next(0, 2);
            Tornado_m_DragCoef = 0.01f;
            Tornado_m_EasyLatFriction = 30f;
            Tornado_m_EasyLongFriction = 1f;
            Tornado_m_HardLatFriction = 50f;
            Tornado_m_HardLongFriction = 1f;
            Tornado_m_TargetAllDistance = 18f;
            Tornado_m_ViewRangleOfTarget = 0.707f;
        }

        public static void CNK_Randomize_WeaponSelection(Random randState)
        {
            //todo
            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;
        }
    }
}
