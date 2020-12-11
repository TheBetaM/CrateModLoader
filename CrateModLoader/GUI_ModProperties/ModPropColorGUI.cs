using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropColorGUI : ModPropertyGUI<ModPropColor>
    {

        private PictureBox colorDisplay;

        public ModPropColorGUI(ModPropColor p) : base(p)
        {

        }

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {
            base.GenerateUI(parent, ref offset);

            colorDisplay = new PictureBox();

            Bitmap pixel = new Bitmap(1, 1);
            pixel.SetPixel(0, 0, Color.FromArgb(Prop.A, Prop.R, Prop.G, Prop.B));
            colorDisplay.Image = pixel;
            colorDisplay.Dock = DockStyle.Fill;
            colorDisplay.SizeMode = PictureBoxSizeMode.StretchImage;

            colorDisplay.Parent = (Control)parent;
            colorDisplay.MouseEnter += FocusUI;

            Button button = new Button();

            button.Text = "Set";
            button.Parent = (Control)parent;
            button.Dock = DockStyle.Fill;
            button.MouseEnter += FocusUI;
            button.Click += SetButtonClicked;

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.SetColumn(colorDisplay, 1);
            table.SetRow(colorDisplay, offset);
            table.SetColumn(button, 2);
            table.SetRow(button, offset);

            if (table.ColumnStyles.Count < 3)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            }
            if (table.ColumnCount < 3)
            {
                table.ColumnCount = 3;
            }

        }

        public void SetButtonClicked(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            colorPicker.Color = Color.FromArgb(Prop.A, Prop.R, Prop.G, Prop.B);

            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Prop.Value[0] = colorPicker.Color.R;
                Prop.Value[1] = colorPicker.Color.G;
                Prop.Value[2] = colorPicker.Color.B;
                Prop.Value[3] = colorPicker.Color.A;
                UpdateUI();
                ValueChange(sender, e);
            }
        }

        public override void UpdateUI()
        {
            Bitmap pixel = new Bitmap(1, 1);
            pixel.SetPixel(0, 0, Color.FromArgb(Prop.A, Prop.R, Prop.G, Prop.B));
            colorDisplay.Image = pixel;

            base.UpdateUI();
        }

        public override void ValueChange(object sender, EventArgs e)
        {

            base.ValueChange(sender, e);
        }
    }
}
