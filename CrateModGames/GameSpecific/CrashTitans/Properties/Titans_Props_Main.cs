using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    static class Titans_Props_Main
    {
        public static ModPropOption Option_TestMod = new ModPropOption(new Titans_TestMod()) { Hidden = true, };
        public static ModPropOption Option_Metadata = new ModPropOption(new Titans_Metadata(), 1) { Hidden = true, };
        public static ModPropOption Option_Rand_CostumeVisuals = new ModPropOption(new Titans_Rand_CostumeVisuals());
        public static ModPropOption Option_Rand_CutsceneCostumes = new ModPropOption(new Titans_Rand_CutsceneCostumes());
        public static ModPropOption Option_RandEpisodeOrder = new ModPropOption(new Titans_Rand_EpisodeOrder()) { Hidden = true, };
    }
}
