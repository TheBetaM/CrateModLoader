using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(1183746)]
    public class AnimationGroupList : Chunk
    {
        public uint Version;
        public uint NumberOfGroups;

        public AnimationGroupList(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            Version = reader.ReadUInt32();
            NumberOfGroups = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Version);
            writer.Write(NumberOfGroups);
        }

        public override string ToString()
        {
            return $"Animation Group List: {NumberOfGroups}";
        }
    }
}
