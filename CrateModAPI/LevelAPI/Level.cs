using System;
using System.Collections.Generic;

namespace CrateModLoader.LevelAPI
{
    /// <summary>
    /// Universal Level handler
    /// </summary>
    public abstract class Level<T> : LevelBase
    {
        public abstract void Load(T data);
        public virtual void Save(T data) { }
    }
}
