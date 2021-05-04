using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using CrateModLoader.ModProperties;
using CrateModAPI.Resources.Text;

namespace CrateModLoader
{
    /*
     * Adding a game:
     * 1. Make a new Modder class that inherits this abstract class. (ensure that the modder's namespace is CrateModLoader.GameSpecific.??? to avoid bugs)
     * 2. Make a new Game class that inherits the Game abstract class. 
     * 3. Override StartModProcess (there are more modding functions that can be overriden but are optional).
     * (optional) 4. Localize game title, API credit, and options using text resources.
     * (optional) 5. Create ModProperty variables for automatic Mod Menu setup.
     * (optional) 6. Add ModPropOption variables for quick options in the main window.
     * 7. Done.
     * 
     */

    /// <summary>
    /// Abstract Modder class, inherited by all game modders
    /// </summary>
    public abstract class Modder
    {
        public List<ModPropertyBase> Props = new List<ModPropertyBase>();

        // External
        public Assembly assembly;
        public ConsolePipeline ConsolePipeline;
        public RegionCode GameRegion;
        public Game SourceGame;
        public List<ModCrate> EnabledModCrates = new List<ModCrate>();

        public bool ModMenuEnabled => Props.Count > 0;
        public virtual bool ModCrateRegionCheck => false; // A game might require some type of verification (i.e. file integrity, region matching) before installing layer0 mod crates.
        public virtual bool CanPreloadGame => false;
        public virtual List<ConsoleMode> PreloadConsoles => null;
        /// <summary>
        /// Is the modder in the Preload phase
        /// </summary>
        public bool ModderIsPreloading { get; set; }
        /// <summary>
        /// Has the Preload phase been finished
        /// </summary>
        public bool ModderHasPreloaded = false;

        // Multithreading stuff

        public List<Mod> Mods = new List<Mod>();
        public List<ModParserBase> ModParsers = new List<ModParserBase>();
        public List<ModPipelineBase> Pipelines = new List<ModPipelineBase>();
        public List<ModPropertyBase> ActiveProps = new List<ModPropertyBase>();
        public virtual bool NoAsyncProcess => false;
        public bool IsBusy => ProcessBusy || PassBusy; //{ get; set; }
        public bool PassBusy { get; set; }
        public bool ProcessBusy { get; set; }
        public string ProcessMessage { get; set; }
        public int PassIterator { get; set; }
        public int PassCount { get; set; }
        public int PassPercent { get; set; }
        public GenericModStruct GenericModStruct { get; set; }

        public Modder() { }

        public void PopulateProperties()
        {
            Props.Clear();

            // Populate property list automatically from namespace
            Assembly asm = assembly;

            string nameSpace = GetType().Namespace;

            foreach (Type type in asm.GetTypes())
            {
                if (!string.IsNullOrEmpty(type.Namespace) && type.Namespace.Contains(nameSpace))
                {
                    foreach (FieldInfo field in type.GetFields())
                    {
                        if (field.FieldType.IsSubclassOf(typeof(ModPropertyBase)))
                        {
                            Props.Add((ModPropertyBase)field.GetValue(null));

                            Props[Props.Count - 1].ResetToDefault();

                            if (Props[Props.Count - 1].Category == null)
                            {
                                ModCategory chunkAttr = (ModCategory)field.GetCustomAttribute(typeof(ModCategory), false);
                                if (chunkAttr == null)
                                {
                                    chunkAttr = (ModCategory)field.DeclaringType.GetCustomAttribute(typeof(ModCategory), false);
                                }
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].Category = chunkAttr.ID;
                                }
                                else
                                {
                                    Props[Props.Count - 1].Category = 0;
                                }
                            }

                            if (!Props[Props.Count - 1].ModMenuOnly)
                            {
                                ModMenuOnly chunkAttr = (ModMenuOnly)field.GetCustomAttribute(typeof(ModMenuOnly), false);
                                if (chunkAttr == null)
                                    chunkAttr = (ModMenuOnly)field.DeclaringType.GetCustomAttribute(typeof(ModMenuOnly), false);
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].ModMenuOnly = true;
                                }
                            }

                            if (Props[Props.Count - 1].AllowedConsoles == null)
                            {
                                ModAllowedConsoles ListConsoles = (ModAllowedConsoles)field.GetCustomAttribute(typeof(ModAllowedConsoles), false);
                                if (ListConsoles == null)
                                    ListConsoles = (ModAllowedConsoles)field.DeclaringType.GetCustomAttribute(typeof(ModAllowedConsoles), false);
                                if (ListConsoles != null)
                                {
                                    Props[Props.Count - 1].AllowedConsoles = ListConsoles.Allowed;
                                }
                            }

                            if (Props[Props.Count - 1].AllowedRegions == null)
                            {
                                ModAllowedRegions ListRegions = (ModAllowedRegions)field.GetCustomAttribute(typeof(ModAllowedRegions), false);
                                if (ListRegions == null)
                                    ListRegions = (ModAllowedRegions)field.DeclaringType.GetCustomAttribute(typeof(ModAllowedRegions), false);
                                if (ListRegions != null)
                                {
                                    Props[Props.Count - 1].AllowedRegions = ListRegions.Allowed;
                                }
                            }

                            if (!Props[Props.Count - 1].Hidden)
                            {
                                ModHidden chunkAttr = (ModHidden)field.GetCustomAttribute(typeof(ModHidden), false);
                                if (chunkAttr == null)
                                    chunkAttr = (ModHidden)field.DeclaringType.GetCustomAttribute(typeof(ModHidden), false);
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].Hidden = true;
                                }
                            }

