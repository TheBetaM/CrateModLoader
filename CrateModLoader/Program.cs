using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Media;
using System.Diagnostics;
using System.Reflection;
using DiscUtils.Iso9660;
//Crate Mod Loader Main Class

namespace CrateModLoader
{
    static class Program
    {
        public static ModLoader ModProgram;
        public static ModLoaderForm ModProgramForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ModProgram = new ModLoader();
            ModProgramForm = new ModLoaderForm();
            Application.Run(ModProgramForm);
        }
    }

    public class ModLoader
    {
        /// <summary> Global Randomizer Seed </summary>
        public int randoSeed = 0;
        public Label processText;
        public ProgressBar progressBar;
        public Button startButton;
        public int processProgress = 0;
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
        /// <summary> String used to show which version of CML the modded game was built with. </summary>
        public string releaseVersionString = "v1.0";
        /// <summary> String of bytes displaying which quick options were selected. (Automatically adjusts to more bytes depending on options available, min. 2 characters, 2 characters per 8 options) </summary>
        public string optionsSelectedString = "00";

        public string inputISOpath = "";
        public string outputISOpath = "";
        public ConsoleMode isoType = ConsoleMode.Undefined;
        public int targetGame = -1;
        public object GameClass;
        public RegionType targetRegion = RegionType.Undefined;
        public string extractedPath = "";
        public string PS2_executable_name = "";
        public string PS2_game_code_name = "";
        public bool loadedISO = false;
        public bool outputPathSet = false;
        public bool keepTempFiles = false;
        private Process ISOcreatorProcess;

        public Timer processTimer = new Timer();

        //ISO settings
        public string ISO_label;

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
                args += "/VOLUMELABEL \"" + PS2_game_code_name + "\" ";
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
                // Use PSPTools?
                //UMD.ISO IsoFile = new UMD.ISO();
                //IsoFile.CreateISO(extractedPath, outputISOpath, true);
            }
            else if (isoType == ConsoleMode.GCN)
            {
                // Use GCR (or Wiimms ISO Tool?)
                string args = "";
                args += "rebuild image: ";
                args += extractedPath + " " + outputISOpath;

                ISOcreatorProcess = new Process();
                ISOcreatorProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gcr.exe";
                ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ISOcreatorProcess.StartInfo.Arguments = args;
                ISOcreatorProcess.Start();
                ISOcreatorProcess.WaitForExit();
            }
            else if (isoType == ConsoleMode.WII)
            {
                // Use Wiimms ISO Tool
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
            try
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

                    extractedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"temp\");
                    if (isoInfo.Length * 2 > GetTotalFreeSpace(extractedPath.Substring(0, 3)))
                    {
                        cd.Dispose();
                        ErrorFinish("Extraction error: Not enough hard drive space where this program is!");
                        return;
                    }
                    if (isoInfo.Length * 2 > GetTotalFreeSpace(outputISOpath.Substring(0, 3)))
                    {
                        cd.Dispose();
                        ErrorFinish("Extraction error: Not enough hard drive space in the output path!");
                        return;
                    }

                    processText.Text = "Extracting ISO...";
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
                                Recursive_CreateDirs(ref cd, directory, ref fileStreamFrom, ref fileStreamTo);
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
            catch
            {
                ErrorFinish("Cannot open game ROM!");
                return;
            }

            ProgressProcess();
        }

        private void Recursive_CreateDirs(ref CDReader cd, string dir, ref Stream fileStreamFrom, ref Stream fileStreamTo)
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
                    Recursive_CreateDirs(ref cd, directory, ref fileStreamFrom, ref fileStreamTo);
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
            if (processProgress == 0)
            {
                processText.Text = "Extracting game...";
                processProgress = 0;
                progressBar.Value = progressBar.Minimum;

                processTimer.Tick += new EventHandler(DelayedProcessStart); // FIXME : change to a background worker instead of a timer
                processTimer.Interval = 1000;
                processTimer.Start();
            }
            else if (processProgress > 0)
            {
                ProgressProcess();
            }
        }

        private void DelayedProcessStart(Object myObject, EventArgs myEventArgs)
        {
            processTimer.Stop();
            ProgressProcess();
        }

        public void ProgressProcess()
        {
            processProgress++;
            
            if (processProgress == 1)
            {
                LoadISO();
            }
            else if (processProgress == 2)
            {
                progressBar.PerformStep();
                processTimer.Start();
                processText.Text = "Modding game...";
            }
            else if (processProgress == 3)
            {
                EditGameContent();
                //startButton.Enabled = true; //DEBUG, pressing the button again will progress
            }
            else if (processProgress == 4)
            {
                progressBar.PerformStep();
                processTimer.Start();
                processText.Text = "Building game...";
            }
            else if (processProgress == 5)
            {
                FinishISO();
            }
            else
            {
                progressBar.PerformStep();
                ProcessFinished();
            }
        }

        public void EditGameContent()
        {
            Type thisType = GameDatabase.Games[targetGame].ModderClass;
            MethodInfo theMethod = thisType.GetMethod("StartModProcess");

            try
            {
                theMethod.Invoke(GameClass, null);
            }
            catch (Exception ex)
            {
                DeleteTempFiles();
                ErrorFinish("Modding error: " + ex.Message);
                return;
            }

            ProgressProcess();
        }

        public void FinishISO()
        {
            CreateISO();

            if (!keepTempFiles)
            {
                DeleteTempFiles();
            }

            ProgressProcess();
        }

        void DeleteTempFiles()
        {
            if (Directory.Exists(extractedPath))
            {
                DirectoryInfo di = new DirectoryInfo(extractedPath);

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                di.Delete();
            }
        }

        public void ProcessFinished()
        {
            processText.Text = "Finished!";
            SystemSounds.Beep.Play();
            EnableInteraction();
        }

        public void ErrorFinish(string errorType)
        {
            processProgress = 0;
            progressBar.Value = progressBar.Minimum;
            SystemSounds.Beep.Play();
            processText.Text = "Error: " + errorType;
            EnableInteraction();
        }

        public void OptionChanged(int option,bool value)
        {
            Type thisType = GameDatabase.Games[targetGame].ModderClass;
            MethodInfo theMethod = thisType.GetMethod("OptionChanged");
            try
            {
                theMethod.Invoke(GameClass, new object[] { option, value });
            }
            catch
            {
                DialogResult dialogResult = MessageBox.Show("This game doesn't have mod options!", "Error", MessageBoxButtons.OK);
                return;
            }

            //Setting Options String
            if (list_modOptions.Items.Count > 0)
            {
                int optionCount = list_modOptions.Items.Count;
                int byteCount = (int)Math.Ceiling(optionCount / 8d);
                byte[] optionsSelected = new byte[byteCount];
                for (int i = 0; i < optionCount; i++)
                {
                    if (list_modOptions.GetItemCheckState(i) == CheckState.Checked)
                    {
                        optionsSelected[(int)Math.Floor(i / 8d)] += (byte)Math.Pow(2, i % 8);
                    }
                }
                optionsSelectedString = optionsSelected[0].ToString("X2");
                if (optionsSelected.Length > 1)
                {
                    for (int i = 1; i < optionsSelected.Length; i++)
                    {
                        if (optionsSelected[i] != 0x00)
                        {
                            optionsSelectedString += optionsSelected[i].ToString("X2");
                        }
                    }
                }
                //Console.WriteLine("options " + optionsSelectedString);
            }
        }

        public void CheckISO()
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
                        isoType = ConsoleMode.PS1;
                    }
                    else if (!CDReader.Detect(isoStream))
                    {
                        // Currently Gamecube ISO's end up here
                        text_gameType.Text = "Game ROM - Invalid PS2/PSP ISO!";
                        return;
                    }
                    else
                    {
                        cd = new CDReader(isoStream, true);
                    }
                    Stream fileStream;
                    bool foundMetaData = false;
                    if (cd.FileExists(@"SYSTEM.CNF"))
                    {
                        fileStream = cd.OpenFile(@"SYSTEM.CNF", FileMode.Open);
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            string input;
                            string titleID;
                            int GameID = -1;
                            input = sr.ReadLine();
                            titleID = input;
                            foundMetaData = false;
                            if (isoType == ConsoleMode.PS1)
                            {
                                for (int game = 0; game < GameDatabase.Games.Length; game++)
                                {
                                    if (GameDatabase.Games[game].RegionID_PS1 != null && GameDatabase.Games[game].RegionID_PS1.Length > 0)
                                    {
                                        foreach (RegionCode rcode in GameDatabase.Games[game].RegionID_PS1)
                                        {
                                            if (titleID == rcode.Name)
                                            {
                                                GameID = game;
                                                SetGameType(GameID, ConsoleMode.PS1, rcode.Region);
                                                PS2_executable_name = rcode.ExecName;
                                                PS2_game_code_name = rcode.CodeName;
                                                foundMetaData = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (foundMetaData)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int game = 0; game < GameDatabase.Games.Length; game++)
                                {
                                    if (GameDatabase.Games[game].RegionID_PS2 != null && GameDatabase.Games[game].RegionID_PS2.Length > 0)
                                    {
                                        foreach (RegionCode rcode in GameDatabase.Games[game].RegionID_PS2)
                                        {
                                            if (titleID == rcode.Name)
                                            {
                                                GameID = game;
                                                SetGameType(GameID, ConsoleMode.PS2, rcode.Region);
                                                PS2_executable_name = rcode.ExecName;
                                                PS2_game_code_name = rcode.CodeName;
                                                foundMetaData = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (foundMetaData)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (cd.FileExists(@"UMD_DATA.BIN"))
                    {
                        fileStream = cd.OpenFile(@"UMD_DATA.BIN", FileMode.Open);
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            string input;
                            string titleID;
                            int GameID = -1;
                            input = sr.ReadLine();
                            titleID = input.Substring(0, 10);
                            foundMetaData = false;
                            for (int game = 0; game < GameDatabase.Games.Length; game++)
                            {
                                if (GameDatabase.Games[game].RegionID_PSP != null && GameDatabase.Games[game].RegionID_PSP.Length > 0)
                                {
                                    foreach (RegionCode rcode in GameDatabase.Games[game].RegionID_PSP)
                                    {
                                        if (titleID == rcode.Name)
                                        {
                                            GameID = game;
                                            SetGameType(GameID, ConsoleMode.PSP, rcode.Region);
                                            foundMetaData = true;
                                            break;
                                        }
                                    }
                                }
                                if (foundMetaData)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (cd.FileExists(@"default.xbe"))
                    {
                        isoType = ConsoleMode.XBOX;
                        //TODO: figure out xbox checks
                    }
                    else if (cd.DirectoryExists("systemdata") && cd.FileExists(@"systemdata\ISO.hdr"))
                    {
                        // CDReader can't detect Gamecube ISO's so GCR must be used instaed?
                        fileStream = cd.OpenFile(@"systemdata\ISO.hdr", FileMode.Open);
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            string input;
                            string titleID;
                            int GameID = -1;
                            input = sr.ReadLine();
                            titleID = input.Substring(0, 4);
                            foundMetaData = false;
                            for (int game = 0; game < GameDatabase.Games.Length; game++)
                            {
                                if (GameDatabase.Games[game].RegionID_GCN != null && GameDatabase.Games[game].RegionID_GCN.Length > 0)
                                {
                                    foreach (RegionCode rcode in GameDatabase.Games[game].RegionID_GCN)
                                    {
                                        if (titleID == rcode.Name)
                                        {
                                            GameID = game;
                                            SetGameType(GameID, ConsoleMode.GCN, rcode.Region);
                                            foundMetaData = true;
                                            break;
                                        }
                                    }
                                }
                                if (foundMetaData)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foundMetaData = false;
                    }

                    if (!foundMetaData)
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


        void SetGameType(int type, ConsoleMode console, RegionType region)
        {
            targetGame = type;
            Type targetType = GameDatabase.Games[targetGame].ModderClass;
            GameClass = Activator.CreateInstance(targetType);

            targetRegion = region;
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

            string region_mod = "";
            if (region == RegionType.Undefined)
            {
                region_mod = "ERROR_REGION";
            }
            else if (region == RegionType.NTSC_U)
            {
                region_mod = "NTSC-U";
            }
            else if (region == RegionType.PAL)
            {
                region_mod = "PAL";
            }
            else if (region == RegionType.NTSC_J)
            {
                region_mod = "NTSC-J";
            }

            if (type == -1)
            {
                text_gameType.Text = "Game ROM - ERROR_GAME";
            }
            else
            {
                string apiCredit = GameDatabase.Games[targetGame].API_Credit;
                string gameName = GameDatabase.Games[targetGame].Name;
                bool hasModMenu = GameDatabase.Games[targetGame].ModMenuEnabled;
                bool hasModCrates = GameDatabase.Games[targetGame].ModCratesSupported;
                System.Drawing.Image gameIcon = GameDatabase.Games[targetGame].Icon;

                if (!hasModMenu)
                {
                    button_modMenu.Enabled = false;
                    button_modMenu.Visible = false;
                }
                else
                {
                    button_modMenu.Enabled = true;
                    button_modMenu.Visible = true;
                }
                if (!hasModCrates)
                {
                    button_modCrateMenu.Enabled = false;
                    button_modCrateMenu.Visible = false;
                }
                else
                {
                    button_modCrateMenu.Enabled = true;
                    button_modCrateMenu.Visible = true;
                }

                text_gameType.Text = "Game ROM - " + gameName + " " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = gameName + " Quick Options (" + apiCredit + ")";
                if (gameIcon != null)
                {
                    image_gameIcon.Image = gameIcon;
                    image_gameIcon.Visible = true;
                }
                else
                {
                    image_gameIcon.Visible = false;
                }
                list_modOptions.Items.Clear();

                int height = 320;
                if (main_form.Size.Height < height)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, height);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, height);

                Type thisType = GameDatabase.Games[targetGame].ModderClass;
                MethodInfo theMethod = thisType.GetMethod("UpdateModOptions");
                try
                {
                    theMethod.Invoke(GameClass, null);
                }
                catch
                {
                    DialogResult dialogResult = MessageBox.Show("This game doesn't have quick options!", "Error", MessageBoxButtons.OK);
                    return;
                }
            }
            
        }

        public void PrepareOptionsList(string[] modOptions)
        {
            list_modOptions.Items.Clear();
            list_modOptions.Items.AddRange(modOptions);
            int height = 320 + (list_modOptions.Items.Count * 15);
            list_modOptions.Visible = true;
            if (main_form.Size.Height < height)
            {
                main_form.Size = new System.Drawing.Size(main_form.Size.Width, height);
            }
            main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, height);
        }

        public void OpenModCrateManager()
        {
            // Mod Crate Manager Window: 
            // Either a checkbox list of .zip files in a mod directory OR
            // A list with a button that lets you manually add .zip files
            // Set availability in GameDatabase (ModCratesSupported variable)
            ModCrateManagerForm modCrateManagerMenu = new ModCrateManagerForm();
            modCrateManagerMenu.Owner = Program.ModProgramForm;
            modCrateManagerMenu.Show();
        }

        public void OpenModMenu()
        {
            // Individual Game Mod Menu
            // Detailed settings UI for some games
            // Set availability in GameDatabase (ModMenuEnabled variable)

            Type thisType = GameDatabase.Games[targetGame].ModderClass;
            MethodInfo theMethod = thisType.GetMethod("OpenModMenu");
            try
            {
                theMethod.Invoke(GameClass, null);
            }
            catch
            {
                DialogResult dialogResult = MessageBox.Show("This game doesn't have a mod menu!", "Error", MessageBoxButtons.OK);
                return;
            }
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
