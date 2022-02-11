using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using System.Numerics;
using Twinsanity;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public class Object_InstanceMB : LevelObjectData<InstanceMB>
    {


        public override void Load(InstanceMB data)
        {
            Position = new System.Numerics.Vector3(data.Pos.X, data.Pos.Y, data.Pos.Z);
            Rotation = new System.Numerics.Vector3(data.RotX, data.RotY, data.RotZ);

        }

        public override void Save(InstanceMB data)
        {
            data.Pos = new Pos(Position.X, Position.Y, Position.Z, 1f);
            data.RotX = (ushort)Rotation.X;
            data.RotY = (ushort)Rotation.Y;
            data.RotZ = (ushort)Rotation.Z;

        }

        public override string ToString()
        {
            return "Instance " + ID;
        }
    }
}
