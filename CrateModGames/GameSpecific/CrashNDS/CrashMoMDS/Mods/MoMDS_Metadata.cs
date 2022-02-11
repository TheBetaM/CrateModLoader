using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashMoMDS.Mods
{
    public class MoMDS_Metadata : ModStruct<MXB_File>
    {
        public override void ModPass(MXB_File mxb)
        {
            if (mxb.FileName.Contains("texts_c01_007"))
            {
                mxb.Texts[1] = "#CRATE MOD LOADER " + ModLoaderGlobals.ProgramVersion.ToUpper();
                mxb.Texts[2] = "#SEED: " + ModLoaderGlobals.RandomizerSeed;
            }
        }
    }
}
