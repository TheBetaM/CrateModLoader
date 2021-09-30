using System.IO;

namespace Twinsanity
{
    public class SkinX : TwinsItem
    {

        public long ItemSize { get; set; }

        public uint SubModels { get; set; }
        public uint[] MaterialIDs { get; set; }
        public uint[] Vertexes { get; set; }
        public uint[] BlockSize { get; set; }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Data);
        }

        public override void Load(BinaryReader reader, int size)
        {
            long pre_pos = reader.BaseStream.Position;

            SubModels = reader.ReadUInt32();

            MaterialIDs = new uint[SubModels];
            Vertexes = new uint[SubModels];
            BlockSize = new uint[SubModels];
            for (uint i = 0; i < SubModels; i++)
            {
                MaterialIDs[i] = reader.ReadUInt32();
                BlockSize[i] = reader.ReadUInt32();
                Vertexes[i] = reader.ReadUInt32();
                while (reader.ReadUInt32() < 65535)
                {
                    // some array of small numbers
                }
                reader.BaseStream.Position = reader.BaseStream.Position + BlockSize[i] - 4;
            }

            ItemSize = size;
            reader.BaseStream.Position = pre_pos;
            Data = reader.ReadBytes(size);
        }

        protected override int GetSize()
        {
            return (int)ItemSize;
        }
    }
}
