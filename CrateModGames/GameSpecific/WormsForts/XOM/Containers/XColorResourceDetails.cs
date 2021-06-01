using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("XColorResourceDetails")]
    public class XColorResourceDetails : Container
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        public VInt NameKey = new VInt();
        public uint Flags;

        public override void Read(BinaryReader reader)
        {
            R = reader.ReadByte();
            G = reader.ReadByte();
            B = reader.ReadByte();
            A = reader.ReadByte();

            NameKey.Read(reader);

            Flags = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(R);
            writer.Write(G);
            writer.Write(B);
            writer.Write(A);

            NameKey.Write(writer);

            writer.Write(Flags);
        }
    }
}
