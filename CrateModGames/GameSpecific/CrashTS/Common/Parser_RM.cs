using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class Parser_RM : ModParser<ChunkInfoRM>
    {
        private TwinsFile.FileType RM_Type;

        public Parser_RM(Modder mod, TwinsFile.FileType RMtype) : base(mod)
        {
            RM_Type = RMtype;
        }

        public override List<string> Extensions => new List<string>() { ".RM2", ".RMX" };

        public override ChunkInfoRM LoadObject(string filePath)
        {
            TwinsFile Archive = new TwinsFile();
            Archive.LoadFile(filePath, RM_Type);
            ChunkInfoRM chunk = new ChunkInfoRM(Archive, Twins_Common.ChunkPathToType(filePath));

            return chunk;
        }

        public override void SaveObject(ChunkInfoRM thing, string filePath)
        {
            thing.File.SaveFile(filePath);
        }
    }
}
