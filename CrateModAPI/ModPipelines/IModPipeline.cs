using System.ComponentModel;

namespace CrateModLoader
{
    interface IModPipeline
    {

        ModPipelineInfo Metadata { get; }

        BackgroundWorker AsyncWorker { get; set; }
        bool IsBusy { get; }

        bool DetectROM(string inputPath, out string titleID, out uint regionID);

        bool DetectFolder(string inputPath, out string titleID, out uint regionID);

        bool Detect(bool directoryMode, string inputPath, out string titleID, out uint regionID);

        void PreStart(bool inputDirectoryMode, bool outputDirectoryMode);

        void Extract(string inputPath, string outputPath);

        void Build(string inputPath, string outputPath);
    }
}
