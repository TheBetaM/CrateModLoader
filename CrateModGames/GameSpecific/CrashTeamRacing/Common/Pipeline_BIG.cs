using System.Collections.Generic;
using System.IO;
using System;
using CTRFramework.Big;
using System.Threading.Tasks;
using System.Text;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Pipeline_BIG : ModPipeline
    {
        public Pipeline_BIG(Modder mod) : base(mod) { }

        public override string Name => "BIGFILE archive";
        public override List<string> Extensions => new List<string>() { ".BIG" };
        public override bool IsModLayer => true;
        public override int ModLayerID => 1;

        public override async Task ExtractObject(string filePath)
        {
            await Task.Run(
                () =>
                {
                    try
                    {
                        string fileName = Path.GetFileName(filePath);
                        string dirName = fileName.Substring(0, fileName.Length - 4);
                        string dirPath = filePath.Substring(0, (filePath.Length - fileName.Length)) + dirName + @"\";
                        //BigFile big = BigFile.FromFile(filePath);
                        //big.Extract(dirPath);
                        //big = null;
                        //File.Delete(filePath);

                        // Have to do it manually because CTRframework won't load the file lists here

                        List<BigEntry> Entries = new List<BigEntry>();
                        using (var reader = BigFileReader.FromFile(ExecutionSource.GameRegion.Region, filePath))
                        {
                            while (reader.NextFile())
                                Entries.Add(reader.ReadEntry());
                        }

                        StringBuilder biglist = new StringBuilder();

                        foreach (var entry in Entries)
                        {
                            string filename = Path.Combine(dirPath, entry.Name);
                            //Helpers.CheckFolder(Path.GetDirectoryName(filename));

                            biglist.AppendLine(entry.Name);

                            //this ensures we don't have dummy files
                            if (entry.Size > 0)
                                CTRFramework.Shared.Helpers.WriteToFile(filename, entry.Data);

                        }

                        CTRFramework.Shared.Helpers.WriteToFile(Path.ChangeExtension(filePath, "txt"), biglist.ToString());

                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
                );
        }

        public override async Task BuildObject(string filePath)
        {
            await Task.Run(
                () =>
                {
                    //string fileName = Path.GetFileName(filePath); //BIGFILE.BIG
                    //string dirName = fileName.Substring(0, fileName.Length - 4); // BIGFILE
                    string dirPath = filePath.Substring(0, (filePath.Length - 4)) + @"\"; // .../BIGFILE/
                    //string path_txt = dirName + ".TXT"; // BIGFILE.TXT
                    //string basePath = ExecutionSource.ConsolePipeline.ExtractedPath;
                    //string tempDir = Path.Combine(ModLoaderGlobals.BaseDirectory, dirName + @"\");

                    //File.Move(ModLoaderGlobals.BaseDirectory + ".txt", Path.Combine(basePath, path_txt));
                    //Directory.Move(dirPath, tempDir);

                    try
                    {
                        //BigFile big = BigFile.FromFile(Path.Combine(basePath, path_txt));
                        //big.Save(filePath);
                        //BigFile big = BigFile.FromFile(Path.ChangeExtension(filePath, ".txt"));

                        BigFile big = new BigFile();
                        string[] files = File.ReadAllLines(Path.ChangeExtension(filePath, ".txt"));

                        for (int i = 0; i < files.Length; i++)
                        {
                            files[i] = Path.Combine(dirPath, files[i]);
                            big.Entries.Add(new BigEntry(files[i]));
                        }

                        byte[] final_big = new byte[big.TotalSize];

                        using (var bw = new CTRFramework.Shared.BinaryWriterEx(new MemoryStream(final_big)))
                        {
                            bw.Write((int)0);
                            bw.Write(big.Entries.Count);

                            bw.Jump(3 * CTRFramework.Shared.Meta.SectorSize);

                            foreach (var entry in big.Entries)
                            {
                                int pos = (int)bw.BaseStream.Position;
                                entry.Offset = pos / CTRFramework.Shared.Meta.SectorSize;

                                bw.Write(entry.Data);

                                bw.Jump(pos + entry.SizePadded);
                            }

                            bw.Jump(8);

                            foreach (var entry in big.Entries)
                            {
                                bw.Write(entry.Offset);
                                bw.Write(entry.Size);
                            }

                            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                fs.SetLength(final_big.Length);
                                fs.Write(final_big, 0, final_big.Length);
                            }
                        }

                        big = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    //Directory.Move(tempDir, dirPath);
                    //File.Delete(Path.Combine(basePath, path_txt));
                    File.Delete(Path.ChangeExtension(filePath, ".txt"));

                    // Extraction cleanup
                    if (Directory.Exists(dirPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(dirPath);

                        foreach (FileInfo file in di.EnumerateFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.EnumerateDirectories())
                        {
                            dir.Delete(true);
                        }

                        Directory.Delete(dirPath);
                    }
                }
                );
        }
    }
}
