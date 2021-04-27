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
        public static ModPropOption Option_LEVTest = new ModPropOption(new CTR_Lev_TestMod()) { Hidden = true, };
        public static ModPropOption Option_AllCratesAreWumpaCrates = new ModPropOption(new CTR_AllCratesAreWumpaCrates());
        public static ModPropOption Option_Rand_PickupLocations = new ModPropOption(new CTR_Rand_PickupLocations()) { Hidden = true, }; // not good
        public static ModPropOption Option_Rand_CrateEffects = new ModPropOption(new CTR_Rand_CrateEffects());
        public static ModPropOption Option_Rand_BackwardsTracks = new ModPropOption(new CTR_Rand_BackwardsTracks(true)) { Hidden = true, };  //unfinished
        public static ModPropOption Option_All_BackwardsTracks = new ModPropOption(new CTR_Rand_BackwardsTracks(false), "Reversed Tracks (Beta)", "") { Hidden = true, };  // unfinished
        public static ModPropOption Option_Rand_MoonGravity = new ModPropOption(new CTR_Rand_MoonGravity(true));
        public static ModPropOption Option_All_MoonGravity = new ModPropOption(new CTR_Rand_MoonGravity(false), "All Tracks Have Moon Gravity", "");
        public static ModPropOption Option_Rand_IcyTracks = new ModPropOption(new CTR_Rand_IcyTracks(true));
        public static ModPropOption Option_All_IcyTracks = new ModPropOption(new CTR_Rand_IcyTracks(false), "All Tracks Have Ice Physics", "") { Hidden = true, }; // not needed
        public static ModPropOption Option_OffroadDead = new ModPropOption(new CTR_OffroadDead(false));
        public static ModPropOption Option_DisableWeather = new ModPropOption(new CTR_DisableWeather());
        public static ModPropOption Option_Rand_WorldPalette = new ModPropOption(new CTR_Rand_WorldPalette());
        public static ModPropOption Option_Rand_InvisibleTracks = new ModPropOption(new CTR_Rand_InvisibleTrack(true)) { Hidden = true, }; // not working yet
        public static ModPropOption Option_All_InvisibleTracks = new ModPropOption(new CTR_Rand_InvisibleTrack(false), "All Tracks Are Invisible", "") { Hidden = true, };  // not working yet
    }
}
