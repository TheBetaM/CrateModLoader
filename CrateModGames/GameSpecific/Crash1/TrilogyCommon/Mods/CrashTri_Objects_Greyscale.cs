using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Objects_Greyscale : ModStruct<NSF_Pair>
    {
        public override string Name => CrashTri_Text.Mod_GreyscaleObjects;
        public override string Description => CrashTri_Text.Mod_GreyscaleObjectsDesc;

        public override void ModPass(NSF_Pair pair)
        {
            foreach (ModelEntry e in pair.nsf.GetEntries<ModelEntry>())
            {
                for (int i = 0; i < e.Colors.Count; ++i)
                {
                    byte intensity = Math.Max(e.Colors[i].Red, e.Colors[i].Green);
                    intensity = Math.Max(intensity, e.Colors[i].Blue);
                    e.Colors[i] = new SceneryColor(intensity, intensity, intensity, 0);
                }
            }
        }
    }
}
