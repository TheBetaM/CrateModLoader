using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace CrateModLoader
{
    // Parses files into API specifc classes
    public abstract class ModParser<T> : IModParser
    {


        public virtual bool TryParse(string Path)
        {
            return false;
        }

        public virtual bool TryParse(Stream Stream)
        {
            return false;
        }


    }
}
