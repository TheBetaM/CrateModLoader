using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.MonkeyBallAdv.Mods;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public static class SMBA_Props_Main
    {
        public static ModPropOption Option_TestMod = new ModPropOption(new SMBA_TestMod()) { Hidden = true, };
        public static ModPropOption Option_Rand_ChallengeMode = new ModPropOption(new SMBA_Rand_ChallengeMode());
        public static ModPropOption Option_Rand_BackwardsPuzzleLevels = new ModPropOption(new SMBA_Rand_BackwardsPuzzleLevels(true));
        public static ModPropOption Option_All_BackwardsPuzzleLevels = new ModPropOption(new SMBA_Rand_BackwardsPuzzleLevels(false), "Backwards Puzzle Levels", "Puzzle Realm/Challenge Mode levels have their start and end swapped. (where possible)");
        public static ModPropOption Option_EnableJump = new ModPropOption(new SMBA_EnableJump());
        public static ModPropOption Option_EnablePowerJump = new ModPropOption(new SMBA_EnablePowerJump());
        public static ModPropOption Option_Metadata = new ModPropOption(new SMBA_Metadata(), 1) { Hidden = true, };
    }
}
