using Twinsanity;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TwinsaityEditor
{
    public class RigidModelController : ItemController
    {
        public new RigidModel Data { get; set; }

        public RigidModelController(MainForm topform, RigidModel item) : base (topform, item)
        {
            Data = item;
            AddMenu("Export mesh to PLY", Menu_ExportPLY);
            AddMenu("Open model viewer", Menu_OpenViewer);
        }

        protected override string GetName()
        {
            return string.Format("Rigid Model [ID {0:X8}/{0}]", Data.ID);
        }

        protected override void GenText()
        {
            var text = new List<string>();
            text.Add(string.Format("ID: {0:X8}", Data.ID));
            text.Add($"Size: {Data.Size}");
            text.Add($"Header: {Data.Header} MaterialCount: {Data.MaterialIDs.Length}");
            for (int i = 0; i < Data.MaterialIDs.Length; ++i)
                text.Add(MainFile.GetMaterialName(Data.MaterialIDs[i]));
            text.Add(string.Format("Model: {0:X8}", Data.MeshID));
            TextPrev = text.ToArray();
        }

        private void Menu_ExportPLY()
        {
            if (MessageBox.Show("PLY export is experimental, material and texture information will not be exported. Continue anyway?", "Export Warning", MessageBoxButtons.YesNo) == DialogResult.No) return;
            SaveFileDialog sfd = new SaveFileDialog { Filter = "PLY files (*.ply)|*.ply", FileName = GetName() };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, Data.Parent.Parent.GetItem<TwinsSection>(2).GetItem<Model>(Data.MeshID).ToPLY());
            }
        }

        private void Menu_OpenViewer()
        {
            MainFile.OpenModelViewer(this);
        }
    }
}
