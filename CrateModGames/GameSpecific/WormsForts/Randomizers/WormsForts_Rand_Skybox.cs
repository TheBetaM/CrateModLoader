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
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(XOM_File file)
        {
            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is XFortsExportedData cont)
                {
                    cont.SkyBoxType = (XFortsExportedData.SkyboxType)rand.Next(20);
                }
            }
        }
    }
}
