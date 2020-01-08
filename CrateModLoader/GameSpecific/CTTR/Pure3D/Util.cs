using System.IO;
using System.Text;

namespace Pure3D
{
    public sealed class Util
    {
        // ReadString accessor because Pure3D loves null terminated strings.
        public static string ReadString(BinaryReader reader, ref ulong padding)
        {
            byte strLen = reader.ReadByte();
            string str = Encoding.ASCII.GetString(reader.ReadBytes(strLen));
            str = ZeroTerminate(str);
            padding = strLen - (ulong)str.Length;

            return str;
        }

        public static void WriteString(BinaryWriter writer, string str, ulong padding)
        {
            if (padding > 0)
            {
                for (ulong i = 0; i < padding; i++)
                {
                    str += char.MinValue;
                }
            }
            writer.Write(str);
        }

        public static string ZeroTerminate(string str)
        {
            int length = str.IndexOf(char.MinValue);
            return length != -1 ? str.Substring(0, length) : str;
        }

        public static Vector2 ReadVector2(BinaryReader reader)
        {
            Vector2 vector = new Vector2();

            vector.X = reader.ReadSingle();
            vector.Y = reader.ReadSingle();

            return vector;
        }

        public static void WriteVector2 (BinaryWriter writer, Vector2 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        public static Vector3 ReadVector3(BinaryReader reader)
        {
            Vector3 vector = new Vector3();

            vector.X = reader.ReadSingle();
            vector.Y = reader.ReadSingle();
            vector.Z = reader.ReadSingle();

            return vector;
        }

        public static void WriteVector3(BinaryWriter writer, Vector3 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        public static Quaternion ReadQuaternion(BinaryReader reader)
        {
            Quaternion vector = new Quaternion();

            vector.X = reader.ReadSingle();
            vector.Y = reader.ReadSingle();
            vector.Z = reader.ReadSingle();
            vector.W = reader.ReadSingle();

            return vector;
        }

        public static void WriteQuaternion(BinaryWriter writer, Quaternion vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
            writer.Write(vector.W);
        }

        public static Matrix ReadMatrix(BinaryReader reader)
        {
            Matrix matrix = new Matrix();

            matrix.M11 = reader.ReadSingle();
            matrix.M12 = reader.ReadSingle();
            matrix.M13 = reader.ReadSingle();
            matrix.M14 = reader.ReadSingle();
            matrix.M21 = reader.ReadSingle();
            matrix.M22 = reader.ReadSingle();
            matrix.M23 = reader.ReadSingle();
            matrix.M24 = reader.ReadSingle();
            matrix.M31 = reader.ReadSingle();
            matrix.M32 = reader.ReadSingle();
            matrix.M33 = reader.ReadSingle();
            matrix.M34 = reader.ReadSingle();
            matrix.M41 = reader.ReadSingle();
            matrix.M42 = reader.ReadSingle();
            matrix.M43 = reader.ReadSingle();
            matrix.M44 = reader.ReadSingle();

            return matrix;
        }

        public static void WriteMatrix(BinaryWriter writer, Matrix matrix)
        {
            writer.Write(matrix.M11);
            writer.Write(matrix.M12);
            writer.Write(matrix.M13);
            writer.Write(matrix.M14);
            writer.Write(matrix.M21);
            writer.Write(matrix.M22);
            writer.Write(matrix.M23);
            writer.Write(matrix.M24);
            writer.Write(matrix.M31);
            writer.Write(matrix.M32);
            writer.Write(matrix.M33);
            writer.Write(matrix.M34);
            writer.Write(matrix.M41);
            writer.Write(matrix.M42);
            writer.Write(matrix.M43);
            writer.Write(matrix.M44);
        }
    }
}
