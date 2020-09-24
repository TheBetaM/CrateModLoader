using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{
    /// <summary>
    /// Property that automatically gets added to the quick options menu in addition to being in the Mod Menu.
    /// </summary>
    public class ModPropOption : ModProperty<int>
    {

        public List<RegionType> AllowedRegions { get; set; }
        public List<ConsoleMode> AllowedConsoles { get; set; }
        public List<string> Items { get; set; }
        public List<string> ItemsDesc { get; set; }
        public int ItemCount = 0;
        public bool SingleChoice = false;
        public override string ToString() => Name;
        public bool Enabled => Value != 0;
        public bool Disabled => Value == 0;
        public bool On => Value != 0;
        public bool Off => Value == 0;

        /// <summary>
        /// Single choice option with just the code name
        /// </summary>
        public ModPropOption() : base(0)
        {

        }
        public ModPropOption(int defaultVal) : base(defaultVal)
        {

        }
        public ModPropOption(string name, string desc) : base(0, name, desc)
        {

        }
        public ModPropOption(int defaultVal, string name, string desc) : base(defaultVal, name, desc)
        {

        }

        public void SetItemCount()
        {
            if (Items == null || Items.Count <= 1)
            {
                SingleChoice = true;
            }
            else
            {
                if (ItemCount == 0)
                {
                    ItemCount = Items.Count;
                }
                SingleChoice = false;
            }
        }

        public bool Allowed()
        {
            if (Hidden)
                return false;
            if (AllowedRegions != null && AllowedRegions.Count > 0 && !AllowedRegions.Contains(ModLoaderGlobals.Region))
                return false;
            if (AllowedConsoles != null && AllowedConsoles.Count > 0 && !AllowedConsoles.Contains(ModLoaderGlobals.Console))
                return false;

            return true;
        }


        public override void GenerateUI(object parent, ref int offset)
        {
            if (!Allowed())
            {
                return;
            }
            SetItemCount();

            TableLayoutPanel table = (TableLayoutPanel)parent;

            if (SingleChoice)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = Name;
                checkBox.BackColor = Color.FromKnownColor(KnownColor.Transparent);
                checkBox.Checked = Enabled;
                checkBox.Parent = (Control)parent;
                checkBox.Dock = DockStyle.Fill;
                checkBox.CheckedChanged += ValueChange;
                checkBox.MouseEnter += FocusUI;

                if (HasChanged && Value != DefaultValue)
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

                if (Items == null || Items.Count <= 0)
                {
                    for (int i = 0; i < ItemCount; i++)
                    {
                        comboBox.Items.Add(i);
                    }
                }
                else
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        comboBox.Items.Add(Items[i]);
                    }
                }

                comboBox.SelectedIndex = Value;
                comboBox.SelectedIndexChanged += ValueChange;
                comboBox.MouseEnter += FocusUI;

                table.SetColumn(comboBox, 1);
                table.SetRow(comboBox, offset);


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

        public override void ValueChange(object sender, object e)
        {
            base.ValueChange(sender, e);
            SetItemCount();

            if (SingleChoice)
            {
                CheckBox box = (CheckBox)sender;
                if (box.Checked)
                {
                    Value = 1;
                }
                else
                {
                    Value = 0;
                }

                if (HasChanged && Value != DefaultValue)
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
                Value = box.SelectedIndex;
            }
            
        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input)
        {
            int val;
            if (int.TryParse(input, out val))
            {
                Value = val;
            }
        }

    }
}