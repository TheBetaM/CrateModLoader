using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using CrateModLoader.Tools;

namespace CrateModLoader.ModPipelines
{
    // no viable options for this yet?
    public class ConsolePipeline_NDS : ConsolePipeline
    {

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.NDS,
            NeedsDetection = true,
            CanExtractROM = false,
            CanBuildROMfromFolder = false,
            CanOnlyReplaceFiles = false,
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\";

        private BackgroundWorker GlobalWorker;

        public ConsolePipeline_NDS()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            titleID = null;
            return false;
            if (!Path.GetExtension(inputPath).ToUpper().EndsWith("NDS"))
            {
                titleID = null;
                return false;
            }
            using (FileStream isoStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x100, FileOptions.SequentialScan))
            {
                using (BinaryReader reader = new BinaryReader(isoStream))
                {
                    try
                    {
                        reader.ReadBytes(0xC);
                        titleID = new string(reader.ReadChars(0x06));
                    }
                    catch
                    {
                        titleID = null;
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            // todo
            titleID = null;
            regionID = 0;
            return false;
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // todo
        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // todo
        }

    }
}