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
        [ExecutesMods(typeof(Crash2_Rand_WarpRoom))] [ModHidden]
        public static ModPropOption Option_RandWarpRoom = new ModPropOption("Randomize Level Order", "");
        [ExecutesMods(typeof(Crash2_Rand_WarpRoomExits))]
        public static ModPropOption Option_RandWarpRoomExits = new ModPropOption(Crash2_Text.Rand_WarpRoom, Crash2_Text.Rand_WarpRoomDesc);
        [ExecutesMods(typeof(Crash2_Rand_BackwardsLevels))]
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(Crash2_Text.Mod_BackwardsLevels, Crash2_Text.Mod_BackwardsLevelsDesc);
        [ExecutesMods(typeof(Crash2_Rand_BackwardsLevels))]
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(CrashTri_Text.Rand_BackwardsLevels, CrashTri_Text.Rand_BackwardsLevelsDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CratesIntoWumpa))]
        public static ModPropOption Option_RandCratesMissing = new ModPropOption(CrashTri_Text.Rand_CratesRemoved, CrashTri_Text.Rand_CratesRemovedDesc);
        [ExecutesMods(typeof(CrashTri_Rand_RemoveEnemies))]
        public static ModPropOption Option_RandEnemiesMissing = new ModPropOption(CrashTri_Text.Rand_EnemiesRemoved, CrashTri_Text.Rand_EnemiesRemovedDesc);
        [ExecutesMods(typeof(CrashTri_Rand_InvisibleCrates))]
        public static ModPropOption Option_InvisibleCrates = new ModPropOption(CrashTri_Text.Mod_InvisibleCrates, CrashTri_Text.Mod_InvisibleCratesDesc);
        [ExecutesMods(typeof(CrashTri_Rand_InvisibleCrates))]
        public static ModPropOption Option_RandInvisibleCrates = new ModPropOption(CrashTri_Text.Rand_InvisibleCrates, CrashTri_Text.Rand_InvisibleCratesDesc);
        [ExecutesMods(typeof(CrashTri_Rand_AshedCrates))]
        public static ModPropOption Option_AllCratesAshed = new ModPropOption("All Crates Are Covered Up", "All crates are covered up to look the same on the outside in each level.");
        [ExecutesMods(typeof(CrashTri_Rand_CrateContents))]
        public static ModPropOption Option_RandCrateContents = new ModPropOption(CrashTri_Text.Rand_CrateContents, CrashTri_Text.Rand_CrateContentsDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CrateParams))]
        public static ModPropOption Option_RandCrateParams = new ModPropOption(CrashTri_Text.Rand_CrateParams, CrashTri_Text.Rand_CrateParamsDesc);
        [ExecutesMods(typeof(CrashTri_Rand_BoxCounter))]
        public static ModPropOption Option_RandBoxCount = new ModPropOption(CrashTri_Text.Rand_CrateCounter, CrashTri_Text.Rand_CrateCounterDesc);
        [ExecutesMods(typeof(Crash2_Rand_BossPaths))]
        public static ModPropOption Option_RandBosses = new ModPropOption(Crash2_Text.Rand_BossLevels, Crash2_Text.Rand_BossLevelsDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CameraFOV))]
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(CrashTri_Text.Mod_CameraWideFOV, CrashTri_Text.Mod_CameraWideFOVDesc);
        [ExecutesMods(typeof(CrashTri_Rand_Music))]
        public static ModPropOption Option_RandMusicTracks = new ModPropOption("Randomize Music Tracks", "Music tracks are randomized, still played using the level's instruments."); //only swaps midis
        [ExecutesMods(typeof(CrashTri_Rand_Audio))]
        public static ModPropOption Option_RandSounds = new ModPropOption(CrashTri_Text.Rand_SFX, CrashTri_Text.Rand_SFXDesc);
        [ExecutesMods(typeof(CrashTri_Rand_StreamedAudio))]
        public static ModPropOption Option_RandStreams = new ModPropOption(CrashTri_Text.Rand_Streams, CrashTri_Text.Rand_StreamsDesc);
        [ExecutesMods(typeof(CrashTri_Rand_PantsColor))]
        public static ModPropOption Option_RandPantsColor = new ModPropOption(CrashTri_Text.Rand_PantsColor, CrashTri_Text.Rand_PantsColorDesc);
        [ExecutesMods(typeof(CrashTri_Crash_RainbowPants))] [ModHidden]
        public static ModPropOption Option_RainbowPants = new ModPropOption("Rainbow Pants", "");
        [ExecutesMods(typeof(CrashTri_Scenery_Rainbow))]
        public static ModPropOption Option_RandWorldColors = new ModPropOption(CrashTri_Text.Rand_WorldColors, CrashTri_Text.Rand_WorldColorsDesc);
        [ExecutesMods(typeof(CrashTri_Scenery_Swizzle))]
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(CrashTri_Text.Rand_WorldPalette, CrashTri_Text.Rand_WorldPaletteDesc);
        [ExecutesMods(typeof(CrashTri_Scenery_Greyscale))]
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(CrashTri_Text.Mod_GreyscaleWorld, CrashTri_Text.Mod_GreyscaleWorldDesc);
        [ExecutesMods(typeof(CrashTri_Scenery_Untextured))]
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(CrashTri_Text.Mod_UntexturedWorld, CrashTri_Text.Mod_UntexturedWorldDesc);
        [ExecutesMods(typeof(CrashTri_Rand_WorldTex))]
        public static ModPropOption Option_RandWorldTex = new ModPropOption(CrashTri_Text.Rand_WorldTex, CrashTri_Text.Rand_WorldTexDesc);
        [ExecutesMods(typeof(CrashTri_Rand_ObjectPalette))]
        public static ModPropOption Option_RandObjPalette = new ModPropOption(CrashTri_Text.Rand_ObjectPalette, CrashTri_Text.Rand_ObjectPaletteDesc);
        [ExecutesMods(typeof(Crash2_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);

        // unfinished
        [ExecutesMods(typeof(CrashTri_Rand_EnemyCrates))] [ModHidden]
        public static ModPropOption Option_RandEnemiesAreCrates = new ModPropOption(CrashTri_Text.Rand_EnemyCrates, CrashTri_Text.Rand_EnemyCratesDesc); //unfinished
        [ExecutesMods(typeof(CrashTri_Objects_Untextured))] [ModHidden]
        public static ModPropOption Option_UntexturedObj = new ModPropOption("Untextured Objects", ""); // broken
        [ExecutesMods(typeof(Crash2_VehicleLevelsOnFoot))] [ModHidden]
        public static ModPropOption Option_VehicleLevelsOnFoot = new ModPropOption("Vehicle Levels On Foot", "");
        [ExecutesMods(typeof(CrashTri_Rand_MirrorLevel))] [ModHidden]
        public static ModPropOption Option_MirroredWorld = new ModPropOption("All Levels Are Mirrored", "");
        [ExecutesMods(typeof(CrashTri_Rand_MirrorLevel))] [ModHidden]
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption("Random Levels Are Mirrored", "");
    }
}
