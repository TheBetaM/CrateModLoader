using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public static class WormsForts_Props_Main
    {
        [ExecutesMods(typeof(WF_Rand_Epoch))]
        public static ModPropOption Option_Rand_Epoch = new ModPropOption("Randomize Level Epochs", "Randomize which era the level is set in.");
        [ExecutesMods(typeof(WF_DM_Rand_ShuffleBuildPoints))]
        public static ModPropOption Option_DM_Rand_ShuffleBuildPoints = new ModPropOption("Deathmatch: Randomize Building Locations", "Randomize the starting locations of Strongholds and neutral buildings in Deathmatch challenges.");
        [ExecutesMods(typeof(WF_DM_Rand_LevelOrder))] [ModHidden]
        public static ModPropOption Option_DM_Rand_LevelOrder = new ModPropOption("Deathmatch: Randomize Level Order", "Randomize the level order of The Trials.");
        [ExecutesMods(typeof(WF_Rand_VictLocations))]
        public static ModPropOption Option_Rand_VictLocations = new ModPropOption("Deathmatch/Multiplayer: Randomize Victory Locations", "Randomize which build locations are victory locations.");
        [ExecutesMods(typeof(WF_RemoveVictLocations))]
        public static ModPropOption Option_RemoveVictLocations = new ModPropOption("Deathmatch/Multiplayer: Remove Victory Locations", "Remove all victory locations from deathmatch and multiplayer levels.");
        [ExecutesMods(typeof(WF_FortWeaponsAnywhere))]
        public static ModPropOption Option_FortWeaponsAnywhere = new ModPropOption("Use Fort Weapons Anywhere", "Weapons that require being on buildings no longer do so.");
        [ExecutesMods(typeof(WF_RemoveGodWormAnimations))]
        public static ModPropOption Option_RemoveGodWormAnims = new ModPropOption("Remove God Worm Animations", "Weapons that show the extended animations no longer do so.");
        [ExecutesMods(typeof(WF_Rand_WorldPalette))]
        public static ModPropOption Option_Rand_WorldPalette = new ModPropOption("Randomize World Palette", "Randomize the world's palette in every level.");
        [ExecutesMods(typeof(WF_Rand_Skybox))]
        public static ModPropOption Option_Rand_Skybox = new ModPropOption("Randomize Skyboxes", "Randomize the skybox type in each level.");
        [ExecutesMods(typeof(WF_Rand_WaterType))]
        public static ModPropOption Option_Rand_WaterType = new ModPropOption("Randomize Water Types", "Randomize the death plane type in each level.");
        [ExecutesMods(typeof(WF_TestMod))] [ModHidden]
        public static ModPropOption Option_TestMod = new ModPropOption("Test Mod", "");
        [ExecutesMods(typeof(WF_Metadata))] [ModHidden]
        public static ModPropOption Option_Metadata = new ModPropOption(1);
    }
}
