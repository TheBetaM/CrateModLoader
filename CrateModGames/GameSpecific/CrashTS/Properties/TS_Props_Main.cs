using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashTS;
using CrateModLoader.GameSpecific.CrashTS.Mods;

namespace CrateModLoader.GameSpecific.CrashTS
{
    static class TS_Props_Main
    {
        public static ModPropOption Option_RandCrates = new ModPropOption(new TS_Rand_Crates())
        {
            Items = new List<string>() {
                "Off", "All Crates", "Types Only", },
            ItemsDesc = new List<string>() {
                "", "Individual crates are randomized.", "Only crate types are switched around.", },
        };
        public static ModPropOption Option_RandGemLocations = new ModPropOption(new TS_Rand_GemLocations());
        public static ModPropOption Option_RandCharacterParams = new ModPropOption(new TS_Rand_CharParams());
        public static ModPropOption Option_RandSurfaces = new ModPropOption(new TS_Rand_Surfaces());
        public static ModPropOption Option_RandStartingChunk = new ModPropOption(new TS_Rand_StartingChunk());
        public static ModPropOption Option_FlyingKick = new ModPropOption(new TS_EnableFlyingKick());
        public static ModPropOption Option_StompKick = new ModPropOption(new TS_EnableStompKick());
        public static ModPropOption Option_DoubleJumpCortex = new ModPropOption(new TS_CortexDoubleJump());
        public static ModPropOption Option_DoubleJumpNina = new ModPropOption(new TS_NinaDoubleJump());
        public static ModPropOption Option_UnusedEnemies = new ModPropOption(new TS_EnableHiddenEnemies());
        public static ModPropOption Option_SwitchCharacters = new ModPropOption(new TS_SwitchCharacters());
        public static ModPropOption Option_ClassicSlideJump = new ModPropOption(new TS_EnableClassicSlideJump());
        public static ModPropOption Option_ClassicHealth = new ModPropOption(new TS_ClassicHealth());
        public static ModPropOption Option_ClassicExplosions = new ModPropOption(new TS_ClassicExplosions());
        public static ModPropOption Option_UnlockedCamera = new ModPropOption(new TS_UnlockedCamera());
        public static ModPropOption Option_SkipCutscenes = new ModPropOption(new TS_SkipCutscenes()) { Hidden = true, };
        public static ModPropOption Option_Metadata = new ModPropOption(new TS_Metadata(), 1) { Hidden = true, };

        [ModAllowedConsoles(ConsoleMode.PS2)]
        public static ModPropOption Option_RandPantsColor = new ModPropOption(new TS_Rand_PantsColor());
        public static ModPropOption Option_RandomizeMusic = new ModPropOption(new TS_Rand_Music());
        [ModAllowedConsoles(ConsoleMode.PS2)]
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(new TS_Rand_WorldPalette());
        [ModAllowedConsoles(ConsoleMode.PS2)]
        public static ModPropOption Option_GreyscaleDimension = new ModPropOption(new TS_GreyscaleDimension());
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(new TS_UntexturedWorld());

        //unfinished
        //public static ModPropOption Option_RandomWumpaCrates = new ModPropOption(new TS_Rand_WumpaIntoCrates()) { Hidden = true, };
        [ModCategory((int)ModProps.Misc), ModMenuOnly]
        public static ModPropOption Option_AllWumpaCrates = new ModPropOption(new TS_Rand_WumpaIntoCrates()) { Hidden = true, };
        public static ModPropOption Option_RandEnemies = new ModPropOption(new TS_Rand_Enemies()) { Hidden = true, }; // not stable enough
        //public static ModPropOption Option_ClassicCrates = new ModPropOption(Twins_Text.Mod_ClassicCratePersistence, Twins_Text.Mod_ClassicCratePersistenceDesc); // TODO
        //public static ModPropOption Option_ClassicBossHealth = new ModPropOption("Classic Boss Health", "Start boss fights with 2 masks."); // TODO
        //public static ModPropOption Option_MirroredWorld = new ModPropOption("Mirrored World", ""); // TODO
    }
}
