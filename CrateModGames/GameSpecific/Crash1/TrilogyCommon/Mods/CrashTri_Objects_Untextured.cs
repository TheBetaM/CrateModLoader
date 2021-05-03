using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    //unfinished
    public class CrashTri_Objects_Untextured : ModStruct<NSF_Pair>
    {
        public override void ModPass(NSF_Pair pair)
        {
            // shouldn't this cause some serious memory read corruption? lol
            foreach (ModelEntry e in pair.nsf.GetEntries<ModelEntry>())
            {
                e.Textures.Clear();
                e.AnimatedTextures.Clear();
            }
        }
    }
}
