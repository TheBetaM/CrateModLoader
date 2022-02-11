using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CTRFramework.Lang;
using CTRFramework.Shared;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class CTR_Metadata : ModStruct<LNG>
    {
        public override void ModPass(LNG file)
        {
            for (int i = 0; i < file.Entries.Count; i++)
            {
                if (file.Entries[i].Contains("LOADING.."))
                {
                    file.Entries[i] = "CML " + ModLoaderGlobals.ProgramVersion + "|" + "SEED: " + ModLoaderGlobals.RandomizerSeed;
                }
            }
        }
    }
}
