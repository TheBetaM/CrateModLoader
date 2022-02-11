using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Rand_MoonGravity : ModStruct<CtrScene>
    {
        private Random rand;
        private bool isRandom;

        public CTR_Rand_MoonGravity()
        {
            isRandom = CTR_Props_Main.Option_Rand_MoonGravity.Enabled;
        }

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(CtrScene lev)
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
