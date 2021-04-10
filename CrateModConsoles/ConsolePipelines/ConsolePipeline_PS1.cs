using DiscUtils.Iso9660;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using CrateModLoader.Tools;
using CrateModLoader.Tools.ISO;

namespace CrateModLoader.ModPipelines
{

    public class ConsolePipeline_PS1 : ConsolePipeline
    {

        public string ISO_Label = "";

        public override ConsolePipelineInfo Metadata => new ConsolePipelineInfo()
        {
            Console = ConsoleMode.PS1,
            NeedsDetection = true,
            CanBuildROMfromFolder = false, // original file required for label
        };

        public override string TempPath => ModLoaderGlobals.BaseDirectory + ModLoaderGlobals.TempName + @"\";
        public override string ProcessPath => ModLoaderGlobals.TempName + @"\";

        public ConsolePipeline_PS1()
        {
            ISO_Label = "";
        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            bool found = false;
            titleID = null;
            regionID = 0;
            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                CDReader cd;
                MemoryStream tempbin = null;

                if (Path.GetExtension(inputPath).ToLower() == ".bin") // PS1 CD image
                {
                    tempbin = new MemoryStream();
                    PSX2ISO.Run(isoStream, tempbin);
                    cd = new CDReader(tempbin, true);
                }
                else if (!CDReader.Detect(isoStream))
                {
                    return false;
                }
                else
                {
                    cd = new CDReader(isoStream, true);
                }

                if (cd.FileExists(@"SYSTEM.CNF"))
                {
                    using (StreamReader sr = new StreamReader(cd.OpenFile(@"SYSTEM.CNF", FileMode.Open)))
                    {
                        titleID = sr.ReadLine();
                        found = (titleID.Contains("BOOT ") || titleID.Contains("BOOT="));
                    }
                }
                else
                {
                    found = false;
                }

                cd.Dispose();

                if (tempbin != null)
                {
                    tempbin.Dispose();
                    tempbin.Close();
                }
            }
            if (!found)
            {
                titleID = null;
            }
            return found;
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            titleID = null;
            regionID = 0;
            bool found = false;
            string filePath = inputPath + @"SYSTEM.CNF";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    titleID = sr.ReadLine();
                    found = (titleID.Contains("BOOT ") || titleID.Contains("BOOT="));
                }
            }
            if (!found)
            {
                titleID = null;
            }
            return found;
        }

        public override void Build(string inputPath, string outputPath, BackgroundWorker worker)
        {
            // Use CDBuilder
            CDBuilder isoBuild = new CDBuilder();
            isoBuild.UseJoliet = true;
            isoBuild.VolumeIdentifier = ISO_Label;

            DirectoryInfo di = new DirectoryInfo(inputPath);
            HashSet<FileStream> files = new HashSet<FileStream>();

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                ISO_Common.Recursive_AddDirs(isoBuild, dir, dir.Name + @"\", files, true);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                ISO_Common.AddFile(isoBuild, file, string.Empty, files, true);
            }

            using (FileStream output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            using (Stream input = isoBuild.Build())
            {
                ISO2PSX.Run(input, output);
            }

            //isoBuild.Build(outputPath);

            foreach (FileStream file in files)
            {
                file.Close();
            }
        }

        public override void Extract(string inputPath, string outputPath, BackgroundWorker worker)
        {
            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                FileInfo isoInfo = new FileInfo(inputPath);
                CDReader cd;
                FileStream tempbin = null;
                if (Path.GetExtension(inputPath).ToLower() == ".bin") // PS1 CD image
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
                ISO_Label = cd.VolumeLabel;

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