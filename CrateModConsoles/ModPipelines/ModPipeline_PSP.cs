using DiscUtils.Iso9660;
using System;
using System.IO;
using System.Diagnostics;
using CrateModLoader.Tools;

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_PSP : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.PSP,
            Layer = 0,
            NeedsDetection = true,
            CanBuildROMfromFolder = false, // original file required for building
        };

        public ModPipeline_PSP()
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

        public override void Build(string inputPath, string outputPath)
        {
            // Use WQSG_UMD (requires input ROM)
            File.Copy(ModLoaderGlobals.InputPath, ModLoaderGlobals.ToolsPath + "Game.iso");

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

        public override void Extract(string inputPath, string outputPath)
        {
            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                FileInfo isoInfo = new FileInfo(inputPath);
                CDReader cd;
                FileStream tempbin = null;
                if (Path.GetExtension(inputPath).ToLower() == ".bin")
                {
                    FileStream binconvout = new FileStream(ModLoaderGlobals.BaseDirectory + "binconvout.iso", FileMode.Create, FileAccess.Write);
                    PSX2ISO.Run(isoStream, binconvout);
                    binconvout.Close();
                    tempbin = new FileStream(ModLoaderGlobals.BaseDirectory + "binconvout.iso", FileMode.Open, FileAccess.Read);
                    cd = new CDReader(tempbin, true);
                }
                else
                {
                    cd = new CDReader(isoStream, true);
                }
                ModLoaderGlobals.ISO_Label = cd.VolumeLabel;

                /* Sometimes doesn't work?
                if (isoInfo.Length * 2 > GetTotalFreeSpace(ModLoaderGlobals.TempPath.Substring(0, 3)))
                {
                    cd.Dispose();
                    throw new IOException("Extraction error: Not enough hard drive space where this program is!");
                }
                if (isoInfo.Length * 2 > GetTotalFreeSpace(ModLoaderGlobals.OutputPath.Substring(0, 3)))
                {
                    cd.Dispose();
                    throw new IOException("Extraction error: Not enough hard drive space in the output path!");
                }
                */

                //fileStream = cd.OpenFile(@"SYSTEM.CNF", FileMode.Open);

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

                if (tempbin != null)
                {
                    tempbin.Dispose();
                    File.Delete(ModLoaderGlobals.BaseDirectory + "binconvout.iso");
                }
            }
        }

        private void Recursive_CreateDirs(CDReader cd, string dir, Stream fileStreamFrom, Stream fileStreamTo)
        {
            foreach (string directory in cd.GetDirectories(dir))
            {
                Directory.CreateDirectory(ModLoaderGlobals.TempPath + @"\" + directory);
                if (cd.GetDirectoryInfo(directory).GetFiles().Length > 0)
                {
                    foreach (string file in cd.GetFiles(directory))
                    {
                        fileStreamFrom = cd.OpenFile(file, FileMode.Open);
                        fileStreamTo = File.Open(ModLoaderGlobals.TempPath + @"\" + file, FileMode.OpenOrCreate);
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