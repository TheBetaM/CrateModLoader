using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{
    public class ModPropNamedFloatArray2 : ModProperty<float[,]>
    {

        public string[] PropNames;

        public ModPropNamedFloatArray2(float[,] f, string[] names) : base(f)
        {
            PropNames = names;
            DefaultValue = new float[Value.GetLength(0), Value.GetLength(1)];
            for (int i = 0; i < Value.GetLength(0); i++)
            {
                for (int d = 0; d < Value.GetLength(1); d++)
                {
                    DefaultValue[i, d] = Value[i, d];
                }
            }
        }
        public ModPropNamedFloatArray2(float[,] f, string[] names, string name, string desc) : base(f, name, desc)
        {
            PropNames = names;
            DefaultValue = new float[Value.GetLength(0), Value.GetLength(1)];
            for (int i = 0; i < Value.GetLength(0); i++)
            {
                for (int d = 0; d < Value.GetLength(1); d++)
                {
                    DefaultValue[i, d] = Value[i, d];
                }
            }
        }

        public override void ResetToDefault()
        {
            for (int i = 0; i < Value.GetLength(0); i++)
            {
                for (int d = 0; d < Value.GetLength(1); d++)
                {
                    Value[i, d] = DefaultValue[i, d];
                }
            }
            HasChanged = false;
        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            for (int i = 0; i < Value.GetLength(0); i++)
            {
                for (int d = 0; d < Value.GetLength(1); d++)
                {
                    line += Value[i,d];
                    line += ";";
                }
            }
        }

        public override void DeSerialize(string input, ModCrate crate)
        {
            string[] vals = input.Split(';');

            if (vals.Length != (Value.GetLength(0) * Value.GetLength(1)) + 1 && vals.Length != (Value.GetLength(0) * Value.GetLength(1)))
            {
                Console.WriteLine("Error: Input Named Float array2 length mismatch!");
                return;
            }

            int page = 0;
            int pos = 0;

            for (int i = 0; i < vals.Length - 1; i++)
            {
                float val;
                if (float.TryParse(vals[i], out val))
                {
                    Value[page, pos] = val;
                }
                pos++;
                if (pos > Value.GetLength(1) - 1)
                {
                    pos = 0;
                    page++;
                }
            }


        }

    }
}