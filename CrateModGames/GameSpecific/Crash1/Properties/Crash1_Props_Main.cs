using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash1;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash1.Mods;

namespace CrateModLoader.GameSpecific.Crash1
{
    static class Crash1_Props_Main
    {
        [ExecutesMods(typeof(Crash1_Rand_BonusRounds))] [ModHidden]
        public static ModPropOption Option_RandBonusRounds = new ModPropOption("Randomize Bonus Rounds", ""); //todo
        [ExecutesMods(typeof(Crash1_Rand_Map))]
        public static ModPropOption Option_RandMap = new ModPropOption("Randomize Level Order", "Shuffle the order of which levels you enter. The Cortex boss is still the last level to play.");
        [ExecutesMods(typeof(Crash1_AddStormyAscent))]
        public static ModPropOption Option_AddStormyAscent = new ModPropOption("Add Stormy Ascent", "Replaces The Great Hall with Stormy Ascent. Works with all other features like Backwards Levels and Randomize Level Order. (Tokens removed from the level to ensure stability)");
        [ExecutesMods(typeof(CrashTri_Rand_WoodenCrates))]
        public static ModPropOption Option_RandCrates = new ModPropOption(CrashTri_Text.Rand_Crates, CrashTri_Text.Rand_CratesDesc);
        [ExecutesMods(typeof(Crash1_Rand_BackwardsLevels))]
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(Crash1_Text.Mod_BackwardsLevels, Crash1_Text.Mod_BackwardsLevelsDesc);
        [ExecutesMods(typeof(Crash1_Rand_BackwardsLevels))]
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(Crash1_Text.Rand_BackwardsLevels, Crash1_Text.Rand_BackwardsLevelsDesc);
        [ExecutesMods(typeof(Crash1_BackwardsHogLevels))]
        public static ModPropOption Option_BackwardsHogLevels = new ModPropOption(Crash1_Text.Mod_BackwardsHogLevels, Crash1_Text.Mod_BackwardsHogLevelsDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CratesIntoWumpa))] [ModHidden]
        public static ModPropOption Option_RandCratesMissing = new ModPropOption(CrashTri_Text.Rand_CratesRemoved, CrashTri_Text.Rand_CratesRemovedDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CrateContents))]
        public static ModPropOption Option_RandCrateContents = new ModPropOption(CrashTri_Text.Rand_CrateContents, CrashTri_Text.Rand_CrateContentsDesc);
        [ExecutesMods(typeof(CrashTri_Rand_InvisibleCrates))]
        public static ModPropOption Option_InvisibleCrates = new ModPropOption(Crash1_Text.Mod_InvisibleCrates, Crash1_Text.Mod_InvisibleCratesDesc);
        [ExecutesMods(typeof(CrashTri_Rand_InvisibleCrates))]
        public static ModPropOption Option_RandInvisibleCrates = new ModPropOption(Crash1_Text.Rand_InvisibleCrates, Crash1_Text.Rand_InvisibleCratesDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CrateParams))] [ModHidden]
        public static ModPropOption Option_RandCrateParams = new ModPropOption(CrashTri_Text.Rand_CrateParams, CrashTri_Text.Rand_CrateParamsDesc);
        [ExecutesMods(typeof(Crash1_Rand_BossPaths))]
        public static ModPropOption Option_RandBosses = new ModPropOption(Crash1_Text.Rand_BossLevels, Crash1_Text.Rand_BossLevelsDesc);
        [ExecutesMods(typeof(Crash1_Rand_LightColor))]
        public static ModPropOption Option_RandLightCol = new ModPropOption(Crash1_Text.Rand_LightCol, Crash1_Text.Rand_LightColDesc);
        [ExecutesMods(typeof(Crash1_EnableDog))]
        public static ModPropOption Option_EnableDog = new ModPropOption(Crash1_Text.Mod_EnableDog, Crash1_Text.Mod_EnableDog);
        [ExecutesMods(typeof(CrashTri_Rand_CameraFOV))] [ModHidden]
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(CrashTri_Text.Mod_CameraWideFOV, CrashTri_Text.Mod_CameraWideFOVDesc);
        [ExecutesMods(typeof(Crash1_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);

        [ExecutesMods(typeof(CrashTri_Rand_Music))]
        public static ModPropOption Option_RandMusicTracks = new ModPropOption("Randomize Music Tracks", "Music tracks are randomized, still played using the level's instruments."); //only swaps midis
        [ExecutesMods(typeof(CrashTri_Rand_Audio))]
        public static ModPropOption Option_RandSounds = new ModPropOption(CrashTri_Text.Rand_SFX, CrashTri_Text.Rand_SFXDesc);
        [ExecutesMods(typeof(CrashTri_Scenery_Rainbow))]
        public static ModPropOption Option_RandWorldColors = new ModPropOption(CrashTri_Text.Rand_WorldColors, CrashTri_Text.Rand_WorldColorsDesc);
        [ExecutesMods(typeof(CrashTri_Scenery_Swizzle))]
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(CrashTri_Text.Rand_WorldPalette, CrashTri_Text.Rand_WorldPaletteDesc);

        //unfinished
        [ExecutesMods(typeof(CrashTri_Rand_PantsColor))] [ModHidden]
        public static ModPropOption Option_RandPantsColor = new ModPropOption(CrashTri_Text.Rand_PantsColor, CrashTri_Text.Rand_PantsColorDesc);

        [ModHidden]
        public static ModPropOption Option_RandEnemiesMissing = new ModPropOption("Random Enemies Removed", ""); //todo
        [ExecutesMods(typeof(Crash1_AddCavernLevel))] [ModHidden]
        public static ModPropOption Option_AddCavernLevel = new ModPropOption("Add Caved In", "Replaces Papu Papu with the unused cavern level.");
        [ModHidden]
        public static ModPropOption Option_HogLevelsOnFoot = new ModPropOption("Hog Levels On Foot", "");
        [ExecutesMods(typeof(CrashTri_Rand_MirrorLevel))] [ModHidden]
        public static ModPropOption Option_MirroredWorld = new ModPropOption("All Levels Are Mirrored", "");
        [ExecutesMods(typeof(CrashTri_Rand_MirrorLevel))] [ModHidden]
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption("Random Levels Are Mirrored", "");
        [ExecutesMods(typeof(CrashTri_Scenery_Greyscale))] [ModHidden]
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(CrashTri_Text.Mod_GreyscaleWorld, CrashTri_Text.Mod_GreyscaleWorldDesc);
        [ExecutesMods(typeof(CrashTri_Scenery_Untextured))] [ModHidden]
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(CrashTri_Text.Mod_UntexturedWorld, CrashTri_Text.Mod_UntexturedWorldDesc);
    }
}
