using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_Rand_WorldPalette : ModStruct<XOM_File>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(XOM_File file)
        {
            string level = Path.GetFileName(file.FileName);
            if (!WormsForts_Common.Levels_All.Contains(level)) return;

            ColorSwizzleData Swiz = new ColorSwizzleData(rand);

            foreach (XColor4ubSet cont in file.GetContainers<XColor4ubSet>())
            {
                for (int col = 0; col < cont.ColorSet.Count; col++)
                {
                    int r = cont.ColorSet[col].R;
                    int g = cont.ColorSet[col].G;
                    int b = cont.ColorSet[col].B;

                    cont.ColorSet[col].R = (byte)((Swiz.r_r * r + Swiz.r_g * g + Swiz.r_b * b) / Swiz.r_s);
                    cont.ColorSet[col].G = (byte)((Swiz.g_r * r + Swiz.g_g * g + Swiz.g_b * b) / Swiz.g_s);
                    cont.ColorSet[col].B = (byte)((Swiz.b_r * r + Swiz.b_g * g + Swiz.b_b * b) / Swiz.b_s);
                }
            }
        }
    }
}
