using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(65537)]
    public class Skin : Mesh
    {
        public string SkeletonName;
        public ulong SkeletonName_padding;

        public Skin(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            Version = reader.ReadUInt32();
            SkeletonName = Util.ReadString(reader, ref SkeletonName_padding);
            NumPrimGroups = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Version);
            Util.WriteString(writer, SkeletonName, SkeletonName_padding);
            writer.Write(NumPrimGroups);
        }

        public override string ToString()
        {
            return $"Skin: {Name} (Skeleton: {SkeletonName}) ({NumPrimGroups} Prim Groups)";
        }
    }
}
