using System.IO;

namespace CrateModLoader
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
}
