using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Rand_WorldPalette : ModStruct<CtrScene>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(CtrScene lev)
        {
            ColorSwizzleData Swiz = new ColorSwizzleData(rand);
            foreach (Vertex v in lev.verts)
            {
                Vector4b inputColor = v.Color;
                float intensity = Math.Max(v.Color.X, v.Color.Y);
                intensity = Math.Max(v.Color.Z, intensity);

                int r = inputColor.X;
                int g = inputColor.Y;
                int b = inputColor.Z;

                Color targetColor = Color.FromArgb(255,
                    (int)((Swiz.r_r * r + Swiz.r_g * g + Swiz.r_b * b) / Swiz.r_s),
                    (int)((Swiz.g_r * r + Swiz.g_g * g + Swiz.g_b * b) / Swiz.g_s),
                    (int)((Swiz.b_r * r + Swiz.b_g * g + Swiz.b_b * b) / Swiz.b_s));

                v.SetColor(new Vector4b(targetColor), Vcolor.Default);
                v.SetColor(new Vector4b(targetColor), Vcolor.Morph);
            }
            foreach (VertexAnim v in lev.vertanims)
            {
                Vector4b inputColor = v.color;
                float intensity = Math.Max(v.color.X, v.color.Y);
                intensity = Math.Max(v.color.Z, intensity);

                int r = inputColor.X;
                int g = inputColor.Y;
                int b = inputColor.Z;

                Color targetColor = Color.FromArgb(255,
                    (int)((Swiz.r_r * r + Swiz.r_g * g + Swiz.r_b * b) / Swiz.r_s),
                    (int)((Swiz.g_r * r + Swiz.g_g * g + Swiz.g_b * b) / Swiz.g_s),
                    (int)((Swiz.b_r * r + Swiz.b_g * g + Swiz.b_b * b) / Swiz.b_s));

                v.color.X = targetColor.R;
                v.color.Y = targetColor.G;
                v.color.Z = targetColor.B;
            }
        }
    }
}
