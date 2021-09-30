using System;
using System.Collections.Generic;
using Twinsanity;
using System.Text;

namespace TwinsaityEditor
{
    public class DynamicSceneryDataMBController : ItemController
    {
        public new DynamicSceneryDataMB Data { get; set; }

        public DynamicSceneryDataMBController(MainForm topform, DynamicSceneryDataMB item) : base(topform, item)
        {
            Data = item;
        }

        protected override string GetName()
        {
            return $"Dynamic Scenery Data [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();

            text.Add($"ID: {Data.ID}");
            text.Add($"Size: {Data.Size}");
            text.Add($"Model Count: {Data.Models.Count}");

            TextPrev = text.ToArray();
        }
    }
}