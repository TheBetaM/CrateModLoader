using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CrateModLoader
{
    public abstract class ModParserBase
    {
        public abstract bool SkipParser { get; set; }
        public virtual bool ForceParser { get; set; }

        public abstract Task StartPass(ModPass pass = ModPass.Mod);
        public abstract Task FileStartPass(FileInfo file, ModPass pass = ModPass.Mod);

        public abstract bool AddFile(FileInfo file);
    }
}
