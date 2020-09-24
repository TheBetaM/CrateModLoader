using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    public abstract class ModPipeline
    {

        public abstract ModPipelineInfo Metadata { get; }

        public virtual bool DetectROM(string inputPath)
        {
            return false;
        }
        public virtual bool DetectFolder(string inputPath)
        {
            return false;
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