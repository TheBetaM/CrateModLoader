using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_OutfitColors : ModStruct<TPL_File>
    {
        public override void ModPass(TPL_File file)
        {

            if (file.Name.ToLower().Contains("fix.tpl"))
            {
                Random rand = GetRandom();

                List<string> OutfitTex = new List<string>()
                {
                    "fix.tpl.mm4.png",
                    "fix.tpl.mm6.png",
                    "fix.tpl.mm7.png",
                    "fix.tpl.mm8.png",
                    "fix.tpl.mm9.png",
                    "fix.tpl.mm10.png",
                    "fix.tpl.mm11.png",
                    "fix.tpl.mm13.png",
                    "fix.tpl.mm20.png",
                    "fix.tpl.mm21.png",
                    "fix.tpl.mm22.png",
                    "fix.tpl.mm57.png",
                    "fix.tpl.mm58.png",
                    "fix.tpl.mm59.png",
                    "fix.tpl.mm60.png",
                    "fix.tpl.mm61.png",
                    "fix.tpl.mm63.png",
                };

                for (int i = 0; i < file.Textures.Count; i++)
                {
                    if (OutfitTex.Contains(file.Textures[i].Name))
                    {
                        ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                        Ray3_Common.Recolor_Texture_File(file.Textures[i].FullName, Swiz);
                    }
                }
            }
        }
    }
}
