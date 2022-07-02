using System.IO;
using System.Text;
using System;

namespace Pure3D.Chunks
{
    [ChunkType(143361)]
    public class SkeletonJointCTTR : Named
    {
        public byte[] Data;
        public uint SkeletonParent;
        public Matrix RestPose;

        public SkeletonJointCTTR(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);

            long currentPos = reader.BaseStream.Position;

            base.ReadHeader(stream, length);
            SkeletonParent = reader.ReadUInt32();
            RestPose = Util.ReadMatrix(reader);

        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(SkeletonParent);
            Util.WriteMatrix(writer, RestPose);
        }

        public override string ToString()
        {
            string SkeletonParentName = "";
            if (SkeletonParent > 0)
            {
                SkeletonParentName = Parent.GetChildren<SkeletonJointCTTR>()[SkeletonParent].Name;
            }
            else
            {
                SkeletonParentName = "Root";
            }
            return $"Skeleton Joint CTTR: {Name} ParentID { SkeletonParent }, ParentName { SkeletonParentName } Rest Pose {RestPose.M11} ; {RestPose.M12} ; {RestPose.M13} ; {RestPose.M14} | {RestPose.M21} ; {RestPose.M22} ; {RestPose.M23} ; {RestPose.M24} |{RestPose.M31} ; {RestPose.M32} ; {RestPose.M33} ; {RestPose.M34} |{RestPose.M41} ; {RestPose.M42} ; {RestPose.M43} ; {RestPose.M44}";
        }
    }
}