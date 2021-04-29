using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class Parser_WMP : ModParser<TWOC_File_WMP>
    {
        public Parser_WMP(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".WMP" };

        public override TWOC_File_WMP LoadObject(string filePath)
        {
            TWOC_File_WMP wmp = new TWOC_File_WMP();
            wmp.Load(filePath);
            return wmp;
        }

        public override void SaveObject(TWOC_File_WMP thing, string filePath)
        {
            thing.Save(filePath);
        }
    }
}
