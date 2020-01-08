using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(17664)]
    public class Skeleton : Named
    {
        public uint Version;
        protected uint NumJoints; // should be equal to # of children

        public Skeleton(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            Version = reader.ReadUInt32();
            NumJoints = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Version);
            writer.Write(NumJoints);
        }

        public override string ToString()
        {
            return $"Skeleton: {Name}";
        }
    }
}
