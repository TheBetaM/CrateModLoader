using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public static class Rayman3_Props_Main
    {
        public static ModPropOption Option_RandLevelOrderAll = new ModPropOption(new Ray3_Rand_LevelOrder(false), Rayman3_Text.Rand_LevelOrder2, Rayman3_Text.Rand_LevelOrder2Desc);
        public static ModPropOption Option_RandLevelOrder = new ModPropOption(new Ray3_Rand_LevelOrder(true));
        public static ModPropOption Option_RandOutfitVisuals = new ModPropOption(new Ray3_Rand_OutfitVisuals());
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandOutfitColors = new ModPropOption(new Ray3_Rand_OutfitColors());
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandCopterColors = new ModPropOption(new Ray3_Rand_CopterColors());
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandHUDColors = new ModPropOption(new Ray3_Rand_HUDColors());
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RandWorldColors = new ModPropOption(new Ray3_Rand_WorldColors())
        { Hidden = true, }; // works in some levels, broken in others
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_NewGameNightmare = new ModPropOption(new Ray3_Start2DNightmare());
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModPropOption Option_RemoveIntroVideos = new ModPropOption(new Ray3_RemoveIntroVideo(), 1);

    }
}
