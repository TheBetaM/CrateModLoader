using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Scenery_Greyscale : ModStruct<NSF_Pair>
    {
        public override string Name => CrashTri_Text.Mod_GreyscaleWorld;
        public override string Description => CrashTri_Text.Mod_GreyscaleWorldDesc;

        public override void ModPass(NSF_Pair pair)
        {
            foreach (SceneryEntry entry in pair.nsf.GetEntries<SceneryEntry>())
            {
                for (int i = 0; i < entry.Colors.Count; i++)
                {
                    SceneryColor color = entry.Colors[i];
                    byte r = color.Red;
                    byte g = color.Green;
                    byte b = color.Blue;
                    byte avg = (byte)((r + g + b) / 3);
                    r = avg;
                    g = avg;
                    b = avg;
                    entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                }
            }
            foreach (NewSceneryEntry entry in pair.nsf.GetEntries<NewSceneryEntry>())
            {
                for (int i = 0; i < entry.Colors.Count; i++)
                {
                    SceneryColor color = entry.Colors[i];
                    byte r = color.Red;
                    byte g = color.Green;
                    byte b = color.Blue;
                    byte avg = (byte)((r + g + b) / 3);
                    r = avg;
                    g = avg;
                    b = avg;
                    entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                }
            }
            foreach (OldSceneryEntry entry in pair.nsf.GetEntries<OldSceneryEntry>())
            {
                for (int i = 0; i < entry.Vertices.Count; i++)
                {
                    OldSceneryVertex color = entry.Vertices[i];
                    byte r = color.Red;
                    byte g = color.Green;
                    byte b = color.Blue;
                    byte avg = (byte)((r + g + b) / 3);
                    r = avg;
                    g = avg;
                    b = avg;
                    entry.Vertices[i] = new OldSceneryVertex(color.X, color.Y, color.Z, r, g, b, color.FX);
                }
            }
        }
    }
}
