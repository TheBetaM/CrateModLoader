using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader
{
    public interface IModParser
    {

        bool TryParse(string Path);

        bool TryParse(Stream FileStream);
    }
}