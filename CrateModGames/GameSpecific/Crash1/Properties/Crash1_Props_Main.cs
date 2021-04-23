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
        public static ModPropOption Option_RandBonusRounds = new ModPropOption(new Crash1_Rand_BonusRounds()) { Hidden = true, }; //todo
        public static ModPropOption Option_RandMap = new ModPropOption(new Crash1_Rand_Map());
        public static ModPropOption Option_AddStormyAscent = new ModPropOption(new Crash1_AddStormyAscent());
        public static ModPropOption Option_RandCrates = new ModPropOption(new CrashTri_Rand_WoodenCrates());
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(new Crash1_Rand_BackwardsLevels(false));
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(new Crash1_Rand_BackwardsLevels(true), Crash1_Text.Rand_BackwardsLevels, Crash1_Text.Rand_BackwardsLevelsDesc);
        public static ModPropOption Option_BackwardsHogLevels = new ModPropOption(new Crash1_BackwardsHogLevels());
        public static ModPropOption Option_RandCratesMissing = new ModPropOption(new CrashTri_Rand_CratesIntoWumpa(true), CrashTri_Text.Rand_CratesRemoved, CrashTri_Text.Rand_CratesRemovedDesc);
        public static ModPropOption Option_RandCrateContents = new ModPropOption(new CrashTri_Rand_CrateContents());
        public static ModPropOption Option_InvisibleCrates = new ModPropOption(new CrashTri_Rand_InvisibleCrates(false), Crash1_Text.Mod_InvisibleCrates, Crash1_Text.Mod_InvisibleCratesDesc);
        public static ModPropOption Option_RandInvisibleCrates = new ModPropOption(new CrashTri_Rand_InvisibleCrates(true), Crash1_Text.Rand_InvisibleCrates, Crash1_Text.Rand_InvisibleCratesDesc);
        public static ModPropOption Option_RandCrateParams = new ModPropOption(new CrashTri_Rand_CrateParams());
        public static ModPropOption Option_RandBosses = new ModPropOption(new Crash1_Rand_BossPaths());
        public static ModPropOption Option_RandLightCol = new ModPropOption(new Crash1_Rand_LightColor());
        public static ModPropOption Option_EnableDog = new ModPropOption(new Crash1_EnableDog());
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(new CrashTri_Rand_CameraFOV(false)) { Hidden = true, };
        public static ModPropOption Option_Metadata = new ModPropOption(new Crash1_Metadata(), 1) { Hidden = true, };

        public static ModPropOption Option_RandMusicTracks = new ModPropOption(new CrashTri_Rand_Music()); //only swaps midis
        public static ModPropOption Option_RandSounds = new ModPropOption(new CrashTri_Rand_Audio());
        public static ModPropOption Option_RandWorldColors = new ModPropOption(new CrashTri_Scenery_Rainbow());
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(new CrashTri_Scenery_Swizzle());

        //unfinished
        public static ModPropOption Option_RandPantsColor = new ModPropOption(new CrashTri_Rand_PantsColor(true)) { Hidden = true };

        public static ModPropOption Option_RandEnemiesMissing = new ModPropOption("Random Enemies Removed", "") { Hidden = true, }; //todo
        public static ModPropOption Option_AddCavernLevel = new ModPropOption(new Crash1_AddCavernLevel());// { Hidden = true, };
        public static ModPropOption Option_HogLevelsOnFoot = new ModPropOption("Hog Levels On Foot", "") { Hidden = true };
        public static ModPropOption Option_MirroredWorld = new ModPropOption(new CrashTri_Rand_MirrorLevel(false)) { Hidden = true };
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption(new CrashTri_Rand_MirrorLevel(true)) { Hidden = true };
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(new CrashTri_Scenery_Greyscale()) { Hidden = true };
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(new CrashTri_Scenery_Untextured()) { Hidden = true };
    }
}
