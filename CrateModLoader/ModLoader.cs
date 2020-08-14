using DiscUtils.Iso9660;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CrateModLoader.Resources.Text;

namespace CrateModLoader
{

    public class ModLoader
    {

        // UI elements
        public Label processText;
        public ProgressBar progressBar;
        public Button startButton;
        public Label text_gameType;
        public LinkLabel text_titleLabel;
        public LinkLabel text_apiLabel;
        public LinkLabel text_optionDescLabel;
        public Panel panel_optionDesc;
        public PictureBox image_gameIcon;
        public CheckedListBox list_modOptions;
        public Form main_form;
        public Button button_browse1;
        public Button button_browse2;
        public Button button_randomize;
        public Button button_modTools;
        public Button button_downloadMods;
        public TextBox textbox_input_path;
        public TextBox textbox_output_path;
        public NumericUpDown textbox_rando_seed;
        public Button button_modMenu;
        public Button button_modCrateMenu;
        public CheckBox checkbox_fromFolder;
        public CheckBox checkbox_toFolder;
        public IntPtr formHandle;

        [DllImport("user32.dll")]
        public static extern int FlashWindow(IntPtr Hwnd, bool Revert);

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
        public Modder Modder;
        private Process ISOcreatorProcess;
        public BackgroundWorker asyncWorker;
        public OpenROM_SelectionType OpenROM_Selection = OpenROM_SelectionType.AutomaticOnly;

        // Builds the ISO
        void CreateISO()
        {
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
                //Use ImgBurn
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
                
                string args = "";
                args += "/MODE BUILD ";
                args += "/BUILDINPUTMODE STANDARD ";
                args += "/BUILDOUTPUTMODE IMAGEFILE ";
                args += "/SRC \"";
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    args += dir.FullName;
                    args += "|";
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    args += file.FullName;
                    args += "|";
                }
                args += "\" ";
                args += "/DEST " + ModLoaderGlobals.OutputPath + " ";
                args += "/FILESYSTEM \"ISO9660 + UDF\" ";
                args += "/UDFREVISION \"1.02\" ";
                args += "/VOLUMELABEL \"" + ModLoaderGlobals.ISO_Label + "\" ";
                args += "/OVERWRITE YES ";
                args += "/START ";
                args += "/CLOSE ";
                args += "/PRESERVEFULLPATHNAMES NO ";
                args += "/RECURSESUBDIRECTORIES YES ";
                args += "/NOIMAGEDETAILS ";
                args += "/NOSAVELOG ";
                args += "/PORTABLE ";
                args += "/NOSAVESETTINGS ";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "ImgBurn.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();
                ISOcreatorProcess.WaitForExit();
                

                /* TODO: Figure out how to make it work with CDBuilder
                CDBuilder isoBuild = new CDBuilder();
                isoBuild.UseJoliet = true;
                isoBuild.UpdateIsolinuxBootTable = false;
                isoBuild.VolumeIdentifier = ISO_label;

                HashSet<FileStream> files = new HashSet<FileStream>();

                foreach (FileInfo file in di.GetFiles())
                {
                    if (file.Name.ToUpper() == "SYSTEM.CNF")
                    {
                        AddFile(isoBuild, file, string.Empty, files);
                    }
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    if (file.Name.ToUpper() == PS2_executable_name)
                    {
                        AddFile(isoBuild, file, string.Empty, files);
                    }
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    isoBuild.AddDirectory(dir.Name);
                    Recursive_AddDirs(isoBuild, dir, dir.Name + @"\", files);
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    if (file.Name.ToUpper() != PS2_executable_name && file.Name.ToUpper() != "SYSTEM.CNF")
                    {
                        AddFile(isoBuild, file, string.Empty, files);
                    }
                }

                isoBuild.Build(ModLoaderGlobals.OutputPath);

                foreach (FileStream file in files)
                {
                    file.Close();
                }
                */
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
                // To fix: PS1, PS2 require ISO label; PSP requires ISO file; GCN: Incorrect paths because of product code folder
                if (ModLoaderGlobals.Console == ConsoleMode.PS1 || ModLoaderGlobals.Console == ConsoleMode.PS2 || ModLoaderGlobals.Console == ConsoleMode.PSP || ModLoaderGlobals.Console == ConsoleMode.GCN)
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
            else if (ModLoaderGlobals.Console == ConsoleMode.XBOX)
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
            FinishISO();
        }

