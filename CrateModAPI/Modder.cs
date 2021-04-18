﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using CrateModLoader.ModProperties;
using CrateModAPI.Resources.Text;

namespace CrateModLoader
{
    /*
     * Adding a game:
     * 1. Make a new modder class that inherits this abstract class. (ensure that the modder's namespace is CrateModLoader.GameSpecific.??? to avoid bugs)
     * 2. Override its Game member with the appropriate info in the getter. 
     * 3. Override StartModProcess (at least, there are more modding functions that can be overriden but are optional).
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
        public List<ModCrate> EnabledModCrates = new List<ModCrate>();

        public bool ModMenuEnabled => Props.Count > 0;
        public bool ModCrateRegionCheck = false; // A game might require some type of verification (i.e. file integrity, region matching) before installing layer0 mod crates.
        public virtual bool CanPreloadGame => false;
        public virtual List<ConsoleMode> PreloadConsoles => null;

        public abstract Game Game { get; }

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
                                {
                                    chunkAttr = (ModMenuOnly)field.DeclaringType.GetCustomAttribute(typeof(ModMenuOnly), false);
                                }
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].ModMenuOnly = true;
                                }
                            }

                            if (Props[Props.Count - 1].AllowedConsoles == null)
                            {
                                ModAllowedConsoles ListConsoles = (ModAllowedConsoles)field.GetCustomAttribute(typeof(ModAllowedConsoles), false);
                                if (ListConsoles == null)
                                {
                                    ListConsoles = (ModAllowedConsoles)field.DeclaringType.GetCustomAttribute(typeof(ModAllowedConsoles), false);
                                }
                                if (ListConsoles != null)
                                {
                                    Props[Props.Count - 1].AllowedConsoles = ListConsoles.Allowed;
                                }
                            }

                            if (Props[Props.Count - 1].AllowedRegions == null)
                            {
                                ModAllowedRegions ListRegions = (ModAllowedRegions)field.GetCustomAttribute(typeof(ModAllowedRegions), false);
                                if (ListRegions == null)
                                {
                                    ListRegions = (ModAllowedRegions)field.DeclaringType.GetCustomAttribute(typeof(ModAllowedRegions), false);
                                }
                                if (ListRegions != null)
                                {
                                    Props[Props.Count - 1].AllowedRegions = ListRegions.Allowed;
                                }
                            }

                            if (!Props[Props.Count - 1].Hidden)
                            {
                                ModHidden chunkAttr = (ModHidden)field.GetCustomAttribute(typeof(ModHidden), false);
                                if (chunkAttr == null)
                                {
                                    chunkAttr = (ModHidden)field.DeclaringType.GetCustomAttribute(typeof(ModHidden), false);
                                }
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].ModMenuOnly = true;
                                }
                            }

                            if (string.IsNullOrWhiteSpace(Props[Props.Count - 1].Name))
                            {
                                Props[Props.Count - 1].Name = field.Name;
                            }
                            if (Game.TextClass != null)
                            {
                                foreach (MethodInfo text in Game.TextClass.GetRuntimeMethods())
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

        public abstract void StartModProcess();

        public virtual void StartPreload() { } // Optional method to preload variables and resources from the game to the Mod Menu+

        // Multithreading stuff

        //public List<Mod> Mods = new List<Mod>();
        //public List<IModParser> ModParsers = new List<IModParser>();
        //public List<ModPipeline> Pipelines = new List<ModPipeline>();
        public Random RandomState; // <- todo consolidate all Random() calls for multithreading!!
        public List<ModPropertyBase> ActiveProps = new List<ModPropertyBase>();
        public virtual bool AsyncProcess => false; // delete this after implementing async for all games
        public bool IsBusy { get; set; }
        public bool PassBusy { get; set; }
        public bool ProcessBusy { get; set; }
        public string ProcessMessage { get; set; }
        public int PassIterator { get; set; }
        public int PassCount { get; set; }

        public void UpdateProcessMessage(string msg)
        {
            ProcessMessage = msg;
        }

        public bool NeedsCachePass()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod.NeedsCachePass)
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
                if (Prop.TargetMod != null && (Prop.HasChanged || (Prop is ModPropOption opt && opt.Enabled)))
                {
                    ActiveProps.Add(Prop);
                }
            }

            //Console.WriteLine("Active Props: " + ActiveProps.Count);
        }

        public void BeforeCachePass()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                Prop.TargetMod.BeforeCachePass();
            }
        }
        public void StartCachePass(object value)
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                Prop.TargetMod.CachePass(value);
            }
        }
        public void BeforeModPass()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                Prop.TargetMod.BeforeModPass();
            }
        }
        public void StartModPass(object value)
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                Prop.TargetMod.ModPass(value);
            }
        }
        public void BeforeQuickPass()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                Prop.TargetMod.BeforeQuickPass();
            }
        }
        public void StartQuickPass(object value)
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                Prop.TargetMod.QuickPass(value);
            }
        }

        public void PopulateModsPipelines()
        {
            //Mods.Clear();
            //Pipelines.Clear();

            // Populate mod and pipeline list automatically from namespace
            Assembly asm = assembly;

            string nameSpace = GetType().Namespace;

            foreach (Type type in asm.GetTypes())
            {
                if (!string.IsNullOrEmpty(type.Namespace) && type.Namespace.Contains(nameSpace))
                {
                    if (type.IsAssignableFrom(typeof(Mod)))
                    {
                        Mod mod = (Mod)Activator.CreateInstance(type);
                        if (!mod.Hidden)
                        {
                            //Mods.Add(mod);
                        }
                    }
                    else if (type.IsAssignableFrom(typeof(ModPipeline)))
                    {
                        ModPipeline pipeline = (ModPipeline)Activator.CreateInstance(type);
                        //Pipelines.Add(pipeline);
                    }
                }
            }
        }


        public void StartAsyncProcess()
        {
            IsBusy = true;

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

            StartModProcess();
            while (ProcessBusy || PassBusy)
            {
                Thread.Sleep(100);
            }

            IsBusy = false;
        }

        private void AsyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;
            a.DoWork -= AsyncWorker_DoWork;
            a.RunWorkerCompleted -= AsyncWorker_RunWorkerCompleted;
            a.ProgressChanged -= AsyncWorker_ProgressChanged;
        }

        private void AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

    }
}
