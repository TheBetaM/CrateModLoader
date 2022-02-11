using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashTitansDS
{
    public class Pipeline_BIN_Rom : ModPipeline
    {
        public Pipeline_BIN_Rom(Modder mod) : base(mod) { }

        public override string Name => "ROM.BIN archive";
        public override List<string> Extensions => new List<string>() { ".BIN" };
        public override bool IsModLayer => true;
        public override int ModLayerID => 1;

        private BIN_Rom_Archive file;

        public override async Task ExtractObject(string filePath)
        {
            await Task.Run(
                () =>
                {
                    file = new BIN_Rom_Archive();
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
        }

        public override async Task BuildObject(string filePath)
        {
            await Task.Run(
                () =>
                {
                    try
                    {
                        file.Write(ExecutionSource, filePath);
                    }
                    catch
                    {
                        Console.WriteLine("ModPipeline Error: " + filePath);
                    }
                }
                );
        }
    }
}
