using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(98315)]
    public class FrontendString : Chunk
    {
        public string BibleName;
        public ulong BibleName_padding;
        public string BibleText;
        public ulong BibleText_padding;

        public FrontendString(File file, uint type) : base(file, type)
        {

        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            BibleName = Util.ReadString(reader, ref BibleName_padding);
            BibleText = Util.ReadString(reader, ref BibleText_padding);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            Util.WriteString(writer, BibleName, BibleName_padding);
            Util.WriteString(writer, BibleText, BibleText_padding);
        }

        public override string ToString()
        {
            return $"Frontend String: Bible - { BibleName }, Text - { BibleText } ";
        }
    }
}