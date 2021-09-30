using System.IO;

namespace Twinsanity
{
    public class InstanceTemplate : TwinsItem
    {

        public string Name;
        public ushort ObjectID;
        public uint Properties;
        public float[] Floats;
        public uint[] Ints;
        public uint[] Flags;

        //Unknown ones
        public ushort Bitfield; // contains the same bits as the GameObject's bitfield
        public uint HeaderInt1;
        public uint HeaderInt2;
        public uint HeaderInt3;
        public ushort UnkShort;
        public byte[] UnkFlags; // 6

        public override void Save(BinaryWriter writer)
        {
            writer.Write((uint)Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(ObjectID);
            writer.Write(Bitfield);
            writer.Write(HeaderInt1);
            writer.Write(HeaderInt2);
            writer.Write(HeaderInt3);
            if (HeaderInt1 == 1)
            {
                writer.Write(UnkShort);
            }
            writer.Write(UnkFlags);
            writer.Write(Properties);
            writer.Write((uint)Flags.Length);
            if (Flags.Length > 0)
            {
                for (int i = 0; i < Flags.Length; i++)
                {
                    writer.Write(Flags[i]);
                }
            }
            writer.Write((uint)Floats.Length);
            if (Floats.Length > 0)
            {
                for (int i = 0; i < Floats.Length; i++)
                {
                    writer.Write(Floats[i]);
                }
            }
            writer.Write((uint)Ints.Length);
            if (Ints.Length > 0)
            {
                for (int i = 0; i < Ints.Length; i++)
                {
                    writer.Write(Ints[i]);
                }
            }
        }

        public override void Load(BinaryReader reader, int size)
        {

            long startPos = reader.BaseStream.Position;

            uint len = reader.ReadUInt32();

            Name = new string(reader.ReadChars((int)len));

            ObjectID = reader.ReadUInt16();

            Bitfield = reader.ReadUInt16();

            HeaderInt1 = reader.ReadUInt32();
            HeaderInt2 = reader.ReadUInt32();
            HeaderInt3 = reader.ReadUInt32();

            if (HeaderInt1 == 1)
            {
                UnkShort = reader.ReadUInt16();
            }

            UnkFlags = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                UnkFlags[i] = reader.ReadByte();
            }

            Properties = reader.ReadUInt32();

            uint FlagLen = reader.ReadUInt32();
            Flags = new uint[FlagLen];
            for (int i = 0; i < FlagLen; i++)
            {
                Flags[i] = reader.ReadUInt32();
            }

            uint FloatLen = reader.ReadUInt32();
            Floats = new float[FloatLen];
            for (int i = 0; i < FloatLen; i++)
            {
                Floats[i] = reader.ReadSingle();
            }

            uint IntLen = reader.ReadUInt32();
            Ints = new uint[IntLen];
            for (int i = 0; i < IntLen; i++)
            {
                Ints[i] = reader.ReadUInt32();
            }

        }

        protected override int GetSize()
        {
            int len = Name.Length + 4 + 12;
            if (HeaderInt1 == 1)
            {
                len += 2;
            }
            len += 14;
            len += 4;
            if (Flags.Length > 0)
            {
                len += Flags.Length * 4;
            }
            len += 4;
            if (Floats.Length > 0)
            {
                len += Floats.Length * 4;
            }
            len += 4;
            if (Ints.Length > 0)
            {
                len += Ints.Length * 4;
            }

            return len;
        }
    }
}
