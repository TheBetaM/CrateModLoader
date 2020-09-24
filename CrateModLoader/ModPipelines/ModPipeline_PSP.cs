

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