using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(94211)]
    public class BillboardDisplayInfo : Chunk
    {
        public uint Version;
        public Quaternion Rotation;
        public string CutOffMode;
        public Vector2 UVOffsetRange;
        public float SourceRange;
        public float EdgeRange;

        public BillboardDisplayInfo(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            Version = reader.ReadUInt32();
            Rotation = Util.ReadQuaternion(reader);
            CutOffMode = Util.ZeroTerminate(Encoding.ASCII.GetString(new BinaryReader(stream).ReadBytes(4)));
            UVOffsetRange = Util.ReadVector2(reader);
            SourceRange = reader.ReadSingle();
            EdgeRange = reader.ReadSingle();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Version);
            Util.WriteQuaternion(writer, Rotation);
            writer.Write(new byte[] { (byte)CutOffMode[0], (byte)CutOffMode[1], (byte)CutOffMode[2], (byte)CutOffMode[3], });
            Util.WriteVector2(writer, UVOffsetRange);
            writer.Write(SourceRange);
            writer.Write(EdgeRange);
        }

        public override string ToString()
        {
            return "Billboard Display Info";
        }
    }
}
