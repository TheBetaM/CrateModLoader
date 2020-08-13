using System;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties
{
    public class ModPropString : ModProperty<string>
    {

        public ModPropString(string text) : base(text)
        {

        }
        public ModPropString(string text, string name, string desc) : base(text, name, desc)
        {

        }

        public override void GenerateUI(TabPage page, ref int offset)
        {
            
            base.GenerateUI(page, ref offset);

            //offset += 20;
            TextBox textBox = new TextBox();
            textBox.Text = Value;
            textBox.Parent = page;
            textBox.Location = new System.Drawing.Point(245, offset);
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            textBox.Size = new System.Drawing.Size(page.Width - 250, textBox.Size.Height);
            textBox.TextChanged += ValueChange;
            textBox.MouseEnter += FocusUI;

        }

        public override void ValueChange(object sender, EventArgs e)
        {
            base.ValueChange(sender, e);

            TextBox box = (TextBox)sender;
            Value = box.Text;
        }

        public override void Serialize(ref string line)
        {
            line += Value;
        }

        public override void DeSerialize(string input)
        {
            Value = input;
        }

    }
}
