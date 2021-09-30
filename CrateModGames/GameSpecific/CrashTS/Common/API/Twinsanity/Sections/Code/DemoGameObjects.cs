using System;

namespace Twinsanity
{
    public class DemoGameObjects : GameObjects
    {
        public new string NodeName = "Demo Game Objects";

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<DemoGameObject>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<DemoGameObject>(pos, indexes);
        }
    }
}
