using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_Rand_WaterType : ModStruct<XOM_File>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(XOM_File file)
        {
            XFortsExportedData cont = file.GetContainer<XFortsExportedData>();
            if (cont == null) return;

            cont.WaterType = (XFortsExportedData.WaterTypes)rand.Next(4);
        }
    }
}
