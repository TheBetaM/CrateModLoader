using System;

namespace Twinsanity
{
    public class Models : BaseSection
    {
        public new string NodeName = "Models";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Model>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Model>(pos, indexes);
        }
    }
}
