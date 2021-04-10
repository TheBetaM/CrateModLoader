using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace CrateModLoader
{
    // A mod describes the process of reading, modifying, and saving a game file (not archives).
    // Pass 1 (Quick): Right after extracting the file (can not rely on other files or cached data existing)
    // Pass 2 (Cache): After extracting all files
    // Pass 3 (Mod): After Pass 2 for all files
    public abstract class Mod : IMod
    {
        public bool Hidden { get; set; }
        public BackgroundWorker AsyncProcess { get; set; }
        public List<BackgroundWorker> AsyncWorkers { get; set; }
        public bool IsBusy
        {
            get
            {
                bool val = AsyncWorkers != null;
                if (!val)
                    return false;
                if (AsyncProcess == null)
                    return false;
                if (AsyncProcess.IsBusy)
                    return true;
                if (AsyncWorkers.Count == 0)
                    return false;
                foreach (BackgroundWorker worker in AsyncWorkers)
                {
                    if (worker.IsBusy)
                        return true;
                }
                return false;
            }
        }
        public bool ProcessIsBusy
        {
            get
            {
                bool val = AsyncWorkers != null;
                if (!val)
                    return false;
                if (AsyncWorkers.Count == 0)
                    return false;
                foreach (BackgroundWorker worker in AsyncWorkers)
                {
                    if (worker.IsBusy)
                        return true;
                }
                return false;
            }
        }
        public bool IsCaching { get; set; }
        public int Order { get; set; } // order of execution

        private List<FileInfo> FileAccessCache;
        private List<FileInfo> FileAccessMod;

        public virtual void ProcessFiles(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker Worker = sender as BackgroundWorker;
            AsyncWorkers = new List<BackgroundWorker>();
            foreach (FileInfo file in FileAccessCache)
            {
                BackgroundWorker AsyncWorker = new BackgroundWorker();
                AsyncWorker.WorkerReportsProgress = true;
                if (IsCaching)
                {
                    AsyncWorker.DoWork += new DoWorkEventHandler(CachePass);
                    AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CachePassCompleted);
                    AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(CachePassProgressChanged);
                }
                else
                {
                    AsyncWorker.DoWork += new DoWorkEventHandler(ModPass);
                    AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ModPassCompleted);
                    AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(ModPassProgressChanged);
                }
                AsyncWorker.RunWorkerAsync(file);
                AsyncWorkers.Add(AsyncWorker);
            }

            int FilesProcessed = 0;
            while (ProcessIsBusy)
            {
                int FileCheck = 0;
                foreach (BackgroundWorker worker in AsyncWorkers)
                {
                    if (!worker.IsBusy)
                        FileCheck++;
                }
                if (FileCheck != FilesProcessed)
                {
                    FilesProcessed = FileCheck;
                    Worker.ReportProgress((int)(((float)FilesProcessed / FileAccessCache.Count) * 100));
                }
                Thread.Sleep(100);
            }
        }

        private void ProcessFilesCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AsyncProcess = null;

            BackgroundWorker Worker = sender as BackgroundWorker;
            Worker.DoWork -= ProcessFiles;
            Worker.RunWorkerCompleted -= ProcessFilesCompleted;
            Worker.ProgressChanged -= ProcessFilesProgressChanged;
        }

        private void ProcessFilesProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        public void StartCache(List<string> paths = null)
        {
            IsCaching = true;
            FileAccessCache = new List<FileInfo>();
            if (paths != null && paths.Count > 0)
            {
                foreach (string path in paths)
                {
                    if (File.Exists(path))
                    {
                        FileAccessCache.Add(new FileInfo(path));
                    }
                }
            }

            if (FileAccessCache.Count > 0)
            {
                AsyncProcess = new BackgroundWorker();
                AsyncProcess.WorkerReportsProgress = true;
                AsyncProcess.DoWork += new DoWorkEventHandler(ProcessFiles);
                AsyncProcess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessFilesCompleted);
                AsyncProcess.ProgressChanged += new ProgressChangedEventHandler(ProcessFilesProgressChanged);
                AsyncProcess.RunWorkerAsync();
            }
        }

        public abstract void CachePass(object sender, DoWorkEventArgs e);
        //{
            //FileInfo file = (FileInfo)e.Argument;
        //}

        private void CachePassCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AsyncWorkers = null;

            BackgroundWorker Worker = sender as BackgroundWorker;
            Worker.DoWork -= CachePass;
            Worker.RunWorkerCompleted -= CachePassCompleted;
            Worker.ProgressChanged -= CachePassProgressChanged;
        }

        private void CachePassProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        public void StartMod(List<string> paths)
        {
            IsCaching = false;
            FileAccessMod = new List<FileInfo>();
            if (paths != null && paths.Count > 0)
            {
                foreach (string path in paths)
                {
                    if (File.Exists(path))
                    {
                        FileAccessMod.Add(new FileInfo(path));
                    }
                }
            }

            if (FileAccessCache.Count > 0)
            {
                AsyncProcess = new BackgroundWorker();
                AsyncProcess.WorkerReportsProgress = true;
                AsyncProcess.DoWork += new DoWorkEventHandler(ProcessFiles);
                AsyncProcess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessFilesCompleted);
                AsyncProcess.ProgressChanged += new ProgressChangedEventHandler(ProcessFilesProgressChanged);
                AsyncProcess.RunWorkerAsync();
            }
        }

        public abstract void ModPass(object sender, DoWorkEventArgs e);
        //{
        //FileInfo file = (FileInfo)e.Argument;
        //}

        private void ModPassCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AsyncWorkers = null;

            BackgroundWorker Worker = sender as BackgroundWorker;
            Worker.DoWork -= ModPass;
            Worker.RunWorkerCompleted -= ModPassCompleted;
            Worker.ProgressChanged -= ModPassProgressChanged;
        }

        private void ModPassProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

    }
}
