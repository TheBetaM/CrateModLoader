using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public enum NormalizeMode
    {
        Never = 0,
        Always,
        Required,
    }

    //[XOM_TypeName("XLightingEnable")]
    public class XLightingEnable : Container
    {
        public ByteBool Enabled = new ByteBool();
        public ByteBool TwoSided = new ByteBool();
        public ByteBool LocalViewer = new ByteBool();
        private uint normalize;
        public Color AmbientColor = new Color();

        public NormalizeMode Normalize
        {
            get { return (NormalizeMode)normalize; }
            set { normalize = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            Enabled.Read(reader);
            TwoSided.Read(reader);
            LocalViewer.Read(reader);
            normalize = reader.ReadUInt32();
            AmbientColor.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            Enabled.Write(writer);
            TwoSided.Write(writer);
            LocalViewer.Write(writer);
            writer.Write(normalize);
            AmbientColor.Write(writer);
        }
    }
}
