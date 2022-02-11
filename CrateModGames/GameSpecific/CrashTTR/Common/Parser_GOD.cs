using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class Parser_GOD : ModParser<GOD_File>
    {
        public Parser_GOD(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".GOD" };

        public override GOD_File LoadObject(string filePath)
        {
            return new GOD_File(filePath);
        }

        public override void SaveObject(GOD_File thing, string filePath)
        {
            thing.Write(filePath);
        }
    }
}
