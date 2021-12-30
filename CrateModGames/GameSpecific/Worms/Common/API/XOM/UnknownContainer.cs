using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public class UnknownContainer : Container
    {
        public byte[] Data;

        public override void Read(BinaryReader reader)
        {
            Data = reader.ReadBytes((int)Size);
        }

        public override void Write(BinaryWriter writer)
        {
            if (Data != null && Data.Length > 0)
            {
                writer.Write(Data);
            }
        }
    }
}
