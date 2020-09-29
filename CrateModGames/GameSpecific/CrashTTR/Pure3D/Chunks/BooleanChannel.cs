using System.IO;
using System.Text;
using System;

namespace Pure3D.Chunks
{
    [ChunkType(1184008)]
    public class BooleanChannel : Chunk
    {
        public uint Version;
        public uint NumberOfFrames;
        public string Parameter;
        public ushort[] Values;
        public ushort StartState;

        public BooleanChannel(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            BinaryReader reader = new BinaryReader(stream);

            Version = reader.ReadUInt32();
            Parameter = Util.ZeroTerminate(Encoding.ASCII.GetString(reader.ReadBytes(4)));
            StartState = reader.ReadUInt16();
            NumberOfFrames = reader.ReadUInt32();

            Values = new ushort[NumberOfFrames];
            for (int i = 0; i < NumberOfFrames; i++)
            {
                Values[i] = reader.ReadUInt16();
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
            writer.Write(StartState);
            writer.Write(NumberOfFrames);
            for (int i = 0; i < NumberOfFrames; i++)
            {
                writer.Write(Values[i]);
            }
        }

        public override string ToString()
        {
            return $"Boolean Channel: {Parameter}, {NumberOfFrames} Frames";
        }
    }
}