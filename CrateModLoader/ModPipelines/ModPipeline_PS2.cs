using DiscUtils.Iso9660;
using System;
using System.IO;
using System.Collections.Generic;
using CrateModLoader.Tools;
using CrateModLoader.Tools.IO;
using CrateModLoader.Tools.ISO;

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_PS2 : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.PS2,
            Layer = 0,
            NeedsDetection = true,
        };

        public bool isCDimage = false;

        public ModPipeline_PS2()
        {
            isCDimage = false;
            AsyncBuild = false;
        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            titleID = null;
            bool found = false;
            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                CDReader cd;
                FileStream tempbin = null;

                if (Path.GetExtension(inputPath).ToLower() == ".bin") // PS2 CD image
                {
                    FileStream binconvout = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Create, FileAccess.Write);
                    PSX2ISO.Run(isoStream, binconvout);
                    binconvout.Close();
                    tempbin = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Open, FileAccess.Read);
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
                        found = titleID.Contains("BOOT2");
                    }
                }

                cd.Dispose();

                if (tempbin != null)
                {
                    tempbin.Dispose();
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso");
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
            regionID = 0;
            titleID = null;
            bool found = false;
            string filePath = inputPath + @"SYSTEM.CNF";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    titleID = sr.ReadLine();
                    found = titleID.Contains("BOOT2");
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
            if (!isCDimage)
            {
                AsyncBuild = true;
                //Use PS2ImageMaker
                DirectoryInfo di = new DirectoryInfo(inputPath);
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.MoveTo(file.FullName);
                    }
                    IO_Common.Recursive_RenameFiles(dir);
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.MoveTo(file.FullName);
                }
                
                PS2ImageMaker.StartPacking(inputPath, outputPath);
            }
            else
            {
                AsyncBuild = false;
                // Use CDBuilder
                CDBuilder isoBuild = new CDBuilder();
                isoBuild.UseJoliet = true;
                isoBuild.VolumeIdentifier = ModLoaderGlobals.ISO_Label;

                // CD image adjustments
                DirectoryInfo dit = new DirectoryInfo(inputPath);
                foreach (DirectoryInfo dir in dit.EnumerateDirectories())
                {
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.MoveTo(file.FullName);
                    }
                    IO_Common.Recursive_RenameFiles(dir);
                }
                foreach (FileInfo file in dit.EnumerateFiles())
                {
                    file.MoveTo(file.FullName);
                }

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

                foreach (FileStream file in files)
                {
                    file.Close();
                }
            }
        }

        public override void Extract(string inputPath, string outputPath)
        {
            using (FileStream isoStream = File.Open(inputPath, FileMode.Open))
            {
                FileInfo isoInfo = new FileInfo(inputPath);
                CDReader cd;
                FileStream tempbin = null;
                if (Path.GetExtension(inputPath).ToLower() == ".bin") // PS2 CD image
                {
                    FileStream binconvout = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Create, FileAccess.Write);
                    PSX2ISO.Run(isoStream, binconvout);
                    binconvout.Close();
                    tempbin = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso", FileMode.Open, FileAccess.Read);
                    cd = new CDReader(tempbin, true);
                    isCDimage = true;
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
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "binconvout.iso");
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