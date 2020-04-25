using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    [ChunkType(1184005)]
    public class QuaternionChannel : Chunk
    {
        public uint Version;
        public uint NumberOfFrames;
        public string Parameter;
        public Quaternion[] Values;
        public ushort[] Frames;

        public QuaternionChannel(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            var streamPosition = stream.Position;

            BinaryReader reader = new BinaryReader(stream);
            Version = reader.ReadUInt32();
            Parameter = Util.ZeroTerminate(Encoding.ASCII.GetString(reader.ReadBytes(4)));
            NumberOfFrames = reader.ReadUInt32();

            Frames = new ushort[NumberOfFrames];
            for (int i = 0; i < NumberOfFrames; i++)
            {
                Frames[i] = reader.ReadUInt16();
            }

            Values = new Quaternion[NumberOfFrames];
            for (int i = 0; i < NumberOfFrames; i++)
            {
                var w = reader.ReadSingle();
                Values[i] = new Quaternion
                (
                    reader.ReadSingle(),
                    reader.ReadSingle(),
                    reader.ReadSingle(),
                    w
                );
            }
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Version);
            for (int i = 0; i < 4; i++)
            {
                if (i < Parameter.Length)
                {
                    writer.Write((byte)Parameter[i]);
                }
                else
                {
                    writer.Write((byte)0x00);
                }
            }
            writer.Write(NumberOfFrames);
            for (int i = 0; i < NumberOfFrames; i++)
            {
                writer.Write(Frames[i]);
            }
            for (int i = 0; i < NumberOfFrames; i++)
            {
                writer.Write(Values[i].W);
                writer.Write(Values[i].X);
                writer.Write(Values[i].Y);
                writer.Write(Values[i].Z);
            }
        }

        public override string ToString()
        {
            return $"Quaternion Channel: {Parameter}, {NumberOfFrames} Frames";
        }
    }
}
