using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    //[XOM_TypeName("XMatrix")]
    public class XMatrix : Container
    {
        public Matrix Mat = new Matrix();

        public override void Read(BinaryReader reader)
        {
            Mat.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            Mat.Write(writer);
        }
    }
}
