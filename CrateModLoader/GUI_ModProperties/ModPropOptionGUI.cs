using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CrateModLoader.ModProperties.GUI
{
    public class ModPropOptionGUI : ModPropertyGUI<ModPropOption>
    {

        private ConsoleMode Console;
        private RegionType Region;

        public ModPropOptionGUI(ModPropOption p, ConsoleMode console, RegionType region) : base(p)
        {
            Console = console;
            Region = region;
        }

        public override void GenerateUI(object parent, ref int offset, bool showTitle)
        {
            if (!Prop.Allowed(Console, Region))
            {
                return;
            }
            Prop.SetItemCount();

            TableLayoutPanel table = (TableLayoutPanel)parent;

            if (Prop.SingleChoice)
            {
                base.GenerateUI(parent, ref offset, false);

                CheckBox checkBox = new CheckBox();
                checkBox.Text = Prop.Name;
                checkBox.BackColor = Color.FromKnownColor(KnownColor.Transparent);
                checkBox.Checked = Prop.Enabled;
                checkBox.Parent = (Control)parent;
                checkBox.Dock = DockStyle.Fill;
                checkBox.CheckedChanged += ValueChange;
                checkBox.MouseEnter += FocusUI;

                if (Prop.HasChanged && Prop.Value != Prop.DefaultValue)
                {
                    if (checkBox.Text[checkBox.Text.Length - 1] != '*')
                    {
                        checkBox.Text += '*';
                    }
                }


                table.SetColumn(checkBox, 0);
                table.SetColumnSpan(checkBox, 2);
                table.SetRow(checkBox, offset);
            }
            else
            {
                base.GenerateUI(parent, ref offset);

                ComboBox comboBox = new ComboBox();
                comboBox.Parent = (Control)parent;
                comboBox.Dock = DockStyle.Fill;
                comboBox.Items.Clear();

                if (Prop.Items == null || Prop.Items.Count <= 0)
                {
                    for (int i = 0; i < Prop.ItemCount; i++)
                    {
                        comboBox.Items.Add(i);
                    }
                }
                else
                {
                    for (int i = 0; i < Prop.Items.Count; i++)
                    {
                        comboBox.Items.Add(Prop.Items[i]);
                    }
                }

                comboBox.SelectedIndex = Prop.Value;
                comboBox.SelectedIndexChanged += ValueChange;
                comboBox.MouseEnter += FocusUI;

                table.SetColumn(comboBox, 1);
                table.SetRow(comboBox, offset);


            }
        }

        public override void ValueChange(object sender, EventArgs e)
        {
            Prop.SetItemCount();

            if (Prop.SingleChoice)
            {
                CheckBox box = (CheckBox)sender;
                if (box.Checked)
                {
                    Prop.Value = 1;
                }
                else
                {
                    Prop.Value = 0;
                }

                if (Prop.HasChanged && Prop.Value != Prop.DefaultValue)
                {
                    if (box.Text[box.Text.Length - 1] != '*')
                    {
                        box.Text += '*';
                    }
                }
            }
            else
            {
                ComboBox box = (ComboBox)sender;
                Prop.Value = box.SelectedIndex;
            }

            base.ValueChange(sender, e);
        }
    }
}
