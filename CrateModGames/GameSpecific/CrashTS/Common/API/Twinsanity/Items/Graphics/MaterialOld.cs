using System;

namespace Twinsanity
{
    public class Material : BaseItem
    {
        public new string NodeName = "Material";
        public string Name;
        public uint Texture;
        public uint LastNum;


        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream(ByteStream.ToArray());
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            if (NewStream.Length >= 8)
            {
                NewStream.Position = NewStream.Length - 8;
                NSWriter.Write(Texture);
                NSWriter.Write(LastNum);
            }
            else
                NSWriter.Write(ByteStream.ToArray());
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 12;
            int Len = BSReader.ReadInt32();
            Name = new string(BSReader.ReadChars(Len - 1));
            ByteStream.Position = ByteStream.Length - 8;
            Texture = BSReader.ReadUInt32();
            LastNum = BSReader.ReadUInt32();
        }
    }
}
