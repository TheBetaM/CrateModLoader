using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_ObjectColors : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (ModelEntry e in pair.nsf.GetEntries<ModelEntry>())
            {
                for (int i = 0; i < e.Colors.Count; ++i)
                {
                    e.Colors[i] = new SceneryColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), 0);
                }
            }
        }
    }
}
