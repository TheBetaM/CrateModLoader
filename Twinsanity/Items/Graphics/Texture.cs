using System.IO;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Twinsanity
{
    /// <summary>
    /// Represents Twinsanity's Texture
    /// </summary>
    public class Texture : TwinsItem
    {
        private int texSize;
        private int unkInt;
        private short w, h;
        private byte m;
        private byte format;
        private byte destinationFormat;
        private byte texColComponent; // 0 - RGB, 1 - RGBA
        private byte unkByte;
        private byte textureFun;
        private byte[] unkBytes = new byte[2];
        private int textureBasePointer;
        private int[] mipLevelsTBP;
        private int textureBufferWidth;
        private int[] mipLevelsTBW;
        private int clutBufferBasePointer;
        private byte[] unkBytes2 = new byte[8];
        private byte[] unkBytes3 = new byte[2];
        private byte[] unusedMetadata = new byte[32]; // This metadata is read but discarded afterwards in game's code
        private byte[] vifBlock = new byte[96]; // This holds a VIF code block, which does some basic texture setup, we don't care about it
        private byte[] imageData;
        private Color[] palette;
        private Color[][] mips;
        private int rrw;
        private int rrh;

        public int Width { get => 1 << w; }
        public int Height { get => 1 << h; }
        public int MipLevels { get => m - 1; }
        public TexturePixelFormat PixelFormat { get => (TexturePixelFormat)format; }
        public TexturePixelFormat DestinationPixelFormat { get => (TexturePixelFormat)destinationFormat; }
        public TextureColorComponent ColorComponent { get => (TextureColorComponent)texColComponent; }
        public TextureFunction TexFun { get => (TextureFunction)textureFun; }
        public Color[] RawData { get; set; }
        public int TextureBufferWidth { get => textureBufferWidth * 64; }

        public Color[] GetMips(int level)
        {
            var index = Math.Min(MipLevels - 1, level);
            return mips[index];
        }

        public override void Load(BinaryReader reader, int size)
        {
            texSize = reader.ReadInt32();
            // Texture header
            unkInt = reader.ReadInt32();
            w = reader.ReadInt16();
            h = reader.ReadInt16();
            m = reader.ReadByte();
            format = reader.ReadByte();
            destinationFormat = reader.ReadByte();
            texColComponent = reader.ReadByte();
            unkByte = reader.ReadByte();
            textureFun = reader.ReadByte();
            unkBytes = reader.ReadBytes(2);
            textureBasePointer = reader.ReadInt32();
            mipLevelsTBP = new int[6];
            for (var i = 0; i < 6; ++i)
            {
                mipLevelsTBP[i] = reader.ReadInt32();
            }
            textureBufferWidth = reader.ReadInt32();
            mipLevelsTBW = new int[6];
            for (var i = 0; i < 6; ++i)
            {
                mipLevelsTBW[i] = reader.ReadInt32();
            }
            clutBufferBasePointer = reader.ReadInt32();
            unkBytes2 = reader.ReadBytes(8);
            reader.ReadInt32(); // Reserved, in game's code refers to an index of vifCodeBlock
            reader.ReadInt32(); // Reserved, in game's code refers to an unknown pointer
            unkBytes3 = reader.ReadBytes(2);
            reader.ReadBytes(2); // Reserved, unknown
            // The rest of data
            unusedMetadata = reader.ReadBytes(32);
            vifBlock = reader.ReadBytes(96);
            // Jumping stream a tiny bit to grab the RRW and RRH from the TRXREG :^)
            var afterVifPos = reader.BaseStream.Position;
            reader.BaseStream.Position -= 96;
            reader.ReadBytes(48);
            rrw = reader.ReadInt32();
            rrh = reader.ReadInt32();

            reader.BaseStream.Position = afterVifPos;
            switch(PixelFormat)
            {
                case TexturePixelFormat.PSMCT32: // Easiest one, raw color data
                    imageData = reader.ReadBytes(texSize - 224);
                    RawData = new Color[Width * Height];
                    var pxInd = 0;
                    for (var i = 0; i < texSize - 224; i += 4)
                    {
                        var r = imageData[i];
                        var g = imageData[i + 1];
                        var b = imageData[i + 2];
                        var a = (byte)(imageData[i + 3] << 1);
                        RawData[pxInd++] = Color.FromArgb(a, r, g, b);
                    }
                    break;
                case TexturePixelFormat.PSMT8: // End me, this cost me a tiny bit of my soul

                    // The limit seems to be 128 in width textures. For bigger widths PSMCT32 is always used
                    // when setting up import/export we need to take that into account
                    EzSwizzle ez = new EzSwizzle();
                    imageData = reader.ReadBytes(texSize - 224);
                    ez.cleanGs();
                    // Deinterleave texture data, even though texture buffer width
                    // for the main texture can be bigger than 1, the BITBLTBUF register's DBW field
                    // in Twins engine is always set to 1. Note: Unless the format is PSMCT32 but that doesn't matter since that is not swizzled
                    // or interleaved in any way
                    ez.writeTexPSMCT32(0, 1, 0, 0, rrw, rrh, imageData);

                    // Unswizzle main texture data
                    var texData = new byte[Width * Height];
                    ez.readTexPSMT8(0, textureBufferWidth, 0, 0, Width, Height, ref texData);

                    // Palette
                    #region Palette reading
                    var palette = new byte[256 * 4];
                    ez.readTexPSMCT32(clutBufferBasePointer, 1, 0, 0, 16, 16, ref palette);
                    this.palette = new Color[256];
                    var palInd = 0;
                    for (var i = 0; i < 256 * 4; i += 4)
                    {
                        var r = palette[i];
                        var g = palette[i + 1];
                        var b = palette[i + 2];
                        var a = (byte)(palette[i + 3] << 1);
                        this.palette[palInd++] = Color.FromArgb(a, r, g, b);
                    }
                    // Palette swapping
                    SwapPalette(ref this.palette);
                    #endregion

                    // Mips
                    #region Mips reading
                    mips = new Color[MipLevels][];
                    for (var i = 0; i < MipLevels; ++i)
                    {
                        // Each mip becomes increasingly smaller. Minimum supported mip by PS2 is 8x8
                        var mipWidth = (Width / (1 << (i + 1)));
                        var mipHeight = (Height / (1 << (i + 1)));
                        var mipData = new byte[mipWidth * mipHeight];
                        // Unswizzle mip data
                        ez.readTexPSMT8(mipLevelsTBP[i], mipLevelsTBW[i], 0, 0, mipWidth, mipHeight, ref mipData);
                        Flip(ref mipData, mipWidth, mipHeight);
                        mips[i] = new Color[mipWidth * mipHeight];
                        for (var j = 0; j < mipWidth * mipHeight; ++j)
                        {
                            mips[i][j] = this.palette[mipData[j]];
                        }
                    }
                    #endregion

                    // Flip the image
                    Flip(ref texData, Width, Height);

                    RawData = new Color[Width * Height];
                    for (var i = 0; i < Width * Height; ++i)
                    {
                        RawData[i] = this.palette[texData[i]];
                    }
                    break;
                default:
                    imageData = reader.ReadBytes(texSize - 224);
                    break;
            }
        }

        public override void Save(BinaryWriter writer)
        {
            // Raw colors to texture data
            switch (PixelFormat)
            {
                case TexturePixelFormat.PSMCT32:

                    imageData = new byte[RawData.Length * 4];
                    for (int i = 0; i < RawData.Length; i++)
                    {
                        imageData[(i * 4) + 0] = RawData[i].R;
                        imageData[(i * 4) + 1] = RawData[i].G;
                        imageData[(i * 4) + 2] = RawData[i].B;
                        imageData[(i * 4) + 3] = (byte)(RawData[i].A >> 1);
                    }

                    break;
                case TexturePixelFormat.PSMT8:
                    break;
                default:
                    break;
            }

            writer.Write(texSize);
            writer.Write(unkInt);
            writer.Write(w);
            writer.Write(h);
            writer.Write(m);
            writer.Write(format);
            writer.Write(destinationFormat);
            writer.Write(texColComponent);
            writer.Write(unkByte);
            writer.Write(textureFun);
            writer.Write(unkBytes);
            writer.Write(textureBasePointer);
            for (var i = 0; i < 6; ++i)
            {
                writer.Write(mipLevelsTBP[i]);
            }
            writer.Write(textureBufferWidth);
            for (var i = 0; i < 6; ++i)
            {
                writer.Write(mipLevelsTBW[i]);
            }
            writer.Write(clutBufferBasePointer);
            writer.Write(unkBytes2);
            writer.Write(0);
            writer.Write(0);
            writer.Write(unkBytes3);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(unusedMetadata);
            writer.Write(vifBlock);
            writer.Write(imageData);

        }

        protected override int GetSize()
        {
            return texSize + 4;
        }

        public void ConvertToPSMCT32()
        {
            // an attempt was made...
            format = (byte)TexturePixelFormat.PSMCT32;
            m = 1;
            textureBufferWidth = 4;
            for (var i = 0; i < 6; ++i)
            {
                mipLevelsTBP[i] = 0;
            }
            for (var i = 0; i < 6; ++i)
            {
                mipLevelsTBW[i] = 0;
            }

            UpdateImageData();

        }

        /// <summary>
        /// Convert RawData or input colors to image data that will be saved when the Texture is saved.
        /// </summary>
        public void UpdateImageData(Color[] inputColors = null)
        {
            if (inputColors != null)
            {
                RawData = inputColors;
            }

            switch (PixelFormat)
            {
                case TexturePixelFormat.PSMCT32:

                    imageData = new byte[RawData.Length * 4];
                    for (int i = 0; i < RawData.Length; i++)
                    {
                        imageData[(i * 4) + 0] = RawData[i].R;
                        imageData[(i * 4) + 1] = RawData[i].G;
                        imageData[(i * 4) + 2] = RawData[i].B;
                        imageData[(i * 4) + 3] = (byte)(RawData[i].A >> 1);
                    }

                    texSize = imageData.Length + 224;
                    break;
                case TexturePixelFormat.PSMT8:

                    m = 1; // no mips!!!!!
                    for (var i = 0; i < 6; ++i)
                    {
                        mipLevelsTBP[i] = 0;
                    }
                    for (var i = 0; i < 6; ++i)
                    {
                        mipLevelsTBW[i] = 0;
                    }

                    EzSwizzle ez = new EzSwizzle();
                    ez.cleanGs();

                    var texData = new byte[Width * Height];
                    var palette = new byte[256 * 4];
                    this.palette = new Color[256];
                    var palInd = 0;
                    imageData = new byte[texSize - 224];

                    //Create palette, texData
                    List<Color> UniqueColors = GetUniqueColors(RawData);
                    if (UniqueColors.Count > 256)
                    {
                        // Only use the most used colors
                        TruncatePalette(ref UniqueColors);
                    }
                    for (int i = 0; i < UniqueColors.Count; i++)
                    {
                        this.palette[i] = UniqueColors[i];
                    }
                    for (int i = 0; i < Width * Height; i++)
                    {
                        texData[i] = (byte)GetClosestColorPos(RawData[i], UniqueColors);
                    }

                    Flip(ref texData, Width, Height);

                    SwapPalette(ref this.palette);
                    for (var i = 0; i < 256 * 4; i += 4)
                    {
                        palette[i] = this.palette[palInd].R;
                        palette[i + 1] = this.palette[palInd].G;
                        palette[i + 2] = this.palette[palInd].B;
                        palette[i + 3] = (byte)(this.palette[palInd].A >> 1);
                        palInd++;
                    }

                    ez.writeTexPSMCT32(clutBufferBasePointer, 1, 0, 0, 16, 16, palette);

                    // Swizzle main texture data?
                    ez.writeTexPSMT8(0, textureBufferWidth, 0, 0, Width, Height, texData);

                    // Interleave texture data?
                    ez.readTexPSMCT32(0, 1, 0, 0, rrw, rrh, ref imageData);


                    break;
                default:
                    break;
            }
            
        }

        #region Util

        private void Flip(ref byte[] Indexes, int width, int height)
        {
            for (uint y = 0; y < height / 2; y++)
            {
                for (uint x = 0; x < width; x++)
                {
                    byte tmp = Indexes[y * width + x];
                    Indexes[y * width + x] = Indexes[(height - y - 1) * width + x];
                    Indexes[(height - y - 1) * width + x] = tmp;
                }
            }
        }

        internal void SwapPalette(ref Color[] palette)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 8 + i * 32; j < 16 + i * 32; j++)
                {
                    Color tmp = palette[j];
                    palette[j] = palette[j + 8];
                    palette[j + 8] = tmp;
                }
            }
        }
        
        private void GenerateMips(byte[] pixels, uint Width, uint Height, ref byte[] mippixels)
        {
            throw new NotImplementedException();
        }

        private void MakeMip(byte[] pixels, ref byte[] mippixels)
        {
            throw new NotImplementedException();
        }

        private List<Color> GetUniqueColors(Color[] Data)
        {
            List<Color> unique = new List<Color>();
            for (int i = 0; i < Data.Length; i++)
            {
                if (!unique.Contains(Data[i]))
                {
                    unique.Add(Data[i]);
                    
                }
            }
            return unique;
        }

        private Color GetClosestColor(Color Source, List<Color> Palette)
        {
            List<int> Distances = new List<int>();
            for (int i = 0; i < Palette.Count; i++)
            {
                int Dist_R = Math.Abs(Palette[i].R - Source.R);
                int Dist_G = Math.Abs(Palette[i].G - Source.G);
                int Dist_B = Math.Abs(Palette[i].B - Source.B);
                int Dist_A = Math.Abs(Palette[i].A - Source.A);
                Distances.Add(Dist_R + Dist_G + Dist_B + Dist_A);
            }
            int minDist = 3000;
            int minDistPos = 0;
            for (int i = 0; i < Distances.Count; i++)
            {
                if (Distances[i] < minDist)
                {
                    minDist = Distances[i];
                    minDistPos = i;
                }
            }
            return Palette[minDistPos];
        }
        private int GetClosestColorPos(Color Source, List<Color> Palette)
        {
            List<int> Distances = new List<int>();
            for (int i = 0; i < Palette.Count; i++)
            {
                int Dist_R = Math.Abs(Palette[i].R - Source.R);
                int Dist_G = Math.Abs(Palette[i].G - Source.G);
                int Dist_B = Math.Abs(Palette[i].B - Source.B);
                int Dist_A = Math.Abs(Palette[i].A - Source.A);
                Distances.Add(Dist_R + Dist_G + Dist_B + Dist_A);
            }
            int minDist = 3000;
            int minDistPos = 0;
            for (int i = 0; i < Distances.Count; i++)
            {
                if (Distances[i] < minDist)
                {
                    minDist = Distances[i];
                    minDistPos = i;
                }
            }
            return minDistPos;
        }

        private void TruncatePalette(ref List<Color> Palette)
        {
            Dictionary<Color, int> ColorUse = new Dictionary<Color, int>();
            int maxVal = 0;
            foreach (Color col in Palette)
            {
                int use = 0;
                for (int i = 0; i < RawData.Length; i++)
                {
                    if (RawData[i] == col)
                    {
                        use++;
                    }
                }
                if (use > maxVal)
                {
                    maxVal = use;
                }
                ColorUse.Add(col, use);
            }

            Palette = new List<Color>();

            while (Palette.Count < 256)
            {
                foreach (KeyValuePair<Color, int> pair in ColorUse)
                {
                    if (pair.Value == maxVal && Palette.Count < 256)
                    {
                        Palette.Add(pair.Key);
                    }
                }
                maxVal--;
                if (maxVal < 0)
                {
                    break;
                }
            }

        }

        #endregion

        #region Enums
        public enum TexturePixelFormat
        {
            PSMCT32     = 0b000000,
            PSMCT24     = 0b000001,
            PSMCT16     = 0b000010,
            PSMCT16S    = 0b001010,
            PSMT8       = 0b010011,
            PSMT4       = 0b010100,
            PSMT8H      = 0b011011,
            PSMT4HL     = 0b100100,
            PSMT4HH     = 0b101100,
            PSMZ32      = 0b110000,
            PSMZ24      = 0b110001,
            PSMZ16      = 0b110010,
            PSMZ16S     = 0b111010
        }
        public enum TextureColorComponent
        {
            RGB = 0,
            RGBA = 1
        }
        public enum TextureFunction
        {
            MODULATE = 0b00,
            DECAL = 0b01,
            HIGHLIGHT = 0b10,
            HIGHLIGHT2 = 0b11
        }
        #endregion
    }
}
