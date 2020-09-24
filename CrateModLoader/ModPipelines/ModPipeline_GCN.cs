

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_GCN : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.GCN,
            Layer = 0,
            NeedsDetection = true,
        };

        public ModPipeline_GCN()
        {

        }

        public override bool DetectROM(string inputPath)
        {
            //todo
            return true;
        }

        public override bool DetectFolder(string inputPath)
        {
            //todo
            return true;
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