                            if (Props[Props.Count - 1].TargetMods == null)
                            {
                                ExecutesMods chunkAttr = (ExecutesMods)field.GetCustomAttribute(typeof(ExecutesMods), false);
                                if (chunkAttr == null)
                                    chunkAttr = (ExecutesMods)field.DeclaringType.GetCustomAttribute(typeof(ExecutesMods), false);
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].TargetMods = chunkAttr.Mods;
                                }
                            }

                            if (!Props[Props.Count - 1].RequiresPreload)
                            {
                                ModRequiresPreload chunkAttr = (ModRequiresPreload)field.GetCustomAttribute(typeof(ModRequiresPreload), false);
                                if (chunkAttr == null)
                                    chunkAttr = (ModRequiresPreload)field.DeclaringType.GetCustomAttribute(typeof(ModRequiresPreload), false);
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].RequiresPreload = true;
                                }
                            }

                            if (!Props[Props.Count - 1].PreloadBonus)
                            {
                                ModPreloadBonus chunkAttr = (ModPreloadBonus)field.GetCustomAttribute(typeof(ModPreloadBonus), false);
                                if (chunkAttr == null)
                                    chunkAttr = (ModPreloadBonus)field.DeclaringType.GetCustomAttribute(typeof(ModPreloadBonus), false);
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].RequiresPreload = true;
                                }
                            }

                            if (string.IsNullOrWhiteSpace(Props[Props.Count - 1].Name))
                            {
                                Props[Props.Count - 1].Name = field.Name;
                            }
                            if (SourceGame != null && SourceGame.TextClass != null)
                            {
                                foreach (MethodInfo text in SourceGame.TextClass.GetRuntimeMethods())
                                {
                                    if (text.Name == "get_" + field.Name)
                                    {
                                        Props[Props.Count - 1].Name = (string)text.Invoke(null, null);
                                    }
                                    else if (text.Name == "get_" + field.Name + "Desc")
                                    {
                                        Props[Props.Count - 1].Description = (string)text.Invoke(null, null);
                                    }
                                }
                            }
                            Props[Props.Count - 1].CodeName = field.Name;
                        }
                    }
                }
            }
        }

        public abstract void StartModProcess(); // Must put ProcessBusy = false; at the end!

        public void UpdateProcessMessage(string msg, int? per = null)
        {
            ProcessMessage = msg;
            if (per != null)
            {
                PassPercent = (int)per;
            }
        }

        public bool NeedsCachePass()
        {
            foreach (Mod mod in Mods)
            {
                if (mod.NeedsCachePass)
                {
                    return true;
                }
            }
            return false;
        }

        public void LoadActiveProps()
        {
            ActiveProps = new List<ModPropertyBase>();

            foreach (ModPropertyBase Prop in Props)
            {
                if (((Prop is ModPropOption opt && opt.Enabled)) || (!(Prop is ModPropOption) && Prop.HasChanged))
                {
                    ActiveProps.Add(Prop);
                }
            }

            GenericModStruct = new GenericModStruct()
            {
                Console = ConsolePipeline.Metadata.Console,
                Region = GameRegion.Region,
                ExecutableFileName = GameRegion.ExecName,
                ExtractedPath = ConsolePipeline.ExtractedPath,
            };

            Mods = new List<Mod>();
            List<Type> DupeCheck = new List<Type>();

            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMods != null)
                {
                    foreach (Type mod in Prop.TargetMods)
                    {
                        if (!DupeCheck.Contains(mod))
                        {
                            DupeCheck.Add(mod);
                            Mod NewMod = (Mod)Activator.CreateInstance(mod);
                            Mods.Add(NewMod);
                        }
                    }
                }
            }

            //Console.WriteLine("Active Props: " + ActiveProps.Count);
        }

        public void LoadPropsPreload()
        {
            ActiveProps = new List<ModPropertyBase>();
            Mods = new List<Mod>();
            List<Type> DupeCheck = new List<Type>();

            GenericModStruct = new GenericModStruct()
            {
                Console = ConsolePipeline.Metadata.Console,
                Region = GameRegion.Region,
                ExecutableFileName = GameRegion.ExecName,
                ExtractedPath = ConsolePipeline.ExtractedPath,
            };

            foreach (ModPropertyBase Prop in Props)
            {
                if (Prop.PreloadBonus || Prop.RequiresPreload)
                {
                    ActiveProps.Add(Prop);
                }
            }

            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMods != null)
                {
                    foreach (Type mod in Prop.TargetMods)
                    {
                        if (!DupeCheck.Contains(mod))
                        {
                            DupeCheck.Add(mod);
                            Mod NewMod = (Mod)Activator.CreateInstance(mod);
                            Mods.Add(NewMod);
                        }
                    }
                }
            }

            //Console.WriteLine("Active Props: " + ActiveProps.Count);
        }

        public void FindFiles(params ModParserBase[] parsers)
        {
            ModParsers = new List<ModParserBase>(parsers);
            ModParsers.Insert(0, new Parser_GenericMod(this));
            PassCount = 0;
            PassIterator = 0;
            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath);
            Recursive_LoadFiles(di);
        }
        // If you want to invoke Generic mods separately (note: Generic is automatically skipped if attempted to be executed twice)
        public void FindFiles(bool NoGeneric, params ModParserBase[] parsers)
        {
            ModParsers = new List<ModParserBase>(parsers);
            PassCount = 0;
            PassIterator = 0;
            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath);
            Recursive_LoadFiles(di);
        }
        public void FindArchives(params ModPipelineBase[] pipelines)
        {
            Pipelines = new List<ModPipelineBase>(pipelines);
            PassCount = 0;
            PassIterator = 0;
            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath);
            Recursive_LoadArchives(di);
        }

        void Recursive_LoadFiles(DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LoadFiles(dir);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                foreach (ModParserBase parser in ModParsers)
                {
                    if (!parser.SkipParser || parser.ForceParser)
                    {
                        bool add = parser.AddFile(file);
                        if (add) PassCount++;
                    }
                }
            }
        }
        void Recursive_LoadArchives(DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LoadArchives(dir);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                foreach (ModPipelineBase pipeline in Pipelines)
                {
                    if (!pipeline.SkipPipeline)
                    {
                        bool add = pipeline.AddFile(file);
                        if (add) PassCount++;
                    }
                }
            }
        }

        // todo: BeforePass could be triggered twice for each mod if there are separate new passes in a Modder
        public async Task StartNewPass()
        {
            ModPass CurrentPass = ModPass.Cache;
            if (!NeedsCachePass())
                CurrentPass = ModPass.Mod;
            if (ModderIsPreloading)
                CurrentPass = ModPass.Preload;

            PassBusy = true;

            while (CurrentPass < ModPass.End)
            {
                PassIterator = 0;
                if (CurrentPass ==  ModPass.Cache)
                {
                    UpdateProcessMessage("Cache Pass", 30);
                    BeforeCachePass();
                }
                else if (CurrentPass == ModPass.Mod)
                {
                    UpdateProcessMessage("Mod Pass", 50);
                    BeforeModPass();
                }
                else if (CurrentPass == ModPass.Preload)
                {
                    UpdateProcessMessage("Preload Pass", 15);
                    BeforePreloadPass();
                }

                IList<Task> editTaskList = new List<Task>();

                foreach (ModParserBase parser in ModParsers)
                {
                    if (!parser.SkipParser || parser.ForceParser)
                    {
                        editTaskList.Add(parser.StartPass(CurrentPass));
                    }
                }

                await Task.WhenAll(editTaskList);

                editTaskList.Clear();

                if (CurrentPass == ModPass.Cache)
                    AfterCachePass();
                else if (CurrentPass == ModPass.Mod)
                    AfterModPass();
                else if (CurrentPass == ModPass.Preload)
                    AfterPreloadPass();

                if (CurrentPass != ModPass.Preload)
                {
                    CurrentPass++;
                }
                else
                {
                    CurrentPass = ModPass.End;
                }
            }
            PassBusy = false;
        }

        public async Task StartPipelines(PipelinePass pass)
        {
            PassBusy = true;
            PassIterator = 0;
            PassCount = 0;
            if (pass == PipelinePass.Extract)
            {
                UpdateProcessMessage("Extracting archives...", 25);
            }
            else if (pass == PipelinePass.Build)
            {
                UpdateProcessMessage("Rebuilding archives...", 75);
            }

            IList<Task> TaskList = new List<Task>();

            foreach (ModPipeline pipeline in Pipelines)
            {
                if (!pipeline.SkipPipeline)
                {
                    TaskList.Add(pipeline.StartPipeline(pass));
                }
            }

            await Task.WhenAll(TaskList);

            TaskList.Clear();

            PassBusy = false;
        }

        public void BeforeCachePass()
        {
            foreach (Mod mod in Mods)
            {
                mod.BeforeCachePass();
            }
        }
        public void StartCachePass(object value)
        {
            foreach (Mod mod in Mods)
            {
                mod.CachePass(value);
            }
        }
        public void AfterCachePass()
        {
            foreach (Mod mod in Mods)
            {
                mod.AfterCachePass();
            }
        }
        public void BeforeModPass()
        {
            foreach (Mod mod in Mods)
            {
                mod.BeforeModPass();
            }
        }
        public void StartModPass(object value)
        {
            foreach (Mod mod in Mods)
            {
                mod.ModPass(value);
            }
        }
        public void AfterModPass()
        {
            foreach (Mod mod in Mods)
            {
                mod.AfterModPass();
            }
        }
        public void BeforePreloadPass()
        {
            foreach (Mod mod in Mods)
            {
                mod.BeforePreloadPass();
            }
        }
        public void StartPreloadPass(object value)
        {
            foreach (Mod mod in Mods)
            {
                mod.PreloadPass(value);
            }
        }
        public void AfterPreloadPass()
        {
            foreach (Mod mod in Mods)
            {
                mod.AfterPreloadPass();
            }
        }
        public void StartPass(object value, ModPass pass = ModPass.Mod)
        {
            switch (pass)
            {
                case ModPass.Cache:
                    StartCachePass(value);
                    break;
                case ModPass.Preload:
                    StartPreloadPass(value);
                    break;
                default:
                case ModPass.Mod:
                    StartModPass(value);
                    break;
            }
        }

        public void StartProcess(bool Preloading = false)
        {
            ProcessBusy = true;
            ModderIsPreloading = Preloading;

            // UI doesn't update until an await if this isn't here
            BackgroundWorker asyncWorker = new BackgroundWorker();
            asyncWorker.DoWork += new DoWorkEventHandler(AsyncWorker_DoWork);
            asyncWorker.RunWorkerAsync();
        }
        private void AsyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            StartModProcess();

            ModderHasPreloaded = true;
            if (NoAsyncProcess)
            {
                ProcessBusy = false;
            }
        }

    }
}
