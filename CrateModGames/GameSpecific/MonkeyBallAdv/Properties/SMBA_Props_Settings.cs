using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.MonkeyBallAdv.Mods;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    [ExecutesMods(typeof(SMBA_SettingsHandling))] [ModCategory(1)]
    public static class SMBA_Props_Settings
    {

        public static ModPropUInt Setting_Challenge_Time = new ModPropUInt(60, "Challenge Mode: Time Limit", "");
        public static ModPropUInt Setting_Challenge_StartLives = new ModPropUInt(3, "Challenge Mode: Start Lives", "");
        public static ModPropUInt Setting_Challenge_MaxLives = new ModPropUInt(99, "Challenge Mode: Max Lives", "");
        public static ModPropUInt Setting_Challenge_Continues = new ModPropUInt(5, "Challenge Mode: Continues", "");
        public static ModPropUInt Setting_Challenge_ExtraLife = new ModPropUInt(50, "Challenge Mode: Extra Life Every X Bananas", "");
        public static ModPropUInt Setting_Challenge_BananaScore = new ModPropUInt(100, "Challenge Mode: Banana Score", "");

        public static ModPropUInt Setting_Rolling_PowerJumpSize = new ModPropUInt(35, "Story Mode: Power Jump Size", "");
        public static ModPropUInt Setting_Rolling_SquashThres = new ModPropUInt(60, "Story Mode: Squash Threshold", "");
        public static ModPropUInt Setting_Rolling_SquashRecoverDist = new ModPropUInt(25, "Story Mode: Squash Recover Distance", "");
    }
}
