using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(65552)]
    public class PackedNormalList : Chunk
    {
        public byte[] Normals;

        public PackedNormalList(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            uint len = reader.ReadUInt32();
            Normals = reader.ReadBytes((int)len);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((uint)Normals.Length);
            writer.Write(Normals);
        }

        public override string ToString()
        {
            return $"Packed Normal List ({Normals.Length})";
        }
    }
}
