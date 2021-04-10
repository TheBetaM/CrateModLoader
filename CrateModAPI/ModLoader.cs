using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
//using System.Threading.Tasks.Dataflow;
using System.Reflection;
using CrateModAPI.Resources.Text;
using CrateModLoader.Tools.IO;
//using SharpFileSystem;
//using SharpFileSystem.FileSystems;

namespace CrateModLoader
{

    public class ModLoader
    {

        public Modder Modder;
        public Game Game;
        public ConsolePipeline Pipeline;
        public int RandomizerSeedBase = 0;
        public string InputPath = "";
        public string OutputPath = "";
        public bool GamePreloaded = false;

        public string TempPath = ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public static bool KeepTempFiles = false;
        public static Dictionary<Game, Assembly> SupportedGames;
        public static Dictionary<ConsolePipelineInfo, Type> SupportedConsoles;
        public List<ModCrate> SupportedMods = new List<ModCrate>();
        public int ModsActiveAmount
        {
            get
            {
                int amount = 0;
                foreach (var mod in SupportedMods)
                {
                    if (mod.IsActivated)
                        ++amount;
                }
                return amount;
            }
        }

        public event EventHandler<EventValueArgs<string>> ProcessMessageChanged;
        public event EventHandler InteractionEnable;
        public event EventHandler InteractionDisable;
        public event EventHandler<EventValueArgs<int>> ProcessProgressChanged;
        public event EventHandler ProcessFinished;
        public event EventHandler ModCratesUpdated;
        public event EventHandler<EventValueArgs<bool>> ResetGameEvent;
        public event EventHandler<EventValueArgs<bool>> SetProcessStartAllow;
        public event EventHandler<EventValueArgs<bool>> ModMenuUpdated;
        public event EventHandler<EventValueArgs<string>> LayoutChangeUnsupported;
        public event EventHandler<EventGameDetails> LayoutChangeSupported;

        public ModLoader()
        {

        }
        public ModLoader(Assembly[] assemblies)
        {
            CacheSupportedGames(assemblies);
            CachePipelines(assemblies);

            //Console.WriteLine("Supported games: " + ModLoaderGlobals.SupportedGames.Count);
            //Console.WriteLine("Supported consoles: " + ModLoaderGlobals.SupportedConsoles.Count);
        }

        /// <summary>
        /// Builds game ROM or copies game files to output path.
        /// </summary>
        /// <param name="inputPath">Input path of the game files</param>
        /// <param name="outputPath">Output path to folder or file</param>
        void BuildGame(string inputPath, string outputPath, BackgroundWorker worker)
        {
            if (Pipeline == null)
            {
                Console.WriteLine("Error: Pipeline not ready!");
                return;
            }

            bool directoryMode = IO_Common.PathIsFolder(outputPath);

            if (directoryMode)
            {
                //Directory Mode - just copy files to output
                DirectoryInfo di = new DirectoryInfo(inputPath);
                if (Pipeline.Metadata.Console == ConsoleMode.PS2)
                {
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
                }

                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                string pathparent = outputPath + @"\";
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Directory.CreateDirectory(pathparent + dir.Name);
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.CopyTo(pathparent + dir.Name + @"\" + file.Name);
                    }
                    IO_Common.Recursive_CopyFiles(dir, pathparent + dir.Name + @"\");
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.CopyTo(pathparent + file.Name);
                }
            }
            else
            {
                Pipeline.Build(inputPath, outputPath, worker);
            }

        }

        /// <summary>
        /// Extracts or copies game files using the set ModPipeline from given input path to given output path.
        /// </summary>
        /// <param name="inputPath">Input path to Game folder or file</param>
        /// <param name="outputPath">Output path of game files</param>
        void ExtractGame(string inputPath, string outputPath)
        {
            if (Pipeline == null)
            {
                Console.WriteLine("Error: Pipeline not ready!");
                return;
            }

            bool directoryMode = IO_Common.PathIsFolder(inputPath);

            if (directoryMode)
            {
                DirectoryInfo di = new DirectoryInfo(inputPath);
                if (!di.Exists)
                {
                    throw new IOException(ModLoaderText.Error_FolderNotAccessible);
                }

                Directory.CreateDirectory(outputPath);

                string pathparent = outputPath + @"\";
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Directory.CreateDirectory(pathparent + dir.Name);
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.CopyTo(pathparent + dir.Name + @"\" + file.Name);
                    }
                    IO_Common.Recursive_CopyFiles(dir, pathparent + dir.Name + @"\");
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.CopyTo(pathparent + file.Name);
                }
            }
            else
            {
                //Pipeline.Extract(inputPath, outputPath);
            }
        }

