using System.IO;

namespace Twinsanity
{
    public class AIPath : TwinsItem
    {
        public ushort[] Arg { get; set; } = new ushort[5];

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Arg[0]);
            writer.Write(Arg[1]);
            writer.Write(Arg[2]);
            writer.Write(Arg[3]);
            writer.Write(Arg[4]);
        }

        public override void Load(BinaryReader reader, int size)
        {
            Arg[0] = reader.ReadUInt16();
            Arg[1] = reader.ReadUInt16();
            Arg[2] = reader.ReadUInt16();
            Arg[3] = reader.ReadUInt16();
            Arg[4] = reader.ReadUInt16();
        }

        protected override int GetSize()
        {
            return 10;
        }
    }
}
