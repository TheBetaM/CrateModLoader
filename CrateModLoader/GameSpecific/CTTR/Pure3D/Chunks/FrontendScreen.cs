using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(98305)]
    public class FrontendScreen : Chunk
    {
        public byte[] Data;

        public FrontendScreen(File file, uint type) : base(file, type)
        {

        }

        public override void ReadHeader(Stream stream, long length)
        {
            Data = new BinaryReader(stream).ReadBytes((int)length);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Data);
        }

        public override string ToString()
        {
            return $"Frontend Screen";
        }
    }
}