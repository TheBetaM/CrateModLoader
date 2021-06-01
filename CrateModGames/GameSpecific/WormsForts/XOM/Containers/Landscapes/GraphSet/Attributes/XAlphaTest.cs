using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public enum CompareFunctions
    {
        Never = 0,
        Less,
        Equal,
        LessEqual,
        Greater,
        NotEqual,
        GreaterEqual,
        Always,
    }

    //[XOM_TypeName("XAlphaTest")]
    public class XAlphaTest : Container
    {
        public ByteBool Enabled = new ByteBool();
        private uint compareFunction;
        public float RefValue;

        public CompareFunctions CompareFunction
        {
            get { return (CompareFunctions)compareFunction; }
            set { compareFunction = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            Enabled.Read(reader);
            compareFunction = reader.ReadUInt32();
            RefValue = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            Enabled.Write(writer);
            writer.Write(compareFunction);
            writer.Write(RefValue);
        }
    }
}
