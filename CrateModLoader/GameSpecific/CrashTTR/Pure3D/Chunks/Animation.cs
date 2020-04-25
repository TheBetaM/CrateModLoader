using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(1183744)]
    public class Animation : VersionNamed
    {
        public string AnimType;
        public float NumberOfFrames;
        public float FrameRate;
        public uint Looping;

        public Animation(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);
            base.ReadHeader(stream, length);
            AnimType = Util.ZeroTerminate(Encoding.ASCII.GetString(reader.ReadBytes(4)));
            NumberOfFrames = reader.ReadSingle();
            FrameRate = reader.ReadSingle();
            Looping = reader.ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            for (int i = 0; i < 4; i++)
            {
                if (i < AnimType.Length)
                {
                    writer.Write((byte)AnimType[i]);
                }
                else
                {
                    writer.Write((byte)0x00);
                }
            }
            writer.Write(NumberOfFrames);
            writer.Write(FrameRate);
            writer.Write(Looping);
        }

        public override string ToString()
        {
            return $"Animation: {Name}, Version {Version}, Type {Type}, Frames {NumberOfFrames}, FrameRate {FrameRate}, Looping {Looping}";
        }
    }
}
