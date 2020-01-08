using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(102401)]
    public class Image : Named
    {
        public uint Version;
        public uint Width;
        public uint Height;
        public uint Bpp;
        public uint Palettized;
        public uint HasAlpha;
        public uint Format;

        public Image(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            Version = reader.ReadUInt32();
            Width = reader.ReadUInt32();
            Height = reader.ReadUInt32();
            Bpp = reader.ReadUInt32();
            Palettized = reader.ReadUInt32();
            HasAlpha = reader.ReadUInt32();
            Format = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Version);
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(Bpp);
            writer.Write(Palettized);
            writer.Write(HasAlpha);
            writer.Write(Format);
        }

        public override string ToString()
        {
            return $"Image Chunk: {Name} ({Format}) ({Width}x{Height})";
        }

        public enum Formats : uint
        {
            PNG = 1,
            DXT1 = 6,
            DXT3 = 8,
            DXT5 = 10,
            P3DI = 11,
            P3DI2 = 25,
        }
    }
}
