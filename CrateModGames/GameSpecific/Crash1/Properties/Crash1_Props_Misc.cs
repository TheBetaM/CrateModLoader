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
        public static ModPropOption Option_AllEnemiesMissing = new ModPropOption("All Enemies Removed", "") { Hidden = true, }; //todo
        public static ModPropOption Option_AllCratesBlank = new ModPropOption(new CrashTri_Crates_AllBlank());
        public static ModPropOption Option_AllCratesWumpa = new ModPropOption(new CrashTri_Rand_CratesIntoWumpa(false));
        public static ModPropOption Option_RandCameraFOV = new ModPropOption(new CrashTri_Rand_CameraFOV(true), Crash1_Text.Rand_CameraFOV, Crash1_Text.Rand_CameraFOVDesc);
        public static ModPropOption Option_InvisibleWorld = new ModPropOption(new CrashTri_Scenery_Invisible());

        // unfinished
        public static ModPropColor Prop_PantsColor = new ModPropColor(new CrashTri_Rand_PantsColor(false), new int[4] { 0, 0, 255, 255 }, "Pants Color", "")
        { Hidden = true };
    }
}
