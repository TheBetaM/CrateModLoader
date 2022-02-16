using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CrateModLoader.LevelAPI
{
    public abstract class LevelObjectDataBase
    {
        public int ID = 0;
        public int ObjectCategory = 0;
        public EditorVisual Visual = EditorVisual.Wireframe;
        public CollisionDataBase VisualData = null;

        public enum EditorVisual
        {
            Wireframe = 0,
            Box,
            Point,
            Custom,
        }

        [Category("Base"), Description("Position of the object."), TypeConverter(typeof(ExpandableObjectConverter))]
        public ObjectVector3 Position { get; set; } = new ObjectVector3(0, 0, 0);
        [Category("Base"), Description("Rotation of the object."), TypeConverter(typeof(ExpandableObjectConverter))]
        public ObjectVector3 Rotation { get; set; } = new ObjectVector3(0, 0, 0);
        [Category("Base"), Description("Scale of the object."), TypeConverter(typeof(ExpandableObjectConverter))]
        public ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);

        public abstract void Load(object LevelObject);
        public abstract void Save();
        public virtual void LoadVisuals(object LevelObject) { }
        public event EventHandler OnPropertyUpdate;
        public void PropertyUpdated()
        {
            if (OnPropertyUpdate != null)
            {
                OnPropertyUpdate.Invoke(this, null);
            }
        }

        public override string ToString()
        {
            return "Object " + ID;
        }
    }

    public class ObjectVector3
    {
        private float vX = 0;
        private float vY = 0;
        private float vZ = 0;

        public float X {
            get {
                return vX;
            }
            set {
                vX = value;
            }
        }
        public float Y
        {
            get
            {
                return vY;
            }
            set
            {
                vY = value;
            }
        }
        public float Z
        {
            get
            {
                return vZ;
            }
            set
            {
                vZ = value;
            }
        }

        public ObjectVector3(float x, float y, float z)
        {
            vX = x;
            vY = y;
            vZ = z;
        }
        public ObjectVector3(float x)
        {
            vX = x;
            vY = x;
            vZ = x;
        }

        public override string ToString()
        {
            return "<" + vX + ", " + vY + ", " + vZ + ">";
        }
    }
}
