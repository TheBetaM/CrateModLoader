using System;

namespace Twinsanity
{
    public class GCs : BaseSection
    {
        public new string NodeName = "GCs";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<GC>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<GC>(pos, indexes);
        }
    }
}
