using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashTeamRacing.Mods;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    static class CTR_Props_Main
    {
        [ModHidden]
        public static ModPropOption Option_RandCharacters = new ModPropOption("Randomize Drivers", "");
        [ExecutesMods(typeof(CTR_Rand_Tracks))] [ModHidden]
        public static ModPropOption Option_RandTracks = new ModPropOption("Randomize Track Order (Any%)", "Shuffles all tracks around. CTR letters don't spawn in bonus tracks so 101% is not possible."); // unstable
        [ExecutesMods(typeof(CTR_Rand_Tracks101))] [ModHidden]
        public static ModPropOption Option_RandTracks101 = new ModPropOption("Randomize Track Order (101%)", "Shuffles all tracks (except bonus tracks) around."); // unstable
        [ModHidden]
        public static ModPropOption Option_RandTracksWithDupes = new ModPropOption("Randomize Tracks (With Duplicates)", "Shuffles tracks around, which can repeat.");
        [ExecutesMods(typeof(CTR_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
        [ExecutesMods(typeof(CTR_Rand_PantsColor))] [ModHidden]
        public static ModPropOption Option_RandPantsColor = new ModPropOption("Randomize Pants Color", "");
        [ExecutesMods(typeof(CTR_Lev_TestMod))] [ModHidden]
        public static ModPropOption Option_LEVTest = new ModPropOption("LEV Test", "");
        [ExecutesMods(typeof(CTR_AllCratesAreWumpaCrates))]
        public static ModPropOption Option_AllCratesAreWumpaCrates = new ModPropOption("All Wooden Crates Are Wumpa Crates", "");
        [ExecutesMods(typeof(CTR_Rand_PickupLocations))] [ModHidden]
        public static ModPropOption Option_Rand_PickupLocations = new ModPropOption("Randomize Pickup Locations", ""); // not good
        [ExecutesMods(typeof(CTR_Rand_CrateEffects))]
        public static ModPropOption Option_Rand_CrateEffects = new ModPropOption("Randomize Crate Effects", "");
        [ExecutesMods(typeof(CTR_Rand_BackwardsTracks))] [ModHidden]
        public static ModPropOption Option_Rand_BackwardsTracks = new ModPropOption("Random Tracks Are Reversed", "") ;  //unfinished
        [ExecutesMods(typeof(CTR_Rand_BackwardsTracks))] [ModHidden]
        public static ModPropOption Option_All_BackwardsTracks = new ModPropOption("Reversed Tracks (Beta)", "");  // unfinished
        [ExecutesMods(typeof(CTR_Rand_MoonGravity))]
        public static ModPropOption Option_Rand_MoonGravity = new ModPropOption("Random Tracks Have Moon Gravity", "");
        [ExecutesMods(typeof(CTR_Rand_MoonGravity))]
        public static ModPropOption Option_All_MoonGravity = new ModPropOption("All Tracks Have Moon Gravity", "");
        [ExecutesMods(typeof(CTR_Rand_IcyTracks))]
        public static ModPropOption Option_Rand_IcyTracks = new ModPropOption("Random Tracks Have Ice Physics", "");
        [ExecutesMods(typeof(CTR_Rand_IcyTracks))] [ModHidden]
        public static ModPropOption Option_All_IcyTracks = new ModPropOption("All Tracks Have Ice Physics", ""); // not needed
        [ExecutesMods(typeof(CTR_OffroadDead))]
        public static ModPropOption Option_OffroadDead = new ModPropOption("Driving Offroad Respawns The Driver", "");
        [ExecutesMods(typeof(CTR_DisableWeather))] [ModHidden]
        public static ModPropOption Option_DisableWeather = new ModPropOption("Disable Weather Effects", "");
        [ExecutesMods(typeof(CTR_Rand_SurfaceParams))] [ModHidden]
        public static ModPropOption Option_Rand_SurfaceParams = new ModPropOption("Randomize Surface Parameters", ""); //works but sucks
        [ExecutesMods(typeof(CTR_Rand_WorldPalette))]
        public static ModPropOption Option_Rand_WorldPalette = new ModPropOption("Randomize World Palette", "");
        [ExecutesMods(typeof(CTR_Rand_InvisibleTrack))] [ModHidden]
        public static ModPropOption Option_Rand_InvisibleTracks = new ModPropOption("Random Tracks Are Invisible", ""); // not working yet
        [ExecutesMods(typeof(CTR_Rand_InvisibleTrack))] [ModHidden]
        public static ModPropOption Option_All_InvisibleTracks = new ModPropOption("All Tracks Are Invisible", "");  // not working yet
    }
}
