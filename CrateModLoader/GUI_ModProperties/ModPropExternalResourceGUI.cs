using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CrateModAPI.Resources.Text;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropExternalResourceGUI : ModPropertyGUI<ModPropExternalResourceBase>
    {

        public Label InfoLabel = null;
        public object ResourceDisplay = null;

        public ModPropExternalResourceGUI(ModPropExternalResourceBase p) : base(p)
        {

        }

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {
            if (Prop.RequiresPreload && !Prop.Loaded)
            {
                return;
            }

            base.GenerateUI(parent, ref offset);

            Button button = new Button();

            button.Text = "Import";
            button.Parent = (Control)parent;
            button.Dock = DockStyle.Fill;
            button.MouseEnter += FocusUI;
            button.Click += BrowseButtonClicked;

            Button button2 = new Button();

            button2.Text = "Clear";
            button2.Parent = (Control)parent;
            button2.Dock = DockStyle.Fill;
            button2.MouseEnter += FocusUI;
            button2.Click += ClearButtonClicked;

            InfoLabel = new Label();
            InfoLabel.Text = Prop.ResourcePath;
            InfoLabel.Parent = (Control)parent;
            InfoLabel.AutoSize = false;
            InfoLabel.Dock = DockStyle.Fill;
            InfoLabel.TextAlign = ContentAlignment.MiddleLeft;
            InfoLabel.BackColor = Color.FromKnownColor(KnownColor.Transparent);
            InfoLabel.MouseEnter += FocusUI;

            TableLayoutPanel table = (TableLayoutPanel)parent;

            table.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 30f);
            table.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 15f);
            if (table.ColumnStyles.Count < 3)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            }
            if (table.ColumnStyles.Count < 4)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            }
            if (table.ColumnCount < 4)
            {
                table.ColumnCount = 4;
            }

            table.SetColumn(button, 1);
            table.SetRow(button, offset);
            table.SetColumn(button2, 2);
            table.SetRow(button2, offset);
            table.SetColumn(InfoLabel, 3);
            table.SetRow(InfoLabel, offset);

            if ((Prop.RequiresPreload || Prop.PreloadBonus) && Prop.Loaded)
            {
                if (Prop is ModPropExternalResource<Bitmap> BitmapProp)
                {
                    offset++;

                    Button button3 = new Button();

                    button3.Text = "Export";
                    button3.Parent = (Control)parent;
                    button3.Dock = DockStyle.Top;
                    button3.Height = 24;
                    button3.MouseEnter += FocusUI;
                    button3.Click += ExportButtonClicked;

                    PictureBox textureDisplay = new PictureBox();

                    textureDisplay.Image = BitmapProp.Resource;
                    textureDisplay.Width = BitmapProp.Resource.Width;
                    textureDisplay.Height = BitmapProp.Resource.Height + 16;

                    textureDisplay.Parent = (Control)parent;
                    textureDisplay.MouseEnter += FocusUI;

                    ResourceDisplay = textureDisplay;

                    table.RowCount++;
                    table.RowStyles.Add(new RowStyle(SizeType.Absolute, Math.Max(button3.Height, BitmapProp.Resource.Height + 16)));
                    table.SetColumn(button3, 0);
                    table.SetRow(button3, offset);
                    table.SetColumn(textureDisplay, 1);
                    table.SetRow(textureDisplay, offset);
                    table.SetColumnSpan(textureDisplay, 3);
                }

            }
        }

        public override void UpdateUI()
        {
            if (ResourceDisplay != null)
            {
                if (Prop is ModPropExternalResource<Bitmap> BitmapProp)
                {
                    PictureBox textureDisplay = (PictureBox)ResourceDisplay;

                    textureDisplay.Image = BitmapProp.Resource;
                    textureDisplay.Width = BitmapProp.Resource.Width;
                    textureDisplay.Height = BitmapProp.Resource.Height + 16;

                    // may need to adjust table row if the height changes?
                }
            }

            base.UpdateUI();
        }

        public void BrowseButtonClicked(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Prop.BrowseFilter + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Prop.TryResource(openFileDialog.FileName))
                {
                    InfoLabel.ForeColor = Color.Black;
                    InfoLabel.Text = openFileDialog.FileName;
                    Prop.Value = true;
                    Prop.ResourcePath = openFileDialog.FileName;
                    ValueChange(sender, e);
                }
                else
                {
                    Prop.Value = false;
                    Prop.ResourcePath = "";
                    Prop.ResetToDefault();
                    InfoLabel.ForeColor = Color.Red;
                    InfoLabel.Text = "Invalid or unsupported file.";
                }
            }
        }

        public void ClearButtonClicked(object sender, EventArgs e)
        {
            Prop.Value = false;
            Prop.ResourcePath = "";
            Prop.ResetToDefault();
            InfoLabel.ForeColor = Color.Black;
            InfoLabel.Text = "";
        }

        public void ExportButtonClicked(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = Prop.BrowseFilter + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";
            saveFileDialog.FileName = Prop.CodeName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Prop.ResourceToFile(saveFileDialog.FileName);
            }
        }

        public override void ValueChange(object sender, EventArgs e)
        {
            base.ValueChange(sender, e);

            UpdateUI();
        }

    }
}
