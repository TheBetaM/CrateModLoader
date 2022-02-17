using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Object_SceneHeader : LevelObjectData<SceneHeader>
    {
        [Browsable(false)]
        public override string Name { get; set; } = string.Empty;


        public override void Load(SceneHeader data)
        {

        }

        public override void Save(SceneHeader data)
        {

        }

        public override string ToString()
        {
            return "CTR Scene";
        }
    }
}
