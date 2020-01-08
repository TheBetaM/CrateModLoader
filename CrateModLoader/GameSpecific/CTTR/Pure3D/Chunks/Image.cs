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
            RAW = 0,
            PNG = 1,
            TGA = 2,
            BMP = 3,
            IPU = 4,
            DXT = 5,
            DXT1 = 6,
            DXT2 = 7,
            DXT3 = 8,
            DXT4 = 9,
            DXT5 = 10,
            PS2_4bit = 11,
            PS2_8bit = 12,
            PS2_16bit = 13,
            PS2_32bit = 14,
            GameCube_4bit = 15,
            GameCube_8bit = 16,
            GameCube_16bit = 17,
            GameCube_32bit = 18,
            GameCube_DXT1 = 19,
            PSP_4bit = 25,
        }
    }
}
