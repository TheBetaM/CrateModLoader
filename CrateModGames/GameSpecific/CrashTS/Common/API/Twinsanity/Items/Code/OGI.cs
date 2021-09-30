using System;

namespace Twinsanity
{
    public class OGI : BaseItem
    {
        public new string NodeName = "OGI";

        public byte T1Number, T2Number, Number, Flag1, Flag2, GCNumber, Flag3, Flag4;
        public uint UnkI321, UnkI322;
        public RM2.Coordinate4 Coordinates1, Coordinates2;
        public StructureType1[] T1;
        public StructureType2[] T2;
        public GCInfo[] GCI;
        public StructureType3[] T3;
        public uint SomeInt321, SomeInt322;
        public ushort[] UInt16 = new ushort[13];
        public RM2.Coordinate4[] SomeCoordinates = new RM2.Coordinate4[20];
        public int SomeUI16Number = 30;
        public ushort[] SomeInt16;
        public byte EndByte = 255;
        public byte[] UnexploredData = new byte[] { 0 };

        public OGI()
        {
            SomeInt16 = new ushort[SomeUI16Number - 1 + 1];
        }

        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(T1Number);
            NSWriter.Write(T2Number);
            NSWriter.Write(Number);
            NSWriter.Write(Flag1);
            NSWriter.Write(Flag2);
            NSWriter.Write(GCNumber);
            NSWriter.Write(Flag3);
            NSWriter.Write(Flag4);
            NSWriter.Write(UnkI321);
            NSWriter.Write(UnkI322);
            NSWriter.Write(Coordinates1.X);
            NSWriter.Write(Coordinates1.Y);
            NSWriter.Write(Coordinates1.Z);
            NSWriter.Write(Coordinates1.W);
            NSWriter.Write(Coordinates2.X);
            NSWriter.Write(Coordinates2.Y);
            NSWriter.Write(Coordinates2.Z);
            NSWriter.Write(Coordinates2.W);
            for (int i = 0; i <= T1Number - 1; i++)
            {
                NSWriter.Write(T1[i].Num1);
                NSWriter.Write(T1[i].Num2);
                NSWriter.Write(T1[i].Num3);
                NSWriter.Write(T1[i].Num4);
                NSWriter.Write(T1[i].Num5);
                NSWriter.Write(T1[i].Coordinate1.X);
                NSWriter.Write(T1[i].Coordinate1.Y);
                NSWriter.Write(T1[i].Coordinate1.Z);
                NSWriter.Write(T1[i].Coordinate1.W);
                NSWriter.Write(T1[i].Coordinate2.X);
                NSWriter.Write(T1[i].Coordinate2.Y);
                NSWriter.Write(T1[i].Coordinate2.Z);
                NSWriter.Write(T1[i].Coordinate2.W);
                NSWriter.Write(T1[i].Coordinate3.X);
                NSWriter.Write(T1[i].Coordinate3.Y);
                NSWriter.Write(T1[i].Coordinate3.Z);
                NSWriter.Write(T1[i].Coordinate3.W);
                NSWriter.Write(T1[i].Coordinate4.X);
                NSWriter.Write(T1[i].Coordinate4.Y);
                NSWriter.Write(T1[i].Coordinate4.Z);
                NSWriter.Write(T1[i].Coordinate4.W);
                NSWriter.Write(T1[i].Coordinate5.X);
                NSWriter.Write(T1[i].Coordinate5.Y);
                NSWriter.Write(T1[i].Coordinate5.Z);
                NSWriter.Write(T1[i].Coordinate5.W);
            }
            for (int i = 0; i <= T2Number - 1; i++)
            {
                NSWriter.Write(T2[i].Num1);
                NSWriter.Write(T2[i].Num2);
                NSWriter.Write(T2[i].Coordinate1.X);
                NSWriter.Write(T2[i].Coordinate1.Y);
                NSWriter.Write(T2[i].Coordinate1.Z);
                NSWriter.Write(T2[i].Coordinate1.W);
                NSWriter.Write(T2[i].Coordinate2.X);
                NSWriter.Write(T2[i].Coordinate2.Y);
                NSWriter.Write(T2[i].Coordinate2.Z);
                NSWriter.Write(T2[i].Coordinate2.W);
                NSWriter.Write(T2[i].Coordinate3.X);
                NSWriter.Write(T2[i].Coordinate3.Y);
                NSWriter.Write(T2[i].Coordinate3.Z);
                NSWriter.Write(T2[i].Coordinate3.W);
                NSWriter.Write(T2[i].Coordinate4.X);
                NSWriter.Write(T2[i].Coordinate4.Y);
                NSWriter.Write(T2[i].Coordinate4.Z);
                NSWriter.Write(T2[i].Coordinate4.W);
            }
            for (int i = 0; i <= GCNumber - 1; i++)
                NSWriter.Write(GCI[i].ID);
            for (int i = 0; i <= GCNumber - 1; i++)
                NSWriter.Write(GCI[i].GCID);
            for (int i = 0; i <= T1Number - 1; i++)
            {
                NSWriter.Write(T3[i].Coordinate1.X);
                NSWriter.Write(T3[i].Coordinate1.Y);
                NSWriter.Write(T3[i].Coordinate1.Z);
                NSWriter.Write(T3[i].Coordinate1.W);
                NSWriter.Write(T3[i].Coordinate2.X);
                NSWriter.Write(T3[i].Coordinate2.Y);
                NSWriter.Write(T3[i].Coordinate2.Z);
                NSWriter.Write(T3[i].Coordinate2.W);
                NSWriter.Write(T3[i].Coordinate3.X);
                NSWriter.Write(T3[i].Coordinate3.Y);
                NSWriter.Write(T3[i].Coordinate3.Z);
                NSWriter.Write(T3[i].Coordinate3.W);
                NSWriter.Write(T3[i].Coordinate4.X);
                NSWriter.Write(T3[i].Coordinate4.Y);
                NSWriter.Write(T3[i].Coordinate4.Z);
                NSWriter.Write(T3[i].Coordinate4.W);
            }
            NSWriter.Write(SomeInt321);
            NSWriter.Write(SomeInt322);
            for (int i = 0; i <= 12; i++)
                NSWriter.Write(UInt16[i]);
            NSWriter.Write(UnexploredData);
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            T1Number = BSReader.ReadByte();
            T2Number = BSReader.ReadByte();
            Number = BSReader.ReadByte();
            Flag1 = BSReader.ReadByte();
            Flag2 = BSReader.ReadByte();
            GCNumber = BSReader.ReadByte();
            Flag3 = BSReader.ReadByte(); // Has ID4
            Flag4 = BSReader.ReadByte(); // Has ID5
            UnkI321 = BSReader.ReadUInt32();
            UnkI322 = BSReader.ReadUInt32();
            Array.Resize(ref T1, T1Number);
            Array.Resize(ref T2, T2Number);
            Array.Resize(ref GCI, GCNumber);
            Array.Resize(ref T3, T1Number);
            Coordinates1.X = BSReader.ReadSingle();
            Coordinates1.Y = BSReader.ReadSingle();
            Coordinates1.Z = BSReader.ReadSingle();
            Coordinates1.W = BSReader.ReadSingle();
            Coordinates2.X = BSReader.ReadSingle();
            Coordinates2.Y = BSReader.ReadSingle();
            Coordinates2.Z = BSReader.ReadSingle();
            Coordinates2.W = BSReader.ReadSingle();
            for (int i = 0; i <= T1Number - 1; i++)
            {
                T1[i].Num1 = BSReader.ReadUInt32();
                T1[i].Num2 = BSReader.ReadUInt32();
                T1[i].Num3 = BSReader.ReadUInt32();
                T1[i].Num4 = BSReader.ReadUInt32();
                T1[i].Num5 = BSReader.ReadUInt32();
                T1[i].Coordinate1.X = BSReader.ReadSingle();
                T1[i].Coordinate1.Y = BSReader.ReadSingle();
                T1[i].Coordinate1.Z = BSReader.ReadSingle();
                T1[i].Coordinate1.W = BSReader.ReadSingle();
                T1[i].Coordinate2.X = BSReader.ReadSingle();
                T1[i].Coordinate2.Y = BSReader.ReadSingle();
                T1[i].Coordinate2.Z = BSReader.ReadSingle();
                T1[i].Coordinate2.W = BSReader.ReadSingle();
                T1[i].Coordinate3.X = BSReader.ReadSingle();
                T1[i].Coordinate3.Y = BSReader.ReadSingle();
                T1[i].Coordinate3.Z = BSReader.ReadSingle();
                T1[i].Coordinate3.W = BSReader.ReadSingle();
                T1[i].Coordinate4.X = BSReader.ReadSingle();
                T1[i].Coordinate4.Y = BSReader.ReadSingle();
                T1[i].Coordinate4.Z = BSReader.ReadSingle();
                T1[i].Coordinate4.W = BSReader.ReadSingle();
                T1[i].Coordinate5.X = BSReader.ReadSingle();
                T1[i].Coordinate5.Y = BSReader.ReadSingle();
                T1[i].Coordinate5.Z = BSReader.ReadSingle();
                T1[i].Coordinate5.W = BSReader.ReadSingle();
            }
            for (int i = 0; i <= T2Number - 1; i++)
            {
                T2[i].Num1 = BSReader.ReadUInt32();
                T2[i].Num2 = BSReader.ReadUInt32();
                T2[i].Coordinate1.X = BSReader.ReadSingle();
                T2[i].Coordinate1.Y = BSReader.ReadSingle();
                T2[i].Coordinate1.Z = BSReader.ReadSingle();
                T2[i].Coordinate1.W = BSReader.ReadSingle();
                T2[i].Coordinate2.X = BSReader.ReadSingle();
                T2[i].Coordinate2.Y = BSReader.ReadSingle();
                T2[i].Coordinate2.Z = BSReader.ReadSingle();
                T2[i].Coordinate2.W = BSReader.ReadSingle();
                T2[i].Coordinate3.X = BSReader.ReadSingle();
                T2[i].Coordinate3.Y = BSReader.ReadSingle();
                T2[i].Coordinate3.Z = BSReader.ReadSingle();
                T2[i].Coordinate3.W = BSReader.ReadSingle();
                T2[i].Coordinate4.X = BSReader.ReadSingle();
                T2[i].Coordinate4.Y = BSReader.ReadSingle();
                T2[i].Coordinate4.Z = BSReader.ReadSingle();
                T2[i].Coordinate4.W = BSReader.ReadSingle();
            }
            for (int i = 0; i <= GCNumber - 1; i++)
                GCI[i].ID = BSReader.ReadByte();
            for (int i = 0; i <= GCNumber - 1; i++)
                GCI[i].GCID = BSReader.ReadUInt32();
            for (int i = 0; i <= T1Number - 1; i++)
            {
                T3[i].Coordinate1.X = BSReader.ReadSingle();
                T3[i].Coordinate1.Y = BSReader.ReadSingle();
                T3[i].Coordinate1.Z = BSReader.ReadSingle();
                T3[i].Coordinate1.W = BSReader.ReadSingle();
                T3[i].Coordinate2.X = BSReader.ReadSingle();
                T3[i].Coordinate2.Y = BSReader.ReadSingle();
                T3[i].Coordinate2.Z = BSReader.ReadSingle();
                T3[i].Coordinate2.W = BSReader.ReadSingle();
                T3[i].Coordinate3.X = BSReader.ReadSingle();
                T3[i].Coordinate3.Y = BSReader.ReadSingle();
                T3[i].Coordinate3.Z = BSReader.ReadSingle();
                T3[i].Coordinate3.W = BSReader.ReadSingle();
                T3[i].Coordinate4.X = BSReader.ReadSingle();
                T3[i].Coordinate4.Y = BSReader.ReadSingle();
                T3[i].Coordinate4.Z = BSReader.ReadSingle();
                T3[i].Coordinate4.W = BSReader.ReadSingle();
            }
            SomeInt321 = BSReader.ReadUInt32();
            SomeInt322 = BSReader.ReadUInt32();
            for (int i = 0; i <= 12; i++)
                UInt16[i] = BSReader.ReadUInt16();
            UnexploredData = BSReader.ReadBytes((int)(ByteStream.Length - ByteStream.Position));
        }

        #region STRUCTURES
        public struct StructureType1
        {
            public uint Num1, Num2, Num3, Num4, Num5;
            public RM2.Coordinate4 Coordinate1, Coordinate2, Coordinate3, Coordinate4, Coordinate5;
        }
        public struct StructureType2
        {
            public uint Num1, Num2;
            public RM2.Coordinate4 Coordinate1, Coordinate2, Coordinate3, Coordinate4;
        }
        public struct GCInfo
        {
            public byte ID;
            public uint GCID;
        }
        public struct StructureType3
        {
            public RM2.Coordinate4 Coordinate1, Coordinate2, Coordinate3, Coordinate4;
        }
        #endregion
    }
}
