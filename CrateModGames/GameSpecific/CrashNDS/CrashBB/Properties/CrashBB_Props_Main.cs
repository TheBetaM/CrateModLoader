using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashBB.Mods;

namespace CrateModLoader.GameSpecific.CrashBB
{
    public static class CrashBB_Props_Main
    {
        [ExecutesMods(typeof(CrashBB_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
