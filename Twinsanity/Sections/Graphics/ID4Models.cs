using System;

namespace Twinsanity
{
    public class ID4Models : BaseSection
    {
        public new string NodeName = "ID4 Models";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<ID4Model>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<ID4Model>(pos, indexes);
        }
    }
}
