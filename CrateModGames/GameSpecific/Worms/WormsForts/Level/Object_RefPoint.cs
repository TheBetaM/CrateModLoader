using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_RefPoint : LevelObjectData<XOM.XFortsExportedData.RefPoint>
    {
        [Category("Settings"), Description("Name of the reference point (referenced by Lua scripts).")]
        public string Name { get; private set; }

        public override void Load(XOM.XFortsExportedData.RefPoint data)
        {
            Position = new ObjectVector3(data.Pos.X / 10, data.Pos.Y / 10, data.Pos.Z / 10);
            Rotation = new ObjectVector3(data.Rot.X, data.Rot.Y, data.Rot.Z);

            Name = data.NamePoint;
        }

        public override void Save(XOM.XFortsExportedData.RefPoint data)
        {
            data.Pos = new XOM.Vector3(Position.X * 10, Position.Y * 10, Position.Z * 10);
            data.Rot = new XOM.Vector3(Rotation.X, Rotation.Y, Rotation.Z);

            //data.NamePoint = Name;
        }

        public override string ToString()
        {
            return "Reference Point " + ID + ": " + Name;
        }
    }
}
