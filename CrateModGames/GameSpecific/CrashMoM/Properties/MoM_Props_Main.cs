using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashMoM.Mods;

namespace CrateModLoader.GameSpecific.CrashMoM
{
    static class MoM_Props_Main
    {
        [ExecutesMods(typeof(MoM_TestMod))] [ModHidden]
        public static ModPropOption Option_TestMod = new ModPropOption("Test Mod", "");
        [ExecutesMods(typeof(MoM_Rand_CostumeVisuals))]
        public static ModPropOption Option_Rand_CostumeVisuals = new ModPropOption("Randomize Costume Visuals", "Shuffles Crash's playable costume visuals.");
        [ExecutesMods(typeof(MoM_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
