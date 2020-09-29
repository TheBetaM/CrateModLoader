using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(66060320)]
    public class WorldDef : Chunk
    {
        public byte[] Data;

        public WorldDef(File file, uint type) : base(file, type)
        {

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
            return $"WorldDef";
        }
    }
}
