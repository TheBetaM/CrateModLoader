using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Scenery_Rainbow : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (SceneryEntry entry in pair.nsf.GetEntries<SceneryEntry>())
            {
                for (int i = 0; i < entry.Colors.Count; i++)
                {
                    SceneryColor color = entry.Colors[i];
                    byte r = (byte)rand.Next(256);
                    byte g = (byte)rand.Next(256);
                    byte b = (byte)rand.Next(256);
                    entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                }
            }
            foreach (NewSceneryEntry entry in pair.nsf.GetEntries<NewSceneryEntry>())
            {
                for (int i = 0; i < entry.Colors.Count; i++)
                {
                    SceneryColor color = entry.Colors[i];
                    byte r = (byte)rand.Next(256);
                    byte g = (byte)rand.Next(256);
                    byte b = (byte)rand.Next(256);
                    entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                }
            }
            foreach (OldSceneryEntry entry in pair.nsf.GetEntries<OldSceneryEntry>())
            {
                for (int i = 0; i < entry.Vertices.Count; i++)
                {
                    OldSceneryVertex color = entry.Vertices[i];
                    byte r = (byte)rand.Next(256);
                    byte g = (byte)rand.Next(256);
                    byte b = (byte)rand.Next(256);
                    entry.Vertices[i] = new OldSceneryVertex(color.X, color.Y, color.Z, r, g, b, color.FX);
                }
            }
        }
    }
}
