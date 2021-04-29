using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class Parser_SM : ModParser<ChunkInfoSM>
    {
        private TwinsFile.FileType SM_Type;

        public Parser_SM(Modder mod, TwinsFile.FileType SMtype) : base(mod)
        {
            SM_Type = SMtype;
        }

        public override List<string> Extensions => new List<string>() { ".SM2", ".SMX" };

        public override ChunkInfoSM LoadObject(string filePath)
        {
            TwinsFile Archive = new TwinsFile();
            Archive.LoadFile(filePath, SM_Type);
            ChunkInfoSM chunk = new ChunkInfoSM(Archive, Twins_Common.ChunkPathToType(filePath));

            return chunk;
        }

        public override void SaveObject(ChunkInfoSM thing, string filePath)
        {
            thing.File.SaveFile(filePath);
        }
    }
}
