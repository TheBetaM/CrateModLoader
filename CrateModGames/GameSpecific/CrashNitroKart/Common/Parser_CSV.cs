using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class Parser_CSV : ModParser<CSV>
    {
        public Parser_CSV(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".CSV" };

        public override CSV LoadObject(string filePath)
        {
            return new CSV(filePath);
        }

        public override void SaveObject(CSV thing, string filePath)
        {
            thing.Write(filePath);
        }
    }
}
