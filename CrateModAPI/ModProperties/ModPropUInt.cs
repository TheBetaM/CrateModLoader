using System;

namespace CrateModLoader.ModProperties
{
    public class ModPropUInt : ModProperty<uint>
    {

        public ModPropUInt(uint i) : base(i)
        {

        }
        public ModPropUInt(uint i, string name, string desc) : base(i, name, desc)
        {

        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input)
        {
            uint val;
            if (uint.TryParse(input, out val))
            {
                Value = val;
            }
        }

    }
}