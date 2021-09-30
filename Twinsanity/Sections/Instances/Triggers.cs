using System;

namespace Twinsanity
{
    public class Triggers : BaseSection
    {
        public new string NodeName = "Triggers";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Trigger>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Trigger>(pos, indexes);
        }
    }
}
