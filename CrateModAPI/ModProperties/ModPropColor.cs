using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{
    public class ModPropColor : ModProperty<int[]>
    {
        // RGBA, 0-255

        public int R => Value[0];
        public int G => Value[1];
        public int B => Value[2];
        public int A => Value[3];
        public int Red => Value[0];
        public int Green => Value[1];
        public int Blue => Value[2];
        public int Alpha => Value[3];

        public ModPropColor(int[] c) : base(c)
        {
            DefaultValue = new int[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                DefaultValue[i] = Value[i];
            }
        }
        public ModPropColor(int[] c, string name, string desc) : base(c, name, desc)
        {
            DefaultValue = new int[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                DefaultValue[i] = Value[i];
            }
        }
        public ModPropColor(Mod mod, int[] c) : base(c, mod.Name, mod.Description)
        {
            DefaultValue = new int[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                DefaultValue[i] = Value[i];
            }
            TargetMod = mod;
        }
        public ModPropColor(Mod mod, int[] c, string name, string desc) : base(c, name, desc)
        {
            DefaultValue = new int[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                DefaultValue[i] = Value[i];
            }
            TargetMod = mod;
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

        public override void DeSerialize(string input, ModCrate crate)
        {
            string[] vals = input.Split(';');

            if (vals.Length != Value.Length + 1 && vals.Length != Value.Length)
            {
                Console.WriteLine("Error: Input Color length mismatch!");
                return;
            }

            for (int i = 0; i < vals.Length - 1; i++)
            {
                int val;
                if (int.TryParse(vals[i], out val))
                {
                    Value[i] = val;
                }
            }


        }

    }
}