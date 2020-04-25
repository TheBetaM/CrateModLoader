using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(117510144)]
    public class PhysicsObject : Named
    {
        public uint Version;
        public string MaterialName;
        public ulong MaterialName_padding;
        public uint NumJoints;
        public float Volume;
        public float RestingSensitivity;

        public PhysicsObject(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            Version = reader.ReadUInt32();
            MaterialName = Util.ReadString(reader, ref MaterialName_padding);
            NumJoints = reader.ReadUInt32();
            Volume = reader.ReadSingle();
            RestingSensitivity = reader.ReadSingle();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Version);
            Util.WriteString(writer, MaterialName, MaterialName_padding);
            writer.Write(NumJoints);
            writer.Write(Volume);
            writer.Write(RestingSensitivity);
        }

        public override string ToString()
        {
            return $"Physics Object: {Name}";
        }
    }
}
