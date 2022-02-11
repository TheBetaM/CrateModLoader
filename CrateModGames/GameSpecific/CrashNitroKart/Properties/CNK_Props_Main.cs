using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public enum ModProps : int
    {
        KartStats = 1,
        DriverStats = 2,
        Surfaces = 3,
        Powerups = 4,
        Adventure = 5,
        Textures = 6,
    }

    public static class CNK_Props_Main
    {
        [ExecutesMods(typeof(CNK_Rand_Adventure))]
        public static ModPropOption Option_RandAdventure = new ModPropOption(CNK_Text.Rand_Adventure, CNK_Text.Rand_AdventureDesc);
        [ExecutesMods(typeof(CNK_Rand_CharacterStats))]
        public static ModPropOption Option_RandCharStats = new ModPropOption(CNK_Text.Rand_CharacterStats, CNK_Text.Rand_CharacterStatsDesc);
        [ExecutesMods(typeof(CNK_Rand_KartStats))]
        public static ModPropOption Option_RandKartStats = new ModPropOption(CNK_Text.Rand_KartStats, CNK_Text.Rand_KartStatsDesc);

        [ExecutesMods(typeof(CNK_Rand_PowerupEffects))]
        public static ModPropOption Option_RandWeaponEffects = new ModPropOption(CNK_Text.Rand_PowerupEffects, CNK_Text.Rand_PowerupEffectsDesc);
        [ExecutesMods(typeof(CNK_Rand_Drivers))]
        public static ModPropOption Option_RandCharacters = new ModPropOption(CNK_Text.Rand_Drivers, CNK_Text.Rand_DriversDesc); //TODO: later version: icon replacement, name replacement, main menu model replacement, adventure character select model
        [ExecutesMods(typeof(CNK_Rand_Karts))]
        public static ModPropOption Option_RandKarts = new ModPropOption(CNK_Text.Rand_Karts, CNK_Text.Rand_KartsDesc);

        [ExecutesMods(typeof(CNK_DisableFadeout))]
        public static ModPropOption Option_DisableFadeout = new ModPropOption(CNK_Text.Mod_DisableFadeout, CNK_Text.Mod_DisableFadeoutDesc);
        [ExecutesMods(typeof(CNK_DisablePopups))]
        public static ModPropOption Option_DisablePopups = new ModPropOption(CNK_Text.Mod_DisableUnlockPopups, CNK_Text.Mod_DisableUnlockPopupsDesc);
        [ExecutesMods(typeof(CNK_SpeedUpMaskHints))]
        public static ModPropOption Option_SpeedUpMaskHints = new ModPropOption(CNK_Text.Mod_SpeedUpMaskHint, CNK_Text.Mod_SpeedUpMaskHintDesc);
        [ExecutesMods(typeof(CNK_NoIntro))]
        public static ModPropOption Option_NoIntro = new ModPropOption(1, CNK_Text.Mod_RemoveIntroVideos, CNK_Text.Mod_RemoveIntroVideosDesc);
        [ExecutesMods(typeof(CNK_Rand_SurfaceParams))] [ModHidden]
        public static ModPropOption Option_RandSurfParams = new ModPropOption("Randomize Surface Parameters", ""); // TODO: just handles props atm, add randomizer
        [ExecutesMods(typeof(CNK_Rand_PowerupDistribution))] [ModHidden]
        public static ModPropOption Option_RandPowerupDistribution = new ModPropOption("Randomize Powerup Distribution", ""); // TODO: just handles props atm, add randomizer
        [ExecutesMods(typeof(CNK_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1) { Hidden = true, };

        //unfinished
        [ExecutesMods(typeof(CNK_Rand_Music))] [ModHidden]
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", ""); // audio.csv does NOTHING
        [ModHidden]
        public static ModPropOption Option_RandWumpaCrate = new ModPropOption();  //TODO dda
        [ModHidden]
        public static ModPropOption Option_RandObstacles = new ModPropOption();  //TODO obstacles
        [ModHidden]
        public static ModPropOption Option_RandCupPoints = new ModPropOption();  //Maybe? gameprogression
        [ModHidden]
        public static ModPropOption Option_NoMaskHints = new ModPropOption(); //TODO, hinthistory.csv
    }
}