        public void EditGameContent()
        {
            //To make sure the seed matches
            ModLoaderGlobals.RandomizerSeed = int.Parse(textbox_rando_seed.Text);

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

        public void FinishISO()
        {
            CreateISO();

            if (!keepTempFiles)
            {
                DeleteTempFiles();
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
            progressBar.Value = 100;
            FlashWindow(formHandle, false);
            if (e.Error != null)
            {
                progressBar.Value = progressBar.Minimum;
                SystemSounds.Beep.Play();
                processText.Text = ModLoaderText.Process_Error + " " + e.Error.Message;
            }
            else if (!e.Cancelled)
            {
                processText.Text = ModLoaderText.Process_Finished;
                SystemSounds.Beep.Play();
            }
            else
            {
                progressBar.Value = progressBar.Minimum;
                processText.Text = ModLoaderText.Process_Cancelled;
            }
            EnableInteraction();
            asyncWorker.DoWork -= asyncWorker_DoWork;
            asyncWorker.RunWorkerCompleted -= asyncWorker_RunWorkerCompleted;
            asyncWorker.ProgressChanged -= asyncWorker_ProgressChanged;
        }

        private void asyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 0)
            {
                processText.Text = ModLoaderText.Process_Step0;
            }
            else if (e.ProgressPercentage == 25)
            {
                if (inputDirectoryMode)
                {
                    processText.Text = ModLoaderText.Process_Step1_Folder;
                }
                else
                {
                    processText.Text = ModLoaderText.Process_Step1_ROM;
                }
            }
            else if (e.ProgressPercentage == 50)
            {
                processText.Text = ModLoaderText.Process_Step2;
            }
            else if (e.ProgressPercentage == 75)
            {
                if (outputDirectoryMode)
                {
                    processText.Text = ModLoaderText.Process_Step3_Folder;
                }
                else
                {
                    processText.Text = ModLoaderText.Process_Step3_ROM;
                }
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
                                    foreach (var rc in Modder.Game.RegionID_PS2)
                                        if (rc.Region == ModLoaderGlobals.Region)
                                            ModLoaderGlobals.ProductCode = rc.CodeName;
                                }
                            }
                            else
                            {
                                SetGameType(titleID, ConsoleMode.PS1);
                                if (Modder != null)
                                {
                                    foreach (var rc in Modder.Game.RegionID_PS1)
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
                            SetGameType(titleID, ConsoleMode.GCN);
                            if (Modder != null)
                            {
                                ModLoaderGlobals.ProductCode = titleID;
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
                    text_gameType.Text = ModLoaderText.Error_UnableToOpenGameFolder;
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
                            SetGameType(titleID, ConsoleMode.GCN);
                            if (Modder != null)
                            {
                                ModLoaderGlobals.ProductCode = titleID;
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
                if (Modder == null && (OpenROM_Selection == OpenROM_SelectionType.AutomaticOnly || OpenROM_Selection == OpenROM_SelectionType.Any))
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
                        processText.Text = ModLoaderText.Step1Text;
                    }

                    DeleteTempFiles();
                }
                if (Modder == null && (OpenROM_Selection == OpenROM_SelectionType.AutomaticOnly || OpenROM_Selection == OpenROM_SelectionType.Any))
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
                                text_gameType.Text = ModLoaderText.Error_UnknownAutoGameROM;
                                loadedISO = false;
                                startButton.Enabled = false;
                                processText.Text = ModLoaderText.Step1Text;
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
                                            foreach (var rc in Modder.Game.RegionID_PS2)
                                                if (rc.Region == ModLoaderGlobals.Region)
                                                    ModLoaderGlobals.ProductCode = rc.CodeName;
                                        }
                                    }
                                    else
                                    {
                                        SetGameType(titleID, ConsoleMode.PS1);
                                        if (Modder != null)
                                        {
                                            foreach (var rc in Modder.Game.RegionID_PS1)
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
                        text_gameType.Text = ModLoaderText.Error_UnableToOpenROM;
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
                    text_gameType.Text = ModLoaderText.Error_UnknownAutoGameROM;
                }
                else
                {
                    text_gameType.Text = ModLoaderText.Error_UnknownGameROM;
                }
                loadedISO = false;
            }
            else
            {
                loadedISO = true;
            }

