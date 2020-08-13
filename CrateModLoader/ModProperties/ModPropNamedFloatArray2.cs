using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

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

        private List<NumericUpDown> nums = new List<NumericUpDown>();

        public override void GenerateUI(TabPage page, ref int offset)
        {
            base.GenerateUI(page, ref offset);

            nums.Clear();

            offset += 20;

            TabControl tabControl = new TabControl();
            tabControl.TabPages.Clear();
            tabControl.Parent = page;
            //tabControl.Multiline = true;
            tabControl.Location = new Point(10, offset);
            tabControl.Size = new Size(page.Width - 20, 50);
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            int x_offset = 0;
            int size = 130;

            for (int i = 0; i < Value.GetLength(0); i++)
            {
                tabControl.TabPages.Add(PropNames[i]);
                tabControl.TabPages[tabControl.TabPages.Count - 1].AutoScroll = false;

                for (int d = 0; d < Value.GetLength(1); d++)
                {
                    NumericUpDown num = new NumericUpDown();

                    num.DecimalPlaces = 5;
                    num.Minimum = decimal.MinValue;
                    num.Maximum = decimal.MaxValue;

                    num.Value = (decimal)Value[i, d];
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

            offset += 30;

        }

        public override void ValueChange(object sender, EventArgs e)
        {
            base.ValueChange(sender, e);

            NumericUpDown box = (NumericUpDown)sender;
            TabPage tabpage = (TabPage)box.Parent;
            TabControl tabcontrol = (TabControl)tabpage.Parent;
            int tab = tabcontrol.SelectedIndex;

            if (nums.Contains(box))
            {
                int pos = nums.IndexOf(box);
                Value[tab, pos] = (float)box.Value;
            }
            else
            {
                Console.WriteLine("Error: Numeric up down not linked to a value!");
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
            for (int i = 0; i < Value.GetLength(0); i++)
            {
                for (int d = 0; d < Value.GetLength(1); d++)
                {
                    line += Value[i,d];
                    line += ";";
                }
            }
        }

        public override void DeSerialize(string input)
        {
            string[] vals = input.Split(';');

            if (vals.Length != Value.GetLength(0) + 1 && vals.Length != Value.GetLength(0))
            {
                Console.WriteLine("Error: Input array length mismatch!");
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