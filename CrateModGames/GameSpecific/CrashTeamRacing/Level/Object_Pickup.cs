using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Object_Pickup : LevelObjectData<PickupHeader>
    {
        public CtrThreadID Type { get; set; }
        public string ModelName { get; private set; }

        public override void Load(PickupHeader data)
        {
            Position = new ObjectVector3(data.Pose.Position.X, data.Pose.Position.Y, data.Pose.Position.Z);
            Rotation = new ObjectVector3(data.Pose.Rotation.X, data.Pose.Rotation.Y, data.Pose.Rotation.Z);
            Scale = new ObjectVector3(data.Scale.X * 16, data.Scale.Y * 16, data.Scale.Z * 16);

            Name = data.Name;
            Type = data.ThreadID;
            ModelName = data.ModelName;
        }

        public override void Save(PickupHeader data)
        {
            data.Pose.Position = new System.Numerics.Vector3(Position.X, Position.Y, Position.Z);
            data.Pose.Rotation = new System.Numerics.Vector3(Rotation.X, Rotation.Y, Rotation.Z);
            data.Scale = new System.Numerics.Vector3(Scale.X / 16, Scale.Y / 16, Scale.Z / 16);

            data.Name = Name;
            data.ThreadID = Type;
        }

        public override string ToString()
        {
            return "Pickup " + ID + ": " + Type;
        }
    }
}
