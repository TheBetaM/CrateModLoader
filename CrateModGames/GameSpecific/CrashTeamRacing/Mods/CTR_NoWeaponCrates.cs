using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_AllCratesAreWumpaCrates : ModStruct<CtrScene>
    {
        public override void ModPass(CtrScene lev)
        {
            PsxPtr FruitModel = PsxPtr.Zero;
            foreach (PickupHeader pick in lev.pickups)
            {
                if (pick.ThreadID == CtrThreadID.CrateFruit)
                {
                    FruitModel = pick.ModelOffset;
                }
            }
            foreach (PickupHeader pick in lev.pickups)
            {
                if (pick.ThreadID == CtrThreadID.CrateWeapon)
                {
                    pick.ThreadID = CtrThreadID.CrateFruit;
                    if (FruitModel != PsxPtr.Zero)
                    {
                        pick.ModelOffset = FruitModel;
                    }
                }
            }
        }
    }
}
