using System.Collections.Generic;
using Twinsanity;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class Parser_RM : ModParser<ChunkInfoRM>
    {
        private ConsoleMode console;

        public Parser_RM(Modder mod, ConsoleMode cons) : base(mod)
        {
            console = cons;
            if (mod.ModderIsPreloading)
            {
                ForceParser = true;
            }
        }

        public override List<string> Extensions => new List<string>() { ".RM2", ".RMX" };
        public override bool IsLevelFile => true;

        public override ChunkInfoRM LoadObject(string filePath)
        {
            TwinsFile Archive = new TwinsFile();
            if (console == ConsoleMode.XBOX)
            {
                Archive.LoadFile(filePath, TwinsFile.FileType.RMX);
            }
            else
            {
                Archive.LoadFile(filePath, TwinsFile.FileType.RM2);
            }
            ChunkInfoRM chunk = new ChunkInfoRM(Archive, Twins_Common.ChunkPathToType(filePath));

            return chunk;
        }

        public override void SaveObject(ChunkInfoRM thing, string filePath)
        {
            thing.File.SaveFile(filePath);
        }

        public override LevelBase LoadLevel(ChunkInfoRM data)
        {
            Level_RM Lev = new Level_RM();
            Lev.Load(data);
            return Lev;
        }
    }
}
