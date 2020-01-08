using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(98336)]
    public class FrontendLayer : Chunk
    {
        public byte[] Data;

        public FrontendLayer(File file, uint type) : base(file, type)
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
            return $"Frontend Layer";
        }
    }
}