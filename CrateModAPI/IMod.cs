using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    // A mod describes the process of modifying a processed structure or file(s).
    // Pass 1 (Cache): After extracting all files
    // Pass 2 (Mod): After Pass 1 for all files
    // Preload Pass: After extracting all files in the preload process
    public interface IMod
    {
        bool NeedsCachePass { get; }

        Modder ExecutionSource { get; set; }
        bool IsBusy { get; set; }
        int Order { get; set; } // order of execution
        bool RandomOverride { get; }

        void CachePass(object value); // Pass 1 (Cache): After extracting all files
        void ModPass(object value); // Pass 2 (Mod): After all files were processed for Cache pass
        void PreloadPass(object value); // Preload pass if the mod acts as a loader/saver for property values/their data (see: custom textures)

        void BeforeCachePass();
        void AfterCachePass();
        void BeforeModPass();
        void AfterModPass();
        void BeforePreloadPass();
        void AfterPreloadPass();

    }
}