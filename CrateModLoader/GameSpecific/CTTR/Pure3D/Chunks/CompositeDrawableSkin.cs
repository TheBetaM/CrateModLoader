using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(17685)]
    public class CompositeDrawableSkin : Named
    {
        public uint IsTranslucent;

        public CompositeDrawableSkin(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            IsTranslucent = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(IsTranslucent);
        }

        public override string ToString()
        {
            return $"Composite Drawable Skin: {Name}, IsTranslucent = {IsTranslucent}";
        }
    }
}
