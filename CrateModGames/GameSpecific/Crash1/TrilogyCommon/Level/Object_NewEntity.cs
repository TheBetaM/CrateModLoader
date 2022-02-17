using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash3
{
    public class Object_NewEntity : LevelObjectData<Entity>
    {

        [Category("Settings"), DisplayName("Path"), Description("The entity's path.")]
        public List<ObjectVector3> Path { get; set; } = new List<ObjectVector3>();
        [Browsable(false)]
        public override ObjectVector3 Rotation { get; set; } = new ObjectVector3(0, 0, 0);

        public int? EntityID { get; set; }
        public int? Type { get; set; }
        public int? Subtype { get; set; }

        public NewZoneEntry Zone;

        [Browsable(false)]
        public override ObjectVector3 WorldScale => new ObjectVector3(0.004f);

        public override void Load(Entity data)
        {
            int xoffset = BitConv.FromInt32(Zone.Layout, 0);
            int yoffset = BitConv.FromInt32(Zone.Layout, 4);
            int zoffset = BitConv.FromInt32(Zone.Layout, 8);

            float scale = 1;
            if (data.Scaling.HasValue)
                scale = (1 << data.Scaling.Value) / 4;

            if (data.Positions.Count > 0)
            {
                float x = data.Positions[0].X * scale + xoffset;
                float y = data.Positions[0].Y * scale + xoffset;
                float z = data.Positions[0].Z * scale + xoffset;
                Position = new ObjectVector3(x, y, z);
            }

            for (int i = 0; i < data.Positions.Count; i++)
            {
                int x = data.Positions[i].X + xoffset;
                int y = data.Positions[i].Y + xoffset;
                int z = data.Positions[i].Z + xoffset;
                Path.Add(new ObjectVector3(data.Positions[i].X, data.Positions[i].Y, data.Positions[i].Z));
            }

            EntityID = data.ID;
            Type = data.Type;
            Subtype = data.Subtype;
            Name = data.Name;

        }

        public override void Save(Entity data)
        {
            data.ID = EntityID;
            data.Type = Type;
            data.Subtype = Subtype;
            data.Name = Name;

        }

        public override string ToString()
        {
            return "Entity " + ID + ": " + Name;
        }
    }
}
