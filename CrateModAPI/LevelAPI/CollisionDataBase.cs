using System;
using System.Collections.Generic;
using System.Numerics;

namespace CrateModLoader.LevelAPI
{
    public class CollisionDataBase
    {
        public List<int> Indices;
        public List<Vector4> Vertices;
        public List<Vector3> Normals;
        public List<Vector2> TexCoords;
    }
}
