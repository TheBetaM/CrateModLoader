using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Object_SpawnGroup : LevelObjectData<SpawnGroup>
    {

        public int SpawnID = 0;

        public override void Load(SpawnGroup data)
        {
            Position = new ObjectVector3(data.Entries[SpawnID].Position.X, data.Entries[SpawnID].Position.Y, data.Entries[SpawnID].Position.Z);
            Rotation = new ObjectVector3(data.Entries[SpawnID].Rotation.X, data.Entries[SpawnID].Rotation.Y, data.Entries[SpawnID].Rotation.Z);
        }

        public override void Save(SpawnGroup data)
        {
            data.Entries[SpawnID].Position = new System.Numerics.Vector3(Position.X, Position.Y, Position.Z);
            data.Entries[SpawnID].Rotation = new System.Numerics.Vector3(Rotation.X, Rotation.Y, Rotation.Z);
        }

        public override string ToString()
        {
            return "Spawn " + ID;
        }
    }
}
