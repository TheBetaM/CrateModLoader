﻿using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(139264)]
    public class TextureFont : Chunk
    {
        public byte[] Data;

        public TextureFont(File file, uint type) : base(file, type)
        {

        }

        public override void ReadHeader(Stream stream, long length)
        {
            Data = new BinaryReader(stream).ReadBytes((int)length);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Data);
        }

        public override string ToString()
        {
            return $"Texture Font";
        }
    }
}