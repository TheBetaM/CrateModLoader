using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash2;
using CrateModGames.GameSpecific.Crash1;
//using CrateModLoader.GameSpecific.Crash2.Mods;

namespace CrateModLoader.GameSpecific.Crash2
{
    [ModCategory(1), ModMenuOnly]
    static class Crash2_Props_Misc
    {
        public static ModPropColor Prop_PantsColor = new ModPropColor(new int[4] { 0, 0, 255, 255 }, CrashTri_Text.Prop_PantsColor, CrashTri_Text.Prop_PantsColorDesc);

        //less used
        public static ModPropOption Option_RandCrates = new ModPropOption(CrashTri_Text.Rand_WoodenCrates, CrashTri_Text.Rand_WoodenCratesDesc);
        public static ModPropOption Option_RandEnemyPaths = new ModPropOption(CrashTri_Text.Rand_EnemyPaths, CrashTri_Text.Rand_EnemyPathsDesc);
        public static ModPropOption Option_AllEnemiesMissing = new ModPropOption(CrashTri_Text.Mod_RemoveEnemies, CrashTri_Text.Mod_RemoveEnemiesDesc);
        public static ModPropOption Option_RandObjCol = new ModPropOption(CrashTri_Text.Rand_ObjCol, CrashTri_Text.Rand_ObjColDesc);
        public static ModPropOption Option_RandObjTex = new ModPropOption(CrashTri_Text.Rand_ObjTex, CrashTri_Text.Rand_ObjTexDesc);
        public static ModPropOption Option_UncoloredObj = new ModPropOption(CrashTri_Text.Mod_GreyscaleObjects, CrashTri_Text.Mod_GreyscaleObjectsDesc);
        public static ModPropOption Option_AllCratesBlank = new ModPropOption(CrashTri_Text.Mod_AllCratesBlank, CrashTri_Text.Mod_AllCratesBlankDesc);
        public static ModPropOption Option_AllCratesWumpa = new ModPropOption(CrashTri_Text.Mod_AllCratesWumpa, CrashTri_Text.Mod_AllCratesWumpaDesc);
        public static ModPropOption Option_RandCameraFOV = new ModPropOption(CrashTri_Text.Rand_CameraFOV, CrashTri_Text.Rand_CameraFOVDesc);
        public static ModPropOption Option_InvisibleWorld = new ModPropOption("Invisible World (Beta)", "The scenery is invisible.");

        //unfinished
        public static ModPropOption Option_AllEnemiesAreCrates = new ModPropOption(Crash2_Text.Rand_AllEnemiesCrates, Crash2_Text.Rand_AllEnemiesCratesDesc) { Hidden = true, };

    }
}
