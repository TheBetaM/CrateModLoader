using System.Collections.Generic;
using System.IO;
using System;
using CTRFramework.Big;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Pipeline_BIG : ModPipeline
    {
        public Pipeline_BIG(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".BIG" };

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
                        BigFile big = BigFile.FromFile(filePath);
                        big.Extract(dirPath);
                        big = null;
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
                    string fileName = Path.GetFileName(filePath); //BIGFILE.BIG
                    string dirName = fileName.Substring(0, fileName.Length - 4); // BIGFILE
                    string dirPath = filePath.Substring(0, (filePath.Length - 4)) + @"\"; // .../BIGFILE/
                    string path_txt = dirName + ".TXT"; // BIGFILE.TXT
                    string basePath = ExecutionSource.ConsolePipeline.ExtractedPath;
                    string tempDir = Path.Combine(ModLoaderGlobals.BaseDirectory, dirName + @"\");

                    File.Move(ModLoaderGlobals.BaseDirectory + ".txt", Path.Combine(basePath, path_txt));
                    Directory.Move(dirPath, tempDir);

                    try
                    {
                        BigFile big = BigFile.FromFile(Path.Combine(basePath, path_txt));
                        big.Save(filePath);
                        big = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    Directory.Move(tempDir, dirPath);
                    File.Delete(Path.Combine(basePath, path_txt));

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
