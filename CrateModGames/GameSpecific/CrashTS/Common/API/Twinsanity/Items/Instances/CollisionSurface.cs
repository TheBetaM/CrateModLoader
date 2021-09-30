using System.IO;

namespace Twinsanity
{
    public class CollisionSurface : TwinsItem
    {
        public byte[] Flags; //4
        public ushort SurfaceID;
        public ushort[] SoundIDs; //10
        public Pos[] Floats; //4
        public ushort[] UnkInts; //12

        public override void Save(BinaryWriter writer)
        {
            for (int i = 0; i < Flags.Length; i++)
            {
                writer.Write(Flags[i]);
            }
            writer.Write(SurfaceID);
            for (int i = 0; i < SoundIDs.Length; i++)
            {
                writer.Write(SoundIDs[i]);
            }
            for (int i = 0; i < Floats.Length; i++)
            {
                writer.Write(Floats[i].X);
                writer.Write(Floats[i].Y);
                writer.Write(Floats[i].Z);
                writer.Write(Floats[i].W);
            }
            for (int i = 0; i < UnkInts.Length; i++)
            {
                writer.Write(UnkInts[i]);
            }
        }

        public override void Load(BinaryReader reader, int size)
        {
            Flags = new byte[4];
            for (int i = 0; i < Flags.Length; i++)
            {
                Flags[i] = reader.ReadByte();
            }
            SurfaceID = reader.ReadUInt16();
            SoundIDs = new ushort[10];
            for (int i = 0; i < SoundIDs.Length; i++)
            {
                SoundIDs[i] = reader.ReadUInt16();
            }
            Floats = new Pos[4];
            for (int i = 0; i < Floats.Length; i++)
            {
                Floats[i] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            }
            UnkInts = new ushort[12];
            for (int i = 0; i < UnkInts.Length; i++)
            {
                UnkInts[i] = reader.ReadUInt16();
            }
        }

        protected override int GetSize()
        {
            return 114;
        }
    }
}
