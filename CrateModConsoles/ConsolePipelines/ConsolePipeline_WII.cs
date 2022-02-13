using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace CrateModLoader.ModPipelines
{

    public class ConsolePipeline_WII : ConsolePipeline
    {

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.WII,
            NeedsDetection = true,
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName;
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\DATA\files\";

        public ConsolePipeline_WII()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            string args = "ID6 ";
            args += "\"" + inputPath + "\"";

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
                    if (titleID.StartsWith("R"))
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

                    if (titleID.StartsWith("R"))
                    {
                        return true;
                    }
                }
            }
            titleID = null;
            return false;
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker, bool LegacyMethod)
        {
            // Use Wiimms ISO Tool
            string args = "copy ";
            args += "\"" + inputPath + "\" ";
            args += "\"" + outputPath + "\" ";

            Process ISOcreatorProcess = new Process();
            ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wit.exe";
            ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ISOcreatorProcess.StartInfo.Arguments = args;
            //ISOcreatorProcess.StartInfo.UseShellExecute = false;
            //ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
            //ISOcreatorProcess.StartInfo.CreateNoWindow = true;
            ISOcreatorProcess.Start();

            //Console.WriteLine(ISOcreatorProcess.StandardOutput.ReadToEnd());

            ISOcreatorProcess.WaitForExit();
        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker, bool LegacyMethod)
        {
            // TODO: add free space checks

            string args = "extract ";
            args += "\"" + inputPath + "\" ";
            args += "\"" + outputPath + "\" ";

            Process ExtractorProcess = new Process();
            ExtractorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wit.exe";
            ExtractorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ExtractorProcess.StartInfo.Arguments = args;
            ExtractorProcess.Start();
            ExtractorProcess.WaitForExit();
        }

    }
}