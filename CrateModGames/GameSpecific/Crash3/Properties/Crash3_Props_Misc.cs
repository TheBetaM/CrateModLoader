﻿using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModGames.GameSpecific.Crash3;
using CrateModLoader.GameSpecific.Crash1.TrilogyCommon;
//using CrateModLoader.GameSpecific.Crash3.Mods;

namespace CrateModLoader.GameSpecific.Crash3
{
    [ModCategory(1), ModMenuOnly]
    static class Crash3_Props_Misc
    {
        [ExecutesMods(typeof(CrashTri_Rand_PantsColor))]
        public static ModPropColor Prop_PantsColor = new ModPropColor(new int[4] { 0, 0, 255, 255 }, CrashTri_Text.Prop_PantsColor, CrashTri_Text.Prop_PantsColorDesc);

        // less used
        [ExecutesMods(typeof(CrashTri_Rand_AshedCrates))]
        public static ModPropOption Option_RandCratesAshed = new ModPropOption("Random Crate Types Are Covered Up", "Random crate types are covered up to look the same on the outside in each level.");
        [ExecutesMods(typeof(CrashTri_Rand_WoodenCrates))]
        public static ModPropOption Option_RandCrates = new ModPropOption(CrashTri_Text.Rand_Crates, CrashTri_Text.Rand_CratesDesc);
        [ExecutesMods(typeof(CrashTri_Rand_RemoveEnemies))]
        public static ModPropOption Option_AllEnemiesMissing = new ModPropOption(CrashTri_Text.Mod_RemoveEnemies, CrashTri_Text.Mod_RemoveEnemies);
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

        //unfinished
        [ExecutesMods(typeof(CrashTri_Scenery_Invisible))] [ModHidden]
        public static ModPropOption Option_InvisibleWorld = new ModPropOption("Invisible World (Beta)", "The scenery is invisible.");
        [ExecutesMods(typeof(CrashTri_Rand_EnemyCrates))] [ModHidden]
        public static ModPropOption Option_AllEnemiesAreCrates = new ModPropOption(Crash3_Text.Mod_EnemyCrates, Crash3_Text.Mod_EnemyCratesDesc) { Hidden = true };
    }
}
