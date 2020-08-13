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

        public override void GenerateUI(TabPage page, ref int offset)
        {
            base.GenerateUI(page, ref offset);

            offset += 20;
            NumericUpDown num = new NumericUpDown();

            num.DecimalPlaces = 8;
            num.Minimum = decimal.MinValue;
            num.Maximum = decimal.MaxValue;

            num.Value = (decimal)Value;
            num.Parent = page;
            num.Location = new System.Drawing.Point(10, offset);
            num.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            num.Size = new System.Drawing.Size(230, num.Size.Height);
            num.ValueChanged += ValueChange;


        }

        public override void ValueChange(object sender, EventArgs e)
        {
            base.ValueChange(sender, e);

            NumericUpDown box = (NumericUpDown)sender;
            Value = (float)box.Value;
        }

    }
}