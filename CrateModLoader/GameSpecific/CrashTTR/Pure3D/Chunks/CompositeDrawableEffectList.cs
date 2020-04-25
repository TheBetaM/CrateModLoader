using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(17687)]
    public class CompositeDrawableEffectList : Chunk
    {
        public uint NumElements; // should be # of children.

        public CompositeDrawableEffectList(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            NumElements = new BinaryReader(stream).ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(NumElements);
        }

        public override string ToString()
        {
            return $"Composite Drawable Effect List ({NumElements} Elements)";
        }
    }
}
