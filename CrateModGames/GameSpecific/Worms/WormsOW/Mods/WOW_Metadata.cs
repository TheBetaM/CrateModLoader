using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsOW
{
    public class WOW_Metadata : ModStruct<XOM_File>
    {
        public override void ModPass(XOM_File file)
        {
            XStringResourceDetails cont = file.GetContainer<XStringResourceDetails>("FEText.PressStart");
            if (cont == null) return;

            cont.Value = "CML " + ModLoaderGlobals.ProgramVersion
                    + " Seed: " + ModLoaderGlobals.RandomizerSeed
                    + "\n" + cont.Value;
        }
    }
}
