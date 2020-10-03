using DiscUtils.Iso9660;
using System;
using System.IO;
using System.Collections.Generic;
using CrateModLoader.Tools;
using CrateModLoader.Tools.ISO;

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_PS1 : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.PS1,
            Layer = 0,
            NeedsDetection = true,
            CanBuildROMfromFolder = false, // original file required for label
        };

        public ModPipeline_PS1()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            bool found = false;
            titleID = null;
            regionID = 0;
            using (FileStream isoStream = File.Open(ModLoaderGlobals.InputPath, FileMode.Open))
            {
                CDReader cd;
                FileStream tempbin = null;

                if (Path.GetExtension(ModLoaderGlobals.InputPath).ToLower() == ".bin") // PS1 CD image
                {
                    FileStream binconvout = new FileStream(ModLoaderGlobals.BaseDirectory + "binconvout.iso", FileMode.Create, FileAccess.Write);
                    PSX2ISO.Run(isoStream, binconvout);
                    binconvout.Close();
                    tempbin = new FileStream(ModLoaderGlobals.BaseDirectory + "binconvout.iso", FileMode.Open, FileAccess.Read);
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
                    File.Delete(ModLoaderGlobals.BaseDirectory + "binconvout.iso");
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

        public override void Build(string inputPath, string outputPath)
        {
            // Use CDBuilder
            CDBuilder isoBuild = new CDBuilder();
            isoBuild.UseJoliet = true;
            isoBuild.VolumeIdentifier = ModLoaderGlobals.ISO_Label;

            DirectoryInfo di = new DirectoryInfo(inputPath);
            HashSet<FileStream> files = new HashSet<FileStream>();

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                ISO_Common.Recursive_AddDirs(isoBuild, dir, dir.Name + @"\", files);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                ISO_Common.AddFile(isoBuild, file, string.Empty, files);
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

        public override void Extract(string inputPath, string outputPath)
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