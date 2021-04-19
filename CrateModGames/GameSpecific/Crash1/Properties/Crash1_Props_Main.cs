using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash1;
//using CrateModLoader.GameSpecific.Crash1.Mods;

namespace CrateModLoader.GameSpecific.Crash1
{
    static class Crash1_Props_Main
    {
        public static ModPropOption Option_RandBonusRounds = new ModPropOption("Randomize Bonus Rounds", "") { Hidden = true, }; //todo
        public static ModPropOption Option_RandMap = new ModPropOption("Randomize Level Order", "Shuffle the order of which levels you enter. The Cortex boss is still the last level to play.");
        public static ModPropOption Option_AddStormyAscent = new ModPropOption("Add Stormy Ascent", "Replaces The Great Hall with Stormy Ascent. Works with all other features like Backwards Levels and Randomize Level Order. (Tokens removed from the level to ensure stability)");
        public static ModPropOption Option_RandCrates = new ModPropOption(CrashTri_Text.Rand_Crates, CrashTri_Text.Rand_CratesDesc);
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(Crash1_Text.Mod_BackwardsLevels, Crash1_Text.Mod_BackwardsLevelsDesc);
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(Crash1_Text.Rand_BackwardsLevels, Crash1_Text.Rand_BackwardsLevelsDesc);
        public static ModPropOption Option_BackwardsHogLevels = new ModPropOption(Crash1_Text.Mod_BackwardsHogLevels, Crash1_Text.Mod_BackwardsHogLevelsDesc);
        public static ModPropOption Option_RandEnemiesMissing = new ModPropOption("Random Enemies Removed", "") { Hidden = true, }; //todo
        public static ModPropOption Option_RandCrateContents = new ModPropOption(CrashTri_Text.Rand_CrateContents, CrashTri_Text.Rand_CrateContentsDesc);
        public static ModPropOption Option_InvisibleCrates = new ModPropOption(Crash1_Text.Mod_InvisibleCrates, Crash1_Text.Mod_InvisibleCratesDesc);
        public static ModPropOption Option_RandInvisibleCrates = new ModPropOption(Crash1_Text.Rand_InvisibleCrates, Crash1_Text.Rand_InvisibleCratesDesc);
        public static ModPropOption Option_RandBosses = new ModPropOption(Crash1_Text.Rand_BossLevels, Crash1_Text.Rand_BossLevelsDesc);
        public static ModPropOption Option_RandLightCol = new ModPropOption(Crash1_Text.Rand_LightCol, Crash1_Text.Rand_LightColDesc);
        public static ModPropOption Option_EnableDog = new ModPropOption(Crash1_Text.Mod_EnableDog, Crash1_Text.Mod_EnableDog);
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(CrashTri_Text.Mod_CameraWideFOV, CrashTri_Text.Mod_CameraWideFOVDesc) { Hidden = true, };

        public static ModPropOption Option_RandMusicTracks = new ModPropOption("Randomize Music Tracks", "Music tracks are randomized, still played using the level's instruments."); //only swaps midis
        public static ModPropOption Option_RandSounds = new ModPropOption(CrashTri_Text.Rand_SFX, CrashTri_Text.Rand_SFXDesc);
        public static ModPropOption Option_RandWorldColors = new ModPropOption(CrashTri_Text.Rand_WorldColors, CrashTri_Text.Rand_WorldColorsDesc);
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(CrashTri_Text.Rand_WorldPalette, CrashTri_Text.Rand_WorldPaletteDesc);

        //unfinished
        public static ModPropOption Option_RandPantsColor = new ModPropOption("Randomize Pants Color", "") { Hidden = true };

        public static ModPropOption Option_AddCavernLevel = new ModPropOption("Add Caved In", "Replaces Papu Papu with the unused cavern level.");// { Hidden = true, };
        public static ModPropOption Option_HogLevelsOnFoot = new ModPropOption("Hog Levels On Foot", "") { Hidden = true };
        public static ModPropOption Option_MirroredWorld = new ModPropOption("Mirrored World", "") { Hidden = true };
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption("Random Levels Are Mirrored", "") { Hidden = true };
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "") { Hidden = true }; //shuffle tracks from different levels (must be identical to vanilla playback, just in a different level)
        public static ModPropOption Option_RandMusicInstruments = new ModPropOption("Randomize Music Instruments", "") { Hidden = true }; //only swap wavebanks
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption("Greyscale World", "") { Hidden = true };
        public static ModPropOption Option_UntexturedWorld = new ModPropOption("Untextured/Greyscale World", "") { Hidden = true };
    }
}
