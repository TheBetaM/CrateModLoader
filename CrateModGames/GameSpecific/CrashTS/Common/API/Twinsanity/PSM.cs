using System;
using System.IO;

namespace Twinsanity
{
    /// <summary>
    /// Represents Twinsanity's image file format
    /// </summary>
    public class PSM
    {
        /// <summary>
        /// Structure of a given PSM segment
        /// </summary>
        public struct PSM_Segment
        {
            public uint Key1;
            public uint Key2;
            public Texture Texture;
            public uint SomeInt1;
            public uint SomeInt2;
            public uint SomeInt3;
            public string Name;
            public byte[] Ending; // 98
        }

        /// <summary>
        /// Array of PSM segments in the PSM image
        /// </summary>
        public PSM_Segment[] PSM_Segments = new PSM_Segment[] { };

        #region PSM
        /// <summary>
        /// Load the PSM image
        /// </summary>
        /// <param name="Path">Path to the image</param>
        /// <param name="Demo">Flag to check for demo</param>
        public void LoadPSM(string Path, bool Demo = false)
        {
            FileStream PSM = new FileStream(Path, FileMode.Open, FileAccess.Read);
            BinaryReader PSM_Reader = new BinaryReader(PSM);
            while (PSM.Position < PSM.Length)
            {
                Array.Resize(ref PSM_Segments, PSM_Segments.Length + 1);
                {
                    var withBlock = PSM_Segments[PSM_Segments.Length - 1];
                    _GenericLoad(ref withBlock, ref PSM, ref PSM_Reader, Demo);
                }
            }
            PSM_Reader.Close();
            PSM.Close();
        }
        /// <summary>
        /// Save the PSM
        /// </summary>
        /// <param name="Path">Path to save at</param>
        public void SavePSM(string Path)
        {
            FileStream PSM = new FileStream(Path, FileMode.Create, FileAccess.Write);
            BinaryWriter PSM_Writer = new BinaryWriter(PSM);
            for (int i = 0; i <= PSM_Segments.Length - 1; i++)
            {
                {
                    var withBlock = PSM_Segments[i];
                    _GenericSave(ref withBlock, ref PSM, ref PSM_Writer);
                }
            }
            PSM_Writer.Close();
            PSM.Close();
        }
        /// <summary>
        /// Replace given PSM segment
        /// </summary>
        /// <param name="Image">Given PSM segment</param>
        /// <param name="index">Index of the segment in the full image</param>
        /// <returns>Whether replacement was successful or not</returns>
        public int ReplacePSMSegment(System.Drawing.Bitmap Image, int index)
        {
            byte[] RawData;
            /*for (int x = 0; x <= Image.Width - 1; x++)
            {
                for (int y = 0; y <= Image.Height - 1; y++)
                    RawData[x + y * Image.Width] = Image.GetPixel(x, y);
            }*/

            System.Drawing.Imaging.BitmapData data = Image.LockBits(new System.Drawing.Rectangle(0, 0, Image.Width, Image.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, Image.PixelFormat);
            RawData = new byte[Math.Abs(data.Stride) * Image.Height];
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, RawData, 0, RawData.Length);

            Texture.BlockFormats fmt;
            if (Image.Width == 128)
            {
                if (Image.Height == 256)
                    fmt = Texture.BlockFormats.fmt128x256;
                else if (Image.Height == 128)
                    fmt = Texture.BlockFormats.fmt128x128;
                else if (Image.Height == 64)
                    fmt = Texture.BlockFormats.fmt128x64;
                else if (Image.Height == 32)
                    fmt = Texture.BlockFormats.fmt128x32;
                else
                    return -1;
            }
            else if (Image.Width == 64)
            {
                if (Image.Height == 64)
                    fmt = Texture.BlockFormats.fmt64x64;
                else if (Image.Height == 32)
                    fmt = Texture.BlockFormats.fmt64x32;
                else
                    return -1;
            }
            else if (Image.Width == 32)
            {
                if (Image.Height == 64)
                    fmt = Texture.BlockFormats.fmt32x64;
                else if (Image.Height == 32)
                    fmt = Texture.BlockFormats.fmt32x32;
                else if (Image.Height == 16)
                    fmt = Texture.BlockFormats.fmt32x16;
                else if (Image.Height == 8)
                    fmt = Texture.BlockFormats.fmt32x8;
                else
                    return -1;
            }
            else if (Image.Width == 16)
            {
                if (Image.Height == 16)
                    fmt = Texture.BlockFormats.fmt16x16;
                else
                    return -1;
            }
            else
                return -1;
            PSM_Segments[index].Texture.Import(RawData, (uint)Image.Width, (uint)Image.Height, fmt, false);
            return 0;
        }
        #endregion
        #region PTC
        /// <summary>
        /// Load the PTC file
        /// </summary>
        /// <param name="Path">Path to load from</param>
        /// <param name="Demo">Flag to check for demo</param>
        public void LoadPTC(string Path, bool Demo)
        {
            var PSM = new FileStream(Path, FileMode.Open, FileAccess.Read);
            var PSM_Reader = new BinaryReader(PSM);
            Array.Resize(ref PSM_Segments, 1);
            {
                var withBlock = PSM_Segments[0];
                _GenericLoad(ref withBlock, ref PSM, ref PSM_Reader, Demo);
            }
            PSM_Reader.Close();
            PSM.Close();
        }
        /// <summary>
        /// Save PTC file
        /// </summary>
        /// <param name="Path">Path to save at</param>
        public void SavePTC(string Path)
        {
            FileStream PSM = new FileStream(Path, FileMode.Create, FileAccess.Write);
            BinaryWriter PSM_Writer = new BinaryWriter(PSM);
            {
                var withBlock = PSM_Segments[0];
                _GenericSave(ref withBlock, ref PSM, ref PSM_Writer);
            }
            PSM_Writer.Close();
            PSM.Close();
        }
        #endregion
        #region PSF
        /// <summary>
        /// Load the PSF file
        /// </summary>
        /// <param name="Path">Path to load from</param>
        /// <param name="Demo">Flag to check for demo</param>
        public void LoadPSF(string Path, bool Demo = false)
        {
            FileStream PSM = new FileStream(Path, FileMode.Open, FileAccess.Read);
            BinaryReader PSM_Reader = new BinaryReader(PSM);
            Array.Resize(ref PSM_Segments, Demo ? 1 : PSM_Reader.ReadInt32());
            var withBlock = PSM_Segments[0];
            _GenericLoad(ref withBlock, ref PSM, ref PSM_Reader, Demo);
            if (!Demo)
            {
                for (int i = 1; i < PSM_Segments.Length; ++i)
                {
                    withBlock = PSM_Segments[1];
                    _GenericLoad(ref withBlock, ref PSM, ref PSM_Reader, false);
                }
            }
            PSM_Reader.Close();
            PSM.Close();
            /*if (Demo)
            {
                Array.Resize(ref PSM_Segments, 1);
                {
                    var withBlock = PSM_Segments[0];
                    _GenericLoad(ref withBlock, ref PSM, ref PSM_Reader, Demo);
                }
            }
            else
            {
                uint N = PSM_Reader.ReadUInt32();
                Array.Resize(ref PSM_Segments, (int)N);
                for (int i = 0; i <= N - 1; i++)
                {
                    {
                        var withBlock = PSM_Segments[i];
                        withBlock.Key1 = PSM_Reader.ReadUInt32();
                        withBlock.Key2 = PSM_Reader.ReadUInt32();
                        withBlock.Texture = new Texture();
                        withBlock.Texture.Base = 0;
                        withBlock.Texture.Offset = (uint)PSM.Position;
                        PSM.Position += 1;
                        withBlock.Texture.Size = (uint)PSM_Reader.ReadUInt16() * 256 + 228;
                        withBlock.Texture.Load(ref PSM, ref PSM_Reader);
                        withBlock.SomeInt1 = PSM_Reader.ReadUInt32();
                        withBlock.SomeInt2 = PSM_Reader.ReadUInt32();
                        withBlock.SomeInt3 = PSM_Reader.ReadUInt32();
                        withBlock.Name = PSM_Reader.ReadChars((int)PSM_Reader.ReadUInt32()).ToString();
                        withBlock.Ending = PSM_Reader.ReadBytes(98);
                    }
                }
            }*/
        }
        /// <summary>
        /// Save the PSF file
        /// </summary>
        /// <param name="Path">Path to save at</param>
        /// <param name="Demo">Flag to check for demo</param>
        public void SavePSF(string Path, bool Demo)
        {
            FileStream PSM = new FileStream(Path, FileMode.Create, FileAccess.Write);
            BinaryWriter PSM_Writer = new BinaryWriter(PSM);
            if (!Demo)
                PSM_Writer.Write(PSM_Segments.Length);
            for (int i = 0; i <= PSM_Segments.Length - 1; i++)
            {
                {
                    var withBlock = PSM_Segments[i];
                    _GenericSave(ref withBlock, ref PSM, ref PSM_Writer);
                }
            }
            PSM_Writer.Close();
            PSM.Close();
        }
        #endregion

