using System;
using System.Collections.Generic;
using System.Numerics;
using CrateModLoader.LevelAPI;
using System.ComponentModel;
using Twinsanity;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class CollisionData_MeshInfo : LevelObjectData<MeshInfo>
    {
        [Browsable(false)]
        public override string Name { get; set; } = string.Empty;
        [Browsable(false)]
        public override ObjectVector3 Position { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 Rotation { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);

        public override void Load(MeshInfo Geom)
        {
            Visual = EditorVisual.Custom;
            VisualData = new CollisionDataBase();

            VisualData.Vertices = new List<Vector4>();
            for (int i = 0; i < Geom.Vertices.Count; i++)
            {
                VisualData.Vertices.Add(new Vector4(Geom.Vertices[i].Position.X, Geom.Vertices[i].Position.Y, Geom.Vertices[i].Position.Z, 1));
            }
            VisualData.Indices = new List<int>();
            for (int i = 0; i < Geom.QuadBlocks.Count; i++)
            {
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[5]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[4]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[0]);

                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[6]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[4]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[5]);

                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[2]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[6]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[5]);

                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[6]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[2]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[8]);

                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[8]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[7]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[6]);

                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[3]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[7]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[8]);

                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[6]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[1]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[4]);

                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[7]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[1]);
                VisualData.Indices.Add(Geom.QuadBlocks[i].ind[6]);
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
            return "Mesh Data";
        }
    }
}
