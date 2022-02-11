using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashMoMDS
{
    // List of strings
    public class MXB_File
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
            reader.ReadBytes(4); // BXM0

            List<int> Offsets = new List<int>();
            int firstOffset = reader.ReadInt32();
            Offsets.Add(firstOffset);
            while (reader.BaseStream.Position < firstOffset)
            {
                Offsets.Add(reader.ReadInt32());
            }

            for (int i = 0; i < Offsets.Count; i++)
            {
                if (Offsets[i] != Offsets[0])
                {
                    reader.BaseStream.Position = Offsets[i];

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
                else
                {
                    Texts.Add(string.Empty);
                }
            }

        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(0x304D5842); // BXM0

            List<int> Offsets = new List<int>();

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
            writer.Write(0xDDDDDDDD); // first string is a dummy string
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
