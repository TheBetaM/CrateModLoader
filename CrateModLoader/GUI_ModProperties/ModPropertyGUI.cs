using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropertyGUI<T> : ModPropertyGUI_Base
    {

        public T Prop;
        public Label TitleLabel = null;
        public ModMenuForm ParentForm = null;

        public ModPropertyGUI(T p)
        {
            Prop = p;
        }

        public override void GenerateUI(object parent, ref int offset, bool showTitle = true)
        {
            if (showTitle)
            {
                GenerateTitle((Control)parent, ref offset);

                ModPropertyBase Base = Prop as ModPropertyBase;
                // Changed values show a * next to the name
                if (Base.HasChanged && TitleLabel != null)
                {
                    if (TitleLabel.Text[TitleLabel.Text.Length - 1] != '*')
                    {
                        TitleLabel.Text += '*';
                    }
                }
            }

            Control oparent = (Control)parent;
            Control target = (Control)parent;
            while (oparent != null)
            {
                oparent = oparent.Parent;
                if (oparent != null)
                {
                    target = oparent;
                }
            }

            ParentForm = (ModMenuForm)target;

        }

        public override void UpdateUI()
        {
            if (TitleLabel != null)
            {
                ModPropertyBase Base = Prop as ModPropertyBase;
                TitleLabel.Text = Base.Name;
                if (Base.HasChanged)
                {
                    TitleLabel.Text += '*';
                }
            }
        }

        void GenerateTitle(Control parent, ref int offset)
        {
            TableLayoutPanel table = (TableLayoutPanel)parent;
            if (table.ColumnCount < 2)
            {
                table.ColumnCount++;
            }

            ModPropertyBase Base = Prop as ModPropertyBase;
            TitleLabel = new Label();
            TitleLabel.Text = Base.Name;
            TitleLabel.Parent = parent;
            //TitleLabel.Location = new Point(5, offset);
            TitleLabel.AutoSize = false;
            //TitleLabel.Size = new Size(240, 20);
            TitleLabel.Dock = DockStyle.Fill;
            TitleLabel.TextAlign = ContentAlignment.MiddleRight;
            TitleLabel.BackColor = Color.FromKnownColor(KnownColor.Transparent);
            TitleLabel.MouseEnter += FocusUI;

            table.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 25f);
            if (table.ColumnStyles.Count < 2)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75f));
            }

            table.SetColumn(TitleLabel, 0);
            table.SetRow(TitleLabel, offset);
        }

        public override void FocusUI(object sender, EventArgs e)
        {
            ModPropertyBase Base = Prop as ModPropertyBase;
            ParentForm.UpdateDescLabel(Base.Description);
        }

        public override void ValueChange(object sender, EventArgs e)
        {
            ModPropertyBase Base = Prop as ModPropertyBase;
            Base.ValueChange(sender, e);
            // Changed values show a * next to the name
            if (TitleLabel != null)
            {
                if (TitleLabel.Text[TitleLabel.Text.Length - 1] != '*')
                {
                    TitleLabel.Text += '*';
                }
            }
        }


    }
}
