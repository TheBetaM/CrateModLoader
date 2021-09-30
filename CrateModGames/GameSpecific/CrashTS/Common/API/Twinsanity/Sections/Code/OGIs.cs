using System;

namespace Twinsanity
{
    public class OGIs : BaseSection
    {
        public new string NodeName = "OGIs";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<OGI>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<OGI>(pos, indexes);
        }
    }
}
