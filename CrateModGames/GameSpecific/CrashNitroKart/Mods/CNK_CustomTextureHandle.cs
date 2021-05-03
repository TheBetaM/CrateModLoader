using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_CustomTextureHandle : ModStruct<GenericModStruct>
    {
        public override void PreloadPass(GenericModStruct mod)
        {
            string path_gob_extracted = mod.ExtractedPath + @"/assets/";
            ConsoleMode console = mod.Console;
            if (console == ConsoleMode.PS2)
            {
                path_gob_extracted = mod.ExtractedPath + @"/ASSETS/";
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "ps2/gfx/hud/8-bit_hud.png");
            }
            else if (console == ConsoleMode.GCN)
            {
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "gcn/gfx/hud/8-bit_hud.png");
            }
            else
            {
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "xbox/gfx/hud/8-bit_hud.png");
            }

            CNK_Data_Textures.Texture_Font.FileToResource(path_gob_extracted + "common/fonts/all_fonts.png");

            CNK_Data_Textures.Texture_Load_Arena1.FileToResource(path_gob_extracted + "common/load/arena1.png");
            CNK_Data_Textures.Texture_Load_Arena2.FileToResource(path_gob_extracted + "common/load/arena2.png");
            CNK_Data_Textures.Texture_Load_Arena3.FileToResource(path_gob_extracted + "common/load/arena3.png");
            CNK_Data_Textures.Texture_Load_Arena4.FileToResource(path_gob_extracted + "common/load/arena4.png");
            CNK_Data_Textures.Texture_Load_Arena5.FileToResource(path_gob_extracted + "common/load/arena5.png");
            CNK_Data_Textures.Texture_Load_Barin1.FileToResource(path_gob_extracted + "common/load/barin1.png");
            CNK_Data_Textures.Texture_Load_Barin2.FileToResource(path_gob_extracted + "common/load/barin2.png");
            CNK_Data_Textures.Texture_Load_Barin3.FileToResource(path_gob_extracted + "common/load/barin3.png");
            CNK_Data_Textures.Texture_Load_Citadel.FileToResource(path_gob_extracted + "common/load/citadel.png");
            CNK_Data_Textures.Texture_Load_Earth1.FileToResource(path_gob_extracted + "common/load/earth1.png");
            CNK_Data_Textures.Texture_Load_Earth2.FileToResource(path_gob_extracted + "common/load/earth2.png");
            CNK_Data_Textures.Texture_Load_Earth3.FileToResource(path_gob_extracted + "common/load/earth3.png");
            CNK_Data_Textures.Texture_Load_Fenom1.FileToResource(path_gob_extracted + "common/load/fenom1.png");
            CNK_Data_Textures.Texture_Load_Fenom2.FileToResource(path_gob_extracted + "common/load/fenom2.png");
            CNK_Data_Textures.Texture_Load_Fenom3.FileToResource(path_gob_extracted + "common/load/fenom3.png");
            CNK_Data_Textures.Texture_Load_Hub1.FileToResource(path_gob_extracted + "common/load/hub1.png");
            CNK_Data_Textures.Texture_Load_Hub2.FileToResource(path_gob_extracted + "common/load/hub2.png");
            CNK_Data_Textures.Texture_Load_Hub3.FileToResource(path_gob_extracted + "common/load/hub3.png");
            CNK_Data_Textures.Texture_Load_Hub4.FileToResource(path_gob_extracted + "common/load/hub4.png");
            CNK_Data_Textures.Texture_Load_Hub5.FileToResource(path_gob_extracted + "common/load/hub5.png");
            CNK_Data_Textures.Texture_Load_Teknee1.FileToResource(path_gob_extracted + "common/load/teknee1.png");
            CNK_Data_Textures.Texture_Load_Teknee2.FileToResource(path_gob_extracted + "common/load/teknee2.png");
            CNK_Data_Textures.Texture_Load_Teknee3.FileToResource(path_gob_extracted + "common/load/teknee3.png");
            CNK_Data_Textures.Texture_Load_Trophy.FileToResource(path_gob_extracted + "common/load/trophy.png");
            CNK_Data_Textures.Texture_Load_Velorace.FileToResource(path_gob_extracted + "common/load/velorace.png");
            CNK_Data_Textures.Texture_Load_MainMenu01.FileToResource(path_gob_extracted + "common/load/mainmenu01.png");
            CNK_Data_Textures.Texture_Load_MainMenu02.FileToResource(path_gob_extracted + "common/load/mainmenu02.png");
            CNK_Data_Textures.Texture_Load_MainMenu03.FileToResource(path_gob_extracted + "common/load/mainmenu03.png");
            CNK_Data_Textures.Texture_Load_MainMenu04.FileToResource(path_gob_extracted + "common/load/mainmenu04.png");
            CNK_Data_Textures.Texture_Load_MainMenu05.FileToResource(path_gob_extracted + "common/load/mainmenu05.png");
            CNK_Data_Textures.Texture_Load_MainMenu06.FileToResource(path_gob_extracted + "common/load/mainmenu06.png");
            CNK_Data_Textures.Texture_Load_MainMenu07.FileToResource(path_gob_extracted + "common/load/mainmenu07.png");
            CNK_Data_Textures.Texture_Load_MainMenu08.FileToResource(path_gob_extracted + "common/load/mainmenu08.png");
            CNK_Data_Textures.Texture_Load_MainMenu09.FileToResource(path_gob_extracted + "common/load/mainmenu09.png");
            CNK_Data_Textures.Texture_Load_MainMenu10.FileToResource(path_gob_extracted + "common/load/mainmenu10.png");
            CNK_Data_Textures.Texture_Load_MainMenu11.FileToResource(path_gob_extracted + "common/load/mainmenu11.png");
            CNK_Data_Textures.Texture_Load_MainMenu12.FileToResource(path_gob_extracted + "common/load/mainmenu12.png");
            CNK_Data_Textures.Texture_Load_MainMenu13.FileToResource(path_gob_extracted + "common/load/mainmenu13.png");

        }


        public override void ModPass(GenericModStruct mod)
        {
            string path_gob_extracted = mod.ExtractedPath + @"/assets/";
            ConsoleMode console = mod.Console;
            if (console == ConsoleMode.PS2)
            {
                path_gob_extracted = mod.ExtractedPath + @"/ASSETS/";
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "ps2/gfx/hud/8-bit_hud.png");
            }
            else if (console == ConsoleMode.GCN)
            {
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "gcn/gfx/hud/8-bit_hud.png");
            }
            else
            {
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "xbox/gfx/hud/8-bit_hud.png");
            }

            CNK_Data_Textures.Texture_Font.ResourceToFile(path_gob_extracted + "common/fonts/all_fonts.png");

            CNK_Data_Textures.Texture_Load_Arena1.ResourceToFile(path_gob_extracted + "common/load/arena1.png");
            CNK_Data_Textures.Texture_Load_Arena2.ResourceToFile(path_gob_extracted + "common/load/arena2.png");
            CNK_Data_Textures.Texture_Load_Arena3.ResourceToFile(path_gob_extracted + "common/load/arena3.png");
            CNK_Data_Textures.Texture_Load_Arena4.ResourceToFile(path_gob_extracted + "common/load/arena4.png");
            CNK_Data_Textures.Texture_Load_Arena5.ResourceToFile(path_gob_extracted + "common/load/arena5.png");
            CNK_Data_Textures.Texture_Load_Barin1.ResourceToFile(path_gob_extracted + "common/load/barin1.png");
            CNK_Data_Textures.Texture_Load_Barin2.ResourceToFile(path_gob_extracted + "common/load/barin2.png");
            CNK_Data_Textures.Texture_Load_Barin3.ResourceToFile(path_gob_extracted + "common/load/barin3.png");
            CNK_Data_Textures.Texture_Load_Citadel.ResourceToFile(path_gob_extracted + "common/load/citadel.png");
            CNK_Data_Textures.Texture_Load_Earth1.ResourceToFile(path_gob_extracted + "common/load/earth1.png");
            CNK_Data_Textures.Texture_Load_Earth2.ResourceToFile(path_gob_extracted + "common/load/earth2.png");
            CNK_Data_Textures.Texture_Load_Earth3.ResourceToFile(path_gob_extracted + "common/load/earth3.png");
            CNK_Data_Textures.Texture_Load_Fenom1.ResourceToFile(path_gob_extracted + "common/load/fenom1.png");
            CNK_Data_Textures.Texture_Load_Fenom2.ResourceToFile(path_gob_extracted + "common/load/fenom2.png");
            CNK_Data_Textures.Texture_Load_Fenom3.ResourceToFile(path_gob_extracted + "common/load/fenom3.png");
            CNK_Data_Textures.Texture_Load_Hub1.ResourceToFile(path_gob_extracted + "common/load/hub1.png");
            CNK_Data_Textures.Texture_Load_Hub2.ResourceToFile(path_gob_extracted + "common/load/hub2.png");
            CNK_Data_Textures.Texture_Load_Hub3.ResourceToFile(path_gob_extracted + "common/load/hub3.png");
            CNK_Data_Textures.Texture_Load_Hub4.ResourceToFile(path_gob_extracted + "common/load/hub4.png");
            CNK_Data_Textures.Texture_Load_Hub5.ResourceToFile(path_gob_extracted + "common/load/hub5.png");
            CNK_Data_Textures.Texture_Load_Teknee1.ResourceToFile(path_gob_extracted + "common/load/teknee1.png");
            CNK_Data_Textures.Texture_Load_Teknee2.ResourceToFile(path_gob_extracted + "common/load/teknee2.png");
            CNK_Data_Textures.Texture_Load_Teknee3.ResourceToFile(path_gob_extracted + "common/load/teknee3.png");
            CNK_Data_Textures.Texture_Load_Trophy.ResourceToFile(path_gob_extracted + "common/load/trophy.png");
            CNK_Data_Textures.Texture_Load_Velorace.ResourceToFile(path_gob_extracted + "common/load/velorace.png");
            CNK_Data_Textures.Texture_Load_MainMenu01.ResourceToFile(path_gob_extracted + "common/load/mainmenu01.png");
            CNK_Data_Textures.Texture_Load_MainMenu02.ResourceToFile(path_gob_extracted + "common/load/mainmenu02.png");
            CNK_Data_Textures.Texture_Load_MainMenu03.ResourceToFile(path_gob_extracted + "common/load/mainmenu03.png");
            CNK_Data_Textures.Texture_Load_MainMenu04.ResourceToFile(path_gob_extracted + "common/load/mainmenu04.png");
            CNK_Data_Textures.Texture_Load_MainMenu05.ResourceToFile(path_gob_extracted + "common/load/mainmenu05.png");
            CNK_Data_Textures.Texture_Load_MainMenu06.ResourceToFile(path_gob_extracted + "common/load/mainmenu06.png");
            CNK_Data_Textures.Texture_Load_MainMenu07.ResourceToFile(path_gob_extracted + "common/load/mainmenu07.png");
            CNK_Data_Textures.Texture_Load_MainMenu08.ResourceToFile(path_gob_extracted + "common/load/mainmenu08.png");
            CNK_Data_Textures.Texture_Load_MainMenu09.ResourceToFile(path_gob_extracted + "common/load/mainmenu09.png");
            CNK_Data_Textures.Texture_Load_MainMenu10.ResourceToFile(path_gob_extracted + "common/load/mainmenu10.png");
            CNK_Data_Textures.Texture_Load_MainMenu11.ResourceToFile(path_gob_extracted + "common/load/mainmenu11.png");
            CNK_Data_Textures.Texture_Load_MainMenu12.ResourceToFile(path_gob_extracted + "common/load/mainmenu12.png");
            CNK_Data_Textures.Texture_Load_MainMenu13.ResourceToFile(path_gob_extracted + "common/load/mainmenu13.png");
        }
    }
}
