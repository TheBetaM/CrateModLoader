using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_ObjectPalette : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(NSF_Pair pair)
        {
            int r_r = rand.Next(2);
            int r_g = rand.Next(2);
            int r_b = rand.Next(2);
            int r_s = r_r + r_g + r_b;
            int g_r = rand.Next(2);
            int g_g = rand.Next(2);
            int g_b = rand.Next(2);
            int g_s = g_r + g_g + g_b;
            int b_r = rand.Next(2);
            int b_g = rand.Next(2);
            int b_b = rand.Next(2);
            int b_s = b_r + b_g + b_b;

            if (r_s == 0) r_s = 1;
            if (g_s == 0) g_s = 1;
            if (b_s == 0) b_s = 1;

            foreach (ModelEntry e in pair.nsf.GetEntries<ModelEntry>())
            {
                for (int i = 0; i < e.Colors.Count; ++i)
                {
                    SceneryColor color = e.Colors[i];
                    int r = color.Red;
                    int g = color.Green;
                    int b = color.Blue;
                    e.Colors[i] = new SceneryColor(
                        (byte)((r_r * r + r_g * g + r_b * b) / r_s),
                        (byte)((g_r * r + g_g * g + g_b * b) / g_s),
                        (byte)((b_r * r + b_g * g + b_b * b) / b_s),
                        color.Extra
                    );
                }
            }
        }
    }
}
