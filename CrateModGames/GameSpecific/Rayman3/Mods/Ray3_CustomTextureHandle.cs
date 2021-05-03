using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Rayman3.Mods
{
    public class Ray3_CustomTextureHandle : ModStruct<TPL_File>
    {
        public override void PreloadPass(TPL_File file)
        {
            if (file.Name.ToLower().Contains(@"fix.tpl"))
            {
                string basePath = file.FolderName;

                Rayman3_Textures_General.Texture_General_Fist_LockJaw.FileToResource(basePath + @"fix.tpl.mm4.png");
                Rayman3_Textures_General.Texture_General_Outfit_ThrottleCopter.FileToResource(basePath + @"fix.tpl.mm6.png");
                Rayman3_Textures_General.Texture_General_Outfit_Normal.FileToResource(basePath + @"fix.tpl.mm7.png");
                Rayman3_Textures_General.Texture_General_Outfit_HeavyMetalFist.FileToResource(basePath + @"fix.tpl.mm8.png");
                Rayman3_Textures_General.Texture_General_Outfit_LockJaw.FileToResource(basePath + @"fix.tpl.mm9.png");
                Rayman3_Textures_General.Texture_General_Outfit_Vortex.FileToResource(basePath + @"fix.tpl.mm10.png");
                Rayman3_Textures_General.Texture_General_Outfit_ShockRocket.FileToResource(basePath + @"fix.tpl.mm11.png");
                Rayman3_Textures_General.Texture_General_Fist_Vortex.FileToResource(basePath + @"fix.tpl.mm13.png");
                Rayman3_Textures_General.Texture_General_Copter_Normal.FileToResource(basePath + @"fix.tpl.mm15.png");
                Rayman3_Textures_General.Texture_General_Copter_LockJaw.FileToResource(basePath + @"fix.tpl.mm17.png");
                Rayman3_Textures_General.Texture_General_Copter_ShockRocket.FileToResource(basePath + @"fix.tpl.mm16.png");
                Rayman3_Textures_General.Texture_General_Copter_HeavyMetalFist.FileToResource(basePath + @"fix.tpl.mm18.png");
                Rayman3_Textures_General.Texture_General_Copter_Vortex.FileToResource(basePath + @"fix.tpl.mm19.png");
                Rayman3_Textures_General.Texture_General_Fist_ShockRocket.FileToResource(basePath + @"fix.tpl.mm20.png");
                Rayman3_Textures_General.Texture_General_Copter_ThrottleCopter.FileToResource(basePath + @"fix.tpl.mm21.png");
                Rayman3_Textures_General.Texture_General_Fist_HeavyMetalFist.FileToResource(basePath + @"fix.tpl.mm22.png");
                Rayman3_Textures_General.Texture_General_RedLum01.FileToResource(basePath + @"fix.tpl.mm24.png");
                Rayman3_Textures_General.Texture_General_RedLum02.FileToResource(basePath + @"fix.tpl.mm25.png");
                Rayman3_Textures_General.Texture_General_RedLum03.FileToResource(basePath + @"fix.tpl.mm26.png");
                Rayman3_Textures_General.Texture_General_RedLum04.FileToResource(basePath + @"fix.tpl.mm23.png");
                Rayman3_Textures_General.Texture_General_GameOver.FileToResource(basePath + @"fix.tpl.mm27.png");
                Rayman3_Textures_General.Texture_General_HUDElements.FileToResource(basePath + @"fix.tpl.mm29.png");
                Rayman3_Textures_General.Texture_General_AimingNear.FileToResource(basePath + @"fix.tpl.mm39.png");
                Rayman3_Textures_General.Texture_General_AimingArrow01.FileToResource(basePath + @"fix.tpl.mm40.png");
                Rayman3_Textures_General.Texture_General_AimingArrow02.FileToResource(basePath + @"fix.tpl.mm41.png");
                Rayman3_Textures_General.Texture_General_AimingArrow03.FileToResource(basePath + @"fix.tpl.mm42.png");
                Rayman3_Textures_General.Texture_General_AimingFar.FileToResource(basePath + @"fix.tpl.mm43.png");
                Rayman3_Textures_General.Texture_General_ResultStarOff.FileToResource(basePath + @"fix.tpl.mm46.png");
                Rayman3_Textures_General.Texture_General_GradientBG.FileToResource(basePath + @"fix.tpl.mm47.png");
                Rayman3_Textures_General.Texture_General_ResultMurfy.FileToResource(basePath + @"fix.tpl.mm48.png");
                Rayman3_Textures_General.Texture_General_ResultStarOn.FileToResource(basePath + @"fix.tpl.mm49.png");
                Rayman3_Textures_General.Texture_General_TeensieUnlocked.FileToResource(basePath + @"fix.tpl.mm53.png");
                Rayman3_Textures_General.Texture_General_TeensieLocked.FileToResource(basePath + @"fix.tpl.mm54.png");
                Rayman3_Textures_General.Texture_General_TeensieBG.FileToResource(basePath + @"fix.tpl.mm55.png");
                Rayman3_Textures_General.Texture_General_ScoreDisplay.FileToResource(basePath + @"fix.tpl.mm56.png");
                Rayman3_Textures_General.Texture_General_Arrow.FileToResource(basePath + @"fix.tpl.mm62.png");
                Rayman3_Textures_General.Texture_General_Damage01.FileToResource(basePath + @"fix.tpl.mm64.png");
                Rayman3_Textures_General.Texture_General_Damage02.FileToResource(basePath + @"fix.tpl.mm66.png");
                Rayman3_Textures_General.Texture_General_Damage03.FileToResource(basePath + @"fix.tpl.mm68.png");
                Rayman3_Textures_General.Texture_General_Damage04.FileToResource(basePath + @"fix.tpl.mm70.png");
                Rayman3_Textures_General.Texture_General_ComboScores.FileToResource(basePath + @"fix.tpl.mm71.png");
                Rayman3_Textures_General.Texture_General_Popup01.FileToResource(basePath + @"fix.tpl.mm72.png");
                Rayman3_Textures_General.Texture_General_Popup02.FileToResource(basePath + @"fix.tpl.mm73.png");
                Rayman3_Textures_General.Texture_General_Popup03.FileToResource(basePath + @"fix.tpl.mm74.png");
                Rayman3_Textures_General.Texture_General_Font.FileToResource(basePath + @"fix.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"menu.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Menu.Texture_Menu_Overlay.FileToResource(basePath + @"menu.tpl.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons.FileToResource(basePath + @"menu.tpl.mm1.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level1.FileToResource(basePath + @"menu.tpl.mm2.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level2.FileToResource(basePath + @"menu.tpl.mm3.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level3.FileToResource(basePath + @"menu.tpl.mm4.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level4.FileToResource(basePath + @"menu.tpl.mm5.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level5.FileToResource(basePath + @"menu.tpl.mm6.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level6.FileToResource(basePath + @"menu.tpl.mm7.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level7.FileToResource(basePath + @"menu.tpl.mm8.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level8.FileToResource(basePath + @"menu.tpl.mm9.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level9.FileToResource(basePath + @"menu.tpl.mm10.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Lum.FileToResource(basePath + @"menu.tpl.mm11.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Options.FileToResource(basePath + @"menu.tpl.mm12.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Rayman.FileToResource(basePath + @"menu.tpl.mm13.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Murfy.FileToResource(basePath + @"menu.tpl.mm14.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Videos.FileToResource(basePath + @"menu.tpl.mm15.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Camera.FileToResource(basePath + @"menu.tpl.mm16.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Plum.FileToResource(basePath + @"menu.tpl.mm17.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons_Videos.FileToResource(basePath + @"menu.tpl.mm18.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons_Misc.FileToResource(basePath + @"menu.tpl.mm19.png");
            }
            if (file.Name.ToLower().Contains(@"lodmeca.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_Gear.FileToResource(lsbinpath + @"lodmeca.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_01.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_01.FileToResource(lsbinpath + @"lodps2_01.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_02.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_02.FileToResource(lsbinpath + @"lodps2_02.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_03.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_03.FileToResource(lsbinpath + @"lodps2_03.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_04.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_04.FileToResource(lsbinpath + @"lodps2_04.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_05.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_05.FileToResource(lsbinpath + @"lodps2_05.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_06.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_06.FileToResource(lsbinpath + @"lodps2_06.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_07.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_07.FileToResource(lsbinpath + @"lodps2_07.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_08.tpl"))
            {
                string lsbinpath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_08.FileToResource(lsbinpath + @"lodps2_08.tpl.png");
            }
        }

        public override void ModPass(TPL_File file)
        {
            if (file.Name.ToLower().Contains(@"fix.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_General.Texture_General_Fist_LockJaw.ResourceToFile(basePath + @"fix.tpl.mm4.png");
                Rayman3_Textures_General.Texture_General_Outfit_ThrottleCopter.ResourceToFile(basePath + @"fix.tpl.mm6.png");
                Rayman3_Textures_General.Texture_General_Outfit_Normal.ResourceToFile(basePath + @"fix.tpl.mm7.png");
                Rayman3_Textures_General.Texture_General_Outfit_HeavyMetalFist.ResourceToFile(basePath + @"fix.tpl.mm8.png");
                Rayman3_Textures_General.Texture_General_Outfit_LockJaw.ResourceToFile(basePath + @"fix.tpl.mm9.png");
                Rayman3_Textures_General.Texture_General_Outfit_Vortex.ResourceToFile(basePath + @"fix.tpl.mm10.png");
                Rayman3_Textures_General.Texture_General_Outfit_ShockRocket.ResourceToFile(basePath + @"fix.tpl.mm11.png");
                Rayman3_Textures_General.Texture_General_Fist_Vortex.ResourceToFile(basePath + @"fix.tpl.mm13.png");
                Rayman3_Textures_General.Texture_General_Copter_Normal.ResourceToFile(basePath + @"fix.tpl.mm15.png");
                Rayman3_Textures_General.Texture_General_Copter_LockJaw.ResourceToFile(basePath + @"fix.tpl.mm17.png");
                Rayman3_Textures_General.Texture_General_Copter_ShockRocket.ResourceToFile(basePath + @"fix.tpl.mm16.png");
                Rayman3_Textures_General.Texture_General_Copter_HeavyMetalFist.ResourceToFile(basePath + @"fix.tpl.mm18.png");
                Rayman3_Textures_General.Texture_General_Copter_Vortex.ResourceToFile(basePath + @"fix.tpl.mm19.png");
                Rayman3_Textures_General.Texture_General_Fist_ShockRocket.ResourceToFile(basePath + @"fix.tpl.mm20.png");
                Rayman3_Textures_General.Texture_General_Copter_ThrottleCopter.ResourceToFile(basePath + @"fix.tpl.mm21.png");
                Rayman3_Textures_General.Texture_General_Fist_HeavyMetalFist.ResourceToFile(basePath + @"fix.tpl.mm22.png");
                Rayman3_Textures_General.Texture_General_RedLum01.ResourceToFile(basePath + @"fix.tpl.mm24.png");
                Rayman3_Textures_General.Texture_General_RedLum02.ResourceToFile(basePath + @"fix.tpl.mm25.png");
                Rayman3_Textures_General.Texture_General_RedLum03.ResourceToFile(basePath + @"fix.tpl.mm26.png");
                Rayman3_Textures_General.Texture_General_RedLum04.ResourceToFile(basePath + @"fix.tpl.mm23.png");
                Rayman3_Textures_General.Texture_General_GameOver.ResourceToFile(basePath + @"fix.tpl.mm27.png");
                Rayman3_Textures_General.Texture_General_HUDElements.ResourceToFile(basePath + @"fix.tpl.mm29.png");
                Rayman3_Textures_General.Texture_General_AimingNear.ResourceToFile(basePath + @"fix.tpl.mm39.png");
                Rayman3_Textures_General.Texture_General_AimingArrow01.ResourceToFile(basePath + @"fix.tpl.mm40.png");
                Rayman3_Textures_General.Texture_General_AimingArrow02.ResourceToFile(basePath + @"fix.tpl.mm41.png");
                Rayman3_Textures_General.Texture_General_AimingArrow03.ResourceToFile(basePath + @"fix.tpl.mm42.png");
                Rayman3_Textures_General.Texture_General_AimingFar.ResourceToFile(basePath + @"fix.tpl.mm43.png");
                Rayman3_Textures_General.Texture_General_ResultStarOff.ResourceToFile(basePath + @"fix.tpl.mm46.png");
                Rayman3_Textures_General.Texture_General_GradientBG.ResourceToFile(basePath + @"fix.tpl.mm47.png");
                Rayman3_Textures_General.Texture_General_ResultMurfy.ResourceToFile(basePath + @"fix.tpl.mm48.png");
                Rayman3_Textures_General.Texture_General_ResultStarOn.ResourceToFile(basePath + @"fix.tpl.mm49.png");
                Rayman3_Textures_General.Texture_General_TeensieUnlocked.ResourceToFile(basePath + @"fix.tpl.mm53.png");
                Rayman3_Textures_General.Texture_General_TeensieLocked.ResourceToFile(basePath + @"fix.tpl.mm54.png");
                Rayman3_Textures_General.Texture_General_TeensieBG.ResourceToFile(basePath + @"fix.tpl.mm55.png");
                Rayman3_Textures_General.Texture_General_ScoreDisplay.ResourceToFile(basePath + @"fix.tpl.mm56.png");
                Rayman3_Textures_General.Texture_General_Arrow.ResourceToFile(basePath + @"fix.tpl.mm62.png");
                Rayman3_Textures_General.Texture_General_Damage01.ResourceToFile(basePath + @"fix.tpl.mm64.png");
                Rayman3_Textures_General.Texture_General_Damage02.ResourceToFile(basePath + @"fix.tpl.mm66.png");
                Rayman3_Textures_General.Texture_General_Damage03.ResourceToFile(basePath + @"fix.tpl.mm68.png");
                Rayman3_Textures_General.Texture_General_Damage04.ResourceToFile(basePath + @"fix.tpl.mm70.png");
                Rayman3_Textures_General.Texture_General_ComboScores.ResourceToFile(basePath + @"fix.tpl.mm71.png");
                Rayman3_Textures_General.Texture_General_Popup01.ResourceToFile(basePath + @"fix.tpl.mm72.png");
                Rayman3_Textures_General.Texture_General_Popup02.ResourceToFile(basePath + @"fix.tpl.mm73.png");
                Rayman3_Textures_General.Texture_General_Popup03.ResourceToFile(basePath + @"fix.tpl.mm74.png");
                Rayman3_Textures_General.Texture_General_Font.ResourceToFile(basePath + @"fix.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"menu.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Menu.Texture_Menu_Overlay.ResourceToFile(basePath + @"menu.tpl.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons.ResourceToFile(basePath + @"menu.tpl.mm1.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level1.ResourceToFile(basePath + @"menu.tpl.mm2.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level2.ResourceToFile(basePath + @"menu.tpl.mm3.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level3.ResourceToFile(basePath + @"menu.tpl.mm4.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level4.ResourceToFile(basePath + @"menu.tpl.mm5.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level5.ResourceToFile(basePath + @"menu.tpl.mm6.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level6.ResourceToFile(basePath + @"menu.tpl.mm7.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level7.ResourceToFile(basePath + @"menu.tpl.mm8.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level8.ResourceToFile(basePath + @"menu.tpl.mm9.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level9.ResourceToFile(basePath + @"menu.tpl.mm10.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Lum.ResourceToFile(basePath + @"menu.tpl.mm11.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Options.ResourceToFile(basePath + @"menu.tpl.mm12.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Rayman.ResourceToFile(basePath + @"menu.tpl.mm13.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Murfy.ResourceToFile(basePath + @"menu.tpl.mm14.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Videos.ResourceToFile(basePath + @"menu.tpl.mm15.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Camera.ResourceToFile(basePath + @"menu.tpl.mm16.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Plum.ResourceToFile(basePath + @"menu.tpl.mm17.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons_Videos.ResourceToFile(basePath + @"menu.tpl.mm18.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons_Misc.ResourceToFile(basePath + @"menu.tpl.mm19.png");
            }
            if (file.Name.ToLower().Contains(@"lodmeca.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_Gear.ResourceToFile(basePath + @"lodmeca.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_01.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_01.ResourceToFile(basePath + @"lodps2_01.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_02.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_02.ResourceToFile(basePath + @"lodps2_02.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_03.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_03.ResourceToFile(basePath + @"lodps2_03.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_04.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_04.ResourceToFile(basePath + @"lodps2_04.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_05.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_05.ResourceToFile(basePath + @"lodps2_05.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_06.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_06.ResourceToFile(basePath + @"lodps2_06.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_07.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_07.ResourceToFile(basePath + @"lodps2_07.tpl.png");
            }
            if (file.Name.ToLower().Contains(@"lodps2_08.tpl"))
            {
                string basePath = file.FolderName;
                Rayman3_Textures_Loading.Texture_Load_08.ResourceToFile(basePath + @"lodps2_08.tpl.png");
            }
        }
    }
}
