using System;
using System.Collections.Generic;
using System.Numerics;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash3
{
    public class SceneryData_NSF_New : CollisionData<NewSceneryEntry>
    {
        public override void Load(NewSceneryEntry entry)
        {

            Vertices = new List<Vector4>();
            for (int i = 0; i < entry.Vertices.Count; i++)
            {
                float X = ((entry.Vertices[i].X * 16) + entry.XOffset) / 100f;
                float Y = ((entry.Vertices[i].Y * 16) + entry.YOffset) / 100f;
                float Z = ((entry.Vertices[i].Z * 16) + entry.ZOffset) / 100f;
                Vertices.Add(new Vector4(X, Y, Z, 1));
            }
            Indices = new List<int>();
            for (int i = 0; i < entry.Triangles.Count; i++)
            {
                Indices.Add(entry.Triangles[i].VertexA);
                Indices.Add(entry.Triangles[i].VertexB);
                Indices.Add(entry.Triangles[i].VertexC);
            }

            Normals = new List<System.Numerics.Vector3>();
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vector3 normal = new Vector3(0.5f, 0.5f, 0.5f);
                Normals.Add(normal);
            }

        }
    }
}
