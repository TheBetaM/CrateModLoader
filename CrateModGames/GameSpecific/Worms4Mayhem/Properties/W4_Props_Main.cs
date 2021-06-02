﻿using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.WormsForts;

namespace CrateModLoader.GameSpecific.Worms4
{
    public static class Worms4_Props_Main
    {
        [ExecutesMods(typeof(WF_TestMod))] [ModHidden]
        public static ModPropOption Option_TestMod = new ModPropOption("Test Mod", "");
        [ExecutesMods(typeof(W4_Rand_LevelVisuals))]
        public static ModPropOption Option_Rand_LevelVisuals = new ModPropOption("Randomize Level Visuals", "Randomize the time of day and theme in each level.");
        [ExecutesMods(typeof(W4_Rand_WorldPalette))] [ModHidden]
        public static ModPropOption Option_Rand_WorldPalette = new ModPropOption("Randomize World Palette", "Randomize the world's palette in every level.");
        [ExecutesMods(typeof(W4_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
