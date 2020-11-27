using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropBoolGUI : ModPropertyGUI<ModPropBool>
    {
        private CheckBox checkBox;

        public ModPropBoolGUI(ModPropBool p) : base(p)
        {

        }

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {
            base.GenerateUI(parent, ref offset, false);

            checkBox = new CheckBox();
            checkBox.Text = Prop.Name;
            checkBox.BackColor = Color.FromKnownColor(KnownColor.Transparent);
            checkBox.Checked = Prop.Value;
            checkBox.Parent = (Control)parent;
            checkBox.Dock = DockStyle.Fill;
            checkBox.CheckedChanged += ValueChange;
            checkBox.MouseEnter += FocusUI;

            if (Prop.HasChanged && Prop.Value != Prop.DefaultValue)
            {
                if (checkBox.Text[checkBox.Text.Length - 1] != '*')
                {
                    checkBox.Text += '*';
                }
            }
        }

        public override void UpdateUI()
        {
            checkBox.Checked = Prop.Value;
            checkBox.Text = Prop.Name;
            if (Prop.HasChanged && Prop.Value != Prop.DefaultValue)
            {
                checkBox.Text += '*';
            }
        }

        public override void ValueChange(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            Prop.Value = box.Checked;

            if (Prop.HasChanged && Prop.Value != Prop.DefaultValue)
            {
                if (box.Text[box.Text.Length - 1] != '*')
                {
                    box.Text += '*';
                }
            }

            base.ValueChange(sender, e);
        }
    }
}
