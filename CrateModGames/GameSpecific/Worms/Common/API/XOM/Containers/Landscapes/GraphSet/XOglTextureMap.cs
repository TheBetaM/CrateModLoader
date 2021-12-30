using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public enum BlendModes
    {
        Replace = 0,
        Modulate,
        Decal,
        Blend,
    }
    public enum AddressModes
    {
        Invalid = 0,
        Repeat,
        Mirror,
        Clamp,
        Border,
    }
    public enum FilterModes
    {
        None = 0,
        Nearest,
        Linear,
        Anisotropic,
        Max,
    }

    //[XOM_TypeName("XOglTextureMap")]
    public class XOglTextureMap : Container
    {
        private uint blendMode;
        public Color BlendColor = new Color();
        public VInt Image = new VInt();
        public float MaxAnisotropy;
        public ByteBool Enabled = new ByteBool();
        public byte TexCoordIndex;
        private uint addressModeS;
        private uint addressModeT;
        private uint magFilter;
        private uint minFilter;
        private uint mipFilter;
        public byte UnkByte;

        public BlendModes BlendMode
        {
            get { return (BlendModes)blendMode; }
            set { blendMode = (uint)value; }
        }
        public AddressModes AddressModeS
        {
            get { return (AddressModes)addressModeS; }
            set { addressModeS = (uint)value; }
        }
        public AddressModes AddressModeT
        {
            get { return (AddressModes)addressModeT; }
            set { addressModeT = (uint)value; }
        }
        public FilterModes MagFilter
        {
            get { return (FilterModes)magFilter; }
            set { magFilter = (uint)value; }
        }
        public FilterModes MinFilter
        {
            get { return (FilterModes)minFilter; }
            set { minFilter = (uint)value; }
        }
        public FilterModes MipFilter
        {
            get { return (FilterModes)mipFilter; }
            set { mipFilter = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            blendMode = reader.ReadUInt32();
            BlendColor.Read(reader);
            Image.Read(reader);
            MaxAnisotropy = reader.ReadSingle();
            Enabled.Read(reader);
            TexCoordIndex = reader.ReadByte();
            addressModeS = reader.ReadUInt32();
            addressModeT = reader.ReadUInt32();
            magFilter = reader.ReadUInt32();
            minFilter = reader.ReadUInt32();
            mipFilter = reader.ReadUInt32();
            UnkByte = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(blendMode);
            BlendColor.Write(writer);
            Image.Write(writer);
            writer.Write(MaxAnisotropy);
            Enabled.Write(writer);
            writer.Write(TexCoordIndex);
            writer.Write(addressModeS);
            writer.Write(addressModeT);
            writer.Write(magFilter);
            writer.Write(minFilter);
            writer.Write(mipFilter);
            writer.Write(UnkByte);
        }
    }
}
