using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Rand_CrateEffects : ModStruct<CtrScene>
    {
        private List<CtrThreadID> ReplaceEvents = new List<CtrThreadID>()
        {
            CtrThreadID.CrateWeapon,
            CtrThreadID.CrateFruit,
            CtrThreadID.CrateRelic1,
            CtrThreadID.CrateRelic2,
            CtrThreadID.CrateRelic3,
        };
        private List<CtrThreadID> PossibleEvents = new List<CtrThreadID>()
        {
            CtrThreadID.CrateWeapon,
            CtrThreadID.SingleFruit,
            CtrThreadID.CrateFruit,
            CtrThreadID.CavesTurtle,
            CtrThreadID.CrateRelic1,
            CtrThreadID.CrateRelic2,
            CtrThreadID.CrateRelic3,
        };

        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(CtrScene lev)
        {
            foreach (PickupHeader pick in lev.pickups)
            {
                if (ReplaceEvents.Contains(pick.ThreadID))
                {
                    pick.ThreadID = PossibleEvents[rand.Next(PossibleEvents.Count)];
                }
            }
        }
    }
}
