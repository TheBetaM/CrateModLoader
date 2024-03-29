﻿using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    // unstable
    public class Ray3_Rand_WorldColors : ModStruct<string>
    {
        public override void ModPass(string filePath)
        {
            Random rand = GetRandom();

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

        void Recursive_WorldColor(DirectoryInfo di, Random rand)
        {
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                Recursive_WorldColor(dir, rand);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Extension.ToLower().Contains("tpl"))
                {
                    //Rand_World_Colors(di, file.FullName, rand);
                }
            }
        }
    }
}
