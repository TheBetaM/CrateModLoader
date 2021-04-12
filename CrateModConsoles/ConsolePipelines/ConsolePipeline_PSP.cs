using DiscUtils.Iso9660;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using CrateModLoader.Tools;

namespace CrateModLoader.ModPipelines
{

    public class ConsolePipeline_PSP : ConsolePipeline
    {

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.PSP,
            NeedsDetection = true,
            CanBuildROMfromFolder = false, // original file required for building
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\PSP_GAME\USRDIR\";

        private BackgroundWorker GlobalWorker;
        private int ExtractIterator = 0;
        private int ExtractFileCount = 0;
        private bool isExtracting = false;
        private string extractInputPath;
        private string extractOutputPath;

        public ConsolePipeline_PSP()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                if (!CDReader.Detect(isoStream))
                {
                    titleID = null;
                    return false;
                }
                else
                {
                    CDReader cd = new CDReader(isoStream, true);
                    titleID = null;
                    bool confirm = false;
                    if (cd.FileExists(@"UMD_DATA.BIN"))
                    {
                        using (StreamReader sr = new StreamReader(cd.OpenFile(@"UMD_DATA.BIN", FileMode.Open)))
                        {
                            titleID = sr.ReadLine().Substring(0, 10);
                        }
                        confirm = true;
                    }
                    cd.Dispose();
                    return confirm;
                }
            }
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            string filePath = inputPath + @"UMD_DATA.BIN";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    titleID = sr.ReadLine().Substring(0, 10);
                    return true;
                }
            }
            titleID = null;
            return false;
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // Use WQSG_UMD (requires input ROM)
            //File.Copy(ModLoaderGlobals.InputPath, ModLoaderGlobals.ToolsPath + "Game.iso");

            string args = "";
            args += @"--iso=";
            args += "\"" + ModLoaderGlobals.BaseDirectory + "/Tools/Game.iso\"";
            args += " --file=\"";
            args += inputPath + "PSP_GAME\"";
            //args += " --log";

            Process ISOcreatorProcess = new Process();
            ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "WQSG_UMD.exe";
            ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ISOcreatorProcess.StartInfo.Arguments = args;
            ISOcreatorProcess.Start();
            ISOcreatorProcess.WaitForExit();

            File.Move(ModLoaderGlobals.ToolsPath + "Game.iso", outputPath);
        }

        private void Extractor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int prog_corrected = (int)((e.ProgressPercentage / 100f) * 25f);
            GlobalWorker.ReportProgress(1 + prog_corrected);
        }
        private void Extractor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;
            a.DoWork -= Extractor_DoWork;
            a.RunWorkerCompleted -= Extractor_RunWorkerCompleted;
            a.ProgressChanged -= Extractor_ProgressChanged;

            AsyncWorker = null;
        }
        private void Extractor_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Extractor_Work(worker);

            int LastIterator = 0;

            while (isExtracting)
            {
                if (ExtractIterator != LastIterator)
                {
                    int p = (int)(((ExtractIterator / (float)ExtractFileCount) * 100f));
                    worker.ReportProgress(p);
                    LastIterator = ExtractIterator;
                }
                Thread.Sleep(100);
            }

        }
        private async void Extractor_Work(BackgroundWorker a)
        {
            //Needed to build
            File.Copy(extractInputPath, ModLoaderGlobals.ToolsPath + "Game.iso");

            IList<Task> extractTaskList = new List<Task>();
            Dictionary<string, string> Paths = new Dictionary<string, string>();
            FileStream extract_isoStream = null;
            CDReader extract_reader;
            ExtractFileCount = 0;
            ExtractIterator = 0;

            // Mounting CDReader
            extract_isoStream = File.Open(extractInputPath, FileMode.Open);
            extract_reader = new CDReader(extract_isoStream, true);

            if (!Directory.Exists(extractOutputPath))
            {
                Directory.CreateDirectory(extractOutputPath);
            }

            // Counting the files
            if (extract_reader.GetDirectories("").Length > 0)
            {
                foreach (string directory in extract_reader.GetDirectories(""))
                {
                    if (extract_reader.GetDirectoryInfo(directory).GetFiles().Length > 0)
                        foreach (string file in extract_reader.GetFiles(directory))
                            ExtractFileCount++;
                    if (extract_reader.GetDirectories(directory).Length > 0)
                    {
                        Recursive_CountFiles(extract_reader, directory, ref ExtractFileCount);
                    }
                }
            }
            if (extract_reader.GetDirectoryInfo("").GetFiles().Length > 0)
            {
                foreach (string file in extract_reader.GetFiles(""))
                {
                    ExtractFileCount++;
                }
            }

            // Scanning file paths, creating directories
            if (extract_reader.GetDirectories("").Length > 0)
            {
                foreach (string directory in extract_reader.GetDirectories(""))
                {
                    Directory.CreateDirectory(extractOutputPath + directory);
                    if (extract_reader.GetDirectoryInfo(directory).GetFiles().Length > 0)
                    {
                        foreach (string file in extract_reader.GetFiles(directory))
                        {
                            string filename = extractOutputPath + file;
                            filename = filename.Replace(";1", string.Empty);
                            Paths.Add(file, filename);
                        }
                    }
                    if (extract_reader.GetDirectories(directory).Length > 0)
                    {
                        Recursive_CreateDirs(extract_reader, directory, ref Paths);
                    }
                }
            }
            if (extract_reader.GetDirectoryInfo("").GetFiles().Length > 0)
            {
                foreach (string file in extract_reader.GetFiles(""))
                {
                    string filename = extractOutputPath + "/" + file;
                    filename = filename.Replace(";1", string.Empty);
                    Paths.Add(file, filename);
                }
            }

            extract_reader.Dispose();
            extract_isoStream.Close();

            // Extracting files
            foreach (KeyValuePair<string, string> Path in Paths)
            {
                extractTaskList.Add(ISO_ExtractAsync(Path.Key, Path.Value, a));
            }

            await Task.WhenAll(extractTaskList);

            isExtracting = false;

        }

        private async Task ISO_ExtractAsync(string file, string path, BackgroundWorker worker)
        {
            Stream fileStreamFrom = null;
            Stream fileStreamTo = null;

            // CDReader doesn't work in async, so this is the workaround
            Stream iso = File.Open(extractInputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            CDReader cd = new CDReader(iso, true);

            fileStreamFrom = cd.OpenFile(file, FileMode.Open);
            fileStreamTo = File.Open(path, FileMode.OpenOrCreate);

            await fileStreamFrom.CopyToAsync(fileStreamTo);
            //fileStreamFrom.CopyTo(fileStreamTo);

            fileStreamFrom.Close();
            fileStreamTo.Close();

            iso.Close();
            cd.Dispose();

            ExtractIterator++;
        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker)
        {

            GlobalWorker = worker;
            extractInputPath = inputPath;
            extractOutputPath = outputPath;
            isExtracting = true;

            AsyncWorker = new BackgroundWorker();
            AsyncWorker.WorkerReportsProgress = true;
            AsyncWorker.DoWork += new DoWorkEventHandler(Extractor_DoWork);
            AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Extractor_RunWorkerCompleted);
            AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(Extractor_ProgressChanged);
            AsyncWorker.RunWorkerAsync();


        }

        private void Recursive_CreateDirs(CDReader cd, string dir, ref Dictionary<string, string> Paths)
        {
            foreach (string directory in cd.GetDirectories(dir))
            {
                Directory.CreateDirectory(TempPath + @"\" + directory);
                if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                {
                    foreach (string file in cd.GetFiles(directory))
                    {
                        string filename = TempPath + @"\" + file;
                        filename = filename.Replace(";1", string.Empty);
                        Paths.Add(file, filename);
                    }
                }
                if (cd.GetDirectories(directory).Length > 0)
                {
                    Recursive_CreateDirs(cd, directory, ref Paths);
                }
            }
        }

        private void Recursive_CountFiles(CDReader cd, string dir, ref int FileCount)
        {
            foreach (string directory in cd.GetDirectories(dir))
            {
                if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                    foreach (string file in cd.GetFiles(directory))
                        FileCount++;
                if (cd.GetDirectories(directory).Length > 0)
                {
                    Recursive_CountFiles(cd, directory, ref FileCount);
                }
            }
        }

    }
}