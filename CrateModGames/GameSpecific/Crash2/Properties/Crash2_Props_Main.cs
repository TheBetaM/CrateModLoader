using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModGames.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash2.Mods;

namespace CrateModLoader.GameSpecific.Crash2
{
    static class Crash2_Props_Main
    {
        public static ModPropOption Option_RandWarpRoom = new ModPropOption(new Crash2_Rand_WarpRoom()) { Hidden = true };
        public static ModPropOption Option_RandWarpRoomExits = new ModPropOption(new Crash2_Rand_WarpRoomExits());
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(new Crash2_Rand_BackwardsLevels(false));
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(new Crash2_Rand_BackwardsLevels(true), CrashTri_Text.Rand_BackwardsLevels, CrashTri_Text.Rand_BackwardsLevelsDesc);
        public static ModPropOption Option_RandCratesMissing = new ModPropOption(new CrashTri_Rand_CratesIntoWumpa(true), CrashTri_Text.Rand_CratesRemoved, CrashTri_Text.Rand_CratesRemovedDesc);
        public static ModPropOption Option_RandEnemiesMissing = new ModPropOption(new CrashTri_Rand_RemoveEnemies(true));
        public static ModPropOption Option_InvisibleCrates = new ModPropOption(new CrashTri_Rand_InvisibleCrates(false));
        public static ModPropOption Option_RandInvisibleCrates = new ModPropOption(new CrashTri_Rand_InvisibleCrates(true));
        public static ModPropOption Option_AllCratesAshed = new ModPropOption(new CrashTri_Rand_AshedCrates(false));
        public static ModPropOption Option_RandCratesAshed = new ModPropOption(new CrashTri_Rand_AshedCrates(true), "Random Crate Types Are Covered Up", "Random crate types are covered up to look the same on the outside in each level.");
        public static ModPropOption Option_RandCrateContents = new ModPropOption(new CrashTri_Rand_CrateContents());
        public static ModPropOption Option_RandCrateParams = new ModPropOption(new CrashTri_Rand_CrateParams());
        public static ModPropOption Option_RandBoxCount = new ModPropOption(new CrashTri_Rand_BoxCounter());
        public static ModPropOption Option_RandBosses = new ModPropOption(new Crash2_Rand_BossPaths());
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(new CrashTri_Rand_CameraFOV(false));
        public static ModPropOption Option_RandMusicTracks = new ModPropOption(new CrashTri_Rand_Music()); //only swaps midis
        public static ModPropOption Option_RandSounds = new ModPropOption(new CrashTri_Rand_Audio());
        public static ModPropOption Option_RandStreams = new ModPropOption(new CrashTri_Rand_StreamedAudio());
        public static ModPropOption Option_RandPantsColor = new ModPropOption(new CrashTri_Rand_PantsColor(true));
        public static ModPropOption Option_RandWorldColors = new ModPropOption(new CrashTri_Scenery_Rainbow());
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(new CrashTri_Scenery_Swizzle());
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(new CrashTri_Scenery_Greyscale());
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(new CrashTri_Scenery_Untextured());
        public static ModPropOption Option_RandWorldTex = new ModPropOption(new CrashTri_Rand_WorldTex());
        public static ModPropOption Option_RandObjPalette = new ModPropOption(new CrashTri_Rand_ObjectPalette());
        public static ModPropOption Option_Metadata = new ModPropOption(new Crash2_Metadata(), 1) { Hidden = true, };

        // unfinished
        public static ModPropOption Option_RandEnemiesAreCrates = new ModPropOption(new CrashTri_Rand_EnemyCrates(true)) { Hidden = true }; //unfinished
        public static ModPropOption Option_UntexturedObj = new ModPropOption(new CrashTri_Objects_Untextured()) { Hidden = true }; // broken
        public static ModPropOption Option_VehicleLevelsOnFoot = new ModPropOption(new Crash2_VehicleLevelsOnFoot()) { Hidden = true };
        public static ModPropOption Option_MirroredWorld = new ModPropOption(new CrashTri_Rand_MirrorLevel(false)) { Hidden = true };
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption(new CrashTri_Rand_MirrorLevel(true)) { Hidden = true };
    }
}
