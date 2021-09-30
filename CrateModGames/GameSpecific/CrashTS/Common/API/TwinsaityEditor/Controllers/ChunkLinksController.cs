using System;
using System.Collections.Generic;
using Twinsanity;

namespace TwinsaityEditor
{
    public class ChunkLinksController : ItemController
    {
        public new ChunkLinks Data { get; set; }

        public ChunkLinksController(MainForm topform, ChunkLinks item) : base(topform, item)
        {
            Data = item;
            AddMenu("Open editor", Menu_OpenEditor);
        }

        protected override string GetName()
        {
            return $"Chunk Links [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();
            text.Add($"ID: {Data.ID}");
            text.Add($"Size: {Data.Size}");
            text.Add($"LinkCount: {Data.Links.Count}");
            for (int i = 0; i < Data.Links.Count; ++i)
            {
                text.Add($"Link{i}");
                text.Add($"Type: {Data.Links[i].Type}");
                text.Add($"Directory: {Data.Links[i].Path}");
                text.Add($"Flags: {Convert.ToString(Data.Links[i].Flags, 16).ToUpper()}");
                ChunkLinks.ChunkLink.LinkTree tree = Data.Links[i].TreeRoot;
                string add = "";
                int depth = 0;
                while (tree != null)
                {
                    text.Add(add + $"Load Zone {depth}");
                    var gi_header = tree.GI_Type.Header;
                    for (var j = 0; j < 11; ++j)
                    {
                        text.Add(add + $"GI header {j}: {gi_header[j]}");
                    }
                    add += "     ";
                    depth++;
                    if (tree.Next != null)
                    {
                        tree = tree.Next;
                    }
                    else
                        break;
                }
                text.Add("");
            }

            TextPrev = text.ToArray();
        }

        private void Menu_OpenEditor()
        {
            MainFile.OpenEditor(this);
        }
    }
}