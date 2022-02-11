using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.SonicRivals
{
    public class SonicRivals_Metadata : ModStruct<TXTFile>
    {
        public override void ModPass(TXTFile file)
        {
            for (int i = 0; i < file.Lines.Count; i++)
            {
                if (file.Lines[i].StartsWith("This game saves data automatically at certain points.[N]"))
                {
                    file.Lines[i] = "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + " Seed: " + ModLoaderGlobals.RandomizerSeed + "[N]" + file.Lines[i];
                }
            }
        }
    }
}
