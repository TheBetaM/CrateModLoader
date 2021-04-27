using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Rand_IcyTracks : ModStruct<Scene>
    {
        public override string Name => "Random Tracks Have Ice Physics";

        private Random rand;
        private bool isRandom;

        public CTR_Rand_IcyTracks(bool isrand)
        {
            isRandom = isrand;
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
