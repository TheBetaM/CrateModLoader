using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_Metadata : ModStruct<XOM_File>
    {
        public override void ModPass(XOM_File file)
        {
            XStringResourceDetails cont = file.GetContainer<XStringResourceDetails>("FE.Copyright");
            if (cont == null) return;

            cont.Value = "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion
                    + " Seed: " + ModLoaderGlobals.RandomizerSeed
                    + "\n" + cont.Value;
        }
    }
}
