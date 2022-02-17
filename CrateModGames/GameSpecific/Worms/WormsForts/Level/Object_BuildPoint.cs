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
        [Category("Settings"), Description("The player ID of the building placed on this point by default.")]
        public uint PlayerID { get; set; }
        [Category("Settings"), Description("The name of the building (unused?).")]
        public string BuildingName { get; private set; }
        [Category("Settings"), Description("The type of the building placed on this build point by default.")]
        public XOM.XFortsExportedData.BuildingTypes BuildingType { get; set; }
        [Category("Settings")]
        public byte BonusType { get; private set; }
        [Category("Connections"), Description("All directtions in which the build point looks for connecting buildings."), Browsable(false)]
        public BuildingConnections Connections { get; private set; }

        [Category("Connections")]
        public bool Connection_Right
        {
            get { return Connections.HasFlag(BuildingConnections.Right); }
            set { Connections ^= BuildingConnections.Right; }
        }
        [Category("Connections")]
        public bool Connection_Bottom
        {
            get { return Connections.HasFlag(BuildingConnections.Bottom); }
            set { Connections ^= BuildingConnections.Bottom; }
        }
        [Category("Connections")]
        public bool Connection_Left
        {
            get { return Connections.HasFlag(BuildingConnections.Left); }
            set { Connections ^= BuildingConnections.Left; }
        }
        [Category("Connections")]
        public bool Connection_Top
        {
            get { return Connections.HasFlag(BuildingConnections.Top); }
            set { Connections ^= BuildingConnections.Top; }
        }
        [Category("Connections")]
        public bool Connection_BottomRight
        {
            get { return Connections.HasFlag(BuildingConnections.BottomRight); }
            set { Connections ^= BuildingConnections.BottomRight; }
        }
        [Category("Connections")]
        public bool Connection_TopRight
        {
            get { return Connections.HasFlag(BuildingConnections.TopRight); }
            set { Connections ^= BuildingConnections.TopRight; }
        }
        [Category("Connections")]
        public bool Connection_BottomLeft
        {
            get { return Connections.HasFlag(BuildingConnections.BottomLeft); }
            set { Connections ^= BuildingConnections.BottomLeft; }
        }
        [Category("Connections")]
        public bool Connection_TopLeft
        {
            get { return Connections.HasFlag(BuildingConnections.TopLeft); }
            set { Connections ^= BuildingConnections.TopLeft; }
        }

        [Flags]
        public enum BuildingConnections
        {
            None = 0,
            Right = 1,
            Bottom = 2,
            Left = 4,
            Top = 8,
            BottomRight = 16,
            TopRight = 32,
            BottomLeft = 64,
            TopLeft = 128,
        }

        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);
        [Browsable(false)]
        public override ObjectVector3 WorldScale => new ObjectVector3(0.025f);

        public override void Load(XOM.XFortsExportedData.BuildPoint data)
        {
            Position = new ObjectVector3(data.Pos.X, data.Pos.Y, data.Pos.Z);
            Rotation = new ObjectVector3(data.Rot);

            VictoryLocation = data.VictoryLocation.Value;
            Name = data.NamePoint;
            PlayerID = data.PlayerID;
            BuildingName = data.NameBuilding;
            BuildingType = data.BuildingType;
            BonusType = data.BonusType;
            Connections = (BuildingConnections)data.Connections;
        }

        public override void Save(XOM.XFortsExportedData.BuildPoint data)
        {
            data.Pos = new XOM.Vector3(Position.X, Position.Y, Position.Z);
            data.Rot = Rotation.X;

            data.VictoryLocation.Value = VictoryLocation;
            //data.NameID.Value = 0;
            data.PlayerID = PlayerID;
            //data.BuildingName.Value = 0;
            data.BuildingType = BuildingType;
            data.BonusType = BonusType;
            data.Connections = (byte)Connections;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
