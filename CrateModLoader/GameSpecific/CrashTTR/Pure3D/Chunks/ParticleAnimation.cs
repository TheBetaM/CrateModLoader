using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(88072)]
    public class ParticleAnimation : Chunk
    {
        public byte[] Data;
        private uint unknownType;

        public ParticleAnimation(File file, uint type) : base(file, type)
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
