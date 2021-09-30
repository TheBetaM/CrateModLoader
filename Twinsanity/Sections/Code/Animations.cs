using System;

namespace Twinsanity
{
    public class Animations : BaseSection
    {
        public new string NodeName = "Animations";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Animation>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Animation>(pos, indexes);
        }
    }
}