        /// <summary>
        /// Detects console and game from the given full path using cached ModPipeline and Game lists.
        /// </summary>
        public void DetectGame(string inputPath)
        {
            bool directoryMode = IO_Common.PathIsFolder(inputPath);

            Modder = null;
            Pipeline = null;
            GamePreloaded = false;
            SupportedMods = new List<ModCrate>();
            bool ConsoleDetected = false;
            string regionID;
            uint regionNumber;

            DeleteTempFiles(TempPath);

            try
            {
                foreach (KeyValuePair<ConsolePipelineInfo, Type> pair in SupportedConsoles)
                {
                    Pipeline = (ConsolePipeline)Activator.CreateInstance(pair.Value);
                    bool DetectResult = Pipeline.Detect(directoryMode, inputPath, out regionID, out regionNumber);
                    if (DetectResult)
                    {
                        ConsoleDetected = true;
                        SetGameType(regionID, pair.Key.Console, regionNumber);
                        break;
                    }
                    DeleteTempFiles(TempPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Detect Error: " + ex.Message);

                UpdateProcessMessage(ModLoaderText.Error_UnableToOpenGame);
                ResetGameSpecific(false);
                return;
            }

            DeleteTempFiles(TempPath);

            SetProcessStartAllow.Invoke(this, new EventValueArgs<bool>(ConsoleDetected));

            if (!ConsoleDetected)
            {
                ResetGameSpecific(false);
                UpdateProcessMessage(ModLoaderText.Error_UnknownGameROM);
            }
        }

        // NO LONGER USED IN FAVOR OF EXTRACTING AND PROCESSING AT THE SAME TIME!
        public void EditGame(BackgroundWorker worker)
        {
            //To make sure the seed matches
            ModLoaderGlobals.RandomizerSeed = RandomizerSeedBase;
            
            if (Modder != null && Modder.ModCrateRegionCheck)
            {
                ModCrates.VerifyModCrates(SupportedMods, Game.ShortName, Modder.GameRegion);
            }
            ModCrates.InstallLayerMods(SupportedMods, Pipeline.ExtractedPath, 0);
            if (Modder != null)
            {
                Modder.EnabledModCrates = new List<ModCrate>();
                foreach (ModCrate Mod in SupportedMods)
                {
                    if (Mod.IsActivated)
                    {
                        Modder.EnabledModCrates.Add(Mod);
                    }
                }
                ModCrates.InstallCrateSettings(SupportedMods, Modder);
                Modder.StartModProcess();
            }
        }

        // New process method
        private int ExtractIterator = 0;
        private int ExtractFileCount = 0;
        private bool isExtracting = false;

        public void ExtractGame2(string inputPath, string outputPath, BackgroundWorker worker)
        {
            bool directoryMode = IO_Common.PathIsFolder(inputPath);

            if (directoryMode)
            {
                int fileCount = 0;
                DirectoryInfo di = new DirectoryInfo(inputPath);
                if (!di.Exists)
                    throw new IOException(ModLoaderText.Error_FolderNotAccessible);

                Dictionary<string, string> CopyList = new Dictionary<string, string>();
                Directory.CreateDirectory(outputPath);

                string pathparent = outputPath + @"\";
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Directory.CreateDirectory(pathparent + dir.Name);
                    foreach (FileInfo file in dir.EnumerateFiles())
                        CopyList.Add(file.FullName, pathparent + dir.Name + @"\" + file.Name);
                    Recursive_ListFiles(dir, pathparent + dir.Name + @"\", ref CopyList);
                }
                foreach (FileInfo file in di.EnumerateFiles())
                    CopyList.Add(file.FullName, pathparent + file.Name);

                fileCount = CopyList.Count;
                ExtractFileCount = CopyList.Count;

                // Copy all files
                /*
                int fileIterator = 1;
                foreach(KeyValuePair<string, string> Path in CopyList)
                {
                    int p = (int)((fileIterator / (float)fileCount) * 25f);
                    if (p > 25) p = 25;
                    worker.ReportProgress(1 + p);
                    File.Copy(Path.Key, Path.Value);
                    fileIterator++;
                }
                */
                isExtracting = true;
                ExtractIterator = 0;
                CopyFilesAsync(CopyList, worker);
            }
            else
            {
                Pipeline.Extract(inputPath, outputPath, worker);
            }

        }

        private async void CopyFilesAsync(Dictionary<string, string> Paths, BackgroundWorker worker)
        {
            IList<Task> writeTaskList = new List<Task>();
            foreach (KeyValuePair<string, string> Path in Paths)
            {
                writeTaskList.Add(CopyFileAsync(Path.Key, Path.Value, worker));
            }
            await Task.WhenAll(writeTaskList);
            isExtracting = false;
        }
        private async Task CopyFileAsync(string from, string to, BackgroundWorker worker)
        {
            using (Stream source = File.Open(from, FileMode.Open))
            {
                using (Stream destination = File.Create(to))
                {
                    await source.CopyToAsync(destination);
                }
            }
            ExtractIterator++;
            int p = (int)((ExtractIterator / (float)ExtractFileCount) * 25f);
            if (p > 25) p = 25;
            worker.ReportProgress(1 + p);
        }

        public static void Recursive_ListFiles(DirectoryInfo di, string pathparent, ref Dictionary<string, string> Paths)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Directory.CreateDirectory(pathparent + dir.Name);
                string pathchild = pathparent + dir.Name + @"\";
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    Paths.Add(file.FullName, pathparent + file.Name);
                }
                Recursive_ListFiles(dir, pathchild, ref Paths);
            }
        }

