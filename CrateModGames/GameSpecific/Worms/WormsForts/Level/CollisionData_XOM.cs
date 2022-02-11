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
                Vertices.Add(new Vector4(Geom.Vertices[i], Geom.Vertices[i + 1], Geom.Vertices[i + 2], Geom.Vertices[i + 3]));
                i += 3;
            }
            Indices = new List<int>();
            for (int i = 0; i < Geom.Indices.Count; i++)
            {
                Indices.Add(Geom.Indices[i]);
            }
        }
    }
}
