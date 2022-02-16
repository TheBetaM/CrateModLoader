using System;
using System.Collections.Generic;
using System.Numerics;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class CollisionData_XOM : CollisionData<XOM.XCollisionGeometry>
    {
        public override void Load(XOM.XCollisionGeometry Geom)
        {
            Vertices = new List<Vector4>();
            for (int i = 0; i < Geom.Vertices.Count; i++)
            {
                Vertices.Add(new Vector4(Geom.Vertices[i] * 10, Geom.Vertices[i + 1] * 10, Geom.Vertices[i + 2] * 10, Geom.Vertices[i + 3]));
                i += 3;
            }
            Indices = new List<int>();
            for (int i = 0; i < Geom.Indices.Count; i++)
            {
                if (i % 4 != 0)
                {
                    Indices.Add(Geom.Indices[i]);
                }
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
