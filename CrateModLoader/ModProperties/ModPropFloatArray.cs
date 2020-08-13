using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties
{
    public class ModPropFloatArray : ModProperty<float[]>
    {

        public ModPropFloatArray(float[] f) : base(f)
        {
            DefaultValue = new float[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                DefaultValue[i] = Value[i];
            }
        }
        public ModPropFloatArray(float[] f, string name, string desc) : base(f, name, desc)
        {
            DefaultValue = new float[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                DefaultValue[i] = Value[i];
            }
        }

        private List<NumericUpDown> nums = new List<NumericUpDown>();

        public override void GenerateUI(TabPage page, ref int offset)
        {
            base.GenerateUI(page, ref offset);

            nums.Clear();

            //offset += 20;

            int x_offset = 245;
            int size = 130;
            if (Value.Length > 5)
            {
                size = 65;
            }

            foreach (float f in Value)
            {
                NumericUpDown num = new NumericUpDown();

                num.DecimalPlaces = 5;
                num.Minimum = decimal.MinValue;
                num.Maximum = decimal.MaxValue;

                num.Value = (decimal)f;
                num.Parent = page;
                num.Location = new System.Drawing.Point(x_offset, offset - 3);
                num.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                num.Size = new System.Drawing.Size(size, num.Size.Height);
                num.ValueChanged += ValueChange;
                num.MouseCaptureChanged += FocusUI;

                nums.Add(num);

                x_offset += size + 20;
            }

        }

        public override void ValueChange(object sender, EventArgs e)
        {
            base.ValueChange(sender, e);

            NumericUpDown box = (NumericUpDown)sender;

            if (nums.Contains(box))
            {
                int pos = nums.IndexOf(box);
                Value[pos] = (float)box.Value;
            }
            else
            {
                Console.WriteLine("Error: Numeric up down not linked to a value!");
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
                Console.WriteLine("Error: Input array length mismatch!");
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