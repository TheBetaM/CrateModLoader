﻿using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.WormsForts;

namespace CrateModLoader.GameSpecific.WormsOW
{
    public static class WOW_Props_Main
    {
        [ExecutesMods(typeof(WF_TestMod))]
        public static ModPropOption Option_TestMod = new ModPropOption("Test Mod", "");
        [ExecutesMods(typeof(WOW_Metadata))]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
