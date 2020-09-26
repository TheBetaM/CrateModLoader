using System;
using System.IO;
using System.Diagnostics;

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

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            string args = "ID6 ";
            args += "\"" + ModLoaderGlobals.InputPath + "\"";

            Process DetectProcess = new Process();
            DetectProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wit.exe";
            //DetectProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            DetectProcess.StartInfo.Arguments = args;
            DetectProcess.StartInfo.UseShellExecute = false;
            DetectProcess.StartInfo.RedirectStandardOutput = true;
            DetectProcess.StartInfo.CreateNoWindow = true;
            DetectProcess.Start();

            string outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);

            DetectProcess.WaitForExit();

            if (outputMessage.Length > 0 && outputMessage.Length <= 8)
            {
                titleID = outputMessage.Substring(0, 6);

                if (!string.IsNullOrWhiteSpace(titleID))
                {
                    if (titleID.StartsWith("G"))
                    {
                        return true;
                    }
                }
            }
            titleID = null;
            return false;
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            titleID = null;
            if (File.Exists(inputPath + @"sys/main.dol") && File.Exists(inputPath + @"sys/boot.bin"))
            {
                using (StreamReader sr = new StreamReader(inputPath + @"sys/boot.bin"))
                {
                    titleID = sr.ReadLine().Substring(0, 6);

                    if (titleID.StartsWith("G"))
                    {
                        return true;
                    }
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