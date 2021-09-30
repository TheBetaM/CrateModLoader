using System;

namespace Twinsanity
{
    public class DemoInstances : Instances
    {
        public new string NodeName = "DemoInstances";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<DemoInstance>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<DemoInstance>(pos, indexes);
        }
    }
}
