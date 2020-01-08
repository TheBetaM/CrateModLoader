using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(65553)]
    public class VertexShader : Chunk
    {
        public string VertexShaderName;
        public ulong VertexShaderName_padding;

        public VertexShader(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            VertexShaderName = Util.ReadString(reader, ref VertexShaderName_padding);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            Util.WriteString(writer, VertexShaderName, VertexShaderName_padding);
        }

        public override string ToString()
        {
            return $"Vertex Shader {VertexShaderName}";
        }
    }
}
