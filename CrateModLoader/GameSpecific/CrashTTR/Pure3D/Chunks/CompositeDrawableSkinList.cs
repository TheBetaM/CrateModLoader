using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(17683)]
    public class CompositeDrawableSkinList : Chunk
    {
        public uint NumElements;

        public CompositeDrawableSkinList(File file, uint type) : base(file, type)
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
            return $"Composite Drawable Skin List (Elements: {NumElements})";
        }
    }
}
