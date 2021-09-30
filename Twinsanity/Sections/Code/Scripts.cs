using System;

namespace Twinsanity
{
    public class Scripts : BaseSection
    {
        public new string NodeName = "Scripts";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Script>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Script>(pos, indexes);
        }
    }
}
