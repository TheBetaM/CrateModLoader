using System;
using System.IO;
using System.Diagnostics;

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_XBOX360 : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.XBOX360,
            Layer = 0,
            NeedsDetection = true,
            CanBuildROMfromFolder = false,
            CanBuildROMfromROM = false,
        };

        public ModPipeline_XBOX360()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            titleID = null;
            regionID = 0;

            string args = "-i -x ";
            args += "\"" + inputPath + "\"";

            //Modified extract-xiso only extracts the executables to check

            Process ExtractorProcess = new Process();
            ExtractorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "extract-xiso.exe";
            ExtractorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            ExtractorProcess.StartInfo.Arguments = args;
            //ExtractorProcess.Start();

            ExtractorProcess.StartInfo.UseShellExecute = false;
            //ExtractorProcess.StartInfo.RedirectStandardOutput = true;
            ExtractorProcess.StartInfo.CreateNoWindow = true;
            ExtractorProcess.Start();
            //string outputMessage = ExtractorProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);

            ExtractorProcess.WaitForExit();

            if (Directory.Exists(ModLoaderGlobals.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputPath)))
            {
                Directory.Move(ModLoaderGlobals.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputPath), ModLoaderGlobals.TempPath);
            }

            if (Directory.Exists(ModLoaderGlobals.TempPath) && File.Exists(ModLoaderGlobals.TempPath + @"default.xex"))
            {
                string xargs = "-l ";
                xargs += "\"" + ModLoaderGlobals.TempPath + @"default.xex" + "\"";

                Process DetectProcess = new Process();
                DetectProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "xextool.exe";
                //DetectProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                DetectProcess.StartInfo.Arguments = xargs;
                DetectProcess.StartInfo.UseShellExecute = false;
                DetectProcess.StartInfo.RedirectStandardOutput = true;
                DetectProcess.StartInfo.CreateNoWindow = true;
                DetectProcess.Start();

                string xoutputMessage = DetectProcess.StandardOutput.ReadToEnd();
                //Console.WriteLine(outputMessage);

                DetectProcess.WaitForExit();

                if (xoutputMessage.Length > 200)
                {
                    string[] outputLines = xoutputMessage.Split('\n');

                    for (int i = 0; i < outputLines.Length; i++)
                    {
                        if (outputLines[i].Contains("Xex Name: "))
                        {
                            titleID = outputLines[i];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            titleID = null;
            if (File.Exists(inputPath + @"default.xex"))
            {
                string args = "-l ";
                args += "\"" + inputPath + @"default.xex" + "\"";

                Process DetectProcess = new Process();
                DetectProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "xextool.exe";
                //DetectProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                DetectProcess.StartInfo.Arguments = args;
                DetectProcess.StartInfo.UseShellExecute = false;
                DetectProcess.StartInfo.RedirectStandardOutput = true;
                DetectProcess.StartInfo.CreateNoWindow = true;
                DetectProcess.Start();

                string outputMessage = DetectProcess.StandardOutput.ReadToEnd();
                //Console.WriteLine(outputMessage);

                DetectProcess.WaitForExit();

                if (outputMessage.Length > 200)
                {
                    string[] outputLines = outputMessage.Split('\n');

                    for (int i = 0; i < outputLines.Length; i++)
                    {
                        if (outputLines[i].Contains("Xex Name: "))
                        {
                            titleID = outputLines[i];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override void Build(string inputPath, string outputPath)
        {
            //todo
            throw new NotImplementedException();
        }

        public override void Extract(string inputPath, string outputPath)
        {
            // TODO: add free space checks
            string args = "-x ";
            args += "\"" + inputPath + "\" ";

            Process ExtractorProcess = new Process();
            ExtractorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "extract-xiso.exe";
            ExtractorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            ExtractorProcess.StartInfo.Arguments = args;
            ExtractorProcess.Start();
            ExtractorProcess.WaitForExit();

            // this may not work with an arbitrary output path, since it assumes it's on the same drive
            Directory.Move(ModLoaderGlobals.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputPath), outputPath);
        }

    }
}