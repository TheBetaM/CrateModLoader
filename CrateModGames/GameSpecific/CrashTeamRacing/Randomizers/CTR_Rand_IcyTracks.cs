using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Rand_IcyTracks : ModStruct<Scene>
    {
        private Random rand;
        private bool isRandom;

        public CTR_Rand_IcyTracks()
        {
            isRandom = CTR_Props_Main.Option_Rand_IcyTracks.Enabled;
        }

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(Scene lev)
        {
            if (!isRandom || rand.Next(2) == 0)
            {
                foreach (QuadBlock quad in lev.quads)
                {
                    if (quad.quadFlags.HasFlag(QuadFlags.Ground))
                    {
                        quad.terrainFlag = TerrainFlags.Ice;
                    }
                }
            }
        }
    }
}
