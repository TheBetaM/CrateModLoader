using OpenTK;
using Twinsanity;

namespace TwinsaityEditor
{
    public static class VectorFuncs
    {
        public static Vector3 CalcNormal(Vector3 Vert1, Vector3 Vert2, Vector3 Vert3)
        {
            Vector3 u = Vert2 - Vert1;
            Vector3 v = Vert3 - Vert1;
            float nx = u.Y * v.Z - u.Z * v.Y;
            float ny = u.Z * v.X - u.X * v.Z;
            float nz = u.X * v.Y - u.Y * v.X;
            return new Vector3(nx, ny, nz);
        }

        public static Vector4 Pos2Vector4(Pos pos)
        {
            return new Vector4(pos.X, pos.Y, pos.Z, pos.W);
        }

        public static Pos ToPos(this Vector4 v)
        {
            return new Pos(v.X, v.Y, v.Z, v.W);
        }

        public static Vector3 ToVec3(this Pos p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }

        public static Vector4 ToVec4(this Pos p)
        {
            return new Vector4(p.X, p.Y, p.Z, p.W);
        }

        public static Vector4 NormalizeW(this Vector4 v)
        {
            v.X *= v.W;
            v.Y *= v.W;
            v.Z *= v.W;
            v.W /= v.W;
            return v;
        }
    }
}
