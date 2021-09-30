using Twinsanity;
using System.Collections.Generic;
using System;

namespace TwinsaityEditor
{
    public class InstaceTemplateDemoController : ItemController
    {
        public new InstanceTemplateDemo Data { get; set; }

        public InstaceTemplateDemoController(MainForm topform, InstanceTemplateDemo item) : base(topform, item)
        {
            Data = item;
        }

        protected override string GetName()
        {
            return string.Format("{0} [ID {1:X8}]", Data.Name, Data.ID);
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();

            string obj_name = MainFile.GetObjectName(Data.ObjectID);
            obj_name = Utils.TextUtils.TruncateObjectName(obj_name, Data.ObjectID, "", " (Not in Objects)");

            text.Add(string.Format("ID: {0:X8}", Data.ID));
            text.Add($"Size: {Data.Size}");
            text.Add($"Name: {Data.Name}");
            text.Add($"HeaderInts: {Data.HeaderInt1}; {Data.HeaderInt2}; {Data.HeaderInt3} ");
            if (Data.HeaderInt1 != 0)
            {
                text.Add($"UnkShort: {Data.UnkShort}");
            }
            text.Add($"Unknown bitfield: 0x{Data.Bitfield.ToString("X")}");
            text.Add($"HeaderFlags: {Data.UnkFlags[0]}; {Data.UnkFlags[1]};");
            text.Add($"Object ID {Data.ObjectID} - {(obj_name != string.Empty ? obj_name : string.Empty)}");
            text.Add(string.Format("Properties: {0:X8}", Data.Properties));
            text.Add($"Flags: {Data.Flags.Length}");
            if (Data.Flags.Length > 0)
            {
                for (int i = 0; i < Data.Flags.Length; i++)
                {
                    text.Add($"#{i}: {Data.Flags[i]}");
                }
            }

            text.Add($"Floats: {Data.Floats.Length}");
            if (Data.Floats.Length > 0)
            {
                for (int i = 0; i < Data.Floats.Length; i++)
                {
                    text.Add($"#{i}: {Data.Floats[i]}");
                }
            }

            text.Add($"Ints: {Data.Ints.Length}");
            if (Data.Ints.Length > 0)
            {
                for (int i = 0; i < Data.Ints.Length; i++)
                {
                    text.Add($"#{i}: {Data.Ints[i]}");
                }
            }

            TextPrev = text.ToArray();

        }
    }
}