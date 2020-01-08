using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(98317)]
    public class FrontendTextBible : Named
    {
        public uint LanguageCount;
        public string Languages;
        public ulong Languages_padding;

        public FrontendTextBible(File file, uint type) : base(file, type)
        {

        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            LanguageCount = reader.ReadUInt32();
            Languages = Util.ReadString(reader, ref Languages_padding);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(LanguageCount);
            Util.WriteString(writer, Languages, Languages_padding);
        }

        public override string ToString()
        {
            return $"Frontend Text Bible: { Name }, Language Count: { LanguageCount }, Languages: { Languages } ";
        }
    }
}