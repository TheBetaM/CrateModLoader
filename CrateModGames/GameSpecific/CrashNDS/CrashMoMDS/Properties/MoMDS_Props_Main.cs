using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashMoMDS.Mods;

namespace CrateModLoader.GameSpecific.CrashMoMDS
{
    public static class CrashBB_Props_Main
    {
        [ExecutesMods(typeof(MoMDS_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
