using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    static class Titans_Props_Main
    {
        [ExecutesMods(typeof(Titans_TestMod))] [ModHidden]
        public static ModPropOption Option_TestMod = new ModPropOption("Test Mod: Wide camera angle in Episode 1", "");
        [ExecutesMods(typeof(Titans_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
        [ExecutesMods(typeof(Titans_Rand_CostumeVisuals))]
        public static ModPropOption Option_Rand_CostumeVisuals = new ModPropOption("Randomize Costume Visuals", "Shuffles Crash's playable costume visuals.");
        [ExecutesMods(typeof(Titans_Rand_CutsceneCostumes))]
        public static ModPropOption Option_Rand_CutsceneCostumes = new ModPropOption("Randomize Cutscene Costumes", "Shuffles Crash's costume in every individual major cutscene.");
        [ExecutesMods(typeof(Titans_Rand_EpisodeOrder))] [ModHidden]
        public static ModPropOption Option_RandEpisodeOrder = new ModPropOption("Randomize Episode Order", "");
    }
}
