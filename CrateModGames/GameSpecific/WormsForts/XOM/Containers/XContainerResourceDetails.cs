using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("XContainerResourceDetails")]
    public class XContainerResourceDetails : Container
    {
        public VInt ContainerID = new VInt();
        public VInt NameKey = new VInt();
        public uint Flags;

        public override void Read(BinaryReader reader)
        {
            ContainerID.Read(reader);
            NameKey.Read(reader);

            Flags = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            ContainerID.Write(writer);
            NameKey.Write(writer);

            writer.Write(Flags);
        }
    }
}
