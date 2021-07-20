using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using CrateModLoader.Tools;

namespace CrateModLoader.ModPipelines
{
    public class ConsolePipeline_NDS : ConsolePipeline
    {

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.NDS,
            NeedsDetection = true,
            CanExtractROM = false,
            CanBuildROMfromFolder = false,
            CanOnlyReplaceFiles = false,
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\data\";
        private string TempProcessPath => ModLoaderGlobals.TempName + @"\";

        private BackgroundWorker GlobalWorker;

        public ConsolePipeline_NDS()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            if (!Path.GetExtension(inputPath).ToUpper().EndsWith("NDS"))
            {
                titleID = null;
                return false;
            }
            using (FileStream isoStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x100, FileOptions.SequentialScan))
            {
                using (BinaryReader reader = new BinaryReader(isoStream))
                {
                    try
                    {
                        reader.ReadBytes(0xC);
                        titleID = new string(reader.ReadChars(0x06));
                    }
                    catch
                    {
                        titleID = null;
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            // todo
            titleID = null;
            regionID = 0;
            return false;
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // use ndstool
            string extPath = TempProcessPath;
            string args = $"-c \"{outputPath}\" -9 {extPath + "arm9.bin"} -7 {extPath + "arm7.bin"} -y9 {extPath + "y9.bin"} -y7 {extPath + "y7.bin"} -d {extPath + "data"} -y {extPath + "overlay"} -t {extPath + "banner.bin"} -h {extPath + "header.bin"}";

            Process DetectProcess = new Process();
            DetectProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"ndstool.exe";
            DetectProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            DetectProcess.StartInfo.Arguments = args;
            DetectProcess.StartInfo.UseShellExecute = false;
            DetectProcess.StartInfo.RedirectStandardOutput = true;
            DetectProcess.StartInfo.CreateNoWindow = true;

            DetectProcess.Start();
            string outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();
        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // use ndstool
            Directory.CreateDirectory(TempPath);
            string extPath = TempProcessPath;

            string args = $"-x \"{inputPath}\" -9 {extPath + "arm9.bin"} -7 {extPath + "arm7.bin"} -y9 {extPath + "y9.bin"} -y7 {extPath + "y7.bin"} -d {extPath + "data"} -y {extPath + "overlay"} -t {extPath + "banner.bin"} -h {extPath + "header.bin"}";

            Process DetectProcess = new Process();
            DetectProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"ndstool.exe";
            DetectProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            DetectProcess.StartInfo.Arguments = args;
            DetectProcess.StartInfo.UseShellExecute = false;
            DetectProcess.StartInfo.RedirectStandardOutput = true;
            DetectProcess.StartInfo.CreateNoWindow = true;

            DetectProcess.Start();
            string outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();
        }

    }
}