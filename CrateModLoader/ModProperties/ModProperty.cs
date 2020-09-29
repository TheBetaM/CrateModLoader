using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{
    public class ModProperty<T> : ModPropertyBase
    {

        public T Value { get; set; }

        public T DefaultValue { get; set; }

        public ModProperty(T o, string name, string desc = "")
        {
            Value = o;
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
            else
            {
                Name = null;
            }
            Description = desc;
            DefaultValue = o;
        }
        public ModProperty(T o)
        {
            Value = o;
            Name = null;
            Description = string.Empty;
            DefaultValue = o;
        }

        public override void ResetToDefault()
        {
            Value = DefaultValue;
            HasChanged = false;
        }

        public override void Serialize(ref string line)
        {
            line += CodeName;
            line += ModCrates.Separator;
        }

        public override void DeSerialize(string input)
        {

        }

        /*
        public override void ValueChange(object sender, EventArgs e)
        {
            
        }
        */

    }
}
