using System;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties
{
    public class ModPropUInt : ModProperty<uint>
    {

        public ModPropUInt(uint i) : base(i)
        {

        }
        public ModPropUInt(uint i, string name, string desc) : base(i, name, desc)
        {

        }

        public override void GenerateUI(Control parent, ref int offset)
        {
            base.GenerateUI(parent, ref offset);

            NumericUpDown num = new NumericUpDown();

            num.DecimalPlaces = 0;
            num.Minimum = uint.MinValue;
            num.Maximum = uint.MaxValue;

            num.Value = Value;
            num.Parent = parent;
            num.Dock = DockStyle.Fill;
            num.ValueChanged += ValueChange;
            num.MouseCaptureChanged += FocusUI;

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.SetColumn(num, 1);
            table.SetRow(num, offset);

        }

        public override void ValueChange(object sender, EventArgs e)
        {
            base.ValueChange(sender, e);

            NumericUpDown box = (NumericUpDown)sender;
            Value = (uint)box.Value;
        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input)
        {
            uint val;
            if (uint.TryParse(input, out val))
            {
                Value = val;
            }
        }

    }
}