using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.Worms4
{
    public class W4_Metadata : ModStruct<XOM_File>
    {
        public override void ModPass(XOM_File file)
        {
            XStringResourceDetails cont = file.GetContainer<XStringResourceDetails>("FETIP.Return");
            if (cont == null) return;

            cont.Value = "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + " Seed: " + ModLoaderGlobals.RandomizerSeed;
        }
    }
}
