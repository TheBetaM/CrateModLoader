using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public class Parser_SM : ModParser<ChunkInfoSM>
    {
        private ConsoleMode console;

        public Parser_SM(Modder mod, ConsoleMode cons) : base(mod)
        {
            console = cons;
        }

        public override List<string> Extensions => new List<string>() { ".SM" };

        public override ChunkInfoSM LoadObject(string filePath)
        {
            TwinsFile Archive = new TwinsFile();
            Archive.LoadFile(filePath, TwinsFile.FileType.MonkeyBallSM);
            ChunkInfoSM chunk = new ChunkInfoSM(Archive, console);

            return chunk;
        }

        public override void SaveObject(ChunkInfoSM thing, string filePath)
        {
            thing.File.SaveFile(filePath);
        }
    }
}
