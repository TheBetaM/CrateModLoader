using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
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

        public List<Mod> Mods = new List<Mod>();
        public List<ModPipeline> Pipelines = new List<ModPipeline>();
        public List<ModPropertyBase> Props = new List<ModPropertyBase>();

        // External
        public Assembly assembly;
        public ModPipeline ConsolePipeline;
        public RegionCode GameRegion;

        public BackgroundWorker AsyncWorker = null;
        public bool ModMenuEnabled => Props.Count > 0;
        public bool ModCratesManualInstall = false; // A game might require some type of verification (i.e. file integrity, region matching) before installing layer0 mod crates.
        public bool IsBusy { get { return AsyncWorker != null && AsyncWorker.IsBusy; } }

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
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].Category = chunkAttr.ID;
                                }
                                else
                                {
                                    ModCategory typeAttr = (ModCategory)field.DeclaringType.GetCustomAttribute(typeof(ModCategory), false);
                                    if (typeAttr != null)
                                    {
                                        Props[Props.Count - 1].Category = typeAttr.ID;
                                    }
                                    else
                                    {
                                        Props[Props.Count - 1].Category = 0;
                                    }
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

        public void PopulateModsPipelines()
        {
            Mods.Clear();
            Pipelines.Clear();

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
                            Mods.Add(mod);
                        }
                    }
                    else if (type.IsAssignableFrom(typeof(ModPipeline)))
                    {
                        ModPipeline pipeline = (ModPipeline)Activator.CreateInstance(type);
                        Pipelines.Add(pipeline);
                    }
                }
            }
        }

        public abstract void StartModProcess();

        public void StartProcess()
        {
            AsyncWorker = new BackgroundWorker();
            AsyncWorker.WorkerReportsProgress = true;
            AsyncWorker.DoWork += new DoWorkEventHandler(Process);
            AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(ProcessProgressChanged);
            AsyncWorker.RunWorkerAsync();
        }

        public virtual void Process(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;

            foreach (Mod mod in Mods)
            {
                mod.BeforeProcess();
            }
            foreach (Mod mod in Mods)
            {
                mod.StartProcess();
            }

            bool Active = true;
            bool IsActive = false;
            int ModCount = 0;
            while (Active)
            {
                IsActive = false;
                ModCount = 0;
                foreach (Mod mod in Mods)
                {
                    if (mod.IsBusy)
                        IsActive = true;
                    else
                    {
                        ModCount++;
                    }
                }
                a.ReportProgress(ModCount / Mods.Count);
                if (!IsActive)
                    Active = false;
            }

        }

        private void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AsyncWorker = null;
        }

        private void ProcessProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker a = sender as BackgroundWorker;

            //ModLoaderGlobals.ModProgram.UpdateProcessMessage(string.Format("{0} {1}%",ModLoaderText.Process_Step2, e.ProgressPercentage));
        }

    }
}
