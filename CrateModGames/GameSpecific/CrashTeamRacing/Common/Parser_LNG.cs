using System.Collections.Generic;
using CTRFramework.Lang;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Parser_LNG : ModParser<LNG>
    {
        public Parser_LNG(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".LNG" };

        public override LNG LoadObject(string filePath)
        {
            return LNG.FromFile(filePath);
        }

        public override void SaveObject(LNG thing, string filePath)
        {
             thing.Save(filePath);
        }
    }
}
