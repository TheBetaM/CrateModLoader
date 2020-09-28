using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using CrateModLoader.Resources.Text;
using CrateModLoader.Tools.IO;

namespace CrateModLoader
{

    public class ModLoader
    {

        public ModLoaderForm main_form;
        public bool asyncBuild = false;
        public Modder Modder;
        public Game Game;
        public ModPipeline Pipeline;

        public ModLoader()
        {
            CacheSupportedGames();
            CachePipelines();
        }

        /// <summary>
        /// Builds game ROM or copies game files to output path.
        /// </summary>
        /// <param name="inputPath">Input path of the game files</param>
        /// <param name="outputPath">Output path to folder or file</param>
        void BuildGame(string inputPath, string outputPath)
        {
            bool directoryMode = IO_Common.PathIsFolder(outputPath);
            asyncBuild = false;

            if (directoryMode)
            {
                //Directory Mode - just copy files to output
                DirectoryInfo di = new DirectoryInfo(inputPath);
                if (ModLoaderGlobals.Console == ConsoleMode.PS2)
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
                Pipeline.Build(inputPath, outputPath);
                if (Pipeline.AsyncBuild)
                {
                    asyncBuild = true;
                    main_form.StartAsyncTimer();
                }
            }

        }

        /// <summary>
        /// Extracts or copies game files using the set ModPipeline from given input path to given output path.
        /// </summary>
        /// <param name="inputPath">Input path to Game folder or file</param>
        /// <param name="outputPath">Output path of game files</param>
        void ExtractGame(string inputPath, string outputPath)
        {
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
                Pipeline.Extract(inputPath, outputPath);
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
            ModCrates.ClearModLists();
            bool ConsoleDetected = false;
            string regionID;
            uint regionNumber;

            DeleteTempFiles();

            try
            {
                foreach (KeyValuePair<ModPipelineInfo, Type> pair in ModLoaderGlobals.SupportedConsoles)
                {
                    ModLoaderGlobals.Console = ConsoleMode.Undefined;
                    Pipeline = (ModPipeline)Activator.CreateInstance(pair.Value);
                    bool DetectResult = Pipeline.Detect(directoryMode, inputPath, out regionID, out regionNumber);
                    if (DetectResult)
                    {
                        ConsoleDetected = true;
                        SetGameType(regionID, pair.Key.Console, regionNumber);
                        if (Modder != null)
                        {
                            ModLoaderGlobals.ProductCode = regionID;
                            ModLoaderGlobals.ISO_Label = regionID;
                        }
                        break;
                    }
                    DeleteTempFiles();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Detect Error: " + ex.Message);
                main_form.UpdateGameTitleText(ModLoaderText.Error_UnableToOpenGame);
                ResetGameSpecific(false, true);
                return;
            }

            DeleteTempFiles();

            main_form.SetProcessStartAllowed(ConsoleDetected);

            if (!ConsoleDetected)
            {
                ResetGameSpecific(false, true);
                main_form.UpdateGameTitleText(ModLoaderText.Error_UnknownGameROM);
            }
        }

        public void EditGame()
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

        public void StartProcess()
        {
            main_form.DisableInteraction();

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
            string inputPath = ModLoaderGlobals.InputPath;
            string tempPath = ModLoaderGlobals.TempPath;
            string outputPath = ModLoaderGlobals.OutputPath;
            bool inputDirectoryMode = IO_Common.PathIsFolder(inputPath);
            bool outputDirectoryMode = IO_Common.PathIsFolder(outputPath);

            a.ReportProgress(0);

            DeleteTempFiles();
            Pipeline.PreStart(inputDirectoryMode, outputDirectoryMode);

            a.ReportProgress(25);

            ExtractGame(inputPath, tempPath);

            a.ReportProgress(50);

            EditGame();

            a.ReportProgress(75);

            BuildGame(tempPath, outputPath);

            if (asyncBuild)
            {
                while (asyncBuild)
                {
                    main_form.AsyncUpdate();
                }
            }

            a.ReportProgress(90);

            if (!ModLoaderGlobals.KeepTempFiles)
            {
                DeleteTempFiles();
            }

        }

        private void AsyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

            main_form.EnableInteraction();

            BackgroundWorker a = sender as BackgroundWorker;
            a.DoWork -= AsyncWorker_DoWork;
            a.RunWorkerCompleted -= AsyncWorker_RunWorkerCompleted;
            a.ProgressChanged -= AsyncWorker_ProgressChanged;
        }

        private void AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            main_form.UpdateProcessProgress(e.ProgressPercentage);
            if (e.ProgressPercentage == 0)
            {
                main_form.UpdateProcessText(ModLoaderText.Process_Step0);
            }
            else if (e.ProgressPercentage == 25)
            {
                main_form.UpdateProcessText(ModLoaderText.Process_Step1_ROM);
            }
            else if (e.ProgressPercentage == 50)
            {
                main_form.UpdateProcessText(ModLoaderText.Process_Step2);
            }
            else if (e.ProgressPercentage == 75)
            {
                main_form.UpdateProcessText(ModLoaderText.Process_Step3_ROM);
            }
            else if (e.ProgressPercentage == 90)
            {
                main_form.UpdateProcessText("Removing temporary files...");
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

        void SetGameType(string serial, ConsoleMode console, uint RegionID)
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

        public void Async_BuildFinished()
        {
            asyncBuild = false;
        }

        public void UpdateModMenuChangedState(bool change)
        {
            main_form.UpdateModMenuChangeState(change);
        }
        public void UpdateModCrateChangedState()
        {
            main_form.UpdateModCrateChangedState();
        }

        public void ResetGameSpecific(bool ClearGameText = false, bool ExtendedWindow = false)
        {
            Modder = null;
            Pipeline = null;
            ModLoaderGlobals.Console = ConsoleMode.Undefined;
            ModCrates.ClearModLists();

            main_form.ResetGameSpecific(ClearGameText, ExtendedWindow);
        }
    }
}
