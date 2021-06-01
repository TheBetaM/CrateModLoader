using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    //[XOM_TypeName("XZBufferWriteEnable")]
    public class XZBufferWriteEnable : Container
    {
        public ByteBool Enabled = new ByteBool();

        public override void Read(BinaryReader reader)
        {
            Enabled.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            Enabled.Write(writer);
        }
    }
}
