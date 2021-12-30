using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public abstract class NamedContainer : Container
    {
        public abstract string Name { get; set; }
    }
}
