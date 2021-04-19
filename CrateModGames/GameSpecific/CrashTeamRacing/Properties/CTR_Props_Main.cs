using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    static class CTR_Props_Main
    {
        public static ModPropOption Option_RandCharacters = new ModPropOption("Randomize Drivers", "") { Hidden = true, };
        public static ModPropOption Option_RandTracks = new ModPropOption("Randomize Track Order (Any%)", "Shuffles all tracks around. CTR letters don't spawn in bonus tracks so 101% is not possible.") { Hidden = true, }; // unstable
        public static ModPropOption Option_RandTracks101 = new ModPropOption("Randomize Track Order (101%)", "Shuffles all tracks (except bonus tracks) around.") { Hidden = true, }; // unstable
        public static ModPropOption Option_RandTracksWithDupes = new ModPropOption("Randomize Tracks (With Duplicates)", "Shuffles tracks around, which can repeat.") { Hidden = true, };
    }
}
