using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropNamedFloatArray2GUI : ModPropertyGUI<ModPropNamedFloatArray2>
    {

        public ModPropNamedFloatArray2GUI(ModPropNamedFloatArray2 p) : base(p)
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

            int x_offset = 0;
            int size = 130;

            for (int i = 0; i < Prop.Value.GetLength(0); i++)
            {
                tabControl.TabPages.Add(Prop.PropNames[i]);
                tabControl.TabPages[tabControl.TabPages.Count - 1].AutoScroll = false;

                for (int d = 0; d < Prop.Value.GetLength(1); d++)
                {
                    NumericUpDown num = new NumericUpDown();

                    num.DecimalPlaces = 5;
                    num.Minimum = decimal.MinValue;
                    num.Maximum = decimal.MaxValue;

                    num.Value = (decimal)Prop.Value[i, d];
                    num.Parent = tabControl.TabPages[tabControl.TabPages.Count - 1];
                    num.Location = new Point(x_offset, 2);
                    num.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    num.Size = new Size(size, num.Size.Height);
                    num.ValueChanged += ValueChange;
                    num.MouseCaptureChanged += FocusUI;

                    nums.Add(num);

                    x_offset += size + 20;
                }

                x_offset = 0;

            }

            TableLayoutPanel table = (TableLayoutPanel)parent;
            table.RowStyles[offset] = new RowStyle(SizeType.Absolute, 54);

            table.SetColumn(tabControl, 1);
            table.SetRow(tabControl, offset);

        }

        public override void ValueChange(object sender, EventArgs e)
        {
            NumericUpDown box = (NumericUpDown)sender;
            TabPage tabpage = (TabPage)box.Parent;
            TabControl tabcontrol = (TabControl)tabpage.Parent;
            int tab = tabcontrol.SelectedIndex;

            if (nums.Contains(box))
            {
                int pos = nums.IndexOf(box);
                Prop.Value[tab, pos] = (float)box.Value;
            }
            else
            {
                Console.WriteLine("Error: Numeric up down not linked to a value!");
            }

            base.ValueChange(sender, e);
        }
    }
}
