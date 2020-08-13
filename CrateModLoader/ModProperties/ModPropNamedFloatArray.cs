using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

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

            int x_offset = 10;
            int size = 150;

            for (int i = 0; i < Value.Length; i++)
            {
                tabControl.TabPages.Add(PropNames[i]);
                tabControl.TabPages[tabControl.TabPages.Count - 1].AutoScroll = false;

                NumericUpDown num = new NumericUpDown();

                num.DecimalPlaces = 5;
                num.Minimum = decimal.MinValue;
                num.Maximum = decimal.MaxValue;

                num.Value = (decimal)Value[i];
                num.Parent = tabControl.TabPages[tabControl.TabPages.Count - 1];
                num.Location = new Point(0, 2);
                num.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                num.Size = new Size(size, num.Size.Height);
                num.ValueChanged += ValueChange;
                num.MouseCaptureChanged += FocusUI;

                nums.Add(num);

            }

            offset += 30;

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