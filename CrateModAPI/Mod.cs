using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    // A mod describes the process of modifying a processed structure or file(s).
    // Pass 1 (Cache): After extracting all files
    // Pass 2 (Mod): After Pass 1 for all files
    // Preload Pass: After extracting all files in the preload process
    public abstract class Mod
    {
        public virtual bool NeedsCachePass { get { return false; } }

        public bool IsBusy { get; set; }
        public int Order { get; set; } // order of execution

        public virtual void CachePass(object value) { } // Pass 1 (Cache): After extracting all files
        public abstract void ModPass(object value); // Pass 2 (Mod): After all files were processed for Cache pass
        public virtual void PreloadPass(object value) { } // Preload pass if the mod acts as a loader/saver for property values/their data (see: custom textures)

        public virtual void Init() { }
        public virtual void BeforeCachePass() { }
        public virtual void AfterCachePass() { }
        public virtual void BeforeModPass() { }
        public virtual void AfterModPass() { }
        public virtual void BeforePreloadPass() { }
        public virtual void AfterPreloadPass() { }

    }
}
