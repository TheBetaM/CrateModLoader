using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(65544)]
    public class ColourList : Chunk
    {
        public uint[] Colours;

        public ColourList(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            uint len = reader.ReadUInt32();
            Colours = new uint[len];
            for (int i = 0; i < len; i++)
                Colours[i] = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((uint)Colours.Length);
            for (int i = 0; i < Colours.Length; i++)
            {
                writer.Write(Colours[i]);
            }
        }

        public override string ToString()
        {
            return $"Colour List ({Colours.Length})";
        }
    }
}
