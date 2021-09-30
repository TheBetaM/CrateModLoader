using System;

namespace Twinsanity
{
    public class Instances : BaseSection
    {
        public new string NodeName = "Instances";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Instance>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Instance>(pos, indexes);
        }
    }
}
