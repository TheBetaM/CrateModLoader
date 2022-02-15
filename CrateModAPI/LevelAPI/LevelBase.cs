using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CrateModLoader.LevelAPI
{
    public abstract class LevelBase
    {
        public List<CollisionDataBase> CollisionData = new List<CollisionDataBase>();
        public BindingList<LevelObjectDataBase> ObjectData = new BindingList<LevelObjectDataBase>();
        public string Name { get; set; }
        public abstract Dictionary<int, string> CategoryNames { get; set; }

    }
}
