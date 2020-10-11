using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CrateModLoader
{
    // Class for Mod Crate plugins/extensions.
    // A mod describes a process of modifying game files.
    public abstract class Mod : IMod
    {
        public bool Hidden { get; set; }
        public BackgroundWorker AsyncWorker { get; set; }
        public List<ModPropertyBase> Props { get; set; }
        public List<ConsoleMode> SupportedConsoles { get; set; }
        public bool IsBusy { get { return AsyncWorker != null && AsyncWorker.IsBusy; } }

        public abstract void BeforeProcess();

        public void StartProcess()
        {
            AsyncWorker = new BackgroundWorker();
            AsyncWorker.WorkerReportsProgress = true;
            AsyncWorker.DoWork += new DoWorkEventHandler(Process);
            AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(ProcessProgressChanged);
            AsyncWorker.RunWorkerAsync();
        }

        public virtual void Process(object sender, DoWorkEventArgs e)
        {
            
        }

        private void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AsyncWorker = null;
            AfterProcess();
        }

        private void ProcessProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        public abstract void AfterProcess();

    }
}
