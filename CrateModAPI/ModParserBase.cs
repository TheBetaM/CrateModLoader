using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CrateModLoader.LevelAPI;

namespace CrateModLoader
{
    public abstract class ModParserBase
    {
        public abstract bool SkipParser { get; set; }
        public virtual bool ForceParser { get; set; }
        public virtual bool IsLevelFile { get => false; }
        public Dictionary<string, List<FileInfo>> FoundFiles { get; set; }
        public Dictionary<string, List<MemoryFile>> FoundMemFiles { get; set; }

        public abstract Task StartPass(ModPass pass = ModPass.Mod);
        public abstract Task FileStartPass(FileInfo file, ModPass pass = ModPass.Mod);
        public abstract Task MemoryFileStartPass(MemoryFile file, ModPass pass = ModPass.Mod);
        public abstract LevelBase LoadLevel(string FileName);

        public abstract bool AddFile(FileInfo file);
        public abstract bool AddFile(MemoryFile file);
    }
}
