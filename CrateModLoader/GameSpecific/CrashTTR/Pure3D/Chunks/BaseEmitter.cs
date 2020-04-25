using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(88069)]
    public class BaseEmitter : Chunk
    {
        public byte[] Data;
        private uint unknownType;

        public BaseEmitter(File file, uint type) : base(file, type)
        {
            unknownType = type;
        }

        public override void ReadHeader(Stream stream, long length)
        {
            Data = new BinaryReader(stream).ReadBytes((int)length);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Data);
        }

        public override string ToString()
        {
            return $"Unknown Chunk (TypeID: {unknownType}) (Len: {Data.Length})";
        }
    }
}
