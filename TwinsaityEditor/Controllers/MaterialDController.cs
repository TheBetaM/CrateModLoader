using Twinsanity;
using System;
using System.Collections.Generic;

namespace TwinsaityEditor
{
    public class MaterialDController : ItemController
    {
        public new MaterialDemo Data { get; set; }

        public MaterialDController(MainForm topform, MaterialDemo item) : base(topform, item)
        {
            Data = item;
        }

        protected override string GetName()
        {
            return string.Format("{1} [ID {0:X8}]", Data.ID, Data.Name.Substring(0, Data.Name.Length - 1));
        }

        protected override void GenText()
        {
            var text = new List<string>
            {
                string.Format("ID: {0:X8}", Data.ID),
                $"Size: {Data.Size}",
                $"Name: {Data.Name.Substring(0, Data.Name.Length - 1)}",
                $"Shaders: {Data.Shaders.Count}"
            };
            foreach (var shd in Data.Shaders)
            {
                text.Add($"Shader Type {shd.ShaderType}");
                text.Add($"\tBound texture: {shd.TextureId:X}");
                text.Add($"\tAlpha blending: {shd.ABlending}");
                text.Add($"\tAlpha test: {shd.ATest}");
                text.Add($"\tAlpha test method: {shd.ATestMethod}");
                text.Add($"\tAlpha value to be compared to: {shd.AlphaValueToBeComparedTo}");
                text.Add($"\tProcess method when alpha test failed: {shd.ProcessMethodWhenAlphaTestFailed}");
                text.Add($"\tDestination alpha test: {shd.DAlphaTest}");
                text.Add($"\tDestination alpha test mode: {shd.DAlphaTestMode}");
                text.Add($"\tDepth test method: {shd.DepthTest}");
                text.Add($"\tShading method: {shd.ShdMethod}");
                text.Add($"\tTexture mapping: {shd.TxtMapping}");
                text.Add($"\tMethod of specifying texture coordinates: {shd.MethodOfSpecifyingTextureCoordinates}");
                text.Add($"\tFogging: {shd.Fog}");
                text.Add($"\tContext: {shd.ContextNum}");
                text.Add($"\tUse alpha reg preset settings: {shd.UsePresetAlphaRegSettings}");
                text.Add($"\tSpec of input color value A: {shd.SpecOfColA}");
                text.Add($"\tSpec of input color value B: {shd.SpecOfColB}");
                text.Add($"\tSpec of input alpha value C: {shd.SpecOfAlphaC}");
                text.Add($"\tSpec of input color value D: {shd.SpecOfColD}");
                text.Add($"\tFixed alpha value (FIX): {shd.FixedAlphaValue}");
                text.Add($"\tTexture filtering: {shd.TextureFilterWhenTextureIsExpanded}");
                text.Add($"\tZ-Value drawing mask: {shd.ZValueDrawingMask}");
            }
            TextPrev = text.ToArray();
        }
    }
}
