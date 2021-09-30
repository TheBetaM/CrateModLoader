using System.Collections.Generic;
using System.IO;

namespace Twinsanity
{
    public class InstanceTemplateDemo : TwinsItem
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
        public byte[] UnkFlags; // 2

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Name.Length);
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

            writer.Write((byte)Flags.Length);
            writer.Write((byte)Floats.Length);
            writer.Write((byte)Ints.Length);
            writer.Write((byte)0);
            writer.Write(Properties);
            
            if (Flags.Length > 0)
            {
                for (int i = 0; i < Flags.Length; i++)
                {
                    writer.Write(Flags[i]);
                }
            }
            if (Floats.Length > 0)
            {
                for (int i = 0; i < Floats.Length; i++)
                {
                    writer.Write(Floats[i]);
                }
            }
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

            UnkFlags = new byte[2];
            for (int i = 0; i < 2; i++)
            {
                UnkFlags[i] = reader.ReadByte();
            }

            uint FlagLen = reader.ReadByte();
            uint FloatLen = reader.ReadByte();
            uint IntLen = reader.ReadByte();
            reader.ReadByte();

            Properties = reader.ReadUInt32();

            Flags = new uint[FlagLen];
            for (int i = 0; i < FlagLen; i++)
            {
                Flags[i] = reader.ReadUInt32();
            }

            Floats = new float[FloatLen];
            for (int i = 0; i < FloatLen; i++)
            {
                Floats[i] = reader.ReadSingle();
            }

            Ints = new uint[IntLen];
            for (int i = 0; i < IntLen; i++)
            {
                Ints[i] = reader.ReadUInt32();
            }
        }

        protected override int GetSize()
        {
            int len = Name.Length + 4 + 16;
            if (HeaderInt1 == 1)
            {
                len += 2;
            }
            len += 10;
            if (Flags.Length > 0)
            {
                len += Flags.Length * 4;
            }
            if (Floats.Length > 0)
            {
                len += Floats.Length * 4;
            }
            if (Ints.Length > 0)
            {
                len += Ints.Length * 4;
            }

            return len;
        }
    }
}
