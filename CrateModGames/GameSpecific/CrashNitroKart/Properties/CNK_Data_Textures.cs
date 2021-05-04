using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    [ModCategory((int)ModProps.Textures)]
    public static class CNK_Data_Textures
    {

        [ModAllowedConsoles(ConsoleMode.PS2)]
        public static ModProp_TextureFile Texture_HudIcons = new ModProp_TextureFile(@"ps2\gfx\hud\8-bit_hud.png", "HUD Icons", "If the file size is too large, some tracks may not load! (original under 100 kB)");
        [ModAllowedConsoles(ConsoleMode.GCN)]
        public static ModProp_TextureFile Texture_HudIconsGCN = new ModProp_TextureFile(@"gcn\gfx\hud\8-bit_hud.png", "HUD Icons", "If the file size is too large, some tracks may not load! (original under 100 kB)");
        [ModAllowedConsoles(ConsoleMode.XBOX)]
        public static ModProp_TextureFile Texture_HudIconsXBOX = new ModProp_TextureFile(@"xbox\gfx\hud\8-bit_hud.png", "HUD Icons", "If the file size is too large, some tracks may not load! (original under 100 kB)");

        public static ModProp_TextureFile Texture_Font = new ModProp_TextureFile(@"common\fonts\all_fonts.png", "Font", "May not affect all menus.");

        public static ModProp_TextureFile Texture_Load_Arena1 = new ModProp_TextureFile(@"common\load\arena1.png", "Loading - Arena 1", "");
        public static ModProp_TextureFile Texture_Load_Arena2 = new ModProp_TextureFile(@"common\load\arena2.png", "Loading - Arena 2", "");
        public static ModProp_TextureFile Texture_Load_Arena3 = new ModProp_TextureFile(@"common\load\arena3.png", "Loading - Arena 3", "");
        public static ModProp_TextureFile Texture_Load_Arena4 = new ModProp_TextureFile(@"common\load\arena4.png", "Loading - Arena 4", "");
        public static ModProp_TextureFile Texture_Load_Arena5 = new ModProp_TextureFile(@"common\load\arena5.png", "Loading - Arena 5", "");
        public static ModProp_TextureFile Texture_Load_Barin1 = new ModProp_TextureFile(@"common\load\barin1.png", "Loading - Barin 1", "");
        public static ModProp_TextureFile Texture_Load_Barin2 = new ModProp_TextureFile(@"common\load\barin2.png", "Loading - Barin 2", "");
        public static ModProp_TextureFile Texture_Load_Barin3 = new ModProp_TextureFile(@"common\load\barin3.png", "Loading - Barin 3", "");
        public static ModProp_TextureFile Texture_Load_Citadel = new ModProp_TextureFile(@"common\load\citadel.png", "Loading - Velo's Citadel", "");
        public static ModProp_TextureFile Texture_Load_Earth1 = new ModProp_TextureFile(@"common\load\earth1.png", "Loading - Terra 1", "");
        public static ModProp_TextureFile Texture_Load_Earth2 = new ModProp_TextureFile(@"common\load\earth2.png", "Loading - Terra 2", "");
        public static ModProp_TextureFile Texture_Load_Earth3 = new ModProp_TextureFile(@"common\load\earth3.png", "Loading - Terra 3", "");
        public static ModProp_TextureFile Texture_Load_Fenom1 = new ModProp_TextureFile(@"common\load\fenom1.png", "Loading - Fenomena 1", "");
        public static ModProp_TextureFile Texture_Load_Fenom2 = new ModProp_TextureFile(@"common\load\fenom2.png", "Loading - Fenomena 2", "");
        public static ModProp_TextureFile Texture_Load_Fenom3 = new ModProp_TextureFile(@"common\load\fenom3.png", "Loading - Fenomena 3", "");
        public static ModProp_TextureFile Texture_Load_Hub1 = new ModProp_TextureFile(@"common\load\hub1.png", "Loading - Hub 1", "");
        public static ModProp_TextureFile Texture_Load_Hub2 = new ModProp_TextureFile(@"common\load\hub2.png", "Loading - Hub 2", "");
        public static ModProp_TextureFile Texture_Load_Hub3 = new ModProp_TextureFile(@"common\load\hub3.png", "Loading - Hub 3", "");
        public static ModProp_TextureFile Texture_Load_Hub4 = new ModProp_TextureFile(@"common\load\hub4.png", "Loading - Hub 4", "");
        public static ModProp_TextureFile Texture_Load_Hub5 = new ModProp_TextureFile(@"common\load\hub5.png", "Loading - Hub 5", "");
        public static ModProp_TextureFile Texture_Load_Teknee1 = new ModProp_TextureFile(@"common\load\teknee1.png", "Loading - Teknee 1", "");
        public static ModProp_TextureFile Texture_Load_Teknee2 = new ModProp_TextureFile(@"common\load\teknee2.png", "Loading - Teknee 2", "");
        public static ModProp_TextureFile Texture_Load_Teknee3 = new ModProp_TextureFile(@"common\load\teknee3.png", "Loading - Teknee 3", "");
        public static ModProp_TextureFile Texture_Load_Trophy = new ModProp_TextureFile(@"common\load\trophy.png", "Loading - Podium", "");
        public static ModProp_TextureFile Texture_Load_Velorace = new ModProp_TextureFile(@"common\load\velorace.png", "Loading - Hyper Spaceway", "");
        public static ModProp_TextureFile Texture_Load_MainMenu01 = new ModProp_TextureFile(@"common\load\mainmenu01.png", "Loading - Main Menu 01", "");
        public static ModProp_TextureFile Texture_Load_MainMenu02 = new ModProp_TextureFile(@"common\load\mainmenu02.png", "Loading - Main Menu 02", "");
        public static ModProp_TextureFile Texture_Load_MainMenu03 = new ModProp_TextureFile(@"common\load\mainmenu03.png", "Loading - Main Menu 03", "");
        public static ModProp_TextureFile Texture_Load_MainMenu04 = new ModProp_TextureFile(@"common\load\mainmenu04.png", "Loading - Main Menu 04", "");
        public static ModProp_TextureFile Texture_Load_MainMenu05 = new ModProp_TextureFile(@"common\load\mainmenu05.png", "Loading - Main Menu 05", "");
        public static ModProp_TextureFile Texture_Load_MainMenu06 = new ModProp_TextureFile(@"common\load\mainmenu06.png", "Loading - Main Menu 06", "");
        public static ModProp_TextureFile Texture_Load_MainMenu07 = new ModProp_TextureFile(@"common\load\mainmenu07.png", "Loading - Main Menu 07", "");
        public static ModProp_TextureFile Texture_Load_MainMenu08 = new ModProp_TextureFile(@"common\load\mainmenu08.png", "Loading - Main Menu 08", "");
        public static ModProp_TextureFile Texture_Load_MainMenu09 = new ModProp_TextureFile(@"common\load\mainmenu09.png", "Loading - Main Menu 09", "");
        public static ModProp_TextureFile Texture_Load_MainMenu10 = new ModProp_TextureFile(@"common\load\mainmenu10.png", "Loading - Main Menu 10", "");
        public static ModProp_TextureFile Texture_Load_MainMenu11 = new ModProp_TextureFile(@"common\load\mainmenu11.png", "Loading - Main Menu 11", "");
        public static ModProp_TextureFile Texture_Load_MainMenu12 = new ModProp_TextureFile(@"common\load\mainmenu12.png", "Loading - Main Menu 12", "");
        public static ModProp_TextureFile Texture_Load_MainMenu13 = new ModProp_TextureFile(@"common\load\mainmenu13.png", "Loading - Main Menu 13", "");

    }
}
