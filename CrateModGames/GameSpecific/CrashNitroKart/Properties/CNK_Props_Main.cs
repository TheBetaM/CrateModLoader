using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public static class CNK_Props_Main
    {
        public static ModPropOption Option_RandAdventure = new ModPropOption(new CNK_Rand_Adventure());
        public static ModPropOption Option_RandCharStats = new ModPropOption(new CNK_Rand_CharacterStats());
        public static ModPropOption Option_RandKartStats = new ModPropOption(new CNK_Rand_KartStats());

        public static ModPropOption Option_RandWeaponEffects = new ModPropOption(new CNK_Rand_PowerupEffects());
        public static ModPropOption Option_RandCharacters = new ModPropOption(new CNK_Rand_Drivers()); //TODO: later version: icon replacement, name replacement, main menu model replacement, adventure character select model
        public static ModPropOption Option_RandKarts = new ModPropOption(new CNK_Rand_Karts());

        public static ModPropOption Option_DisableFadeout = new ModPropOption(new CNK_DisableFadeout());
        public static ModPropOption Option_DisablePopups = new ModPropOption(new CNK_DisablePopups());
        public static ModPropOption Option_SpeedUpMaskHints = new ModPropOption(new CNK_SpeedUpMaskHints());
        public static ModPropOption Option_NoIntro = new ModPropOption(new CNK_NoIntro(), 1);
        public static ModPropOption Option_RandSurfParams = new ModPropOption(new CNK_Rand_SurfaceParams()) { Hidden = true }; // TODO: just handles props atm, add randomizer
        public static ModPropOption Option_RandPowerupDistribution = new ModPropOption(new CNK_Rand_PowerupDistribution()) { Hidden = true }; // TODO: just handles props atm, add randomizer
        public static ModPropOption Option_Metadata = new ModPropOption(new CNK_Metadata(), 1) { Hidden = true, };

        //unfinished
        public static ModPropOption Option_RandMusic = new ModPropOption(new CNK_Rand_Music()) { Hidden = true }; // audio.csv does NOTHING
        public static ModPropOption Option_RandWumpaCrate = new ModPropOption() { Hidden = true };  //TODO dda
        public static ModPropOption Option_RandObstacles = new ModPropOption() { Hidden = true };  //TODO obstacles
        public static ModPropOption Option_RandCupPoints = new ModPropOption() { Hidden = true };  //Maybe? gameprogression
        public static ModPropOption Option_NoMaskHints = new ModPropOption() { Hidden = true }; //TODO, hinthistory.csv
    }
}