using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Media;
using DiscUtils;
using DiscUtils.Iso9660;

namespace CrateModLoader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static Randomizer RandoProgram;

        [STAThread]
        static void Main()
        {
            RandoProgram = new Randomizer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Randomizer
    {
        public long randoSeed = 0;
        public Label processText;
        public ProgressBar progressBar;
        public Button startButton;
        public int processProgress = 0;
        public Label text_gameType;
        public Label text_optionsLabel;
        public PictureBox image_gameIcon;
        public CheckedListBox list_modOptions;
        public Form main_form;

        public string inputISOpath = "";
        public string outputISOpath = "";
        public ConsoleMode isoType = ConsoleMode.Undefined;
        public GameType targetGame = GameType.Undefined;
        public RegionType targetRegion = RegionType.Undefined;
        public string extractedPath = "";
        public string PS2_executable_name = "";
        public bool loadedISO = false;
        public bool outputPathSet = false;
        public bool keepTempFiles = false;

        public Timer processTimer = new Timer();

        //ISO settings
        public string ISO_label;

        //CTTR specific
        public string path_RCF_default = "";
        public string path_RCF_common = "";
        public string path_RCF_frontend = "";
        public string path_executable = "";
        public string path_RCF_onfoot0 = "";
        public string path_RCF_onfoot1 = "";
        public string path_RCF_onfoot2 = "";
        public string path_RCF_onfoot3 = "";
        public string path_RCF_onfoot4 = "";
        public string path_RCF_onfoot5 = "";
        public string path_RCF_onfoot6 = "";
        public string path_RCF_onfoot7 = "";
        public string path_RCF_advent1 = "";
        public string path_RCF_advent2 = "";
        public string path_RCF_advent3 = "";
        public string path_RCF_adventa = "";
        public string path_RCF_dino1 = "";
        public string path_RCF_dino2 = "";
        public string path_RCF_dino3 = "";
        public string path_RCF_dinoa = "";
        public string path_RCF_egypt1 = "";
        public string path_RCF_egypt2 = "";
        public string path_RCF_egypt3 = "";
        public string path_RCF_egypta = ""; // PSP/PS2
        public string path_RCF_fairy1 = "";
        public string path_RCF_fairy2 = "";
        public string path_RCF_fairy3 = "";
        public string path_RCF_fairys = "";
        public string path_RCF_solar1 = "";
        public string path_RCF_solar2 = "";
        public string path_RCF_solar3 = "";
        public string path_RCF_solars = "";
        public string path_RCF_0 = "";
        public string path_RCF_1 = "";
        public string path_RCF_2 = "";
        public string path_RCF_3 = "";
        public string path_RCF_4 = "";
        public string path_RCF_5 = "";
        public string path_RCF_6 = "";
        public string path_RCF_sound = "";
        public string path_RCF_english = "";

        public bool CTTR_rand_hubs = false;
        public bool CTTR_rand_tracks = false;
        public bool CTTR_rand_minigames = false;
        public bool CTTR_rand_missions = false;
        public bool CTTR_add_unused_cutscenes = false;
        public bool CTTR_add_sequence_break_checks = false;

        public enum ConsoleMode
        {
            Undefined = -1,
            PSP = 0,
            PS2 = 1,
            XBOX = 2,
            GCN = 3,
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
            Bash = 10,
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
            CDBuilder isoBuild = new CDBuilder();
            isoBuild.UseJoliet = true;
            isoBuild.VolumeIdentifier = ISO_label;

            DirectoryInfo di = new DirectoryInfo(extractedPath);
            string stackedName = "";

            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                stackedName = dir.Name + @"\";
                isoBuild.AddDirectory(dir.Name);
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    isoBuild.AddFile(stackedName + file.Name, file.Open(FileMode.Open));
                }
                Recursive_AddDirs(ref isoBuild, dir, stackedName);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                isoBuild.AddFile(file.Name, file.Open(FileMode.Open));
            }

            isoBuild.Build(outputISOpath);
        }

        void Recursive_AddDirs(ref CDBuilder isoBuild, DirectoryInfo di, string sName)
        {
            string stackName = sName;
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                stackName = sName + dir.Name + @"\";
                //isoBuild.AddDirectory(dir.Name);
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    isoBuild.AddFile(stackName + file.Name, file.Open(FileMode.Open));
                }
                Recursive_AddDirs(ref isoBuild, dir, stackName);
            }
        }

        void LoadISO()
        {
            using (FileStream isoStream = File.Open(inputISOpath,FileMode.Open))
            {
                CDReader cd = new CDReader(isoStream, true);
                //add copying ISO settings to memory
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
                if (cd.GetDirectoryInfo("").GetFiles().Length > 0)
                {
                    foreach (string file in cd.GetFiles(""))
                    {
                        fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                        fileStreamTo = File.Open(extractedPath + "/" + file, FileMode.OpenOrCreate);
                        fileStreamFrom.CopyTo(fileStreamTo);
                        fileStreamFrom.Close();
                        fileStreamTo.Close();
                    }
                }

                cd.Dispose();
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

                processTimer.Tick += new EventHandler(DelayedProcessStart);
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
                //EditGameContent();
                startButton.Enabled = true; //DEBUG
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
            //Add modding process (duh)

            //ProgressProcess();
        }

        public void FinishISO()
        {

            CreateISO();

            /* TODO: delete temp files */
            if (!keepTempFiles)
            {
                if (Directory.Exists(extractedPath))
                {
                    DirectoryInfo di = new DirectoryInfo(extractedPath);

                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }

                    Directory.Delete(extractedPath);
                }
            }

            ProgressProcess();
        }

        public void ProcessFinished()
        {
            processText.Text = "Finished!";
            SystemSounds.Beep.Play();
            startButton.Enabled = true;
        }

        public void ErrorFinish(string errorType)
        {
            processProgress = 0;
            progressBar.Value = progressBar.Minimum;
            SystemSounds.Beep.Play();
            processText.Text = "Error: " + errorType;
            startButton.Enabled = true;
        }

        public void CheckISO()
        {
            using (FileStream isoStream = File.Open(inputISOpath, FileMode.Open))
            {
                if (!CDReader.Detect(isoStream))
                {
                    text_gameType.Text = "Unknown game ISO!";
                    return;
                }
                CDReader cd = new CDReader(isoStream, true);
                Stream fileStream;
                bool foundMetaData = false;
                if (cd.FileExists(@"SYSTEM.CNF"))
                {
                    fileStream = cd.OpenFile(@"SYSTEM.CNF", FileMode.Open);
                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        string input;
                        string titleID;
                        while (sr.Peek() > -1)
                        {
                            input = sr.ReadLine();
                            titleID = input;
                            if (titleID == @"BOOT2 = cdrom0:\SLUS_211.91;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.CTTR, RegionType.NTSC_U);
                                foundMetaData = true;
                                PS2_executable_name = "SLUS_211.91";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLES_534.39;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.CTTR, RegionType.PAL);
                                foundMetaData = true;
                                PS2_executable_name = "SLES_534.39";
                            }
                            else if (titleID == "ULES-00168") //Unknown, TODO
                            {
                                SetGameType(ConsoleMode.PS2, GameType.CTTR, RegionType.NTSC_J);
                                foundMetaData = false;
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLUS_209.09;1 " || titleID == @"BOOT2 = cdrom0:\SLUS_209.09;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.Twins, RegionType.NTSC_U);
                                foundMetaData = true;
                                PS2_executable_name = "SLUS_209.09";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLES_525.68;1 " || titleID == @"BOOT2 = cdrom0:\SLES_525.68;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.Twins, RegionType.PAL);
                                foundMetaData = true;
                                PS2_executable_name = "SLES_525.68";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLPM_658.01;1 " || titleID == @"BOOT2 = cdrom0:\SLPM_658.01;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.Twins, RegionType.NTSC_J);
                                foundMetaData = true;
                                PS2_executable_name = "SLPM_658.01";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLUS_215.83;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.Titans, RegionType.NTSC_U);
                                foundMetaData = true;
                                PS2_executable_name = "SLUS_215.83";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLES_548.41;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.Titans, RegionType.PAL);
                                foundMetaData = true;
                                PS2_executable_name = "SLES_548.41";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLUS_217.28;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.MoM, RegionType.NTSC_U);
                                foundMetaData = true;
                                PS2_executable_name = "SLUS_217.28";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLES_552.04;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.MoM, RegionType.PAL);
                                foundMetaData = true;
                                PS2_executable_name = "SLES_552.04";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLUS_206.49;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.NTSC_U);
                                foundMetaData = true;
                                PS2_executable_name = "SLUS_206.49";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLES_515.11;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.PAL);
                                foundMetaData = true;
                                PS2_executable_name = "SLES_515.11";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLPM_660.67;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.NTSC_J);
                                foundMetaData = true;
                                PS2_executable_name = "SLPM_660.67";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLUS_202.38;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.NTSC_U);
                                foundMetaData = true;
                                PS2_executable_name = "SLUS_202.38";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLES_503.86;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.PAL);
                                foundMetaData = true;
                                PS2_executable_name = "SLUS_202.38";
                            }
                            else if (titleID == @"BOOT2 = cdrom0:\SLPM_740.03;1")
                            {
                                SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.NTSC_J);
                                foundMetaData = true;
                                PS2_executable_name = "SLPM_740.03";
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (foundMetaData)
                    {
                        path_executable = PS2_executable_name;
                        path_RCF_default = @"ADEFAULT\DEFAULT.RCF";
                        path_RCF_advent1 = @"ADVENT\ADVENT1.RCF";
                        path_RCF_advent2 = @"ADVENT\ADVENT2.RCF";
                        path_RCF_advent3 = @"ADVENT\ADVENT3.RCF";
                        path_RCF_adventa = @"ADVENT\ADVENTA.RCF";
                        path_RCF_common = @"COMMON\COMMON.RCF";
                        path_RCF_dino1 = @"DINO\DINO1.RCF";
                        path_RCF_dino2 = @"DINO\DINO2.RCF";
                        path_RCF_dino3 = @"DINO\DINO3.RCF";
                        path_RCF_dinoa = @"DINO\DINOA.RCF";
                        path_RCF_egypt1 = @"EGYPT\EGYPT1.RCF";
                        path_RCF_egypt2 = @"EGYPT\EGYPT2.RCF";
                        path_RCF_egypt3 = @"EGYPT\EGYPT3.RCF";
                        path_RCF_egypta = @"EGYPT\EGYPTA.RCF";
                        path_RCF_english = @"ENGLISH.RCF";
                        path_RCF_fairy1 = @"FAIRY\FAIRY1.RCF";
                        path_RCF_fairy2 = @"FAIRY\FAIRY2.RCF";
                        path_RCF_fairy3 = @"FAIRY\FAIRY3.RCF";
                        path_RCF_fairys = @"FAIRY\FAIRYS.RCF";
                        path_RCF_frontend = @"COMMON\FRONTEND.RCF";
                        path_RCF_solar1 = @"SOLAR\SOLAR1.RCF";
                        path_RCF_solar2 = @"SOLAR\SOLAR2.RCF";
                        path_RCF_solar3 = @"SOLAR\SOLAR3.RCF";
                        path_RCF_solars = @"SOLAR\SOLARS.RCF";
                        path_RCF_onfoot0 = @"ONFOOT\ONFOOT.RCF";
                        path_RCF_onfoot1 = @"ONFOOT\ONFOOT1.RCF";
                        path_RCF_onfoot2 = @"ONFOOT\ONFOOT2.RCF";
                        path_RCF_onfoot3 = @"ONFOOT\ONFOOT3.RCF";
                        path_RCF_onfoot5 = @"ONFOOT\ONFOOT5.RCF";
                        path_RCF_onfoot6 = @"ONFOOT\ONFOOT6.RCF";
                    }
                }
                else if (cd.FileExists(@"UMD_DATA.BIN"))
                {
                    fileStream = cd.OpenFile(@"UMD_DATA.BIN", FileMode.Open);
                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        string input;
                        string titleID;
                        while (sr.Peek() > -1)
                        {
                            input = sr.ReadLine();
                            titleID = input.Substring(0, 10);
                            if (titleID == "ULUS-10044")
                            {
                                SetGameType(ConsoleMode.PSP, GameType.CTTR, RegionType.NTSC_U);
                                foundMetaData = true;
                            }
                            else if (titleID == "ULJM-05036")
                            {
                                SetGameType(ConsoleMode.PSP, GameType.CTTR, RegionType.NTSC_J);
                                foundMetaData = true;
                            }
                            else if (titleID == "ULES-00168")
                            {
                                SetGameType(ConsoleMode.PSP, GameType.CTTR, RegionType.PAL);
                                foundMetaData = true;
                            }
                            else if (titleID == "ULUS-10304")
                            {
                                SetGameType(ConsoleMode.PSP, GameType.Titans, RegionType.NTSC_U);
                                foundMetaData = true;
                            }
                            else if (titleID == "ULES-00917")
                            {
                                SetGameType(ConsoleMode.PSP, GameType.Titans, RegionType.PAL);
                                foundMetaData = true;
                            }
                            else if (titleID == "ULUS-10377")
                            {
                                SetGameType(ConsoleMode.PSP, GameType.MoM, RegionType.NTSC_U);
                                foundMetaData = true;
                            }
                            else if (titleID == "ULES-01171")
                            {
                                SetGameType(ConsoleMode.PSP, GameType.MoM, RegionType.PAL);
                                foundMetaData = true;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (foundMetaData)
                    {
                        path_executable = @"PSP_GAME\SYSDIR\BOOT.BIN";
                        path_RCF_default = @"PSP_GAME\USRDIR\adefault\default.rcf";
                        path_RCF_advent1 = @"PSP_GAME\USRDIR\advent\advent1.rcf";
                        path_RCF_advent2 = @"PSP_GAME\USRDIR\advent\advent2.rcf";
                        path_RCF_advent3 = @"PSP_GAME\USRDIR\advent\advent3.rcf";
                        path_RCF_adventa = @"PSP_GAME\USRDIR\advent\adventa.rcf";
                        path_RCF_common = @"PSP_GAME\USRDIR\common\common.rcf";
                        path_RCF_dino1 = @"PSP_GAME\USRDIR\dino\dino1.rcf";
                        path_RCF_dino2 = @"PSP_GAME\USRDIR\dino\dino2.rcf";
                        path_RCF_dino3 = @"PSP_GAME\USRDIR\dino\dino3.rcf";
                        path_RCF_dinoa = @"PSP_GAME\USRDIR\dino\dinoa.rcf";
                        path_RCF_egypt1 = @"PSP_GAME\USRDIR\egypt\egypt1.rcf";
                        path_RCF_egypt2 = @"PSP_GAME\USRDIR\egypt\egypt2.rcf";
                        path_RCF_egypt3 = @"PSP_GAME\USRDIR\egypt\egypt3.rcf";
                        path_RCF_egypta = @"PSP_GAME\USRDIR\egypt\egypta.rcf";
                        path_RCF_english = @"PSP_GAME\USRDIR\english.rcf";
                        path_RCF_fairy1 = @"PSP_GAME\USRDIR\fairy\fairy1.rcf";
                        path_RCF_fairy2 = @"PSP_GAME\USRDIR\fairy\fairy2.rcf";
                        path_RCF_fairy3 = @"PSP_GAME\USRDIR\fairy\fairy3.rcf";
                        path_RCF_fairys = @"PSP_GAME\USRDIR\fairy\fairys.rcf";
                        path_RCF_frontend = @"PSP_GAME\USRDIR\common\frontend.rcf";
                        path_RCF_solar1 = @"PSP_GAME\USRDIR\solar\solar1.rcf";
                        path_RCF_solar2 = @"PSP_GAME\USRDIR\solar\solar2.rcf";
                        path_RCF_solar3 = @"PSP_GAME\USRDIR\solar\solar3.rcf";
                        path_RCF_solars = @"PSP_GAME\USRDIR\solar\solars.rcf";
                        path_RCF_onfoot0 = @"PSP_GAME\USRDIR\onfoot\onfoot.rcf";
                        path_RCF_onfoot1 = @"PSP_GAME\USRDIR\onfoot\onfoot1.rcf";
                        path_RCF_onfoot2 = @"PSP_GAME\USRDIR\onfoot\onfoot2.rcf";
                        path_RCF_onfoot3 = @"PSP_GAME\USRDIR\onfoot\onfoot3.rcf";
                        path_RCF_onfoot4 = @"PSP_GAME\USRDIR\onfoot\onfoot4.rcf";
                        path_RCF_onfoot5 = @"PSP_GAME\USRDIR\onfoot\onfoot5.rcf";
                        path_RCF_onfoot6 = @"PSP_GAME\USRDIR\onfoot\onfoot6.rcf";
                    }
                }
                else if (cd.FileExists(@"default.xbe"))
                {
                    isoType = ConsoleMode.XBOX;
                    //TODO: figure out xbox checks
                }
                else if (cd.DirectoryExists("sys") && cd.FileExists(@"sys\boot.bin"))
                {
                    fileStream = cd.OpenFile(@"sys\boot.bin", FileMode.Open);
                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        string input;
                        string titleID;
                        while (sr.Peek() > -1)
                        {
                            input = sr.ReadLine();
                            titleID = input.Substring(0, 4);
                            if (titleID == "G9RE")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.CTTR, RegionType.NTSC_U);
                                foundMetaData = true;
                            }
                            else if (titleID == "G9RJ")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.CTTR, RegionType.NTSC_J);
                                foundMetaData = false;
                            }
                            else if (titleID == "G9RH" || titleID == "G9RD" || titleID == "G9RF" || titleID == "G9RP")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.CTTR, RegionType.PAL);
                                foundMetaData = false;
                            }
                            else if (titleID == "GCNE")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.CNK, RegionType.NTSC_U);
                                foundMetaData = false;
                            }
                            else if (titleID == "GCNP")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.CNK, RegionType.PAL);
                                foundMetaData = false;
                            }
                            else if (titleID == "GC8J")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.CNK, RegionType.NTSC_J);
                                foundMetaData = false;
                            }
                            else if (titleID == "GCBE")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.TWOC, RegionType.NTSC_U);
                                foundMetaData = false;
                            }
                            else if (titleID == "GCBP")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.TWOC, RegionType.PAL);
                                foundMetaData = false;
                            }
                            else if (titleID == "GCBJ")
                            {
                                SetGameType(ConsoleMode.GCN, GameType.TWOC, RegionType.NTSC_J);
                                foundMetaData = false;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (foundMetaData)
                    {
                        path_executable = @"sys\main.dol";
                        path_RCF_default = @"root\adefault\default.rcf";
                        path_RCF_advent1 = @"root\advent\advent1.rcf";
                        path_RCF_advent2 = @"root\advent\advent2.rcf";
                        path_RCF_advent3 = @"root\advent\advent3.rcf";
                        path_RCF_adventa = @"root\advent\adventa.rcf";
                        path_RCF_common = @"root\common\common.rcf";
                        path_RCF_dino1 = @"root\dino\dino1.rcf";
                        path_RCF_dino2 = @"root\dino\dino2.rcf";
                        path_RCF_dino3 = @"root\dino\dino3.rcf";
                        path_RCF_dinoa = @"root\dino\dinoa.rcf";
                        path_RCF_egypt1 = @"root\egypt\egypt1.rcf";
                        path_RCF_egypt2 = @"root\egypt\egypt2.rcf";
                        path_RCF_egypt3 = @"root\egypt\egypt3.rcf";
                        path_RCF_english = @"root\english.rcf";
                        path_RCF_fairy1 = @"root\fairy\fairy1.rcf";
                        path_RCF_fairy2 = @"root\fairy\fairy2.rcf";
                        path_RCF_fairy3 = @"root\fairy\fairy3.rcf";
                        path_RCF_fairys = @"root\fairy\fairys.rcf";
                        path_RCF_frontend = @"root\common\frontend.rcf";
                        path_RCF_solar1 = @"root\solar\solar1.rcf";
                        path_RCF_solar2 = @"root\solar\solar2.rcf";
                        path_RCF_solar3 = @"root\solar\solar3.rcf";
                        path_RCF_solars = @"root\solar\solars.rcf";
                        path_RCF_onfoot0 = @"root\onfoot\onfoot.rcf";
                        path_RCF_onfoot1 = @"root\onfoot\onfoot1.rcf";
                        path_RCF_onfoot2 = @"root\onfoot\onfoot2.rcf";
                        path_RCF_onfoot3 = @"root\onfoot\onfoot3.rcf";
                        path_RCF_onfoot5 = @"root\onfoot\onfoot5.rcf";
                        path_RCF_onfoot6 = @"root\onfoot\onfoot6.rcf";
                        path_RCF_onfoot7 = @"root\onfoot\onfoot7.rcf";
                    }
                }
                else
                {
                    foundMetaData = false;
                }

                if (!foundMetaData)
                {
                    text_gameType.Text = "Unknown game ISO!";
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
            else if (type == GameType.CTTR)
            {
                text_gameType.Text = "CTTR " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "CTTR Options: (API by NeoKesha)";
                image_gameIcon.Image = Properties.Resources.icon_crash;
                list_modOptions.Items.Clear();
                string[] modOptions_CTTR = { "Randomize hubs", "Randomize tracks", "Randomize minigames", "Randomize missions", "Add unused cutscenes", "Prevent sequence breaks" };
                list_modOptions.Items.AddRange(modOptions_CTTR);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.Twins)
            {
                text_gameType.Text = "Twinsanity " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "Twinsanity Options: (API by NeoKesha)";
                image_gameIcon.Image = Properties.Resources.icon_twins;
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.Titans)
            {
                text_gameType.Text = "Titans " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "Titans Options: (API by NeoKesha)";
                image_gameIcon.Image = Properties.Resources.icon_titans;
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.MoM)
            {
                text_gameType.Text = "MoM " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "MoM Options: (API by NeoKesha)";
                image_gameIcon.Image = Properties.Resources.icon_titans;
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.CTR)
            {
                text_gameType.Text = "CTR " + region_mod + " detected!";
                text_optionsLabel.Text = "CTR Options: (API by DCxDemo)";
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.CNK)
            {
                text_gameType.Text = "CNK " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "CNK Options: (API by BetaM, ManDude and eezstreet)";
                image_gameIcon.Image = Properties.Resources.icon_cnk;
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.Crash1)
            {
                text_gameType.Text = "Crash 1 " + region_mod + " detected!";
                text_optionsLabel.Text = "Crash 1 Options: (API by chekwob and ManDude)";
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.Crash2)
            {
                text_gameType.Text = "Crash 2 " + region_mod + " detected!";
                text_optionsLabel.Text = "Crash 2 Options: (API by chekwob and ManDude)";
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.Crash3)
            {
                text_gameType.Text = "Crash 3 " + region_mod + " detected!";
                text_optionsLabel.Text = "Crash 3 Options: (API by chekwob and ManDude)";
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.TWOC)
            {
                text_gameType.Text = "TWOC " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "TWOC Options: ";
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
            else if (type == GameType.Bash)
            {
                text_gameType.Text = "Bash " + region_mod + " detected!";
                text_optionsLabel.Text = "Bash Options: ";
                list_modOptions.Items.Clear();
                string[] modOptions_TS = { "No options available" };
                list_modOptions.Items.AddRange(modOptions_TS);
                list_modOptions.Visible = true;
                if (main_form.Size.Height < 380)
                {
                    main_form.Size = new System.Drawing.Size(main_form.Size.Width, 380);
                }
                main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, 380);
            }
        }
    }
}
