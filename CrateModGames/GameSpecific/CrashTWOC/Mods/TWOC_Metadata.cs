using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_Metadata : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string filePath = mod.ExtractedPath + mod.ExecutableFileName;

            if (!File.Exists(filePath)) return;

            byte[] buffer;
            long offset1 = 0;
            long offset2 = 0;

            using (var br = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
            {
                buffer = new byte[br.Length];
                br.Read(buffer, 0, buffer.Length);
                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (BinaryReader reader = new BinaryReader(memoryStream))
                    {
                        uint Sound1 = 0x4E554F53;
                        uint Sound2 = 0x504F2044;
                        uint Sound3 = 0x4E4F4954;

                        uint Screen1 = 0x554A4441;
                        uint Screen2 = 0x53205453;
                        uint Screen3 = 0x45455243;

                        while (reader.BaseStream.Position < reader.BaseStream.Length - 0xF)
                        {
                            long pos = reader.BaseStream.Position;
                            uint test = reader.ReadUInt32();
                            if (test == Sound1)
                            {
                                if (reader.ReadUInt32() == Sound2)
                                {
                                    if (reader.ReadUInt32() == Sound3)
                                    {
                                        reader.BaseStream.Position -= 0xC;
                                        offset1 = reader.BaseStream.Position;
                                        reader.BaseStream.Position += 0xC;
                                    }
                                }
                            }
                            else if (test == Screen1)
                            {
                                if (reader.ReadUInt32() == Screen2)
                                {
                                    if (reader.ReadUInt32() == Screen3)
                                    {
                                        reader.BaseStream.Position -= 0xC;
                                        offset2 = reader.BaseStream.Position;
                                        reader.BaseStream.Position += 0xC;
                                    }
                                }
                            }
                            reader.BaseStream.Position = pos + 1;
                        }
                    }
                }
            }

            if (offset1 == 0 || offset2 == 0)
            {
                Console.WriteLine("TWOC: text offset not found!");
                return;
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    int i = 0;
                    string add = "CML " + ModLoaderGlobals.ProgramVersion.ToUpper();
                    char[] add_char = add.ToCharArray();
                    List<byte> add_byte = new List<byte>(System.Text.Encoding.ASCII.GetBytes(add_char));
                    while (add_byte.Count < 0x0D)
                    {
                        add_byte.Add(0x20);
                    }
                    string add2 = ModLoaderGlobals.RandomizerSeed.ToString();
                    char[] add2_char = add2.ToCharArray();
                    List<byte> add2_byte = new List<byte>(System.Text.Encoding.ASCII.GetBytes(add2_char));
                    while (add2_byte.Count < 0x0D)
                    {
                        add2_byte.Add(0x20);
                    }

                    while (i < buffer.Length)
                    {
                        if (writer.BaseStream.Position == offset1)
                        {
                            writer.Write(add_byte.ToArray());
                            i += 0x0D;
                        }
                        else if (writer.BaseStream.Position == offset2)
                        {
                            writer.Write(add2_byte.ToArray());
                            i += 0x0D;
                        }
                        else
                        {
                            writer.Write(buffer[i]);
                            i++;
                        }
                    }
                }
            }

            return;
        }
    }
}