        void DeleteTempFiles(string Path)
        {
            if (Directory.Exists(Path))
            {
                DirectoryInfo di = new DirectoryInfo(Path);

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

        public void StartProcess()
        {
            InteractionDisable.Invoke(this, null);

            BackgroundWorker asyncWorker = new BackgroundWorker();
            asyncWorker.WorkerReportsProgress = true;
            asyncWorker.DoWork += new DoWorkEventHandler(AsyncWorker_DoWork);
            asyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AsyncWorker_RunWorkerCompleted);
            asyncWorker.ProgressChanged += new ProgressChangedEventHandler(AsyncWorker_ProgressChanged);
            asyncWorker.RunWorkerAsync();
        }

        private void AsyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;
            string inputPath = InputPath;
            //string tempPath = ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
            string outputPath = OutputPath;
            bool inputDirectoryMode = IO_Common.PathIsFolder(inputPath);
            bool outputDirectoryMode = IO_Common.PathIsFolder(outputPath);

            a.ReportProgress(0);

            DeleteTempFiles(TempPath);
            Pipeline.PreStart(inputDirectoryMode, outputDirectoryMode);

            a.ReportProgress(1);

            ExtractGame2(inputPath, TempPath, a);
            while (Pipeline.IsBusy || isExtracting) Thread.Sleep(100);

            a.ReportProgress(26);

            EditGame(a);

            a.ReportProgress(74);

            BuildGame(TempPath, outputPath, a);
            while (Pipeline.IsBusy) Thread.Sleep(100);

            a.ReportProgress(99);

            if (!KeepTempFiles)
            {
                DeleteTempFiles(TempPath);
            }
        }

        private void AsyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProcessProgressChanged.Invoke(this, new EventValueArgs<int>(100));
            ProcessFinished.Invoke(this, null);
            if (e.Error != null)
            {
                ProcessProgressChanged.Invoke(this, new EventValueArgs<int>(0));
                UpdateProcessMessage(ModLoaderText.Process_Error + " " + e.Error.Message);
            }
            else if (!e.Cancelled)
            {
                UpdateProcessMessage(ModLoaderText.Process_Finished);
            }
            else
            {
                ProcessProgressChanged.Invoke(this, new EventValueArgs<int>(0));
                UpdateProcessMessage(ModLoaderText.Process_Cancelled);
            }

            InteractionEnable.Invoke(this, null);

