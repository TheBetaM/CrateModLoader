using DiscUtils.Iso9660;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using CrateModLoader.Tools;
using CrateModLoader.Tools.IO;
using CrateModLoader.Tools.ISO;

namespace CrateModLoader.ModPipelines
{

    public class ConsolePipeline_PS2 : ConsolePipeline
    {

        public string ISO_Label = "";

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.PS2,
            NeedsDetection = true,
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\";

        private string TempBinPath = ModLoaderGlobals.ToolsPath + "binconvout.iso";
        private BackgroundWorker GlobalWorker;
        public bool isCDimage = false;
        private int ExtractIterator = 0;
        private int ExtractFileCount = 0;
        private bool isExtracting = false;
        private string buildInputPath;
        private string buildOutputPath;
        private string extractInputPath;
        private string extractOutputPath;

        public ConsolePipeline_PS2()
        {
            ISO_Label = "";
            isCDimage = false;
        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            bool found = false;
            titleID = null;
            regionID = 0;

            if (Path.GetExtension(inputPath).ToLower() == ".bin") // PS2 CD image
            {
                isCDimage = true;
                using (var fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
                {
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        try
                        {
                            fileStream.Seek(0x9319, SeekOrigin.Begin);
                            string test = new string(reader.ReadChars(0x05));
                            if (test.Contains("CD001"))
                            {
                                // It's a bin image
                                //Console.WriteLine("is bin");
                                reader.ReadUInt16();
                                test = new string(reader.ReadChars(0x0B));
                                if (test.Contains("PLAYSTATION"))
                                {
                                    // It's a PSX or PS2 CD
                                    //Console.WriteLine("is playstation bin");

                                    int totalSectors = (int)(reader.BaseStream.Length / 2352);

                                    fileStream.Seek(0x93B6, SeekOrigin.Begin);
                                    uint rootDirSector = reader.ReadUInt32(); // usually 22 but better to be sure
                                    if (rootDirSector >= totalSectors)
                                        throw new Exception("root dir sector out of bounds");
                                    fileStream.Seek((2352 * rootDirSector) + 0x18, SeekOrigin.Begin);
                                    //Console.WriteLine("root dir sector " + rootDirSector);

                                    // Looking for SYSTEM.CNF now
                                    // Note: This may not work if the root folder listing is larger than one sector

                                    bool foundCNF = false;
                                    byte nameSize = 0;
                                    string entryName = string.Empty;
                                    uint cnfSector = 0;

                                    while (!foundCNF)
                                    {
                                        byte entrySize = reader.ReadByte();
                                        if (entrySize == 0)
                                        {
                                            foundCNF = true;
                                            throw new Exception("SYSTEM.CNF not found");
                                        }
                                        fileStream.Seek(0x1F, SeekOrigin.Current);
                                        nameSize = reader.ReadByte();
                                        if (nameSize > 1)
                                        {
                                            entryName = new string(reader.ReadChars(nameSize));
                                        }
                                        else
                                        {
                                            reader.ReadByte();
                                            entryName = string.Empty;
                                        }

                                        if (entryName.Contains("SYSTEM.CNF"))
                                        {
                                            foundCNF = true;
                                            fileStream.Seek(-(nameSize + 0x1F), SeekOrigin.Current);
                                            cnfSector = reader.ReadUInt32();
                                        }
                                        else
                                        {
                                            if (nameSize % 2 == 0)
                                            {
                                                reader.ReadByte();
                                            }
                                            fileStream.Seek(0xE, SeekOrigin.Current);
                                        }
                                    }

                                    if (cnfSector >= totalSectors)
                                        throw new Exception("cnf sector out of bounds");
                                    fileStream.Seek((2352 * cnfSector) + 0x18, SeekOrigin.Begin);
                                    //Console.WriteLine("cnf sector " + cnfSector);

                                    titleID = new string(reader.ReadChars(0x1D));
                                    //Console.WriteLine("title ID " + titleID);
                                    if (titleID.Contains("BOOT2"))
                                    {
                                        // It's a PS2 CD!
                                        found = true;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            found = false;
                            titleID = null;
                        }
                    }
                }
            }
            else
            {
                // CDReader needs to instantiate from FileStream or else it will not dispose correctly
                using (var isoStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
                {
                    CDReader cd;

                    if (!CDReader.Detect(isoStream))
                    {
                        return false;
                    }
                    else
                    {
                        cd = new CDReader(isoStream, true);
                    }

                    if (cd.FileExists(@"SYSTEM.CNF"))
                    {
                        using (StreamReader sr = new StreamReader(cd.OpenFile(@"SYSTEM.CNF", FileMode.Open)))
                        {
                            titleID = sr.ReadLine();
                            found = titleID.Contains("BOOT2");
                        }
                    }
                    cd.Dispose();
                    cd = null;
                }
            }

            if (!found)
            {
                titleID = null;
            }
            return found;
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            titleID = null;
            bool found = false;
            string filePath = inputPath + @"SYSTEM.CNF";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    titleID = sr.ReadLine();
                    found = titleID.Contains("BOOT2");
                }
            }
            if (!found)
            {
                titleID = null;
            }
            return found;
        }

        private void Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;

            if (isCDimage)
            {
                Builder_CD_Work();

                while (isExtracting)
                {
                    Thread.Sleep(100);
                }
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(buildInputPath);
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.MoveTo(file.FullName);
                    }
                    IO_Common.Recursive_RenameFiles(dir);
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.MoveTo(file.FullName);
                }

                //Use PS2ImageMaker
                PS2ImageMaker.StartPacking(buildInputPath, buildOutputPath);

                int progress;
                while (!Builder_CheckProgress(out progress))
                {
                    a.ReportProgress(progress);
                    Thread.Sleep(100);
                }

                isExtracting = false;
            }
        }

