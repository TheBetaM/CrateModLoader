using System;
using System.Collections.Generic;
using System.Numerics;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class SceneryData_NSF_Old : CollisionData<OldSceneryEntry>
    {
        public override void Load(OldSceneryEntry entry)
        {

            Vertices = new List<Vector4>();
            for (int i = 0; i < entry.Vertices.Count; i++)
            {
                Vertices.Add(new Vector4(entry.Vertices[i].X, entry.Vertices[i].Y, entry.Vertices[i].Z, 1));
            }
            Indices = new List<int>();
            for (int i = 0; i < entry.Polygons.Count; i++)
            {
                Indices.Add(entry.Polygons[i].VertexA);
                Indices.Add(entry.Polygons[i].VertexB);
                Indices.Add(entry.Polygons[i].VertexC);
            }

            Normals = new List<System.Numerics.Vector3>();
            for (int i = 0; i < Vertices.Count; i++)
            {
                //int ind = i * 3;
                //int v1 = Indices[ind];
                //int v2 = Indices[ind + 1];
                //int v3 = Indices[ind + 2];
                //System.Numerics.Vector3 Vert1 = new System.Numerics.Vector3(Vertices[v1].X, Vertices[v1].Y, Vertices[v1].Z);
                //System.Numerics.Vector3 Vert2 = new System.Numerics.Vector3(Vertices[v2].X, Vertices[v2].Y, Vertices[v2].Z);
                //System.Numerics.Vector3 Vert3 = new System.Numerics.Vector3(Vertices[v3].X, Vertices[v3].Y, Vertices[v3].Z);
                //System.Numerics.Vector3 normal = CalcNormal(Vert1, Vert2, Vert3);
                Vector3 normal = new Vector3(0.5f, 0.5f, 0.5f);
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
