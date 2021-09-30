using System;
using OpenTK;

namespace TwinsaityEditor.Utils
{
    public static class MatrixWrapper
    {
        public static Matrix4 RotateMatrix4(float angleX, float angleY, float angleZ)
        {
            Matrix4 mat = Matrix4.Identity;

            mat *= Matrix4.CreateRotationX(angleX);
            mat *= Matrix4.CreateRotationY(angleY);
            mat *= Matrix4.CreateRotationZ(angleZ);

            return mat;
        }

        public static void RotateMatrix4(ref Matrix4 mat, float angleX, float angleY, float angleZ)
        {
            mat *= RotateMatrix4(angleX, angleY, angleZ);
        }

        public static void DecomposeMatrix(ref Matrix4 m, ref Vector3 pos, ref Vector3 rot, ref Vector3 sca, ref float w)
        {
            //no touchy
            w = m.M44;

            //position
            pos.X = m.M14;
            pos.Y = m.M24;
            pos.Z = m.M34;

            //scale
            double sca_x = Math.Sqrt(m.M11 * m.M11 + m.M21 * m.M21 + m.M31 * m.M31);
            double sca_y = Math.Sqrt(m.M12 * m.M12 + m.M22 * m.M22 + m.M32 * m.M32);
            double sca_z = Math.Sqrt(m.M13 * m.M13 + m.M23 * m.M23 + m.M33 * m.M33);

            sca.X = (float)sca_x;
            sca.Y = (float)sca_y;
            sca.Z = (float)sca_z;

            //rotation
            double rot_y = Math.Asin(-m.M13);
            double rot_x, rot_z;
            if (Math.Cos(rot_y) != 0)
            {
                rot_x = Math.Atan2(m.M23 * sca_x, m.M33);
                rot_z = Math.Atan2(m.M12 * sca_y, m.M11);
            }
            else
            {
                rot_x = 0;
                rot_z = Math.Atan2(m.M22, m.M21);
            }

            rot.X = (float)rot_x;
            rot.Y = (float)rot_y;
            rot.Z = (float)rot_z;
        }
    }
}
