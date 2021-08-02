using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_Rand_Skybox : ModStruct<XOM_File>
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

            cont.SkyBoxType = (XFortsExportedData.SkyboxType)rand.Next(20);
        }
    }
}
