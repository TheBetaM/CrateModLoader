using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_RemoveGodWormAnimations : ModStruct<XOM_File>
    {
        public override void ModPass(XOM_File file)
        {
            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is WeaponDataCtr cont)
                {
                    if (cont.IsGodWeapon.Value)
                    {
                        cont.IsGodWeapon.Value = false;
                        cont.GodWormEndAnimationName.Value = 0;
                        cont.GodWormStartAnimationName.Value = 0;
                    }
                }
            }
        }
    }
}
