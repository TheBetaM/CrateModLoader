using System;

namespace Twinsanity
{
    public class GameObjects : BaseSection
    {
        public new string NodeName = "Game Objects";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<GameObject>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<GameObject>(pos, indexes);
        }
    }
}
