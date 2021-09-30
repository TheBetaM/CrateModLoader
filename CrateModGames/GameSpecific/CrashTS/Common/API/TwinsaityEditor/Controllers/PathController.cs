using Twinsanity;

namespace TwinsaityEditor
{
    public class PathController : ItemController
    {
        public new Path Data { get; set; }

        public PathController(MainForm topform, Path item) : base (topform, item)
        {
            Data = item;
            AddMenu("Open editor", Menu_OpenEditor);
        }

        protected override string GetName()
        {
            return $"Path [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            TextPrev = new string[4 + Data.Positions.Count + Data.Params.Count];
            TextPrev[0] = $"ID: {Data.ID}";
            TextPrev[1] = $"Size: {Data.Size}";
            TextPrev[2] = $"Points: {Data.Positions.Count}";
            for (int i = 0; i < Data.Positions.Count; ++i)
            {
                TextPrev[3 + i] = $"Point{i}: {Data.Positions[i].X}, {Data.Positions[i].Y}, {Data.Positions[i].Z}";
            }
            TextPrev[3 + Data.Positions.Count] = $"Params: {Data.Params.Count}";
            for (int i = 0; i < Data.Params.Count; ++i)
            {
                TextPrev[4 + Data.Positions.Count + i] = $"Param{i}: {Data.Params[i].P2}, {Data.Params[i].P2}";
            }
        }

        private void Menu_OpenEditor()
        {
            MainFile.OpenEditor((SectionController)Node.Parent.Tag);
        }
    }
}
