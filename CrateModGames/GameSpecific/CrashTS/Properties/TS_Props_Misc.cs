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
        [ExecutesMods(typeof(TS_GreyscaleWorld))] [ModAllowedConsoles(ConsoleMode.PS2)]
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(Twins_Text.Mod_GreyscaleWorld, Twins_Text.Mod_GreyscaleWorldDesc);
        [ExecutesMods(typeof(TS_Rand_PantsColor))]
        public static ModPropColor Prop_PantsColor = new ModPropColor(new int[4] { 0, 0, 255, 255 }, Twins_Text.Prop_PantsColor, Twins_Text.Prop_PantsColorDesc);
        
        [ExecutesMods(typeof(TS_ChangeStartingChunk))]
        public static ModPropString StartingChunk = new ModPropString(@"Levels\Earth\Hub\Beach", 0x17);
        [ExecutesMods(typeof(TS_ChangeCreditsChunk))]
        public static ModPropString CreditsChunk = new ModPropString(@"Levels\Ice\Hub\LabExt", 0x22); //0x1C
        
        [ExecutesMods(typeof(TS_SwapStartCreditsSpawn))]
        public static ModPropOption Option_SwapStartAndCreditsChunk = new ModPropOption(Twins_Text.Mod_SwapStartAndCreditsChunk, Twins_Text.Mod_SwapStartAndCreditsChunkDesc);
        [ExecutesMods(typeof(TS_StartSpawnCredits))]
        public static ModPropOption Option_StartAndCreditsSpawn = new ModPropOption(Twins_Text.Mod_StartAndCreditsSpawn, Twins_Text.Mod_StartAndCreditsSpawnDesc);

        //public static ModPropString ArchiveName = new ModPropString("Crash");
        //public static ModPropString UnsafeStartingChunk = new ModPropString(@"Levels\Earth\Hub\Beach", 0x2D);
    }
}
