using System;

namespace Twinsanity
{
    public class Terrain : BaseItem
    {
        public new string NodeName = "Terrain";
        public uint Header;
        public uint Num;
        public byte NUL;
        public uint[] K;
        public uint[] IDS = new uint[4];

        public Terrain()
        {
            Header = 4098;
            NUL = 0;
            Num = 0;
            Array.Resize(ref K, (int)Num);
        }

        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(Header);
            NSWriter.Write(Num);
            NSWriter.Write(NUL);
            for (int i = 0; i <= Num - 1; i++)
                NSWriter.Write(K[i]);
            for (int i = 0; i <= 3; i++)
                NSWriter.Write(IDS[i]);
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            Header = BSReader.ReadUInt32();
            Num = BSReader.ReadUInt32();
            NUL = BSReader.ReadByte();
            Array.Resize(ref K, (int)Num);
            for (int i = 0; i <= Num - 1; i++)
                K[i] = BSReader.ReadUInt32();
            for (int i = 0; i <= 3; i++)
                IDS[i] = BSReader.ReadUInt32();
        }
    }
}
