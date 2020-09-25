using DiscUtils.Iso9660;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using CrateModLoader.Resources.Text;
using CrateModLoader.Tools;

namespace CrateModLoader
{

    public class ModLoader
    {

        // UI elements
        public ModLoaderForm main_form;

        // Active settings
        public enum OpenROM_SelectionType
        {
            AutomaticOnly = 1, //PSX/PS2/PSP/GC/WII/XBOX/360
            Any = 2,
        }
        public bool loadedISO = false;
        public bool outputPathSet = false;
        public bool keepTempFiles = false;
        public bool inputDirectoryMode = false;
        public bool outputDirectoryMode = false;
        public bool processActive = false;
        public bool isCDimage = false; //as opposed to DVD image
        public bool asyncBuild = false;
        public Modder Modder;
        public Game Game;
        public ModPipeline Pipeline;
        private Process ISOcreatorProcess;
        public BackgroundWorker asyncWorker;
        public OpenROM_SelectionType OpenROM_Selection = OpenROM_SelectionType.AutomaticOnly;

        public ModLoader()
        {
            CacheSupportedGames();
            CachePipelines();

            asyncWorker = new BackgroundWorker();
            asyncWorker.WorkerReportsProgress = true;
        }

        // Builds the ISO
        void CreateISO()
        {
            asyncBuild = false;

            if (outputDirectoryMode)
            {
                //Directory Mode
                DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.TempPath);
                if (ModLoaderGlobals.Console == ConsoleMode.PS2)
                {
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        foreach (FileInfo file in dir.EnumerateFiles())
                        {
                            file.MoveTo(file.FullName);
                        }
                        Recursive_RenameFiles(dir);
                    }
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.MoveTo(file.FullName);
                    }
                }

                if (!Directory.Exists(ModLoaderGlobals.OutputPath))
                {
                    Directory.CreateDirectory(ModLoaderGlobals.OutputPath);
                }

