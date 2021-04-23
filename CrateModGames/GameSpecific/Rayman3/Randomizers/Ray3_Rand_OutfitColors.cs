using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_OutfitColors : ModStruct<string>
    {
        public override string Name => Rayman3_Text.Rand_OutfitColors;
        public override string Description => Rayman3_Text.Rand_OutfitColorsDesc;

        public override void ModPass(string basePath)
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            if (File.Exists(basePath + @"fix.tpl"))
            {
                Ray3_Common.GCN_ExportTextures(basePath + @"fix.tpl");

                File.Delete(basePath + @"fix.tpl");

                List<string> OutfitTex = new List<string>()
                {
                    "fix.tpl.mm4",
                    "fix.tpl.mm6",
                    "fix.tpl.mm7",
                    "fix.tpl.mm8",
                    "fix.tpl.mm9",
                    "fix.tpl.mm10",
                    "fix.tpl.mm11",
                    "fix.tpl.mm13",
                    "fix.tpl.mm20",
                    "fix.tpl.mm21",
                    "fix.tpl.mm22",
                    "fix.tpl.mm57",
                    "fix.tpl.mm58",
                    "fix.tpl.mm59",
                    "fix.tpl.mm60",
                    "fix.tpl.mm61",
                    "fix.tpl.mm63",
                };

                foreach (string fileName in OutfitTex)
                {
                    //Color OutfitColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    Ray3_Common.Recolor_Texture_File(basePath + fileName + ".png", Swiz);
                }

                Ray3_Common.GCN_ImportTextures(basePath + @"fix.tpl.png");
            }
        }
    }
}
