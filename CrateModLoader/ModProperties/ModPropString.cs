using System;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties
{
    public class ModPropString : ModProperty<string>
    {

        public int MaxLength = int.MaxValue;

        public ModPropString(string text) : base(text)
        {

        }
        public ModPropString(string text, string name, string desc) : base(text, name, desc)
        {

        }
        public ModPropString(string text, int maxLen) : base(text)
        {
            MaxLength = maxLen;
        }
        public ModPropString(string text, int maxLen, string name, string desc) : base(text, name, desc)
        {
            MaxLength = maxLen;
        }

        public override void GenerateUI(object parent, ref int offset)
        {
            
            base.GenerateUI(parent, ref offset);

            TextBox textBox = new TextBox();
            textBox.Text = Value;
            textBox.Parent = (Control)parent;
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.TextChanged += ValueChange;
            textBox.MouseEnter += FocusUI;
            if (MaxLength != int.MaxValue)
            {
                textBox.MaxLength = MaxLength;
            }

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.SetColumn(textBox, 1);
            table.SetRow(textBox, offset);

        }

        public override void ValueChange(object sender, object e)
        {
            base.ValueChange(sender, e);

            TextBox box = (TextBox)sender;
            Value = box.Text;
        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input)
        {
            Value = input;
        }

    }
}
