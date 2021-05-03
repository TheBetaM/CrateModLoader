using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    public abstract class ModStruct<T> : Mod
    {
        public sealed override void ModPass(object value)
        {
            if (value is T)
            {
                ModPass((T)value);
            }
            else
            {
                return;
            }
        }

        public sealed override void CachePass(object value)
        {
            if (value is T)
            {
                CachePass((T)value);
            }
            else
            {
                return;
            }
        }

        public sealed override void PreloadPass(object value)
        {
            if (value is T)
            {
                PreloadPass((T)value);
            }
            else
            {
                return;
            }
        }

        public virtual void CachePass(T value) { }
        public abstract void ModPass(T value);
        public virtual void PreloadPass(T value) { }
    }

    public abstract class ModStruct<T, U> : Mod
    {
        public sealed override void ModPass(object value)
        {
            if (value is T)
            {
                ModPass((T)value);
            }
            else if (value is U)
            {
                ModPass((U)value);
            }
            else
            {
                return;
            }
        }

        public sealed override void CachePass(object value)
        {
            if (value is T)
            {
                CachePass((T)value);
            }
            else if (value is U)
            {
                CachePass((U)value);
            }
            else
            {
                return;
            }
        }

        public sealed override void PreloadPass(object value)
        {
            if (value is T)
            {
                PreloadPass((T)value);
            }
            else if (value is U)
            {
                PreloadPass((U)value);
            }
            else
            {
                return;
            }
        }

        public virtual void CachePass(T value) { }
        public abstract void ModPass(T value);
        public virtual void PreloadPass(T value) { }
        public virtual void CachePass(U value) { }
        public abstract void ModPass(U value);
        public virtual void PreloadPass(U value) { }
    }
}
