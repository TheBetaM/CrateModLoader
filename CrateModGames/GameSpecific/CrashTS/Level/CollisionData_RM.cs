using System;
using System.Collections.Generic;
using System.Numerics;
using CrateModLoader.LevelAPI;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class CollisionData_RM : CollisionData<ColData>
    {
        public override void Load(ColData Geom)
        {
            Vertices = new List<Vector4>();
            for (int i = 0; i < Geom.Vertices.Count; i++)
            {
                Vertices.Add(new Vector4(-Geom.Vertices[i].X, Geom.Vertices[i].Y, Geom.Vertices[i].Z, Geom.Vertices[i].W));
            }
            Indices = new List<int>();
            for (int i = 0; i < Geom.Tris.Count; i++)
            {
                Indices.Add(Geom.Tris[i].Vert1);
                Indices.Add(Geom.Tris[i].Vert2);
                Indices.Add(Geom.Tris[i].Vert3);
            }

            Normals = new List<System.Numerics.Vector3>();
            for (int i = 0; i < Vertices.Count; i++)
            {
                int ind = i * 3;
                int v1 = Indices[ind];
                int v2 = Indices[ind + 1];
                int v3 = Indices[ind + 2];
                System.Numerics.Vector3 Vert1 = new System.Numerics.Vector3(Vertices[v1].X, Vertices[v1].Y, Vertices[v1].Z);
                System.Numerics.Vector3 Vert2 = new System.Numerics.Vector3(Vertices[v2].X, Vertices[v2].Y, Vertices[v2].Z);
                System.Numerics.Vector3 Vert3 = new System.Numerics.Vector3(Vertices[v3].X, Vertices[v3].Y, Vertices[v3].Z);
                System.Numerics.Vector3 normal = CalcNormal(Vert1, Vert2, Vert3);
                Normals.Add(normal);
            }

        }

        public System.Numerics.Vector3 CalcNormal(System.Numerics.Vector3 Vert1, System.Numerics.Vector3 Vert2, System.Numerics.Vector3 Vert3)
        {
            System.Numerics.Vector3 u = Vert2 - Vert1;
            System.Numerics.Vector3 v = Vert3 - Vert1;
            float nx = u.Y * v.Z - u.Z * v.Y;
            float ny = u.Z * v.X - u.X * v.Z;
            float nz = u.X * v.Y - u.Y * v.X;
            return new System.Numerics.Vector3(nx, ny, nz);
        }
    }
}
