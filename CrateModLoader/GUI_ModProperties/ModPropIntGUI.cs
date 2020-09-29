using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropIntGUI : ModPropertyGUI<ModPropInt>
    {

        public ModPropIntGUI(ModPropInt p) : base(p)
        {

        }

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {
            base.GenerateUI(parent, ref offset);

            NumericUpDown num = new NumericUpDown();

            num.DecimalPlaces = 0;
            num.Minimum = int.MinValue;
            num.Maximum = int.MaxValue;

            num.Value = Prop.Value;
            num.Parent = (Control)parent;
            num.Dock = DockStyle.Fill;
            num.ValueChanged += ValueChange;

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.SetColumn(num, 1);
            table.SetRow(num, offset);
        }

        public override void ValueChange(object sender, EventArgs e)
        {
            NumericUpDown box = (NumericUpDown)sender;
            Prop.Value = (int)box.Value;

            base.ValueChange(sender, e);
        }

    }
}