        ///////////////////////PRIVATE INTERFACE///////////////////////////////////
        #region SAVE/LOAD ALGORITHMS
        internal void _GenericLoad(ref PSM_Segment withBlock, ref FileStream PSM, ref BinaryReader PSM_Reader, bool Demo)
        {
            withBlock.Key1 = PSM_Reader.ReadUInt32();
            withBlock.Key2 = PSM_Reader.ReadUInt32();
            withBlock.Texture = new Texture();
            withBlock.Texture.Base = 0;
            withBlock.Texture.Offset = (uint)PSM.Position;
            PSM.Position += 1;
            withBlock.Texture.Size = (uint)PSM_Reader.ReadUInt16() * 256 + 228;
            withBlock.Texture.Load(ref PSM, ref PSM_Reader);
            withBlock.SomeInt1 = PSM_Reader.ReadUInt32();
            withBlock.SomeInt2 = PSM_Reader.ReadUInt32();
            withBlock.SomeInt3 = PSM_Reader.ReadUInt32();
            withBlock.Name = PSM_Reader.ReadChars((int)PSM_Reader.ReadUInt32()).ToString();
            if (!Demo)
                withBlock.Ending = PSM_Reader.ReadBytes(98);
            else
                withBlock.Ending = PSM_Reader.ReadBytes(96);
        }
        internal void _GenericSave(ref PSM_Segment withBlock, ref FileStream PSM, ref BinaryWriter PSM_Writer)
        {
            PSM_Writer.Write(withBlock.Key1);
            PSM_Writer.Write(withBlock.Key2);
            PSM_Writer.Write(withBlock.Texture.ByteStream.ToArray());
            PSM_Writer.Write(withBlock.SomeInt1);
            PSM_Writer.Write(withBlock.SomeInt2);
            PSM_Writer.Write(withBlock.SomeInt3);
            PSM_Writer.Write(Convert.ToUInt32(withBlock.Name.Length));
            for (int j = 0; j <= withBlock.Name.Length - 1; j++)
                PSM_Writer.Write(Convert.ToChar(withBlock.Name[j]));
            PSM_Writer.Write(withBlock.Ending);
        }
        #endregion
    }
}
