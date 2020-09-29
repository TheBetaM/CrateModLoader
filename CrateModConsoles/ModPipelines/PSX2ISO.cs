using System.IO;

namespace CrateModLoader.Tools
{
    public static class PSX2ISO
    {
        public static void Run(Stream i, Stream o)
        {
            byte[] buf = new byte[2048];
            while (i.Position < i.Length)
            {
                i.Read(buf, 0, 24);
                i.Read(buf, 0, 2048); // meaningful sector data
                o.Write(buf, 0, 2048);
                i.Read(buf, 0, 280);
            }
        }
    }

    // written by chekwob https://github.com/cbhacks/CrashEdit/blob/deprecate/CrashEdit/ISO2PSX.cs
    public static class ISO2PSX
    {
        public static void Run(Stream i, Stream o)
        {
            byte[] buffer = new byte[2352];
            buffer[0] = 0;
            buffer[1] = 0xFF;
            buffer[2] = 0xFF;
            buffer[3] = 0xFF;
            buffer[4] = 0xFF;
            buffer[5] = 0xFF;
            buffer[6] = 0xFF;
            buffer[7] = 0xFF;
            buffer[8] = 0xFF;
            buffer[9] = 0xFF;
            buffer[10] = 0xFF;
            buffer[11] = 0;
            buffer[15] = 2;
            int minutes = 0;
            int seconds = 2;
            int frames = 0;
            while (true)
            {
                buffer[12] = (byte)((minutes / 10 * 16) | (minutes % 10));
                buffer[13] = (byte)((seconds / 10 * 16) | (seconds % 10));
                buffer[14] = (byte)((frames / 10 * 16) | (frames % 10));
                int length = i.Read(buffer, 24, 2048);
                if (length == 0)
                    break;
                if (length < 2048)
                    throw new System.ApplicationException();
                frames++;
                if (frames == 75)
                {
                    seconds++;
                    frames = 0;
                }
                if (seconds == 60)
                {
                    minutes++;
                    seconds = 0;
                }
                o.Write(buffer, 0, 2352);
            }
        }
    }
}
