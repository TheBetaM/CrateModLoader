using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashTS;
using CrateModLoader.GameSpecific.CrashTS.Mods;

namespace CrateModLoader.GameSpecific.CrashTS
{
    [ModCategory((int)ModProps.Misc), ModMenuOnly]
    static class TS_Props_Misc
    {

        // Misc. props
        [ModAllowedConsoles(ConsoleMode.PS2)]
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(new TS_GreyscaleWorld());
        public static ModPropColor Prop_PantsColor = new ModPropColor(new TS_Rand_PantsColor(), new int[4] { 0, 0, 255, 255 }, Twins_Text.Prop_PantsColor, Twins_Text.Prop_PantsColorDesc);
        //public static ModPropString ArchiveName = new ModPropString("Crash");
        public static ModPropString StartingChunk = new ModPropString(new TS_ChangeStartingChunk(), @"Levels\Earth\Hub\Beach", 0x17);
        public static ModPropString CreditsChunk = new ModPropString(new TS_ChangeCreditsChunk(), @"Levels\Ice\Hub\LabExt", 0x22); //0x1C
        //public static ModPropString UnsafeStartingChunk = new ModPropString(@"Levels\Earth\Hub\Beach", 0x2D);
        public static ModPropOption Option_SwapStartAndCreditsChunk = new ModPropOption(new TS_SwapStartCreditsSpawn());
        public static ModPropOption Option_StartAndCreditsSpawn = new ModPropOption(new TS_StartSpawnCredits());
    }
}
