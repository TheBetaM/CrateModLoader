using System;

namespace Twinsanity
{
    public class GC : BaseItem
    {
        public new string NodeName = "GC";
        public uint Header;
        public int MaterialNumber;
        public uint[] Material;
        public uint Model;

        
        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(Header);
            NSWriter.Write(MaterialNumber);
            for (int i = 0; i <= MaterialNumber - 1; i++)
                NSWriter.Write(Material[i]);
            NSWriter.Write(Model);
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            Header = BSReader.ReadUInt32();
            MaterialNumber = BSReader.ReadInt32();
            Material = new uint[MaterialNumber];
            for (int i = 0; i <= MaterialNumber - 1; i++)
                Material[i] = BSReader.ReadUInt32();
            Model = BSReader.ReadUInt32();
        }
    }
}
