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
            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is XStringResourceDetails cont)
                {
                    if (file.Strings[cont.NameKey.RawValue] == "FE.Copyright")
                    {
                        file.Strings[cont.ValueKey.RawValue] = "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion
                            + " Seed: " + ModLoaderGlobals.RandomizerSeed
                            + "\n" + file.Strings[cont.ValueKey.RawValue];
                    }
                }
            }
        }
    }
}
