﻿using System;

namespace CrateModLoader.ModProperties
{
    public class ModPropString : ModProperty<string>
    {

        public int MaxLength = int.MaxValue;

        public ModPropString(string text) : base(text)
        {

        }
        public ModPropString(string text, string name, string desc) : base(text, name, desc)
        {

        }
        public ModPropString(string text, int maxLen) : base(text)
        {
            MaxLength = maxLen;
        }
        public ModPropString(string text, int maxLen, string name, string desc) : base(text, name, desc)
        {
            MaxLength = maxLen;
        }

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);

            line += Value;
        }

        public override void DeSerialize(string input, ModCrate crate)
        {
            Value = input;
        }

    }
}
