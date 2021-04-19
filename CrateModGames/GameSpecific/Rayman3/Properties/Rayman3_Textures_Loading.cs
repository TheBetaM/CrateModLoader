using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Rayman3;
namespace CrateModLoader.GameSpecific.Rayman3
{
    [ModCategory((int)R3_ModProps.Textures_Loading), ModAllowedConsoles(ConsoleMode.GCN)]
    public static class Rayman3_Textures_Loading
    {
        public static ModProp_TextureFile Texture_Load_Gear = new ModProp_TextureFile(false, "Loading - Gear", "");
        public static ModProp_TextureFile Texture_Load_01 = new ModProp_TextureFile(false, "Loading - 01", "");
        public static ModProp_TextureFile Texture_Load_02 = new ModProp_TextureFile(false, "Loading - 02", "");
        public static ModProp_TextureFile Texture_Load_03 = new ModProp_TextureFile(false, "Loading - 03", "");
        public static ModProp_TextureFile Texture_Load_04 = new ModProp_TextureFile(false, "Loading - 04", "");
        public static ModProp_TextureFile Texture_Load_05 = new ModProp_TextureFile(false, "Loading - 05", "");
        public static ModProp_TextureFile Texture_Load_06 = new ModProp_TextureFile(false, "Loading - 06", "");
        public static ModProp_TextureFile Texture_Load_07 = new ModProp_TextureFile(false, "Loading - 07", "");
        public static ModProp_TextureFile Texture_Load_08 = new ModProp_TextureFile(false, "Loading - 08", "");
    }
}