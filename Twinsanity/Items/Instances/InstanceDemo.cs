using System.Collections.Generic;
using System.IO;

namespace Twinsanity
{
    public class InstanceDemo : TwinsItem
    {
        public Pos Pos { get; set; }
        public ushort RotX { get; set; }
        public ushort RotY { get; set; }
        public ushort RotZ { get; set; }
        public ushort COMRotX { get; set; }
        public ushort COMRotY { get; set; }
        public ushort COMRotZ { get; set; }
        public List<ushort> InstanceIDs { get; set; } = new List<ushort>();
        public List<ushort> PositionIDs { get; set; } = new List<ushort>();
        public List<ushort> PathIDs { get; set; } = new List<ushort>();
        public int SomeNum1 { get; set; }
        public int SomeNum2 { get; set; }
        public int SomeNum3 { get; set; }
        public ushort ObjectID { get; set; }
        public uint AfterOID { get; set; }
        //public uint PHeader { get; set; }
        public uint Flags { get; set; }
        public List<uint> UnkI321 { get; set; } = new List<uint>();
        public List<float> UnkI322 { get; set; } = new List<float>();
        public List<uint> UnkI323 { get; set; } = new List<uint>();

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Pos.X);
            writer.Write(Pos.Y);
            writer.Write(Pos.Z);
            writer.Write(Pos.W);
            writer.Write(RotX);
            writer.Write(COMRotX);
            writer.Write(RotY);
            writer.Write(COMRotY);
            writer.Write(RotZ);
            writer.Write(COMRotZ);
            writer.Write(InstanceIDs.Count);
            writer.Write(InstanceIDs.Count);
            writer.Write(SomeNum1);
            for (int i = 0; i < InstanceIDs.Count; ++i)
                writer.Write(InstanceIDs[i]);
            writer.Write(PositionIDs.Count);
            writer.Write(PositionIDs.Count);
            writer.Write(SomeNum2);
            for (int i = 0; i < PositionIDs.Count; ++i)
                writer.Write(PositionIDs[i]);
            writer.Write(PathIDs.Count);
            writer.Write(PathIDs.Count);
            writer.Write(SomeNum3);
            for (int i = 0; i < PathIDs.Count; ++i)
                writer.Write(PathIDs[i]);
            writer.Write(ObjectID);
            writer.Write(AfterOID);
            //PHeader = (uint)((byte)UnkI321.Count
            //    | (UnkI322.Count << 8)
            //    | (UnkI323.Count << 16));
            //writer.Write(PHeader);

            writer.Write((byte)UnkI321.Count);
            writer.Write((byte)UnkI322.Count);
            writer.Write((byte)UnkI323.Count);
            writer.Write((byte)0);

            writer.Write(Flags);
            
            for (int i = 0; i < UnkI321.Count; ++i)
                writer.Write(UnkI321[i]);
            for (int i = 0; i < UnkI322.Count; ++i)
                writer.Write(UnkI322[i]);
            for (int i = 0; i < UnkI323.Count; ++i)
                writer.Write(UnkI323[i]);
        }

        public override void Load(BinaryReader reader, int size)
        {
            Pos = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            RotX = reader.ReadUInt16();
            COMRotX = reader.ReadUInt16();
            RotY = reader.ReadUInt16();
            COMRotY = reader.ReadUInt16();
            RotZ = reader.ReadUInt16();
            COMRotZ = reader.ReadUInt16();

            var n = reader.ReadInt32();
            n = reader.ReadInt32();
            SomeNum1 = reader.ReadInt32();
            InstanceIDs.Clear();
            for (int i = 0; i < n; ++i)
                InstanceIDs.Add(reader.ReadUInt16());
            n = reader.ReadInt32();
            n = reader.ReadInt32();
            SomeNum2 = reader.ReadInt32();
            PositionIDs.Clear();
            for (int i = 0; i < n; ++i)
                PositionIDs.Add(reader.ReadUInt16());
            n = reader.ReadInt32();
            n = reader.ReadInt32();
            SomeNum3 = reader.ReadInt32();
            PathIDs.Clear();
            for (int i = 0; i < n; ++i)
                PathIDs.Add(reader.ReadUInt16());
            ObjectID = reader.ReadUInt16();
            AfterOID = reader.ReadUInt32();
            //PHeader = reader.ReadUInt32();

            //reader.BaseStream.Position -= 4;
            var Count_Flags = reader.ReadByte();
            var Count_Floats = reader.ReadByte();
            var Count_Ints = reader.ReadByte();
            reader.ReadByte();

            Flags = reader.ReadUInt32();
            UnkI321.Clear();
            for (int i = 0; i < Count_Flags; ++i)
                UnkI321.Add(reader.ReadUInt32());
            UnkI322.Clear();
            for (int i = 0; i < Count_Floats; ++i)
                UnkI322.Add(reader.ReadSingle());
            UnkI323.Clear();
            for (int i = 0; i < Count_Ints; ++i)
                UnkI323.Add(reader.ReadUInt32());
        }

        protected override int GetSize()
        {
            return 78 + (InstanceIDs.Count + PositionIDs.Count + PathIDs.Count) * 2 + (UnkI321.Count + UnkI322.Count + UnkI323.Count) * 4;
        }
    }
}
