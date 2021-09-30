/**
 * Code taken from https://github.com/Robmaister/SharpFont/blob/master/Source/Examples/FontService.cs
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using SharpFont;

namespace TwinsaityEditor.FontWrapper
{
    public class FontService
    {
        private Library _lib;

        internal Face FontFace { get => _fontFace; set => SetFont(value); }
        private Face _fontFace;

        internal float Size { get => _size; set => SetSize(value); }
        private float _size;

        internal FontFormatCollection SupportedFormats { get; private set; }

        internal FontService()
        {
            _lib = new Library();
            _size = 9f;
            SupportedFormats = new FontFormatCollection();
            AddFormat("TrueType", "ttf");
            AddFormat("OpenType", "otf");
        }

        private void AddFormat(string name, string ext)
        {
            SupportedFormats.Add(name, ext);
        }

        internal void SetFont(Face face)
        {
            _fontFace = face;
            SetSize(this.Size);
        }

        internal void SetFont(string filename)
        {
            FontFace = new Face(_lib, filename);
            SetSize(this.Size);
        }

        internal void SetSize(float size)
        {
            if (size <= 0)
                throw new ArgumentException("Font size must be non-null positive.");
            _size = size;
            FontFace?.SetCharSize(0, size, 0, 96);
        }

        internal IEnumerable<FileInfo> GetFontFiles(DirectoryInfo folder, bool recurse)
        {
            var files = new List<FileInfo>();
            var option = recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach(var file in folder.GetFiles("*.*", option))
            {
                if (SupportedFormats.ContainsExt(file.Extension))
                {
                    files.Add(file);
                }
            }
            return files;
        }

        #region TEXT_RENDERING

        internal virtual Bitmap RenderString(string text)
        {
            try
            {
                return RenderString(_lib, FontFace, text, SystemColors.ControlText, Color.Transparent);
            }
            catch { }
            return null;
        }

        internal virtual Bitmap RenderString(string text, Color foreColor)
        {
            return RenderString(_lib, FontFace, text, foreColor, Color.Transparent);
        }

        internal virtual Bitmap RenderString(string text, Color foreColor, Color backColor)
        {
            return RenderString(_lib, FontFace, text, foreColor, backColor);
        }

        internal static Bitmap RenderString(Library library, Face face, string text, Color foreColor, Color backColor)
        {
            var measuredChars = new List<DebugChar>();
            var renderedChars = new List<DebugChar>();
            float penX = 0, penY = 0;
            float stringWidth = 0;
            float stringHeight = 0;
            float overrun = 0;
            float underrun = 0;
            float kern = 0;
            float spaceError = 0;
            bool trackingUnderrun = true;
            int rightEdge = 0;

            float top = 0, bottom = 0;

            for (int i = 0; i < text.Length; ++i) 
            {
                char c = text[i];

                uint glyphIndex = face.GetCharIndex(c);

                face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

                float gAdvanceX = (float)face.Glyph.Advance.X;
                float gBearingX = (float)face.Glyph.Metrics.HorizontalBearingX;
                float gWidth = face.Glyph.Metrics.Width.ToSingle();
                var rc = new DebugChar(c, gAdvanceX, gBearingX, gWidth);

                underrun += -(gBearingX);
                if (stringWidth == 0)
                    stringWidth += underrun;
                if (trackingUnderrun)
                    rc.Underrun = underrun;
                if (trackingUnderrun && underrun <= 0) 
                {
                    underrun = 0;
                    trackingUnderrun = false;
                }

                if (gBearingX + gWidth > 0 || gAdvanceX > 0)
                {
                    overrun -= Math.Max(gBearingX + gWidth, gAdvanceX);
                    if (overrun <= 0) overrun = 0;
                }
                overrun += (gBearingX == 0 && gWidth == 0 ? 0 : gBearingX + gWidth - gAdvanceX);
                if (i == text.Length - 1)
                    stringWidth += overrun;
                rc.Overrun = overrun;

                float glyphTop = (float)face.Glyph.Metrics.HorizontalBearingY;
                float glyphBottom = (float)(face.Glyph.Metrics.Height - face.Glyph.Metrics.HorizontalBearingY);
                if (glyphTop > top)
                    top = glyphTop;
                if (glyphBottom > bottom)
                    bottom = glyphBottom;

                stringWidth += gAdvanceX;
                rc.RightEdge = stringWidth;
                measuredChars.Add(rc);

                if (face.HasKerning && i < text.Length - 1) 
                {
                    char cNext = text[i + 1];
                    kern = (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
                    if (kern > gAdvanceX * 5 || kern < -(gAdvanceX * 5))
                        kern = 0;
                    rc.Kern = kern;
                    stringWidth += kern;
                }
            }

            stringHeight = top + bottom;

            if (stringWidth == 0 || stringHeight == 0)
                return null;

            Bitmap bmp = new Bitmap((int)Math.Ceiling(stringWidth), (int)Math.Ceiling(stringHeight));
            trackingUnderrun = true;
            underrun = 0;
            overrun = 0;
            stringWidth = 0;
            using (var g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingMode = CompositingMode.SourceOver;

                g.Clear(backColor);

                for (int i = 0; i < text.Length; ++i)
                {
                    char c = text[i];

                    uint glyphIndex = face.GetCharIndex(c);
                    face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
                    face.Glyph.RenderGlyph(RenderMode.Normal);
                    FTBitmap ftbmp = face.Glyph.Bitmap;

                    float gAdvanceX = (float)face.Glyph.Advance.X;
                    float gBearingX = (float)face.Glyph.Metrics.HorizontalBearingX;
                    float gWidth = (float)face.Glyph.Metrics.Width;

                    var rc = new DebugChar(c, gAdvanceX, gBearingX, gWidth);

                    underrun += -(gBearingX);
                    if (penX == 0)
                        penX += underrun;
                    if (trackingUnderrun)
                        rc.Underrun = underrun;
                    if (trackingUnderrun && underrun <= 0)
                    {
                        underrun = 0;
                        trackingUnderrun = false;
                    }

                    if (ftbmp.Width > 0 && ftbmp.Rows > 0)
                    {
                        Bitmap cBmp = ftbmp.ToGdipBitmap(foreColor);
                        rc.Width = cBmp.Width;
                        rc.BearingX = face.Glyph.BitmapLeft;
                        int x = (int)Math.Round(penX + face.Glyph.BitmapLeft);
                        int y = (int)Math.Round(penY + top - (float)face.Glyph.Metrics.HorizontalBearingY);
                        g.DrawImageUnscaled(cBmp, x, y);
                        rc.Overrun = face.Glyph.BitmapLeft + cBmp.Width - gAdvanceX;
                        rightEdge = Math.Max(rightEdge, x + cBmp.Width);
                        spaceError = bmp.Width - rightEdge;
                    }
                    else
                    {
                        rightEdge = (int)(penX + gAdvanceX);
                        spaceError = bmp.Width - rightEdge;
                    }

                    if (gBearingX + gWidth > 0 || gAdvanceX > 0)
                    {
                        overrun -= Math.Max(gBearingX + gWidth, gAdvanceX);
                        if (overrun <= 0) overrun = 0;
                    }
                    overrun += (float)(gBearingX == 0 && gWidth == 0 ? 0 : gBearingX + gWidth - gAdvanceX);
                    if (i == text.Length - 1) penX += overrun;
                    rc.Overrun = overrun;

                    penX += (float)face.Glyph.Advance.X;
                    penY += (float)face.Glyph.Advance.Y;

                    rc.RightEdge = penX;
                    spaceError = bmp.Width - (int)Math.Round(rc.RightEdge);
                    renderedChars.Add(rc);

                    if (face.HasKerning && i < text.Length - 1)
                    {
                        char cNext = text[i + 1];
                        kern = (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
                        if (kern > gAdvanceX * 5 || kern < -(gAdvanceX * 5))
                            kern = 0;
                        rc.Kern = kern;
                        penX += (float)kern;
                    }

                }
            }

            return bmp;
        }

        #endregion

        private class DebugChar
        {
            public char Char { get; set; }
            public float AdvanceX { get; set; }
            public float BearingX { get; set; }
            public float Width { get; set; }
            public float Underrun { get; set; }
            public float Overrun { get; set; }
            public float Kern { get; set; }
            public float RightEdge { get; set; }
            internal DebugChar(char c, float advanceX, float bearingX, float width)
            {
                this.Char = c; this.AdvanceX = advanceX; this.BearingX = bearingX; this.Width = width;
            }

            public override string ToString()
            {
                return string.Format("'{0}' {1,5:F0} {2,5:F0} {3,5:F0} {4,5:F0} {5,5:F0} {6,5:F0} {7,5:F0}",
                    this.Char, this.AdvanceX, this.BearingX, this.Width, this.Underrun, this.Overrun,
                    this.Kern, this.RightEdge);
            }
            public static void PrintHeader()
            {
                Debug.Print("    {1,5} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5}",
                    "", "adv", "bearing", "wid", "undrn", "ovrrn", "kern", "redge");
            }
        }
    }
}
