using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CrateModLoader.LevelAPI
{
    public abstract class LevelBase
    {
        public BindingList<LevelObjectDataBase> ObjectData = new BindingList<LevelObjectDataBase>();
        [Browsable(false)]
        public string Name { get; set; }
        [Browsable(false)]
        public abstract Dictionary<int, string> CategoryNames { get; set; }

    }
}
