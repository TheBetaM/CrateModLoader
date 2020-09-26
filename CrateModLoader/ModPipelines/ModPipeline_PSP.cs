using DiscUtils.Iso9660;
using System;
using System.IO;

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_PSP : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.PSP,
            Layer = 0,
            NeedsDetection = true,
        };

        public ModPipeline_PSP()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                if (!CDReader.Detect(isoStream))
                {
                    titleID = null;
                    return false;
                }
                else
                {
                    CDReader cd = new CDReader(isoStream, true);
                    titleID = null;
                    bool confirm = false;
                    if (cd.FileExists(@"UMD_DATA.BIN"))
                    {
                        using (StreamReader sr = new StreamReader(cd.OpenFile(@"UMD_DATA.BIN", FileMode.Open)))
                        {
                            titleID = sr.ReadLine().Substring(0, 10);
                        }
                        confirm = true;
                    }
                    cd.Dispose();
                    return confirm;
                }
            }
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            string filePath = inputPath + @"UMD_DATA.BIN";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    titleID = sr.ReadLine().Substring(0, 10);
                    return true;
                }
            }
            titleID = null;
            return false;
        }

        public override void Build(string inputPath, string outputPath)
        {
            //todo
        }

        public override void Extract(string inputPath, string outputPath)
        {
            //todo
        }

    }
}