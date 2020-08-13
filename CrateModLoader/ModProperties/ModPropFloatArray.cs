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

            offset += 20;

            int x_offset = 10;

            foreach (float f in Value)
            {
                NumericUpDown num = new NumericUpDown();

                num.DecimalPlaces = 8;
                num.Minimum = decimal.MinValue;
                num.Maximum = decimal.MaxValue;

                num.Value = (decimal)f;
                num.Parent = page;
                num.Location = new System.Drawing.Point(x_offset, offset);
                num.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                num.Size = new System.Drawing.Size(130, num.Size.Height);
                num.ValueChanged += ValueChange;

                nums.Add(num);

                x_offset += 150;
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

    }
}