using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    //[XOM_TypeName("XShape")]
    public class XShape : Container
    {
        public uint Unk1;
        public VInt ShaderID = new VInt();
        public VInt TriangleStripSetID = new VInt();
        public byte[] Unk2; //0x5
        public Vector4 Bounds = new Vector4();
        private uint boundMode;
        public VInt Name = new VInt();

        public BoundModes BoundMode
        {
            get { return (BoundModes)boundMode; }
            set { boundMode = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            Unk1 = reader.ReadUInt32();
            ShaderID.Read(reader);
            TriangleStripSetID.Read(reader);
            Unk2 = reader.ReadBytes(0x5);
            Bounds.Read(reader);
            boundMode = reader.ReadUInt32();
            Name.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Unk1);
            ShaderID.Write(writer);
            TriangleStripSetID.Write(writer);
            writer.Write(Unk2);
            Bounds.Write(writer);
            writer.Write(boundMode);
            Name.Write(writer);
        }
    }
}
