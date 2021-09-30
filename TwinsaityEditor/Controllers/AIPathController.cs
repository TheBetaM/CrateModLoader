using Twinsanity;

namespace TwinsaityEditor
{
    public class AIPathController : ItemController
    {
        public new AIPath Data { get; set; }

        public AIPathController(MainForm topform, AIPath item) : base (topform, item)
        {
            Data = item;
        }

        protected override string GetName()
        {
            return $"AI Path [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            TextPrev = new string[7];
            TextPrev[0] = $"ID: {Data.ID}";
            TextPrev[1] = $"Size: {Data.Size}";
            TextPrev[2] = $"Argument0: {Data.Arg[0]}";
            TextPrev[3] = $"Argument1: {Data.Arg[1]}";
            TextPrev[4] = $"Argument2: {Data.Arg[2]}";
            TextPrev[5] = $"Argument3: {Data.Arg[3]}";
            TextPrev[6] = $"Argument4: {Data.Arg[4]}";
        }
    }
}
