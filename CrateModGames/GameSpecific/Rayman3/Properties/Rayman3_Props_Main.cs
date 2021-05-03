using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public enum ModProps : int
    {
        Options = 0,
        Textures_General,
        Textures_Menu,
        Textures_Loading,
    }

    public static class Rayman3_Props_Main
    {
        [ExecutesMods(typeof(Ray3_Rand_LevelOrder))]
        public static ModPropOption Option_RandLevelOrderAll = new ModPropOption(Rayman3_Text.Rand_LevelOrder2, Rayman3_Text.Rand_LevelOrder2Desc);
        [ExecutesMods(typeof(Ray3_Rand_LevelOrder))]
        public static ModPropOption Option_RandLevelOrder = new ModPropOption(Rayman3_Text.Rand_LevelOrder, Rayman3_Text.Rand_LevelOrderDesc);
        [ExecutesMods(typeof(Ray3_Rand_OutfitVisuals))]
        public static ModPropOption Option_RandOutfitVisuals = new ModPropOption(Rayman3_Text.Rand_OutfitVisuals, Rayman3_Text.Rand_OutfitVisualsDesc);
        [ExecutesMods(typeof(Ray3_Rand_OutfitColors))] [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandOutfitColors = new ModPropOption(Rayman3_Text.Rand_OutfitColors, Rayman3_Text.Rand_OutfitColorsDesc);
        [ExecutesMods(typeof(Ray3_Rand_CopterColors))] [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandCopterColors = new ModPropOption(Rayman3_Text.Rand_CopterColors, Rayman3_Text.Rand_CopterColorsDesc);
        [ExecutesMods(typeof(Ray3_Rand_HUDColors))] [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandHUDColors = new ModPropOption(Rayman3_Text.Rand_HUDColors, Rayman3_Text.Rand_HUDColorsDesc);
        [ExecutesMods(typeof(Ray3_Rand_WorldColors))] [ModAllowedConsoles(ConsoleMode.GCN)] [ModHidden]
        public static ModPropOption Option_RandWorldColors = new ModPropOption(Rayman3_Text.Rand_WorldColors, Rayman3_Text.Rand_WorldColorsDesc); // works in some levels, broken in others
        [ExecutesMods(typeof(Ray3_Start2DNightmare))] [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_NewGameNightmare = new ModPropOption(Rayman3_Text.Mod_NewGameNightmare, Rayman3_Text.Mod_NewGameNightmareDesc);
        [ExecutesMods(typeof(Ray3_RemoveIntroVideo))] [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RemoveIntroVideos = new ModPropOption(1, Rayman3_Text.Mod_RemoveIntroVideo, Rayman3_Text.Mod_RemoveIntroVideoDesc);

    }
}
