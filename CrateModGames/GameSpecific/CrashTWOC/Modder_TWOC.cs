using System;
using System.Collections.Generic;
/*
 * Mod Passes:
 * string -> extraction path
 */

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public sealed class Modder_TWOC : Modder
    {
        public Modder_TWOC() { }

        public override void StartModProcess()
        {
            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                // rebuilding the GC version makes it not boot for some reason...
                return;
            }

            UpdateProcessMessage("Cache Pass", 25);
            BeforeCachePass();

            StartCachePass(ConsolePipeline.ExtractedPath);

            UpdateProcessMessage("Mod Pass", 50);
            BeforeModPass();

            StartModPass(ConsolePipeline.ExtractedPath);
        }
    }

    
}
