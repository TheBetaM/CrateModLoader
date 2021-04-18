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

        public sealed override void QuickPass(object value)
        {
            if (value is T)
            {
                QuickPass((T)value);
            }
            else
            {
                return;
            }
        }

        public virtual void CachePass(T value) { }
        public virtual void QuickPass(T value) { }
        public abstract void ModPass(T value);
    }
}
