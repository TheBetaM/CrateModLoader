using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using System.Numerics;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class Object_Instance : LevelObjectData<Instance>
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
        [Category("Settings"), DisplayName("Flags"), Description("The flags of this instance.")]
        public uint Flags { get; set; }
        [Category("Settings"), DisplayName("Flag List")]
        public List<uint> UnkI321 { get; set; } = new List<uint>();
        [Category("Settings"), DisplayName("Float List"), Description("List of floating point properties of the instance. Some are used by the game object, some by script.")]
        public List<float> UnkI322 { get; set; } = new List<float>();
        [Category("Settings"), DisplayName("Integer List"), Description("List of integer properties of this instance. Some are used by the game object, some by script.")]
        public List<uint> UnkI323 { get; set; } = new List<uint>();

        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);
        [Browsable(false)]
        public override string Name { get; set; } = string.Empty;

        public override void Load(Instance data)
        {
            Position = new ObjectVector3(-data.Pos.X, data.Pos.Y, data.Pos.Z);
            Rotation = new ObjectVector3(data.RotX, data.RotY, data.RotZ);

            InstanceIDs = new List<ushort>(data.InstanceIDs);
            PositionIDs = new List<ushort>(data.PositionIDs);
            PathIDs = new List<ushort>(data.PathIDs);
            ObjectID = data.ObjectID;
            ScriptID = data.ScriptID;
            Flags = data.Flags;
            UnkI321 = new List<uint>();
            UnkI322 = new List<float>();
            UnkI323 = new List<uint>();
        }

        public override void Save(Instance data)
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
            data.Flags = Flags;
            data.UnkI321 = new List<uint>(UnkI321);
            data.UnkI322 = new List<float>(UnkI322);
            data.UnkI323 = new List<uint>(UnkI323);

        }

        public override string ToString()
        {
            if (Enum.IsDefined(typeof(DefaultEnums.ObjectID), ObjectID))
            {
                return "Instance " + ID + ": " + (DefaultEnums.ObjectID)ObjectID;
            }
            else
            {
                return "Instance " + ID;
            }
        }
    }
}
