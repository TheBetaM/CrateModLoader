using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    static class Titans_Props_Main
    {
        public static ModPropOption Option_TestMod = new ModPropOption(new Titans_TestMod());
        public static ModPropOption Option_RandEpisodeOrder = new ModPropOption(new Titans_Rand_EpisodeOrder()) { Hidden = true, };
    }
}
