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
            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is XStringResourceDetails cont)
                {
                    if (file.Strings[cont.NameKey.RawValue] == "FETIP.Return")
                    {
                        file.Strings[cont.ValueKey.RawValue] = "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + " Seed: " + ModLoaderGlobals.RandomizerSeed;
                    }
                }
            }
        }
    }
}
