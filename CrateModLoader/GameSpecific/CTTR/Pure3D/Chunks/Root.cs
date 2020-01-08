using System.IO;

namespace Pure3D.Chunks
{
    /// <summary>
    /// A dummy chunk we use to represent the Root of a file.
    /// </summary>
    public class Root : Chunk
    {
        public Root(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
        }

        public override void WriteHeader(Stream stream)
        {
        }

        public override string ToString()
        {
            return "Root";
        }
    }
}
