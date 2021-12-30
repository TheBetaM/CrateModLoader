using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashBB
{
    // List of strings
    public class LNG_File
    {
        public string FileName;
        public List<string> Texts;

        public void Read(string filePath)
        {
            FileName = Path.GetFileName(filePath);
            using (var br = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
            {
                byte[] buffer = new byte[br.Length];
                br.Read(buffer, 0, buffer.Length);
                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (BinaryReader reader = new BinaryReader(memoryStream))
                    {
                        Read(reader);
                    }
                }
            }
        }

        public void Write(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    Write(writer);
                }
            }
        }

        public void Read(BinaryReader reader)
        {
            Texts = new List<string>();

            ushort TextCount = reader.ReadUInt16();
            List<int> Offsets = new List<int>();
            for (int i = 0; i < TextCount; i++)
            {
                Offsets.Add(reader.ReadUInt16());
            }
            for (int i = 0; i < Offsets.Count; i++)
            {
                reader.BaseStream.Position = Offsets[i];

                //todo
                List<char> Chars = new List<char>();
                char input = reader.ReadChar();
                while (input != '\0')
                {
                    Chars.Add(input);
                    input = reader.ReadChar();
                }
                string NewText = new string(Chars.ToArray());

                Texts.Add(NewText);
            }

        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort)Texts.Count);
            List<int> Offsets = new List<int>();

            //todo
            int firstOffset = (Texts.Count * 4) + 4;
            int offset = firstOffset + 4;
            for (int i = 0; i < Texts.Count; i++)
            {
                writer.Write(firstOffset);
                if (string.IsNullOrEmpty(Texts[i]))
                {
                    Offsets.Add(firstOffset);
                }
                else
                {
                    Offsets.Add(0);
                }
            }

            for (int i = 1; i < Texts.Count; i++)
            {
                if (!string.IsNullOrEmpty(Texts[i]))
                {
                    Offsets[i] = (int)writer.BaseStream.Position;
                    writer.Write(Texts[i].ToCharArray());
                    writer.Write((byte)0);
                    if (writer.BaseStream.Position % 4 != 0)
                    {
                        while (writer.BaseStream.Position % 4 != 0)
                        {
                            writer.Write((byte)0xAA);
                        }
                    }
                }
            }

            writer.BaseStream.Position = 4;
            for (int i = 0; i < Texts.Count; i++)
            {
                writer.Write(Offsets[i]);
            }
        }
    }
}
