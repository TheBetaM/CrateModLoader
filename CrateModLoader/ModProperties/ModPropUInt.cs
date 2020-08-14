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

        public override void GenerateUI(TabPage page, ref int offset)
        {
            base.GenerateUI(page, ref offset);

            //offset += 20;
            NumericUpDown num = new NumericUpDown();

            num.DecimalPlaces = 0;
            num.Minimum = uint.MinValue;
            num.Maximum = uint.MaxValue;

            num.Value = Value;
            num.Parent = page;
            num.Location = new System.Drawing.Point(245, offset - 3);
            num.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            num.Size = new System.Drawing.Size(230, num.Size.Height);
            num.ValueChanged += ValueChange;
            num.MouseCaptureChanged += FocusUI;

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