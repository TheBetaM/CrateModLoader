using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_AllCratesAreWumpaCrates : ModStruct<Scene>
    {
        public override void ModPass(Scene lev)
        {
            uint FruitModel = 0;
            foreach (PickupHeader pick in lev.pickups)
            {
                if (pick.Event == CTREvent.CrateFruit)
                {
                    FruitModel = pick.ModelOffset;
                }
            }
            foreach (PickupHeader pick in lev.pickups)
            {
                if (pick.Event == CTREvent.CrateWeapon)
                {
                    pick.Event = CTREvent.CrateFruit;
                    if (FruitModel != 0)
                    {
                        pick.ModelOffset = FruitModel;
                    }
                }
            }
        }
    }
}
