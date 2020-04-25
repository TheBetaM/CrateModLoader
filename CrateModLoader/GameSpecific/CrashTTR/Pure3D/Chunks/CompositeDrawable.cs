using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(17682)]
    public class CompositeDrawable : Named
    {
        public string SkeletonName;
        public ulong SkeletonName_padding;

        public CompositeDrawable(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            base.ReadHeader(stream, length);
            SkeletonName = Util.ReadString(new BinaryReader(stream), ref SkeletonName_padding);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            Util.WriteString(writer, SkeletonName, SkeletonName_padding);
        }

        public override string ToString()
        {
            return $"Composite Drawable: {Name} (Skeleton: {SkeletonName})";
        }
    }
}
