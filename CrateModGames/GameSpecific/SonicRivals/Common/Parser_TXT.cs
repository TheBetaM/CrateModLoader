using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using System.IO;

namespace CrateModLoader.GameSpecific.SonicRivals
{
    // Just a txt file
    public class Parser_TXT : ModParser<TXTFile>
    {
        public Parser_TXT(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".TXT" };

        public override TXTFile LoadObject(string filePath)
        {
            TXTFile file = new TXTFile();
            file.Lines = new List<string>(File.ReadAllLines(filePath));
            file.FileNameFull = filePath;
            file.FileName = Path.GetFileName(filePath);

            return file;
        }

        public override void SaveObject(TXTFile thing, string filePath)
        {
            File.WriteAllLines(filePath, thing.Lines);
        }
    }

    public class TXTFile
    {
        public List<string> Lines;
        public string FileName;
        public string FileNameFull;
    }
}
