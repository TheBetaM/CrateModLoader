using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropFloatGUI : ModPropertyGUI<ModPropFloat>
    {

        public ModPropFloatGUI(ModPropFloat p) : base(p)
        {

        }

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {
            base.GenerateUI(parent, ref offset);

            NumericUpDown num = new NumericUpDown();

            num.DecimalPlaces = 5;
            num.Minimum = decimal.MinValue;
            num.Maximum = decimal.MaxValue;

            num.Value = (decimal)Prop.Value;
            num.Parent = (Control)parent;
            num.Dock = DockStyle.Fill;
            num.ValueChanged += ValueChange;
            num.MouseCaptureChanged += FocusUI;

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.SetColumn(num, 1);
            table.SetRow(num, offset);

        }

        public override void ValueChange(object sender, EventArgs e)
        {
            NumericUpDown box = (NumericUpDown)sender;
            Prop.Value = (float)box.Value;

            base.ValueChange(sender, e);
        }

    }
}
