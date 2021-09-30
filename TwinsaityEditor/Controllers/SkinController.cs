using Twinsanity;

namespace TwinsaityEditor
{
    public class SkinController : ItemController
    {
        public new Skin Data { get; set; }

        public SkinController(MainForm topform, Skin item) : base(topform, item)
        {
            Data = item;
        }

        protected override string GetName()
        {
            return $"Skin [ID {Data.ID:X}/{Data.ID}]";
        }

        protected override void GenText()
        {
            TextPrev = new string[2 + (Data.SubModels * 4)];
            TextPrev[0] = $"ID: {Data.ID}";
            TextPrev[1] = $"SubModels {Data.SubModels}";
            int line = 2;
            for (int i = 0; i < Data.SubModels; i++)
            {
                TextPrev[line + 0] = $"SubModel {i}";
                TextPrev[line + 1] = $"MaterialID {Data.MaterialIDs[i]}";
                TextPrev[line + 2] = $"Block Size {Data.BlockSize[i]}";
                TextPrev[line + 3] = $"Vertexes {Data.Vertexes[i]}";
                line += 4;
            }
        }
    }
}