using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class Parser_SM : ModParser<ChunkInfoSM>
    {
        private ConsoleMode console;

        public Parser_SM(Modder mod, ConsoleMode cons) : base(mod)
        {
            console = cons;
        }

        public override List<string> Extensions => new List<string>() { ".SM2", ".SMX" };

        public override ChunkInfoSM LoadObject(string filePath)
        {
            TwinsFile Archive = new TwinsFile();
            if (console == ConsoleMode.XBOX)
            {
                Archive.LoadFile(filePath, TwinsFile.FileType.SMX);
            }
            else
            {
                Archive.LoadFile(filePath, TwinsFile.FileType.SM2);
            }
            ChunkInfoSM chunk = new ChunkInfoSM(Archive, Twins_Common.ChunkPathToType(filePath));

            return chunk;
        }

        public override void SaveObject(ChunkInfoSM thing, string filePath)
        {
            thing.File.SaveFile(filePath);
        }
    }
}
