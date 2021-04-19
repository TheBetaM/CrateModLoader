using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    static class CTTR_Props_Main
    {
        public static ModPropOption Option_RandCharacters = new ModPropOption(CTTR_Text.Rand_PlatformingCharacter, CTTR_Text.Rand_PlatformingCharacterDesc);// todo: change missions to unlock crash and cortex if they're not in the starting pool
        public static ModPropOption Option_RandTrackEntrances = new ModPropOption(CTTR_Text.Rand_TrackEntrances, CTTR_Text.Rand_TrackEntrancesDesc); // todo: arenas
        public static ModPropOption Option_RandMinigames = new ModPropOption(CTTR_Text.Rand_Minigames, CTTR_Text.Rand_MinigamesDesc); // todo: minigame challenges aswell
        public static ModPropOption Option_RandRaceLaps = new ModPropOption(CTTR_Text.Rand_RaceLaps, CTTR_Text.Rand_RaceLapsDesc);
        public static ModPropOption Option_NoSequenceBreaks = new ModPropOption(CTTR_Text.Mod_PreventSkips, CTTR_Text.Mod_PreventSkipsDesc);

        //unfinished
        public static ModPropOption Option_RandHubEntrances = new ModPropOption("Randomize Hub Entrances", "") { Hidden = true }; // todo: gem keys in missionobjectives_x and platforming_objects, unlock failure message, key missions
        public static ModPropOption Option_RandMissions = new ModPropOption("Randomize Missions", "") { Hidden = true }; // todo, genericobjectives, missionobjectives_x, level NIS+NPC
        public static ModPropOption Option_RandCarStats = new ModPropOption("Randomize Car Stats", "") { Hidden = true }; // todo: vehicles, levels/common for speed tier values
        public static ModPropOption Option_RandBattleKOs = new ModPropOption("Randomize Battle KO's", "") { Hidden = true }; // doesn't work?
        public static ModPropOption Option_RandCrashinator = new ModPropOption("Randomize Crashinator", "") { Hidden = true }; // todo: kamikaze
        public static ModPropOption Option_RandRunAndGun = new ModPropOption("Randomize Run & Gun", "") { Hidden = true }; // todo: railshooter
        public static ModPropOption Option_RandStuntArena = new ModPropOption("Randomize Stunt Arena", "") { Hidden = true }; //todo: permanent_objects, stunt_objects
        public static ModPropOption Option_RandSurfParams = new ModPropOption("Randomize Surface Parameters", "") { Hidden = true }; //todo: car_effect_objects
        public static ModPropOption Option_RandPowerupDist = new ModPropOption("Randomize Powerup Distribution", "") { Hidden = true }; // todo: driving_objects
        public static ModPropOption Option_RandPowerupEffects = new ModPropOption("Randomize Powerup Effects", "") { Hidden = true }; //todo: driving_objects
        public static ModPropOption Option_RandWeapons = new ModPropOption("Randomize Weapons", "") { Hidden = true }; // todo: turretmotifs
        public static ModPropOption Option_RandNPCLocations = new ModPropOption("Randomize NPC Locations", "") { Hidden = true }; // todo: NPC - locator list

    }
}
