using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_TriggerSphere : LevelObjectData<XOM.XFortsExportedData.PhantomSphere>
    {
        [Category("Settings"), Description("Name of the reference point (referenced by Lua scripts).")]
        public string Name { get; private set; }

        public override void Load(XOM.XFortsExportedData.PhantomSphere data)
        {
            Position = new ObjectVector3(data.Pos.X / 10, data.Pos.Y / 10, data.Pos.Z / 10);
            Scale = new ObjectVector3(data.Radius);

            Name = data.NamePoint;
        }

        public override void Save(XOM.XFortsExportedData.PhantomSphere data)
        {
            data.Pos = new XOM.Vector3(Position.X * 10, Position.Y * 10, Position.Z * 10);
            data.Radius = Scale.X;

            //data.NamePoint = Name;
        }

        public override string ToString()
        {
            return "Trigger Sphere " + ID + ": " + Name;
        }
    }
}
