using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    public abstract class ModPipeline
    {

        public abstract ModPipelineInfo Metadata { get; }

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


        public abstract void Extract(string inputPath, string outputPath);

        public abstract void Build(string inputPath, string outputPath);

    }

    public class ModPipelineInfo
    {
        public ConsoleMode Console { get; set; }
        public int Layer { get; set; }

        public bool NeedsDetection = false;
        public bool CanExtractROM = true;
        public bool CanBuildROMfromROM = true;
        public bool CanBuildROMfromFolder = true;
    }
}