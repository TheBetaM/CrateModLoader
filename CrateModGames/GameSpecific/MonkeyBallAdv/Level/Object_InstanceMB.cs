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

        [Category("Settings"), DisplayName("Instance ID's"), Description("List of connected instances.")]
        public List<ushort> InstanceIDs { get; set; } = new List<ushort>();
        [Category("Settings"), DisplayName("Position ID's"), Description("List of connected positions.")]
        public List<ushort> PositionIDs { get; set; } = new List<ushort>();
        [Category("Settings"), DisplayName("Path ID's"), Description("List of connected paths.")]
        public List<ushort> PathIDs { get; set; } = new List<ushort>();
        [Category("Settings"), DisplayName("Object ID"), Description("The type of game object this instance creates.")]
        public ushort ObjectID { get; set; }
        //public short RefList { get; set; }
        [Category("Settings"), DisplayName("Script ID"), Description("The ID of the script that this object executes on every spawn.")]
        public short ScriptID { get; set; }

        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);
        [Browsable(false)]
        public override string Name { get; set; } = string.Empty;

        public override void Load(InstanceMB data)
        {
            Position = new ObjectVector3(-data.Pos.X, data.Pos.Y, data.Pos.Z);
            Rotation = new ObjectVector3(data.RotX, data.RotY, data.RotZ);

            InstanceIDs = new List<ushort>(data.InstanceIDs);
            PositionIDs = new List<ushort>(data.PositionIDs);
            PathIDs = new List<ushort>(data.PathIDs);
            ObjectID = data.ObjectID;
            ScriptID = data.ScriptID;
        }

        public override void Save(InstanceMB data)
        {
            data.Pos = new Pos(-Position.X, Position.Y, Position.Z, 1f);
            data.RotX = (ushort)Rotation.X;
            data.RotY = (ushort)Rotation.Y;
            data.RotZ = (ushort)Rotation.Z;

            data.InstanceIDs = new List<ushort>(InstanceIDs);
            data.PositionIDs = new List<ushort>(PositionIDs);
            data.PathIDs = new List<ushort>(PathIDs);
            data.ObjectID = ObjectID;
            data.ScriptID = ScriptID;
        }

        public override string ToString()
        {
            if (Enum.IsDefined(typeof(DefaultEnums.ObjectID_MB), ObjectID))
            {
                return "Instance " + ID + ": " + (DefaultEnums.ObjectID_MB)ObjectID;
            }
            else
            {
                return "Instance " + ID;
            }
        }
    }
}
