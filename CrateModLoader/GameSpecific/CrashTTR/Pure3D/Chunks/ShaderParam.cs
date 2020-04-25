using System.IO;
using System.Text;

namespace Pure3D.Chunks
{
    public abstract class ShaderParam : Chunk
    {
        public string Param;

        public ShaderParam(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            Param = Util.ZeroTerminate(Encoding.ASCII.GetString(new BinaryReader(stream).ReadBytes(4)));
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            for (int i = 0; i < 4; i++)
            {
                if (i < Param.Length)
                {
                    writer.Write((byte)Param[i]);
                }
                else
                {
                    writer.Write((byte)0x00);
                }
            }
        }

        public override string ToString()
        {
            return $"Shader Parameter: {Param}";
        }
    }

    [ChunkType(69634)]
    public class ShaderTextureParam : ShaderParam
    {
        public string Value;
        public ulong Value_paddding;

        public ShaderTextureParam(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            base.ReadHeader(stream, length);
            Value = Util.ReadString(new BinaryReader(stream), ref Value_paddding);
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            Util.WriteString(writer, Value, Value_paddding);
        }
    }

    [ChunkType(69635)]
    public class ShaderIntParam : ShaderParam
    {
        public uint Value;

        public ShaderIntParam(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            base.ReadHeader(stream, length);
            Value = new BinaryReader(stream).ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Value);
        }
    }

    [ChunkType(69636)]
    public class ShaderFloatParam : ShaderParam
    {
        public float Value;

        public ShaderFloatParam(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            base.ReadHeader(stream, length);
            Value = new BinaryReader(stream).ReadSingle();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Value);
        }
    }

    [ChunkType(69637)]
    public class ShaderColourParam : ShaderParam
    {
        public uint Color;

        public ShaderColourParam(File file, uint type) : base(file, type)
        {
        }

        public override void ReadHeader(Stream stream, long length)
        {
            base.ReadHeader(stream, length);
            Color = new BinaryReader(stream).ReadUInt32();
        }

        public override void WriteHeader(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            base.WriteHeader(stream);
            writer.Write(Color);
        }
    }
}
