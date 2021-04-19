using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public static class TWOC_Props_Main
    {
        public static ModPropOption Option_RandCrates = new ModPropOption("Randomize Wooden Crates", "The types of wooden crates are randomized.");
        public static ModPropOption Option_RandCratesRemoved = new ModPropOption("Random Crates Removed", "Wooden crates are randomly removed in each level. The box counter is adjusted accordingly.")
        { Hidden = true, };
        public static ModPropOption Option_RandEnemyPaths = new ModPropOption("Randomize Enemy Paths", "Reverses paths of random enemies.");
        public static ModPropOption Option_RandEnemiesRemoved = new ModPropOption("Random Enemies Removed", "Enemies are randomly removed in each level.");
        [ModAllowedConsoles(ConsoleMode.GCN, ConsoleMode.XBOX)]
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "Music tracks are shuffled around."); //works on xbox

        public static ModPropOption Option_RandWumpaCrates = new ModPropOption("Random Wumpa Are Random Crates", "Wumpas are randomly turned into crates in each level. The box counter is adjusted accordingly.")
        { Hidden = true, }; //todo: new box positions are off
        public static ModPropOption Option_RandEnemyCrates = new ModPropOption("Random Enemies Are Random Crates", "Enemies are randomly turned into random cratesin each level. The box counter is adjusted accordingly.")
        { Hidden = true, }; //todo
        [ModMenuOnly]
        public static ModPropOption Option_AllEnemyCrates = new ModPropOption("All Enemies Are Random Crates", "The box counter is adjusted accordingly.")
        { Hidden = true, }; //todo
        public static ModPropOption Option_RandLevelOrder = new ModPropOption("Randomize Level Order", "") //todo, unbeatable levels, enemies not spawning, etc.
        { Hidden = true, };
        public static ModPropOption Option_SphereLevelsOnFoot = new ModPropOption("Atlasphere Levels On Foot", "") //todo: camera!!!
        { Hidden = true, };
    }
}
