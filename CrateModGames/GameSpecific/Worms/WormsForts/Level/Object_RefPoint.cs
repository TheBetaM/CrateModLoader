using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_RefPoint : LevelObjectData<XOM.XFortsExportedData.RefPoint>
    {

        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);
        [Browsable(false)]
        public override ObjectVector3 WorldScale => new ObjectVector3(0.025f);

        public override void Load(XOM.XFortsExportedData.RefPoint data)
        {
            Position = new ObjectVector3(data.Pos.X, data.Pos.Y, data.Pos.Z);
            Rotation = new ObjectVector3(data.Rot.X, data.Rot.Y, data.Rot.Z);

            Name = data.NamePoint;
        }

        public override void Save(XOM.XFortsExportedData.RefPoint data)
        {
            data.Pos = new XOM.Vector3(Position.X, Position.Y, Position.Z);
            data.Rot = new XOM.Vector3(Rotation.X, Rotation.Y, Rotation.Z);

            //data.NamePoint = Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
