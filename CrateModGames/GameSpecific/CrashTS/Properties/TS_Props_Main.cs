using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashTS;
using CrateModLoader.GameSpecific.CrashTS.Mods;

namespace CrateModLoader.GameSpecific.CrashTS
{
    static class TS_Props_Main
    {
        [ExecutesMods(typeof(TS_Rand_Crates))]
        public static ModPropOption Option_RandCrates = new ModPropOption(Twins_Text.Rand_Crates, Twins_Text.Rand_CratesDesc)
        {
            Items = new List<string>() {
                "Off", "All Crates", "Types Only", },
            ItemsDesc = new List<string>() {
                "", "Individual crates are randomized.", "Only crate types are switched around.", },
        };
        [ExecutesMods(typeof(TS_Rand_GemLocations))]
        public static ModPropOption Option_RandGemLocations = new ModPropOption(Twins_Text.Rand_GemLocations, Twins_Text.Rand_GemLocationsDesc);
        [ExecutesMods(typeof(TS_Rand_CharParams))]
        public static ModPropOption Option_RandCharacterParams = new ModPropOption(Twins_Text.Rand_CharParams, Twins_Text.Rand_CharParamsDesc);
        [ExecutesMods(typeof(TS_Rand_Surfaces))]
        public static ModPropOption Option_RandSurfaces = new ModPropOption(Twins_Text.Rand_SurfaceParams, Twins_Text.Rand_SurfaceParamsDesc);
        [ExecutesMods(typeof(TS_Rand_StartingChunk), typeof(TS_SwapStartCreditsSpawn), typeof(TS_ChangeStartingChunk))]
        public static ModPropOption Option_RandStartingChunk = new ModPropOption(Twins_Text.Rand_StartingLevel, Twins_Text.Rand_StartingLevelDesc);
        [ExecutesMods(typeof(TS_EnableFlyingKick))]
        public static ModPropOption Option_FlyingKick = new ModPropOption(Twins_Text.Mod_FlyingKick, Twins_Text.Mod_FlyingKickDesc);
        [ExecutesMods(typeof(TS_EnableStompKick))]
        public static ModPropOption Option_StompKick = new ModPropOption(Twins_Text.Mod_StompKick, Twins_Text.Mod_StompKickDesc);
        [ExecutesMods(typeof(TS_CortexDoubleJump))]
        public static ModPropOption Option_DoubleJumpCortex = new ModPropOption(Twins_Text.Mod_CortexDoubleJump, Twins_Text.Mod_CortexDoubleJumpDesc);
        [ExecutesMods(typeof(TS_NinaDoubleJump))]
        public static ModPropOption Option_DoubleJumpNina = new ModPropOption(Twins_Text.Mod_NinaDoubleJump, Twins_Text.Mod_NinaDoubleJumpDesc);
        [ExecutesMods(typeof(TS_EnableHiddenEnemies))]
        public static ModPropOption Option_UnusedEnemies = new ModPropOption(Twins_Text.Mod_UnusedEnemies, Twins_Text.Mod_UnusedEnemiesDesc);
        [ExecutesMods(typeof(TS_SwitchCharacters))]
        public static ModPropOption Option_SwitchCharacters = new ModPropOption(Twins_Text.Mod_SwitchCharacters, Twins_Text.Mod_SwitchCharactersDesc);
        [ExecutesMods(typeof(TS_EnableClassicSlideJump))]
        public static ModPropOption Option_ClassicSlideJump = new ModPropOption(Twins_Text.Mod_ClassicSlideJump, Twins_Text.Mod_ClassicSlideJumpDesc);
        [ExecutesMods(typeof(TS_ClassicHealth))]
        public static ModPropOption Option_ClassicHealth = new ModPropOption(Twins_Text.Mod_ClassicHealth, Twins_Text.Mod_ClassicHealthDesc);
        [ExecutesMods(typeof(TS_ClassicExplosions))]
        public static ModPropOption Option_ClassicExplosions = new ModPropOption(Twins_Text.Mod_ClassicExplosionDaamge, Twins_Text.Mod_ClassicExplosionDamageDesc);
        [ExecutesMods(typeof(TS_UnlockedCamera))]
        public static ModPropOption Option_UnlockedCamera = new ModPropOption(Twins_Text.Mod_UnlockedCamera, Twins_Text.Mod_UnlockedCameraDesc);
        [ExecutesMods(typeof(TS_SkipCutscenes))] [ModHidden]
        public static ModPropOption Option_SkipCutscenes = new ModPropOption(Twins_Text.Mod_SkipCutscenes, Twins_Text.Mod_SkipCutscenesDesc);
        [ExecutesMods(typeof(TS_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);

        [ModAllowedConsoles(ConsoleMode.PS2)] [ExecutesMods(typeof(TS_Rand_PantsColor))]
        public static ModPropOption Option_RandPantsColor = new ModPropOption(Twins_Text.Rand_PantsColor, Twins_Text.Rand_PantsColorDesc);
        [ExecutesMods(typeof(TS_Rand_Music))]
        public static ModPropOption Option_RandomizeMusic = new ModPropOption(Twins_Text.Rand_Music, Twins_Text.Rand_MusicDesc);
        [ModAllowedConsoles(ConsoleMode.PS2)] [ExecutesMods(typeof(TS_Rand_WorldPalette))]
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(Twins_Text.Rand_WorldPalette, Twins_Text.Rand_WorldPaletteDesc);
        [ModAllowedConsoles(ConsoleMode.PS2)] [ExecutesMods(typeof(TS_GreyscaleDimension))]
        public static ModPropOption Option_GreyscaleDimension = new ModPropOption(Twins_Text.Mod_GreyscaleDimension, Twins_Text.Mod_GreyscaleDimensionDesc);
        [ExecutesMods(typeof(TS_UntexturedWorld))]
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(Twins_Text.Mod_UntexturedWorld, Twins_Text.Mod_UntexturedWorldDesc);

        //unfinished
        [ModCategory((int)ModProps.Misc), ModMenuOnly] [ExecutesMods(typeof(TS_Rand_WumpaIntoCrates))] [ModHidden]
        public static ModPropOption Option_AllWumpaCrates = new ModPropOption(Twins_Text.Rand_WumpaIntoCrates, Twins_Text.Rand_WumpaIntoCratesDesc);
        [ExecutesMods(typeof(TS_Rand_Enemies))] [ModHidden]
        public static ModPropOption Option_RandEnemies = new ModPropOption("Randomize Enemies (Soundless)", ""); // not stable enough
        //public static ModPropOption Option_RandomWumpaCrates = new ModPropOption(new TS_Rand_WumpaIntoCrates()) { Hidden = true, };
        //public static ModPropOption Option_ClassicCrates = new ModPropOption(Twins_Text.Mod_ClassicCratePersistence, Twins_Text.Mod_ClassicCratePersistenceDesc); // TODO
        //public static ModPropOption Option_ClassicBossHealth = new ModPropOption("Classic Boss Health", "Start boss fights with 2 masks."); // TODO
        //public static ModPropOption Option_MirroredWorld = new ModPropOption("Mirrored World", ""); // TODO
    }
}
