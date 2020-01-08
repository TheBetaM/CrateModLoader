using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(8704)]
    public class Camera : Named
    {
        public uint Version;
        public float FieldOfView;
        public float AspectRatio;
        public float NearClip;
        public float FarClip;
        public Vector3 Position;
        public Vector3 Look;
        public Vector3 Up;

        public Camera(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            Version = reader.ReadUInt32();
            FieldOfView = reader.ReadSingle();
            AspectRatio = reader.ReadSingle();
            NearClip = reader.ReadSingle();
            FarClip = reader.ReadSingle();
            Position = Util.ReadVector3(reader);
            Look = Util.ReadVector3(reader);
            Up = Util.ReadVector3(reader);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Version);
            writer.Write(FieldOfView);
            writer.Write(AspectRatio);
            writer.Write(NearClip);
            writer.Write(FarClip);
            Util.WriteVector3(writer, Position);
            Util.WriteVector3(writer, Look);
            Util.WriteVector3(writer, Up);
        }

        public override string ToString()
        {
            return $"Camera: {Name}";
        }
    }
}
