using System;
using System.Collections.Generic;

namespace CrateModAPI
{
    public enum CaseRules
    {
        AnyCase = 0,
        CaseSensitive,
        Upper,
        Lower,
    }

    public enum FileType
    {
        Unknown = 0,
        File, // file that can be processed and modded
        Archive, // file that can be extracted and rebuilt
    }

    // For identifying a file to probe by name
    public class FileFilter
    {

    }
}
