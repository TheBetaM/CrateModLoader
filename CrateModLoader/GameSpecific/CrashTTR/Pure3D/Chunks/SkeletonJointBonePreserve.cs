using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(17668)]
    public class SkeletonJointBonePreserve : Chunk
    {
        public uint PreserveBoneLengths;

        public SkeletonJointBonePreserve(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            PreserveBoneLengths = new BinaryReader(stream).ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(PreserveBoneLengths);
        }

        public override string ToString()
        {
            return "Skeleton Joint Bone Preserve";
        }
    }
}
