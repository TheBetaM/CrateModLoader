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

        public static string GetDataPath(GenericModStruct mod)
        {
            string basePath = mod.ExtractedPath;
            if (mod.Console == ConsoleMode.PS2)
                basePath += @"DATABIN\";
            else if (mod.Console == ConsoleMode.GCN)
                basePath += @"GAMEDATABIN\";
            else if (mod.Console == ConsoleMode.XBOX)
                basePath += @"gamedatabin\";
            else if (mod.Console == ConsoleMode.PC)
                basePath += @"Gamedatabin\";
            return basePath;
        }
        public static string GetDataPath(ConsoleMode Console, string ExtractedPath)
        {
            string basePath = ExtractedPath;
            if (Console == ConsoleMode.PS2)
                basePath += @"DATABIN\";
            else if (Console == ConsoleMode.GCN)
                basePath += @"GAMEDATABIN\";
            else if (Console == ConsoleMode.XBOX)
                basePath += @"gamedatabin\";
            else if (Console == ConsoleMode.PC)
                basePath += @"Gamedatabin\";
            return basePath;
        }

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

    }
}
