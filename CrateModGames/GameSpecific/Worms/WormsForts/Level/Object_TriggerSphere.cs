using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_TriggerSphere : LevelObjectData<XOM.XFortsExportedData.PhantomSphere>
    {
        [Browsable(false)]
        public override ObjectVector3 Rotation { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 WorldScale => new ObjectVector3(0.025f);

        public override void Load(XOM.XFortsExportedData.PhantomSphere data)
        {
            Visual = EditorVisual.Point;

            Position = new ObjectVector3(data.Pos.X, data.Pos.Y, data.Pos.Z);
            Scale = new ObjectVector3(data.Radius);

            Name = data.NamePoint;
        }

        public override void Save(XOM.XFortsExportedData.PhantomSphere data)
        {
            data.Pos = new XOM.Vector3(Position.X, Position.Y, Position.Z);
            data.Radius = Scale.X;

            //data.NamePoint = Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
