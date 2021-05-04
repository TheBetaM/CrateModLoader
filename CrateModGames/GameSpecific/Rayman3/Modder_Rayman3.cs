using System.Collections.Generic;
/*
 * Mod Passes:
 * TPL_File -> texture archives
 */
namespace CrateModLoader.GameSpecific.Rayman3
{
    public sealed class Modder_Rayman3 : Modder
    {
        public override bool CanPreloadGame => true;
        public override List<ConsoleMode> PreloadConsoles => new List<ConsoleMode>() { ConsoleMode.GCN, };

        public override async void StartModProcess()
        {
            FindFiles(new Parser_TPL(this, ConsolePipeline.ExtractedPath, ModderIsPreloading));
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
