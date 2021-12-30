using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public enum RotateOrders
    {
        XYZ = 0,
        YZX,
        ZXY,
        XZY,
        YXZ,
        ZYX,
    }

    //[XOM_TypeName("XTransform")]
    public class XTransform : Container
    {
        public Vector3 Pos = new Vector3();
        public Vector3 Rot = new Vector3();
        public Vector3 Scale = new Vector3();
        private uint rotateOrder;
        public Matrix Mat = new Matrix();

        public RotateOrders RotateOrder
        {
            get { return (RotateOrders)rotateOrder; }
            set { rotateOrder = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            Pos.Read(reader);
            Rot.Read(reader);
            Scale.Read(reader);
            rotateOrder = reader.ReadUInt32();
            Mat.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            Pos.Write(writer);
            Rot.Write(writer);
            Scale.Write(writer);
            writer.Write(rotateOrder);
            Mat.Write(writer);
        }
    }
}
