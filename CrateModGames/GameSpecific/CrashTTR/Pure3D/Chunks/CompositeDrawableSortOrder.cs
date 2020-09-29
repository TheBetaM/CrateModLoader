using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(17689)]
    public class CompositeDrawableSortOrder : Chunk
    {
        public float SortOrder;

        public CompositeDrawableSortOrder(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            SortOrder = new BinaryReader(stream).ReadSingle();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(SortOrder);
        }

        public override string ToString()
        {
            return $"Composite Drawable Sort Order ({SortOrder})";
        }
    }
}
