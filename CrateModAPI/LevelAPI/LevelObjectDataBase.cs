using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace CrateModLoader.LevelAPI
{
    public abstract class LevelObjectDataBase
    {
        public int ID = 0;
        public int ObjectCategory = 0;

        [Category("Base"), Description("Position of the object."), TypeConverter(typeof(ExpandableObjectConverter))]
        public Vector3 Position
        {
            get => position;
            set => position = value;
        }
        [Category("Base"), Description("Rotation of the object."), TypeConverter(typeof(ExpandableObjectConverter))]
        public Vector3 Rotation
        {
            get => rotation;
            set => rotation = value;
        }
        [Category("Base"), Description("Scale of the object."), TypeConverter(typeof(ExpandableObjectConverter))]
        public Vector3 Scale
        {
            get => scale;
            set => scale = value;
        }

        private Vector3 position = new Vector3(0,0,0);
        private Vector3 rotation = new Vector3(0,0,0);
        private Vector3 scale = new Vector3(1,1,1);

        public abstract void Load(object LevelObject);
        public abstract void Save();

        public override string ToString()
        {
            return "Object " + ID;
        }
    }
}
