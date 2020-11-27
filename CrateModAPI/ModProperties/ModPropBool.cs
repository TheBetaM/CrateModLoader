using System;

namespace CrateModLoader.ModProperties
{
    public class ModPropBool : ModProperty<bool>
    {

        public ModPropBool(bool b) : base(b)
        {

        }
        public ModPropBool(bool b, string name, string desc) : base(b, name, desc)
        {

        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            if (Value)
                line += "1";
            else
                line += "0";
        }

        public override void DeSerialize(string input, ModCrate crate)
        {
            if (input == "1")
                Value = true;
            else
                Value = false;
        }

    }
}