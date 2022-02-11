using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashMoMDS
{
    public class Parser_MXB : ModParser<MXB_File>
    {
        public Parser_MXB(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".MXB" };

        public override bool SecondarySkip => true;
        public override List<string> SecondaryList => new List<string>() // to fix
        {
            "texts_c01_000.mxb",
            "texts_c01_004.mxb",
        };

        public override MXB_File LoadObject(string filePath)
        {
            MXB_File mxb = new MXB_File();
            try
            {
                mxb.Read(filePath);
            }
            catch
            {
                Console.WriteLine("HELP " + filePath);
            }

            return mxb;
        }

        public override void SaveObject(MXB_File thing, string filePath)
        {
            try
            {
                thing.Write(filePath);
            }
            catch
            {
                Console.WriteLine("SAVE ME " + filePath);
            }
        }
    }
}
