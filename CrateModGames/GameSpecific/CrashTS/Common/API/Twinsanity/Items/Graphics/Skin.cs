using System.IO;

namespace Twinsanity
{
    public class Skin : TwinsItem
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
            for (int i = 0; i < SubModels; i++)
            {
                MaterialIDs[i] = reader.ReadUInt32();
                BlockSize[i] = reader.ReadUInt32();
                Vertexes[i] = reader.ReadUInt32();
                reader.BaseStream.Position = reader.BaseStream.Position + BlockSize[i];
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
