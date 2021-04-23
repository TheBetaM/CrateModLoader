using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    // A mod describes the process of modifying a processed structure or file(s).
    // Pass 1 (Cache): After extracting all files
    // Pass 2 (Mod): After Pass 2 for all files
    public abstract class Mod : IMod
    {
        // Metadata
        public virtual string Name { get { return string.Empty; } }
        public virtual string Description { get { return string.Empty; } }
        public virtual CreditContributors Contributors { get { return new CreditContributors(); } }
        public virtual string Version { get { return string.Empty; } }
        public virtual int Category { get { return 0; } }
        public virtual List<ConsoleMode> SupportedConosles { get; }
        public virtual List<RegionType> SupportedRegions { get; }
        //public abstract Dictionary<ConsoleMode, RegionType> BlockedSetups { get; } // todo
        public virtual bool Hidden { get { return false; } }
        public virtual bool NeedsCachePass { get { return false; } }

        // Process-relevant stuff
        public bool IsBusy { get; set; }
        public int Order { get; set; } // order of execution

        public virtual void CachePass(object value) { } // Pass 1 (Cache): After extracting all files
        public abstract void ModPass(object value); // Pass 2 (Mod): After all files were processed for Cache pass

        public virtual void Init() { }
        public virtual void BeforeCachePass() { }
        public virtual void AfterCachePass() { }
        public virtual void BeforeModPass() { }
        public virtual void AfterModPass() { }

        public virtual void ProcessFiles()
        {
            
        }

        public void StartCachePass()
        {

        }

        public void StartModPass()
        {

        }

    }
}
