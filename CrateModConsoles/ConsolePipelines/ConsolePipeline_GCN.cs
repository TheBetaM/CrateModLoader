using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace CrateModLoader.ModPipelines
{

    public class ConsolePipeline_GCN : ConsolePipeline
    {

        public string ProductCode = "";

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName;
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\P-" + ProductCode.Substring(0, 4) + @"\files\";

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.GCN,
            NeedsDetection = true,
            CanBuildROMfromFolder = false, // to fix: Incorrect root path because of missing product code folder ("/temp/files" instead of "/temp/<productCode>/files")
        };

        public ConsolePipeline_GCN()
        {
            ProductCode = "";
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
                    if (titleID.StartsWith("G"))
                    {
                        ProductCode = titleID;
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
                        ProductCode = titleID;
                        return true;
                    }
                }
            }
            titleID = null;
            return false;
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker, bool LegacyMethod)
        {
            // Use GCIT (Wiims ISO Tool doesn't work for this)
            string folderFix = ProductCode.Substring(0, 4);

            Directory.Move(inputPath + @"\P-" + folderFix + @"\files\", inputPath + @"\P-" + folderFix + @"\root\");

            string args = "";
            args += "\"" + inputPath + @"\P-" + folderFix + "\" -q -d ";
            args += "\"" + outputPath + "\" ";

            Process ISOcreatorProcess = new Process();
            ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gcit.exe";
            ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            ISOcreatorProcess.StartInfo.Arguments = args;
            //ISOcreatorProcess.StartInfo.UseShellExecute = false;
            //ISOcreatorProcess.StartInfo.RedirectStandardOutput = true;
            //ISOcreatorProcess.StartInfo.CreateNoWindow = true;
            ISOcreatorProcess.Start();

            //Console.WriteLine(ISOcreatorProcess.StandardOutput.ReadToEnd());

            ISOcreatorProcess.WaitForExit();
        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // TODO: add free space checks

            string outputPathFixed = outputPath.Substring(0, outputPath.Length - 1);

            string args = "extract ";
            args += "\"" + inputPath + "\" ";
            args += "\"" + outputPathFixed + "\" ";

            Process ExtractorProcess = new Process();
            ExtractorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wit.exe";
            ExtractorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ExtractorProcess.StartInfo.Arguments = args;
            ExtractorProcess.Start();
            ExtractorProcess.WaitForExit();
        }

    }
}