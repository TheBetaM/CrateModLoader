using System;
using System.Drawing;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties
{
    public class ModPropBool : ModProperty<bool>
    {

        public ModPropBool(bool b) : base(b)
        {

        }
        public ModPropBool(bool b, string name, string desc) : base(b, name, desc)
        {

        }

        public override void GenerateUI(TabPage page, ref int offset)
        {
            //base.GenerateUI(page, ref offset);
            
            CheckBox checkBox = new CheckBox();
            checkBox.Text = Name;
            checkBox.BackColor = Color.FromKnownColor(KnownColor.Transparent);
            checkBox.Checked = Value;
            checkBox.Parent = page;
            checkBox.Location = new Point(10, offset);
            checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox.Size = new Size(page.Width - 30, checkBox.Size.Height);
            checkBox.CheckedChanged += ValueChange;
            checkBox.MouseEnter += FocusUI;

            if (HasChanged && Value != DefaultValue)
            {
                if (checkBox.Text[checkBox.Text.Length - 1] != '*')
                {
                    checkBox.Text += '*';
                }
            }
        }

        public override void ValueChange(object sender, EventArgs e)
        {
            base.ValueChange(sender, e);

            CheckBox box = (CheckBox)sender;
            Value = box.Checked;

            if (HasChanged && Value != DefaultValue)
            {
                if (box.Text[box.Text.Length - 1] != '*')
                {
                    box.Text += '*';
                }
            }
        }

        public override void Serialize(ref string line)
        {
            if (Value)
                line += "1";
            else
                line += "0";
        }

        public override void DeSerialize(string input)
        {
            if (input == "1")
                Value = true;
            else
                Value = false;
        }

    }
}