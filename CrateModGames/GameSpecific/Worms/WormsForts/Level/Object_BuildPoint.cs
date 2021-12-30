using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using System.Numerics;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_BuildPoint : LevelObjectData<XOM.XFortsExportedData.BuildPoint>
    {

        [Category("Settings"), Description("Is the build point a Victory Location (star)?")]
        public bool VictoryLocation
        {
            get => victoryLocation;
            set => victoryLocation = value;
        }
        [Category("Settings"), Description("Name of the build point (referenced by Lua scripts).")]
        public string Name
        {
            get => name;
            set => name = value;
        }
        [Category("Settings"), Description("The player ID of the building placed on this point by default.")]
        public uint PlayerID
        {
            get => playerID;
            set => playerID = value;
        }
        [Category("Settings"), Description("The name of the building (unused?).")]
        public string BuildingName
        {
            get => buildingName;
            set => buildingName = value;
        }
        [Category("Settings"), Description("The type of the building placed on this build point by default.")]
        public XOM.XFortsExportedData.BuildingTypes BuildingType
        {
            get => buildingType;
            set => buildingType = value;
        }
        [Category("Settings")]
        public byte BonusType
        {
            get => bonusType;
            set => bonusType = value;
        }
        [Category("Settings")]
        public byte Connections
        {
            get => connections;
            set => connections = value;
        }

        private bool victoryLocation;
        private string name;
        private uint playerID;
        private string buildingName;
        private XOM.XFortsExportedData.BuildingTypes buildingType;
        private byte bonusType;
        private byte connections;

        public override void Load(XOM.XFortsExportedData.BuildPoint data)
        {
            Position = new Vector3(data.Pos.X, data.Pos.Y, data.Pos.Z);
            Rotation = new Vector3(data.Rot);

            victoryLocation = data.VictoryLocation.Value;
            name = data.NameID.Value.ToString(); //todo
            playerID = data.PlayerID;
            buildingName = data.BuildingName.Value.ToString(); //todo
            buildingType = data.BuildingType;
            bonusType = data.BonusType;
            connections = data.Connections;
        }

        public override void Save(XOM.XFortsExportedData.BuildPoint data)
        {
            data.Pos = new XOM.Vector3(Position.X, Position.Y, Position.Z);
            data.Rot = Rotation.X;

            data.VictoryLocation.Value = victoryLocation;
            //data.NameID.Value = 0;
            data.PlayerID = playerID;
            //data.BuildingName.Value = 0;
            data.BuildingType = buildingType;
            data.BonusType = bonusType;
            data.Connections = connections;
        }

        public override string ToString()
        {
            return "Build Point " + ID;
        }
    }
}
