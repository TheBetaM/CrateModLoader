using System;

namespace Twinsanity
{
    public class Textures : BaseSection
    {
        public new string NodeName = "Textures";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Texture>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Texture>(pos, indexes);
        }
    }
}
