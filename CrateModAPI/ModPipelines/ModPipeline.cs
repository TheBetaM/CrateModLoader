using System;
using System.Reflection;
using System.ComponentModel;

namespace CrateModLoader
{
    // A mod pipeline describes a process of extracting, rebuilding and detecting the format of a game file/folder or a container (archive) of game files.
    public abstract class ModPipeline : IModPipeline
    {

        public abstract ModPipelineInfo Metadata { get; }

        public BackgroundWorker AsyncWorker { get; set; }
        public bool IsBusy { get { return AsyncWorker != null && AsyncWorker.IsBusy; } }

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
                        throw new NotImplementedException();
                }
                else
                {
                    if (!Metadata.CanBuildROMfromROM)
                        throw new NotImplementedException();
                }
            }
            else
            {
                if (!Metadata.CanBuildFolder)
                    throw new NotImplementedException();
            }
        }

        public abstract void Extract(string inputPath, string outputPath);

        public abstract void Build(string inputPath, string outputPath);

    }

    public class ModPipelineInfo
    {
        public ConsoleMode Console { get; set; }
        public int Layer { get; set; }
        public Assembly Assembly { get; set; }

        public bool NeedsDetection = false;
        public bool CanExtractROM = true;
        public bool CanBuildROMfromROM = true;
        public bool CanBuildROMfromFolder = true;
        public bool CanBuildFolder = true;
    }
}