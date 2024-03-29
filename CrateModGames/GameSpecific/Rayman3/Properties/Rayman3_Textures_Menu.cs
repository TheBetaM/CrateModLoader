﻿using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Rayman3;
namespace CrateModLoader.GameSpecific.Rayman3
{
    [ModCategory((int)ModProps.Textures_Menu), ModAllowedConsoles(ConsoleMode.GCN)]
    public static class Rayman3_Textures_Menu
    {
        public static ModProp_TextureFile Texture_Menu_Overlay = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.png", "Pause Menu Overlay", "");
        public static ModProp_TextureFile Texture_Menu_Icons = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm1.png", "Pause Menu Sprites", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level1 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm2.png", "Icon - The Fairy Council", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level2 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm3.png", "Icon - Clearleaf Forest", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level3 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm4.png", "Icon - The Bog of Murk", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level4 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm5.png", "Icon - The Land of the Livid Dead", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level5 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm6.png", "Icon - The Desert of the Knaaren", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level6 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm7.png", "Icon - The Longest Shortcut", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level7 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm8.png", "Icon - The Summit Beyond the Clouds", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level8 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm9.png", "Icon - Hoodlum Headquarters", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Level9 = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm10.png", "Icon - The Tower of the Leptys", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Lum = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm11.png", "Icon - Black Lum", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Options = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm12.png", "Icon - Options", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Rayman = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm13.png", "Icon - Rayman", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Murfy = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm14.png", "Icon - Murfy", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Videos = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm15.png", "Icon - Videos", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Camera = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm16.png", "Icon - Camera", "");
        public static ModProp_TextureFile Texture_Menu_Icon_Plum = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm17.png", "Icon - Minigames", "");
        public static ModProp_TextureFile Texture_Menu_Icons_Videos = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm18.png", "Video Icons", "");
        public static ModProp_TextureFile Texture_Menu_Icons_Misc = new ModProp_TextureFile(@"GAMEDATABIN\menu.tpl.mm19.png", "Misc. Icons", "");
    }
}