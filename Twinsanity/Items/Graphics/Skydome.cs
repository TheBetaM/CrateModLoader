using System.IO;

namespace Twinsanity
{
    public class Skydome : TwinsItem
    {
        public uint Unknown { get; set; }
        public uint[] MeshIDs { get; set; }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Unknown);
            writer.Write(MeshIDs.Length);
            for (int i = 0; i < MeshIDs.Length; ++i)
                writer.Write(MeshIDs[i]);
        }

        public override void Load(BinaryReader reader, int size)
        {
            Unknown = reader.ReadUInt32();
            var count = reader.ReadInt32();
            MeshIDs = new uint[count];
            for (int i = 0; i < count; ++i)
                MeshIDs[i] = reader.ReadUInt32();
        }

        protected override int GetSize()
        {
            return 8 + MeshIDs.Length * 4;
        }
    }
}
