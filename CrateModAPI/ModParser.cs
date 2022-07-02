using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CrateModLoader.LevelAPI;

namespace CrateModLoader
{
    // Finds and parses files into API specifc classes
    public abstract class ModParser<T> : ModParserBase
    {
        public abstract List<string> Extensions { get; } // case-insensitive
        public virtual List<string> SecondaryList => null;
        public virtual bool SecondarySkip => true;
        public virtual bool DisableAsync => false;
        public Modder ExecutionSource;
        public override bool SkipParser { get; set; }
        public bool LoadOnly = false;
        private uint ErrorCount = 0;
        private bool StreamedParser = false;

        public ModParser(Modder source)
        {
            ExecutionSource = source;
            FoundFiles = new Dictionary<string, List<FileInfo>>();
            FoundMemFiles = new Dictionary<string, List<MemoryFile>>();
            if (Extensions != null)
            {
                foreach (string ext in Extensions)
                {
                    FoundFiles.Add(ext.ToLower(), new List<FileInfo>());
                    FoundMemFiles.Add(ext.ToLower(), new List<MemoryFile>());
                }
            }
            SkipParser = !CheckModsForType();
            StreamedParser = ExecutionSource.StreamedModder;
        }

        /// <summary>
        /// Load the file and return its type. File must be closed at the end!
        /// </summary>
        public abstract T LoadObject(string filePath);
        /// <summary>
        /// Save the type to the file. File must be closed at the end!
        /// </summary>
        public abstract void SaveObject(T thing, string filePath);
        // MemoryFile operations
        public virtual async Task<T> LoadObject(MemoryFile file, Dictionary<string, MemoryFile> AllFiles)
        {
            await new Task(null);
            throw new NotImplementedException();
        }
        public virtual async void SaveObject(T thing, MemoryFile file, Dictionary<string, MemoryFile> AllFiles)
        {
            await new Task(null);
            throw new NotImplementedException();
        }

