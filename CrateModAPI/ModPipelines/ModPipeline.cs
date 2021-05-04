using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CrateModLoader
{
    // Extracts/builds archives of game files (inside ROMs)
    public abstract class ModPipeline : ModPipelineBase
    {

        public abstract List<string> Extensions { get; } // case-insensitive
        public virtual List<string> SecondaryList { get; set; }
        public virtual bool SecondarySkip { get; set; }
        public virtual bool DisableAsync => false;
        public virtual bool IsModLayer => false;
        public virtual int ModLayerID => 0;
        public virtual bool ModLayerReplaceOnly => false;
        public virtual string Name { get; }
        public Modder ExecutionSource;
        private Dictionary<string, List<FileInfo>> FoundFiles;
        public override bool SkipPipeline { get; set; }

        public ModPipeline(Modder source)
        {
            ExecutionSource = source;
            FoundFiles = new Dictionary<string, List<FileInfo>>();
            foreach (string ext in Extensions)
            {
                FoundFiles.Add(ext.ToLower(), new List<FileInfo>());
            }
            SkipPipeline = false;
            SecondarySkip = true;
            //SkipPipeline = !CheckModsForType();
        }

        /// <summary>
        /// Extract archive at path to directory path that is named after the archive file.
        /// </summary>
        public abstract Task ExtractObject(string filePath);
        /// <summary>
        /// Rebuild archive at path from files in directory path named after the archive file.
        /// </summary>
        public abstract Task BuildObject(string filePath);


        public override async Task StartPipeline(PipelinePass pass)
        {
            //ExecutionSource.PassIterator = 0;
            IList<Task> editTaskList = new List<Task>();
            foreach (KeyValuePair<string, List<FileInfo>> list in FoundFiles)
            {
                foreach (FileInfo file in list.Value)
                {
                    editTaskList.Add(FileStartPipeline(file, pass));
                }
            }
            await Task.WhenAll(editTaskList);
            editTaskList.Clear();
        }
        public override async Task FileStartPipeline(FileInfo file, PipelinePass pass)
        {
            string filePath = file.FullName;
            if (DisableAsync)
            {
                try
                {
                    if (pass == PipelinePass.Extract)
                    {
                        ExtractObject(filePath);
                    }
                    else if (pass == PipelinePass.Build)
                    {
                        BuildObject(filePath);
                    }
                }
                catch
                {
                    Console.WriteLine("ModPipeline Error: " + filePath);
                }
            }
            else
            {
                try
                {
                    if (pass == PipelinePass.Extract)
                    {
                        await ExtractObject(filePath);
                    }
                    else if (pass == PipelinePass.Build)
                    {
                        await BuildObject(filePath);
                    }
                }
                catch
                {
                    Console.WriteLine("ModPipeline Error: " + filePath);
                }
            }

            if (IsModLayer)
            {
                string fileName = Path.GetFileName(filePath);
                string fileNameNoExt = Path.GetFileNameWithoutExtension(filePath);
                string dirPath = filePath.Substring(0, (filePath.Length - fileName.Length)) + fileNameNoExt + @"\";
                ModCrates.InstallLayerMods(ExecutionSource.EnabledModCrates, dirPath, ModLayerID, ModLayerReplaceOnly);
            }

            //ExecutionSource.PassIterator++;
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
            // Add a pipeline requirement for ModStructs? and check it here
            //foreach (ModPropertyBase Prop in ExecutionSource.ActiveProps)
            //{
                //if (Prop.TargetMod is ModStruct<T>)
                //{
                    //return true;
                //}
            //}
            return false;
        }

    }
}
