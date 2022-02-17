using System;
using System.Collections.Generic;
using System.Numerics;
using System.ComponentModel;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class CollisionData_XOM : LevelObjectData<XOM.XCollisionGeometry>
    {
        [Browsable(false)]
        public override string Name { get; set; } = string.Empty;
        [Browsable(false)]
        public override ObjectVector3 Position { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 Rotation { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);

        [Browsable(false)]
        public override ObjectVector3 WorldScale => new ObjectVector3(2.2f);

        public override void Load(XOM.XCollisionGeometry Geom)
        {
            Visual = EditorVisual.Custom;
            VisualData = new CollisionDataBase();

            VisualData.Vertices = new List<Vector4>();
            for (int i = 0; i < Geom.Vertices.Count; i++)
            {
                VisualData.Vertices.Add(new Vector4(Geom.Vertices[i], Geom.Vertices[i + 1], Geom.Vertices[i + 2], Geom.Vertices[i + 3]));
                i += 3;
            }
            VisualData.Indices = new List<int>();
            for (int i = 0; i < Geom.Indices.Count; i++)
            {
                if (i % 4 != 0)
                {
                    VisualData.Indices.Add(Geom.Indices[i]);
                }
            }

            VisualData.Normals = new List<System.Numerics.Vector3>();
            for (int i = 0; i < VisualData.Vertices.Count; i++)
            {
                Vector3 normal = new Vector3(0.5f, 0.5f, 0.5f);
                VisualData.Normals.Add(normal);
            }
        }

        public override string ToString()
        {
            return "Collision Data";
        }
    }
}
