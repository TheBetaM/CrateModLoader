using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashMoM.Mods;

namespace CrateModLoader.GameSpecific.CrashMoM
{
    static class MoM_Props_Main
    {
        public static ModPropOption Option_TestMod = new ModPropOption(new MoM_TestMod()) { Hidden = true, };
        public static ModPropOption Option_Rand_CostumeVisuals = new ModPropOption(new MoM_Rand_CostumeVisuals());
        public static ModPropOption Option_Metadata = new ModPropOption(new MoM_Metadata(), 1) { Hidden = true, };
    }
}
