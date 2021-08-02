using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Rand_CrateEffects : ModStruct<Scene>
    {
        private List<CTREvent> ReplaceEvents = new List<CTREvent>()
        {
            CTREvent.CrateWeapon,
            CTREvent.CrateFruit,
            CTREvent.CrateRelic1,
            CTREvent.CrateRelic2,
            CTREvent.CrateRelic3,
        };
        private List<CTREvent> PossibleEvents = new List<CTREvent>()
        {
            CTREvent.CrateWeapon,
            CTREvent.SingleFruit,
            CTREvent.CrateFruit,
            CTREvent.StateTurleJump,
            CTREvent.CrateRelic1,
            CTREvent.CrateRelic2,
            CTREvent.CrateRelic3,
        };

        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(Scene lev)
        {
            foreach (PickupHeader pick in lev.pickups)
            {
                if (ReplaceEvents.Contains(pick.Event))
                {
                    pick.Event = PossibleEvents[rand.Next(PossibleEvents.Count)];
                }
            }
        }
    }
}
