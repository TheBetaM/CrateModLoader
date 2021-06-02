using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Metadata : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string basePath = Ray3_Common.GetDataPath(mod);
            if (!File.Exists(basePath + @"fix.lvl"))
            {
                Metadata(basePath + @"fix.lvl");
            }
            else
            {
                Metadata(basePath + @"FIX.LVL");
            }
        }

        void Metadata(string filePath)
        {
            if (!File.Exists(filePath)) return;

            byte[] buffer;
            List<long> offsets = new List<long>();

            using (var br = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
            {
                buffer = new byte[br.Length];
                br.Read(buffer, 0, buffer.Length);
                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (BinaryReader reader = new BinaryReader(memoryStream))
                    {
                        uint Part1 = 0x20696255;
                        uint Part2 = 0x74666F53;
                        uint Part3 = 0x676F6C20;

                        while (reader.BaseStream.Position < reader.BaseStream.Length - 0xF)
                        {
                            long pos = reader.BaseStream.Position;
                            if (reader.ReadUInt32() == Part1)
                            {
                                if (reader.ReadUInt32() == Part2)
                                {
                                    if (reader.ReadUInt32() == Part3)
                                    {
                                        reader.BaseStream.Position -= 0xC;
                                        offsets.Add(reader.BaseStream.Position);
                                        reader.BaseStream.Position += 0xC;
                                    }
                                }
                            }
                            reader.BaseStream.Position = pos + 1;
                        }
                    }
                }
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    int i = 0;
                    string add = "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + " Seed: " + ModLoaderGlobals.RandomizerSeed;
                    char[] add_char = add.ToCharArray();
                    List<byte> add_byte = new List<byte>(System.Text.Encoding.ASCII.GetBytes(add_char));
                    while (add_byte.Count < 0x55)
                    {
                        add_byte.Add(0x20);
                    }

                    while (i < buffer.Length)
                    {
                        if (offsets.Contains(writer.BaseStream.Position))
                        {
                            writer.Write(add_byte.ToArray());
                            i += 0x55;
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
