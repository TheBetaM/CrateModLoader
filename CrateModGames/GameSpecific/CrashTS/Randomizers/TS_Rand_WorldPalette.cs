using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: test, randomize per file
    public class TS_Rand_WorldPalette : ModStruct<ChunkInfoSM>
    {
        public override string Name => Twins_Text.Rand_WorldPalette;
        public override string Description => Twins_Text.Rand_WorldPaletteDesc;

        private ColorSwizzleData Swiz;

        public override void BeforeModPass()
        {
            Swiz = new ColorSwizzleData(new Random(ModLoaderGlobals.RandomizerSeed));
        }

        public override void ModPass(ChunkInfoSM info)
        {
            TwinsFile SM_Archive = info.File;

            if (SM_Archive.Type != TwinsFile.FileType.SM2) return;
            if (!SM_Archive.ContainsItem(6)) return;
            TwinsSection section = SM_Archive.GetItem<TwinsSection>(6);
            if (section.ContainsItem((uint)RM_Graphics_Sections.Models) && section.Records.Count > 0)
            {
                TwinsSection model_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Models);

                foreach (TwinsItem item in model_section.Records)
                {
                    Model model = (Model)item;
                    for (int i = 0; i < model.SubModels.Count; i++)
                    {
                        for (int g = 0; g < model.SubModels[i].Groups.Count; g++)
                        {
                            for (int v = 0; v < model.SubModels[i].Groups[g].VData.Length; v++)
                            {
                                float maxVal = Math.Max(model.SubModels[i].Groups[g].VData[v].R, model.SubModels[i].Groups[g].VData[v].G);
                                maxVal = Math.Max(maxVal, model.SubModels[i].Groups[g].VData[v].B);
                                maxVal = maxVal / 255f;

                                int r = model.SubModels[i].Groups[g].VData[v].R;
                                int gr = model.SubModels[i].Groups[g].VData[v].G;
                                int b = model.SubModels[i].Groups[g].VData[v].B;

                                model.SubModels[i].Groups[g].VData[v].R = (byte)((Swiz.r_r * r + Swiz.r_g * gr + Swiz.r_b * b) / Swiz.r_s);
                                model.SubModels[i].Groups[g].VData[v].G = (byte)((Swiz.g_r * r + Swiz.g_g * gr + Swiz.g_b * b) / Swiz.g_s);
                                model.SubModels[i].Groups[g].VData[v].B = (byte)((Swiz.b_r * r + Swiz.b_g * gr + Swiz.b_b * b) / Swiz.b_s);
                            }
                        }
                    }
                }

            }
        }

        class ColorSwizzleData
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
    }
}
