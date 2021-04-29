using System.Collections.Generic;
using Pure3D;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class Parser_P3D : ModParser<File>
    {
        public Parser_P3D(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".P3D" };

        public override File LoadObject(string filePath)
        {
            File lev = new File();
            lev.Load(filePath);
            return lev;
        }

        public override void SaveObject(File thing, string filePath)
        {
            thing.Save(filePath);
        }
    }
}
