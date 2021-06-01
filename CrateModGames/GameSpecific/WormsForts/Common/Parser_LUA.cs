using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Parser_LUA : ModParser<LUA_File>
    {
        public Parser_LUA(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".LUA" };

        public override LUA_File LoadObject(string filePath)
        {
            return new LUA_File(filePath);
        }

        public override void SaveObject(LUA_File thing, string filePath)
        {
            thing.Write(filePath);
        }
    }
}
