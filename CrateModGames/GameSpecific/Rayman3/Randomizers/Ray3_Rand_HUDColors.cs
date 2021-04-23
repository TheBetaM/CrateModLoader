using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_HUDColors : ModStruct<string>
    {
        public override string Name => Rayman3_Text.Rand_HUDColors;
        public override string Description => Rayman3_Text.Rand_HUDColorsDesc;

        public override void ModPass(string basePath)
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            if (File.Exists(basePath + @"fix.tpl"))
            {
                Ray3_Common.GCN_ExportTextures(basePath + @"fix.tpl");

                File.Delete(basePath + @"fix.tpl");

                List<List<string>> HUDTex = new List<List<string>>()
                {
                    new List<string>() {
                        "fix.tpl.mm27",
                    },
                    new List<string>() {
                        "fix.tpl.mm29",
                    },
                    new List<string>() {
                        "fix.tpl.mm39",
                        "fix.tpl.mm40",
                        "fix.tpl.mm41",
                        "fix.tpl.mm42",
                    },
                    new List<string>() {
                        "fix.tpl.mm43",
                    },
                    new List<string>() {
                        "fix.tpl.mm47",
                    },
                    new List<string>() {
                        "fix.tpl.mm46",
                        "fix.tpl.mm49",
                        "fix.tpl.mm53",
                        "fix.tpl.mm54",
                        "fix.tpl.mm55",
                    },
                    new List<string>() {
                        "fix.tpl.mm56",
                    },
                    new List<string>() {
                        "fix.tpl.mm71",
                    },
                    new List<string>() {
                        "fix.tpl.mm72",
                        "fix.tpl.mm73",
                        "fix.tpl.mm74",
                    },
                };

                for (int i = 0; i < HUDTex.Count; i++)
                {
                    //Color targetColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    for (int t = 0; t < HUDTex[i].Count; t++)
                    {
                        Ray3_Common.Recolor_Texture_File(basePath + HUDTex[i][t] + ".png", Swiz);
                    }
                }

                Ray3_Common.GCN_ImportTextures(basePath + @"fix.tpl.png");
            }

            string lsbinpath = Ray3_Common.ExtPath + @"LSBIN\";
            if (File.Exists(lsbinpath + @"lodmeca.tpl"))
            {
                Ray3_Common.GCN_ExportTextures(lsbinpath + @"lodmeca.tpl");
                File.Delete(lsbinpath + @"lodmeca.tpl");

                //Color GearColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                Ray3_Common.Recolor_Texture_File(lsbinpath + @"lodmeca.tpl.png", Swiz);

                Ray3_Common.GCN_ImportTextures(lsbinpath + @"lodmeca.tpl.png");
            }
            if (File.Exists(lsbinpath + @"lodps2_01.tpl"))
            {
                for (int i = 1; i < 9; i++)
                {
                    //Color LoadColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    string filePath = lsbinpath + @"lodps2_0" + i + ".tpl";
                    string pngPath = lsbinpath + @"lodps2_0" + i + ".tpl.png";

                    Ray3_Common.GCN_ExportTextures(filePath);
                    File.Delete(filePath);

                    Ray3_Common.Recolor_Texture_File(pngPath, Swiz);

                    Ray3_Common.GCN_ImportTextures(pngPath);
                }
            }
        }
    }
}
