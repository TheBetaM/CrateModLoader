using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(1184257)]
    public class FrameController : Chunk
    {
        public byte[] Data;

        public FrameController(File file, uint type) : base(file, type)
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
            return $"Frame Controller";
        }
    }
}