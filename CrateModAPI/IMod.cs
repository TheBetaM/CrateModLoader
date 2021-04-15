using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CrateModLoader
{
    public interface IMod
    {
        bool Hidden { get; set; }
        bool IsBusy { get; set; }
        bool NeedsCachePass { get; }
        int Order { get; set; }

        void QuickPass();
        void CachePass();
        void ModPass();
    }
}
