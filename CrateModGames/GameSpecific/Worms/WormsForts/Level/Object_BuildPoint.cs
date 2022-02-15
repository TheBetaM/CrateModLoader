using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
//using System.Numerics;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_BuildPoint : LevelObjectData<XOM.XFortsExportedData.BuildPoint>
    {

        [Category("Settings"), Description("Is the build point a Victory Location (star)?")]
        public bool VictoryLocation { get; set; }
        [Category("Settings"), Description("Name of the build point (referenced by Lua scripts).")]
        public string Name { get; private set; }
        [Category("Settings"), Description("The player ID of the building placed on this point by default.")]
        public uint PlayerID { get; set; }
        [Category("Settings"), Description("The name of the building (unused?).")]
        public string BuildingName { get; private set; }
        [Category("Settings"), Description("The type of the building placed on this build point by default.")]
        public XOM.XFortsExportedData.BuildingTypes BuildingType { get; set; }
        [Category("Settings")]
        public byte BonusType { get; private set; }
        [Category("Settings")]
        public byte Connections { get; private set; }

        public override void Load(XOM.XFortsExportedData.BuildPoint data)
        {
            Position = new ObjectVector3(data.Pos.X / 10, data.Pos.Y / 10, data.Pos.Z / 10);
            Rotation = new ObjectVector3(data.Rot);

            VictoryLocation = data.VictoryLocation.Value;
            Name = data.NamePoint;
            PlayerID = data.PlayerID;
            BuildingName = data.NameBuilding;
            BuildingType = data.BuildingType;
            BonusType = data.BonusType;
            Connections = data.Connections;
        }

        public override void Save(XOM.XFortsExportedData.BuildPoint data)
        {
            data.Pos = new XOM.Vector3(Position.X * 10, Position.Y * 10, Position.Z * 10);
            data.Rot = Rotation.X;

            data.VictoryLocation.Value = VictoryLocation;
            //data.NameID.Value = 0;
            data.PlayerID = PlayerID;
            //data.BuildingName.Value = 0;
            data.BuildingType = BuildingType;
            data.BonusType = BonusType;
            data.Connections = Connections;
        }

        public override string ToString()
        {
            return "Build Point " + ID + ": " + Name;
        }
    }
}
