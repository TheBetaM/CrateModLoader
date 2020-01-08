using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(69632)]
    public class Shader : Named
    {
        public uint Version;
        public string PddiShaderName;
        public ulong PddiShaderName_padding;
        public uint HasTranslucency;
        public uint VertexNeeds;
        public uint VertexMask;
        protected uint NumParams; // Should match the number of children

        public Shader(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            base.ReadHeader(stream, length);

            BinaryReader reader = new BinaryReader(stream);
            Version = reader.ReadUInt32();
            PddiShaderName = Util.ReadString(reader, ref PddiShaderName_padding);
            HasTranslucency = reader.ReadUInt32();
            VertexNeeds = reader.ReadUInt32();
            VertexMask = reader.ReadUInt32();
            NumParams = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            base.WriteHeader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Version);
            Util.WriteString(writer, PddiShaderName, PddiShaderName_padding);
            writer.Write(HasTranslucency);
            writer.Write(VertexNeeds);
            writer.Write(VertexMask);
            writer.Write(NumParams);
        }

        public override string ToString()
        {
            return $"Shader: {Name} ({PddiShaderName})";
        }
    }
}
