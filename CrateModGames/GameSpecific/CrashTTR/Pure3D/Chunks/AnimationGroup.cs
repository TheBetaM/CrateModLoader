using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(1183745)]
    public class AnimationGroup : VersionNamed
    {
        public uint NumberOfChannels;
        public uint GroupId;

        public AnimationGroup(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            GroupId = reader.ReadUInt32();
            NumberOfChannels = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(GroupId);
            writer.Write(NumberOfChannels);
        }

        public override string ToString()
        {
            return $"Animation Group: {Name}, Channels {NumberOfChannels}, Group {GroupId}";
        }
    }
}
