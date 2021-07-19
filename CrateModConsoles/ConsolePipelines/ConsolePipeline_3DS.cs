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
    public class ConsolePipeline_3DS : ConsolePipeline
    {

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.N3DS,
            NeedsDetection = true,
            CanOnlyReplaceFiles = false,
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\romfs\";
        private string TempProcessPath => ModLoaderGlobals.TempName + @"\";

        private BackgroundWorker GlobalWorker;

        public ConsolePipeline_3DS()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            titleID = null;
            bool is3DS = false;

            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    try
                    {
                        fileStream.Seek(0x100, SeekOrigin.Begin);
                        titleID = new string(reader.ReadChars(0x04));
                        if (titleID.Contains("NCSD"))
                        {
                            is3DS = true;
                            fileStream.Seek(0x1150 - 0x104, SeekOrigin.Current);
                            titleID = new string(reader.ReadChars(0x0A)); //CTR-P-XXXX
                        }
                        else
                        {
                            titleID = null;
                        }
                    }
                    catch
                    {
                        titleID = null;
                    }
                }
            }

            return is3DS;
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
            // use 3dstool
            string extPath = TempProcessPath;

            string args1 = $"-cvtf romfs {extPath + "romfs.bin"} --romfs-dir {extPath + "romfs"}";
            string args2 = $"-czvtf exefs {extPath + "exefs.bin"} --header {extPath + "exefsheader.bin"} --exefs-dir {extPath + "exefs"}";
            string args3 = $"-cvtf cxi {extPath + "0.cxi"} --header {extPath + "ncchheader.bin"} --exh {extPath + "exh.bin"} --logo {extPath + "logo.darc.lz"} --plain {extPath + "plain.bin"} --exefs {extPath + "exefs.bin"} --romfs {extPath + "romfs.bin"} --not-encrypt";
            string args4 = $"-cvt01267f cci {extPath + "0.cxi"} {extPath + "1.cfa"} {extPath + "2.cfa"} {extPath + "6.cfa"} {extPath + "7.cfa"} {outputPath} --header {extPath + "ncsdheader.bin"}";

            Process DetectProcess = new Process();
            DetectProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"3dstool.exe";
            DetectProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            DetectProcess.StartInfo.Arguments = args1;
            DetectProcess.StartInfo.UseShellExecute = false;
            DetectProcess.StartInfo.RedirectStandardOutput = true;
            DetectProcess.StartInfo.CreateNoWindow = true;

            DetectProcess.Start();
            string outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();

            DetectProcess.StartInfo.Arguments = args2;
            DetectProcess.Start();
            outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();

            DetectProcess.StartInfo.Arguments = args3;
            DetectProcess.Start();
            outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();

            DetectProcess.StartInfo.Arguments = args4;
            DetectProcess.Start();
            outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();

        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // use 3dstool
            Directory.CreateDirectory(TempPath);
            string extPath = TempProcessPath;

            string args1 = $"-xvt01267f cci {extPath + "0.cxi"} {extPath + "1.cfa"} {extPath + "2.cfa"} {extPath + "6.cfa"} {extPath + "7.cfa"} {inputPath} --header {extPath + "ncsdheader.bin"}";
            string args2 = $"-xvtf cxi {extPath + "0.cxi"} --header {extPath + "ncchheader.bin"} --exh {extPath + "exh.bin"} --logo {extPath + "logo.darc.lz"} --plain {extPath + "plain.bin"} --exefs {extPath + "exefs.bin"} --romfs {extPath + "romfs.bin"}";
            string args3 = $"-xuvtf exefs {extPath + "exefs.bin"} --header {extPath + "exefsheader.bin"} --exefs-dir {extPath + "exefs"}";
            string args4 = $"-xvtf romfs {extPath + "romfs.bin"} --romfs-dir {extPath + "romfs"}";

            Process DetectProcess = new Process();
            DetectProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"3dstool.exe";
            DetectProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            DetectProcess.StartInfo.Arguments = args1;
            DetectProcess.StartInfo.UseShellExecute = false;
            DetectProcess.StartInfo.RedirectStandardOutput = true;
            DetectProcess.StartInfo.CreateNoWindow = true;

            DetectProcess.Start();
            string outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();

            DetectProcess.StartInfo.Arguments = args2;
            DetectProcess.Start();
            outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();

            if (File.Exists(TempPath + "0.cxi"))
                File.Delete(TempPath + "0.cxi");

            DetectProcess.StartInfo.Arguments = args3;
            DetectProcess.Start();
            outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();

            if (File.Exists(TempPath + "exefs.bin"))
                File.Delete(TempPath + "exefs.bin");

            DetectProcess.StartInfo.Arguments = args4;
            DetectProcess.Start();
            outputMessage = DetectProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);
            DetectProcess.WaitForExit();

            if (File.Exists(TempPath + "romfs.bin"))
                File.Delete(TempPath + "romfs.bin");
        }

    }
}