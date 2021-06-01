using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    // variable-length quantity int, size depends on the number (0-127 - 1 byte, 128-16383 - 2 bytes, 16384-2097151 - 3 bytes, 2097152-268435455 - 4 bytes) (sacrifices 4 bits for size)
    public class VInt
    {
        public uint Value;
        public uint RawValue; // make this private at some point

        public VInt() { }
        public VInt(uint v)
        {
            Value = v;
            Compress(Value);
        }
        public VInt(BinaryReader reader)
        {
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            RawValue = reader.ReadByte();
            if (RawValue > 0x7F)
            {
                byte valueKeyExt = reader.ReadByte();
                uint valueAdd = (uint)(valueKeyExt * 0x100);
                RawValue += valueAdd;
            }
            if (RawValue > 0x7FFF)
            {
                byte valueKeyExt2 = reader.ReadByte();
                uint valueAdd2 = (uint)(valueKeyExt2 * 0x10000);
                RawValue += valueAdd2;
            }
            if (RawValue > 0x7FFFFF)
            {
                byte valueKeyExt3 = reader.ReadByte();
                uint valueAdd3 = (uint)(valueKeyExt3 * 0x1000000);
                RawValue += valueAdd3;
            }

            Value = GetUncompressed();
        }

        public void Write(BinaryWriter writer)
        {
            Compress(Value);

            if (RawValue > 0x7FFFFF)
            {
                byte valueKeyExt3 = (byte)(RawValue / 0x1000000);
                uint valueKeySub1 = (uint)(RawValue - (valueKeyExt3 * 0x1000000));
                byte valueKeyExt2 = (byte)(valueKeySub1 / 0x10000);
                uint valueKeySub2 = (uint)(valueKeySub1 - (valueKeyExt2 * 0x10000));
                byte valueKeyExt1 = (byte)(valueKeySub2 / 0x100);
                uint valueKeySub3 = (uint)(valueKeySub2 - (valueKeyExt1 * 0x100));
                byte valueKeyEnd = (byte)(valueKeySub3);
                writer.Write(valueKeyEnd);
                writer.Write(valueKeyExt1);
                writer.Write(valueKeyExt2);
                writer.Write(valueKeyExt3);
            }
            else if (RawValue > 0x7FFF)
            {
                byte valueKeyExt2 = (byte)(RawValue / 0x10000);
                uint valueKeySub2 = (uint)(RawValue - (valueKeyExt2 * 0x10000));
                byte valueKeyExt1 = (byte)(valueKeySub2 / 0x100);
                uint valueKeySub3 = (uint)(valueKeySub2 - (valueKeyExt1 * 0x100));
                byte valueKeyEnd = (byte)(valueKeySub3);
                writer.Write(valueKeyEnd);
                writer.Write(valueKeyExt1);
                writer.Write(valueKeyExt2);
            }
            else if (RawValue > 0x7F)
            {
                byte valueKeyExt = (byte)(RawValue / 0x100);
                uint valueKeySub = (uint)(valueKeyExt * 0x100);
                byte valueKeyEnd = (byte)(RawValue - valueKeySub);
                writer.Write(valueKeyEnd);
                writer.Write(valueKeyExt);
            }
            else
            {
                writer.Write((byte)RawValue);
            }
        }

        private uint GetUncompressed()
        {
            if (RawValue < 0x80)
            {
                return RawValue;
            }
            else if (RawValue >= 0x0180 && RawValue <= 0x7FFF)
            {
                uint Comp = (byte)(RawValue / 0x100);
                uint valueSub = (Comp * 0x100);
                uint Rest = (RawValue - valueSub);
                Rest = (Rest - 0x80);
                return ((Comp * 0x80) + Rest);
            }
            else if (RawValue >= 0x018080 && RawValue <= 0x7FFFFF)
            {
                uint Comp1 = (byte)(RawValue / 0x10000);
                uint valueSub1 = (Comp1 * 0x10000);
                uint Rest1 = (RawValue - valueSub1); //0x008080

                uint Comp2 = (byte)(Rest1 / 0x100);
                uint valueSub2 = (Comp2 * 0x100);
                uint Rest2 = (RawValue - valueSub1) - valueSub2; //0x000080

                Rest1 -= Rest2; //0x008000
                Rest1 -= 0x8000;
                Rest1 = (byte)(Rest1 / 0x100);
                Rest2 -= 0x80;
                return ((Comp1 * 0x80 * 0x80) + (Rest1 * 0x80) + Rest2);
            }
            else if (RawValue >= 0x01808080)
            {
                uint Comp1 = (byte)(RawValue / 0x1000000);
                uint valueSub1 = (Comp1 * 0x1000000);
                uint Rest1 = (RawValue - valueSub1); //0x00808080

                uint Comp2 = (byte)(RawValue / 0x10000);
                uint valueSub2 = (Comp2 * 0x10000);
                uint Rest2 = (RawValue - valueSub1) - valueSub2; //0x00008080

                uint Comp3 = (byte)(Rest2 / 0x100);
                uint valueSub3 = (Comp3 * 0x100);
                uint Rest3 = ((RawValue - valueSub1) - valueSub2) - valueSub3; //0x00000080

                Rest1 -= Rest2; //0x00800080
                Rest1 -= Rest3; //0x00800000
                Rest2 -= Rest3; //0x00008000
                Rest1 -= 0x800000;
                Rest1 = (byte)(Rest1 / 0x10000);
                Rest2 -= 0x8000;
                Rest2 = (byte)(Rest2 / 0x100);
                Rest3 -= 0x80;
                return ((Comp1 * 0x80 * 0x80 * 0x80) + (Rest1 * 0x80 * 0x80) + (Rest2 * 0x80) + Rest3);
            }
            else
            {
                throw new FormatException("Invalid value!");
            }
        }

        private void Compress(uint In)
        {
            if (In < 128)
            {
                RawValue = In;
            }
            else if (In >= 128 && In <= 16383)
            {
                byte Low = (byte)(In % 0x80);
                Low += 0x80;
                byte Pow = (byte)(In / 0x80);
                RawValue = (uint)((Pow * 0x100) + Low);
            }
            else if (In >= 16384 && In <= 2097151)
            {
                byte Pow = (byte)(In / 0x4000);
                uint Val1 = (uint)(In - (Pow * 0x10000));

                byte Mid = (byte)(Val1 / 0x80);
                Mid += 0x80;

                byte Low = (byte)(Val1 % 0x80);
                Low += 0x80;
                
                RawValue = (uint)((Pow * 0x10000) + (Mid * 0x100) + Low);
            }
            else if (In >= 2097152 && In <= 268435455)
            {
                byte Top = (byte)(In / 0x200000);
                uint Val1 = (uint)(In - (Top * 0x1000000));

                byte Pow = (byte)(Val1 / 0x4000);
                uint Val2 = (uint)((In - (Top * 0x1000000)) - (Pow * 0x10000));
                Pow += 0x80;

                byte Mid = (byte)(Val2 / 0x80);
                Mid += 0x80;

                byte Low = (byte)(Val2 % 0x80);
                Low += 0x80;

                RawValue = (uint)((Top * 0x1000000) + (Pow * 0x10000) + (Mid * 0x100) + Low);
            }
            else
            {
                throw new FormatException("Value too big to compress!");
            }
        }
    }

    public class ByteBool
    {
        public bool Value;

        public ByteBool() { }
        public ByteBool(BinaryReader reader)
        {
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            byte test = reader.ReadByte();
            if (test != 0x00)
            {
                Value = true;
            }
            else
            {
                Value = false;
            }
        }

        public void Write(BinaryWriter writer)
        {
            if (Value)
            {
                writer.Write((byte)0x01);
            }
            else
            {
                writer.Write((byte)0x00);
            }
        }
    }

    public class Vector2
    {
        public float X;
        public float Y;

        public Vector2() { }
        public Vector2(BinaryReader reader)
        {
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();

        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }
    }

    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3() { }
        public Vector3(BinaryReader reader)
        {
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }
    }

    public class Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4() { }
        public Vector4(BinaryReader reader)
        {
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            W = reader.ReadSingle();
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(W);
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }
    }

    public class Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color() { }
        public Color(BinaryReader reader)
        {
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            R = reader.ReadSingle();
            G = reader.ReadSingle();
            B = reader.ReadSingle();
            A = reader.ReadSingle();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(R);
            writer.Write(G);
            writer.Write(B);
            writer.Write(A);
        }
    }
    public class ByteColor
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public ByteColor() { }
        public ByteColor(BinaryReader reader)
        {
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            R = reader.ReadByte();
            G = reader.ReadByte();
            B = reader.ReadByte();
            A = reader.ReadByte();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(R);
            writer.Write(G);
            writer.Write(B);
            writer.Write(A);
        }
    }

    public class Matrix
    {
        public Vector4 M1 = new Vector4();
        public Vector4 M2 = new Vector4();
        public Vector4 M3 = new Vector4();
        public float MF;

        public Matrix() { }
        public Matrix(BinaryReader reader)
        {
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            M1.Read(reader);
            M2.Read(reader);
            M3.Read(reader);
            MF = reader.ReadSingle();
        }

        public void Write(BinaryWriter writer)
        {
            M1.Write(writer);
            M2.Write(writer);
            M3.Write(writer);
            writer.Write(MF);
        }
    }
}
