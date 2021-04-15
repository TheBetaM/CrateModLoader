using System;
using System.Reflection;
using System.ComponentModel;

namespace CrateModLoader
{
    // Extracts archives of game files (inside ROMs)
    public abstract class ModPipeline
    {

        public bool IsBusy { get; set; }

        /// <summary> Relative path to the extracted files' folder that starts with the temp folder's name. Ends with '\' </summary>
        public abstract string ProcessPath { get; }
        /// <summary> Folder path to extract game files to (only for use in console Pipelines & ModLoader) </summary>
        public abstract string TempPath { get; }

        public virtual bool Detect(string inputPath)
        {
            return false;
        }

        public virtual void PreStart()
        {

        }

        public abstract void Extract(string inputPath, string outputPath);

        public abstract void Build(string inputPath, string outputPath);

    }
}
