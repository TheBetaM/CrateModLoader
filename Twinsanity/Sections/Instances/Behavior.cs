using System;

namespace Twinsanity
{
    public class Behaviors : BaseSection
    {
        public new string NodeName = "Behaviors";
        
        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            base.Load<Behavior>(ref File, ref Reader);
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<Behavior>(pos, indexes);
        }
    }
}
