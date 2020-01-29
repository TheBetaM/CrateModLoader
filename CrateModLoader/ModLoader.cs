using DiscUtils.Iso9660;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;

namespace CrateModLoader
{
    public enum OpenROM_SelectionType
    {
        PSXPS2PSP = 1,
        GCNWII = 2,
        Any = 3,
    }
    public enum ConsoleMode
    {
        Undefined = -1,
        PSP = 0,
        PS2 = 1,
        XBOX = 2,
        GCN = 3,
        PS1 = 4,
        WII = 5,
        XBOX360 = 6,
    }
    public enum RegionType
    {
        Undefined = -1,
        NTSC_U = 0,
        PAL = 1,
        NTSC_J = 2,
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
        public Label text_optionsLabel;
        public PictureBox image_gameIcon;
        public CheckedListBox list_modOptions;
        public Form main_form;
        public Button button_browse1;
        public Button button_browse2;
        public Button button_randomize;
        public TextBox textbox_output_path;
        public NumericUpDown textbox_rando_seed;
        public Button button_modMenu;
        public Button button_modCrateMenu;
        public BackgroundWorker asyncWorker;
        /// <summary> String used to show which version of CML the modded game was built with. </summary>
        public string releaseVersionString = "v1.0";
        /// <summary> Hexadecimal display of which quick options were selected (automatically adjusts according the amount of quick options) - MSB is first option from the top </summary>
        public string optionsSelectedString
        {
            get
            {
                string str = string.Empty;
                if (list_modOptions != null)
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
                return str;
            }
        }

        public string inputISOpath = "";
        public string outputISOpath = "";
        public ConsoleMode isoType = ConsoleMode.Undefined;
        public Modder Modder;
        public RegionType targetRegion = RegionType.Undefined;
        public string extractedPath = "";
        public string PS2_executable_name = "";
        public string ProductCode = "";
        public bool loadedISO = false;
        public bool outputPathSet = false;
        public bool keepTempFiles = false;
        private Process ISOcreatorProcess;
        public OpenROM_SelectionType OpenROM_Selection = OpenROM_SelectionType.PSXPS2PSP;

        //ISO settings
        public string ISO_label;

        // Build the ISO
        void CreateISO()
        {
            if (isoType == ConsoleMode.PS2)
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
                // Use WQSG_UMD
                File.Copy(inputISOpath, AppDomain.CurrentDomain.BaseDirectory + "/Tools/Game.iso");

                string args = "";
                args += @"--iso=";
                args += AppDomain.CurrentDomain.BaseDirectory + "/Tools/Game.iso";
                args += @" --file=";
                args += extractedPath + "PSP_GAME";
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
                args += extractedPath + @"\P-" + Program.ModProgram.ProductCode.Substring(0, 4) + " -q -d ";
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
                args += extractedPath + " ";
                if (isoType == ConsoleMode.GCN)
                {
                    args += "--ciso ";
                }
                args += "\"" + outputISOpath + "\" ";

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/wit/wit.exe";
                //ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.StartInfo.UseShellExecute = false;
                ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
                ISOcreatorProcess.StartInfo.CreateNoWindow = true;
                ISOcreatorProcess.Start();

                Console.WriteLine(ISOcreatorProcess.StandardOutput.ReadToEnd());

                ISOcreatorProcess.WaitForExit();
            }
            else if (isoType == ConsoleMode.XBOX)
            {
                // Directory mode, so there's nothing to do here
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

            if (isoType == ConsoleMode.GCN || isoType == ConsoleMode.WII)
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
            Modder.StartModProcess();
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
                processText.Text = "Extracting game...";
            }
            else if (e.ProgressPercentage == 50)
            {
                processText.Text = "Modding game...";
            }
            else if (e.ProgressPercentage == 75)
            {
                processText.Text = "Building game...";
            }
        }

        public void CheckISO()
        {
            if (OpenROM_Selection == OpenROM_SelectionType.GCNWII || OpenROM_Selection == OpenROM_SelectionType.Any)
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
                    }
                }
            }
            else if (OpenROM_Selection != OpenROM_SelectionType.GCNWII)
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
                            // Currently Gamecube ISO's end up here
                            text_gameType.Text = "Game ROM - Invalid PS1/PS2/PSP ROM!";
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
                                SetGameType(titleID, ConsoleMode.PS2);
                                if (Modder != null)
                                {
                                    foreach (var rc in Modder.Game.RegionID_PS2)
                                        if (rc.Region == targetRegion)
                                            ProductCode = rc.CodeName;
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
                            }
                        }
                        else if (cd.FileExists(@"default.xbe"))
                        {
                            isoType = ConsoleMode.XBOX;
                            //TODO: figure out xbox checks
                        }
                        else
                        {
                            Modder = null;
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
                    text_gameType.Text = "Game ROM - Could not open the game ROM!";
                    loadedISO = false;
                    return;
                }
            }

            if (Modder == null)
            {
                text_gameType.Text = "Game ROM - Unknown game ROM!";
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
            else if (loadedISO)
            {
                startButton.Enabled = false;
                processText.Text = "Waiting for output path...";
            }
        }

        void SetGameType(string serial, ConsoleMode console)
        {
            Modder = null;
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
                    : null;
                foreach (var r in codelist)
                {
                    if (r.Name == serial)
                    {
                        targetRegion = r.Region;
                        Modder = modder;
                        break;
                    }
                }
                if (Modder != null)
                    break;
            }
            isoType = console;

            string cons_mod = "";
            if (console == ConsoleMode.Undefined)
            {
                cons_mod = "ERROR_CONSOLE";
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
                default:
                    region_mod = "ERROR_REGION";
                    break;
            }

            list_modOptions.Items.Clear();
            if (Modder == null)
            {
                button_modMenu.Enabled = button_modMenu.Visible = false;
                button_modCrateMenu.Enabled = button_modCrateMenu.Visible = false;

                text_gameType.Text = "Unsupported game detected.";
                text_optionsLabel.Text = string.Empty;

                image_gameIcon.Visible = false;
            }
            else
            {
                Image gameIcon = Modder.Game.Icon;

                button_modMenu.Enabled = button_modMenu.Visible = Modder.Game.ModMenuEnabled;
                button_modCrateMenu.Enabled = button_modCrateMenu.Visible = Modder.Game.ModCratesSupported;

                text_gameType.Text = string.Format("Game ROM - {0} {1} {2} detected!", Modder.Game.Name, region_mod, cons_mod);
                text_optionsLabel.Text = string.Format("{0} Quick Options ({1})", Modder.Game.Name, Modder.Game.API_Credit);
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
                    list_modOptions.Items.Add("No options available", false);
                }
            }
            int height = 320 + (list_modOptions.Items.Count * 15);
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
        }
    }
}
