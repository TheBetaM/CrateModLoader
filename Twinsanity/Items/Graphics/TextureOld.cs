using Microsoft.VisualBasic;
using System;
using System.Drawing;
using Twinsanity.Properties;

namespace Twinsanity
{
    /// <summary>
    /// Represents Twinsanity's Texture
    /// </summary>
    public class Texture : BaseItem
    {
        public new string NodeName = "Texture";
        public byte HeaderSize;
        public ushort ImagePages;
        public ushort Reserved;
        public uint SomeKey;
        public uint Width;
        public ushort MipWidth = 0;
        public uint IBlockSize;
        public uint Height;
        public byte Mip;
        public byte PaletteFlag;
        public ushort PaletteSize;
        public byte[] Space = new byte[32];
        public uint DataType;
        public byte[] Unexplored = new byte[176];
        // Image
        public Color[] Palette;
        public byte[] Index;
        public byte[] MipIndex;
        public Color[] RawData;

        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(ByteStream.ToArray());
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        public void ChangeState(bool UnSwizzlePic, bool SwizzlePic, bool FlipPic, bool Fix)
        {
            if (DataType == 4)
                return;
            if (UnSwizzlePic)
                UnSwizzle(ref Index, (ushort)Width, (ushort)Height);
            else if (SwizzlePic)
                Swizzle(ref Index, (ushort)Width, (ushort)Height);
            if (FlipPic)
                Flip(ref Index, (ushort)Width, (ushort)Height);
            if (Fix)
                SwapPalette(ref Palette);
            for (int i = 0; i <= RawData.Length - 1; i++)
                RawData[i] = Palette[Index[i]];
        }

