using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrateModLoader
{
    public abstract class ModPipelineBase
    {
        public abstract bool SkipPipeline { get; set; }
        public abstract bool ModLayerReplaceOnly { get; }

        public abstract Task StartPipeline(PipelinePass pass);
        public abstract Task FileStartPipeline(FileInfo file, PipelinePass pass);

        public abstract bool AddFile(FileInfo file);
    }
}
