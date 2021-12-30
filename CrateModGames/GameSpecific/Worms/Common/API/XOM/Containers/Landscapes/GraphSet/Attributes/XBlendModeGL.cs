using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public enum BlendFactors
    {
        Zero = 0,
        One,
        DestColor,
        OneMinusDestColor,
        SrcColor,
        OneMinusSrcColor,
        SrcAlpha,
        OneMinusSrcAlpha,
        DestAlpha,
        OneMinusDestAlpha,
        SrcAlphaSaturate,
        MinusOne,
    }

    //[XOM_TypeName("XBlendModeGL")]
    public class XBlendModeGL : Container
    {
        private uint sourceFactor;
        private uint destFactor;

        public BlendFactors SourceFactor
        {
            get { return (BlendFactors)sourceFactor; }
            set { sourceFactor = (uint)value; }
        }
        public BlendFactors DestFactor
        {
            get { return (BlendFactors)destFactor; }
            set { destFactor = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            sourceFactor = reader.ReadUInt32();
            destFactor = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(sourceFactor);
            writer.Write(destFactor);
        }
    }
}
