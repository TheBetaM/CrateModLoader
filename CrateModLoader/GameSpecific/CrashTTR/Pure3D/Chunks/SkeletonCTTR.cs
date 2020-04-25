using System.IO;
using System.Text;
using System;

namespace Pure3D.Chunks
{
    [ChunkType(143360)]
    public class SkeletonCTTR : Named
    {
        public uint Version;
        protected uint NumJoints; // should be equal to # of children
        public uint UnkData;
        public uint UnkData1;
        public long PosOffset;

        public SkeletonCTTR(File file, uint type) : base(file, type)
        {

        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);

            base.ReadHeader(stream, length);
            Version = reader.ReadUInt32();
            NumJoints = reader.ReadUInt32();
            UnkData = reader.ReadUInt32();
            UnkData1 = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Version);
            writer.Write(NumJoints);
            writer.Write(UnkData);
            writer.Write(UnkData1);
        }

        public override string ToString()
        {
            return $"Skeleton CTTR: Name: { Name } Version { Version } NumJoints { NumJoints } UnkData { UnkData } UnkData1 { UnkData1 }";
        }
    }
}