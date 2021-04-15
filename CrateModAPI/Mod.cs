using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace CrateModLoader
{
    // A mod describes the process of modifying a processed structure or file(s).
    // Pass 1 (Quick): Right after extracting the file (can not rely on other files or cached data existing)
    // Pass 2 (Cache): After extracting all files
    // Pass 3 (Mod): After Pass 2 for all files
    public abstract class Mod : IMod
    {
        public bool Hidden { get; set; }
        public bool IsBusy { get; set; }
        public bool NeedsCachePass { get; }
        public int Order { get; set; } // order of execution

        public virtual void ProcessFiles(object sender, DoWorkEventArgs e)
        {
            
        }

        public void StartCachePass()
        {

        }

        public void StartModPass()
        {

        }

        public abstract void QuickPass(); // Pass 1 (Quick): Right after extracting the file (can not rely on other files or cached data existing)
        public abstract void CachePass(); // Pass 2 (Cache): After extracting all files
        public abstract void ModPass(); // Pass 3 (Mod): After Pass 2 for all files


    }
}
