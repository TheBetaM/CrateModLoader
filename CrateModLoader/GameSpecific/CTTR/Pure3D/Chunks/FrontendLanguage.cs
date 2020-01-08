using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Pure3D.Chunks
{
    [ChunkType(98318)]
    public class FrontendLanguage : Named
    {

        public uint StringAmount;
        public uint BufferSize;
        public char LanguageLetter; // E F I G S
        public uint Modulo; // always the same value
        public uint[] HashesList;
        public uint[] OffsetsList;
        public byte[] Buffer;
        public List<string> StringHashes; //todo, variable names that the text refers to
        public List<string> TextStrings;

        public FrontendLanguage(File file, uint type) : base(file, type)
        {

        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            LanguageLetter = (char)reader.ReadByte();
            StringAmount = reader.ReadUInt32();
            Modulo = reader.ReadUInt32();
            BufferSize = reader.ReadUInt32();
            HashesList = new uint[StringAmount];
            OffsetsList = new uint[StringAmount];
            for (int i = 0; i < StringAmount; i++)
            {
                HashesList[i] = reader.ReadUInt32();
            }
            for (int i = 0; i < StringAmount; i++)
            {
                OffsetsList[i] = reader.ReadUInt32();
            }
            Buffer = new byte[BufferSize];
            for (int i = 0; i < BufferSize; i++)
            {
                Buffer[i] = reader.ReadByte();
            }

            TextStrings = new List<string>();
            int pos = 0;
            string targetString = "";
            for (int i = 0; i < StringAmount; i++)
            {
                targetString = "";
                if (i != StringAmount - 1)
                {
                    while (pos < OffsetsList[i + 1])
                    {
                        targetString += (char)Buffer[pos];
                        pos += 2;
                    }
                }
                else
                {
                    while (pos < BufferSize)
                    {
                        targetString += (char)Buffer[pos];
                        pos += 2;
                    }
                }
                TextStrings.Add(Util.ZeroTerminate(targetString));
            }

        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write((byte)LanguageLetter);

            StringAmount = (uint)TextStrings.Count;
            writer.Write(StringAmount);
            writer.Write(Modulo);

            BufferSize = 0;
            for (int i = 0; i < StringAmount; i++)
            {
                BufferSize += (uint)(TextStrings[i].Length + 1) * 2;
            }

            Buffer = new byte[BufferSize];
            int BufferPos = 0;
            for (int i = 0; i < StringAmount; i++)
            {
                OffsetsList[i] = (uint)BufferPos;
                for (int a = 0; a < TextStrings[i].Length; a++)
                {
                    Buffer[BufferPos] = (byte)TextStrings[i][a];
                    BufferPos++;
                    BufferPos++;
                }
                BufferPos += 2;
            }

            writer.Write(BufferSize);
            for (int i = 0; i < StringAmount; i++)
            {
                writer.Write(HashesList[i]);
            }
            for (int i = 0; i < StringAmount; i++)
            {
                writer.Write(OffsetsList[i]);
            }
            for (int i = 0; i < BufferSize; i++)
            {
                writer.Write(Buffer[i]);
            }
        }

        public override string ToString()
        {
            string main = $"Frontend Language: Name { Name }, Language Type { LanguageLetter }, String Amount { StringAmount }, Modulo { Modulo }, Buffer Size { BufferSize } ";
            main += "\n";
            if (TextStrings.Count > 0)
            {
                for (int i = 0; i < TextStrings.Count; i++)
                {
                    main += "String " + i + ":" + TextStrings[i] + "\n";
                }
            }
            return main;
        }
    }
}