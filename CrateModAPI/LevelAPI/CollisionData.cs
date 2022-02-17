using System;
using System.Collections.Generic;
using System.Numerics;

namespace CrateModLoader.LevelAPI
{
    /// <summary>
    /// Universal collision model data handler
    /// </summary>
    public abstract class CollisionData<T>
    {
        public abstract void Load(T data);
        public virtual void Save(T data) { }
    }
}
