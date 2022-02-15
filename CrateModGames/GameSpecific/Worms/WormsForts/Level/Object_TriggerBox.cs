using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_TriggerBox : LevelObjectData<XOM.XFortsExportedData.PhantomBox>
    {
        [Category("Settings"), Description("Name of the reference point (referenced by Lua scripts).")]
        public string Name { get; private set; }

        public override void Load(XOM.XFortsExportedData.PhantomBox data)
        {
            Position = new ObjectVector3(data.Pos.X / 10, data.Pos.Y / 10, data.Pos.Z / 10);
            Scale = new ObjectVector3(data.Extents.X / 10, data.Extents.Y / 10, data.Extents.Z / 10);

            Name = data.NamePoint;
        }

        public override void Save(XOM.XFortsExportedData.PhantomBox data)
        {
            data.Pos = new XOM.Vector3(Position.X * 10, Position.Y * 10, Position.Z * 10);
            data.Extents = new XOM.Vector3(Scale.X * 10, Scale.Y * 10, Scale.Z * 10);

            //data.NamePoint = Name;
        }

        public override string ToString()
        {
            return "Trigger Box " + ID + ": " + Name;
        }
    }
}
