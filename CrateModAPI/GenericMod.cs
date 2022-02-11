using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    // Generic struct for ModStruct<T> when you mod general file stuff, like moving folders, or don't have an API to work in.
    public struct GenericModStruct
    {
        /// <summary>
        /// Target console mode.
        /// </summary>
        public ConsoleMode Console;
        /// <summary>
        /// Target detected region.
        /// </summary>
        public RegionType Region;
        /// <summary>
        /// File name (not path) of the executable (where applicable - check ConsolePipelines)
        /// </summary>
        public string ExecutableFileName;
        /// <summary>
        /// The extraction path of the game files from ConsolePipeline.
        /// </summary>
        public string ExtractedPath;

    }
}
