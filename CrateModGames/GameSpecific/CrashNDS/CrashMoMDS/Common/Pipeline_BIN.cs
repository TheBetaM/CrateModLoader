using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashMoMDS
{
    public class Pipeline_BIN : ModPipeline
    {
        public Pipeline_BIN(Modder mod) : base(mod)
        {
            Archives = new Dictionary<string, BIN_Archive>();
        }

        public override string Name => "BIN archives";
        public override List<string> Extensions => new List<string>() { ".BIN" };
        public override bool IsModLayer => true;
        public override int ModLayerID => 1;
        public override bool SecondarySkip => false;
        public override List<string> SecondaryList => new List<string>() // temporary, not really satisfied with the speed as it is now
        {
            "texts_c01.bin",
        };
        /*
        public override bool SecondarySkip => true; //one BIN is not an archive
        public override List<string> SecondaryList => new List<string>()
        {
            "DebugFont_bmp.bin",
        };
        */
        //public override bool DisableAsync => true; //async too resource intensive atm
        private Dictionary<string, BIN_Archive> Archives;

        public override async Task ExtractObject(string filePath)
        {
            BIN_Archive file = new BIN_Archive();
            file.Read(ExecutionSource, filePath);
            Archives.Add(filePath, file);

            /*
            await Task.Run(
                () =>
                {
                    BIN_Archive file = new BIN_Archive();
                    try
                    {
                        file.Read(ExecutionSource, filePath);
                    }
                    catch
                    {
                        Console.WriteLine("ModPipeline Error: " + filePath);
                    }
                }
                );
            */
        }

        public override async Task BuildObject(string filePath)
        {
            Archives[filePath].Write(ExecutionSource, filePath);

            /*
            await Task.Run(
                () =>
                {
                    BIN_Archive file = new BIN_Archive();
                    file.Write(ExecutionSource, filePath);
                }
                );
            */
        }
    }
}
