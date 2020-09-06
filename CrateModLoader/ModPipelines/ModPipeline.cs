using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    public abstract class ModPipeline
    {

        public bool CanExtractROM = true;
        public bool CanBuildROMfromROM = true;
        public bool CanBuildROMfromFolder = true;

        public abstract void Extract(string inputPath, string outputPath);

        public abstract void Build(string inputPath, string outputPath);

    }

    public class ModPipelineID : Attribute
    {
        public ConsoleMode ID { get; set; }
        public int Layer { get; set; }

        public ModPipelineID(ConsoleMode CategoryID, int LayerID)
        {
            ID = CategoryID;
            Layer = LayerID;
        }

    }
}