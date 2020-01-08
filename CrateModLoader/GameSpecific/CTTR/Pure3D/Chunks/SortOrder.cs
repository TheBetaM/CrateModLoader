using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(1187840)]
    public class SortOrder : Chunk
    {
        public byte[] Data;

        public SortOrder(File file, uint type) : base(file, type)
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
            return $"Sort Order";
        }
    }
}