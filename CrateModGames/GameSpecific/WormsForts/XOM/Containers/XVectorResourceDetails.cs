using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("XVectorResourceDetails")]
    public class XVectorResourceDetails : NamedContainer
    {
        public override string Name
        {
            get { return ParentFile.Strings[(int)NameKey.Value]; }
            set { ParentFile.Strings[(int)NameKey.Value] = value; }
        }
        public float X;
        public float Y;
        public float Z;
        public VInt NameKey = new VInt();
        public uint Flags;

        public override void Read(BinaryReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();

            NameKey.Read(reader);

            Flags = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);

            NameKey.Write(writer);

            writer.Write(Flags);
        }
    }
}
