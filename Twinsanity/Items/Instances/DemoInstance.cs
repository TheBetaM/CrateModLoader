using System;

namespace Twinsanity
{
    public class DemoInstance : Instance
    {
        public new string NodeName = "Instance";

        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(X);
            NSWriter.Write(Y);
            NSWriter.Write(Z);
            NSWriter.Write(W);
            NSWriter.Write(RX);
            NSWriter.Write(COMRX);
            NSWriter.Write(RY);
            NSWriter.Write(COMRY);
            NSWriter.Write(RZ);
            NSWriter.Write(COMRZ);
            NSWriter.Write(Size1);
            NSWriter.Write(Size1);
            NSWriter.Write(SomeNum1);
            for (int i = 0; i <= Size1 - 1; i++)
                NSWriter.Write(Something1[i]);
            NSWriter.Write(Size2);
            NSWriter.Write(Size2);
            NSWriter.Write(SomeNum2);
            for (int i = 0; i <= Size2 - 1; i++)
                NSWriter.Write(Something2[i]);
            NSWriter.Write(Size3);
            NSWriter.Write(Size3);
            NSWriter.Write(SomeNum3);
            for (int i = 0; i <= Size3 - 1; i++)
                NSWriter.Write(Something3[i]);
            NSWriter.Write(ObjectID);
            NSWriter.Write(AfterOID);
            byte b = (byte)UnkI321Number;
            NSWriter.Write(b);
            b = (byte)UnkI322Number;
            NSWriter.Write(b);
            b = (byte)UnkI323Number;
            NSWriter.Write(b);
            b = 0;
            NSWriter.Write(b);
            NSWriter.Write(UnkI32);
            // NSWriter.Write(UnkI321Number)
            for (int i = 0; i <= UnkI321Number - 1; i++)
                NSWriter.Write(UnkI321[i]);
            // NSWriter.Write(UnkI322Number)
            for (int i = 0; i <= UnkI322Number - 1; i++)
                NSWriter.Write(UnkI322[i]);
            // NSWriter.Write(UnkI323Number)
            for (int i = 0; i <= UnkI323Number - 1; i++)
                NSWriter.Write(UnkI323[i]);
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            X = BSReader.ReadSingle();
            Y = BSReader.ReadSingle();
            Z = BSReader.ReadSingle();
            W = BSReader.ReadSingle();
            RX = BSReader.ReadUInt16();
            COMRX = BSReader.ReadUInt16();
            RY = BSReader.ReadUInt16();
            COMRY = BSReader.ReadUInt16();
            RZ = BSReader.ReadUInt16();
            COMRZ = BSReader.ReadUInt16();
            Size1 = BSReader.ReadInt32();
            Size1 = BSReader.ReadInt32();
            SomeNum1 = BSReader.ReadInt32();
            Array.Resize(ref Something1, Size1);
            for (int i = 0; i <= Size1 - 1; i++)
                Something1[i] = BSReader.ReadUInt16();
            Size2 = BSReader.ReadInt32();
            Size2 = BSReader.ReadInt32();
            SomeNum2 = BSReader.ReadInt32();
            Array.Resize(ref Something2, Size2);
            for (int i = 0; i <= Size2 - 1; i++)
                Something2[i] = BSReader.ReadUInt16();
            Size3 = BSReader.ReadInt32();
            Size3 = BSReader.ReadInt32();
            SomeNum3 = BSReader.ReadInt32();
            Array.Resize(ref Something3, Size3);
            for (int i = 0; i <= Size3 - 1; i++)
                Something3[i] = BSReader.ReadUInt16();
            ObjectID = BSReader.ReadUInt16();
            AfterOID = BSReader.ReadUInt32();
            ParametersHeader = BSReader.ReadUInt32();
            ByteStream.Position -= 4;
            UnkI321Number = BSReader.ReadByte();
            UnkI322Number = BSReader.ReadByte();
            UnkI323Number = BSReader.ReadByte();
            BSReader.ReadByte();
            UnkI32 = BSReader.ReadUInt32();
            Array.Resize(ref UnkI321, UnkI321Number);
            for (int i = 0; i <= UnkI321Number - 1; i++)
                UnkI321[i] = BSReader.ReadUInt32();
            Array.Resize(ref UnkI322, UnkI322Number);
            for (int i = 0; i <= UnkI322Number - 1; i++)
                UnkI322[i] = BSReader.ReadSingle();
            Array.Resize(ref UnkI323, UnkI323Number);
            for (int i = 0; i <= UnkI323Number - 1; i++)
                UnkI323[i] = BSReader.ReadUInt32();
        }
    }
}
