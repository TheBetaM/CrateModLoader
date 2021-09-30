using System;

namespace Twinsanity
{
    public class SurfaceBehaviour : BaseItem
    {
        public new string NodeName = "Surface Behaviour";
        public byte[] Flag = new byte[4];
        public ushort[] IDs = new ushort[11];
        public RM2.Coordinate4[] Pos = new RM2.Coordinate4[4];
        public ushort[] EndIn16 = new ushort[12];

        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(Flag);
            for (int i = 0; i <= 10; i++)
                NSWriter.Write(IDs[i]);
            for (int i = 0; i <= 3; i++)
            {
                NSWriter.Write(Pos[i].X);
                NSWriter.Write(Pos[i].Y);
                NSWriter.Write(Pos[i].Z);
                NSWriter.Write(Pos[i].W);
            }
            for (int i = 0; i <= 11; i++)
                NSWriter.Write(EndIn16[i]);
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            Flag = BSReader.ReadBytes(4);
            for (int i = 0; i <= 10; i++)
                IDs[i] = BSReader.ReadUInt16();
            for (int i = 0; i <= 3; i++)
            {
                Pos[i].X = BSReader.ReadSingle();
                Pos[i].Y = BSReader.ReadSingle();
                Pos[i].Z = BSReader.ReadSingle();
                Pos[i].W = BSReader.ReadSingle();
            }
            for (int i = 0; i <= 11; i++)
                EndIn16[i] = BSReader.ReadUInt16();
        }
    }
}
