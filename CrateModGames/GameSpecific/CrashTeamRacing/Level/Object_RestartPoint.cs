using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CTRFramework;
using CTRFramework.Shared;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Object_RestartPoint : LevelObjectData<Pose>
    {
        [Browsable(false)]
        public override string Name { get; set; } = string.Empty;
        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);

        public override void Load(Pose data)
        {
            Position = new ObjectVector3(data.Position.X, data.Position.Y, data.Position.Z);
            Rotation = new ObjectVector3(data.Rotation.X, data.Rotation.Y, data.Rotation.Z);
        }

        public override void Save(Pose data)
        {
            data.Position = new System.Numerics.Vector3(Position.X, Position.Y, Position.Z);
            data.Rotation = new System.Numerics.Vector3(Rotation.X, Rotation.Y, Rotation.Z);
        }

        public override string ToString()
        {
            return "Restart Point " + ID;
        }
    }
}
