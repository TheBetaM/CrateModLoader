using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{
    /// <summary>
    /// Property that automatically gets added to the quick options menu in addition to being in the Mod Menu.
    /// </summary>
    public class ModPropOption : ModProperty<int>
    {

        public List<string> Items { get; set; }
        public List<string> ItemsDesc { get; set; }
        public int ItemCount = 0;
        public bool SingleChoice = false;
        public override string ToString() => Name;
        public bool Enabled => Value != 0;
        public bool Disabled => Value == 0;
        public bool On => Value != 0;
        public bool Off => Value == 0;
        //public bool ModMenuOnly = false; // Whether this property can show up in quick options or not (soft hidden)

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
        public ModPropOption(Mod mod) : base(0, mod.Name, mod.Description)
        {
            TargetMod = mod;
        }
        public ModPropOption(Mod mod, int defaultVal) : base(defaultVal, mod.Name, mod.Description)
        {
            TargetMod = mod;
        }
        public ModPropOption(Mod mod, string name, string desc) : base(0, name, desc)
        {
            TargetMod = mod;
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

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input, ModCrate crate)
        {
            int val;
            if (int.TryParse(input, out val))
            {
                Value = val;
            }
        }

    }
}