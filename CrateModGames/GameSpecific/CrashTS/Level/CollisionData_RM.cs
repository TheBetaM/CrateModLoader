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
                Vertices.Add(new Vector4(Geom.Vertices[i].X, Geom.Vertices[i].Y, Geom.Vertices[i].Z, Geom.Vertices[i].W));
            }
            Indices = new List<int>();
            for (int i = 0; i < Geom.Tris.Count; i++)
            {
                Indices.Add(Geom.Tris[i].Vert1);
                Indices.Add(Geom.Tris[i].Vert2);
                Indices.Add(Geom.Tris[i].Vert3);
            }
        }
    }
}
