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
            foreach (WeaponDataCtr cont in file.GetContainers<WeaponDataCtr>())
            {
                cont.FiringPlatformRequired = 0;
                cont.CanBeFiredFromAnywhere.Value = true;
            }
        }
    }
}
