using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(81920)]
    public class Locator : Named
    {
        public uint Version;
        public Vector3 Position;

        public Locator(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            Version = reader.ReadUInt32();
            Position = Util.ReadVector3(reader);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Version);
            Util.WriteVector3(writer, Position);
        }

        public override string ToString()
        {
            return $"Locator: {Name}";
        }
    }
}
