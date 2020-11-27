using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropStringGUI : ModPropertyGUI<ModPropString>
    {

        private TextBox textBox;

        public ModPropStringGUI(ModPropString p) : base(p)
        {

        }

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {

            base.GenerateUI(parent, ref offset);

            textBox = new TextBox();
            textBox.Text = Prop.Value;
            textBox.Parent = (Control)parent;
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.TextChanged += ValueChange;
            textBox.MouseEnter += FocusUI;
            if (Prop.MaxLength != int.MaxValue)
            {
                textBox.MaxLength = Prop.MaxLength;
            }

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.SetColumn(textBox, 1);
            table.SetRow(textBox, offset);

        }

        public override void UpdateUI()
        {
            textBox.Text = Prop.Value;

            base.UpdateUI();
        }

        public override void ValueChange(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            Prop.Value = box.Text;

            base.ValueChange(sender, e);
        }

    }
}
