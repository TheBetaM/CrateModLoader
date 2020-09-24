

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_XBOX360 : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.XBOX360,
            Layer = 0,
            NeedsDetection = true,
        };

        public ModPipeline_XBOX360()
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