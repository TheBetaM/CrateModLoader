using System;
using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(65549)]
    public class MatrixPalette : Chunk
    {
        public uint[] Matrices;

        public MatrixPalette(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            uint len = reader.ReadUInt32();
            Matrices = new uint[len];
            for (int i = 0; i < len; i++)
                Matrices[i] = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((uint)Matrices.Length);
            for (int i = 0; i < Matrices.Length; i++)
            {
                writer.Write(Matrices[i]);
            }
        }

        public override string ToString()
        {
            return $"Matrix Palette ({Matrices.Length})";
        }
    }
}
