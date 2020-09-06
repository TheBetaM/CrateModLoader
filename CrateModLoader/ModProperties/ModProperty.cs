﻿using System;
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

        public override void GenerateUI(TabPage page, ref int offset)
        {
            GenerateTitle(page, ref offset);

            // Changed values show a * next to the name
            if (HasChanged && TitleLabel != null)
            {
                if (TitleLabel.Text[TitleLabel.Text.Length - 1] != '*')
                {
                    TitleLabel.Text += '*';
                }
            }

            Control parent = page;
            Control target = page;
            while (parent != null)
            {
                parent = parent.Parent;
                if (parent != null)
                {
                    target = parent;
                }
            }

            ParentForm = (ModMenuForm)target;

        }

        public override void ValueChange(object sender, System.EventArgs e)
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

        void GenerateTitle(TabPage page, ref int offset)
        {
            TitleLabel = new Label();
            TitleLabel.Text = Name;
            TitleLabel.Parent = page;
            TitleLabel.Location = new Point(5, offset);
            TitleLabel.AutoSize = false;
            TitleLabel.Size = new Size(240, 20);
            TitleLabel.TextAlign = ContentAlignment.TopRight;
            TitleLabel.BackColor = Color.FromKnownColor(KnownColor.Transparent);
            TitleLabel.MouseEnter += FocusUI;
        }

        public override void FocusUI(object sender, object e)
        {
            ParentForm.UpdateDescLabel(Description);
        }




    }
}
