using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(65548)]
    public class WeightList : Chunk
    {
        public Vector3[] Weights;

        public WeightList(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            uint len = reader.ReadUInt32();
            Weights = new Vector3[len];
            for (int i = 0; i < len; i++)
            {
                Weights[i] = Util.ReadVector3(reader);
            }
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((uint)Weights.Length);
            for (int i = 0; i < Weights.Length; i++)
            {
                Util.WriteVector3(writer, Weights[i]);
            }
        }

        public override string ToString()
        {
            return $"Weight List ({Weights.Length})";
        }
    }
}