                string pathparent = ModLoaderGlobals.OutputPath + @"\";
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Directory.CreateDirectory(pathparent + dir.Name);
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.CopyTo(pathparent + dir.Name + @"\" + file.Name);
                    }
                    Recursive_CopyFiles(dir, pathparent + dir.Name + @"\");
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.CopyTo(pathparent + file.Name);
                }
            }
            else if (ModLoaderGlobals.Console == ConsoleMode.PS2 && !isCDimage)
            {
                //Use PS2ImageMaker
                DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.TempPath);
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.MoveTo(file.FullName);
                    }
                    Recursive_RenameFiles(dir);
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.MoveTo(file.FullName);
                }

                asyncBuild = true;
                PS2ImageMaker.StartPacking(ModLoaderGlobals.TempPath, ModLoaderGlobals.OutputPath);
                main_form.StartAsyncTimer();
            }
            else if (ModLoaderGlobals.Console == ConsoleMode.PSP)
            {
                if (inputDirectoryMode)
                {
                    throw new Exception(ModLoaderText.Error_FolderToROMNotSupported);
                }
                // Use WQSG_UMD
                File.Copy(ModLoaderGlobals.InputPath, ModLoaderGlobals.ToolsPath + "Game.iso");

                string args = "";
                args += @"--iso=";
                args += "\"" + AppDomain.CurrentDomain.BaseDirectory + "/Tools/Game.iso\"";
                args += " --file=\"";
                args += ModLoaderGlobals.TempPath + "PSP_GAME\"";
                //args += " --log";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "WQSG_UMD.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();
                ISOcreatorProcess.WaitForExit();

                File.Move(ModLoaderGlobals.ToolsPath + "Game.iso", ModLoaderGlobals.OutputPath);
            }
            else if (ModLoaderGlobals.Console == ConsoleMode.GCN)
            {
                // Use GCIT (Wiims ISO Tool doesn't work for this?)

                Directory.Move(ModLoaderGlobals.TempPath + @"\P-" + ModLoaderGlobals.ProductCode.Substring(0, 4) + @"\files\", ModLoaderGlobals.TempPath + @"\P-" + ModLoaderGlobals.ProductCode.Substring(0, 4) + @"\root\");

                string args = "";
                args += "\"" + ModLoaderGlobals.TempPath + @"\P-" + ModLoaderGlobals.ProductCode.Substring(0, 4) + "\" -q -d ";
                args += "\"" + ModLoaderGlobals.OutputPath + "\" ";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gcit.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                ISOcreatorProcess.StartInfo.Arguments = args;
                //ISOcreatorProcess.StartInfo.UseShellExecute = false;
                //ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                //ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                ISOcreatorProcess.Start();

                //Console.WriteLine(ISOcreatorProcess.StandardOutput.ReadToEnd());

                ISOcreatorProcess.WaitForExit();
            }
            else if (ModLoaderGlobals.Console == ConsoleMode.WII)
            {
                // Use Wiimms ISO Tool
                string args = "copy ";
                args += "\"" + ModLoaderGlobals.TempPath + "\" ";
                if (ModLoaderGlobals.Console == ConsoleMode.GCN)
                {
                    args += "--ciso ";
                }
                args += "\"" + ModLoaderGlobals.OutputPath + "\" ";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wit.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ISOcreatorProcess.StartInfo.Arguments = args;
                //ISOcreatorProcess.StartInfo.UseShellExecute = false;
                //ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                //ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                ISOcreatorProcess.Start();

                //Console.WriteLine(ISOcreatorProcess.StandardOutput.ReadToEnd());

                ISOcreatorProcess.WaitForExit();
            }
            else if (ModLoaderGlobals.Console == ConsoleMode.XBOX)
            {
                //Use extract-xiso
                string args = "-c ";
                args += ModLoaderGlobals.TempPath + " ";
                args += "\"" + ModLoaderGlobals.OutputPath + "\" ";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "extract-xiso.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();

                ISOcreatorProcess.WaitForExit();
            }
            else
            {
                // Otherwise, try CDBuilder
                CDBuilder isoBuild = new CDBuilder();
                isoBuild.UseJoliet = true;
                isoBuild.VolumeIdentifier = ModLoaderGlobals.ISO_Label;

                if (isCDimage)
                {
                    DirectoryInfo dit = new DirectoryInfo(ModLoaderGlobals.TempPath);
                    foreach (DirectoryInfo dir in dit.EnumerateDirectories())
                    {
                        foreach (FileInfo file in dir.EnumerateFiles())
                        {
                            file.MoveTo(file.FullName);
                        }
                        Recursive_RenameFiles(dir);
                    }
                    foreach (FileInfo file in dit.EnumerateFiles())
                    {
                        file.MoveTo(file.FullName);
                    }
                }

                DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.TempPath);
                HashSet<FileStream> files = new HashSet<FileStream>();

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    Recursive_AddDirs(isoBuild, dir, dir.Name + @"\", files);
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    AddFile(isoBuild, file, string.Empty, files);
                }

                if (ModLoaderGlobals.Console == ConsoleMode.PS1 || ModLoaderGlobals.Console == ConsoleMode.PS2)
                {
                    using (FileStream output = new FileStream(ModLoaderGlobals.OutputPath, FileMode.Create, FileAccess.Write))
                    using (Stream input = isoBuild.Build())
                    {
                        ISO2PSX.Run(input, output);
                    }
                }
                else
                {
                    isoBuild.Build(ModLoaderGlobals.OutputPath);
                }

                foreach (FileStream file in files)
                {
                    file.Close();
                }
            }
        }

        void Recursive_RenameFiles(DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    file.MoveTo(file.FullName.Substring(0, file.FullName.Length - 2));
                }
                Recursive_RenameFiles(dir);
            }
        }

        void Recursive_AddDirs(CDBuilder isoBuild, DirectoryInfo di, string sName, HashSet<FileStream> files)
        {
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                isoBuild.AddDirectory(sName + dir.Name);
                Recursive_AddDirs(isoBuild, dir, sName + dir.Name + @"\", files);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                AddFile(isoBuild, file, sName, files);
            }
        }

        void Recursive_CopyFiles(DirectoryInfo di, string pathparent)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Directory.CreateDirectory(pathparent + dir.Name);
                string pathchild = pathparent + dir.Name + @"\";
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    file.CopyTo(pathchild + file.Name);
                }
                Recursive_CopyFiles(dir, pathchild);
            }
        }

        void AddFile(CDBuilder isoBuild, FileInfo file, string sName, HashSet<FileStream> files)
        {
            var fstream = file.Open(FileMode.Open);
            if (ModLoaderGlobals.Console == ConsoleMode.PS1 || ModLoaderGlobals.Console == ConsoleMode.PS2)
            {
                isoBuild.AddFile(sName + file.Name + ";1", fstream);
            }
            else
            {
                isoBuild.AddFile(sName + file.Name, fstream);
            }
            files.Add(fstream);
        }

        void LoadISO()
        {

            isCDimage = false;
            if (Directory.Exists(ModLoaderGlobals.TempPath))
            {
                DeleteTempFiles();
            }

            if (inputDirectoryMode && !outputDirectoryMode)
            {
                // To fix: PS1, PSP requires the original file; GCN: Incorrect paths because of product code folder
                if (ModLoaderGlobals.Console == ConsoleMode.PS1 || ModLoaderGlobals.Console == ConsoleMode.PSP || ModLoaderGlobals.Console == ConsoleMode.GCN)
                {
                    throw new Exception(ModLoaderText.Error_FolderToROMNotSupported);
                }
            }
            if (outputDirectoryMode)
            {
                // To fix: Incorrect paths because of product code folder
                if (ModLoaderGlobals.Console == ConsoleMode.GCN)
                {
                    throw new Exception(ModLoaderText.Error_GC_FolderNotSupported);
                }
            }
            if (ModLoaderGlobals.Console == ConsoleMode.XBOX360 && !outputDirectoryMode)
            {
                throw new Exception(ModLoaderText.Error_360_ROMNotSupported);
            }
            if (ModLoaderGlobals.Console == ConsoleMode.PC)
            {
                if (!inputDirectoryMode || !outputDirectoryMode)
                {
                    throw new Exception(ModLoaderText.Error_PC_FolderOnly);
                }
            }

            if (inputDirectoryMode)
            {
                DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.InputPath);
                if (!di.Exists)
                {
                    throw new IOException(ModLoaderText.Error_FolderNotAccessible);
                }

                Directory.CreateDirectory(ModLoaderGlobals.TempPath);

                asyncWorker.ReportProgress(25);

                string pathparent = ModLoaderGlobals.TempPath + @"\";
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Directory.CreateDirectory(pathparent + dir.Name);
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.CopyTo(pathparent + dir.Name + @"\" + file.Name);
                    }
                    Recursive_CopyFiles(dir, pathparent + dir.Name + @"\");
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.CopyTo(pathparent + file.Name);
                }
            }
            else if (ModLoaderGlobals.Console == ConsoleMode.GCN || ModLoaderGlobals.Console == ConsoleMode.WII)
            {
                // TODO: add free space checks

                string args = "extract ";
                args += "\"" + ModLoaderGlobals.InputPath + "\" ";
                args += "\"" + ModLoaderGlobals.TempPath + "\" ";

                asyncWorker.ReportProgress(25);

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wit.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();
                ISOcreatorProcess.WaitForExit();
            }
            else if (ModLoaderGlobals.Console == ConsoleMode.XBOX || ModLoaderGlobals.Console == ConsoleMode.XBOX360)
            {
                // TODO: add free space checks
                string args = "-x ";
                args += "\"" + ModLoaderGlobals.InputPath + "\" ";

                asyncWorker.ReportProgress(25);

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "extract-xiso.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();
                ISOcreatorProcess.WaitForExit();

                Directory.Move(AppDomain.CurrentDomain.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(ModLoaderGlobals.InputPath), ModLoaderGlobals.TempPath);
            }
            else
            {
                using (FileStream isoStream = File.Open(ModLoaderGlobals.InputPath, FileMode.Open))
                {
                    FileInfo isoInfo = new FileInfo(ModLoaderGlobals.InputPath);
                    CDReader cd;
                    FileStream tempbin = null;
                    if (Path.GetExtension(ModLoaderGlobals.InputPath).ToLower() == ".bin") // PS1/PS2 CD image
                    {
                        FileStream binconvout = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Create, FileAccess.Write);
                        PSX2ISO.Run(isoStream, binconvout);
                        binconvout.Close();
                        tempbin = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Open, FileAccess.Read);
                        cd = new CDReader(tempbin, true);
                        isCDimage = true;
                    }
                    else
                        cd = new CDReader(isoStream, true);
                    ModLoaderGlobals.ISO_Label = cd.VolumeLabel;

                    /* Sometimes doesn't work?
                    if (isoInfo.Length * 2 > GetTotalFreeSpace(ModLoaderGlobals.TempPath.Substring(0, 3)))
                    {
                        cd.Dispose();
                        throw new IOException("Extraction error: Not enough hard drive space where this program is!");
                    }
                    if (isoInfo.Length * 2 > GetTotalFreeSpace(ModLoaderGlobals.OutputPath.Substring(0, 3)))
                    {
                        cd.Dispose();
                        throw new IOException("Extraction error: Not enough hard drive space in the output path!");
                    }
                    */

                    asyncWorker.ReportProgress(25);
                    //fileStream = cd.OpenFile(@"SYSTEM.CNF", FileMode.Open);

                    if (!Directory.Exists(ModLoaderGlobals.TempPath))
                    {
                        Directory.CreateDirectory(ModLoaderGlobals.TempPath);
                    }

                    //Extracting ISO
                    Stream fileStreamFrom = null;
                    Stream fileStreamTo = null;
                    if (cd.GetDirectories("").Length > 0)
                    {
                        foreach (string directory in cd.GetDirectories(""))
                        {
                            Directory.CreateDirectory(ModLoaderGlobals.TempPath + directory);
                            if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                            {
                                foreach (string file in cd.GetFiles(directory))
                                {
                                    fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                                    string filename = ModLoaderGlobals.TempPath + file;
                                    filename = filename.Replace(";1", string.Empty);
                                    fileStreamTo = File.Open(filename, FileMode.OpenOrCreate);
                                    fileStreamFrom.CopyTo(fileStreamTo);
                                    fileStreamFrom.Close();
                                    fileStreamTo.Close();
                                }
                            }
                            if (cd.GetDirectories(directory).Length > 0)
                            {
                                Recursive_CreateDirs(cd, directory, fileStreamFrom, fileStreamTo);
                            }
                        }
                    }
                    if (cd.GetDirectoryInfo("").GetFiles().Length > 0)
                    {
                        foreach (string file in cd.GetFiles(""))
                        {
                            fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                            string filename = ModLoaderGlobals.TempPath + "/" + file;
                            filename = filename.Replace(";1", string.Empty);
                            fileStreamTo = File.Open(filename, FileMode.OpenOrCreate);
                            fileStreamFrom.CopyTo(fileStreamTo);
                            fileStreamFrom.Close();
                            fileStreamTo.Close();
                        }
                    }

                    cd.Dispose();

                    if (tempbin != null)
                    {
                        tempbin.Dispose();
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso");
                    }
                }
            }
        }

        private void Recursive_CreateDirs(CDReader cd, string dir, Stream fileStreamFrom, Stream fileStreamTo)
        {
            foreach (string directory in cd.GetDirectories(dir))
            {
                Directory.CreateDirectory(ModLoaderGlobals.TempPath + @"\" + directory);
                if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                {
                    foreach (string file in cd.GetFiles(directory))
                    {
                        fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                        fileStreamTo = File.Open(ModLoaderGlobals.TempPath + @"\" + file, FileMode.OpenOrCreate);
                        fileStreamFrom.CopyTo(fileStreamTo);
                        fileStreamFrom.Close();
                        fileStreamTo.Close();
                    }
                }
                if (cd.GetDirectories(directory).Length > 0)
                {
                    Recursive_CreateDirs(cd, directory, fileStreamFrom, fileStreamTo);
                }
            }
        }

        private long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }

        public void StartButtonPressed()
        {
            if (!asyncWorker.IsBusy)
            {
                asyncWorker.DoWork += new DoWorkEventHandler(asyncWorker_DoWork);
                asyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(asyncWorker_RunWorkerCompleted);
                asyncWorker.ProgressChanged += new ProgressChangedEventHandler(asyncWorker_ProgressChanged);
                asyncWorker.RunWorkerAsync();
            }
        }

        private void asyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;

            a.ReportProgress(0);

            LoadISO();

            a.ReportProgress(50);

            EditGameContent();

            a.ReportProgress(75);

            CreateISO();

            if (asyncBuild)
            {
                while (asyncBuild)
                {
                    //Application.DoEvents();
                }
            }

            a.ReportProgress(90);
            if (!keepTempFiles)
            {
                DeleteTempFiles();
            }

        }

        public void Async_BuildFinished()
        {
            asyncBuild = false;
        }

        public void EditGameContent()
        {
            //To make sure the seed matches
            ModLoaderGlobals.RandomizerSeed = ModLoaderGlobals.RandomizerSeedBase;

            if (ModCrates.ModsActiveAmount > 0 && (Modder == null || !Modder.ModCratesManualInstall))
            {
                ModCrates.InstallLayerMods(ModLoaderGlobals.ExtractedPath, 0);
            }
            if (Modder != null)
            {
                if (ModCrates.ModsActiveAmount > 0)
                {
                    Modder.InstallCrateSettings();
                }
                Modder.StartModProcess();
            }
        }

        void DeleteTempFiles()
        {
            if (Directory.Exists(ModLoaderGlobals.TempPath))
            {
                DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.TempPath);

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch (IOException)
                    {
                        //Console.WriteLine("dir:" + dir.FullName);
                        //Console.WriteLine("dirname:" + dir.Name);
                    }
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                try
                {
                    di.Delete(true);
                }
                catch (IOException)
                {
                    //Console.WriteLine("di:" + di.FullName);
                    //Console.WriteLine("dirame:" + di.Name);
                }
            }
        }

        private void asyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            main_form.UpdateProcessProgress(100);
            main_form.Notify_ProcessFinished();
            if (e.Error != null)
            {
                main_form.ResetProcessProgress();
                main_form.UpdateProcessText(ModLoaderText.Process_Error + " " + e.Error.Message);
            }
            else if (!e.Cancelled)
            {
                main_form.UpdateProcessText(ModLoaderText.Process_Finished);
            }
            else
            {
                main_form.ResetProcessProgress();
                main_form.UpdateProcessText(ModLoaderText.Process_Cancelled);
            }
            EnableInteraction();
            asyncWorker.DoWork -= asyncWorker_DoWork;
            asyncWorker.RunWorkerCompleted -= asyncWorker_RunWorkerCompleted;
            asyncWorker.ProgressChanged -= asyncWorker_ProgressChanged;
        }

        private void asyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            main_form.UpdateProcessProgress(e.ProgressPercentage);
            if (e.ProgressPercentage == 0)
            {
                main_form.UpdateProcessText(ModLoaderText.Process_Step0);
            }
            else if (e.ProgressPercentage == 25)
            {
                if (inputDirectoryMode)
                {
                    main_form.UpdateProcessText(ModLoaderText.Process_Step1_Folder);
                }
                else
                {
                    main_form.UpdateProcessText(ModLoaderText.Process_Step1_ROM);
                }
            }
            else if (e.ProgressPercentage == 50)
            {
                main_form.UpdateProcessText(ModLoaderText.Process_Step2);
            }
            else if (e.ProgressPercentage == 75)
            {
                if (outputDirectoryMode)
                {
                    main_form.UpdateProcessText(ModLoaderText.Process_Step3_Folder);
                }
                else
                {
                    main_form.UpdateProcessText(ModLoaderText.Process_Step3_ROM);
                }
            }
            else if (e.ProgressPercentage == 90)
            {
                main_form.UpdateProcessText("Removing temporary files...");
            }
        }

        public void CheckISO()
        {
            Modder = null;
            ModCrates.ClearModLists();
            bool ConsoleDetected = false;

            if (inputDirectoryMode)
            {
                try
                {
                    ModLoaderGlobals.Console = ConsoleMode.Undefined;

                    if (File.Exists(ModLoaderGlobals.InputPath + @"SYSTEM.CNF"))
                    {
                        using (StreamReader sr = new StreamReader(ModLoaderGlobals.InputPath + @"SYSTEM.CNF"))
                        {
                            string titleID = sr.ReadLine();
                            if (titleID.Contains("BOOT2"))
                            {
                                SetGameType(titleID, ConsoleMode.PS2);
                                if (Modder != null)
                                {
                                    foreach (var rc in Game.RegionID[ConsoleMode.PS2])
                                        if (rc.Region == ModLoaderGlobals.Region)
                                            ModLoaderGlobals.ProductCode = rc.CodeName;
                                }
                            }
                            else
                            {
                                SetGameType(titleID, ConsoleMode.PS1);
                                if (Modder != null)
                                {
                                    foreach (var rc in Game.RegionID[ConsoleMode.PS1])
                                        if (rc.Region == ModLoaderGlobals.Region)
                                            ModLoaderGlobals.ProductCode = rc.CodeName;
                                }
                            }
                            ConsoleDetected = true;
                        }
                    }
                    else if (File.Exists(ModLoaderGlobals.InputPath + @"UMD_DATA.BIN"))
                    {
                        using (StreamReader sr = new StreamReader(ModLoaderGlobals.InputPath + @"UMD_DATA.BIN"))
                        {
                            string titleID = sr.ReadLine().Substring(0, 10);
                            SetGameType(titleID, ConsoleMode.PSP);
                            if (Modder != null)
                            {
                                ModLoaderGlobals.ProductCode = titleID;
                            }
                            ConsoleDetected = true;
                        }
                    }
                    else if (File.Exists(ModLoaderGlobals.InputPath + @"default.xbe"))
                    {
                        ModLoaderGlobals.Console = ConsoleMode.XBOX;
                        //Based on OpenXDK
                        using (FileStream fileStream = new FileStream(ModLoaderGlobals.InputPath + @"default.xbe", FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            fileStream.Seek(0x0118, SeekOrigin.Begin);
                            BinaryReader reader = new BinaryReader(fileStream);
                            uint CertOffset = reader.ReadUInt16();
                            fileStream.Seek(CertOffset, SeekOrigin.Begin);
                            fileStream.Seek(CertOffset + 0x0008, SeekOrigin.Begin);
                            uint CertID = reader.ReadUInt32();
                            fileStream.Seek(CertOffset + 0x000C, SeekOrigin.Begin);
                            byte[] CertNameUnicode = new byte[2];
                            string TitleName = "";
                            for (int i = 0; i < 40; i++)
                            {
                                CertNameUnicode[0] = reader.ReadByte();
                                CertNameUnicode[1] = reader.ReadByte();
                                TitleName += System.Text.Encoding.Unicode.GetString(CertNameUnicode);
                            }
                            fileStream.Seek(CertOffset + 0x00A0, SeekOrigin.Begin);
                            uint CertRegion = reader.ReadUInt32();
                            fileStream.Seek(CertOffset + 0x00AC, SeekOrigin.Begin);
                            uint CertVersion = reader.ReadUInt32();

                            /*
                            Console.WriteLine("Cert offset: " + CertOffset.ToString("X"));
                            Console.WriteLine("Cert Title ID: " + CertID);
                            Console.WriteLine("Cert Region: " + CertRegion);
                            Console.WriteLine("Cert Version: " + CertVersion);
                            Console.WriteLine("Cert Name: " + TitleName);
                            */

                            SetGameType(TitleName, ConsoleMode.XBOX, CertRegion);
                            ConsoleDetected = true;
                        }
                    }
                    else if (File.Exists(ModLoaderGlobals.InputPath + @"default.xex"))
                    {
                        ModLoaderGlobals.Console = ConsoleMode.XBOX360;

                        string args = "-l ";
                        args += "\"" + ModLoaderGlobals.InputPath + @"default.xex" + "\"";

                        ISOcreatorProcess = new Process();
                        ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "xextool.exe";
                        //ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        ISOcreatorProcess.StartInfo.Arguments = args;
                        ISOcreatorProcess.StartInfo.UseShellExecute = false;
                        ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                        ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                        ISOcreatorProcess.Start();

                        string outputMessage = ISOcreatorProcess.StandardOutput.ReadToEnd();
                        //Console.WriteLine(outputMessage);

                        ISOcreatorProcess.WaitForExit();

                        if (outputMessage.Length > 200)
                        {
                            string[] outputLines = outputMessage.Split('\n');
                            string titleID = "";

                            for (int i = 0; i < outputLines.Length; i++)
                            {
                                if (outputLines[i].Contains("Xex Name: "))
                                {
                                    titleID = outputLines[i];
                                    break;
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(titleID))
                            {
                                SetGameType(titleID, ConsoleMode.XBOX360);
                                if (Modder != null)
                                {
                                    ModLoaderGlobals.ProductCode = titleID;
                                }
                                ConsoleDetected = true;
                            }
                        }
                    }
                    else if (File.Exists(ModLoaderGlobals.InputPath + @"sys/main.dol") && File.Exists(ModLoaderGlobals.InputPath + @"sys/boot.bin"))
                    {
                        ModLoaderGlobals.Console = ConsoleMode.GCN;

                        using (StreamReader sr = new StreamReader(ModLoaderGlobals.InputPath + @"sys/boot.bin"))
                        {
                            string titleID = sr.ReadLine().Substring(0, 6);

                            if (titleID.StartsWith("G"))
                            {
                                SetGameType(titleID, ConsoleMode.GCN);
                                if (Modder != null)
                                {
                                    ModLoaderGlobals.ProductCode = titleID;
                                }
                            }
                            else
                            {
                                SetGameType(titleID, ConsoleMode.WII);
                                if (Modder != null)
                                {
                                    ModLoaderGlobals.ProductCode = titleID;
                                }
                            }
                            ConsoleDetected = true;
                        }
                    }
                    else if (Directory.GetFiles(ModLoaderGlobals.InputPath, "*.exe").Length > 0 || Directory.GetFiles(ModLoaderGlobals.InputPath, "*.EXE").Length > 0)
                    {
                        ModLoaderGlobals.Console = ConsoleMode.PC;

                        string[] ExeFiles;
                        if (Modder == null && Directory.GetFiles(ModLoaderGlobals.InputPath, "*.exe").Length > 0)
                        {
                            ExeFiles = Directory.GetFiles(ModLoaderGlobals.InputPath, "*.exe");
                            for (int i = 0; i < ExeFiles.Length; i++)
                            {
                                if (Modder == null)
                                {
                                    SetGameType(ExeFiles[i], ConsoleMode.PC);
                                    if (Modder != null)
                                    {
                                        ModLoaderGlobals.ProductCode = ExeFiles[i];
                                    }
                                    ConsoleDetected = true;
                                }
                            }
                        }
                        else if (Modder == null && Directory.GetFiles(ModLoaderGlobals.InputPath, "*.EXE").Length > 0)
                        {
                            ExeFiles = Directory.GetFiles(ModLoaderGlobals.InputPath, "*.EXE");
                            for (int i = 0; i < ExeFiles.Length; i++)
                            {
                                if (Modder == null)
                                {
                                    SetGameType(ExeFiles[i], ConsoleMode.PC);
                                    if (Modder != null)
                                    {
                                        ModLoaderGlobals.ProductCode = ExeFiles[i];
                                    }
                                    ConsoleDetected = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        Modder = null;
                        ModCrates.ClearModLists();
                    }
                    ModLoaderGlobals.ISO_Label = ModLoaderGlobals.ProductCode;
                }
                catch
                {
                    main_form.UpdateGameTitleText(ModLoaderText.Error_UnableToOpenGameFolder);
                    loadedISO = false;
                    ResetGameSpecific(false, true);
                    return;
                }
            }
            else
            {
                if (OpenROM_Selection == OpenROM_SelectionType.AutomaticOnly || OpenROM_Selection == OpenROM_SelectionType.Any)
                {
                    // Gamecube/Wii ROMs

                    string args = "ID6 ";
                    args += "\"" + ModLoaderGlobals.InputPath + "\"";

                    ISOcreatorProcess = new Process();
                    ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wit.exe";
                    //ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    ISOcreatorProcess.StartInfo.Arguments = args;
                    ISOcreatorProcess.StartInfo.UseShellExecute = false;
                    ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                    ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                    ISOcreatorProcess.Start();

                    string outputMessage = ISOcreatorProcess.StandardOutput.ReadToEnd();
                    //Console.WriteLine(outputMessage);

                    ISOcreatorProcess.WaitForExit();

                    if (outputMessage.Length > 0 && outputMessage.Length <= 8)
                    {
                        string titleID = outputMessage.Substring(0, 6);

                        if (!string.IsNullOrWhiteSpace(titleID))
                        {
                            if (titleID.StartsWith("G"))
                            {
                                SetGameType(titleID, ConsoleMode.GCN);
                                if (Modder != null)
                                {
                                    ModLoaderGlobals.ProductCode = titleID;
                                }
                            }
                            else
                            {
                                SetGameType(titleID, ConsoleMode.WII);
                                if (Modder != null)
                                {
                                    ModLoaderGlobals.ProductCode = titleID;
                                }
                            }
                            ConsoleDetected = true;
                        }
                    }
                }
                if (!ConsoleDetected && Modder == null && (OpenROM_Selection == OpenROM_SelectionType.AutomaticOnly || OpenROM_Selection == OpenROM_SelectionType.Any))
                {

                    DeleteTempFiles();

                    string args = "-i -x ";
                    args += "\"" + ModLoaderGlobals.InputPath + "\"";

                    //Modified extract-xiso only extracts the executables to check

                    ISOcreatorProcess = new Process();
                    ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "extract-xiso.exe";
                    ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    ISOcreatorProcess.StartInfo.Arguments = args;
                    //ISOcreatorProcess.Start();

                    ISOcreatorProcess.StartInfo.UseShellExecute = false;
                    ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                    ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                    ISOcreatorProcess.Start();
                    string outputMessage = ISOcreatorProcess.StandardOutput.ReadToEnd();
                    Console.WriteLine(outputMessage);

                    ISOcreatorProcess.WaitForExit();

                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(ModLoaderGlobals.InputPath)))
                    {
                        Directory.Move(AppDomain.CurrentDomain.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(ModLoaderGlobals.InputPath), ModLoaderGlobals.TempPath);
                    }

                    if (Directory.Exists(ModLoaderGlobals.TempPath) && File.Exists(ModLoaderGlobals.TempPath + @"default.xbe"))
                    {
                        ModLoaderGlobals.Console = ConsoleMode.XBOX;
                        //Based on OpenXDK
                        using (FileStream fileStream = new FileStream(ModLoaderGlobals.TempPath + @"default.xbe", FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            fileStream.Seek(0x0118, SeekOrigin.Begin);
                            BinaryReader reader = new BinaryReader(fileStream);
                            uint CertOffset = reader.ReadUInt16();
                            fileStream.Seek(CertOffset, SeekOrigin.Begin);
                            fileStream.Seek(CertOffset + 0x0008, SeekOrigin.Begin);
                            uint CertID = reader.ReadUInt32();
                            fileStream.Seek(CertOffset + 0x000C, SeekOrigin.Begin);
                            byte[] CertNameUnicode = new byte[2];
                            string TitleName = "";
                            for (int i = 0; i < 40; i++)
                            {
                                CertNameUnicode[0] = reader.ReadByte();
                                CertNameUnicode[1] = reader.ReadByte();
                                TitleName += System.Text.Encoding.Unicode.GetString(CertNameUnicode);
                            }
                            fileStream.Seek(CertOffset + 0x00A0, SeekOrigin.Begin);
                            uint CertRegion = reader.ReadUInt32();
                            fileStream.Seek(CertOffset + 0x00AC, SeekOrigin.Begin);
                            uint CertVersion = reader.ReadUInt32();

                            /*
                            Console.WriteLine("Cert offset: " + CertOffset.ToString("X"));
                            Console.WriteLine("Cert Title ID: " + CertID);
                            Console.WriteLine("Cert Region: " + CertRegion);
                            Console.WriteLine("Cert Version: " + CertVersion);
                            Console.WriteLine("Cert Name: " + TitleName);
                            */

                            SetGameType(TitleName, ConsoleMode.XBOX, CertRegion);
                            ConsoleDetected = true;
                        }
                    }
                    else if (Directory.Exists(ModLoaderGlobals.TempPath) && File.Exists(ModLoaderGlobals.TempPath + @"default.xex"))
                    {
                        ModLoaderGlobals.Console = ConsoleMode.XBOX360;

                        string xargs = "-l ";
                        xargs += "\"" + ModLoaderGlobals.TempPath + @"default.xex" + "\"";

                        ISOcreatorProcess = new Process();
                        ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "xextool.exe";
                        //ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        ISOcreatorProcess.StartInfo.Arguments = xargs;
                        ISOcreatorProcess.StartInfo.UseShellExecute = false;
                        ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                        ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                        ISOcreatorProcess.Start();

                        string xoutputMessage = ISOcreatorProcess.StandardOutput.ReadToEnd();
                        //Console.WriteLine(outputMessage);

                        ISOcreatorProcess.WaitForExit();

                        if (xoutputMessage.Length > 200)
                        {
                            string[] outputLines = xoutputMessage.Split('\n');
                            string titleID = "";

                            for (int i = 0; i < outputLines.Length; i++)
                            {
                                if (outputLines[i].Contains("Xex Name: "))
                                {
                                    titleID = outputLines[i];
                                    break;
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(titleID))
                            {
                                SetGameType(titleID, ConsoleMode.XBOX360);
                                if (Modder != null)
                                {
                                    ModLoaderGlobals.ProductCode = titleID;
                                }
                                ConsoleDetected = true;
                            }
                        }
                    }
                    else
                    {
                        main_form.UpdateProcessText(ModLoaderText.Step1Text);
                    }

                    DeleteTempFiles();
                }
                if (!ConsoleDetected && Modder == null && (OpenROM_Selection == OpenROM_SelectionType.AutomaticOnly || OpenROM_Selection == OpenROM_SelectionType.Any))
                {
                    try
                    {
                        using (FileStream isoStream = File.Open(ModLoaderGlobals.InputPath, FileMode.Open))
                        {
                            CDReader cd;
                            FileStream tempbin = null;
                            ModLoaderGlobals.Console = ConsoleMode.Undefined;

                            if (Path.GetExtension(ModLoaderGlobals.InputPath).ToLower() == ".bin") // PS1/PS2 CD image
                            {
                                FileStream binconvout = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Create, FileAccess.Write);
                                PSX2ISO.Run(isoStream, binconvout);
                                binconvout.Close();
                                tempbin = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Open, FileAccess.Read);
                                cd = new CDReader(tempbin, true);
                            }
                            else if (!CDReader.Detect(isoStream))
                            {
                                main_form.UpdateGameTitleText(ModLoaderText.Error_UnknownAutoGameROM);
                                loadedISO = false;
                                main_form.SetProcessStartAllowed(false);
                                main_form.UpdateProcessText(ModLoaderText.Step1Text);
                                ResetGameSpecific(false, true);

                                return;
                            }
                            else
                            {
                                cd = new CDReader(isoStream, true);
                            }
                            if (cd.FileExists(@"SYSTEM.CNF"))
                            {
                                using (StreamReader sr = new StreamReader(cd.OpenFile(@"SYSTEM.CNF", FileMode.Open)))
                                {
                                    string titleID = sr.ReadLine();
                                    if (titleID.Contains("BOOT2"))
                                    {
                                        SetGameType(titleID, ConsoleMode.PS2);
                                        if (Modder != null)
                                        {
                                            foreach (var rc in Game.RegionID[ConsoleMode.PS2])
                                                if (rc.Region == ModLoaderGlobals.Region)
                                                    ModLoaderGlobals.ProductCode = rc.CodeName;
                                        }
                                    }
                                    else
                                    {
                                        SetGameType(titleID, ConsoleMode.PS1);
                                        if (Modder != null)
                                        {
                                            foreach (var rc in Game.RegionID[ConsoleMode.PS1])
                                                if (rc.Region == ModLoaderGlobals.Region)
                                                    ModLoaderGlobals.ProductCode = rc.CodeName;
                                        }
                                    }
                                    ConsoleDetected = true;
                                }
                            }
                            else if (cd.FileExists(@"UMD_DATA.BIN"))
                            {
                                using (StreamReader sr = new StreamReader(cd.OpenFile(@"UMD_DATA.BIN", FileMode.Open)))
                                {
                                    string titleID = sr.ReadLine().Substring(0, 10);
                                    SetGameType(titleID, ConsoleMode.PSP);
                                    if (Modder != null)
                                    {
                                        ModLoaderGlobals.ProductCode = titleID;
                                    }
                                    ConsoleDetected = true;
                                }
                            }
                            else if (cd.FileExists(@"default.xbe"))
                            {
                                ModLoaderGlobals.Console = ConsoleMode.XBOX;
                                //Based on OpenXDK
                                using (FileStream fileStream = new FileStream(@"default.xbe", FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    fileStream.Seek(0x0118, SeekOrigin.Begin);
                                    BinaryReader reader = new BinaryReader(fileStream);
                                    uint CertOffset = reader.ReadUInt16();
                                    fileStream.Seek(CertOffset, SeekOrigin.Begin);
                                    fileStream.Seek(CertOffset + 0x0008, SeekOrigin.Begin);
                                    uint CertID = reader.ReadUInt32();
                                    fileStream.Seek(CertOffset + 0x000C, SeekOrigin.Begin);
                                    byte[] CertNameUnicode = new byte[2];
                                    string TitleName = "";
                                    for (int i = 0; i < 40; i++)
                                    {
                                        CertNameUnicode[0] = reader.ReadByte();
                                        CertNameUnicode[1] = reader.ReadByte();
                                        TitleName += System.Text.Encoding.Unicode.GetString(CertNameUnicode);
                                    }
                                    fileStream.Seek(CertOffset + 0x00A0, SeekOrigin.Begin);
                                    uint CertRegion = reader.ReadUInt32();
                                    fileStream.Seek(CertOffset + 0x00AC, SeekOrigin.Begin);
                                    uint CertVersion = reader.ReadUInt32();

                                    /*
                                    Console.WriteLine("Cert offset: " + CertOffset.ToString("X"));
                                    Console.WriteLine("Cert Title ID: " + CertID);
                                    Console.WriteLine("Cert Region: " + CertRegion);
                                    Console.WriteLine("Cert Version: " + CertVersion);
                                    Console.WriteLine("Cert Name: " + TitleName);
                                    */

                                    SetGameType(TitleName, ConsoleMode.XBOX, CertRegion);
                                    ConsoleDetected = true;
                                }
                            }
                            else
                            {
                                Modder = null;
                                ModCrates.ClearModLists();
                            }

                            cd.Dispose();

                            if (tempbin != null)
                            {
                                tempbin.Dispose();
                                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso");
                            }
                        }
                    }
                    catch
                    {
                        main_form.UpdateGameTitleText(ModLoaderText.Error_UnableToOpenROM);
                        loadedISO = false;
                        ResetGameSpecific(false, true);
                        return;
                    }
                }
            }

            if (!ConsoleDetected)
            {
                if (OpenROM_Selection == OpenROM_SelectionType.AutomaticOnly)
                {
                    main_form.UpdateGameTitleText(ModLoaderText.Error_UnknownAutoGameROM);
                }
                else
                {
                    main_form.UpdateGameTitleText(ModLoaderText.Error_UnknownGameROM);
                }
                loadedISO = false;
            }
            else
            {
                loadedISO = true;
            }

            if (loadedISO && outputPathSet)
            {
                main_form.SetProcessStartAllowed(true);
                main_form.UpdateProcessText(ModLoaderText.ProcessReady);
            }
            else
            {
                main_form.SetProcessStartAllowed(false);

                if (loadedISO)
                {
                    main_form.UpdateProcessText(ModLoaderText.Step2Text);
                }
                else
                {
                    main_form.UpdateProcessText(ModLoaderText.Step1Text);

                    ResetGameSpecific(false, true);
                }
            }
        }

        void CacheSupportedGames()
        {
            ModLoaderGlobals.SupportedGames = new List<Game>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || !typeof(Modder).IsAssignableFrom(type)) // only get non-abstract modders
                    continue;
                Modder modder = (Modder)Activator.CreateInstance(type);
                Game game = modder.Game;
                game.ModderClass = type;

                ModLoaderGlobals.SupportedGames.Add(game);
            }
        }

        void CachePipelines()
        {
            ModLoaderGlobals.SupportedConsoles = new Dictionary<ModPipelineInfo, Type>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || !typeof(ModPipeline).IsAssignableFrom(type)) // only get non-abstract modders
                    continue;
                ModPipeline pipeline = (ModPipeline)Activator.CreateInstance(type);

                ModLoaderGlobals.SupportedConsoles.Add(pipeline.Metadata, type);
            }
        }

        void SetGameType(string serial, ConsoleMode console, uint RegionID = 0)
        {
            bool RegionNotSupported = true;
            Modder = null;

            ModCrates.ClearModLists();

            ModLoaderGlobals.Console = console;

            if (ModLoaderGlobals.SupportedGames.Count <= 0)
            {
                Console.WriteLine("ERROR: No games supported!");
                return; 
            }

            foreach (Game game in ModLoaderGlobals.SupportedGames)
            {
                if (!game.Consoles.Contains(console))
                    continue;

                RegionCode[] codelist;

                if (game.RegionID.ContainsKey(console))
                {
                    codelist = game.RegionID[console];
                }
                else
                {
                    codelist = null;
                    Console.WriteLine("ERROR: Missing RegionID for game " + game.Name);
                }

                foreach (var r in codelist)
                {
                    if (serial.Contains(r.Name))
                    {
                        if (console == ConsoleMode.XBOX && RegionID != r.RegionNumber)
                        {
                            RegionNotSupported = true;
                        }
                        else
                        {
                            RegionNotSupported = false;
                            ModLoaderGlobals.Region = r.Region;
                            Modder = (Modder)Activator.CreateInstance(game.ModderClass);
                            Game = game;
                            if (!string.IsNullOrEmpty(r.ExecName))
                            {
                                ModLoaderGlobals.ExecutableName = r.ExecName;
                            }
                            break;
                        }
                    }
                }

                if (RegionNotSupported)
                {
                    foreach (var r in codelist)
                    {
                        if (serial.Contains(r.Name))
                        {
                            ModLoaderGlobals.Region = RegionType.Undefined;
                            Modder = (Modder)Activator.CreateInstance(game.ModderClass);
                            Game = game;
                            if (!string.IsNullOrEmpty(r.ExecName))
                            {
                                ModLoaderGlobals.ExecutableName = r.ExecName;
                            }
                            break;
                        }
                    }
                }

                if (Modder != null)
                    break;
            }

            string cons_mod = "";
            switch (console)
            {
                default: cons_mod = console.ToString(); break;
                case ConsoleMode.Undefined: cons_mod = "(" + ModLoaderText.UnknownConsole + ")"; break;
            }

            string region_mod = "";
            switch (ModLoaderGlobals.Region)
            {
                case RegionType.NTSC_J: region_mod = "NTSC-J"; break;
                case RegionType.NTSC_U: region_mod = "NTSC-U"; break;
                case RegionType.PAL: region_mod = "PAL"; break;
                case RegionType.Global: region_mod = ""; break;
                default: region_mod = "(" + ModLoaderText.UnknownRegion + ")"; break;
            }

            // UI stuff
            if (Modder == null)
            {
                main_form.SetLayoutUnsupportedGame(cons_mod);
            }
            else
            {
                Modder.PopulateProperties();
                main_form.SetLayoutSupportedGame(Game, cons_mod, region_mod);
            }
        }

        public void DisableInteraction()
        {
            main_form.DisableInteraction();

            processActive = true;
        }
        public void EnableInteraction()
        {
            main_form.EnableInteraction();

            processActive = false;
        }

        public void UpdateInputSetting(bool state)
        {
            inputDirectoryMode = state;

            main_form.UpdateProcessText(ModLoaderText.Step1Text);
            ModLoaderGlobals.InputPath = "";

            ResetGameSpecific(true, false);
        }
        public void UpdateOutputSetting(bool state)
        {
            outputDirectoryMode = state;
            ModLoaderGlobals.OutputPath = "";

            if (loadedISO)
            {
                main_form.UpdateProcessText(ModLoaderText.Step2Text);
            }
        }

        public void API_Link_Clicked()
        {
            if (Modder != null && !string.IsNullOrWhiteSpace(Game.API_Credit) && !string.IsNullOrWhiteSpace(Game.API_Link))
            {
                Process.Start(Game.API_Link);
            }
        }

        public void UpdateModMenuChangedState(bool change)
        {
            main_form.UpdateModMenuChangeState(change);
        }
        public void UpdateModCrateChangedState()
        {
            main_form.UpdateModCrateChangedState();
        }

        void ResetGameSpecific(bool ClearGameText = false, bool ExtendedWindow = false)
        {
            Modder = null;
            ModCrates.ClearModLists();
            loadedISO = false;

            main_form.ResetGameSpecific(ClearGameText, ExtendedWindow);
        }
    }
}