        private async void Builder_CD_Work()
        {
            await Task.Run(
                () =>
                {
                    // Use CDBuilder
                    CDBuilder isoBuild = new CDBuilder();
                    isoBuild.UseJoliet = true;
                    isoBuild.VolumeIdentifier = ISO_Label;

                    // CD image adjustments
                    DirectoryInfo dit = new DirectoryInfo(buildInputPath);
                    foreach (DirectoryInfo dir in dit.EnumerateDirectories())
                    {
                        foreach (FileInfo file in dir.EnumerateFiles())
                        {
                            file.MoveTo(file.FullName);
                        }
                        IO_Common.Recursive_RenameFiles(dir);
                    }
                    foreach (FileInfo file in dit.EnumerateFiles())
                    {
                        file.MoveTo(file.FullName);
                    }

                    DirectoryInfo di = new DirectoryInfo(buildInputPath);
                    HashSet<FileStream> files = new HashSet<FileStream>();

                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        ISO_Common.Recursive_AddDirs(isoBuild, dir, dir.Name + @"\", files, true);
                    }
                    foreach (FileInfo file in di.GetFiles())
                    {
                        ISO_Common.AddFile(isoBuild, file, string.Empty, files, true);
                    }

                    using (FileStream output = new FileStream(buildOutputPath, FileMode.Create, FileAccess.Write))
                    using (Stream input = isoBuild.Build())
                    {
                        ISO2PSX.Run(input, output, AsyncWorker);
                    }

                    isoBuild = null;

                    foreach (FileStream file in files)
                    {
                        file.Close();
                    }
                    files.Clear();

                    isExtracting = false;
                }
                );
        }

        bool Builder_CheckProgress(out int prog)
        {
            var progress = PS2ImageMaker.PollProgress();
            int progPercent = (int)(progress.ProgressPercentage * 100);
            prog = progPercent;
            switch (progress.ProgressS)
            {
                default:
                    return false;
                case PS2ImageMaker.ProgressState.FAILED:
                    Console.WriteLine("Error: PS2 Image Build failed!");
                    return true;
                case PS2ImageMaker.ProgressState.FINISHED:
                    return true;
            }
        }

        private void Builder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;
            a.DoWork -= Builder_DoWork;
            a.RunWorkerCompleted -= Builder_RunWorkerCompleted;
            a.ProgressChanged -= Builder_ProgressChanged;

            AsyncWorker = null;
        }

        private void Builder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int prog_corrected = (int)((e.ProgressPercentage / 100f) * 25f);
            GlobalWorker.ReportProgress(74 + prog_corrected);
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker)
        {
            GlobalWorker = worker;
            buildInputPath = inputPath;
            buildOutputPath = outputPath;
            isExtracting = true;

            AsyncWorker = new BackgroundWorker();
            AsyncWorker.WorkerReportsProgress = true;
            AsyncWorker.DoWork += new DoWorkEventHandler(Builder_DoWork);
            AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Builder_RunWorkerCompleted);
            AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(Builder_ProgressChanged);
            AsyncWorker.RunWorkerAsync();
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

            IList<Task> extractTaskList = new List<Task>();
            Dictionary<string, string> Paths = new Dictionary<string, string>();
            FileStream tempbin = null;
            FileStream extract_isoStream = null;
            CDReader extract_reader;
            ExtractFileCount = 0;
            ExtractIterator = 0;

            // Mounting CDReader
            if (isCDimage) // PS2 CD image
            {
                using (FileStream isoStream = File.Open(extractInputPath, FileMode.Open))
                {
                    FileStream binconvout = new FileStream(TempBinPath, FileMode.Create, FileAccess.Write);
                    PSX2ISO.Run(isoStream, binconvout);
                    binconvout.Close();
                    tempbin = new FileStream(TempBinPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                extract_reader = new CDReader(tempbin, true);
            }
            else
            {
                extract_isoStream = File.Open(extractInputPath, FileMode.Open);
                extract_reader = new CDReader(extract_isoStream, true);
            }
            ISO_Label = extract_reader.VolumeLabel;

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
            if (!isCDimage)
            {
                extract_isoStream.Dispose();
                extract_isoStream.Close();
            }

            // Extracting files
            foreach (KeyValuePair<string, string> Path in Paths)
            {
                extractTaskList.Add(ISO_ExtractAsync(Path.Key, Path.Value, a));
            }

            await Task.WhenAll(extractTaskList);

            if (tempbin != null)
            {
                tempbin.Dispose();
                File.Delete(TempBinPath);
            }
            extractTaskList.Clear();

            isExtracting = false;

        }

        private async Task ISO_ExtractAsync(string file, string path, BackgroundWorker worker)
        {
            string input = extractInputPath;
            if (isCDimage)
                input = TempBinPath;

            // CDReader doesn't work in async, so this is the workaround
            using (FileStream iso = new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
            {
                using (CDReader cd = new CDReader(iso, true))
                {
                    using (Stream fileStreamFrom = cd.OpenFile(file, FileMode.Open))
                    {
                        using (Stream fileStreamTo = File.Open(path, FileMode.OpenOrCreate))
                        {
                            await fileStreamFrom.CopyToAsync(fileStreamTo);
                            //fileStreamFrom.CopyTo(fileStreamTo);
                        }
                    }
                }
            }

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
                        //fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                        //fileStreamTo = File.Open(TempPath + @"\" + file, FileMode.OpenOrCreate);
                        //UpdateExtractProgress(worker, FileCount, ref FileIterator);
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