using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public enum BoundModes
    {
        UpdateModeStatic = 0,
        UpdateModeDynamic,
        UpdateModeIgnore,
    }

    //[XOM_TypeName("XGroup")]
    public class XGroup : Container
    {
        public VInt MatrixID = new VInt();
        public List<VInt> Children = new List<VInt>();
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
            MatrixID.Read(reader);
            byte ChildCount = reader.ReadByte();
            for (int i = 0; i < ChildCount; i++)
            {
                VInt child = new VInt();
                child.Read(reader);
                Children.Add(child);
            }
            Bounds.Read(reader);
            boundMode = reader.ReadUInt32();
            Name.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            MatrixID.Write(writer);
            writer.Write((byte)Children.Count);
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Write(writer);
            }
            Bounds.Write(writer);
            writer.Write(boundMode);
            Name.Write(writer);
        }
    }
}