            if (loadedISO && outputPathSet)
            {
                startButton.Enabled = true;
                processText.Text = ModLoaderText.ProcessReady;
            }
            else
            {
                startButton.Enabled = false;

                if (loadedISO)
                {
                    processText.Text = ModLoaderText.Step2Text;
                }
                else
                {
                    processText.Text = ModLoaderText.Step1Text;

                    ResetGameSpecific(false, true);
                }
            }
        }

        void SetGameType(string serial, ConsoleMode console, uint RegionID = 0)
        {
            bool RegionNotSupported = true;
            Modder = null;
            button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            ModCrates.ClearModLists();

            ModLoaderGlobals.Console = console;

            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || !typeof(Modder).IsAssignableFrom(type)) // only get non-abstract modders
                    continue;
                Modder modder = (Modder)Activator.CreateInstance(type);
                if (!modder.Game.Consoles.Contains(console))
                    continue;
                RegionCode[] codelist =
                      console == ConsoleMode.PS2 ? modder.Game.RegionID_PS2
                    : console == ConsoleMode.PS1 ? modder.Game.RegionID_PS1
                    : console == ConsoleMode.PSP ? modder.Game.RegionID_PSP
                    : console == ConsoleMode.GCN ? modder.Game.RegionID_GCN
                    : console == ConsoleMode.WII ? modder.Game.RegionID_WII
                    : console == ConsoleMode.XBOX ? modder.Game.RegionID_XBOX
                    : console == ConsoleMode.XBOX360 ? modder.Game.RegionID_XBOX360
                    : console == ConsoleMode.PC ? modder.Game.RegionID_PC
                    : null;
                foreach (var r in codelist)
                {
                    if (serial.Contains(r.Name))
                    {
                        if (console == ConsoleMode.XBOX)
                        {
                            if (RegionID == r.RegionNumber)
                            {
                                ModLoaderGlobals.Region = r.Region;
                                RegionNotSupported = false;
                                Modder = modder;
                                if (!string.IsNullOrEmpty(r.ExecName))
                                {
                                    ModLoaderGlobals.ExecutableName = r.ExecName;
                                }
                                break;
                            }
                            else
                            {
                                RegionNotSupported = true;
                            }
                        }
                        else
                        {
                            ModLoaderGlobals.Region = r.Region;
                            Modder = modder;
                            RegionNotSupported = false;
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
                            Modder = modder;
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
                default:
                case ConsoleMode.Undefined: cons_mod = "(" + ModLoaderText.UnknownConsole + ")"; break;
                case ConsoleMode.PSP: cons_mod = "PSP"; break;
                case ConsoleMode.PS2: cons_mod = "PS2"; break;
                case ConsoleMode.GCN: cons_mod = "GC"; break;
                case ConsoleMode.XBOX: cons_mod = "XBOX"; break;
                case ConsoleMode.PS1: cons_mod = "PS1"; break;
                case ConsoleMode.WII: cons_mod = "Wii"; break;
                case ConsoleMode.XBOX360: cons_mod = "360"; break;
                case ConsoleMode.DC: cons_mod = "DC"; break;
                case ConsoleMode.PC: cons_mod = "PC"; break;
                case ConsoleMode.Android: cons_mod = "Android"; break;
                case ConsoleMode.N3DS: cons_mod = "3DS"; break;
                case ConsoleMode.N64: cons_mod = "N64"; break;
                case ConsoleMode.NDS: cons_mod = "DS"; break;
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

            list_modOptions.Items.Clear();
            if (Modder == null)
            {
                button_modMenu.Visible = true;
                button_modMenu.Enabled = false;
                button_modCrateMenu.Enabled = button_modCrateMenu.Visible = button_modTools.Visible = button_modTools.Enabled = true;
                button_randomize.Enabled = button_randomize.Visible = button_modMenu.Visible = button_modMenu.Enabled = button_downloadMods.Enabled = button_downloadMods.Visible = false;
                textbox_rando_seed.Enabled = textbox_rando_seed.Visible = false;

                text_gameType.Text = ModLoaderText.UnsupportedGameTitle + " (" + cons_mod + ")";
                text_apiLabel.Text = string.Empty;
                text_optionDescLabel.Text = string.Empty;
                text_optionDescLabel.Visible = false;
                panel_optionDesc.Visible = false;

                image_gameIcon.Visible = false;
            }
            else
            {
                Image gameIcon = Modder.Game.Icon;

                //button_modMenu.Enabled = button_modMenu.Visible = Modder.Game.ModMenuEnabled;
                button_modMenu.Visible = true;
                button_modMenu.Enabled = Modder.ModMenuEnabled;
                button_modCrateMenu.Visible = true;
                button_modCrateMenu.Enabled = Modder.Game.ModCratesSupported;
                button_randomize.Enabled = button_randomize.Visible = button_modTools.Enabled = button_modTools.Visible = button_downloadMods.Visible = true;
                textbox_rando_seed.Enabled = textbox_rando_seed.Visible = true;
                text_optionDescLabel.Text = string.Empty;
                text_optionDescLabel.Visible = false;
                panel_optionDesc.Visible = false;

                if (string.IsNullOrWhiteSpace(region_mod))
                {
                    text_gameType.Text = string.Format("{0}\n({1})", Modder.Game.Name, cons_mod);
                }
                else
                {
                    text_gameType.Text = string.Format("{0}\n({1} {2})", Modder.Game.Name, region_mod, cons_mod);
                }
                
                if (!string.IsNullOrWhiteSpace(Modder.Game.API_Credit))
                {
                    text_apiLabel.Text = Modder.Game.API_Credit;
                    if (!string.IsNullOrWhiteSpace(Modder.Game.API_Link))
                    {
                        text_apiLabel.Enabled = true;
                    }
                    else
                    {
                        text_apiLabel.Enabled = false;
                    }
                }
                else
                {
                    text_apiLabel.Text = ModLoaderText.GameHasNoAPI;
                    text_apiLabel.Enabled = false;
                }

                if (gameIcon != null)
                {
                    int offset_x = 4;
                    int offset_y = 4;
                    Bitmap bitmap_orig = new Bitmap(gameIcon);
                    Bitmap bitmap = new Bitmap(gameIcon.Width + offset_x, gameIcon.Height + offset_y);

                    for (int x = offset_x; x < bitmap.Width; x++)
                    {
                        for (int y = offset_y; y < bitmap.Height; y++)
                        {
                            Color bitColor = bitmap_orig.GetPixel(x - offset_x, y - offset_y);
                            if (bitColor.A > 1)
                            {
                                bitmap.SetPixel(x, y, Color.FromArgb(128, 0, 0, 0));
                            }
                            else
                            {
                                bitmap.SetPixel(x, y, Color.FromArgb(0, 0, 0, 0));
                            }
                        }
                    }
                    for (int x = 0; x < bitmap_orig.Width; x++)
                    {
                        for (int y = 0; y < bitmap_orig.Height; y++)
                        {
                            Color bitColor = bitmap_orig.GetPixel(x, y);
                            if (bitColor.A > 1)
                            {
                                bitmap.SetPixel(x, y, bitColor);
                            }
                        }
                    }

                    image_gameIcon.Image = bitmap;
                    
                    image_gameIcon.Visible = true;
                }
                else
                {
                    image_gameIcon.Visible = false;
                }

                if (Modder.Options.Count > 0)
                {
                    foreach (var option in Modder.Options.Values)
                    {
                        list_modOptions.Items.Add(option, option.Enabled);
                    }
                }
                else
                {
                    // probably no longer needed
                    //list_modOptions.Items.Add("No options available", false);
                }
            }
            int height = 295 + 45 + (list_modOptions.Items.Count * 15);
            list_modOptions.Visible = list_modOptions.Enabled = list_modOptions.Items.Count > 0;
            if (main_form.Size.Height < height)
            {
                //main_form.Size = new Size(main_form.MinimumSize.Width, height + 300);
                main_form.Size = new Size(main_form.MinimumSize.Width, height);
            }
            main_form.MinimumSize = new Size(main_form.MinimumSize.Width, height);
            if (main_form.Size.Height > height)
            {
                main_form.Size = new Size(main_form.MinimumSize.Width, height);
            }
        }

        public void OpenModCrateManager()
        {
            // Mod Crate Manager Window: 
            // Either a checkbox list of .zip files in a mod directory OR
            // A list with a button that lets you manually add .zip files
            // Set availability in the respective modder's Game struct (ModCratesSupported variable) 

            ModCrateManagerForm modCrateManagerMenu = new ModCrateManagerForm();

            if (ModCrates.SupportedMods.Count <= 0)
            {
                ModCrates.PopulateModList();
            }
            else
            {
                ModCrates.UpdateModList();
            }

            modCrateManagerMenu.Owner = Program.ModProgramForm;
            modCrateManagerMenu.Show();
        }

        public void OpenModMenu()
        {
            // Individual Game Mod Menu
            // Detailed settings UI for mod properties
            // Automatically generated for any ModProperty in the modder class' namespace

            ModMenuForm modMenu = new ModMenuForm(Modder);

            modMenu.Owner = Program.ModProgramForm;
            modMenu.Show();

            //Modder.OpenModMenu();
        }

        public void DisableInteraction()
        {
            startButton.Enabled = false;
            list_modOptions.Enabled = false;
            button_browse1.Enabled = false;
            button_browse2.Enabled = false;
            button_randomize.Enabled = false;
            textbox_output_path.ReadOnly = true;
            textbox_rando_seed.ReadOnly = true;
            textbox_rando_seed.Enabled = false;
            button_modMenu.Enabled = false;
            button_modCrateMenu.Enabled = false;
            checkbox_fromFolder.Enabled = false;
            checkbox_toFolder.Enabled = false;
            text_apiLabel.Enabled = false;
            text_titleLabel.Enabled = false;
            button_modTools.Enabled = false;
            button_downloadMods.Enabled = false;
            processActive = true;
        }
        public void EnableInteraction()
        {
            startButton.Enabled = true;
            list_modOptions.Enabled = true;
            button_browse1.Enabled = true;
            button_browse2.Enabled = true;
            button_randomize.Enabled = true;
            textbox_output_path.ReadOnly = false;
            textbox_rando_seed.ReadOnly = false;
            textbox_rando_seed.Enabled = true;
            button_modCrateMenu.Enabled = true;

            if (Modder != null)
            {
                button_modMenu.Enabled = Modder.ModMenuEnabled;
            }
            //button_modTools.Enabled = true;
            //button_downloadMods.Enabled = true;

            checkbox_fromFolder.Enabled = true;
            checkbox_toFolder.Enabled = true;
            if (Modder != null && !string.IsNullOrWhiteSpace(Modder.Game.API_Link))
            {
                text_apiLabel.Enabled = true;
            }
            text_titleLabel.Enabled = true;
            processActive = false;
        }

        public void UpdateInputSetting()
        {
            inputDirectoryMode = checkbox_fromFolder.Checked;

            processText.Text = ModLoaderText.Step1Text;
            textbox_input_path.Text = "";
            ModLoaderGlobals.InputPath = "";

            ResetGameSpecific(true, false);
        }
        public void UpdateOutputSetting()
        {
            outputDirectoryMode = checkbox_toFolder.Checked;

            textbox_output_path.Text = "";
            ModLoaderGlobals.OutputPath = "";

            if (loadedISO)
            {
                processText.Text = ModLoaderText.Step2Text;
            }

            startButton.Enabled = false;
        }

        public void API_Link_Clicked()
        {
            if (Modder != null && Modder.Game.API_Credit != null && Modder.Game.API_Link != null && Modder.Game.API_Link != "" && Modder.Game.API_Link != string.Empty)
            {
                text_apiLabel.LinkVisited = true;
                Process.Start(Modder.Game.API_Link);
            }
        }

        void ResetGameSpecific(bool ClearGameText = false, bool ExtendedWindow = false)
        {
            Modder = null;
            button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            button_modMenu.Text = ModLoaderText.ModMenuButton;
            ModCrates.ClearModLists();
            loadedISO = false;

            startButton.Enabled = false;

            button_modMenu.Enabled = button_modMenu.Visible = false;
            button_modCrateMenu.Enabled = button_modCrateMenu.Visible = false;
            button_randomize.Enabled = button_randomize.Visible = button_modTools.Visible = button_modTools.Enabled = button_downloadMods.Enabled = button_downloadMods.Visible = false;
            textbox_rando_seed.Enabled = textbox_rando_seed.Visible = false;

            text_apiLabel.Text = string.Empty;
            text_apiLabel.LinkVisited = false;
            if (ClearGameText)
            {
                text_gameType.Text = string.Empty;
            }

            image_gameIcon.Visible = false;
            text_optionDescLabel.Text = string.Empty;
            text_optionDescLabel.Visible = false;
            panel_optionDesc.Visible = false;

            list_modOptions.Visible = list_modOptions.Enabled = false;

            int Height = 188;
            if (ExtendedWindow)
            {
                Height = 220;
            }

            if (main_form.Size.Height > Height)
            {
                main_form.Size = new Size(main_form.Size.Width, Height);
            }
            main_form.MinimumSize = new Size(main_form.MinimumSize.Width, Height);
        }
    }
}
