﻿using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("XIntResourceDetails")]
    public class XIntResourceDetails : NamedContainer
    {
        public override string Name
        {
            get { return ParentFile.Strings[(int)NameKey.Value]; }
            set { ParentFile.Strings[(int)NameKey.Value] = value; }
        }
        public int Value;
        public VInt NameKey = new VInt();
        public uint Flags;

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadInt32();

            NameKey.Read(reader);

            Flags = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);

            NameKey.Write(writer);

            writer.Write(Flags);
        }
    }
}
