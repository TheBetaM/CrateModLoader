using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash1;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash1.TrilogyCommon;
//using CrateModLoader.GameSpecific.Crash1.Mods;

namespace CrateModLoader.GameSpecific.Crash1
{
    [ModCategory(1), ModMenuOnly]
    static class Crash1_Props_Misc
    {
        // less used
        [ModHidden]
        public static ModPropOption Option_AllEnemiesMissing = new ModPropOption("All Enemies Removed", ""); //todo
        [ExecutesMods(typeof(CrashTri_Crates_AllBlank))]
        public static ModPropOption Option_AllCratesBlank = new ModPropOption(CrashTri_Text.Mod_AllCratesBlank, CrashTri_Text.Mod_AllCratesBlankDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CratesIntoWumpa))]
        public static ModPropOption Option_AllCratesWumpa = new ModPropOption(CrashTri_Text.Mod_AllCratesWumpa, CrashTri_Text.Mod_AllCratesWumpaDesc);
        [ExecutesMods(typeof(CrashTri_Rand_CameraFOV))]
        public static ModPropOption Option_RandCameraFOV = new ModPropOption(Crash1_Text.Rand_CameraFOV, Crash1_Text.Rand_CameraFOVDesc);
        [ExecutesMods(typeof(CrashTri_Scenery_Invisible))]
        public static ModPropOption Option_InvisibleWorld = new ModPropOption("Invisible World (Beta)", "The scenery is invisible.");

        // unfinished
        [ExecutesMods(typeof(CrashTri_Rand_PantsColor))] [ModHidden]
        public static ModPropColor Prop_PantsColor = new ModPropColor(new int[4] { 0, 0, 255, 255 }, "Pants Color", "");
    }
}
