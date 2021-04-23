using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public static class TWOC_Props_Main
    {
        public static ModPropOption Option_RandCrates = new ModPropOption(new TWOC_Rand_Crates());
        public static ModPropOption Option_RandCratesRemoved = new ModPropOption(new TWOC_Rand_CratesRemoved())
        { Hidden = true, };
        public static ModPropOption Option_RandEnemyPaths = new ModPropOption(new TWOC_Rand_EnemyPaths());
        public static ModPropOption Option_RandEnemiesRemoved = new ModPropOption(new TWOC_Rand_EnemiesRemoved());
        [ModAllowedConsoles(ConsoleMode.GCN, ConsoleMode.XBOX)]
        public static ModPropOption Option_RandMusic = new ModPropOption(new TWOC_Rand_Music()); //works on xbox

        public static ModPropOption Option_RandWumpaCrates = new ModPropOption(new TWOC_Rand_WumpaIntoCrates())
        { Hidden = true, }; //todo: new box positions are off
        public static ModPropOption Option_RandEnemyCrates = new ModPropOption("Random Enemies Are Random Crates", "Enemies are randomly turned into random cratesin each level. The box counter is adjusted accordingly.")
        { Hidden = true, }; //todo
        [ModMenuOnly]
        public static ModPropOption Option_AllEnemyCrates = new ModPropOption("All Enemies Are Random Crates", "The box counter is adjusted accordingly.")
        { Hidden = true, }; //todo
        public static ModPropOption Option_RandLevelOrder = new ModPropOption(new TWOC_Rand_LevelOrder()) //todo, unbeatable levels, enemies not spawning, etc.
        { Hidden = true, };
        public static ModPropOption Option_SphereLevelsOnFoot = new ModPropOption(new TWOC_SwapLevels()) //todo: camera!!!
        { Hidden = true, };
    }
}
