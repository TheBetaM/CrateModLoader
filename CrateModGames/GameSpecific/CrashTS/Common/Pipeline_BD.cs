using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class Pipeline_BD : ModPipeline
    {
        public Pipeline_BD(Modder mod) : base(mod) { }

        public override string Name => "BD archive";
        public override List<string> Extensions => new List<string>() { ".BD" };
        public override bool IsModLayer => true;
        public override int ModLayerID => 1;

        public override async Task ExtractObject(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string bhPath = Path.ChangeExtension(filePath, ".BH");
            string bdPath = filePath.Substring(0, (filePath.Length - 3)) + @"\";
            Directory.CreateDirectory(bdPath);

            BD_Archive Archive = new BD_Archive();
            await Archive.ExtractAsync(ExecutionSource, filePath, bdPath);

            //BDArchive.ExtractAll(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH"), bdPath);

            File.Delete(filePath);
            File.Delete(bhPath);
        }

        public override async Task BuildObject(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string bdPath = filePath.Substring(0, (filePath.Length - 3)) + @"\";
            BD_Archive Archive = new BD_Archive();
            await Archive.CompileAsync(ExecutionSource, filePath, bdPath);

            //BDArchive.CompileAll(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH"), bdPath);

            // Get rid of extracted files
            if (Directory.Exists(bdPath))
            {
                DirectoryInfo di = new DirectoryInfo(bdPath);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(bdPath);
            }
        }
    }
}
