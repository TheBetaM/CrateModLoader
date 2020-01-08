using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(65554)]
    public class PrimitiveGroupMemoryImageVertex : Chunk
    {
        public byte[] Data;

        public PrimitiveGroupMemoryImageVertex(File file, uint type) : base(file, type)
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
            return $"Primitive Group Memory Image Vertex";
        }
    }
}