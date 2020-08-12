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

            offset += 20;
            TextBox textBox = new TextBox();
            textBox.Text = (string)Value;
            textBox.Parent = page;
            textBox.Location = new System.Drawing.Point(10, offset);
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Size = new System.Drawing.Size(page.Width - 30, textBox.Size.Height);
            textBox.TextChanged += ValueChange;

        }

    }
}
