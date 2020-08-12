using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using CrateModLoader.ModProperties;

namespace CrateModLoader
{
    public class ModProperty<T> : ModPropertyBase
    {

        // Code access
        public T Value { get; set; }

        public T DefaultValue { get; }

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
            DefaultValue = Value;
        }
        public ModProperty(T o)
        {
            Value = o;
            Name = null;
            Description = string.Empty;
            DefaultValue = Value;
        }

        public override void GenerateUI(TabPage page, ref int offset)
        {
            GenerateTitle(page, ref offset);

        }

        public override void ValueChange(object sender, System.EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox box = (CheckBox)sender;
                Value = (T)(object)box.Checked;
            }
            else if (sender is TextBox)
            {
                TextBox box = (TextBox)sender;
                Value = (T)(object)box.Text;
            }
            else if (sender is ComboBox)
            {
                ComboBox box = (ComboBox)sender;
                Value = (T)(object)box.SelectedIndex;
            }
        }

        public override void ResetToDefault()
        {
            Value = DefaultValue;
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
