using DiscUtils.Iso9660;
using System;
using System.Threading;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using CrateModLoader.Tools;

namespace CrateModLoader.ModPipelines
{

    public class ConsolePipeline_PSP : ConsolePipeline
    {

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.PSP,
            NeedsDetection = true,
            CanBuildROMfromFolder = false, // original file required for building
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\PSP_GAME\USRDIR\";

        public ConsolePipeline_PSP()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                if (!CDReader.Detect(isoStream))
                {
                    titleID = null;
                    return false;
                }
                else
                {
                    CDReader cd = new CDReader(isoStream, true);
                    titleID = null;
                    bool confirm = false;
                    if (cd.FileExists(@"UMD_DATA.BIN"))
                    {
                        using (StreamReader sr = new StreamReader(cd.OpenFile(@"UMD_DATA.BIN", FileMode.Open)))
                        {
                            titleID = sr.ReadLine().Substring(0, 10);
                        }
                        confirm = true;
                    }
                    cd.Dispose();
                    return confirm;
                }
            }
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            string filePath = inputPath + @"UMD_DATA.BIN";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    titleID = sr.ReadLine().Substring(0, 10);
                    return true;
                }
            }
            titleID = null;
            return false;
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // Use WQSG_UMD (requires input ROM)
            //File.Copy(ModLoaderGlobals.InputPath, ModLoaderGlobals.ToolsPath + "Game.iso");

            string args = "";
            args += @"--iso=";
            args += "\"" + ModLoaderGlobals.BaseDirectory + "/Tools/Game.iso\"";
            args += " --file=\"";
            args += inputPath + "PSP_GAME\"";
            //args += " --log";

            Process ISOcreatorProcess = new Process();
            ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "WQSG_UMD.exe";
            ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ISOcreatorProcess.StartInfo.Arguments = args;
            ISOcreatorProcess.Start();
            ISOcreatorProcess.WaitForExit();

            File.Move(ModLoaderGlobals.ToolsPath + "Game.iso", outputPath);
        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker)
        {
            //Needed to build
            File.Copy(inputPath, ModLoaderGlobals.ToolsPath + "Game.iso");

            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                FileInfo isoInfo = new FileInfo(inputPath);
                CDReader cd = new CDReader(isoStream, true);

                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                //Extracting ISO
                Stream fileStreamFrom = null;
                Stream fileStreamTo = null;
                if (cd.GetDirectories("").Length > 0)
                {
                    foreach (string directory in cd.GetDirectories(""))
                    {
                        Directory.CreateDirectory(outputPath + directory);
                        if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                        {
                            foreach (string file in cd.GetFiles(directory))
                            {
                                fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                                string filename = outputPath + file;
                                filename = filename.Replace(";1", string.Empty);
                                fileStreamTo = File.Open(filename, FileMode.OpenOrCreate);
                                fileStreamFrom.CopyTo(fileStreamTo);
                                fileStreamFrom.Close();
                                fileStreamTo.Close();
                            }
                        }
                        if (cd.GetDirectories(directory).Length > 0)
                        {
                            Recursive_CreateDirs(cd, directory, fileStreamFrom, fileStreamTo);
                        }
                    }
                }
                if (cd.GetDirectoryInfo("").GetFiles().Length > 0)
                {
                    foreach (string file in cd.GetFiles(""))
                    {
                        fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                        string filename = outputPath + "/" + file;
                        filename = filename.Replace(";1", string.Empty);
                        fileStreamTo = File.Open(filename, FileMode.OpenOrCreate);
                        fileStreamFrom.CopyTo(fileStreamTo);
                        fileStreamFrom.Close();
                        fileStreamTo.Close();
                    }
                }

                cd.Dispose();
            }
        }

        private void Recursive_CreateDirs(CDReader cd, string dir, Stream fileStreamFrom, Stream fileStreamTo)
        {
            foreach (string directory in cd.GetDirectories(dir))
            {
                Directory.CreateDirectory(TempPath + @"\" + directory);
                if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                {
                    foreach (string file in cd.GetFiles(directory))
                    {
                        fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                        fileStreamTo = File.Open(TempPath + @"\" + file, FileMode.OpenOrCreate);
                        fileStreamFrom.CopyTo(fileStreamTo);
                        fileStreamFrom.Close();
                        fileStreamTo.Close();
                    }
                }
                if (cd.GetDirectories(directory).Length > 0)
                {
                    Recursive_CreateDirs(cd, directory, fileStreamFrom, fileStreamTo);
                }
            }
        }

    }
}