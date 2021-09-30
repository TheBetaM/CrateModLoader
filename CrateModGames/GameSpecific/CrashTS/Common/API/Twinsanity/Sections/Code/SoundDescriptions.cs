using System;

namespace Twinsanity
{
    public class SoundDescriptions : BaseSection
    {
        public new string NodeName = "SoundDescriptions";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<SoundDescription>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<SoundDescription>(pos, indexes);
        }
    }
}
