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
using DiscUtils;
using DiscUtils.Iso9660;

namespace CrateModLoader
{
    static class Program
    {
        public static ModLoader ModProgram;
        public static Modder_CNK ModCNK;
        public static Modder_CTTR ModCTTR;
        public static Modder_MoM ModMoM;
        public static Modder_Titans ModTitans;
        public static Modder_Twins ModTwins;
        public static Modder_CTR ModCTR;
        public static Modder_Crash1 ModCrash1;
        public static Modder_Crash2 ModCrash2;
        public static Modder_Crash3 ModCrash3;
        public static ModLoaderForm ModProgramForm;

        [STAThread]
        static void Main()
        {
            ModProgram = new ModLoader();
            ModCNK = new Modder_CNK();
            ModCTTR = new Modder_CTTR();
            ModMoM = new Modder_MoM();
            ModTitans = new Modder_Titans();
            ModTwins = new Modder_Twins();
            ModCTR = new Modder_CTR();
            ModCrash1 = new Modder_Crash1();
            ModCrash2 = new Modder_Crash2();
            ModCrash3 = new Modder_Crash3();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ModProgramForm = new ModLoaderForm();
            Application.Run(ModProgramForm);
        }
    }

    public class ModLoader
    {
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

        public string inputISOpath = "";
        public string outputISOpath = "";
        public ConsoleMode isoType = ConsoleMode.Undefined;
        public GameType targetGame = GameType.Undefined;
        public RegionType targetRegion = RegionType.Undefined;
        public string extractedPath = "";
        public string PS2_executable_name = "";
        public string PS2_game_code_name = "";
        public bool loadedISO = false;
        public bool outputPathSet = false;
        public bool keepTempFiles = false;
        public bool useImgBurn = true;
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
        public enum GameType
        {
            Undefined = -1,
            CTTR = 0,
            Titans = 1,
            MoM = 2,
            CNK = 3,
            Twins = 4,
            Crash1 = 5,
            Crash2 = 6,
            Crash3 = 7,
            CTR = 8,
            TWOC = 9,
            Bash = 10
        }
        public enum RegionType
        {
            Undefined = -1,
            NTSC_U = 0,
            PAL = 1,
            NTSC_J = 2,
        }

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
            }
            else if (isoType == ConsoleMode.PSP)
            {
                // Use ???
            }
            else if (isoType == ConsoleMode.GCN)
            {
                // Use GCR
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
            else
            {
                // failsafe
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
            isoBuild.AddDirectory(di.Name);
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
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
                isoBuild.AddFile(sName + file.Name + ";1", fstream);
            else
                isoBuild.AddFile(sName + file.Name, fstream);
            files.Add(fstream);
        }

        void LoadISO()
        {
            using (FileStream isoStream = File.Open(inputISOpath,FileMode.Open))
            {
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

                /* TODO
                if (cd.ClusterSize * 2 > GetTotalFreeSpace(extractedPath.Substring(0,4)))
                {
                    cd.Dispose();
                    ErrorFinish("Not enough hard drive space to proceed with extraction!");
                    return;
                }
                */

                processText.Text = "Extracting ISO...";
                //fileStream = cd.OpenFile(@"SYSTEM.CNF", FileMode.Open);
                extractedPath = AppDomain.CurrentDomain.BaseDirectory + "/temp/";
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
                        Directory.CreateDirectory(extractedPath + "/" + directory);
                        if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                        {
                            foreach (string file in cd.GetFiles(directory))
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
                processText.Text = "Extracting ISO...";
                processProgress = 0;
                progressBar.Value = progressBar.Minimum;

                processTimer.Tick += new EventHandler(DelayedProcessStart); // FIXME : change to a background worker instead of a timer
                processTimer.Interval = 500;
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
                processText.Text = "Saving ISO...";
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
            switch (targetGame)
            {
                case GameType.Twins:
                    Program.ModTwins.StartModProcess();
                    break;
                case GameType.CTTR:
                    Program.ModCTTR.StartModProcess();
                    break;
                case GameType.CNK:
                    Program.ModCNK.StartModProcess();
                    break;
                case GameType.Titans:
                    Program.ModTitans.StartModProcess();
                    break;
                case GameType.MoM:
                    Program.ModMoM.StartModProcess();
                    break;
                case GameType.Crash1:
                    Program.ModCrash1.StartModProcess();
                    break;
                case GameType.Crash2:
                    Program.ModCrash2.StartModProcess();
                    break;
                case GameType.Crash3:
                    Program.ModCrash3.StartModProcess();
                    break;
                case GameType.CTR:
                    Program.ModCTR.StartModProcess();
                    break;
            }

            ProgressProcess();
        }

        public void FinishISO()
        {
            CreateISO();

            // TODO: delete temp files
            if (!keepTempFiles)
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
            

            ProgressProcess();
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
            switch (targetGame)
            {
                case GameType.CTTR:
                    Program.ModCTTR.OptionChanged(option, value);
                    break;
                case GameType.Twins:
                    Program.ModTwins.OptionChanged(option, value);
                    break;
                case GameType.CNK:
                    Program.ModCNK.OptionChanged(option, value);
                    break;
                case GameType.Titans:
                    Program.ModTitans.OptionChanged(option, value);
                    break;
                case GameType.MoM:
                    Program.ModMoM.OptionChanged(option, value);
                    break;
                case GameType.Crash1:
                    Program.ModCrash1.OptionChanged(option, value);
                    break;
                case GameType.Crash2:
                    Program.ModCrash2.OptionChanged(option, value);
                    break;
                case GameType.Crash3:
                    Program.ModCrash3.OptionChanged(option, value);
                    break;
                case GameType.CTR:
                    Program.ModCTR.OptionChanged(option, value);
                    break;
            }
        }

        public void CheckISO()
        {
            using (FileStream isoStream = File.Open(inputISOpath, FileMode.Open))
            {
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
                else if (!CDReader.Detect(isoStream))
                {
                    // Currently Gamecube ISO's end up here
                    text_gameType.Text = "Invalid PS2/PSP ISO!";
                    return;
                }
                else
                    cd = new CDReader(isoStream, true);
                Stream fileStream;
                bool foundMetaData = false;
                if (cd.FileExists(@"SYSTEM.CNF"))
                {
                    fileStream = cd.OpenFile(@"SYSTEM.CNF", FileMode.Open);
                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        string input;
                        string titleID;
                        input = sr.ReadLine();
                        titleID = input;
                        foundMetaData = true;
                        if (titleID == @"BOOT2 = cdrom0:\SLUS_211.91;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CTTR, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_211.91";
                            PS2_game_code_name = "SLUS_21191";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_534.39;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CTTR, RegionType.PAL);
                            PS2_executable_name = "SLES_534.39";
                            PS2_game_code_name = "SLES_53439";
                        }
                        else if (titleID == "ULES-00168") //Unknown, TODO
                        {
                            //SetGameType(ConsoleMode.PS2, GameType.CTTR, RegionType.NTSC_J);
                            foundMetaData = false;
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_209.09;1 " || titleID == @"BOOT2 = cdrom0:\SLUS_209.09;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Twins, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_209.09";
                            PS2_game_code_name = "SLUS_20909";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_525.68;1 " || titleID == @"BOOT2 = cdrom0:\SLES_525.68;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Twins, RegionType.PAL);
                            PS2_executable_name = "SLES_525.68";
                            PS2_game_code_name = "SLES_52568";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLPM_658.01;1 " || titleID == @"BOOT2 = cdrom0:\SLPM_658.01;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Twins, RegionType.NTSC_J);
                            PS2_executable_name = "SLPM_658.01";
                            PS2_game_code_name = "SLPM_65801";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_215.83;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Titans, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_215.83";
                            PS2_game_code_name = "SLUS_21583";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_548.41;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Titans, RegionType.PAL);
                            PS2_executable_name = "SLES_548.41";
                            PS2_game_code_name = "SLES_54841";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_217.28;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.MoM, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_217.28";
                            PS2_game_code_name = "SLUS_21728";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_552.04;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.MoM, RegionType.PAL);
                            PS2_executable_name = "SLES_552.04";
                            PS2_game_code_name = "SLES_55204";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_206.49;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_206.49";
                            PS2_game_code_name = "SLUS_20649";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_515.11;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.PAL);
                            PS2_executable_name = "SLES_515.11";
                            PS2_game_code_name = "SLES_51511";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLPM_660.67;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.NTSC_J);
                            PS2_executable_name = "SLPM_660.67";
                            PS2_game_code_name = "SLPM_66067";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_202.38;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_202.38";
                            PS2_game_code_name = "SLUS_20238";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_503.86;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.PAL);
                            PS2_executable_name = "SLES_503.86";
                            PS2_game_code_name = "SLES_50386";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLPM_740.03;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.NTSC_J);
                            PS2_executable_name = "SLPM_740.03";
                            PS2_game_code_name = "SLPM_74003";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCUS_949.00;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash1, RegionType.NTSC_U);
                            PS2_executable_name = "SCUS_949.00";
                            PS2_game_code_name = "SCUS_94900";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCES_003.44;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash1, RegionType.PAL);
                            PS2_executable_name = "SCES_003.44";
                            PS2_game_code_name = "SCES_00344";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCPS_100.31;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash1, RegionType.NTSC_J);
                            PS2_executable_name = "SCPS_100.31";
                            PS2_game_code_name = "SCPS_10031";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCUS_941.54;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash2, RegionType.NTSC_U);
                            PS2_executable_name = "SCUS_94154";
                            PS2_game_code_name = "SCUS_94154";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCES_009.67;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash2, RegionType.PAL);
                            PS2_executable_name = "SCES_009.67";
                            PS2_game_code_name = "SCES_00967";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCPS_100.47;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash2, RegionType.NTSC_J);
                            PS2_executable_name = "SCPS_100.47";
                            PS2_game_code_name = "SCPS_10047";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCUS_942.44;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash3, RegionType.NTSC_U);
                            PS2_executable_name = "SCUS_942.44";
                            PS2_game_code_name = "SCUS_94244";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCES_014.20;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash3, RegionType.PAL);
                            PS2_executable_name = "SCES_014.20";
                            PS2_game_code_name = "SCES_01420";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCPS_100.73;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Crash3, RegionType.NTSC_J);
                            PS2_executable_name = "SCPS_100.73";
                            PS2_game_code_name = "SCPS_10073";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCUS_944.26;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.CTR, RegionType.NTSC_U);
                            PS2_executable_name = "SCUS_944.26";
                            PS2_game_code_name = "SCUS_94426";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCES_021.05;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.CTR, RegionType.PAL);
                            PS2_executable_name = "SCES_021.05";
                            PS2_game_code_name = "SCES_02105";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCPS_101.18;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.CTR, RegionType.NTSC_J);
                            PS2_executable_name = "SCPS_101.18";
                            PS2_game_code_name = "SCPS_10118";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCUS_945.70;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Bash, RegionType.NTSC_U);
                            PS2_executable_name = "SCUS_945.70";
                            PS2_game_code_name = "SCUS_94570";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCES_028.34;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Bash, RegionType.PAL);
                            PS2_executable_name = "SCES_028.34";
                            PS2_game_code_name = "SCES_02834";
                        }
                        else if (titleID == @"BOOT = cdrom:\SCPS_101.40;1")
                        {
                            SetGameType(ConsoleMode.PS1, GameType.Bash, RegionType.NTSC_J);
                            PS2_executable_name = "SCPS_101.40";
                            PS2_game_code_name = "SCPS_10140";
                        }
                        else
                        {
                            foundMetaData = false;
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
                        input = sr.ReadLine();
                        titleID = input.Substring(0, 10);
                        foundMetaData = true;
                        if (titleID == "ULUS-10044")
                        {
                            SetGameType(ConsoleMode.PSP, GameType.CTTR, RegionType.NTSC_U);
                        }
                        else if (titleID == "ULJM-05036")
                        {
                            SetGameType(ConsoleMode.PSP, GameType.CTTR, RegionType.NTSC_J);
                        }
                        else if (titleID == "ULES-00168")
                        {
                            SetGameType(ConsoleMode.PSP, GameType.CTTR, RegionType.PAL);
                        }
                        else if (titleID == "ULUS-10304")
                        {
                            SetGameType(ConsoleMode.PSP, GameType.Titans, RegionType.NTSC_U);
                        }
                        else if (titleID == "ULES-00917")
                        {
                            SetGameType(ConsoleMode.PSP, GameType.Titans, RegionType.PAL);
                        }
                        else if (titleID == "ULUS-10377")
                        {
                            SetGameType(ConsoleMode.PSP, GameType.MoM, RegionType.NTSC_U);
                        }
                        else if (titleID == "ULES-01171")
                        {
                            SetGameType(ConsoleMode.PSP, GameType.MoM, RegionType.PAL);
                        }
                        else
                        {
                            foundMetaData = false;
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
                        input = sr.ReadLine();
                        titleID = input.Substring(0, 4);
                        foundMetaData = true;
                        if (titleID == "G9RE")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.CTTR, RegionType.NTSC_U);
                        }
                        else if (titleID == "G9RJ")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.CTTR, RegionType.NTSC_J);
                        }
                        else if (titleID == "G9RH" || titleID == "G9RD" || titleID == "G9RF" || titleID == "G9RP")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.CTTR, RegionType.PAL);
                        }
                        else if (titleID == "GCNE")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.CNK, RegionType.NTSC_U);
                        }
                        else if (titleID == "GCNP")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.CNK, RegionType.PAL);
                        }
                        else if (titleID == "GC8J")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.CNK, RegionType.NTSC_J);
                        }
                        else if (titleID == "GCBE")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.TWOC, RegionType.NTSC_U);
                        }
                        else if (titleID == "GCBP")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.TWOC, RegionType.PAL);
                        }
                        else if (titleID == "GCBJ")
                        {
                            SetGameType(ConsoleMode.GCN, GameType.TWOC, RegionType.NTSC_J);
                        }
                        else
                        {
                            foundMetaData = false;
                        }
                    }
                }
                else
                {
                    foundMetaData = false;
                }

                if (!foundMetaData)
                {
                    text_gameType.Text = "Unknown game ROM!";
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


        void SetGameType(ConsoleMode console,GameType type,RegionType region)
        {
            targetGame = type;
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

            if (type == GameType.Undefined)
            {
                text_gameType.Text = "ERROR_GAME";
            }
            else
            {
                string[] modOptions = Program.ModCTTR.modOptions;
                string apiCredit = Program.ModCTTR.apiCredit;
                string gameName = Program.ModCTTR.gameName;
                bool hasModMenu = false;
                bool hasModCrates = false;
                System.Drawing.Image gameIcon = null;
                if (type == GameType.CTTR)
                {
                    gameName = Program.ModCTTR.gameName;
                    apiCredit = Program.ModCTTR.apiCredit;
                    modOptions = Program.ModCTTR.modOptions;
                    gameIcon = Program.ModCTTR.gameIcon;
                    hasModMenu = Program.ModCTTR.ModMenuEnabled;
                    hasModCrates = Program.ModCTTR.ModCratesSupported;
                }
                else if (type == GameType.Twins)
                {
                    gameName = Program.ModTwins.gameName;
                    apiCredit = Program.ModTwins.apiCredit;
                    modOptions = Program.ModTwins.modOptions;
                    gameIcon = Program.ModTwins.gameIcon;
                    hasModMenu = Program.ModTwins.ModMenuEnabled;
                    hasModCrates = Program.ModTwins.ModCratesSupported;
                }
                else if (type == GameType.Titans)
                {
                    gameName = Program.ModTitans.gameName;
                    apiCredit = Program.ModTitans.apiCredit;
                    modOptions = Program.ModTitans.modOptions;
                    gameIcon = Program.ModTitans.gameIcon;
                    hasModMenu = Program.ModTitans.ModMenuEnabled;
                    hasModCrates = Program.ModTitans.ModCratesSupported;
                }
                else if (type == GameType.MoM)
                {
                    gameName = Program.ModMoM.gameName;
                    apiCredit = Program.ModMoM.apiCredit;
                    modOptions = Program.ModMoM.modOptions;
                    gameIcon = Program.ModMoM.gameIcon;
                    hasModMenu = Program.ModMoM.ModMenuEnabled;
                    hasModCrates = Program.ModMoM.ModCratesSupported;
                }
                else if (type == GameType.CTR)
                {
                    gameName = Program.ModCTR.gameName;
                    apiCredit = Program.ModCTR.apiCredit;
                    modOptions = Program.ModCTR.modOptions;
                    gameIcon = Program.ModCTR.gameIcon;
                    hasModMenu = Program.ModCTR.ModMenuEnabled;
                    hasModCrates = Program.ModCTR.ModCratesSupported;
                }
                else if (type == GameType.CNK)
                {
                    gameName = Program.ModCNK.gameName;
                    apiCredit = Program.ModCNK.apiCredit;
                    modOptions = Program.ModCNK.modOptions;
                    gameIcon = Program.ModCNK.gameIcon;
                    hasModMenu = Program.ModCNK.ModMenuEnabled;
                    hasModCrates = Program.ModCNK.ModCratesSupported;
                }
                else if (type == GameType.Crash1)
                {
                    gameName = Program.ModCrash1.gameName;
                    apiCredit = Program.ModCrash1.apiCredit;
                    modOptions = Program.ModCrash1.modOptions;
                    gameIcon = Program.ModCrash1.gameIcon;
                    hasModMenu = Program.ModCrash1.ModMenuEnabled;
                    hasModCrates = Program.ModCrash1.ModCratesSupported;
                }
                else if (type == GameType.Crash2)
                {
                    gameName = Program.ModCrash2.gameName;
                    apiCredit = Program.ModCrash2.apiCredit;
                    modOptions = Program.ModCrash2.modOptions;
                    gameIcon = Program.ModCrash2.gameIcon;
                    hasModMenu = Program.ModCrash2.ModMenuEnabled;
                    hasModCrates = Program.ModCrash2.ModCratesSupported;
                }
                else if (type == GameType.Crash3)
                {
                    gameName = Program.ModCrash3.gameName;
                    apiCredit = Program.ModCrash3.apiCredit;
                    modOptions = Program.ModCrash3.modOptions;
                    gameIcon = Program.ModCrash3.gameIcon;
                    hasModMenu = Program.ModCrash3.ModMenuEnabled;
                    hasModCrates = Program.ModCrash3.ModCratesSupported;
                }
                else if (type == GameType.TWOC)
                {
                    modOptions = new string[] { "No options available" };
                    gameName = "TWOC";
                    apiCredit = "No API available!";
                    gameIcon = null;
                    hasModMenu = false;
                    hasModCrates = false;
                }
                else if (type == GameType.Bash)
                {
                    modOptions = new string[] { "No options available" };
                    gameName = "Bash";
                    apiCredit = "No API available!";
                    gameIcon = null;
                    hasModMenu = false;
                    hasModCrates = false;
                }
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

                text_gameType.Text = gameName + " " + region_mod + " " + cons_mod + " detected!";
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
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            
        }

        void PrepareOptionsList()
        {
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
            // Set availability in Game classes (ModCratesSupported variable)
            ModCrateManagerForm modCrateManagerMenu = new ModCrateManagerForm();
            modCrateManagerMenu.Owner = Program.ModProgramForm;
            modCrateManagerMenu.Show();
        }

        public void OpenModMenu()
        {
            // Individual Game Mod Menu
            // Detailed settings UI for some games
            // Set availability in Game classes (ModMenuEnabled variable)
            switch (targetGame)
            {
                case GameType.CNK:
                    Program.ModCNK.OpenModMenu();
                    break;
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
