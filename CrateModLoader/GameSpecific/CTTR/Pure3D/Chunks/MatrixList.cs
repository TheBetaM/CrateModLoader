using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(65547)]
    public class MatrixList : Chunk
    {
        public byte[][] Matrices;

        public MatrixList(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            uint len = reader.ReadUInt32();
            Matrices = new byte[len][];
            for (int i = 0; i < len; i++)
            {
                Matrices[i] = new byte[4];
                Matrices[i][0] = reader.ReadByte();
                Matrices[i][1] = reader.ReadByte();
                Matrices[i][2] = reader.ReadByte();
                Matrices[i][3] = reader.ReadByte();
            }
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((uint)Matrices.Length);
            for (int i = 0; i < Matrices.Length; i++)
            {
                writer.Write(Matrices[i][0]);
                writer.Write(Matrices[i][1]);
                writer.Write(Matrices[i][2]);
                writer.Write(Matrices[i][3]);
            }
        }

        public override string ToString()
        {
            return $"Matrix List ({Matrices.Length})";
        }
    }
}
