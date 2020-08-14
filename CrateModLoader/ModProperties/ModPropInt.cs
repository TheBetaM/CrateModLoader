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

        public override void GenerateUI(TabPage page, ref int offset)
        {
            base.GenerateUI(page, ref offset);

            //offset += 20;
            NumericUpDown num = new NumericUpDown();

            num.DecimalPlaces = 0;
            num.Minimum = int.MinValue;
            num.Maximum = int.MaxValue;

            num.Value = Value;
            num.Parent = page;
            num.Location = new System.Drawing.Point(245, offset - 3);
            num.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            num.Size = new System.Drawing.Size(230, num.Size.Height);
            num.ValueChanged += ValueChange;


        }

        public override void ValueChange(object sender, EventArgs e)
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