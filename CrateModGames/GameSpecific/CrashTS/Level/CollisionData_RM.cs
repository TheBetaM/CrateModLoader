using System;
using System.Collections.Generic;
using System.Numerics;
using System.ComponentModel;
using CrateModLoader.LevelAPI;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class CollisionData_RM : LevelObjectData<ColData>
    {
        [Browsable(false)]
        public override string Name { get; set; } = string.Empty;
        [Browsable(false)]
        public override ObjectVector3 Position { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 Rotation { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);

        public override void Load(ColData Geom)
        {
            Visual = EditorVisual.Custom;
            VisualData = new CollisionDataBase();

            VisualData.Vertices = new List<Vector4>();
            for (int i = 0; i < Geom.Vertices.Count; i++)
            {
                VisualData.Vertices.Add(new Vector4(-Geom.Vertices[i].X, Geom.Vertices[i].Y, Geom.Vertices[i].Z, Geom.Vertices[i].W));
            }
            VisualData.Indices = new List<int>();
            for (int i = 0; i < Geom.Tris.Count; i++)
            {
                VisualData.Indices.Add(Geom.Tris[i].Vert1);
                VisualData.Indices.Add(Geom.Tris[i].Vert2);
                VisualData.Indices.Add(Geom.Tris[i].Vert3);
            }

            VisualData.Normals = new List<System.Numerics.Vector3>();
            for (int i = 0; i < VisualData.Vertices.Count; i++)
            {
                int ind = i * 3;
                int v1 = VisualData.Indices[ind];
                int v2 = VisualData.Indices[ind + 1];
                int v3 = VisualData.Indices[ind + 2];
                System.Numerics.Vector3 Vert1 = new System.Numerics.Vector3(VisualData.Vertices[v1].X, VisualData.Vertices[v1].Y, VisualData.Vertices[v1].Z);
                System.Numerics.Vector3 Vert2 = new System.Numerics.Vector3(VisualData.Vertices[v2].X, VisualData.Vertices[v2].Y, VisualData.Vertices[v2].Z);
                System.Numerics.Vector3 Vert3 = new System.Numerics.Vector3(VisualData.Vertices[v3].X, VisualData.Vertices[v3].Y, VisualData.Vertices[v3].Z);
                System.Numerics.Vector3 normal = CalcNormal(Vert1, Vert2, Vert3);
                VisualData.Normals.Add(normal);
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

        public override string ToString()
        {
            return "Collision Data";
        }
    }
}
