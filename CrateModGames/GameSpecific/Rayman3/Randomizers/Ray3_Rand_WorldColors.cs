using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_WorldColors : ModStruct<string>
    {
        public override string Name => Rayman3_Text.Rand_WorldColors;
        public override string Description => Rayman3_Text.Rand_WorldColorsDesc;

        public override void ModPass(string filePath)
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            Ray3_Common.GCN_ExportTextures(filePath, true);

            File.Delete(filePath);

            DirectoryInfo dir = new DirectoryInfo(filePath); // to fix: this is supposed to be the folder where the file is located

            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension.ToLower().Contains("png"))
                {
                    //Color CopterColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    Ray3_Common.Recolor_Texture_File(file.FullName, Swiz);
                }

            }

            Ray3_Common.GCN_ImportTextures(filePath + ".png");
        }
    }
}
