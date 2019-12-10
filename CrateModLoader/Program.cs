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
        public static Modder_CNK ModCNK;
        public static Modder_CTTR ModCTTR;
        public static Modder_MoM ModMoM;
        public static Modder_Titans ModTitans;
        public static Modder_Twins ModTwins;

        [STAThread]
        static void Main()
        {
            RandoProgram = new Randomizer();
            ModCNK = new Modder_CNK();
            ModCTTR = new Modder_CTTR();
            ModMoM = new Modder_MoM();
            ModTitans = new Modder_Titans();
            ModTwins = new Modder_Twins();
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
                startButton.Enabled = true; //DEBUG, pressing the button again will progress
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

            /* TODO: delete temp files
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
            */

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

        public void OptionChanged(int option,bool value)
        {
            if (targetGame == GameType.CTTR)
            {
                Program.ModCTTR.OptionChanged(option,value);
            }
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
                        input = sr.ReadLine();
                        titleID = input;
                        foundMetaData = true;
                        if (titleID == @"BOOT2 = cdrom0:\SLUS_211.91;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CTTR, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_211.91";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_534.39;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CTTR, RegionType.PAL);
                            PS2_executable_name = "SLES_534.39";
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
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_525.68;1 " || titleID == @"BOOT2 = cdrom0:\SLES_525.68;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Twins, RegionType.PAL);
                            PS2_executable_name = "SLES_525.68";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLPM_658.01;1 " || titleID == @"BOOT2 = cdrom0:\SLPM_658.01;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Twins, RegionType.NTSC_J);
                            PS2_executable_name = "SLPM_658.01";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_215.83;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Titans, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_215.83";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_548.41;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.Titans, RegionType.PAL);
                            PS2_executable_name = "SLES_548.41";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_217.28;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.MoM, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_217.28";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_552.04;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.MoM, RegionType.PAL);
                            PS2_executable_name = "SLES_552.04";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_206.49;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_206.49";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_515.11;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.PAL);
                            PS2_executable_name = "SLES_515.11";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLPM_660.67;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.CNK, RegionType.NTSC_J);
                            PS2_executable_name = "SLPM_660.67";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLUS_202.38;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.NTSC_U);
                            PS2_executable_name = "SLUS_202.38";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLES_503.86;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.PAL);
                            PS2_executable_name = "SLUS_202.38";
                        }
                        else if (titleID == @"BOOT2 = cdrom0:\SLPM_740.03;1")
                        {
                            SetGameType(ConsoleMode.PS2, GameType.TWOC, RegionType.NTSC_J);
                            PS2_executable_name = "SLPM_740.03";
                        }
                        else
                        {
                            foundMetaData = false;
                        }
                    }
                    if (foundMetaData)
                    {
                        if (targetGame == GameType.CTTR)
                        {
                            Program.ModCTTR.SetPaths(ConsoleMode.PS2, PS2_executable_name);
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
                    if (foundMetaData)
                    {
                        if (targetGame == GameType.CTTR)
                        {
                            Program.ModCTTR.SetPaths(ConsoleMode.PSP);
                        }
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
                    if (foundMetaData)
                    {
                        if (targetGame == GameType.CTTR)
                        {
                            Program.ModCTTR.SetPaths(ConsoleMode.GCN);
                        }
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
                string[] modOptions = Program.ModCTTR.modOptions_CTTR;
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.Twins)
            {
                text_gameType.Text = "Twinsanity " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "Twinsanity Options: (API by NeoKesha)";
                image_gameIcon.Image = Properties.Resources.icon_twins;
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.Titans)
            {
                text_gameType.Text = "Titans " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "Titans Options: (API by NeoKesha)";
                image_gameIcon.Image = Properties.Resources.icon_titans;
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.MoM)
            {
                text_gameType.Text = "MoM " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "MoM Options: (API by NeoKesha)";
                image_gameIcon.Image = Properties.Resources.icon_titans;
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.CTR)
            {
                text_gameType.Text = "CTR " + region_mod + " detected!";
                text_optionsLabel.Text = "CTR Options: (API by DCxDemo)";
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.CNK)
            {
                text_gameType.Text = "CNK " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "CNK Options: (API by BetaM, ManDude and eezstreet)";
                image_gameIcon.Image = Properties.Resources.icon_cnk;
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.Crash1)
            {
                text_gameType.Text = "Crash 1 " + region_mod + " detected!";
                text_optionsLabel.Text = "Crash 1 Options: (API by chekwob and ManDude)";
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.Crash2)
            {
                text_gameType.Text = "Crash 2 " + region_mod + " detected!";
                text_optionsLabel.Text = "Crash 2 Options: (API by chekwob and ManDude)";
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.Crash3)
            {
                text_gameType.Text = "Crash 3 " + region_mod + " detected!";
                text_optionsLabel.Text = "Crash 3 Options: (API by chekwob and ManDude)";
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.TWOC)
            {
                text_gameType.Text = "TWOC " + region_mod + " " + cons_mod + " detected!";
                text_optionsLabel.Text = "TWOC Options: ";
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
            else if (type == GameType.Bash)
            {
                text_gameType.Text = "Bash " + region_mod + " detected!";
                text_optionsLabel.Text = "Bash Options: ";
                list_modOptions.Items.Clear();
                string[] modOptions = { "No options available" };
                list_modOptions.Items.AddRange(modOptions);
                PrepareOptionsList();
            }
        }

        void PrepareOptionsList()
        {
            int height = 290 + (list_modOptions.Items.Count * 15);
            list_modOptions.Visible = true;
            if (main_form.Size.Height < height)
            {
                main_form.Size = new System.Drawing.Size(main_form.Size.Width, height);
            }
            main_form.MinimumSize = new System.Drawing.Size(main_form.MinimumSize.Width, height);
        }
    }
}
