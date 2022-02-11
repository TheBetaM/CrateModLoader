using System;
using System.Collections.Generic;

namespace CrateModLoader.LevelAPI
{
    public abstract class LevelObjectData<T> : LevelObjectDataBase
    {
        public T WrappedObject;

        public sealed override void Load(object LevelObject)
        {
            if (LevelObject is T Target)
            {
                WrappedObject = Target;
                Load(WrappedObject);
            }
        }

        public sealed override void Save()
        {
            if (WrappedObject != null)
            {
                Save(WrappedObject);
            }
        }

        public abstract void Load(T data);
        public virtual void Save(T data) { }
    }
}
