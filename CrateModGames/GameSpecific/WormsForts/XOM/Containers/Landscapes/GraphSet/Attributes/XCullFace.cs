using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public enum CullModes
    {
        Off = 0,
        Front,
        Back,
        ForceFront,
        ForceBack,
    }

    //[XOM_TypeName("XCullFace")]
    public class XCullFace : Container
    {
        private uint cullMode;

        public CullModes CullMode
        {
            get { return (CullModes)cullMode; }
            set { cullMode = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            cullMode = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(cullMode);
        }
    }
}
