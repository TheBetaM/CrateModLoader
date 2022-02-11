using System.Collections.Generic;
using System.IO;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Parser_CTR : ModParser<CtrModel>
    {
        public Parser_CTR(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".CTR" };

        public override CtrModel LoadObject(string filePath)
        {
            return CtrModel.FromFile(filePath);
        }

        public override void SaveObject(CtrModel thing, string filePath)
        {
            string loneFile = Path.GetFileName(filePath);
            thing.Save(filePath.Substring(0, filePath.Length - loneFile.Length), loneFile);
        }
    }
}
