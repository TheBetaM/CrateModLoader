using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(117510176)]
    public class PhysicsJoint : Chunk
    {
        public uint Index;
        public float Volume;
        public float Stiffness;
        public float MaxAngle;
        public float MinAngle;
        public uint DOF;

        public PhysicsJoint(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            Index = reader.ReadUInt32();
            Volume = reader.ReadSingle();
            Stiffness = reader.ReadSingle();
            MaxAngle = reader.ReadSingle();
            MinAngle = reader.ReadSingle();
            DOF = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Index);
            writer.Write(Volume);
            writer.Write(Stiffness);
            writer.Write(MaxAngle);
            writer.Write(MinAngle);
            writer.Write(DOF);
        }

        public override string ToString()
        {
            return $"Physics Joint {Index}";
        }
    }
}
