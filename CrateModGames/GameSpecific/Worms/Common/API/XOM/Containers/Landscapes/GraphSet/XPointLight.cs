using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    //[XOM_TypeName("XPointLight")]
    public class XPointLight : Container
    {
        public Vector3 Pos = new Vector3();
        public float Range;
        public float ConstantAttenuation;
        public Vector2 LinearAttenuation = new Vector2();
        public byte[] UnkStuff; //0xE
        public Vector3 QuadraticAttenuation1 = new Vector3();
        public Vector3 QuadraticAttenuation2 = new Vector3();
        public Vector3 QuadraticAttenuation3 = new Vector3();

        public BoundModes BoundMode
        {
            get { return (BoundModes)boundMode; }
            set { boundMode = (uint)value; }
        }

        public Vector4 Bounds = new Vector4();
        private uint boundMode;
        public VInt Name = new VInt();

        public override void Read(BinaryReader reader)
        {
            Pos.Read(reader);
            Range = reader.ReadSingle();
            ConstantAttenuation = reader.ReadSingle();
            LinearAttenuation.Read(reader);
            UnkStuff = reader.ReadBytes(0xE);
            QuadraticAttenuation1.Read(reader);
            QuadraticAttenuation2.Read(reader);
            QuadraticAttenuation3.Read(reader);
            Bounds.Read(reader);
            boundMode = reader.ReadUInt32();
            Name.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            Pos.Write(writer);
            writer.Write(Range);
            writer.Write(ConstantAttenuation);
            LinearAttenuation.Write(writer);
            writer.Write(UnkStuff);
            QuadraticAttenuation1.Write(writer);
            QuadraticAttenuation2.Write(writer);
            QuadraticAttenuation3.Write(writer);
            Bounds.Write(writer);
            writer.Write(boundMode);
            Name.Write(writer);
        }
    }
}
