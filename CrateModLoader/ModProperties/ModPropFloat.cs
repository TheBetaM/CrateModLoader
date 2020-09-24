using System;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties
{
    public class ModPropFloat : ModProperty<float>
    {

        public ModPropFloat(float f) : base(f)
        {

        }
        public ModPropFloat(float f, string name, string desc) : base(f, name, desc)
        {

        }

        public override void GenerateUI(object parent, ref int offset)
        {
            base.GenerateUI(parent, ref offset);

            NumericUpDown num = new NumericUpDown();

            num.DecimalPlaces = 5;
            num.Minimum = decimal.MinValue;
            num.Maximum = decimal.MaxValue;

            num.Value = (decimal)Value;
            num.Parent = (Control)parent;
            num.Dock = DockStyle.Fill;
            num.ValueChanged += ValueChange;
            num.MouseCaptureChanged += FocusUI;

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.SetColumn(num, 1);
            table.SetRow(num, offset);

        }

        public override void ValueChange(object sender, object e)
        {
            base.ValueChange(sender, e);

            NumericUpDown box = (NumericUpDown)sender;
            Value = (float)box.Value;
        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input)
        {
            float val;
            if (float.TryParse(input, out val))
            {
                Value = val;
            }
        }

    }
}