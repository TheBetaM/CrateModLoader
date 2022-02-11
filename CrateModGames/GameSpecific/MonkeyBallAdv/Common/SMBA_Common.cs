using System;
using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{

    public class ChunkInfoRM
    {
        public TwinsFile File;
        //public ChunkType Type;
        public ConsoleMode Console;

        public ChunkInfoRM(TwinsFile f, ConsoleMode c)//, ChunkType t)
        {
            File = f;
            Console = c;
        }

    }
    public class ChunkInfoSM
    {
        public TwinsFile File;
        //public ChunkType Type;
        public ConsoleMode Console;

        public ChunkInfoSM(TwinsFile f, ConsoleMode c)//, ChunkType t)
        {
            File = f;
            Console = c;
        }

    }
    public class ChunkInfoFull
    {
        public TwinsFile FileRM;
        public TwinsFile FileSM;
        //public ChunkType Type;
        public ConsoleMode Console;

        public ChunkInfoFull(TwinsFile rm, TwinsFile sm, ConsoleMode c)//, ChunkType t)
        {
            FileRM = rm;
            FileSM = sm;
            //Type = t;
            Console = c;
        }

    }

    public static class SMBA_Common
    {
        public static string GetDataPath(GenericModStruct mod)
        {
            return System.IO.Path.Combine(mod.ExtractedPath, @"MB\");
        }
    }
}
