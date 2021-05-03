using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    static class CTTR_Props_Main
    {
        [ExecutesMods(typeof(CTTR_Rand_PlatformingCharacter))]
        public static ModPropOption Option_RandCharacters = new ModPropOption(CTTR_Text.Rand_PlatformingCharacter, CTTR_Text.Rand_PlatformingCharacterDesc);// todo: change missions to unlock crash and cortex if they're not in the starting pool
        [ExecutesMods(typeof(CTTR_Rand_TrackEntrances))]
        public static ModPropOption Option_RandTrackEntrances = new ModPropOption(CTTR_Text.Rand_TrackEntrances, CTTR_Text.Rand_TrackEntrancesDesc); // todo: arenas
        [ExecutesMods(typeof(CTTR_Rand_MinigameEntrances))]
        public static ModPropOption Option_RandMinigames = new ModPropOption(CTTR_Text.Rand_Minigames, CTTR_Text.Rand_MinigamesDesc); // todo: minigame challenges aswell
        [ExecutesMods(typeof(CTTR_Rand_BattleKOs))]
        public static ModPropOption Option_RandBattleKOs = new ModPropOption("Randomize Battle KO's", ""); // doesn't work?
        [ExecutesMods(typeof(CTTR_Rand_RaceLaps))]
        public static ModPropOption Option_RandRaceLaps = new ModPropOption(CTTR_Text.Rand_RaceLaps, CTTR_Text.Rand_RaceLapsDesc);
        [ExecutesMods(typeof(CTTR_PreventSequenceBreaks))]
        public static ModPropOption Option_NoSequenceBreaks = new ModPropOption(CTTR_Text.Mod_PreventSkips, CTTR_Text.Mod_PreventSkipsDesc);
        [ExecutesMods(typeof(CTTR_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);

        //unfinished
        [ExecutesMods(typeof(CTTR_Rand_HubEntrances))] [ModHidden]
        public static ModPropOption Option_RandHubEntrances = new ModPropOption("Randomize Hub Entrances", ""); // todo: gem keys in missionobjectives_x and platforming_objects, unlock failure message, key missions
        [ModHidden]
        public static ModPropOption Option_RandMissions = new ModPropOption("Randomize Missions", ""); // todo, genericobjectives, missionobjectives_x, level NIS+NPC
        [ModHidden]
        public static ModPropOption Option_RandCarStats = new ModPropOption("Randomize Car Stats", ""); // todo: vehicles, levels/common for speed tier values
        [ModHidden]
        public static ModPropOption Option_RandCrashinator = new ModPropOption("Randomize Crashinator", ""); // todo: kamikaze
        [ModHidden]
        public static ModPropOption Option_RandRunAndGun = new ModPropOption("Randomize Run & Gun", ""); // todo: railshooter
        [ModHidden]
        public static ModPropOption Option_RandStuntArena = new ModPropOption("Randomize Stunt Arena", ""); //todo: permanent_objects, stunt_objects
        [ModHidden]
        public static ModPropOption Option_RandSurfParams = new ModPropOption("Randomize Surface Parameters", ""); //todo: car_effect_objects
        [ModHidden]
        public static ModPropOption Option_RandPowerupDist = new ModPropOption("Randomize Powerup Distribution", ""); // todo: driving_objects
        [ModHidden]
        public static ModPropOption Option_RandPowerupEffects = new ModPropOption("Randomize Powerup Effects", ""); //todo: driving_objects
        [ModHidden]
        public static ModPropOption Option_RandWeapons = new ModPropOption("Randomize Weapons", ""); // todo: turretmotifs
        [ModHidden]
        public static ModPropOption Option_RandNPCLocations = new ModPropOption("Randomize NPC Locations", ""); // todo: NPC - locator list

    }
}
