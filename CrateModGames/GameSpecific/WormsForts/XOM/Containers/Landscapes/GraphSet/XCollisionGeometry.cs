using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    //[XOM_TypeName("XCollisionGeometry")]
    public class XCollisionGeometry : Container
    {
        public float Restitution;
        public float Friction;
        public float Mass;
        public List<ushort> Indices = new List<ushort>();
        public List<float> Vertices = new List<float>();
        public Vector4 Bounds = new Vector4();
        private uint boundMode;
        public VInt Name = new VInt();

        public BoundModes BoundMode
        {
            get { return (BoundModes)boundMode; }
            set { boundMode = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            Restitution = reader.ReadSingle();
            Friction = reader.ReadSingle();
            Mass = reader.ReadSingle();
            byte IndCount = reader.ReadByte();
            for (int i = 0; i < IndCount; i++)
            {
                Indices.Add(reader.ReadUInt16());
            }
            byte VertCount = reader.ReadByte();
            for (int i = 0; i < VertCount; i++)
            {
                Vertices.Add(reader.ReadSingle());
            }
            Bounds.Read(reader);
            boundMode = reader.ReadUInt32();
            Name.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Restitution);
            writer.Write(Friction);
            writer.Write(Mass);

            writer.Write((byte)Indices.Count);
            for (int i = 0; i < Indices.Count; i++)
            {
                writer.Write(Indices[i]);
            }
            writer.Write((byte)Vertices.Count);
            for (int i = 0; i < Vertices.Count; i++)
            {
                writer.Write(Vertices[i]);
            }

            Bounds.Write(writer);
            writer.Write(boundMode);
            Name.Write(writer);
        }
    }
}
