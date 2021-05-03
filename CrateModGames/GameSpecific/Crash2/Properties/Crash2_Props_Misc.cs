using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash2;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash1.TrilogyCommon;
//using CrateModLoader.GameSpecific.Crash2.Mods;

namespace CrateModLoader.GameSpecific.Crash2
{
    [ModCategory(1), ModMenuOnly]
    static class Crash2_Props_Misc
    {
        [ExecutesMods(typeof(CrashTri_Rand_PantsColor))]
        public static ModPropColor Prop_PantsColor = new ModPropColor(new int[4] { 0, 0, 255, 255 }, CrashTri_Text.Prop_PantsColor, CrashTri_Text.Prop_PantsColorDesc);

        //less used
        [ExecutesMods(typeof(CrashTri_Rand_WoodenCrates))]
        public static ModPropOption Option_RandCrates = new ModPropOption(CrashTri_Text.Rand_Crates, CrashTri_Text.Rand_CratesDesc);
        [ExecutesMods(typeof(CrashTri_Rand_EnemyPaths))]
        public static ModPropOption Option_RandEnemyPaths = new ModPropOption(CrashTri_Text.Rand_EnemyPaths, CrashTri_Text.Rand_EnemyPathsDesc);
        [ExecutesMods(typeof(CrashTri_Rand_RemoveEnemies))]
        public static ModPropOption Option_AllEnemiesMissing = new ModPropOption(CrashTri_Text.Mod_RemoveEnemies, CrashTri_Text.Mod_RemoveEnemiesDesc);
        [ExecutesMods(typeof(CrashTri_Rand_ObjectColors))]
        public static ModPropOption Option_RandObjCol = new ModPropOption(CrashTri_Text.Rand_ObjCol, CrashTri_Text.Rand_ObjColDesc);
        [ExecutesMods(typeof(CrashTri_Rand_ObjectTextures))]
        public static ModPropOption Option_RandObjTex = new ModPropOption(CrashTri_Text.Rand_ObjTex, CrashTri_Text.Rand_ObjTexDesc);
        [ExecutesMods(typeof(CrashTri_Objects_Greyscale))]
        public static ModPropOption Option_UncoloredObj = new ModPropOption(CrashTri_Text.Mod_GreyscaleObjects, CrashTri_Text.Mod_GreyscaleObjectsDesc);
        [ExecutesMods(typeof(CrashTri_Crates_AllBlank))]
        public static ModPropOption Option_AllCratesBlank = new ModPropOption(CrashTri_Text.Mod_AllCratesBlank, CrashTri_Text.Mod_AllCratesBlankDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CratesIntoWumpa))]
        public static ModPropOption Option_AllCratesWumpa = new ModPropOption(CrashTri_Text.Mod_AllCratesWumpa, CrashTri_Text.Mod_AllCratesWumpaDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CameraFOV))]
        public static ModPropOption Option_RandCameraFOV = new ModPropOption(CrashTri_Text.Rand_CameraFOV, CrashTri_Text.Rand_CameraFOVDesc);
        [ExecutesMods(typeof(CrashTri_Scenery_Invisible))]
        public static ModPropOption Option_InvisibleWorld = new ModPropOption("Invisible World (Beta)", "The scenery is invisible.");

        //unfinished
        [ExecutesMods(typeof(CrashTri_Rand_EnemyCrates))] [ModHidden]
        public static ModPropOption Option_AllEnemiesAreCrates = new ModPropOption(Crash2_Text.Rand_AllEnemiesCrates, Crash2_Text.Rand_AllEnemiesCratesDesc);

    }
}