            BackgroundWorker a = sender as BackgroundWorker;
            a.DoWork -= AsyncWorker_DoWork;
            a.RunWorkerCompleted -= AsyncWorker_RunWorkerCompleted;
            a.ProgressChanged -= AsyncWorker_ProgressChanged;
        }

        private void AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProcessProgressChanged.Invoke(this, new EventValueArgs<int>(e.ProgressPercentage));
            if (e.ProgressPercentage == 0)
            {
                UpdateProcessMessage(ModLoaderText.Process_Step0);
            }
            else if (e.ProgressPercentage == 1)
            {
                UpdateProcessMessage(ModLoaderText.Process_Step1_ROM);
            }
            else if (e.ProgressPercentage < 26)
            {
                int ExtractProgress = (int)(((e.ProgressPercentage - 1f) / 25f) * 100f);
                UpdateProcessMessage($"{ModLoaderText.Process_Step1_ROM} ({ExtractProgress}%)");
            }
            else if (e.ProgressPercentage < 74)
            {
                UpdateProcessMessage(ModLoaderText.Process_Step2);
            }
            else if (e.ProgressPercentage == 74)
            {
                UpdateProcessMessage(ModLoaderText.Process_Step3_ROM);
            }
            else if (e.ProgressPercentage < 99)
            {
                int BuildProgress = (int)(((e.ProgressPercentage - 74f) / 25f) * 100f);
                UpdateProcessMessage($"{ModLoaderText.Process_Step3_ROM} ({BuildProgress}%)");
            }
            else if (e.ProgressPercentage == 99)
            {
                UpdateProcessMessage("Removing temporary files...");
            }
        }

        void CacheSupportedGames(Assembly[] assemblies)
        {
            SupportedGames = new Dictionary<Game, Assembly>();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract || !typeof(Modder).IsAssignableFrom(type)) // only get non-abstract modders
                        continue;
                    Modder modder = (Modder)Activator.CreateInstance(type);
                    Game game = modder.Game;
                    game.ModderClass = type;

                    SupportedGames.Add(game, assembly);
                }
            }
        }

        void CachePipelines(Assembly[] assemblies)
        {
            SupportedConsoles = new Dictionary<ConsolePipelineInfo, Type>();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract || !typeof(ConsolePipeline).IsAssignableFrom(type)) // only get non-abstract modders
                        continue;
                    ConsolePipeline pipeline = (ConsolePipeline)Activator.CreateInstance(type);
                    pipeline.Metadata.Assembly = assembly;

                    SupportedConsoles.Add(pipeline.Metadata, type);
                }
            }
        }

        void SetGameType(string serial, ConsoleMode console, uint RegionID)
        {
            bool RegionNotSupported = true;
            RegionCode TargetRegion = new RegionCode();
            Modder = null;

            SupportedMods = new List<ModCrate>();

            if (SupportedGames.Count <= 0)
            {
                Console.WriteLine("ERROR: No games supported!");
                return; 
            }

            foreach (KeyValuePair<Game, Assembly> pair in SupportedGames)
            {
                Game game = pair.Key;

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
                            Modder = (Modder)Activator.CreateInstance(game.ModderClass);
                            Game = game;
                            TargetRegion = r;
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
                            Modder = (Modder)Activator.CreateInstance(game.ModderClass);
                            Game = game;
                            TargetRegion = r;
                            break;
                        }
                    }
                }

                if (Modder != null)
                {
                    Modder.assembly = pair.Value;
                    Modder.ConsolePipeline = Pipeline;
                    Modder.GameRegion = TargetRegion;
                    break;
                }
            }

            string cons_mod = "";
            switch (console)
            {
                default: cons_mod = console.ToString(); break;
                case ConsoleMode.Undefined: cons_mod = "(" + ModLoaderText.UnknownConsole + ")"; break;
            }

            string region_mod = "(" + ModLoaderText.UnknownRegion + ")";
            if (!RegionNotSupported)
            {
                switch (TargetRegion.Region)
                {
                    case RegionType.NTSC_J: region_mod = "NTSC-J"; break;
                    case RegionType.NTSC_U: region_mod = "NTSC-U"; break;
                    case RegionType.PAL: region_mod = "PAL"; break;
                    case RegionType.Global: region_mod = ""; break;
                    default: region_mod = "(" + ModLoaderText.UnknownRegion + ")"; break;
                }
            }

            // UI stuff
            if (Modder == null)
            {
                LayoutChangeUnsupported.Invoke(this, new EventValueArgs<string>(cons_mod));
            }
            else
            {
                Modder.PopulateProperties();
                LayoutChangeSupported.Invoke(this, new EventGameDetails(Game, cons_mod, region_mod));
            }
        }

        public void UpdateProcessMessage(string txt)
        {
            ProcessMessageChanged.Invoke(this, new EventValueArgs<string>(txt));
        }
        public void UpdateModMenuChangedState(bool change)
        {
            ModMenuUpdated.Invoke(this, new EventValueArgs<bool>(change));
        }
        public void UpdateModCrateChangedState()
        {
            ModCratesUpdated.Invoke(this, null);
        }

        public void ResetGameSpecific(bool ClearGameText = false)
        {
            Modder = null;
            Pipeline = null;

            SupportedMods = new List<ModCrate>();

            ResetGameEvent.Invoke(this, new EventValueArgs<bool>(ClearGameText));
        }

        public void StartPreload()
        {
            InteractionDisable.Invoke(this, null);

            BackgroundWorker asyncWorker = new BackgroundWorker();
            asyncWorker.WorkerReportsProgress = true;
            asyncWorker.DoWork += new DoWorkEventHandler(Preload_DoWork);
            asyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Preload_RunWorkerCompleted);
            asyncWorker.ProgressChanged += new ProgressChangedEventHandler(Preload_ProgressChanged);
            asyncWorker.RunWorkerAsync();
        }

        private void Preload_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;
            string inputPath = InputPath;
            //string tempPath = ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
            //string outputPath = OutputPath;
            bool inputDirectoryMode = IO_Common.PathIsFolder(inputPath);
            //bool outputDirectoryMode = IO_Common.PathIsFolder(outputPath);

            a.ReportProgress(0);

            DeleteTempFiles(TempPath);
            //Pipeline.PreStart(inputDirectoryMode, outputDirectoryMode);

            a.ReportProgress(33);

            ExtractGame(inputPath, TempPath);
            while (Pipeline.IsBusy) Thread.Sleep(100);

            a.ReportProgress(66);

            PreloadGame();

            a.ReportProgress(90);

            if (!KeepTempFiles)
            {
                DeleteTempFiles(TempPath);
            }
        }

        public void PreloadGame()
        {
            if (Modder != null)
            {
                Modder.StartPreload();
            }
        }

        private void Preload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProcessProgressChanged.Invoke(this, new EventValueArgs<int>(100));
            ProcessFinished.Invoke(this, null);
            if (e.Error != null)
            {
                ProcessProgressChanged.Invoke(this, new EventValueArgs<int>(0));
                UpdateProcessMessage(ModLoaderText.Process_Error + " " + e.Error.Message);
            }
            else if (!e.Cancelled)
            {
                UpdateProcessMessage("Preload complete!");
                GamePreloaded = true;
                ModMenuUpdated.Invoke(this, new EventValueArgs<bool>(false));
            }
            else
            {
                ProcessProgressChanged.Invoke(this, new EventValueArgs<int>(0));
                UpdateProcessMessage(ModLoaderText.Process_Cancelled);
            }

            InteractionEnable.Invoke(this, null);

            BackgroundWorker a = sender as BackgroundWorker;
            a.DoWork -= Preload_DoWork;
            a.RunWorkerCompleted -= Preload_RunWorkerCompleted;
            a.ProgressChanged -= Preload_ProgressChanged;
        }

        private void Preload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProcessProgressChanged.Invoke(this, new EventValueArgs<int>(e.ProgressPercentage));
            if (e.ProgressPercentage == 0)
            {
                UpdateProcessMessage(ModLoaderText.Process_Step0);
            }
            else if (e.ProgressPercentage == 33)
            {
                UpdateProcessMessage(ModLoaderText.Process_Step1_ROM);
            }
            else if (e.ProgressPercentage == 66)
            {
                UpdateProcessMessage("Processing game files...");
            }
            else if (e.ProgressPercentage == 90)
            {
                UpdateProcessMessage("Removing temporary files...");
            }
        }
    }
}
