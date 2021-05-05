using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
        private Dictionary<string, List<FileInfo>> FoundFiles;
        public override bool SkipParser { get; set; }
        public bool LoadOnly = false;

        public ModParser(Modder source)
        {
            ExecutionSource = source;
            FoundFiles = new Dictionary<string, List<FileInfo>>();
            if (Extensions != null)
            {
                foreach (string ext in Extensions)
                {
                    FoundFiles.Add(ext.ToLower(), new List<FileInfo>());
                }
            }
            SkipParser = !CheckModsForType();
        }

        /// <summary>
        /// Load the file and return its type. File must be closed at the end!
        /// </summary>
        public abstract T LoadObject(string filePath);
        /// <summary>
        /// Save the type to the file. File must be closed at the end!
        /// </summary>
        public abstract void SaveObject(T thing, string filePath);


        public override async Task StartPass(ModPass pass = ModPass.Mod)
        {
            IList<Task> editTaskList = new List<Task>();
            foreach (KeyValuePair<string, List<FileInfo>> list in FoundFiles)
            {
                foreach (FileInfo file in list.Value)
                {
                    editTaskList.Add(FileStartPass(file, pass));
                }
            }
            await Task.WhenAll(editTaskList);
            editTaskList.Clear();
        }
        public override async Task FileStartPass(FileInfo file, ModPass pass = ModPass.Mod)
        {
            if (DisableAsync)
            {
                string filePath = file.FullName;
                try
                {
                    T thing = LoadObject(filePath);

                    ExecutionSource.StartPass(thing, pass);

                    if (!LoadOnly)
                    {
                        SaveObject(thing, filePath);
                    }
                }
                catch
                {
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

                        ExecutionSource.StartPass(thing, pass);

                        if (!LoadOnly)
                        {
                            SaveObject(thing, filePath);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("ModParser Error: " + filePath);
                    }
                }
                );
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
}
