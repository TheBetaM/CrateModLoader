using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CrateModLoader
{
    interface IMod
    {
        bool Hidden { get; set; }
        BackgroundWorker AsyncProcess { get; set; }
        List<BackgroundWorker> AsyncWorkers { get; set; }
        bool IsBusy { get; }
        bool IsCaching { get; set; }
        int Order { get; set; }

        void StartCache(List<string> paths);
        void StartMod(List<string> paths);

        void CachePass(object sender, DoWorkEventArgs e);

        void ModPass(object sender, DoWorkEventArgs e);

    }
}
