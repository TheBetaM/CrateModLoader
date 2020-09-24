using System;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties
{
    public class ModPropInt : ModProperty<int>
    {

        public ModPropInt(int i) : base(i)
        {

        }
        public ModPropInt(int i, string name, string desc) : base(i, name, desc)
        {

        }

        public override void GenerateUI(object parent, ref int offset)
        {
            base.GenerateUI(parent, ref offset);

            NumericUpDown num = new NumericUpDown();

            num.DecimalPlaces = 0;
            num.Minimum = int.MinValue;
            num.Maximum = int.MaxValue;

            num.Value = Value;
            num.Parent = (Control)parent;
            num.Dock = DockStyle.Fill;
            num.ValueChanged += ValueChange;

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.SetColumn(num, 1);
            table.SetRow(num, offset);
        }

        public override void ValueChange(object sender, object e)
        {
            base.ValueChange(sender, e);

            NumericUpDown box = (NumericUpDown)sender;
            Value = (int)box.Value;
        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input)
        {
            int val;
            if (int.TryParse(input, out val))
            {
                Value = val;
            }
        }

    }
}