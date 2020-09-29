using System;
using System.Reflection;
using System.ComponentModel;

namespace CrateModLoader
{
    public abstract class ModPipeline
    {

        public abstract ModPipelineInfo Metadata { get; }

        public BackgroundWorker AsyncWorker = null;
        public bool IsBusy { get { return AsyncWorker != null && AsyncWorker.IsBusy; } }

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