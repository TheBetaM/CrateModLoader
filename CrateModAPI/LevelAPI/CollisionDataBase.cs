using System;
using System.Collections.Generic;
using System.Numerics;

namespace CrateModLoader.LevelAPI
{
    public abstract class CollisionDataBase
    {
        public List<int> Indices { get; set; }
        public List<Vector4> Vertices { get; set; }
        public List<Vector3> Normals { get; set; }
        public List<Vector2> TexCoords { get; set; }
    }
}
