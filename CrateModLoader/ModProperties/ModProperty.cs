using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CrateModLoader.ModProperties
{
    public class ModProperty<T> : ModPropertyBase
    {

        public T Value { get; set; }

        public T DefaultValue { get; set; }

        public Label TitleLabel = null;
        public ModMenuForm ParentForm = null;

        public ModProperty(T o, string name, string desc = "")
        {
            Value = o;
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
            else
            {
                Name = null;
            }
            Description = desc;
            DefaultValue = o;
        }
        public ModProperty(T o)
        {
            Value = o;
            Name = null;
            Description = string.Empty;
            DefaultValue = o;
        }

        public override void ResetToDefault()
        {
            Value = DefaultValue;
            HasChanged = false;
        }

        public override void Serialize(ref string line)
        {
            line += CodeName;
            line += ModCrates.Separator;
        }

        public override void DeSerialize(string input)
        {

        }

        public override void ValueChange(object sender, object e)
        {
            HasChanged = true;

            // Changed values show a * next to the name
            if (TitleLabel != null)
            {
                if (TitleLabel.Text[TitleLabel.Text.Length - 1] != '*')
                {
                    TitleLabel.Text += '*';
                }
            }
        }

        public override void GenerateUI(object parent, ref int offset)
        {
            GenerateTitle((Control)parent, ref offset);

            // Changed values show a * next to the name
            if (HasChanged && TitleLabel != null)
            {
                if (TitleLabel.Text[TitleLabel.Text.Length - 1] != '*')
                {
                    TitleLabel.Text += '*';
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

        void GenerateTitle(Control parent, ref int offset)
        {
            TableLayoutPanel table = (TableLayoutPanel)parent;
            if (table.ColumnCount < 2)
            {
                table.ColumnCount++;
            }

            TitleLabel = new Label();
            TitleLabel.Text = Name;
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

        public override void FocusUI(object sender, object e)
        {
            ParentForm.UpdateDescLabel(Description);
        }




    }
}
