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

namespace CrateModLoader
{
    public enum OpenROM_SelectionType
    {
        PSXPS2PSPGCNWIIXBOX = 1,
        Any = 2,
    }
    public enum ConsoleMode
    {
        Undefined = -1,
        PSP = 0, // PlayStation Portable
        PS2 = 1, // PlayStation 2
        XBOX = 2, // Xbox
        GCN = 3, // Gamecube
        PS1 = 4, // PlayStation
        WII = 5, // Wii
        XBOX360 = 6,  // Xbox 360
        PC = 7, // PC CDROM/DVDROM, being considered
        DC = 8, // Dreamcast, not supported yet
        PS3 = 9, // PlayStation 3, just for reference
        Android = 10, // Android, being considered
        NDS = 11, // DS, not supported yet
        N3DS = 12, // 3DS, not supported yet
        N64 = 13, // N64, being considered
    }
    public enum RegionType
    {
        Undefined = -1,
        NTSC_U = 0,
        PAL = 1,
        NTSC_J = 2,
        Global = 3,
    }

    //Crate Mod Loader Main Class
    public class ModLoader
    {
        /// <summary> Global Randomizer Seed </summary>
        public int randoSeed = 0;
        public Label processText;
        public ProgressBar progressBar;
        public Button startButton;
        public Label text_gameType;
        public LinkLabel text_titleLabel;
        public LinkLabel text_apiLabel;
        public PictureBox image_gameIcon;
        public CheckedListBox list_modOptions;
        public Form main_form;
        public Button button_browse1;
        public Button button_browse2;
        public Button button_randomize;
        public TextBox textbox_input_path;
        public TextBox textbox_output_path;
        public NumericUpDown textbox_rando_seed;
        public Button button_modMenu;
        public Button button_modCrateMenu;
        public CheckBox checkbox_fromFolder;
        public CheckBox checkbox_toFolder;
        //public RadioButton button_radio_FromROM;
        //public RadioButton button_radio_FromFolder;
        //public RadioButton button_radio_ToROM;
        //public RadioButton button_radio_ToFolder;
        public BackgroundWorker asyncWorker;
        /// <summary> String used to show which version of CML the modded game was built with. </summary>
        public string releaseVersionString = "v1.2.0";
        /// <summary> Hexadecimal display of which quick options were selected (automatically adjusts according the amount of quick options) - MSB is first option from the top </summary>
        public string optionsSelectedString
        {
            get
            {
                string str = string.Empty;
                if (list_modOptions != null && list_modOptions.Items.Count > 0)
                {
                    for (int l = 0; l < (list_modOptions.Items.Count+31) / 32; ++l)
                    {
                        int val = 0;
                        for (int i = 0, s = Math.Min(32,list_modOptions.Items.Count - l * 32); i < s; ++i)
                        {
                            if (list_modOptions.Items[l*32+i] is ModOption o)
                            {
                                if (o.Enabled)
                                    val |= 1 << (31 - i);
                            }
                        }
                        str += val.ToString("X08");
                    }
                }
                else
                {
                    str = "00000000";
                }
                return str;
            }
        }

        public string inputISOpath = "";
        public string outputISOpath = "";
        public ConsoleMode isoType = ConsoleMode.Undefined;
        public Modder Modder;
        public RegionType targetRegion = RegionType.Undefined;

        public string extractedPath = "";
        public string tempPath = AppDomain.CurrentDomain.BaseDirectory + @"temp\";

        public string PS2_executable_name = "";
        public string ProductCode = "";
        public bool loadedISO = false;
        public bool outputPathSet = false;
        public bool keepTempFiles = false;
        public bool inputDirectoryMode = false;
        public bool outputDirectoryMode = false;
        private Process ISOcreatorProcess;
        public OpenROM_SelectionType OpenROM_Selection = OpenROM_SelectionType.PSXPS2PSPGCNWIIXBOX;
        public bool processActive = false;

        //ISO settings
        public string ISO_label;

