using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(65542)]
    public class NormalList : Chunk
    {
        public Vector3[] Normals;

        public NormalList(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            uint len = reader.ReadUInt32();
            Normals = new Vector3[len];
            for (int i = 0; i < len; i++)
                Normals[i] = Util.ReadVector3(reader);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((uint)Normals.Length);
            for (int i = 0; i < Normals.Length; i++)
            {
                Util.WriteVector3(writer, Normals[i]);
            }
        }

        public override string ToString()
        {
            return $"Normal List ({Normals.Length})";
        }
    }
}
