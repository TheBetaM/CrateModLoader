using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.MonkeyBallAdv.Mods;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public static class SMBA_Props_Main
    {
        [ExecutesMods(typeof(SMBA_TestMod))] [ModHidden]
        public static ModPropOption Option_TestMod = new ModPropOption();
        [ExecutesMods(typeof(SMBA_Rand_ChallengeMode))]
        public static ModPropOption Option_Rand_ChallengeMode = new ModPropOption("Randomize Challenge Mode", "Randomize which levels to play in all Challenge modes and randomize challenge settings.");
        [ExecutesMods(typeof(SMBA_Rand_BackwardsPuzzleLevels))] [ModAllowedConsoles(ConsoleMode.PSP)]
        public static ModPropOption Option_Rand_BackwardsPuzzleLevels = new ModPropOption("Random Puzzle Levels Are Backwards", "Random Challenge Mode/Puzzle Realm levels have their start and end swapped. (where possible)");
        [ExecutesMods(typeof(SMBA_Rand_BackwardsPuzzleLevels))] [ModAllowedConsoles(ConsoleMode.PSP)]
        public static ModPropOption Option_All_BackwardsPuzzleLevels = new ModPropOption("Backwards Puzzle Levels", "Puzzle Realm/Challenge Mode levels have their start and end swapped. (where possible)");
        [ExecutesMods(typeof(SMBA_EnableJump))]
        public static ModPropOption Option_EnableJump = new ModPropOption("Story Mode: Enable Jump By Default", "Enables Jump from the start by pressing the Square button.");
        [ExecutesMods(typeof(SMBA_EnablePowerJump))]
        public static ModPropOption Option_EnablePowerJump = new ModPropOption("Story Mode: Enable Power Jump By Default", "Enables Power Jump from the start by holding the Square button.");
        [ExecutesMods(typeof(SMBA_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
