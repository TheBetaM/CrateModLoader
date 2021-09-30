using System;

namespace Twinsanity
{
    public class Terrains : BaseSection
    {
        public new string NodeName = "Terrains";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Terrain>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Terrain>(pos, indexes);
        }
    }
}
