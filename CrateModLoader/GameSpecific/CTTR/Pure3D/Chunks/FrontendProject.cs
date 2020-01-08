using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(98304)]
    public class FrontendProject : Chunk
    {
        public byte[] Data;

        public FrontendProject(File file, uint type) : base(file, type)
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
            return $"Frontend Project";
        }
    }
}
