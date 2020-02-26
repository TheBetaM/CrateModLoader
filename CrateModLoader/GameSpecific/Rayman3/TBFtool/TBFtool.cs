using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
//TBF Tool by TheGameExplorer

namespace tbftool
{
    class TBF_Worker
    {

        private static List<string> TBF_Files;

        public static void TBF_Extract(string path)
        {
            TBF_Files = new List<string>();
            string dirName;
            string fileName;
            fileName = Path.GetFileNameWithoutExtension(path);
            dirName = Path.GetDirectoryName(path);
            //Directory.CreateDirectory(dirName);

            using (BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                int index = 0;
                while (true)
                {
                    if (reader.BaseStream.Position == reader.BaseStream.Length)
                        return;
                    else if (reader.ReadUInt32() != 0x0AB9FC72)
                        return;
                    uint type = reader.ReadUInt32();
                    bool hasAlpha = (type & 0x30) == 0x30;
                    type = type & 0xF;
                    int width = reader.ReadInt32();
                    int height = reader.ReadInt32();
                    Bitmap bitmap = null;
                    index++;

                    Console.WriteLine("Extracting texture: {0} type: {1} alpha: {2} width: {3} height: {4}", index, type, hasAlpha, width, height);

                    switch (type)
                    {
                        case 0x1:
                            {
                                Color[] palette;
                                PS2ImageHelper.ReadPSMCT32(reader, 4, 4, out palette);
                                byte[] indices;
                                PS2ImageHelper.ReadPSMT4(reader, width, height, out indices);
                                PS2ImageHelper.UnSwizzle8(width, height, indices, out indices);
                                bitmap = BitmapHelper.Create(palette, indices, width, height);
                            }
                            break;
                        case 0x2:
                            {
                                Color[] palette;
                                PS2ImageHelper.ReadPSMCT32(reader, 16, 16, out palette);
                                PS2ImageHelper.TilePalette(palette, out palette);
                                byte[] indices;
                                PS2ImageHelper.ReadPSMT8(reader, width, height, out indices);
                                PS2ImageHelper.UnSwizzle8(width, height, indices, out indices);
                                bitmap = BitmapHelper.Create(palette, indices, width, height);
                            }
                            break;
                        default:
                            throw new InvalidDataException("Unknown type: " + type);
                    }
                    bitmap.Save(dirName + "\\" + string.Format("{0}_{1}.png", fileName, index));
                    TBF_Files.Add(dirName + "\\" + string.Format("{0}_{1}.png", fileName, index));
                }
            }
        }

        public static void TBF_Build(string path)
        {
            string dirName;
            string fileName;
            fileName = Path.GetFileNameWithoutExtension(path);
            dirName = Path.GetDirectoryName(path);

            using (BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.Create)))
            {
                for (int i = 0; i < TBF_Files.Count; i++)
                {
                    Bitmap bFile = new Bitmap(TBF_Files[i]);

                    writer.Write(0x0AB9FC72);
                    uint type = 0x1;
                    writer.Write(type);
                    int width = bFile.Width;
                    int height = bFile.Height;
                    writer.Write(width);
                    writer.Write(height);

                    switch (type)
                    {
                        case 0x1:
                            {
                                Color[] palette = new Color[1];
                                PS2ImageHelper.WritePSMCT32(writer, 4, 4, palette);
                                byte[] indices = new byte[1];
                                byte[] indices_out;
                                PS2ImageHelper.Swizzle8(width, height, indices, out indices_out);
                                PS2ImageHelper.WritePSMT4(writer, width, height, indices);
                            }
                            break;
                        case 0x2:
                            {
                                Color[] palette = new Color[1];
                                Color[] palette_out = new Color[1];
                                PS2ImageHelper.TilePalette(palette, out palette_out);
                                PS2ImageHelper.WritePSMCT32(writer, 16, 16, palette_out);
                                byte[] indices = new byte[1];
                                byte[] indices_out;
                                PS2ImageHelper.Swizzle8(width, height, indices, out indices_out);
                                PS2ImageHelper.WritePSMT8(writer, width, height, indices);
                                
                            }
                            break;
                        default:
                            throw new InvalidDataException("Unknown type: " + type);
                    }
                }
            }
        }
    }
}
