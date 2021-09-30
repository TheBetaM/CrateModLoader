using System;

namespace Twinsanity
{
    public class Positions : BaseSection
    {
        public new string NodeName = "Positions";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Position>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Position>(pos, indexes);
        }
    }
}
