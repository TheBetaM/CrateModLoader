

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_WII : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.WII,
            Layer = 0,
            NeedsDetection = true,
        };

        public ModPipeline_WII()
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