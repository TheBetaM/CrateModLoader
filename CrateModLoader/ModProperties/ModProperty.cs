using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using CrateModLoader.ModProperties;

namespace CrateModLoader
{
    public class ModProperty<T> : ModPropertyBase
    {

        public T Value { get; set; }

        public T DefaultValue { get; set; }

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

        }

        public override void ValueChange(object sender, System.EventArgs e)
        {
            HasChanged = true;
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
            Label TitleLabel = new Label();
            TitleLabel.Text = Name;
            TitleLabel.Parent = page;
            TitleLabel.Location = new Point(10, offset);
            TitleLabel.AutoSize = true;
            TitleLabel.BackColor = Color.FromKnownColor(KnownColor.Transparent);
        }

        


    }
}
