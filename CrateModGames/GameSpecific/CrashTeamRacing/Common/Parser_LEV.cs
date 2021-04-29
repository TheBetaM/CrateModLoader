using System.Collections.Generic;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Parser_LEV : ModParser<Scene>
    {
        public Parser_LEV(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".LEV" };

        public override Scene LoadObject(string filePath)
        {
            return Scene.FromFile(filePath);
        }

        public override void SaveObject(Scene thing, string filePath)
        {
            thing.Write(filePath);
            thing.Dispose();
        }
    }
}
