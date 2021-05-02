using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public class Parser_RM : ModParser<ChunkInfoRM>
    {
        private ConsoleMode console;

        public Parser_RM(Modder mod, ConsoleMode cons) : base(mod)
        {
            console = cons;
        }

        public override List<string> Extensions => new List<string>() { ".RM" };
        public override List<string> SecondaryList => new List<string>() { "Default.rm" }; // particle data needs fixing, but there's not much in it anyway

        public override ChunkInfoRM LoadObject(string filePath)
        {
            TwinsFile Archive = new TwinsFile();
            Archive.LoadFile(filePath, TwinsFile.FileType.MonkeyBallRM);
            ChunkInfoRM chunk = new ChunkInfoRM(Archive, console);

            return chunk;
        }

        public override void SaveObject(ChunkInfoRM thing, string filePath)
        {
            thing.File.SaveFile(filePath);
        }
    }
}
