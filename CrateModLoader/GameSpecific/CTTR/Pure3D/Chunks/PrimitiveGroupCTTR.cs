using System.IO;
using System.Text;
using System;

namespace Pure3D.Chunks
{
    [ChunkType(65568)]
    public class PrimitiveGroupCTTR : Chunk
    {
        public byte[] Data;

        public uint Version;
        public string ShaderName;
        public ulong ShaderName_padding;
        public PrimitiveTypes PrimitiveType;
        public VertexTypes VertexType;
        public uint NumVertices;
        public uint NumIndices;
        public uint NumMatrices;
        public uint MemoryImaged;
        public uint Optimised;
        public uint VertexAnimated;
        public uint VertexAnimatedMask;

        public PrimitiveGroupCTTR(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);

            Version = reader.ReadUInt32();
            ShaderName = Util.ReadString(reader, ref ShaderName_padding);
            PrimitiveType = (PrimitiveTypes)reader.ReadUInt32();
            VertexType = (VertexTypes)reader.ReadUInt32();
            NumVertices = reader.ReadUInt32();
            NumIndices = reader.ReadUInt32();
            NumMatrices = reader.ReadUInt32();
            MemoryImaged = reader.ReadUInt32();
            Optimised = reader.ReadUInt32();
            VertexAnimated = reader.ReadUInt32();
            VertexAnimatedMask = reader.ReadUInt32();

        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Version);
            Util.WriteString(writer, ShaderName, ShaderName_padding);
            writer.Write((uint)PrimitiveType);
            writer.Write((uint)VertexType);
            writer.Write(NumVertices);
            writer.Write(NumIndices);
            writer.Write(NumMatrices);
            writer.Write(MemoryImaged);
            writer.Write(Optimised);
            writer.Write(VertexAnimated);
            writer.Write(VertexAnimatedMask);
        }

        public override string ToString()
        {
            return $"Primitive Group {ShaderName}, PrimitiveType {PrimitiveType}, VertexType {VertexType}, NumVertices {NumVertices}, NumIndices {NumIndices}, NumMatrices {NumMatrices}, MemoryImaged {MemoryImaged}, Optimised {Optimised}, Vertex Animated {VertexAnimated}, Vertex Animated Mask {VertexAnimatedMask}";
        }

        public enum PrimitiveTypes : uint
        {
            TriangleList,
            TriangleStrip,
            LineList,
            LineStrip,
        }

        [System.Flags]
        public enum VertexTypes : uint
        {
            UVs = 1U,
            UVs2 = 2U,
            UVs3 = 4U,
            UVs4 = 8U,
            Normals = 16U,
            Colours = 32U,
            Matrices = 128U,
            Weights = 256U,
            Unknown = 8192U,
        }
    }
}