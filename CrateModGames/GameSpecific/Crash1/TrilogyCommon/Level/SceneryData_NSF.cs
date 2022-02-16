using System;
using System.Collections.Generic;
using System.Numerics;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash2
{
    public class SceneryData_NSF : CollisionData<SceneryEntry>
    {
        public override void Load(SceneryEntry entry)
        {

            Vertices = new List<Vector4>();
            for (int i = 0; i < entry.Vertices.Count; i++)
            {
                float X = ((entry.Vertices[i].X << 4) + entry.XOffset) / 100f;
                float Y = ((entry.Vertices[i].Y << 4) + entry.YOffset) / 100f;
                float Z = ((entry.Vertices[i].Z << 4) + entry.ZOffset) / 100f;
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