        public override async Task StartPass(ModPass pass = ModPass.Mod)
        {
            IList<Task> editTaskList = new List<Task>();
            if (ExecutionSource.StreamedModder)
            {
                foreach (KeyValuePair<string, List<MemoryFile>> list in FoundMemFiles)
                {
                    foreach (MemoryFile file in list.Value)
                    {
                        editTaskList.Add(MemoryFileStartPass(file, pass));
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<string, List<FileInfo>> list in FoundFiles)
                {
                    foreach (FileInfo file in list.Value)
                    {
                        editTaskList.Add(FileStartPass(file, pass));
                    }
                }
            }
            
            await Task.WhenAll(editTaskList);
            editTaskList.Clear();
            Console.WriteLine("ModParser finished, errors: " + ErrorCount);
        }
        public override async Task FileStartPass(FileInfo file, ModPass pass = ModPass.Mod)
        {
            if (DisableAsync)
            {
                string filePath = file.FullName;
                try
                {
                    T thing = LoadObject(filePath);

                    if (thing == null)
                    {
                        ExecutionSource.PassIterator++;
                        return;
                    }

                    ExecutionSource.StartPass(thing, pass);

                    if (!LoadOnly)
                    {
                        SaveObject(thing, filePath);
                    }
                }
                catch
                {
                    ErrorCount++;
                    Console.WriteLine("ModParser Error: " + filePath);
                }
            }
            else
            {
                await Task.Run(
                () =>
                {
                    string filePath = file.FullName;
                    try
                    {
                        T thing = LoadObject(filePath);

                        if (thing == null)
                        {
                            ExecutionSource.PassIterator++;
                            return;
                        }

                        ExecutionSource.StartPass(thing, pass);

                        if (!LoadOnly)
                        {
                            SaveObject(thing, filePath);
                        }
                    }
                    catch
                    {
                        ErrorCount++;
                        Console.WriteLine("ModParser Error: " + filePath);
                    }
                }
                );
            }

            ExecutionSource.PassIterator++;
            //ExecutionSource.PassPercent = (int)((ExecutionSource.PassIterator / (float)ExecutionSource.PassCount) * 100f); //* ExecutionSource.PassPercentMod) + ExecutionSource.PassPercentAdd;
        }

        public override async Task MemoryFileStartPass(MemoryFile file, ModPass pass = ModPass.Mod)
        {
            if (DisableAsync)
            {
                try
                {
                    Task<T> task = LoadObject(file, ExecutionSource.ConsolePipeline.ExtractedFiles);
                    T thing = task.Result;

                    if (thing == null)
                    {
                        ExecutionSource.PassIterator++;
                        return;
                    }

                    ExecutionSource.StartPass(thing, pass);

                    if (!LoadOnly)
                    {
                        SaveObject(thing, file, ExecutionSource.ConsolePipeline.ExtractedFiles);
                    }
                }
                catch
                {
                    ErrorCount++;
                    Console.WriteLine("ModParser Error: " + file.FullName);
                }
            }
            else
            {
                try
                {
                    T thing = await LoadObject(file, ExecutionSource.ConsolePipeline.ExtractedFiles);

                    if (thing == null)
                    {
                        ExecutionSource.PassIterator++;
                        return;
                    }

                    ExecutionSource.StartPass(thing, pass);

                    if (!LoadOnly)
                    {
                        SaveObject(thing, file, ExecutionSource.ConsolePipeline.ExtractedFiles);
                    }
                }
                catch
                {
                    ErrorCount++;
                    Console.WriteLine("ModParser Error: " + file.FullName);
                }
            }

            ExecutionSource.PassIterator++;
            //ExecutionSource.PassPercent = (int)((ExecutionSource.PassIterator / (float)ExecutionSource.PassCount) * 100f); //* ExecutionSource.PassPercentMod) + ExecutionSource.PassPercentAdd;
        }

        public override bool AddFile(FileInfo file)
        {
            string extension = file.Extension.ToLower();
            if (FoundFiles.ContainsKey(extension))
            {
                if (SecondaryList != null && SecondaryList.Count > 0)
                {
                    if (SecondarySkip && !SecondaryList.Contains(file.Name))
                    {
                        FoundFiles[extension].Add(file);
                        return true;
                    }
                    else if (!SecondarySkip && SecondaryList.Contains(file.Name))
                    {
                        FoundFiles[extension].Add(file);
                        return true;
                    }
                }
                else
                {
                    FoundFiles[extension].Add(file);
                    return true;
                }
            }
            return false;
        }

        public override bool AddFile(MemoryFile file)
        {
            string[] splitPath = file.FullName.ToLower().Split('.');
            string extension = "." + splitPath[splitPath.Length - 1];
            if (FoundFiles.ContainsKey(extension))
            {
                if (SecondaryList != null && SecondaryList.Count > 0)
                {
                    if (SecondarySkip && !SecondaryList.Contains(file.FullName))
                    {
                        FoundMemFiles[extension].Add(file);
                        return true;
                    }
                    else if (!SecondarySkip && SecondaryList.Contains(file.FullName))
                    {
                        FoundMemFiles[extension].Add(file);
                        return true;
                    }
                }
                else
                {
                    FoundMemFiles[extension].Add(file);
                    return true;
                }
            }
            return false;
        }

        public sealed override LevelBase LoadLevel(string FileName)
        {
            LevelBase lev = null;
            try
            {
                T thing = LoadObject(FileName);

                lev = LoadLevel(thing);
            }
            catch
            {
                Console.WriteLine("ModParser Error: " + FileName);
            }
            return lev;
        }

        public virtual LevelBase LoadLevel(T data) { return null; }
        public virtual void SaveLevel(string Path) {  }

        bool CheckModsForType()
        {
            foreach (Mod mod in ExecutionSource.Mods)
            {
                Type thistype = mod.GetType();
                List<Type> types = new List<Type>(thistype.BaseType.GenericTypeArguments);
                if (types.Contains(typeof(T)))
                {
                    return true;
                }
            }
            return false;
        }


    }

    // Mod Parser with level editor suport
    public abstract class ModParser<T1, T2> : ModParser<T1> where T2 : Level<T1>, new()
    {
        public ModParser(Modder source) : base(source) { }
        public override bool IsLevelFile => true;

        public override LevelBase LoadLevel(T1 data)
        {
            T2 lev = new T2();
            lev.Load(data);
            return lev;
        }

        public override void SaveLevel(string Path)
        {
            
        }
    }
}
