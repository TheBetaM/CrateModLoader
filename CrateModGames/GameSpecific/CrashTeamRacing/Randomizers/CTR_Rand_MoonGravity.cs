using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Rand_MoonGravity : ModStruct<Scene>
    {
        public override string Name => "Random Tracks Have Moon Gravity";

        private Random rand;
        private bool isRandom;

        public CTR_Rand_MoonGravity(bool isrand)
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
                        quad.quadFlags |= QuadFlags.MoonGravity;
                    }
                }
            }
        }
    }
}
