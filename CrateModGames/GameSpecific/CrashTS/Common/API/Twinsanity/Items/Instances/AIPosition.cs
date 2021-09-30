using System.IO;

namespace Twinsanity
{
    public class AIPosition : TwinsItem
    {
        public Pos Pos { get; set; }
        public ushort Num { get; set; }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Pos.X);
            writer.Write(Pos.Y);
            writer.Write(Pos.Z);
            writer.Write(Pos.W);
            writer.Write(Num);
        }

        public override void Load(BinaryReader reader, int size)
        {
            Pos = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Num = reader.ReadUInt16();
        }

        protected override int GetSize()
        {
            return 18;
        }
    }
}
