using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class ColorSwizzleData
    {
        public int r_r;
        public int r_g;
        public int r_b;
        public int r_s;
        public int g_r;
        public int g_g;
        public int g_b;
        public int g_s;
        public int b_r;
        public int b_g;
        public int b_b;
        public int b_s;

        public ColorSwizzleData(Random rand)
        {
            r_r = rand.Next(2);
            r_g = rand.Next(2);
            r_b = rand.Next(2);
            r_s = r_r + r_g + r_b;
            g_r = rand.Next(2);
            g_g = rand.Next(2);
            g_b = rand.Next(2);
            g_s = g_r + g_g + g_b;
            b_r = rand.Next(2);
            b_g = rand.Next(2);
            b_b = rand.Next(2);
            b_s = b_r + b_g + b_b;

            if (r_s == 0) r_s = 1;
            if (g_s == 0) g_s = 1;
            if (b_s == 0) b_s = 1;
        }
    }

    public enum LevelID
    {
        Invalid = -1,
        Fairy1_Intro_10 = 0,
        Fairy2_Intro_15 = 1,
        Fairy3_Intro_17 = 2,
        Fairy4_Intro_20 = 3,
        Fairy5_Menu_00 = 4,
        Fairy6_Sk8_00 = 5,
        Forest1_Wood_11 = 6,
        Forest2_Wood_10 = 7,
        Forest3_Wood_19 = 8,
        Forest4_Wood_50 = 9,
        Forest5_Menu_10 = 10,
        Forest6_Sk8_10 = 11,
        Bog1_Swamp_60 = 12,
        Bog2_Swamp_82 = 13,
        Bog3_Swamp_81 = 14,
        Bog4_Swamp_83 = 15,
        Bog5_Swamp_50 = 16,
        Bog6_Swamp_51 = 17,
        LividDead1_Moor_00 = 18,
        LividDead2_Moor_30 = 19,
        LividDead3_Moor_60 = 20,
        LividDead4_Moor_19 = 21,
        LividDead5_Menu_20 = 22,
        LividDead6_Sk8_20 = 23,
        Desert1_Knaar_10 = 24,
        Desert2_Knaar_20 = 25,
        Desert3_Knaar_30 = 26,
        Desert4_Knaar_45 = 27,
        Desert5_Knaar_60 = 28,
        Desert6_Knaar_69 = 29,
        Desert7_Knaar_70 = 30,
        Desert8_Menu_30 = 31,
        Shortcut1_Flash_20 = 32,
        Shortcut2_Flash_30 = 33,
        Shortcut3_Flash_10 = 34,
        Summit1_Sea_10 = 35,
        Summit2_Mount_50 = 36,
        Summit3_Mount_4x = 37,
        Hoodlum1_Fact_40 = 38,
        Hoodlum2_Fact_50 = 39,
        Hoodlum3_Fact_55 = 40,
        Hoodlum4_Fact_34 = 41,
        Hoodlum5_Fact_22 = 42,
        Tower1_Tower_10 = 43,
        Tower2_Tower_20 = 44,
        Tower3_Tower_30 = 45,
        Tower4_Tower_40 = 46,
        Tower5_Lept_15 = 47,
    }

    public class Rayman3_GenericMod
    {
        public string mainPath;
        public ConsoleMode console;

        public Rayman3_GenericMod(string main, ConsoleMode cons)
        {
            mainPath = main;
            console = cons;
        }
    }

    public static class Ray3_Common
    {
        public static string[] LevelNames = new string[]
        {
            "intro_10",
            "intro_15",
            "Intro_17",
            "intro_20",
            "menu_00",
            "sk8_00",
            "wood_11",
            "Wood_10",
            "Wood_19",
            "Wood_50",
            "menu_10",
            "Sk8_10",
            "Swamp_60",
            "Swamp_82",
            "Swamp_81",
            "swamp_83",
            "Swamp_50",
            "Swamp_51",
            "Moor_00",
            "Moor_30",
            "moor_60",
            "moor_19",
            "menu_20",
            "Sk8_20",
            "Knaar_10",
            "Knaar_20",
            "Knaar_30",
            "Knaar_45",
            "Knaar_60",
            "Knaar_69",
            "Knaar_70",
            "menu_30",
            "Flash_20",
            "Flash_30",
            "flash_10",
            "Sea_10",
            "mount_50",
            "mount_4x",
            "Fact_40",
            "Fact_50",
            "Fact_55",
            "fact_34",
            "Fact_22",
            "Tower_10",
            "Tower_20",
            "Tower_30",
            "Tower_40",
            "lept_15",
        };

        public static List<string> AllowedTPL = new List<string>()
        {
            "menu.tpl",
            "fix.tpl",
            "lodmeca.tpl",
            "lodps2_01.tpl",
            "lodps2_02.tpl",
            "lodps2_03.tpl",
            "lodps2_04.tpl",
            "lodps2_05.tpl",
            "lodps2_06.tpl",
            "lodps2_07.tpl",
            "lodps2_08.tpl",
        };

        public static void Recolor_Texture_File(string filePath, ColorSwizzleData Swiz)
        {
            Bitmap temp = new Bitmap(filePath);
            Bitmap tex = new Bitmap(temp);
            temp.Dispose();
            File.Delete(filePath);

            for (int x = 0; x < tex.Width; x++)
            {
                for (int y = 0; y < tex.Height; y++)
                {
                    Color sourceColor = tex.GetPixel(x, y);
                    float intensity = Math.Max(sourceColor.R, sourceColor.G);
                    intensity = Math.Max(intensity, sourceColor.B);
                    intensity = intensity / 255f;

                    int r = sourceColor.R;
                    int g = sourceColor.G;
                    int b = sourceColor.B;

                    Color targetColor = Color.FromArgb(tex.GetPixel(x, y).A,
                        (int)((Swiz.r_r * r + Swiz.r_g * g + Swiz.r_b * b) / Swiz.r_s),
                        (int)((Swiz.g_r * r + Swiz.g_g * g + Swiz.g_b * b) / Swiz.g_s),
                        (int)((Swiz.b_r * r + Swiz.b_g * g + Swiz.b_b * b) / Swiz.b_s));

                    tex.SetPixel(x, y, targetColor);
                }
            }

            tex.Save(filePath);
            tex.Dispose();
        }

        public static void GCN_ExportTextures(string path, bool noCleanup = false)
        {
            string args = "decode ";
            args += path;

            Process wimgt = new Process();
            wimgt.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wimgt.exe";
            //wimgt.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            wimgt.StartInfo.Arguments = args;
            wimgt.StartInfo.UseShellExecute = false;
            wimgt.StartInfo.RedirectStandardOutput = true;
            wimgt.StartInfo.CreateNoWindow = true;
            wimgt.Start();

            string outputMessage = wimgt.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);

            wimgt.WaitForExit();
        }
        public static void GCN_ImportTextures(string path)
        {
            string args = "encode ";
            args += path;

            Process wimgt = new Process();
            wimgt.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wimgt.exe";
            //wimgt.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            wimgt.StartInfo.Arguments = args;
            wimgt.StartInfo.UseShellExecute = false;
            wimgt.StartInfo.RedirectStandardOutput = true;
            wimgt.StartInfo.CreateNoWindow = true;
            wimgt.Start();

            string outputMessage = wimgt.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);

            wimgt.WaitForExit();
        }

        public static void Custom_Texture_Handle(TPL_File file)
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
