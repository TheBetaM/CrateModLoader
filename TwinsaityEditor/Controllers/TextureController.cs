using System.Collections.Generic;
using Twinsanity;

namespace TwinsaityEditor
{
    public class TextureController : ItemController
    {
        public new Texture Data { get; set; }

        public TextureController(MainForm topform, Texture item) : base (topform, item)
        {
            Data = item;
            if (Data.RawData != null)
            {
                AddMenu("Open viewer", Menu_OpenViewer);
            }
        }

        protected override string GetName()
        {
            return string.Format("Texture [ID {0:X8}]", Data.ID);
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();
            text.Add(string.Format("ID: {0:X8}", Data.ID));
            text.Add($"Size: {Data.Size}");
            text.Add($"Resolution: {Data.Width}x{Data.Height}");
            text.Add($"Mip levels: {Data.MipLevels}");
            text.Add($"Texture format: {Data.PixelFormat}");
            text.Add($"VRAM storage format: {Data.DestinationPixelFormat}");
            text.Add($"Texture function: {Data.TexFun}");
            text.Add($"Color component: {Data.ColorComponent}");
            TextPrev = text.ToArray();
        }

        private void Menu_OpenViewer()
        {
            MainFile.OpenTextureViewer(this);
        }
    }
}
