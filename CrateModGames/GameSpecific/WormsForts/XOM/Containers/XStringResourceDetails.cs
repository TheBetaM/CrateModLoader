using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("XStringResourceDetails")]
    public class XStringResourceDetails : NamedContainer
    {
        public override string Name
        {
            get { return ParentFile.Strings[(int)NameKey.Value]; }
            set { ParentFile.Strings[(int)NameKey.Value] = value; }
        }
        public string Value
        {
            get { return ParentFile.Strings[(int)ValueKey.Value]; }
            set { ParentFile.Strings[(int)ValueKey.Value] = value; }
        }
        public VInt NameKey = new VInt();
        public VInt ValueKey = new VInt();
        public uint Flags;

        public override void Read(BinaryReader reader)
        {
            ValueKey = new VInt(reader);
            NameKey = new VInt(reader);
            Flags = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            ValueKey.Write(writer);
            NameKey.Write(writer);
            writer.Write(Flags);
        }
    }
}
