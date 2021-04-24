using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashTeamRacing.Mods;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    static class CTR_Props_Main
    {
        public static ModPropOption Option_RandCharacters = new ModPropOption("Randomize Drivers", "") { Hidden = true, };
        public static ModPropOption Option_RandTracks = new ModPropOption(new CTR_Rand_Tracks()) { Hidden = true, }; // unstable
        public static ModPropOption Option_RandTracks101 = new ModPropOption(new CTR_Rand_Tracks101()) { Hidden = true, }; // unstable
        public static ModPropOption Option_RandTracksWithDupes = new ModPropOption("Randomize Tracks (With Duplicates)", "Shuffles tracks around, which can repeat.") { Hidden = true, };
        public static ModPropOption Option_Metadata = new ModPropOption(new CTR_Metadata(), 1) { Hidden = true, };
        public static ModPropOption Option_RandPantsColor = new ModPropOption(new CTR_Rand_PantsColor()) { Hidden = true, };
    }
}
