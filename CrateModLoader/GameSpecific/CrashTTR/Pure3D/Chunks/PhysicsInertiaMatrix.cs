using System.IO;

namespace Pure3D.Chunks
{
    [ChunkType(117510145)]
    public class PhysicsInertiaMatrix : Chunk
    {
        public Vector3 X;
        public Vector3 Y;

        public PhysicsInertiaMatrix(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            X = Util.ReadVector3(reader);
            Y = Util.ReadVector3(reader);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            Util.WriteVector3(writer, X);
            Util.WriteVector3(writer, Y);
        }

        public override string ToString()
        {
            return "Physics Inertia Matrix";
        }
    }
}
