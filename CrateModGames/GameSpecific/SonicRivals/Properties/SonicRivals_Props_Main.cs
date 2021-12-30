using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.SonicRivals
{
    public static class SonicRivals_Props_Main
    {
        [ExecutesMods(typeof(SonicRivals_Rand_LevelOrder))]
        public static ModPropOption Option_Rand_LevelOrder= new ModPropOption("Ranodmize Level Order", "Shuffles the level order around.");
        [ExecutesMods(typeof(SonicRivals_Rand_Characters))] [ModHidden]
        public static ModPropOption Option_Rand_Characters = new ModPropOption("Ranodmize Characters", "Shuffles characters and skins around.");
        [ExecutesMods(typeof(SonicRivals_Rand_Music))]
        public static ModPropOption Option_Rand_Music = new ModPropOption("Ranodmize Act Music", "Shuffles all act music tracks.");
        [ExecutesMods(typeof(SonicRivals_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
