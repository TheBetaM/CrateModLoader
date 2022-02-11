using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.Worms3D
{
    public class W3D_Metadata : ModStruct<XOM_File>
    {
        public override void ModPass(XOM_File file)
        {
            XStringResourceDetails cont = file.GetContainer<XStringResourceDetails>("Lang.MainMenu");
            if (cont == null) return;

            cont.Value = "CML " + ModLoaderGlobals.ProgramVersion + " Seed: " + ModLoaderGlobals.RandomizerSeed;
        }
    }
}
