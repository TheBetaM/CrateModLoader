using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public static class CNK_Props_Main
    {
        public static ModPropOption Option_RandAdventure = new ModPropOption(CNK_Text.Rand_Adventure, CNK_Text.Rand_AdventureDesc);
        public static ModPropOption Option_RandCharStats = new ModPropOption(CNK_Text.Rand_CharacterStats, CNK_Text.Rand_CharacterStatsDesc);
        public static ModPropOption Option_RandKartStats = new ModPropOption(CNK_Text.Rand_KartStats, CNK_Text.Rand_KartStatsDesc);

        public static ModPropOption Option_RandWeaponEffects = new ModPropOption(CNK_Text.Rand_PowerupEffects, CNK_Text.Rand_PowerupEffectsDesc);
        public static ModPropOption Option_RandCharacters = new ModPropOption(CNK_Text.Rand_Drivers, CNK_Text.Rand_DriversDesc); //TODO: later version: icon replacement, name replacement, main menu model replacement, adventure character select model
        public static ModPropOption Option_RandKarts = new ModPropOption(CNK_Text.Rand_Karts, CNK_Text.Rand_KartsDesc);

        public static ModPropOption Option_DisableFadeout = new ModPropOption(CNK_Text.Mod_DisableFadeout, CNK_Text.Mod_DisableFadeoutDesc);
        public static ModPropOption Option_DisablePopups = new ModPropOption(CNK_Text.Mod_DisableUnlockPopups, CNK_Text.Mod_DisableUnlockPopupsDesc);
        public static ModPropOption Option_SpeedUpMaskHints = new ModPropOption(CNK_Text.Mod_SpeedUpMaskHint, CNK_Text.Mod_SpeedUpMaskHintDesc);
        public static ModPropOption Option_NoIntro = new ModPropOption(1, CNK_Text.Mod_RemoveIntroVideos, CNK_Text.Mod_RemoveIntroVideosDesc);

        //unfinished
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "") { Hidden = true }; // audio.csv does NOTHING
        public static ModPropOption Option_RandWumpaCrate = new ModPropOption() { Hidden = true };  //TODO dda
        public static ModPropOption Option_RandObstacles = new ModPropOption() { Hidden = true };  //TODO obstacles
        public static ModPropOption Option_RandCupPoints = new ModPropOption() { Hidden = true };  //Maybe? gameprogression
        public static ModPropOption Option_RandSurfParams = new ModPropOption() { Hidden = true }; // TODO: later version
        public static ModPropOption Option_RandWeaponPools = new ModPropOption() { Hidden = true }; // TODO: later version
        public static ModPropOption Option_NoMaskHints = new ModPropOption() { Hidden = true }; //TODO, hinthistory.csv
    }
}