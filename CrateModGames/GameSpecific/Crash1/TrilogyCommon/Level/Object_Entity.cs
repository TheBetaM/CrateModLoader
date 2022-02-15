using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash2
{
    public class Object_Entity : LevelObjectData<Entity>
    {

        [Category("Settings"), DisplayName("Path"), Description("The entity's path.")]
        public List<ObjectVector3> Path { get; set; } = new List<ObjectVector3>();

        public int? EntityID { get; set; }
        public int? Type { get; set; }
        public int? Subtype { get; set; }
        public string Name { get; set; }

        public ZoneEntry Zone;

        public override void Load(Entity data)
        {
            int xoffset = BitConv.FromInt32(Zone.Layout, 0);
            int yoffset = BitConv.FromInt32(Zone.Layout, 4);
            int zoffset = BitConv.FromInt32(Zone.Layout, 8);

            if (data.Positions.Count > 0)
            {
                //x = (position.X << 2) + xoffset;
                int x = data.Positions[0].X + xoffset;
                int y = data.Positions[0].Y + xoffset;
                int z = data.Positions[0].Z + xoffset;
                Position = new ObjectVector3(x / 100f, y / 100f, z / 100f);
            }

            for (int i = 0; i < data.Positions.Count; i++)
            {
                int x = data.Positions[i].X + xoffset;
                int y = data.Positions[i].Y + xoffset;
                int z = data.Positions[i].Z + xoffset;
                Path.Add(new ObjectVector3(data.Positions[i].X / 100f, data.Positions[i].Y / 100f, data.Positions[i].Z / 100f));
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
