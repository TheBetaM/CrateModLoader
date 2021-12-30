using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashTitansDS.Mods;

namespace CrateModLoader.GameSpecific.CrashTitansDS
{
    public static class CrashBB_Props_Main
    {
        [ExecutesMods(typeof(TitansDS_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
