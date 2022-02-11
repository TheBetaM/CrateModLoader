using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Rayman3;
namespace CrateModLoader.GameSpecific.Rayman3
{
    [ModCategory((int)ModProps.Textures_Loading), ModAllowedConsoles(ConsoleMode.GCN)]
    public static class Rayman3_Textures_Loading
    {
        public static ModProp_TextureFile Texture_Load_Gear = new ModProp_TextureFile(@"LSBIN\lodmeca.tpl.png", "Loading - Gear", "");
        public static ModProp_TextureFile Texture_Load_01 = new ModProp_TextureFile(@"LSBIN\lodps2_01.tpl.png", "Loading - 01", "");
        public static ModProp_TextureFile Texture_Load_02 = new ModProp_TextureFile(@"LSBIN\lodps2_02.tpl.png", "Loading - 02", "");
        public static ModProp_TextureFile Texture_Load_03 = new ModProp_TextureFile(@"LSBIN\lodps2_03.tpl.png", "Loading - 03", "");
        public static ModProp_TextureFile Texture_Load_04 = new ModProp_TextureFile(@"LSBIN\lodps2_04.tpl.png", "Loading - 04", "");
        public static ModProp_TextureFile Texture_Load_05 = new ModProp_TextureFile(@"LSBIN\lodps2_05.tpl.png", "Loading - 05", "");
        public static ModProp_TextureFile Texture_Load_06 = new ModProp_TextureFile(@"LSBIN\lodps2_06.tpl.png", "Loading - 06", "");
        public static ModProp_TextureFile Texture_Load_07 = new ModProp_TextureFile(@"LSBIN\lodps2_07.tpl.png", "Loading - 07", "");
        public static ModProp_TextureFile Texture_Load_08 = new ModProp_TextureFile(@"LSBIN\lodps2_08.tpl.png", "Loading - 08", "");
    }
}