using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Scenery_Invisible : ModStruct<NSF_Pair>
    {
        public override string Name => "Invisible World (Beta)";
        public override string Description => "The scenery is invisible.";

        public override void ModPass(NSF_Pair pair)
        {
            // Well, "invisible" without being disruptive to stability
            foreach (SceneryEntry entry in pair.nsf.GetEntries<SceneryEntry>())
            {
                entry.XOffset = int.MaxValue - 1;
                entry.YOffset = int.MinValue + 1;
                entry.ZOffset = int.MaxValue - 1;
            }
            foreach (NewSceneryEntry entry in pair.nsf.GetEntries<NewSceneryEntry>())
            {
                entry.XOffset = int.MaxValue - 1;
                entry.YOffset = int.MinValue + 1;
                entry.ZOffset = int.MaxValue - 1;
            }
            foreach (OldSceneryEntry entry in pair.nsf.GetEntries<OldSceneryEntry>())
            {
                entry.XOffset = int.MaxValue - 1;
                entry.YOffset = int.MinValue + 1;
                entry.ZOffset = int.MaxValue - 1;
            }
        }
    }
}
