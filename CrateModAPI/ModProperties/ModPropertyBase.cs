using System;

namespace CrateModLoader
{
    public abstract class ModPropertyBase
    {

        public string Name;

        public string Description;

        /// <summary> Property name in code </summary>
        public string CodeName;

        /// <summary> UI category tab ID </summary>
        public int? Category = null;

        /// <summary> Value changed from default </summary>
        public bool HasChanged = false;

        /// <summary> Hidden from UI </summary>
        public bool Hidden = false;

        /// <summary> Appends input string with a serialized version of the property. </summary>
        public abstract void Serialize(ref string input);

        /// <summary> Changes the property's value by deserializing the input string. </summary>
        public abstract void DeSerialize(string input);

        /// <summary> Resets the property's value to it's default state. </summary>
        public abstract void ResetToDefault();

        public virtual void ValueChange(object sender, EventArgs e)
        {
            HasChanged = true;
        }
    }
}
