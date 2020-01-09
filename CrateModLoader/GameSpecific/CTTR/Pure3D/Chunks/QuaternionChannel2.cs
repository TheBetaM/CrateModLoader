using System.IO;
using System.Text;
using System;

namespace Pure3D.Chunks
{
    [ChunkType(1184018)]
    public class QuaternionChannel2 : Chunk
    {
        public uint Version;
        public uint NumberOfFrames;
        public string Parameter;
        public Vector3[] Values; // Array of 3 (u)int16s
        public ushort[] Frames;

        public byte[] Data;

        public QuaternionChannel2(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {

            BinaryReader reader = new BinaryReader(stream);

            long currentPos = reader.BaseStream.Position;

            Version = reader.ReadUInt32();
            Parameter = Util.ZeroTerminate(Encoding.ASCII.GetString(reader.ReadBytes(4)));
            NumberOfFrames = reader.ReadUInt32();

            Frames = new ushort[NumberOfFrames];
            for (int i = 0; i < NumberOfFrames; i++)
            {
                Frames[i] = reader.ReadUInt16();
            }

            Values = new Vector3[NumberOfFrames];
            for (int i = 0; i < NumberOfFrames; i++)
            {
                Values[i] = new Vector3();
                Values[i].X = reader.ReadUInt16();
                Values[i].Y = reader.ReadUInt16();
                Values[i].Z = reader.ReadUInt16();
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
                writer.Write((ushort)Values[i].X);
                writer.Write((ushort)Values[i].Y);
                writer.Write((ushort)Values[i].Z);
            }
            
        }

        public override string ToString()
        {
            return $"Quaternion Channel 2: {Parameter}, Version {Version}, {NumberOfFrames} Frames";
        }
    }
}
