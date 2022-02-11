using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashBB.Mods
{
    public class CrashBB_Metadata : ModStruct<LNG_File>
    {
        public override void ModPass(LNG_File lng)
        {
            if (lng.FileName.Contains("data_001"))
            {
                
            }
        }
    }
}
