using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    //[XOM_TypeName("XTriangleStripSet")]
    public class XTriangleStripSet : Container
    {
        public byte UnkByte1;
        public uint UnkInt1;
        public byte UnkByte2;
        public uint UnkInt2;
        public VInt Coord3sSet_4fScale = new VInt();
        public VInt Normal3fSet = new VInt();
        public VInt Color4ubSet = new VInt();
        public VInt TexCoord2fSet = new VInt();
        public byte UnkByte3;
        public Vector3 UnkVector1 = new Vector3();
        public Vector3 UnkVector2 = new Vector3();
        public uint UnkInt3;

        public override void Read(BinaryReader reader)
        {
            UnkByte1 = reader.ReadByte();
            UnkInt1 = reader.ReadUInt32();
            UnkByte2 = reader.ReadByte();
            UnkInt2 = reader.ReadUInt32();
            Coord3sSet_4fScale.Read(reader);
            Normal3fSet.Read(reader);
            Color4ubSet.Read(reader);
            TexCoord2fSet.Read(reader);
            UnkByte3 = reader.ReadByte();
            UnkVector1.Read(reader);
            UnkVector2.Read(reader);
            UnkInt3 = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(UnkByte1);
            writer.Write(UnkInt1);
            writer.Write(UnkByte2);
            writer.Write(UnkInt2);
            Coord3sSet_4fScale.Write(writer);
            Normal3fSet.Write(writer);
            TexCoord2fSet.Write(writer);
            writer.Write(UnkByte3);
            UnkVector1.Write(writer);
            UnkVector2.Write(writer);
            writer.Write(UnkInt3);
        }
    }
}
