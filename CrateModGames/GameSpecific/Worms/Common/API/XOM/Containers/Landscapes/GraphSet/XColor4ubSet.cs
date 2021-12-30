using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("XColor4ubSet")]
    public class XColor4ubSet : Container
    {
        public List<ByteColor> ColorSet = new List<ByteColor>();

        public override void Read(BinaryReader reader)
        {
            VInt Count = new VInt(reader);
            for (int i = 0; i < Count.Value; i++)
            {
                ColorSet.Add(new ByteColor(reader));
            }
        }

        public override void Write(BinaryWriter writer)
        {
            VInt Count = new VInt();
            Count.Value = (uint)ColorSet.Count;
            Count.Write(writer);
            for (int i = 0; i < ColorSet.Count; i++)
            {
                ColorSet[i].Write(writer);
            }
        }
    }
}
