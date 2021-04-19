using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public static class Rayman3_Props_Main
    {
        public static ModPropOption Option_RandLevelOrderAll = new ModPropOption(Rayman3_Text.Rand_LevelOrder2, Rayman3_Text.Rand_LevelOrder2Desc);
        public static ModPropOption Option_RandLevelOrder = new ModPropOption(Rayman3_Text.Rand_LevelOrder, Rayman3_Text.Rand_LevelOrderDesc);
        public static ModPropOption Option_RandOutfitVisuals = new ModPropOption(Rayman3_Text.Rand_OutfitVisuals, Rayman3_Text.Rand_OutfitVisualsDesc);
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandOutfitColors = new ModPropOption(Rayman3_Text.Rand_OutfitColors, Rayman3_Text.Rand_OutfitColorsDesc);
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandCopterColors = new ModPropOption(Rayman3_Text.Rand_CopterColors, Rayman3_Text.Rand_CopterColorsDesc);
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandHUDColors = new ModPropOption(Rayman3_Text.Rand_HUDColors, Rayman3_Text.Rand_HUDColorsDesc);
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandWorldColors = new ModPropOption(Rayman3_Text.Rand_WorldColors, Rayman3_Text.Rand_WorldColorsDesc)
        { Hidden = true, }; // works in some levels, broken in others
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_NewGameNightmare = new ModPropOption(Rayman3_Text.Mod_NewGameNightmare, Rayman3_Text.Mod_NewGameNightmareDesc);
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RemoveIntroVideos = new ModPropOption(1, Rayman3_Text.Mod_RemoveIntroVideo, Rayman3_Text.Mod_RemoveIntroVideoDesc);

    }
}
