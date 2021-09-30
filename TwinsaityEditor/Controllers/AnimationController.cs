using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity;

namespace TwinsaityEditor.Controllers
{
    public class AnimationController : ItemController
    {
        public new Animation Data { get; set; }
        
        public AnimationController(MainForm topform, Animation item) : base(topform, item)
        {
            Data = item;
            AddMenu("Open editor", Menu_OpenEditor);
        }

        protected override string GetName()
        {
            return $"Animation [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            List<string> text = new List<string>
            {
                $"ID: {Data.ID}",
                $"Size: {Data.Size}",
                $"Unknown bitfield: 0x{Data.Bitfield:X}",
                $"Blob packed 1: 0x{Data.UnkBlobSizePacked1:X}",
                $"Bone settings 1: {Data.BonesSettings.Count}",
                $"Transformations 1: {Data.Transformations.Count}",
                $"Timelines 1: {Data.Timelines.Count}",
                $"Blob packed 2: 0x{Data.UnkBlobSizePacked2:X}",
                $"Bone settings 2: {Data.BonesSettings2.Count}",
                $"Transformations 2: {Data.Transformations2.Count}",
                $"Timelines 2: {Data.Timelines2.Count}",
            };
            TextPrev = text.ToArray();
        }

        private void Menu_OpenEditor()
        {
            MainFile.OpenEditor(this);
        }
    }
}
