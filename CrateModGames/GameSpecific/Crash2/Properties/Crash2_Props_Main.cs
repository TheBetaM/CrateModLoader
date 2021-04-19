using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash1;
using CrateModGames.GameSpecific.Crash2;
//using CrateModLoader.GameSpecific.Crash2.Mods;

namespace CrateModLoader.GameSpecific.Crash2
{
    static class Crash2_Props_Main
    {
        public static ModPropOption Option_RandWarpRoom = new ModPropOption("Randomize Level Order", "") { Hidden = true };
        public static ModPropOption Option_RandWarpRoomExits = new ModPropOption(Crash2_Text.Rand_WarpRoom, Crash2_Text.Rand_WarpRoomDesc);
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(Crash2_Text.Mod_BackwardsLevels, Crash2_Text.Mod_BackwardsLevelsDesc);
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(CrashTri_Text.Rand_BackwardsLevels, CrashTri_Text.Rand_BackwardsLevelsDesc);
        public static ModPropOption Option_RandCratesMissing = new ModPropOption(CrashTri_Text.Rand_CratesRemoved, CrashTri_Text.Rand_EnemiesRemovedDesc);
        public static ModPropOption Option_RandEnemiesMissing = new ModPropOption(CrashTri_Text.Rand_EnemiesRemoved, CrashTri_Text.Rand_EnemiesRemovedDesc);
        public static ModPropOption Option_InvisibleCrates = new ModPropOption(CrashTri_Text.Mod_InvisibleCrates, CrashTri_Text.Mod_InvisibleCratesDesc);
        public static ModPropOption Option_RandInvisibleCrates = new ModPropOption(CrashTri_Text.Rand_InvisibleCrates, CrashTri_Text.Rand_InvisibleCratesDesc);
        public static ModPropOption Option_AllCratesAshed = new ModPropOption("All Crates Are Covered Up", "All crates are covered up to look the same on the outside in each level.");
        public static ModPropOption Option_RandCratesAshed = new ModPropOption("Random Crate Types Are Covered Up", "Random crate types are covered up to look the same on the outside in each level.");
        public static ModPropOption Option_RandCrateContents = new ModPropOption(CrashTri_Text.Rand_CrateContents, CrashTri_Text.Rand_CrateContentsDesc);
        public static ModPropOption Option_RandCrateParams = new ModPropOption(CrashTri_Text.Rand_CrateParams, CrashTri_Text.Rand_CrateParamsDesc);
        public static ModPropOption Option_RandBoxCount = new ModPropOption(CrashTri_Text.Rand_CrateCounter, CrashTri_Text.Rand_CrateCounterDesc);
        public static ModPropOption Option_RandBosses = new ModPropOption(Crash2_Text.Rand_BossLevels, Crash2_Text.Rand_BossLevelsDesc);
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(CrashTri_Text.Mod_CameraWideFOV, CrashTri_Text.Mod_CameraWideFOVDesc);
        public static ModPropOption Option_RandMusicTracks = new ModPropOption("Randomize Music Tracks", "Music tracks are randomized, still played using the level's instruments."); //only swaps midis
        public static ModPropOption Option_RandSounds = new ModPropOption(CrashTri_Text.Rand_SFX, CrashTri_Text.Rand_SFXDesc);
        public static ModPropOption Option_RandStreams = new ModPropOption(CrashTri_Text.Rand_Streams, CrashTri_Text.Rand_StreamsDesc);
        public static ModPropOption Option_RandPantsColor = new ModPropOption(CrashTri_Text.Rand_PantsColor, CrashTri_Text.Rand_PantsColorDesc);
        public static ModPropOption Option_RandWorldColors = new ModPropOption(CrashTri_Text.Rand_WorldColors, CrashTri_Text.Rand_WorldColorsDesc);
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(CrashTri_Text.Rand_WorldPalette, CrashTri_Text.Rand_WorldPaletteDesc);
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(CrashTri_Text.Mod_GreyscaleWorld, CrashTri_Text.Mod_GreyscaleWorldDesc);
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(CrashTri_Text.Mod_UntexturedWorld, CrashTri_Text.Mod_UntexturedWorldDesc);
        public static ModPropOption Option_RandWorldTex = new ModPropOption(CrashTri_Text.Rand_WorldTex, CrashTri_Text.Rand_WorldTexDesc);
        public static ModPropOption Option_RandObjPalette = new ModPropOption(CrashTri_Text.Rand_ObjectPalette, CrashTri_Text.Rand_ObjectPaletteDesc);

        // unfinished
        public static ModPropOption Option_RandEnemiesAreCrates = new ModPropOption(CrashTri_Text.Rand_EnemyCrates, CrashTri_Text.Rand_EnemyCratesDesc) { Hidden = true }; //unfinished
        public static ModPropOption Option_UntexturedObj = new ModPropOption("Untextured Objects", "") { Hidden = true }; // broken
        public static ModPropOption Option_VehicleLevelsOnFoot = new ModPropOption("Vehicle Levels On Foot", "") { Hidden = true };
        public static ModPropOption Option_MirroredWorld = new ModPropOption("Mirrored World", "") { Hidden = true };
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption("Random Levels Are Mirrored", "") { Hidden = true };
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "") { Hidden = true }; //shuffle tracks from different levels (must be identical to vanilla playback, just in a different level)
        public static ModPropOption Option_RandMusicInstruments = new ModPropOption("Randomize Music Instruments", "") { Hidden = true }; //only swap wavebanks
    }
}
