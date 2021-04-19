using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    static class Titans_Props_Main
    {
        public static ModPropOption Option_TestMod = new ModPropOption("Test Mod: Wide camera angle in Episode 1", "");
        public static ModPropOption Option_RandEpisodeOrder = new ModPropOption("Randomize Episode Order", "") { Hidden = true, };
    }
}
