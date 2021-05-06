using System;
using System.Reflection;
using System.ComponentModel;

namespace CrateModLoader
{
    // A mod pipeline describes a process of extracting, rebuilding and detecting the format of a game file/folder or a container (archive) of game files.
    public abstract class ConsolePipeline : IConsolePipeline
    {

        public abstract ConsolePipelineInfo Metadata { get; }

        public BackgroundWorker AsyncWorker { get; set; }
        public bool ForceBusy = false;
        public bool IsBusy { get { return (AsyncWorker != null && AsyncWorker.IsBusy) || ForceBusy; } }

        /// <summary> Full path to the extracted files' folder. Differs based on console, but always points to the same game data. Ends with '\' </summary>
        public string ExtractedPath
        {
            get
            {
                return ModLoaderGlobals.BaseDirectory + ProcessPath;
            }
        }

        /// <summary> Relative path to the extracted files' folder that starts with the temp folder's name. Ends with '\' </summary>
        public abstract string ProcessPath { get; }
        /// <summary> Folder path to extract game files to (only for use in console Pipelines & ModLoader) </summary>
        public abstract string TempPath { get; }
        /// <summary> Executable file name from the detected RegionCode struct of the currently loaded ROM. ex. "SLUS_209.09" </summary>
        //public abstract string ExecutablePath { get; } // todo

        public virtual bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            titleID = null;
            regionID = 0;
            return false;
        }

        public virtual bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            titleID = null;
            regionID = 0;
            return false;
        }

        public virtual bool Detect(bool directoryMode, string inputPath, out string titleID, out uint regionID)
        {
            if (directoryMode)
                return DetectFolder(inputPath, out titleID, out regionID);
            else
                return DetectROM(inputPath, out titleID, out regionID);
        }

        public virtual void PreStart(bool inputDirectoryMode, bool outputDirectoryMode)
        {
            if (!outputDirectoryMode)
            {
                if (inputDirectoryMode)
                {
                    if (!Metadata.CanBuildROMfromFolder)
                        throw new NotImplementedException("Building a ROM from a folder of this console is not supported yet.");
                }
                else
                {
                    if (!Metadata.CanBuildROMfromROM)
                        throw new NotImplementedException("Building a ROM from a ROM of this console is not supported yet.");
                }
            }
            else
            {
                if (!Metadata.CanBuildFolder)
                    throw new NotImplementedException("Building a folder of this console is not supported yet.");
            }
        }

        // Use background worker if the extractor can iterate over files
        public abstract void Extract(string inputPath, string outputPath, BackgroundWorker worker);

        // Use background worker if the builder can iterate over files
        public abstract void Build(string inputPath, string outputPath, BackgroundWorker worker);

        public void UpdateExtractProgress(BackgroundWorker worker, int FileCount, ref int FileIterator)
        {
            FileIterator++;
            int p = (int)((FileIterator / (float)FileCount) * 25f);
            if (p > 25) p = 25;
            worker.ReportProgress(1 + p);
        }

    }

    public class ConsolePipelineInfo
    {
        public ConsoleMode Console { get; set; }
        public Assembly Assembly { get; set; }

        public bool NeedsDetection = false;
        public bool CanExtractROM = true;
        public bool CanBuildROMfromROM = true;
        public bool CanBuildROMfromFolder = true;
        public bool CanBuildFolder = true;
        public bool CanOnlyReplaceFiles = true;
    }
}