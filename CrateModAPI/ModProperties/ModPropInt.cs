using System;

namespace CrateModLoader.ModProperties
{
    public class ModPropInt : ModProperty<int>
    {

        public ModPropInt(int i) : base(i)
        {

        }
        public ModPropInt(int i, string name, string desc) : base(i, name, desc)
        {

        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input, ModCrate crate)
        {
            int val;
            if (int.TryParse(input, out val))
            {
                Value = val;
            }
        }

    }
}