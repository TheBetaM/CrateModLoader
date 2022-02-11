using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class Pipeline_GOB : ModPipeline
    {
        public Pipeline_GOB(Modder mod) : base(mod) { }

        public override string Name => "GOB archive";
        public override List<string> Extensions => new List<string>() { ".GOB" };
        public override bool IsModLayer => true;
        public override int ModLayerID => 1;

        public override async Task ExtractObject(string filePath)
        {
            await Task.Run(
                () =>
                {
                    string fileName = Path.GetFileName(filePath);
                    string fileNameNoExt = fileName.Substring(0, fileName.Length - 4);
                    string gfc = ".GFC";
                    if (Path.GetExtension(filePath) == ".gob")
                    {
                        gfc = ".gfc";
                    }
                    string gfcPath = Path.ChangeExtension(filePath, gfc);
                    string dirPath = filePath.Substring(0, (filePath.Length - fileName.Length));
                    string relativePath = ExecutionSource.ConsolePipeline.ProcessPath;

                    Process GobExtract = new Process();
                    GobExtract.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gobextract.exe";
                    GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    GobExtract.StartInfo.Arguments = relativePath + fileName + " " + relativePath + fileNameNoExt;
                    GobExtract.Start();
                    GobExtract.WaitForExit();

                    File.Delete(filePath);
                    File.Delete(gfcPath);
                }
                );
            
        }

        public override async Task BuildObject(string filePath)
        {
            await Task.Run(
                () =>
                {
                    string fileName = Path.GetFileName(filePath);
                    string gfc = ".GFC";
                    if (Path.GetExtension(filePath) == ".gob")
                    {
                        gfc = ".gfc";
                    }
                    string gfcPath = Path.ChangeExtension(filePath, gfc);
                    string fileNameNoExt = fileName.Substring(0, fileName.Length - 4);
                    string dirPath = filePath.Substring(0, (filePath.Length - fileName.Length));
                    string relativePath = ExecutionSource.ConsolePipeline.ProcessPath;

                    Process GobExtract = new Process();
                    GobExtract.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gobextract.exe";
                    GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    GobExtract.StartInfo.Arguments = relativePath + fileName + " " + relativePath + fileNameNoExt + " -create 1";
                    GobExtract.Start();
                    GobExtract.WaitForExit();

                    // Extraction cleanup
                    if (Directory.Exists(relativePath + fileNameNoExt))
                    {
                        DirectoryInfo di = new DirectoryInfo(relativePath + fileNameNoExt);

                        foreach (FileInfo file in di.EnumerateFiles())
                        {
                            file.Delete();
                        }
                        try
                        {
                            foreach (DirectoryInfo dir in di.EnumerateDirectories())
                            {
                                dir.Delete(true);
                            }
                            Directory.Delete(relativePath + fileNameNoExt);
                        }
                        catch
                        {

                        }

                    }

                }
                );
        }
    }
}
