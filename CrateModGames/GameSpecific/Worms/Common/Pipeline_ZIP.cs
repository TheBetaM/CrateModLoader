using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Pipeline_ZIP : ModPipeline
    {
        public Pipeline_ZIP(Modder mod) : base(mod) { }

        public override string Name => "ZIP archive";
        public override List<string> Extensions => new List<string>() { ".ZIP" };
        public override bool IsModLayer => true;
        public override int ModLayerID => 1;

        public override async Task ExtractObject(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string dirPath = filePath.Substring(0, (filePath.Length - 4)) + @"\";
            ZipFile.ExtractToDirectory(filePath, dirPath);
        }

        public override async Task BuildObject(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            string fileName = Path.GetFileName(filePath);
            string dirPath = filePath.Substring(0, (filePath.Length - 4)) + @"\";
            ZipFile.CreateFromDirectory(dirPath, filePath);

            Directory.Delete(dirPath, true);
        }
    }
}
