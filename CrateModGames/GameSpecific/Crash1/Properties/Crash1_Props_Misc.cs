using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Crash1;
//using CrateModLoader.GameSpecific.Crash1.Mods;

namespace CrateModLoader.GameSpecific.Crash1
{
    [ModCategory(1), ModMenuOnly]
    static class Crash1_Props_Misc
    {
        // less used
        public static ModPropOption Option_AllEnemiesMissing = new ModPropOption("All Enemies Removed", "") { Hidden = true, };
        public static ModPropOption Option_AllCratesBlank = new ModPropOption(CrashTri_Text.Mod_AllCratesBlank, CrashTri_Text.Mod_AllCratesBlankDesc);
        public static ModPropOption Option_AllCratesWumpa = new ModPropOption(Crash1_Text.Mod_AllCratesWumpa, Crash1_Text.Mod_AllCratesWumpaDesc);
        public static ModPropOption Option_RandCameraFOV = new ModPropOption(Crash1_Text.Rand_CameraFOV, Crash1_Text.Rand_CameraFOVDesc);
        public static ModPropOption Option_InvisibleWorld = new ModPropOption("Invisible World (Beta)", "The scenery is invisible.");

        // unfinished
        public static ModPropColor Prop_PantsColor = new ModPropColor(new int[4] { 0, 0, 255, 255 }, "Pants Color", "")
        { Hidden = true };
    }
}
