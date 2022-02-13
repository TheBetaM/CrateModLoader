using DiscUtils.Iso9660;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using CrateModLoader.Tools;
using CrateModLoader.Tools.ISO;

namespace CrateModLoader.ModPipelines
{

    public class ConsolePipeline_PS1 : ConsolePipeline
    {

        public string ISO_Label = "";

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.PS1,
            NeedsDetection = true,
            CanBuildROMfromFolder = false, // original file required for label
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\";
        public override bool StreamPipeline => true;

        private string TempBinPath = ModLoaderGlobals.ToolsPath + "binconvout.iso";
        private BackgroundWorker GlobalWorker;
        public bool isBINimage = false;
        private int ExtractIterator = 0;
        private int ExtractFileCount = 0;
        private bool isExtracting = false;
        private string buildInputPath;
        private string buildOutputPath;
        private string extractInputPath;
        private string extractOutputPath;
        private bool UseNewTools = true;

        public ConsolePipeline_PS1()
        {
            ISO_Label = "";
        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            bool found = false;
            titleID = null;
            regionID = 0;

            if (Path.GetExtension(inputPath).ToLower() == ".bin") // PS1 CD image
            {
                isBINimage = true;
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
                                        found = false;
                                        titleID = null;
                                    }
                                    else if (titleID.Contains("BOOT"))
                                    {
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
                            found = (titleID.Contains("BOOT ") || titleID.Contains("BOOT="));
                        }
                    }
                    else
                    {
                        found = false;
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
            titleID = null;
            regionID = 0;
            bool found = false;
            string filePath = inputPath + @"SYSTEM.CNF";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    titleID = sr.ReadLine();
                    found = (titleID.Contains("BOOT ") || titleID.Contains("BOOT="));
                }
            }
            if (!found)
            {
                titleID = null;
            }
            return found;
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker, bool LegacyMethod)
        {

            GlobalWorker = worker;
            buildInputPath = inputPath;
            buildOutputPath = outputPath;
            isExtracting = true;
            if (LegacyMethod)
            {
                UseNewTools = false;
            }

            AsyncWorker = new BackgroundWorker();
            AsyncWorker.WorkerReportsProgress = true;
            AsyncWorker.DoWork += new DoWorkEventHandler(Builder_DoWork);
            AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Builder_RunWorkerCompleted);
            AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(Builder_ProgressChanged);
            AsyncWorker.RunWorkerAsync();

            
        }

        private void Builder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int prog_corrected = (int)((e.ProgressPercentage / 100f) * 25f);
            GlobalWorker.ReportProgress(74 + prog_corrected);
        }
        private void Builder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;
            a.DoWork -= Builder_DoWork;
            a.RunWorkerCompleted -= Builder_RunWorkerCompleted;
            a.ProgressChanged -= Builder_ProgressChanged;

            AsyncWorker = null;
        }
        private void Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;

            Builder_Work();

            while (isExtracting)
            {
                Thread.Sleep(100);
            }
        }
        private async void Builder_Work()
        {
            await Task.Run(
                () =>
                {
                    if (UseNewTools)
                    {
                        // Use mkpsxiso

                        string args = $"-y -q -o \"{buildOutputPath}\" Input.xml";

                        Process BuildProcess = new Process();
                        BuildProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"mkpsxiso.exe";
                        BuildProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        BuildProcess.StartInfo.Arguments = args;
                        BuildProcess.StartInfo.UseShellExecute = false;
                        BuildProcess.StartInfo.RedirectStandardOutput = true;
                        BuildProcess.StartInfo.CreateNoWindow = true;

                        BuildProcess.Start();
                        string outputMessage = BuildProcess.StandardOutput.ReadToEnd();
                        //Console.WriteLine(outputMessage);
                        BuildProcess.WaitForExit();

                        if (File.Exists(ModLoaderGlobals.BaseDirectory + "Input.xml"))
                            File.Delete(ModLoaderGlobals.BaseDirectory + "Input.xml");
                        if (File.Exists(ModLoaderGlobals.BaseDirectory + "mkpsxiso.cue"))
                            File.Delete(ModLoaderGlobals.BaseDirectory + "mkpsxiso.cue");

                    }
                    else
                    {
                        // Use CDBuilder
                        CDBuilder isoBuild = new CDBuilder();
                        isoBuild.UseJoliet = true;
                        isoBuild.VolumeIdentifier = ISO_Label;

                        if (UsingStreamPipeline)
                        {
                            foreach (KeyValuePair<string, MemoryFile> pair in ExtractedFiles)
                            {
                                pair.Value.Stream.Position = 0;
                                isoBuild.AddFile(pair.Key, pair.Value.Stream);
                            }

                            using (FileStream output = new FileStream(buildOutputPath, FileMode.Create, FileAccess.Write))
                            using (Stream input = isoBuild.Build())
                            {
                                /*
                                foreach (MemoryFile file in ExtractedFiles)
                                {
                                    file.Dispose();
                                }
                                ExtractedFiles.Clear();
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                */

                                ISO2PSX.Run(input, output, AsyncWorker);
                            }

                            isoBuild = null;
                        }
                        else
                        {
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
                        }
                    }

                    isExtracting = false;
                }
                );
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

            if (isBINimage && UseNewTools)
            {
                await Task.Run(
                () =>
                {
                    //use dumpsxiso
                    Directory.CreateDirectory(TempPath);
                    string args = $"\"{extractInputPath}\" -x {ModLoaderGlobals.TempName} -s Input.xml";

                    Process ExtractProcess = new Process();
                    ExtractProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"dumpsxiso.exe";
                    ExtractProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    ExtractProcess.StartInfo.Arguments = args;
                    ExtractProcess.StartInfo.UseShellExecute = false;
                    ExtractProcess.StartInfo.RedirectStandardOutput = true;
                    ExtractProcess.StartInfo.CreateNoWindow = true;

                    ExtractProcess.Start();
                    string outputMessage = ExtractProcess.StandardOutput.ReadToEnd();
                    //Console.WriteLine(outputMessage);
                    ExtractProcess.WaitForExit();
                });
            }
            else
            {
                IList<Task> extractTaskList = new List<Task>();
                Dictionary<string, string> Paths = new Dictionary<string, string>();
                FileStream tempbin = null;
                FileStream extract_isoStream = null;
                CDReader extract_reader;
                ExtractFileCount = 0;
                ExtractIterator = 0;

                // Mounting CDReader
                if (isBINimage) // PS1 BIN image
                {
                    using (FileStream isoStream = File.Open(extractInputPath, FileMode.Open))
                    {
                        FileStream binconvout = new FileStream(TempBinPath, FileMode.Create, FileAccess.Write);
                        PSX2ISO.Run(isoStream, binconvout);
                        binconvout.Close();
                        tempbin = new FileStream(TempBinPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    extract_reader = new CDReader(tempbin, true);
                }
                else
                {
                    extract_isoStream = File.Open(extractInputPath, FileMode.Open);
                    extract_reader = new CDReader(extract_isoStream, true);
                }
                ISO_Label = extract_reader.VolumeLabel;

                if (!UsingStreamPipeline && !Directory.Exists(extractOutputPath))
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
                        if (!UsingStreamPipeline)
                        {
                            Directory.CreateDirectory(extractOutputPath + directory);
                        }
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
                if (!isBINimage)
                {
                    extract_isoStream.Dispose();
                    extract_isoStream.Close();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();

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
            }

            isExtracting = false;

        }

        private async Task ISO_ExtractAsync(string file, string path, BackgroundWorker worker)
        {
            string input = extractInputPath;
            if (isBINimage)
                input = TempBinPath;

            // CDReader doesn't work in async, so this is the workaround
            using (FileStream iso = new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
            {
                using (CDReader cd = new CDReader(iso, true))
                {
                    using (Stream fileStreamFrom = cd.OpenFile(file, FileMode.Open))
                    {
                        if (UsingStreamPipeline)
                        {
                            MemoryFile memfile = new MemoryFile();
                            await memfile.FromStreamAsync(fileStreamFrom, file);
                            ExtractedFiles.Add(file, memfile);
                        }
                        else
                        {
                            using (Stream fileStreamTo = File.Open(path, FileMode.OpenOrCreate))
                            {
                                await fileStreamFrom.CopyToAsync(fileStreamTo);
                                //fileStreamFrom.CopyTo(fileStreamTo);
                            }
                        }
                    }
                }
            }

            ExtractIterator++;
        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker, bool LegacyMethod)
        {

            GlobalWorker = worker;
            extractInputPath = inputPath;
            extractOutputPath = outputPath;
            isExtracting = true;
            if (LegacyMethod)
            {
                UseNewTools = false;
            }

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
                if (!UsingStreamPipeline)
                {
                    Directory.CreateDirectory(TempPath + @"\" + directory);
                }
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