using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CrateModLoader
{
    interface IMod
    {
        bool Hidden { get; set; }
        BackgroundWorker AsyncWorker { get; set; }
        bool IsBusy { get; }
        List<ModPropertyBase> Props { get; set; }
        List<ConsoleMode> SupportedConsoles { get; set; }

        void BeforeProcess();

        void Process(object sender, DoWorkEventArgs e);

        void AfterProcess();

    }
}
