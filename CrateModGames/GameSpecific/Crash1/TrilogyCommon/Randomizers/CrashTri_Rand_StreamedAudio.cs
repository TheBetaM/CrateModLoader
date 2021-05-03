using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_StreamedAudio : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            var streams = pair.nsf.GetEntries<SpeechEntry>();
            var eids = new List<int>();
            foreach (var stream in streams)
            {
                eids.Add(stream.EID);
            }
            foreach (SpeechEntry stream in streams)
            {
                int i = rand.Next(eids.Count);
                stream.EID = eids[i];
                eids.RemoveAt(i);
            }
        }
    }
}
