using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_HUDColors : ModStruct<TPL_File>
    {
        public override string Name => Rayman3_Text.Rand_HUDColors;
        public override string Description => Rayman3_Text.Rand_HUDColorsDesc;

        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(TPL_File file)
        {

            if (file.Name.ToLower().Contains("fix.tpl"))
            {

                List<List<string>> HUDTex = new List<List<string>>()
                {
                    new List<string>() {
                        "fix.tpl.mm27.png",
                    },
                    new List<string>() {
                        "fix.tpl.mm29.png",
                    },
                    new List<string>() {
                        "fix.tpl.mm39.png",
                        "fix.tpl.mm40.png",
                        "fix.tpl.mm41.png",
                        "fix.tpl.mm42.png",
                    },
                    new List<string>() {
                        "fix.tpl.mm43.png",
                    },
                    new List<string>() {
                        "fix.tpl.mm47.png",
                    },
                    new List<string>() {
                        "fix.tpl.mm46.png",
                        "fix.tpl.mm49.png",
                        "fix.tpl.mm53.png",
                        "fix.tpl.mm54.png",
                        "fix.tpl.mm55.png",
                    },
                    new List<string>() {
                        "fix.tpl.mm56.png",
                    },
                    new List<string>() {
                        "fix.tpl.mm71.png",
                    },
                    new List<string>() {
                        "fix.tpl.mm72.png",
                        "fix.tpl.mm73.png",
                        "fix.tpl.mm74.png",
                    },
                };
                List<ColorSwizzleData> SwizData = new List<ColorSwizzleData>();
                for (int i = 0; i < HUDTex.Count; i++)
                {
                    SwizData.Add(new ColorSwizzleData(rand));
                }

                for (int i = 0; i < file.Textures.Count; i++)
                {
                    for (int x = 0; x < HUDTex.Count; x++)
                    {
                        if (HUDTex[x].Contains(file.Textures[i].Name))
                        {
                            Ray3_Common.Recolor_Texture_File(file.Textures[i].FullName, SwizData[x]);
                        }
                    }
                }
            }

            if (file.Name.ToLower().Contains("lodmeca.tpl"))
            {
                for (int i = 0; i < file.Textures.Count; i++)
                {
                    if (file.Textures[i].Name.Contains("lodmeca.tpl.png"))
                    {
                        ColorSwizzleData Swiz = new ColorSwizzleData(rand);
                        Ray3_Common.Recolor_Texture_File(file.Textures[i].FullName, Swiz);
                    }
                }
            }
            if (file.Name.ToLower().Contains("lodps2_"))
            {
                for (int i = 0; i < file.Textures.Count; i++)
                {
                    if (file.Textures[i].Name.Contains("lodps2_"))
                    {
                        ColorSwizzleData Swiz = new ColorSwizzleData(rand);
                        Ray3_Common.Recolor_Texture_File(file.Textures[i].FullName, Swiz);
                    }
                }
            }
        }
    }
}
