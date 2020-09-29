using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{
    public class ModPropNamedFloatArray : ModProperty<float[]>
    {

        public string[] PropNames;

        public ModPropNamedFloatArray(float[] f, string[] names) : base(f)
        {
            PropNames = names;
            DefaultValue = new float[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                DefaultValue[i] = Value[i];
            }
        }
        public ModPropNamedFloatArray(float[] f, string[] names, string name, string desc) : base(f, name, desc)
        {
            PropNames = names;
            DefaultValue = new float[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                DefaultValue[i] = Value[i];
            }
        }

        public override void ResetToDefault()
        {
            for (int i = 0; i < Value.Length; i++)
            {
                Value[i] = DefaultValue[i];
            }
            HasChanged = false;
        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            for (int i = 0; i < Value.Length; i++)
            {
                line += Value[i];
                line += ";";
            }
        }

        public override void DeSerialize(string input)
        {
            string[] vals = input.Split(';');

            if (vals.Length != Value.Length + 1 && vals.Length != Value.Length)
            {
                Console.WriteLine("Error: Input Named float array length mismatch!");
                return;
            }

            for (int i = 0; i < vals.Length - 1; i++)
            {
                float val;
                if (float.TryParse(vals[i], out val))
                {
                    Value[i] = val;
                }
            }


        }

    }
}