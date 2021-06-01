using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_FortWeaponsAnywhere : ModStruct<XOM_File>
    {
        public override void ModPass(XOM_File file)
        {
            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is WeaponDataCtr cont)
                {
                    cont.FiringPlatformRequired = 0;
                    cont.CanBeFiredFromAnywhere.Value = true;
                }
            }
        }
    }
}
