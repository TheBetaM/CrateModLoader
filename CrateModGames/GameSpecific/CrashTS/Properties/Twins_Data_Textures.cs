using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashTS.Mods;

namespace CrateModLoader.GameSpecific.CrashTS
{
    [ModCategory((int)ModProps.Textures), ModAllowedConsoles(ConsoleMode.PS2)] [ExecutesMods(typeof(TS_CustomTextureHandle))]
    public static class Twins_Data_Textures
    {

        public static ModProp_TextureFile Texture_Icons = new ModProp_TextureFile(false, "Icons", "");

        public static ModProp_TextureFile Texture_Titles_Crash = new ModProp_TextureFile(false, "Game Logo", "");
        public static ModProp_TextureFile Texture_Titles_Hub01 = new ModProp_TextureFile(false, "Logo - N.Sanity Island", "");
        public static ModProp_TextureFile Texture_Titles_Hub02 = new ModProp_TextureFile(false, "Logo - Iceberg Lab", "");
        public static ModProp_TextureFile Texture_Titles_Hub03 = new ModProp_TextureFile(false, "Logo - Academy Of Evil", "");
        public static ModProp_TextureFile Texture_Titles_Hub04 = new ModProp_TextureFile(false, "Logo - Twinsanity Island", "");
        public static ModProp_TextureFile Texture_Titles_Level01 = new ModProp_TextureFile(false, "Logo - Jungle Bungle", "");
        public static ModProp_TextureFile Texture_Titles_Level02 = new ModProp_TextureFile(false, "Logo - Cavern Catastrophe", "");
        public static ModProp_TextureFile Texture_Titles_Level03 = new ModProp_TextureFile(false, "Logo - Totem Hokum", "");
        public static ModProp_TextureFile Texture_Titles_Level04 = new ModProp_TextureFile(false, "Logo - Ice Climb", "");
        public static ModProp_TextureFile Texture_Titles_Level05 = new ModProp_TextureFile(false, "Logo - Slip Slide Icecapades", "");
        public static ModProp_TextureFile Texture_Titles_Level06 = new ModProp_TextureFile(false, "Logo - Hi Seas Hijinks", "");
        public static ModProp_TextureFile Texture_Titles_Level07 = new ModProp_TextureFile(false, "Logo - Gone A Bit Coco", "");
        public static ModProp_TextureFile Texture_Titles_Level08 = new ModProp_TextureFile(false, "Logo - Boiler Room Doom", "");
        public static ModProp_TextureFile Texture_Titles_Level09 = new ModProp_TextureFile(false, "Logo - Classroom Chaos", "");
        public static ModProp_TextureFile Texture_Titles_Level10 = new ModProp_TextureFile(false, "Logo - Rooftop Rampage", "");
        public static ModProp_TextureFile Texture_Titles_Level11 = new ModProp_TextureFile(false, "Logo - Rockslide Rumble", "");
        public static ModProp_TextureFile Texture_Titles_Level12 = new ModProp_TextureFile(false, "Logo - Bandicoot Pursuit", "");
        public static ModProp_TextureFile Texture_Titles_Level13 = new ModProp_TextureFile(false, "Logo - Ant Agony", "");

        public static ModProp_TextureFile Texture_Loading_01 = new ModProp_TextureFile(false, "Loading - 01", "");
        public static ModProp_TextureFile Texture_Loading_02 = new ModProp_TextureFile(false, "Loading - 02", "");
        public static ModProp_TextureFile Texture_Loading_03 = new ModProp_TextureFile(false, "Loading - 03", "");

        public static ModProp_TextureFile Texture_Credits = new ModProp_TextureFile(false, "Credits", "");

        public static ModProp_TextureFile Texture_Legal = new ModProp_TextureFile(false, "Legal Display", "");

        public static ModProp_TextureFile Texture_GameOver_Crash = new ModProp_TextureFile(false, "Game Over - Crash", "");
        public static ModProp_TextureFile Texture_GameOver_Cortex = new ModProp_TextureFile(false, "Game Over - Cortex", "");
        public static ModProp_TextureFile Texture_GameOver_Nina = new ModProp_TextureFile(false, "Game Over - Nina", "");
        public static ModProp_TextureFile Texture_GameOver_Mecha = new ModProp_TextureFile(false, "Game Over - Mecha", "");
        public static ModProp_TextureFile Texture_GameOver_CrashAndCortex = new ModProp_TextureFile(false, "Game Over - Crash And Cortex", "");

        [ModHidden]
        public static ModProp_TextureFile Texture_Decals = new ModProp_TextureFile(false, "Decals", ""); // changing it does nothing, game seems to load it from default instead?

        public static ModProp_TextureFile Texture_Font_Arial = new ModProp_TextureFile(false, "Font - Arial (Debug)", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Font_Crash = new ModProp_TextureFile(false, "Font - Crash (NTSC-U)", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Font_CrashPAL = new ModProp_TextureFile(false, "Font - Crash (PAL)", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Font_CrashJPN = new ModProp_TextureFile(false, "Font - Crash (NTSC-J)", "");

    }
}