        public void Import(Color[] RawData, uint pwidth, uint pheight, BlockFormats format, bool Mip)
        {
            byte[] Index = new byte[] { };
            Color[] Palette = new Color[] { };
            byte[] MipIndex = new byte[] { };
            ARGB2INDEX(RawData, ref Index, ref Palette);
            Flip(ref Index, (ushort)pwidth, (ushort)pheight);
            Swizzle(ref Index, (ushort)pwidth, (ushort)pheight);
            SwapPalette(ref Palette);
            if (Mip)
                GenerateMips(Index, pwidth, pheight, ref MipIndex);
            System.IO.MemoryStream Data = new System.IO.MemoryStream();
            System.IO.MemoryStream Header = new System.IO.MemoryStream();
            MakeInterleave(ref Data, Index, Palette, MipIndex, format);
            FormHeader(ref Header, format);
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(Header.ToArray());
            NSWriter.Write(Data.ToArray());
            ByteStream = NewStream;
            System.IO.FileStream File = new System.IO.FileStream(@"C:\N", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
            Writer.Write(ByteStream.ToArray());
            Writer.Close();
            File.Close();
            Size = (uint)ByteStream.Length;
            DataUpdate();
        }

        public void ARGB2INDEX(Color[] RawData, ref byte[] Index, ref Color[] Palette)
        {
            Array.Resize(ref Palette, 256);
            Array.Resize(ref Index, RawData.Length);
            int cnt = -1;
            for (int n = 0; n <= RawData.Length - 1; n++)
            {
                Color c = RawData[n];
                bool flag = true;
                for (int i = 0; i <= cnt; i++)
                {
                    if (c == Palette[i])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    cnt += 1;
                    Palette[cnt] = c;
                }
                if (cnt == 255)
                    break;
            }
            for (int n = 0; n <= RawData.Length - 1; n++)
            {
                float MinUnMath = -1.0F;
                byte MaxIndex = 0;
                Color c = RawData[n];
                bool flag = false;
                for (int i = 0; i <= 255; i++)
                {
                    Color p = Palette[i];
                    float cUnMath = Math.Abs(System.Convert.ToInt32(p.A) - System.Convert.ToInt32(c.A)) + Math.Abs(System.Convert.ToInt32(p.R) - System.Convert.ToInt32(c.R)) + Math.Abs(System.Convert.ToInt32(p.G) - System.Convert.ToInt32(c.G)) + Math.Abs(System.Convert.ToInt32(p.B) - System.Convert.ToInt32(c.B));
                    if (c == p)
                    {
                        flag = true;
                        Index[n] = (byte)i;
                        break;
                    }
                    if ((cUnMath < MinUnMath) || MinUnMath == -1.0F)
                    {
                        MaxIndex = (byte)i;
                        MinUnMath = cUnMath;
                    }
                }
                if (!flag)
                    Index[n] = MaxIndex;
            }
        }

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            HeaderSize = BSReader.ReadByte();
            ImagePages = BSReader.ReadUInt16();
            Reserved = BSReader.ReadByte();
            SomeKey = BSReader.ReadUInt32();
            Width = (uint)Math.Pow(2, BSReader.ReadUInt16());
            MipWidth = (ushort)(Width / 2);
            Height = (uint)Math.Pow(2, BSReader.ReadUInt16());
            Mip = BSReader.ReadByte();
            PaletteFlag = BSReader.ReadByte();
            PaletteSize = BSReader.ReadUInt16();
            Space = BSReader.ReadBytes(32);
            DataType = BSReader.ReadUInt32();
            Unexplored = BSReader.ReadBytes(176);
            switch (DataType)
            {
                case 1:
                    {
                        uint DataPos = (uint)ByteStream.Position;
                        Array.Resize(ref Index, (int)(Width * Height));
                        Array.Resize(ref MipIndex, (int)(MipWidth * Height));
                        Array.Resize(ref RawData, (int)(Width * Height));
                        Array.Resize(ref Palette, ImagePages * 16);
                        if ((Width == 32) && (Height == 8))
                        {
                            for (int i = 0; i <= 3; i++)
                            {
                                for (int j = 0; j <= 63; j++)
                                    Index[j + i * 64] = BSReader.ReadByte();
                                ByteStream.Position += 192;
                            }
                            ByteStream.Position += 12 * 256;
                            for (int i = 0; i <= 15; i++)
                            {
                                for (int j = 0; j <= 15; j++)
                                {
                                    byte a, r, g, b;
                                    r = BSReader.ReadByte();
                                    g = BSReader.ReadByte();
                                    b = BSReader.ReadByte();
                                    a = (byte)(BSReader.ReadByte() << 1);
                                    Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                }
                                ByteStream.Position += 192;
                            }
                        }
                        else if ((Width == 16) && (Height == 16))
                        {
                            for (int i = 0; i <= 7; i++)
                            {
                                for (int j = 0; j <= 31; j++)
                                    Index[j + i * 32] = BSReader.ReadByte();
                                ByteStream.Position += 224;
                            }
                            for (int i = 0; i <= 3; i++)
                            {
                                for (int j = 0; j <= 31; j++)
                                    MipIndex[j + i * 32] = BSReader.ReadByte();
                                ByteStream.Position += 224;
                            }
                            ByteStream.Position += 4 * 256;
                            for (int i = 0; i <= 15; i++)
                            {
                                for (int j = 0; j <= 15; j++)
                                {
                                    byte a, r, g, b;
                                    r = BSReader.ReadByte();
                                    g = BSReader.ReadByte();
                                    b = BSReader.ReadByte();
                                    a = (byte)(BSReader.ReadByte() << 1);
                                    Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                }
                                ByteStream.Position += 192;
                            }
                        }
                        else if ((Width == 32) && (Height == 16))
                        {
                            for (int i = 0; i <= 7; i++)
                            {
                                for (int j = 0; j <= 63; j++)
                                    Index[j + i * 64] = BSReader.ReadByte();
                                ByteStream.Position += 192;
                            }
                            for (int i = 0; i <= 7; i++)
                            {
                                for (int j = 0; j <= 31; j++)
                                    MipIndex[j + i * 32] = BSReader.ReadByte();
                                ByteStream.Position += 224;
                            }
                            for (int i = 0; i <= 15; i++)
                            {
                                for (int j = 0; j <= 15; j++)
                                {
                                    byte a, r, g, b;
                                    r = BSReader.ReadByte();
                                    g = BSReader.ReadByte();
                                    b = BSReader.ReadByte();
                                    a = (byte)(BSReader.ReadByte() << 1);
                                    Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                }
                                ByteStream.Position += 192;
                            }
                        }
                        else if ((Width == 32) && (Height == 32))
                        {
                            if (Mip == 1)
                            {
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 63; j++)
                                        Index[j + i * 64] = BSReader.ReadByte();
                                    ByteStream.Position += 192;
                                }
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 192;
                                }
                            }
                            else
                            {
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 63; j++)
                                        Index[j + i * 64] = BSReader.ReadByte();
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 128;
                                }
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 31; j++)
                                        MipIndex[j + i * 32] = BSReader.ReadByte();
                                    ByteStream.Position += 224;
                                }
                            }
                        }
                        else if ((Width == 32) && (Height == 64))
                        {
                            if (Mip == 1)
                            {
                                for (int i = 0; i <= 31; i++)
                                {
                                    for (int j = 0; j <= 63; j++)
                                        Index[j + i * 64] = BSReader.ReadByte();
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 128;
                                }
                            }
                            else
                            {
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 63; j++)
                                        Index[j + i * 64] = BSReader.ReadByte();
                                    for (int j = 0; j <= 63; j++)
                                        MipIndex[j + i * 64] = BSReader.ReadByte();
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 64;
                                }
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 63; j++)
                                        Index[1024 + j + i * 64] = BSReader.ReadByte();
                                    ByteStream.Position += 64;
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[256 + j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 64;
                                }
                            }
                        }
                        else if ((Width == 64) && (Height == 32))
                        {
                            if (Mip == 1)
                            {
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 127; j++)
                                        Index[j + i * 128] = BSReader.ReadByte();
                                    ByteStream.Position += 128;
                                }
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 192;
                                }
                            }
                            else
                            {
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 127; j++)
                                        Index[j + i * 128] = BSReader.ReadByte();
                                    ByteStream.Position += 128;
                                }
                                for (int i = 0; i <= 15; i++)
                                {
                                    for (int j = 0; j <= 63; j++)
                                        MipIndex[j + i * 64] = BSReader.ReadByte();
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 128;
                                }
                            }
                        }
                        else if ((Width == 64) && (Height == 64))
                        {
                            if (Mip == 1)
                            {
                                for (int i = 0; i <= 31; i++)
                                {
                                    for (int j = 0; j <= 127; j++)
                                        Index[j + i * 128] = BSReader.ReadByte();
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 64;
                                }
                            }
                            else
                                for (int i = 0; i <= 31; i++)
                                {
                                    for (int j = 0; j <= 127; j++)
                                        Index[j + i * 128] = BSReader.ReadByte();
                                    for (int j = 0; j <= 63; j++)
                                        MipIndex[j + i * 64] = BSReader.ReadByte();
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                }
                        }
                        else
                            Interaction.MsgBox("ID:" + ID.ToString() + " Width: " + Width.ToString() + " Height: " + Height.ToString(), MsgBoxStyle.Exclamation, "Kesha, we have a problem!");
                        UnSwizzle(ref Index, (ushort)Width, (ushort)Height);
                        UnSwizzle(ref MipIndex, MipWidth, (ushort)Height);
                        Flip(ref Index, (ushort)Width, (ushort)Height);
                        SwapPalette(ref Palette);
                        for (int i = 0; i <= RawData.Length - 1; i++)
                            RawData[i] = Palette[Index[i]];
                        break;
                    }

                case 2:
                    {
                        Array.Resize(ref Index, (int)(Width * Height));
                        Array.Resize(ref MipIndex, (int)(MipWidth * Height));
                        Array.Resize(ref Palette, 512);
                        if ((Width == 128) && (Height == 32))
                        {
                            Index = BSReader.ReadBytes(4096);
                            for (int i = 0; i <= 15; i++)
                            {
                                for (int j = 0; j <= 127; j++)
                                    MipIndex[j + i * 128] = BSReader.ReadByte();
                                for (int j = 0; j <= 15; j++)
                                {
                                    byte a, r, g, b;
                                    r = BSReader.ReadByte();
                                    g = BSReader.ReadByte();
                                    b = BSReader.ReadByte();
                                    a = (byte)(BSReader.ReadByte() << 1);
                                    Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                }
                                ByteStream.Position += 64;
                            }
                        }
                        else if ((Width == 128) && (Height == 64))
                        {
                            Index = BSReader.ReadBytes(8192);
                            for (int i = 0; i <= 15; i++)
                            {
                                for (int j = 0; j <= 63; j++)
                                    MipIndex[j + i * 64] = BSReader.ReadByte();
                                ByteStream.Position += 192;
                            }
                            for (int i = 0; i <= 15; i++)
                            {
                                for (int j = 0; j <= 63; j++)
                                    MipIndex[512 + j + i * 64] = BSReader.ReadByte();
                                for (int j = 0; j <= 15; j++)
                                {
                                    byte a, r, g, b;
                                    r = BSReader.ReadByte();
                                    g = BSReader.ReadByte();
                                    b = BSReader.ReadByte();
                                    a = (byte)(BSReader.ReadByte() << 1);
                                    Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                }
                                ByteStream.Position += 128;
                            }
                        }
                        else if ((Width == 128) && (Height == 128))
                        {
                            Index = BSReader.ReadBytes(16384);
                            if (Mip > 1)
                            {
                                for (int i = 0; i < 32; i++)
                                {
                                    for (int j = 0; j < 192; j++)
                                        MipIndex[j + i * 192] = BSReader.ReadByte();
                                    for (int j = 0; j < 16; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                }
                            }
                            else
                                for (int i = 0; i <= 31; i++)
                                {
                                    for (int j = 0; j <= 15; j++)
                                    {
                                        byte a, r, g, b;
                                        r = BSReader.ReadByte();
                                        g = BSReader.ReadByte();
                                        b = BSReader.ReadByte();
                                        a = (byte)(BSReader.ReadByte() << 1);
                                        Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                    }
                                    ByteStream.Position += 192;
                                }
                        }
                        else if ((Width == 128) && (Height == 256))
                        {
                            Index = BSReader.ReadBytes(32768);
                            for (int i = 0; i < 32; i++)
                            {
                                for (int j = 0; j < 16; j++)
                                {
                                    byte a, r, g, b;
                                    r = BSReader.ReadByte();
                                    g = BSReader.ReadByte();
                                    b = BSReader.ReadByte();
                                    a = (byte)(BSReader.ReadByte() << 1);
                                    Palette[j + i * 16] = Color.FromArgb(a, r, g, b);
                                }
                                ByteStream.Position += 192;
                            }
                        }
                        else
                            Interaction.MsgBox("ID:" + ID.ToString() + " Width: " + Width.ToString() + " Height: " + Height.ToString(), MsgBoxStyle.Exclamation, "Kesha, we have a problem! Abort now!");
                        UnSwizzle(ref Index, (ushort)Width, (ushort)Height);
                        Flip(ref Index, (ushort)Width, (ushort)Height);
                        SwapPalette(ref Palette);
                        Array.Resize(ref RawData, Index.Length);
                        for (int i = 0; i <= RawData.Length - 1; i++)
                            RawData[i] = Palette[Index[i]];
                        break;
                    }

                case 4:
                    {
                        Array.Resize(ref RawData, (int)(Width * Height));
                        for (int i = 0; i <= (Width) * Height - 1; i++)
                        {
                            byte a, r, g, b;
                            r = BSReader.ReadByte();
                            g = BSReader.ReadByte();
                            b = BSReader.ReadByte();
                            a = (byte)(BSReader.ReadByte() << 1);
                            RawData[i] = Color.FromArgb(a, r, g, b);
                        }

                        break;
                    }
            }
        }

        #region INTERNALS
        internal void UnSwizzle(ref byte[] indexes, ushort width, ushort height)
        {
            byte[] tmp = new byte[indexes.Length - 1 + 1];
            indexes.CopyTo(tmp, 0);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int block_location = (y & (~0xF)) * width + (x & (~0xF)) * 2;
                    int swap_selector = (((y + 2) >> 2) & 0x1) * 4;
                    int posY = (((y & (~3)) >> 1) + (y & 1)) & 0x7;
                    int posX = posY * width * 2 + ((x + swap_selector) & 0x7) * 4;
                    var byte_num = ((y >> 1) & 1) + ((x >> 2) & 2);
                    indexes[Math.Min((y * width) + x, indexes.Length - 1)] = tmp[Math.Min(block_location + posX + byte_num, tmp.Length - 1)];
                }
            }
        }
        internal void Swizzle(ref byte[] indexes, ushort width, ushort height)
        {
            byte[] tmp = new byte[indexes.Length - 1 + 1];
            indexes.CopyTo(tmp, 0);
            for (int y = 0; y <= height - 1; y++)
            {
                for (int x = 0; x <= width - 1; x++)
                {
                    int block_location = (y & (~0xF)) * width + (x & (~0xF)) * 2;
                    int swap_selector = (((y + 2) >> 2) & 0x1) * 4;
                    int posY = (((y & (~3)) >> 1) + (y & 1)) & 0x7;
                    int posX = posY * width * 2 + ((x + swap_selector) & 0x7) * 4;
                    var byte_num = ((y >> 1) & 1) + ((x >> 2) & 2);
                    indexes[Math.Min(block_location + posX + byte_num, tmp.Length - 1)] = tmp[Math.Min((y * width) + x, indexes.Length - 1)];
                }
            }
        }
        internal void Flip(ref byte[] Indexes, ushort width, ushort height)
        {
            height = (ushort)(Indexes.Length / width);
            for (uint y = 0; y <= height / (double)2 - 1; y++)
            {
                for (uint x = 0; x <= width - 1; x++)
                {
                    byte tmp = Indexes[y * width + x];
                    Indexes[y * width + x] = Indexes[(height - y - 1) * width + x];
                    Indexes[(height - y - 1) * width + x] = tmp;
                }
            }
        }
        internal byte SwapBit(byte num, byte b1, byte b2)
        {
            byte mask = (byte)(1 << b1 + 1 << b2);
            byte new_num = (byte)(num & (~mask));
            byte swap = (byte)(((num & (1 << b1)) >> b1) << b2 + ((num & (1 << b2)) >> b2) << b1);
            return (byte)(swap + new_num);
        }
        internal void SwapPalette(ref Color[] Palette)
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 8 + i * 32; j <= 15 + i * 32; j++)
                {
                    Color tmp = Palette[j];
                    Palette[j] = Palette[j + 8];
                    Palette[j + 8] = tmp;
                }
            }
        }
        
        internal void GenerateMips(byte[] Index, uint Width, uint Height, ref byte[] MipIndex)
        {
            byte mips = (byte)(Math.Log(Width, 2) - 2);
            byte[][] tmp = new byte[Index.Length][];
            Array.Resize(ref tmp[0], Index.Length);
            Index.CopyTo(tmp[0], 0);
            Array.Resize(ref MipIndex, (int)(Width * Height / 2));
            ushort w, h, yoffset = 0;
            w = (ushort)Width;
            h = (ushort)Height;
            for (int i = 0; i <= MipIndex.Length - 1; i++)
                MipIndex[i] = 255;
            for (int i = 1; i <= mips; i++)
            {
                w /= 2;
                h /= 2;
                MakeMip(tmp[i - 1], ref tmp[i]);
                Swizzle(ref tmp[i], w, h);
                Flip(ref tmp[i], w, h);
                MergeSurfaces(Width / 2, Height, w, h, 0, yoffset, ref MipIndex, ref tmp[i]);
            }
        }

        internal void MakeMip(byte[] Index, ref byte[] MipIndex)
        {
            Array.Resize(ref MipIndex, Index.Length / 2);
            for (int i = 0; i <= Index.Length - 1; i += 4)
                MipIndex[i / 2] = Index[i];
        }

        internal void MergeSurfaces(uint Width, uint Height, uint MWidth, uint MHeigth, uint x, uint y, ref byte[] Index, ref byte[] MipIndex)
        {
            for (int i = 0; i <= MHeigth - 1; i++)
            {
                for (int j = 0; j <= MWidth - 1; j++)
                    Index[i * Width + j] = MipIndex[i * MWidth + j];
            }
        }

        internal void MakeInterleave(ref System.IO.MemoryStream Data, byte[] Index, Color[] Palette, byte[] MipIndex, BlockFormats Format)
        {
            BlockFormat[][] fmt = new BlockFormat[][] { };
            InitFMT(ref fmt);
            Data = new System.IO.MemoryStream();
            System.IO.BinaryWriter DWriter = new System.IO.BinaryWriter(Data);
            uint ind_offset = 0;
            uint mip_offset = 0;
            uint pal_offset = 0;
            var format_ind = (int)Format;
            for (int i = 0; i <= fmt[format_ind].Length - 1; i++)
            {
                for (int j = 0; j <= fmt[format_ind][i].Index - 1; j++)
                    DWriter.Write(Index[j + ind_offset]);
                for (int j = 0; j <= fmt[format_ind][i].Mip - 1; j++)
                    DWriter.Write(MipIndex[j + mip_offset]);
                for (int j = 0; j <= fmt[format_ind][i].Palette / 4 - 1; j++)
                {
                    DWriter.Write(Palette[j + pal_offset].R);
                    DWriter.Write(Palette[j + pal_offset].G);
                    DWriter.Write(Palette[j + pal_offset].B);
                    DWriter.Write(Palette[j + pal_offset].A);
                }
                for (int j = 0; j <= fmt[format_ind][i].Space - 1; j++)
                    DWriter.Write(System.Convert.ToByte(255));
                ind_offset += fmt[format_ind][i].Index;
                mip_offset += fmt[format_ind][i].Mip;
                pal_offset += (uint)fmt[format_ind][i].Palette / 4;
            }
        }

        internal void InitFMT(ref BlockFormat[][] fmt)
        {
            Array.Resize(ref fmt, 19);
            // fmt128x256 128I256 16P64S192 16S256
            Array.Resize(ref fmt[0], 160);
            for (int i = 0; i <= 127; i++)
                fmt[0][i] = new BlockFormat(256, 0, 0, 0);
            for (int i = 128; i <= 143; i++)
                fmt[0][i] = new BlockFormat(0, 0, 64, 192);
            for (int i = 144; i <= 159; i++)
                fmt[0][i] = new BlockFormat(0, 0, 0, 256);
            // fmt128x128 64I256 16P64S192 16S256
            Array.Resize(ref fmt[1], 96);
            for (int i = 0; i <= 63; i++)
                fmt[1][i] = new BlockFormat(256, 0, 0, 0);
            for (int i = 64; i <= 79; i++)
                fmt[1][i] = new BlockFormat(0, 0, 64, 192);
            for (int i = 80; i <= 95; i++)
                fmt[1][i] = new BlockFormat(0, 0, 0, 256);
            // fmt128x128mip 64I256 16M192P64 16S256 
            Array.Resize(ref fmt[2], 96);
            for (int i = 0; i <= 63; i++)
                fmt[2][i] = new BlockFormat(256, 0, 0, 0);
            for (int i = 64; i <= 79; i++)
                fmt[2][i] = new BlockFormat(0, 192, 64, 0);
            for (int i = 80; i <= 95; i++)
                fmt[2][i] = new BlockFormat(0, 0, 0, 256);
            // fmt128x64 32I256 16P64S192 16S256
            Array.Resize(ref fmt[3], 64);
            for (int i = 0; i <= 31; i++)
                fmt[3][i] = new BlockFormat(256, 0, 0, 0);
            for (int i = 32; i <= 47; i++)
                fmt[3][i] = new BlockFormat(0, 0, 64, 192);
            for (int i = 48; i <= 63; i++)
                fmt[3][i] = new BlockFormat(0, 0, 0, 256);
            // fmt128x64mip 32I256 16M64S192 16M64P64S128
            Array.Resize(ref fmt[4], 64);
            for (int i = 0; i <= 31; i++)
                fmt[4][i] = new BlockFormat(256, 0, 0, 0);
            for (int i = 32; i <= 47; i++)
                fmt[4][i] = new BlockFormat(0, 64, 0, 192);
            for (int i = 48; i <= 63; i++)
                fmt[4][i] = new BlockFormat(0, 64, 64, 128);
            // fmt128x32 16I256 16P64S192 16S256
            Array.Resize(ref fmt[5], 48);
            for (int i = 0; i <= 15; i++)
                fmt[5][i] = new BlockFormat(256, 0, 0, 0);
            for (int i = 16; i <= 31; i++)
                fmt[5][i] = new BlockFormat(0, 0, 64, 192);
            for (int i = 32; i <= 47; i++)
                fmt[5][i] = new BlockFormat(0, 0, 0, 256);
            // fmt128x32mip 16I256 16M128P64S64 16S256
            Array.Resize(ref fmt[6], 48);
            for (int i = 0; i <= 15; i++)
                fmt[6][i] = new BlockFormat(256, 0, 0, 0);
            for (int i = 16; i <= 31; i++)
                fmt[6][i] = new BlockFormat(0, 128, 64, 64);
            for (int i = 32; i <= 47; i++)
                fmt[6][i] = new BlockFormat(0, 0, 0, 256);
            // fmt64x64 16I128P64S64 16I128S128
            Array.Resize(ref fmt[7], 32);
            for (int i = 0; i <= 15; i++)
                fmt[7][i] = new BlockFormat(128, 0, 64, 64);
            for (int i = 16; i <= 31; i++)
                fmt[7][i] = new BlockFormat(128, 0, 0, 128);
            // fmt64x64mip 16I128M64P64 16I128M64S64
            Array.Resize(ref fmt[8], 32);
            for (int i = 0; i <= 15; i++)
                fmt[8][i] = new BlockFormat(128, 0, 64, 64);
            for (int i = 16; i <= 31; i++)
                fmt[8][i] = new BlockFormat(128, 64, 0, 64);
            // fmt64x32 16I128S128 16P64S192
            Array.Resize(ref fmt[9], 32);
            for (int i = 0; i <= 15; i++)
                fmt[9][i] = new BlockFormat(128, 0, 0, 128);
            for (int i = 16; i <= 31; i++)
                fmt[9][i] = new BlockFormat(0, 0, 64, 192);
            // fmt64x32mip 16I128S128 16M64P64S128
            Array.Resize(ref fmt[10], 32);
            for (int i = 0; i <= 15; i++)
                fmt[10][i] = new BlockFormat(128, 0, 0, 128);
            for (int i = 16; i <= 31; i++)
                fmt[10][i] = new BlockFormat(0, 64, 64, 128);
            // fmt32x64 16I64P64S128 16I64S192
            Array.Resize(ref fmt[11], 32);
            for (int i = 0; i <= 15; i++)
                fmt[11][i] = new BlockFormat(64, 0, 64, 128);
            for (int i = 16; i <= 31; i++)
                fmt[11][i] = new BlockFormat(64, 0, 0, 192);
            // fmt32x64mip 16I64M64P64S64 16I64S192
            Array.Resize(ref fmt[12], 32);
            for (int i = 0; i <= 15; i++)
                fmt[12][i] = new BlockFormat(64, 64, 64, 64);
            for (int i = 16; i <= 31; i++)
                fmt[12][i] = new BlockFormat(64, 0, 0, 192);
            // fmt32x32 16I64S192 16P64S192
            Array.Resize(ref fmt[13], 32);
            for (int i = 0; i <= 15; i++)
                fmt[13][i] = new BlockFormat(64, 0, 0, 192);
            for (int i = 16; i <= 31; i++)
                fmt[13][i] = new BlockFormat(0, 0, 64, 192);
            // fmt32x32mip 16I64P64S128 16M32S224
            Array.Resize(ref fmt[14], 32);
            for (int i = 0; i <= 15; i++)
                fmt[14][i] = new BlockFormat(64, 0, 64, 128);
            for (int i = 16; i <= 31; i++)
                fmt[14][i] = new BlockFormat(0, 32, 0, 224);
            // fmt32x16 8I64S192 16P64S192 8S256 EXPEREMENTAL!
            Array.Resize(ref fmt[15], 32);
            for (int i = 0; i <= 7; i++)
                fmt[15][i] = new BlockFormat(64, 0, 0, 192);
            for (int i = 8; i <= 15; i++)
                fmt[15][i] = new BlockFormat(0, 0, 64, 224);
            for (int i = 16; i <= 31; i++)
                fmt[15][i] = new BlockFormat(0, 0, 0, 256);
            // fmt32x16mip 8I64S192 4M32S224 4S256 16P64S192
            Array.Resize(ref fmt[16], 32);
            for (int i = 0; i <= 7; i++)
                fmt[16][i] = new BlockFormat(64, 0, 0, 192);
            for (int i = 8; i <= 11; i++)
                fmt[16][i] = new BlockFormat(0, 32, 0, 224);
            for (int i = 12; i <= 15; i++)
                fmt[16][i] = new BlockFormat(0, 0, 0, 256);
            for (int i = 16; i <= 31; i++)
                fmt[16][i] = new BlockFormat(0, 0, 64, 192);
            // fmt32x8 4I64S192 12S256 16P64S192
            Array.Resize(ref fmt[17], 32);
            for (int i = 0; i <= 3; i++)
                fmt[17][i] = new BlockFormat(64, 0, 0, 192);
            for (int i = 4; i <= 15; i++)
                fmt[17][i] = new BlockFormat(0, 0, 0, 256);
            for (int i = 16; i <= 31; i++)
                fmt[17][i] = new BlockFormat(0, 0, 64, 192);
            // fmt16x16mip 8I32S224 4M32S224 4S256 16P64
            Array.Resize(ref fmt[18], 32);
            for (int i = 0; i <= 7; i++)
                fmt[18][i] = new BlockFormat(32, 0, 0, 224);
            for (int i = 8; i <= 11; i++)
                fmt[18][i] = new BlockFormat(0, 32, 0, 224);
            for (int i = 12; i <= 15; i++)
                fmt[18][i] = new BlockFormat(0, 0, 0, 256);
            for (int i = 16; i <= 31; i++)
                fmt[18][i] = new BlockFormat(0, 0, 64, 192);
        }

        internal void FormHeader(ref System.IO.MemoryStream Header, BlockFormats fmt)
        {
            Header = new System.IO.MemoryStream(228);
            System.IO.BinaryWriter DWriter = new System.IO.BinaryWriter(Header);
            Header.Position = 0;
            switch (fmt)
            {
                case BlockFormats.fmt128x256:
                    {
                        
                        DWriter.Write(Resources._128x256);
                        break;
                    }

                case BlockFormats.fmt128x128:
                    {
                        DWriter.Write(Resources._128x128);
                        break;
                    }

                case BlockFormats.fmt128x128mip:
                    {
                        DWriter.Write(Resources._128x128mip);
                        break;
                    }

                case BlockFormats.fmt128x64:
                    {
                        break;
                    }

                case BlockFormats.fmt128x64mip:
                    {
                        DWriter.Write(Resources._128x64mip);
                        break;
                    }

                case BlockFormats.fmt128x32:
                    {
                        break;
                    }

                case BlockFormats.fmt128x32mip:
                    {
                        DWriter.Write(Resources._128x32mip);
                        break;
                    }

                case BlockFormats.fmt64x64:
                    {
                        DWriter.Write(Resources._64x64);
                        break;
                    }

                case BlockFormats.fmt64x64mip:
                    {
                        DWriter.Write(Resources._64x64mip);
                        break;
                    }

                case BlockFormats.fmt64x32:
                    {
                        break;
                    }

                case BlockFormats.fmt64x32mip:
                    {
                        DWriter.Write(Resources._64x32mip);
                        break;
                    }

                case BlockFormats.fmt32x64:
                    {
                        DWriter.Write(Resources._32x64);
                        break;
                    }

                case BlockFormats.fmt32x64mip:
                    {
                        DWriter.Write(Resources._32x64mip);
                        break;
                    }

                case BlockFormats.fmt32x32:
                    {
                        break;
                    }

                case BlockFormats.fmt32x32mip:
                    {
                        DWriter.Write(Resources._32x32mip);
                        break;
                    }

                case BlockFormats.fmt32x16:
                    {
                        break;
                    }

                case BlockFormats.fmt32x16mip:
                    {
                        DWriter.Write(Resources._32x16mip);
                        break;
                    }

                case BlockFormats.fmt32x8:
                    {
                        DWriter.Write(Resources._32x8);
                        break;
                    }

                case BlockFormats.fmt16x16:
                    {
                        break;
                    }

                case BlockFormats.fmt16x16mip:
                    {
                        DWriter.Write(Resources._16x16mip);
                        break;
                    }
            }
        }
        #endregion

        #region STRUCTURES
        public struct BlockFormat
        {
            public ushort Index;
            public ushort Mip;
            public ushort Palette;
            public ushort Space;
            public BlockFormat(ushort pIndex, ushort pMip, ushort pPalette, ushort pSpace)
            {
                Index = pIndex;
                Mip = pMip;
                Palette = pPalette;
                Space = pSpace;
            }
        }
        public enum BlockFormats
        {
            fmt128x256 = 0,
            fmt128x128 = 1,
            fmt128x128mip = 2,
            fmt128x64 = 3,
            fmt128x64mip = 4,
            fmt128x32 = 5,
            fmt128x32mip = 6,
            fmt64x64 = 7,
            fmt64x64mip = 8,
            fmt64x32 = 9,
            fmt64x32mip = 10,
            fmt32x64 = 11,
            fmt32x64mip = 12,
            fmt32x32 = 13,
            fmt32x32mip = 14,
            fmt32x16 = 15,
            fmt32x16mip = 16,
            fmt32x8 = 17,
            fmt16x16 = 18,
            fmt16x16mip = 19
        }
        #endregion
    }
}
