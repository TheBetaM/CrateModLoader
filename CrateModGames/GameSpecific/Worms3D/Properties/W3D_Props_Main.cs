using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.WormsForts;

namespace CrateModLoader.GameSpecific.Worms3D
{
    public static class Worms3D_Props_Main
    {
        [ExecutesMods(typeof(WF_TestMod))] [ModHidden]
        public static ModPropOption Option_TestMod = new ModPropOption("Test Mod", "");
        [ExecutesMods(typeof(W3D_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
