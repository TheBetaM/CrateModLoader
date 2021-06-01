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
            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is XStringResourceDetails cont)
                {
                    if (file.Strings[cont.NameKey.RawValue] == "Lang.MainMenu")
                    {
                        file.Strings[cont.ValueKey.RawValue] = "CML " + ModLoaderGlobals.ProgramVersion + " Seed: " + ModLoaderGlobals.RandomizerSeed;
                    }
                }
            }
        }
    }
}
