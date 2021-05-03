using System;
using System.Collections.Generic;
using System.IO;
/*
 * Mod Passes:
 * TPL_File -> texture archives
 */

namespace CrateModLoader.GameSpecific.Rayman3
{
    public enum ModProps : int
    {
        Options = 0,
        Textures_General,
        Textures_Menu,
        Textures_Loading,
    }

    public sealed class Modder_Rayman3 : Modder
    {
        public override bool CanPreloadGame => true;
        public override List<ConsoleMode> PreloadConsoles => new List<ConsoleMode>() { ConsoleMode.GCN, };

        public override async void StartModProcess()
        {
            FindFiles(new Parser_TPL(this));
            await StartNewPass();

            ProcessBusy = false;
        }

        public override async void StartPreload()
        {
            FindFiles(new Parser_TPL(this));
            await StartPreloadPass();

            ProcessBusy = false;
        }

    }
}
