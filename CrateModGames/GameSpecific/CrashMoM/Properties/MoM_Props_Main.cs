using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashMoM
{
    static class MoM_Props_Main
    {
        public static ModPropOption Option_Metadata = new ModPropOption(new MoM_Metadata(), 1) { Hidden = true, };
    }
}
