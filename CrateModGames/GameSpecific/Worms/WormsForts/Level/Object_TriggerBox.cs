using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_TriggerBox : LevelObjectData<XOM.XFortsExportedData.PhantomBox>
    {

        [Browsable(false)]
        public override ObjectVector3 Rotation { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 WorldScale => new ObjectVector3(0.025f);

        public override void Load(XOM.XFortsExportedData.PhantomBox data)
        {
            Position = new ObjectVector3(data.Pos.X, data.Pos.Y, data.Pos.Z);
            Scale = new ObjectVector3(data.Extents.X * 0.05f, data.Extents.Y * 0.05f, data.Extents.Z * 0.05f);

            Name = data.NamePoint;
        }

        public override void Save(XOM.XFortsExportedData.PhantomBox data)
        {
            data.Pos = new XOM.Vector3(Position.X, Position.Y, Position.Z);
            data.Extents = new XOM.Vector3(Scale.X * 20f, Scale.Y * 20f, Scale.Z * 20f);

            //data.NamePoint = Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
