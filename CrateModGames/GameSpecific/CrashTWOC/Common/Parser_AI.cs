using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class Parser_AI : ModParser<TWOC_File_AI>
    {
        public Parser_AI(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".AI" };

        public override TWOC_File_AI LoadObject(string filePath)
        {
            TWOC_File_AI ai = new TWOC_File_AI();
            ai.Load(filePath);
            return ai;
        }

        public override void SaveObject(TWOC_File_AI thing, string filePath)
        {
            thing.Save(filePath);
        }
    }
}
