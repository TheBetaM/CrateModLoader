using System;

namespace Twinsanity
{
    public class SurfaceBehaviours : BaseSection
    {
        public new string NodeName = "Surface Behaviours";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<SurfaceBehaviour>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<SurfaceBehaviour>(pos, indexes);
        }
    }
}
