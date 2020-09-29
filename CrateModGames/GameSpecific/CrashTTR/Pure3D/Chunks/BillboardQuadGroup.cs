using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(94210)]
    public class BillboardQuadGroup : Named
    {
        public uint Version;
        public string Shader;
        public ulong Shader_padding;
        public uint ZTest;
        public uint ZWrite;
        public uint Fog;
        public uint NumQuads;

        public BillboardQuadGroup(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            Version = reader.ReadUInt32(); // version before name, rare case.
            base.ReadHeader(stream, length);
            Shader = Util.ReadString(reader, ref Shader_padding);
            ZTest = reader.ReadUInt32();
            ZWrite = reader.ReadUInt32();
            Fog = reader.ReadUInt32();
            NumQuads = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Version);
            base.WriteHeader(stream);
            Util.WriteString(writer, Shader, Shader_padding);
            writer.Write(ZTest);
            writer.Write(ZWrite);
            writer.Write(Fog);
            writer.Write(NumQuads);
        }

        public override string ToString()
        {
            return $"Billboard Quad Group: {Name}";
        }
    }
}
