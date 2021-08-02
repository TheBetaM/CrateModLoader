using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_CopterColors : ModStruct<TPL_File>
    {
        public override void ModPass(TPL_File file)
        {
            if (file.Name.ToLower().Contains("fix.tpl"))
            {
                Random rand = GetRandom();

                List<string> CopterTex = new List<string>()
                {
                    "fix.tpl.mm15.png",
                    "fix.tpl.mm16.png",
                    "fix.tpl.mm17.png",
                    "fix.tpl.mm18.png",
                    "fix.tpl.mm19.png",
                };

                for (int i = 0; i < file.Textures.Count; i++)
                {
                    if (CopterTex.Contains(file.Textures[i].Name))
                    {
                        ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                        Ray3_Common.Recolor_Texture_File(file.Textures[i].FullName, Swiz);
                    }
                }
            }
        }
    }
}