        // Build the ISO
        void CreateISO()
        {
            if (outputDirectoryMode)
            {
                //Directory Mode
                DirectoryInfo di = new DirectoryInfo(extractedPath);
                if (isoType == ConsoleMode.PS2)
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

                if (!Directory.Exists(outputISOpath))
                {
                    Directory.CreateDirectory(outputISOpath);
                }

                string pathparent = outputISOpath + @"\";
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
            else if (isoType == ConsoleMode.PS2)
            {
                //Use ImgBurn
                DirectoryInfo di = new DirectoryInfo(extractedPath);
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
                args += "/DEST " + outputISOpath + " ";
                args += "/FILESYSTEM \"ISO9660 + UDF\" ";
                args += "/UDFREVISION \"1.02\" ";
                args += "/VOLUMELABEL \"" + ISO_label + "\" ";
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
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/ImgBurn.exe";
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

                isoBuild.Build(outputISOpath);

                foreach (FileStream file in files)
                {
                    file.Close();
                }
                */
            }
            else if (isoType == ConsoleMode.PSP)
            {
                if (inputDirectoryMode)
                {
                    throw new Exception("Building PSP ROMs from directories is not supported!");
                }
                // Use WQSG_UMD
                File.Copy(inputISOpath, AppDomain.CurrentDomain.BaseDirectory + "/Tools/Game.iso");

                string args = "";
                args += @"--iso=";
                args += "\"" + AppDomain.CurrentDomain.BaseDirectory + "/Tools/Game.iso\"";
                args += " --file=\"";
                args += extractedPath + "PSP_GAME\"";
                //args += " --log";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/WQSG_UMD.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();
                ISOcreatorProcess.WaitForExit();

                File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/Game.iso", outputISOpath);
            }
            else if (isoType == ConsoleMode.GCN)
            {
                // Use GCIT (Wiims ISO Tool doesn't work for this?)

                Directory.Move(extractedPath + @"\P-" + Program.ModProgram.ProductCode.Substring(0, 4) + @"\files\", extractedPath + @"\P-" + Program.ModProgram.ProductCode.Substring(0, 4) + @"\root\");

                string args = "";
                args += "\"" + extractedPath + @"\P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "\" -q -d ";
                args += "\"" + outputISOpath + "\" ";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gcit.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                ISOcreatorProcess.StartInfo.Arguments = args;
                //ISOcreatorProcess.StartInfo.UseShellExecute = false;
                //ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                //ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                ISOcreatorProcess.Start();

                //Console.WriteLine(ISOcreatorProcess.StandardOutput.ReadToEnd());

                ISOcreatorProcess.WaitForExit();
            }
            else if (isoType == ConsoleMode.WII)
            {
                // Use Wiimms ISO Tool
                string args = "copy ";
                args += "\"" + extractedPath + "\" ";
                if (isoType == ConsoleMode.GCN)
                {
                    args += "--ciso ";
                }
                args += "\"" + outputISOpath + "\" ";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/wit/wit.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ISOcreatorProcess.StartInfo.Arguments = args;
                //ISOcreatorProcess.StartInfo.UseShellExecute = false;
                //ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                //ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                ISOcreatorProcess.Start();

                //Console.WriteLine(ISOcreatorProcess.StandardOutput.ReadToEnd());

                ISOcreatorProcess.WaitForExit();
            }
            else if (isoType == ConsoleMode.XBOX)
            {
                //Use extract-xiso
                string args = "-c ";
                args += extractedPath + " ";
                args += "\"" + outputISOpath + "\" ";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/extract-xiso.exe";
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
                isoBuild.VolumeIdentifier = ISO_label;

                DirectoryInfo di = new DirectoryInfo(extractedPath);
                HashSet<FileStream> files = new HashSet<FileStream>();

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    Recursive_AddDirs(isoBuild, dir, dir.Name + @"\", files);
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    AddFile(isoBuild, file, string.Empty, files);
                }

                if (isoType == ConsoleMode.PS1)
                {
                    using (FileStream output = new FileStream(outputISOpath, FileMode.Create, FileAccess.Write))
                    using (Stream input = isoBuild.Build())
                    {
                        ISO2PSX.Run(input, output);
                    }
                }
                else
                {
                    isoBuild.Build(outputISOpath);
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
            if (isoType == ConsoleMode.PS1 || isoType == ConsoleMode.PS2)
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
            extractedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"temp\");
            if (Directory.Exists(extractedPath))
            {
                DeleteTempFiles();
            }

            if (inputDirectoryMode && !outputDirectoryMode)
            {
                // To fix: PS1, PS2 require ISO label; PSP requires ISO file; GCN: Incorrect paths because of product code folder
                if (isoType == ConsoleMode.PS1 || isoType == ConsoleMode.PS2 || isoType == ConsoleMode.PSP || isoType == ConsoleMode.GCN)
                {
                    throw new Exception("Building ROMs from directories with this console is not supported yet!");
                }
            }
            if (outputDirectoryMode)
            {
                // To fix: Incorrect paths because of product code folder
                if (isoType == ConsoleMode.GCN)
                {
                    throw new Exception("Building GC directories is not supported yet!");
                }
            }
            if (isoType == ConsoleMode.XBOX360 && !outputDirectoryMode)
            {
                throw new Exception("Building 360 ROMs is not supported yet! Save to folder instead.");
            }
            if (isoType == ConsoleMode.PC)
            {
                if (!inputDirectoryMode || !outputDirectoryMode)
                {
                    throw new Exception("PC games are only supported in Folder mode!");
                }
            }

            if (inputDirectoryMode)
            {
                DirectoryInfo di = new DirectoryInfo(inputISOpath);
                if (!di.Exists)
                {
                    throw new IOException("Extraction error: Input directory cannot be accessed!");
                }

                Directory.CreateDirectory(extractedPath);

                asyncWorker.ReportProgress(25);

                string pathparent = extractedPath + @"\";
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
            else if (isoType == ConsoleMode.GCN || isoType == ConsoleMode.WII)
            {
                // TODO: add free space checks
                extractedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"temp");

                string args = "extract ";
                args += "\"" + inputISOpath + "\" ";
                args += "\"" + extractedPath + "\" ";

                asyncWorker.ReportProgress(25);

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/wit/wit.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();
                ISOcreatorProcess.WaitForExit();
            }
            else if (isoType == ConsoleMode.XBOX)
            {
                // TODO: add free space checks
                string args = "-x ";
                args += "\"" + inputISOpath + "\" ";

                asyncWorker.ReportProgress(25);

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/extract-xiso.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();
                ISOcreatorProcess.WaitForExit();

                Directory.Move(AppDomain.CurrentDomain.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputISOpath), extractedPath);
            }
            else
            {
                using (FileStream isoStream = File.Open(inputISOpath, FileMode.Open))
                {
                    FileInfo isoInfo = new FileInfo(inputISOpath);
                    CDReader cd;
                    FileStream tempbin = null;
                    if (Path.GetExtension(inputISOpath).ToLower() == ".bin") // PS1 image
                    {
                        FileStream binconvout = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Create, FileAccess.Write);
                        PSX2ISO.Run(isoStream, binconvout);
                        binconvout.Close();
                        tempbin = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Open, FileAccess.Read);
                        cd = new CDReader(tempbin, true);
                    }
                    else
                        cd = new CDReader(isoStream, true);
                    ISO_label = cd.VolumeLabel;

                    /* Sometimes doesn't work?
                    if (isoInfo.Length * 2 > GetTotalFreeSpace(extractedPath.Substring(0, 3)))
                    {
                        cd.Dispose();
                        throw new IOException("Extraction error: Not enough hard drive space where this program is!");
                    }
                    if (isoInfo.Length * 2 > GetTotalFreeSpace(outputISOpath.Substring(0, 3)))
                    {
                        cd.Dispose();
                        throw new IOException("Extraction error: Not enough hard drive space in the output path!");
                    }
                    */

                    asyncWorker.ReportProgress(25);
                    //fileStream = cd.OpenFile(@"SYSTEM.CNF", FileMode.Open);

                    if (!Directory.Exists(extractedPath))
                    {
                        Directory.CreateDirectory(extractedPath);
                    }

                    //Extracting ISO
                    Stream fileStreamFrom = null;
                    Stream fileStreamTo = null;
                    if (cd.GetDirectories("").Length > 0)
                    {
                        foreach (string directory in cd.GetDirectories(""))
                        {
                            Directory.CreateDirectory(extractedPath + directory);
                            if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                            {
                                foreach (string file in cd.GetFiles(directory))
                                {
                                    fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                                    string filename = extractedPath + file;
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
                            string filename = extractedPath + "/" + file;
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
                Directory.CreateDirectory(extractedPath + "/" + directory);
                if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                {
                    foreach (string file in cd.GetFiles(directory))
                    {
                        fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                        fileStreamTo = File.Open(extractedPath + "/" + file, FileMode.OpenOrCreate);
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
            if (ModCrates.ModsActiveAmount > 0 && (Modder == null || !Modder.ModCratesManualInstall))
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\";
                if (isoType == ConsoleMode.GCN)
                {
                    basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\P-" + ProductCode.Substring(0, 4) + @"\files\";
                }
                else if (isoType == ConsoleMode.WII)
                {
                    basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\DATA\files\";
                }
                else if (isoType == ConsoleMode.PSP)
                {
                    basePath = AppDomain.CurrentDomain.BaseDirectory + @"temp\PSP_GAME\USRDIR\";
                }
                ModCrates.InstallLayerMods(basePath, 0);
            }
            if (Modder != null)
            {
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
            if (Directory.Exists(extractedPath))
            {
                DirectoryInfo di = new DirectoryInfo(extractedPath);

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
            if (e.Error != null)
            {
                progressBar.Value = progressBar.Minimum;
                SystemSounds.Beep.Play();
                processText.Text = "Error: " + e.Error.Message;
            }
            else if (!e.Cancelled)
            {
                processText.Text = "Finished!";
                SystemSounds.Beep.Play();
            }
            else
            {
                progressBar.Value = progressBar.Minimum;
                processText.Text = "Canceled!";
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
                processText.Text = "Preparing...";
            }
            else if (e.ProgressPercentage == 25)
            {
                if (inputDirectoryMode)
                {
                    processText.Text = "Copying files...";
                }
                else
                {
                    processText.Text = "Extracting game...";
                }
            }
            else if (e.ProgressPercentage == 50)
            {
                processText.Text = "Modding game...";
            }
            else if (e.ProgressPercentage == 75)
            {
                if (outputDirectoryMode)
                {
                    processText.Text = "Copying modded files...";
                }
                else
                {
                    processText.Text = "Building game...";
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
                    isoType = ConsoleMode.Undefined;

                    if (File.Exists(inputISOpath + @"SYSTEM.CNF"))
                    {
                        using (StreamReader sr = new StreamReader(inputISOpath + @"SYSTEM.CNF"))
                        {
                            string titleID = sr.ReadLine();
                            if (titleID.Contains("BOOT2"))
                            {
                                SetGameType(titleID, ConsoleMode.PS2);
                                if (Modder != null)
                                {
                                    foreach (var rc in Modder.Game.RegionID_PS2)
                                        if (rc.Region == targetRegion)
                                            ProductCode = rc.CodeName;
                                }
                            }
                            else
                            {
                                SetGameType(titleID, ConsoleMode.PS1);
                                if (Modder != null)
                                {
                                    foreach (var rc in Modder.Game.RegionID_PS1)
                                        if (rc.Region == targetRegion)
                                            ProductCode = rc.CodeName;
                                }
                            }
                            ConsoleDetected = true;
                        }
                    }
                    else if (File.Exists(inputISOpath + @"UMD_DATA.BIN"))
                    {
                        using (StreamReader sr = new StreamReader(inputISOpath + @"UMD_DATA.BIN"))
                        {
                            string titleID = sr.ReadLine().Substring(0, 10);
                            SetGameType(titleID, ConsoleMode.PSP);
                            if (Modder != null)
                            {
                                ProductCode = titleID;
                            }
                            ConsoleDetected = true;
                        }
                    }
                    else if (File.Exists(inputISOpath + @"default.xbe"))
                    {
                        isoType = ConsoleMode.XBOX;
                        //Based on OpenXDK
                        using (FileStream fileStream = new FileStream(inputISOpath + @"default.xbe", FileMode.Open, FileAccess.Read, FileShare.Read))
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
                    else if (File.Exists(inputISOpath + @"default.xex"))
                    {
                        isoType = ConsoleMode.XBOX360;

                        string args = "-l ";
                        args += "\"" + inputISOpath + @"default.xex" + "\"";

                        ISOcreatorProcess = new Process();
                        ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/xextool.exe";
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
                                    ProductCode = titleID;
                                }
                                ConsoleDetected = true;
                            }
                        }
                    }
                    else if (File.Exists(inputISOpath + @"sys/main.dol") && File.Exists(inputISOpath + @"sys/boot.bin"))
                    {
                        isoType = ConsoleMode.GCN;

                        using (StreamReader sr = new StreamReader(inputISOpath + @"sys/boot.bin"))
                        {
                            string titleID = sr.ReadLine().Substring(0, 6);
                            SetGameType(titleID, ConsoleMode.GCN);
                            if (Modder != null)
                            {
                                ProductCode = titleID;
                            }
                            else
                            {
                                SetGameType(titleID, ConsoleMode.WII);
                                if (Modder != null)
                                {
                                    ProductCode = titleID;
                                }
                            }
                            ConsoleDetected = true;
                        }
                    }
                    else if (Directory.GetFiles(inputISOpath, "*.exe").Length > 0 || Directory.GetFiles(inputISOpath, "*.EXE").Length > 0)
                    {
                        isoType = ConsoleMode.PC;

                        string[] ExeFiles;
                        if (Modder == null && Directory.GetFiles(inputISOpath, "*.exe").Length > 0)
                        {
                            ExeFiles = Directory.GetFiles(inputISOpath, "*.exe");
                            for (int i = 0; i < ExeFiles.Length; i++)
                            {
                                if (Modder == null)
                                {
                                    SetGameType(ExeFiles[i], ConsoleMode.PC);
                                    if (Modder != null)
                                    {
                                        ProductCode = ExeFiles[i];
                                    }
                                    ConsoleDetected = true;
                                }
                            }
                        }
                        else if (Modder == null && Directory.GetFiles(inputISOpath, "*.EXE").Length > 0)
                        {
                            ExeFiles = Directory.GetFiles(inputISOpath, "*.EXE");
                            for (int i = 0; i < ExeFiles.Length; i++)
                            {
                                if (Modder == null)
                                {
                                    SetGameType(ExeFiles[i], ConsoleMode.PC);
                                    if (Modder != null)
                                    {
                                        ProductCode = ExeFiles[i];
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
                    ISO_label = ProductCode;
                }
                catch
                {
                    text_gameType.Text = "Could not open the game directory!";
                    loadedISO = false;
                    ResetGameSpecific(false, true);
                    return;
                }
            }
            else
            {
                if (OpenROM_Selection == OpenROM_SelectionType.PSXPS2PSPGCNWIIXBOX || OpenROM_Selection == OpenROM_SelectionType.Any)
                {
                    // Gamecube/Wii ROMs

                    string args = "ID6 ";
                    args += "\"" + inputISOpath + "\"";

                    ISOcreatorProcess = new Process();
                    ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/wit/wit.exe";
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
                                ProductCode = titleID;
                            }
                            else
                            {
                                SetGameType(titleID, ConsoleMode.WII);
                                if (Modder != null)
                                {
                                    ProductCode = titleID;
                                }
                            }
                            ConsoleDetected = true;
                        }
                    }
                }
                if (Modder == null && (OpenROM_Selection == OpenROM_SelectionType.PSXPS2PSPGCNWIIXBOX || OpenROM_Selection == OpenROM_SelectionType.Any))
                {
                    extractedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"temp\");
                    DeleteTempFiles();

                    string args = "-i -x ";
                    args += "\"" + inputISOpath + "\"";

                    //Modified extract-xiso only extracts the executables to check

                    ISOcreatorProcess = new Process();
                    ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/extract-xiso.exe";
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

                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputISOpath)))
                    {
                        Directory.Move(AppDomain.CurrentDomain.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputISOpath), extractedPath);
                    }

                    processText.Text = "Reading XISO...";

                    if (Directory.Exists(extractedPath) && File.Exists(extractedPath + @"default.xbe"))
                    {
                        isoType = ConsoleMode.XBOX;
                        //Based on OpenXDK
                        using (FileStream fileStream = new FileStream(extractedPath + @"default.xbe", FileMode.Open, FileAccess.Read, FileShare.Read))
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
                    else if (Directory.Exists(extractedPath) && File.Exists(extractedPath + @"default.xex"))
                    {
                        isoType = ConsoleMode.XBOX360;

                        string xargs = "-l ";
                        xargs += "\"" + extractedPath + @"default.xex" + "\"";

                        ISOcreatorProcess = new Process();
                        ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/xextool.exe";
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
                                    ProductCode = titleID;
                                }
                                ConsoleDetected = true;
                            }
                        }
                    }
                    else
                    {
                        processText.Text = "Waiting for input (1) ...";
                    }

                    DeleteTempFiles();
                }
                if (Modder == null && (OpenROM_Selection == OpenROM_SelectionType.PSXPS2PSPGCNWIIXBOX || OpenROM_Selection == OpenROM_SelectionType.Any))
                {
                    try
                    {
                        using (FileStream isoStream = File.Open(inputISOpath, FileMode.Open))
                        {
                            CDReader cd;
                            FileStream tempbin = null;
                            isoType = ConsoleMode.Undefined;

                            if (Path.GetExtension(inputISOpath).ToLower() == ".bin") // PS1 image
                            {
                                FileStream binconvout = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Create, FileAccess.Write);
                                PSX2ISO.Run(isoStream, binconvout);
                                binconvout.Close();
                                tempbin = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Open, FileAccess.Read);
                                cd = new CDReader(tempbin, true);
                            }
                            else if (!CDReader.Detect(isoStream))
                            {
                                text_gameType.Text = "Unknown PSX/PS2/PSP/GCN/WII/XBOX game ROM!";
                                loadedISO = false;
                                startButton.Enabled = false;
                                processText.Text = "Waiting for input (1) ...";
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
                                                if (rc.Region == targetRegion)
                                                    ProductCode = rc.CodeName;
                                        }
                                    }
                                    else
                                    {
                                        SetGameType(titleID, ConsoleMode.PS1);
                                        if (Modder != null)
                                        {
                                            foreach (var rc in Modder.Game.RegionID_PS1)
                                                if (rc.Region == targetRegion)
                                                    ProductCode = rc.CodeName;
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
                                        ProductCode = titleID;
                                    }
                                    ConsoleDetected = true;
                                }
                            }
                            else if (cd.FileExists(@"default.xbe"))
                            {
                                isoType = ConsoleMode.XBOX;
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
                        text_gameType.Text = "Could not open the game ROM!";
                        loadedISO = false;
                        ResetGameSpecific(false, true);
                        return;
                    }
                }
            }

            if (!ConsoleDetected)
            {
                if (OpenROM_Selection == OpenROM_SelectionType.PSXPS2PSPGCNWIIXBOX)
                {
                    text_gameType.Text = "Unknown PSX/PS2/PSP/GCN/WII/XBOX game ROM!";
                }
                else
                {
                    text_gameType.Text = "Unknown game ROM!";
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
                processText.Text = "Ready!";
            }
            else
            {
                startButton.Enabled = false;

                if (loadedISO)
                {
                    processText.Text = "Waiting for output path (2) ...";
                }
                else
                {
                    processText.Text = "Waiting for input (1) ...";

                    ResetGameSpecific(false, true);
                }
            }
        }

        void SetGameType(string serial, ConsoleMode console, uint RegionID = 0)
        {
            bool RegionNotSupported = true;
            Modder = null;
            button_modCrateMenu.Text = "Mod Crates";
            ModCrates.ClearModLists();

            isoType = console;

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
                                targetRegion = r.Region;
                                RegionNotSupported = false;
                                Modder = modder;
                                if (!string.IsNullOrEmpty(r.ExecName))
                                {
                                    PS2_executable_name = r.ExecName;
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
                            targetRegion = r.Region;
                            Modder = modder;
                            RegionNotSupported = false;
                            if (!string.IsNullOrEmpty(r.ExecName))
                            {
                                PS2_executable_name = r.ExecName;
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
                            targetRegion = RegionType.Undefined;
                            Modder = modder;
                            if (!string.IsNullOrEmpty(r.ExecName))
                            {
                                PS2_executable_name = r.ExecName;
                            }
                            break;
                        }
                    }
                }

                if (Modder != null)
                    break;
            }

            string cons_mod = "";
            if (console == ConsoleMode.Undefined)
            {
                cons_mod = "(Unknwon Console)";
            }
            else if (console == ConsoleMode.PSP)
            {
                cons_mod = "PSP";
            }
            else if (console == ConsoleMode.PS2)
            {
                cons_mod = "PS2";
            }
            else if (console == ConsoleMode.GCN)
            {
                cons_mod = "GC";
            }
            else if (console == ConsoleMode.XBOX)
            {
                cons_mod = "XBOX";
            }
            else if (console == ConsoleMode.PS1)
            {
                cons_mod = "PS1";
            }
            else if (console == ConsoleMode.WII)
            {
                cons_mod = "WII";
            }
            else if (console == ConsoleMode.XBOX360)
            {
                cons_mod = "360";
            }
            else if (console == ConsoleMode.DC)
            {
                cons_mod = "DC";
            }
            else if (console == ConsoleMode.PC)
            {
                cons_mod = "PC";
            }

            string region_mod;
            switch (targetRegion)
            {
                case RegionType.NTSC_J:
                    region_mod = "NTSC-J";
                    break;
                case RegionType.NTSC_U:
                    region_mod = "NTSC-U";
                    break;
                case RegionType.PAL:
                    region_mod = "PAL";
                    break;
                case RegionType.Global:
                    region_mod = "";
                    break;
                default:
                    region_mod = "(Unknown Region)";
                    break;
            }

            list_modOptions.Items.Clear();
            if (Modder == null)
            {
                button_modMenu.Visible = true;
                button_modMenu.Enabled = false;
                button_modCrateMenu.Enabled = button_modCrateMenu.Visible = true;
                button_randomize.Enabled = button_randomize.Visible = false;
                textbox_rando_seed.Enabled = textbox_rando_seed.Visible = false;

                text_gameType.Text = "Unsupported " + cons_mod + " Game";
                text_apiLabel.Text = string.Empty;

                image_gameIcon.Visible = false;
            }
            else
            {
                Image gameIcon = Modder.Game.Icon;

                //button_modMenu.Enabled = button_modMenu.Visible = Modder.Game.ModMenuEnabled;
                button_modMenu.Visible = true;
                button_modMenu.Enabled = Modder.Game.ModMenuEnabled;
                button_modCrateMenu.Enabled = button_modCrateMenu.Visible = Modder.Game.ModCratesSupported;
                button_randomize.Enabled = button_randomize.Visible = true;
                textbox_rando_seed.Enabled = textbox_rando_seed.Visible = true;

                if (string.IsNullOrWhiteSpace(region_mod))
                {
                    text_gameType.Text = string.Format("{0} ({1})", Modder.Game.Name, cons_mod);
                }
                else
                {
                    text_gameType.Text = string.Format("{0} ({1} {2})", Modder.Game.Name, region_mod, cons_mod);
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
                    text_apiLabel.Text = "No API available";
                    text_apiLabel.Enabled = false;
                }

                if (gameIcon != null)
                {
                    image_gameIcon.Image = gameIcon;
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
            int height = 306 + (list_modOptions.Items.Count * 15);
            list_modOptions.Visible = list_modOptions.Enabled = list_modOptions.Items.Count > 0;
            if (main_form.Size.Height < height)
            {
                main_form.Size = new Size(main_form.Size.Width, height);
            }
            main_form.MinimumSize = new Size(main_form.MinimumSize.Width, height);
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
            // Detailed settings UI for some games
            // Set availability in the respective modder's Game struct (ModMenuEnabled variable)

            Modder.OpenModMenu();
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
            button_modMenu.Enabled = false;
            button_modCrateMenu.Enabled = false;
            checkbox_fromFolder.Enabled = false;
            checkbox_toFolder.Enabled = false;
            text_apiLabel.Enabled = false;
            text_titleLabel.Enabled = false;
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
            button_modMenu.Enabled = true;
            button_modCrateMenu.Enabled = true;
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

            processText.Text = "Waiting for input (1) ...";
            textbox_input_path.Text = "";
            inputISOpath = "";

            ResetGameSpecific(true, false);
        }
        public void UpdateOutputSetting()
        {
            outputDirectoryMode = checkbox_toFolder.Checked;

            textbox_output_path.Text = "";
            outputISOpath = "";

            if (loadedISO)
            {
                processText.Text = "Waiting for output path (2) ...";
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
            button_modCrateMenu.Text = "Mod Crates";
            ModCrates.ClearModLists();
            loadedISO = false;

            startButton.Enabled = false;

            button_modMenu.Enabled = button_modMenu.Visible = false;
            button_modCrateMenu.Enabled = button_modCrateMenu.Visible = false;
            button_randomize.Enabled = button_randomize.Visible = false;
            textbox_rando_seed.Enabled = textbox_rando_seed.Visible = false;

            text_apiLabel.Text = string.Empty;
            text_apiLabel.LinkVisited = false;
            if (ClearGameText)
            {
                text_gameType.Text = string.Empty;
            }

            image_gameIcon.Visible = false;

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
