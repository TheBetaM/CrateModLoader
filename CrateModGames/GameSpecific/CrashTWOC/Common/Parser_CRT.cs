using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class Parser_CRT : ModParser<TWOC_File_CRT>
    {
        public Parser_CRT(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".CRT" };

        public override TWOC_File_CRT LoadObject(string filePath)
        {
            TWOC_File_CRT crt = new TWOC_File_CRT();
            crt.Load(filePath);
            return crt;
        }

        public override void SaveObject(TWOC_File_CRT thing, string filePath)
        {
            thing.Save(filePath);
        }
    }
}
