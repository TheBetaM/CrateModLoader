using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    public abstract class ModStruct<T> : Mod
    {
        public sealed override void ModPass(object value)
        {
            if (value is T)
                ModPass((T)value);
            else
                return;
        }

        public sealed override void CachePass(object value)
        {
            if (value is T)
                CachePass((T)value);
            else
                return;
        }

        public sealed override void PreloadPass(object value)
        {
            if (value is T)
                PreloadPass((T)value);
            else
                return;
        }

        public virtual void CachePass(T value) { }
        public abstract void ModPass(T value);
        public virtual void PreloadPass(T value) { }
    }

    public abstract class ModStruct<T1, T2> : Mod
    {
        public sealed override void ModPass(object value)
        {
            if (value is T1)
                ModPass((T1)value);
            else if (value is T2)
                ModPass((T2)value);
            else
                return;
        }

        public sealed override void CachePass(object value)
        {
            if (value is T1)
                CachePass((T1)value);
            else if (value is T2)
                CachePass((T2)value);
            else
                return;
        }

        public sealed override void PreloadPass(object value)
        {
            if (value is T1)
                PreloadPass((T1)value);
            else if (value is T2)
                PreloadPass((T2)value);
            else
                return;
        }

        public virtual void CachePass(T1 value) { }
        public abstract void ModPass(T1 value);
        public virtual void PreloadPass(T1 value) { }
        public virtual void CachePass(T2 value) { }
        public abstract void ModPass(T2 value);
        public virtual void PreloadPass(T2 value) { }
    }

    public abstract class ModStruct<T1, T2, T3> : Mod
    {
        public sealed override void ModPass(object value)
        {
            if (value is T1)
                ModPass((T1)value);
            else if (value is T2)
                ModPass((T2)value);
            else if (value is T3)
                ModPass((T3)value);
            else
                return;
        }

        public sealed override void CachePass(object value)
        {
            if (value is T1)
                CachePass((T1)value);
            else if (value is T2)
                CachePass((T2)value);
            else if (value is T3)
                CachePass((T3)value);
            else
                return;
        }

        public sealed override void PreloadPass(object value)
        {
            if (value is T1)
                PreloadPass((T1)value);
            else if (value is T2)
                PreloadPass((T2)value);
            else if (value is T3)
                PreloadPass((T3)value);
            else
                return;
        }

        public virtual void CachePass(T1 value) { }
        public abstract void ModPass(T1 value);
        public virtual void PreloadPass(T1 value) { }
        public virtual void CachePass(T2 value) { }
        public abstract void ModPass(T2 value);
        public virtual void PreloadPass(T2 value) { }
        public virtual void CachePass(T3 value) { }
        public abstract void ModPass(T3 value);
        public virtual void PreloadPass(T3 value) { }
    }

    public abstract class ModStruct<T1, T2, T3, T4> : Mod
    {
        public sealed override void ModPass(object value)
        {
            if (value is T1)
                ModPass((T1)value);
            else if (value is T2)
                ModPass((T2)value);
            else if (value is T3)
                ModPass((T3)value);
            else if (value is T4)
                ModPass((T4)value);
            else
                return;
        }

        public sealed override void CachePass(object value)
        {
            if (value is T1)
                CachePass((T1)value);
            else if (value is T2)
                CachePass((T2)value);
            else if (value is T3)
                CachePass((T3)value);
            else if (value is T4)
                CachePass((T4)value);
            else
                return;
        }

        public sealed override void PreloadPass(object value)
        {
            if (value is T1)
                PreloadPass((T1)value);
            else if (value is T2)
                PreloadPass((T2)value);
            else if (value is T3)
                PreloadPass((T3)value);
            else if (value is T4)
                PreloadPass((T4)value);
            else
                return;
        }

        public virtual void CachePass(T1 value) { }
        public abstract void ModPass(T1 value);
        public virtual void PreloadPass(T1 value) { }
        public virtual void CachePass(T2 value) { }
        public abstract void ModPass(T2 value);
        public virtual void PreloadPass(T2 value) { }
        public virtual void CachePass(T3 value) { }
        public abstract void ModPass(T3 value);
        public virtual void PreloadPass(T3 value) { }
        public virtual void CachePass(T4 value) { }
        public abstract void ModPass(T4 value);
        public virtual void PreloadPass(T4 value) { }
    }
}
