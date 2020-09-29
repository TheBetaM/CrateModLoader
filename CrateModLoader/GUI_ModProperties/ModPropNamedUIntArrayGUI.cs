using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropNamedUIntArrayGUI : ModPropertyGUI<ModPropNamedUIntArray>
    {

        public ModPropNamedUIntArrayGUI(ModPropNamedUIntArray p) : base(p)
        {

        }

        private List<NumericUpDown> nums = new List<NumericUpDown>();

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {
            base.GenerateUI(parent, ref offset);

            nums.Clear();

            TabControl tabControl = new TabControl();
            tabControl.TabPages.Clear();
            tabControl.Parent = (Control)parent;
            //tabControl.Multiline = true;
            tabControl.Dock = DockStyle.Fill;
            tabControl.MouseEnter += FocusUI;

            int x_offset = 10;
            int size = 150;

            for (int i = 0; i < Prop.Value.Length; i++)
            {
                tabControl.TabPages.Add(Prop.PropNames[i]);
                tabControl.TabPages[tabControl.TabPages.Count - 1].AutoScroll = false;

                NumericUpDown num = new NumericUpDown();

                num.DecimalPlaces = 0;
                num.Minimum = uint.MinValue;
                num.Maximum = uint.MaxValue;

                num.Value = (decimal)Prop.Value[i];
                num.Parent = tabControl.TabPages[tabControl.TabPages.Count - 1];
                num.Dock = DockStyle.Fill;
                num.ValueChanged += ValueChange;
                num.MouseCaptureChanged += FocusUI;

                nums.Add(num);

            }

            TableLayoutPanel table = (TableLayoutPanel)parent;
            table.RowStyles[offset] = new RowStyle(SizeType.Absolute, 54);

            table.SetColumn(tabControl, 1);
            table.SetRow(tabControl, offset);

        }

        public override void ValueChange(object sender, EventArgs e)
        {
            NumericUpDown box = (NumericUpDown)sender;

            if (nums.Contains(box))
            {
                int pos = nums.IndexOf(box);
                Prop.Value[pos] = (uint)box.Value;
            }
            else
            {
                Console.WriteLine("Error: Numeric up down not linked to a value!");
            }

            base.ValueChange(sender, e);
        }
    }
}
