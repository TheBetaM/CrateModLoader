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

        public override void GenerateUI(Control parent, ref int offset)
        {
            base.GenerateUI(parent, ref offset);

            nums.Clear();
            TableLayoutPanel table = (TableLayoutPanel)parent;
            int Count = 2;

            foreach (float f in Value)
            {
                NumericUpDown num = new NumericUpDown();

                num.DecimalPlaces = 5;
                num.Minimum = decimal.MinValue;
                num.Maximum = decimal.MaxValue;

                num.Value = (decimal)f;
                num.Parent = parent;
                num.Dock = DockStyle.Fill;
                num.ValueChanged += ValueChange;
                num.MouseCaptureChanged += FocusUI;

                nums.Add(num);

                if (table.ColumnCount < Count)
                {
                    table.ColumnCount++;
                    table.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 20f);
                    table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
                }

                table.SetColumn(num, Count - 1);
                table.SetRow(num, offset);
                Count++;

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
                Console.WriteLine("Error: Input Float array length mismatch!");
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