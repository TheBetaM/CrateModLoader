using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(17665)]
    public class SkeletonJoint : Named
    {
        public uint SkeletonParent;
        public int DOF;
        public int FreeAxis;
        public int PrimaryAxis;
        public int SecondaryAxis;
        public int TwistAxis;
        public Matrix RestPose;

        public SkeletonJoint(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            SkeletonParent = reader.ReadUInt32();
            DOF = reader.ReadInt32();
            FreeAxis = reader.ReadInt32();
            PrimaryAxis = reader.ReadInt32();
            SecondaryAxis = reader.ReadInt32();
            TwistAxis = reader.ReadInt32();
            RestPose = Util.ReadMatrix(reader);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(SkeletonParent);
            writer.Write(DOF);
            writer.Write(FreeAxis);
            writer.Write(PrimaryAxis);
            writer.Write(SecondaryAxis);
            writer.Write(TwistAxis);
            Util.WriteMatrix(writer, RestPose);
        }

        public override string ToString()
        {
            return $"Skeleton Joint: {Name}";
        }
    }
}
