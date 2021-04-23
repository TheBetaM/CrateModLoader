using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_CopterColors : ModStruct<string>
    {
        public override string Name => Rayman3_Text.Rand_CopterColors;
        public override string Description => Rayman3_Text.Rand_CopterColorsDesc;

        public override void ModPass(string basePath)
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            if (File.Exists(basePath + @"fix.tpl"))
            {
                Ray3_Common.GCN_ExportTextures(basePath + @"fix.tpl");

                File.Delete(basePath + @"fix.tpl");

                List<string> CopterTex = new List<string>()
                {
                    "fix.tpl.mm15",
                    "fix.tpl.mm16",
                    "fix.tpl.mm17",
                    "fix.tpl.mm18",
                    "fix.tpl.mm19",
                };

                foreach (string fileName in CopterTex)
                {
                    //Color CopterColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    Ray3_Common.Recolor_Texture_File(basePath + fileName + ".png", Swiz);
                }

                Ray3_Common.GCN_ImportTextures(basePath + @"fix.tpl.png");
            }
        }
    }
}
