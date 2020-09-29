using System;

namespace CrateModLoader.ModProperties
{
    public class ModPropFloat : ModProperty<float>
    {

        public ModPropFloat(float f) : base(f)
        {

        }
        public ModPropFloat(float f, string name, string desc) : base(f, name, desc)
        {

        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input)
        {
            float val;
            if (float.TryParse(input, out val))
            {
                Value = val;
            }
        }

    }
}