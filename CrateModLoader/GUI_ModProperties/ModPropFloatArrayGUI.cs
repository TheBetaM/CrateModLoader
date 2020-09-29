using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropFloatArrayGUI : ModPropertyGUI<ModPropFloatArray>
    {

        public ModPropFloatArrayGUI(ModPropFloatArray p) : base(p)
        {

        }

        private List<NumericUpDown> nums = new List<NumericUpDown>();

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {
            base.GenerateUI(parent, ref offset);

            nums.Clear();
            TableLayoutPanel table = (TableLayoutPanel)parent;
            int Count = 2;

            foreach (float f in Prop.Value)
            {
                NumericUpDown num = new NumericUpDown();

                num.DecimalPlaces = 5;
                num.Minimum = decimal.MinValue;
                num.Maximum = decimal.MaxValue;

                num.Value = (decimal)f;
                num.Parent = (Control)parent;
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
            NumericUpDown box = (NumericUpDown)sender;

            if (nums.Contains(box))
            {
                int pos = nums.IndexOf(box);
                Prop.Value[pos] = (float)box.Value;
            }
            else
            {
                Console.WriteLine("Error: Numeric up down not linked to a value!");
            }

            base.ValueChange(sender, e);
        }

    }
}
