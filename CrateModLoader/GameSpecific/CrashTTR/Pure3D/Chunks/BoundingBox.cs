using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(65539)]
    public class BoundingBox : Chunk
    {
        public Vector3 Low;
        public Vector3 High;

        public BoundingBox(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            Low = Util.ReadVector3(reader);
            High = Util.ReadVector3(reader);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            Util.WriteVector3(writer, Low);
            Util.WriteVector3(writer, High);
        }

        public override string ToString()
        {
            return "Bounding Box";
        }
    }
}
