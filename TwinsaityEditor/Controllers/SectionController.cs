using Twinsanity;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Linq;

namespace TwinsaityEditor
{
    public class SectionController : Controller
    {
        public TwinsSection Data { get; set; }
        public FileController MainFile { get; private set; }

        public SectionController(MainForm topform, TwinsSection item) : base (topform)
        {
            MainFile = TopForm.CurCont;
            Data = item;
            if (item.Type != SectionType.Texture && item.Type != SectionType.TextureX
                && item.Type != SectionType.Material && item.Type != SectionType.Model
                && item.Type != SectionType.ModelX && item.Type != SectionType.RigidModel
                && item.Type != SectionType.Skin && item.Type != SectionType.BlendSkin && item.Type != SectionType.SkinX
                && item.Type != SectionType.Mesh && item.Type != SectionType.LodModel
                && item.Type != SectionType.Skydome && !(item is TwinsFile))
            {
                
                if (item.Type == SectionType.ObjectInstance
                    || item.Type == SectionType.AIPosition
                    || item.Type == SectionType.AIPath
                    || item.Type == SectionType.Position
                    || item.Type == SectionType.Path
                    || item.Type == SectionType.Trigger
                    || item.Type == SectionType.Script
                    || item.Type == SectionType.ScriptDemo
                    || item.Type == SectionType.ScriptX
                    || item.Type == SectionType.Object)
                {
                    AddMenu("Open editor", Menu_OpenEditor);
                    AddMenu("Add new item", Menu_AddNew);
                    AddMenu("Re-ID by order", Menu_ReIDByOrder);
                }
                else if (item.Type == SectionType.Instance)
                {
                    AddMenu("Clear instance section", Menu_ClearInstanceSection);
                    AddMenu("Fill instance section", Menu_FillInstanceSection);
                }
                else if (item.Type >= SectionType.SE && item.Type <= SectionType.SE_Jpn)
                {
                    AddMenu("Extract extra data", Menu_ExtractExtraData);
                    AddMenu("Add new item", Menu_AddNew);
                }
                AddMenu("Re-order by ID (asc.)", Menu_ReOrderByID_Asc);
            }
            else if (item is TwinsFile f && f.Type == TwinsFile.FileType.RM2)
            {
                AddMenu("Add remaining instance sections", Menu_FileFillInstanceSections);
            }
            else
            {
                AddMenu("Add new item", Menu_AddNew);
                AddMenu("Re-order by ID (desc.)", Menu_ReOrderByID_Desc);
            }
            if (item.Type == SectionType.Model || item.Type == SectionType.ModelX || item.Type == SectionType.RigidModel || item.Type == SectionType.Mesh)
            {
                AddMenu("Export all meshes to PLY", Menu_ExportAllPLY);
            }
        }

        protected override string GetName()
        {
            return $"{Data.Type} Section [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            TextPrev = new string[Data.ExtraData == null ? 3 : 4];
            TextPrev[0] = $"ID: {Data.ID}";
            TextPrev[1] = $"Size: {Data.Size}";
            TextPrev[2] = $"ContentSize: {Data.ContentSize} Element Count: {Data.Records.Count}";
            if (Data.ExtraData != null)
                TextPrev[3] = $"ExtraDataSize: {Data.ExtraData.Length}";
        }

        public void AddItem(uint id, TwinsItem item)
        {
            Data.AddItem(id, item);
            TopForm.GenTreeNode(item, this);
            UpdateText();
            ((Controller)Node.Nodes[Data.RecordIDs[item.ID]].Tag).UpdateText();
        }

        public void RemoveItem(TwinsItem item)
        {
            RemoveItem(item.ID);
        }

        public void RemoveItem(uint id)
        {
            Node.Nodes[Data.RecordIDs[id]].Remove();
            Data.RemoveItem(id);
            UpdateText();
        }

        public void ChangeID(uint old_id, uint new_id)
        {
            if (Data.ContainsItem(new_id))
                throw new System.ArgumentException("New ID already exists.");
            var index = Data.RecordIDs[old_id];
            Data.GetItem<TwinsItem>(old_id).ID = new_id;
            Data.RecordIDs.Remove(old_id);
            Data.RecordIDs.Add(new_id, index);
        }

        private Controller GetItem(uint id)
        {
            return (Controller)Node.Nodes[Data.RecordIDs[id]].Tag;
        }

        public T GetItem<T>(uint id) where T : Controller
        {
            return Node.Nodes[Data.RecordIDs[id]].Tag as T;
        }


        private void Menu_AddNew()
        {
            TwinsItem newItem = null;
            switch (Data.Type)
            {
                case SectionType.Texture:
                    newItem = new Texture();
                    break;
                case SectionType.Material:
                    newItem = new Material();
                    break;
                case SectionType.Model:
                    newItem = new Model();
                    break;
                case SectionType.RigidModel:
                    newItem = new RigidModel();
                    break;
                case SectionType.Skin:
                    newItem = new Skin();
                    break;
                case SectionType.Object:
                    newItem = new GameObject();
                    break;
                case SectionType.ScriptDemo:
                case SectionType.ScriptX:
                case SectionType.Script:
                    newItem = new Script();
                    break;
                case SectionType.SE:
                case SectionType.SE_Eng:
                case SectionType.SE_Fre:
                case SectionType.SE_Ger:
                case SectionType.SE_Ita:
                case SectionType.SE_Spa:
                case SectionType.SE_Jpn:
                    newItem = new SoundEffect();
                    ((SoundEffect)newItem).SoundOffset = (uint)Data.ExtraData.Length;
                    break;
                default:
                    newItem = new TwinsItem();
                    break;
            }

            if (newItem == null)
            {
                throw new System.Exception("Unsupported");
            }
            else
            {
                uint newId = Data.RecordIDs.Keys.Max() + 1;
                newItem.ID = newId;
                newItem.Parent = Data;
                AddItem(newId, newItem);
            }
            
        }

        private void Menu_ExtractExtraData()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = GetName().Replace(":", string.Empty);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(file);
                writer.Write(Data.ExtraData);
                writer.Close();
            }
        }

        private void Menu_ClearInstanceSection()
        {
            for (uint i = 0; i <= 8; ++i)
            {
                if (Data.ContainsItem(i))
                {
                    RemoveItem(i);
                }
            }
        }

        private void Menu_FillInstanceSection()
        {
            for (uint i = 0; i <= 8; ++i)
            {
                SectionType type = SectionType.Null;
                switch (i)
                {
                    case 0: type = SectionType.InstanceTemplate; break;
                    case 1: type = SectionType.AIPosition; break;
                    case 2: type = SectionType.AIPath; break;
                    case 3: type = SectionType.Position; break;
                    case 4: type = SectionType.Path; break;
                    case 5: type = SectionType.CollisionSurface; break;
                    case 6: type = SectionType.ObjectInstance; break;
                    case 7: type = SectionType.Trigger; break;
                    case 8: type = SectionType.Camera; break;
                }
                if (!Data.ContainsItem(i))
                {
                    TwinsSection sec = new TwinsSection
                    {
                        ID = i,
                        Level = Data.Level + 1,
                        Type = type,
                        Parent = Data
                    };
                    AddItem(i, sec);
                }
            }
        }

        private void Menu_FileFillInstanceSections()
        {
            for (uint i = 0; i <= 7; ++i)
            {
                if (!Data.ContainsItem(i))
                {
                    TwinsSection sec = new TwinsSection
                    {
                        ID = i,
                        Level = Data.Level + 1,
                        Type = SectionType.Instance,
                        Parent = Data
                    };
                    AddItem(i, sec);
                }
            }
        }

        private void Menu_ReOrderByID_Asc()
        {
            if (Data.Type == SectionType.ObjectInstance)
            {
                MainFile.CloseEditor(Editors.Instance, (int)Data.Parent.ID);
            }
            else if (Data.Type == SectionType.Position)
            {
                MainFile.CloseEditor(Editors.Position, (int)Data.Parent.ID);
            }
            Node.TreeView.BeginUpdate();
            Node.Nodes.Clear();
            SortedDictionary<uint, int> sdic = new SortedDictionary<uint, int>(Data.RecordIDs);
            List<TwinsItem> slist = new List<TwinsItem>();
            foreach (var i in sdic)
            {
                slist.Add(Data.Records[i.Value]);
                TopForm.GenTreeNode(Data.Records[i.Value], this);
            }
            Data.Records = slist;
            Node.TreeView.EndUpdate();
        }

        private void Menu_ReOrderByID_Desc()
        {
            Node.TreeView.BeginUpdate();
            Node.Nodes.Clear();
            SortedDictionary<uint, int> sdic = new SortedDictionary<uint, int>(new Utils.DescendingComparer<uint>());
            foreach (var i in Data.RecordIDs)
                sdic.Add(i.Key, i.Value);
            List<TwinsItem> slist = new List<TwinsItem>();
            foreach (var i in sdic)
            {
                slist.Add(Data.Records[i.Value]);
                TopForm.GenTreeNode(Data.Records[i.Value], this);
            }
            Data.Records = slist;
            Node.TreeView.EndUpdate();
        }

        private void Menu_ReIDByOrder()
        {
            if (Data.Type == SectionType.ObjectInstance)
            {
                MainFile.CloseEditor(Editors.Instance, (int)Data.Parent.ID);
            }
            else if (Data.Type == SectionType.Position)
            {
                MainFile.CloseEditor(Editors.Position, (int)Data.Parent.ID);
            }
            Node.TreeView.BeginUpdate();
            Node.Nodes.Clear();
            Data.RecordIDs.Clear();
            for (int i = 0; i < Data.Records.Count; ++i)
            {
                Data.Records[i].ID = (uint)i;
                Data.RecordIDs.Add((uint)i, i);
                TopForm.GenTreeNode(Data.Records[i], this);
            }
            Node.TreeView.EndUpdate();
        }

        private void Menu_OpenEditor()
        {
            MainFile.OpenEditor(this);
        }

        private void Menu_ExportAllPLY()
        {
            if (Data.Type == SectionType.RigidModel || Data.Type == SectionType.Mesh)
                if (MessageBox.Show("PLY export is experimental, material and texture information will not be exported. Continue anyway?", "Export Warning", MessageBoxButtons.YesNo) == DialogResult.No) return;
            var fdbSave = new CommonOpenFileDialog { IsFolderPicker = true };
            if (fdbSave.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (TreeNode n in Node.Nodes)
                {
                    string fname = fdbSave.FileName + @"\{n.Text}.ply";
                    if (n.Tag is ModelController c)
                    {
                        File.WriteAllBytes(fname, c.Data.ToPLY());
                    }
                    else if (n.Tag is RigidModelController d)
                    {
                        File.WriteAllBytes(fname, Data.Parent.GetItem<TwinsSection>(2).GetItem<Model>(d.Data.MeshID).ToPLY());
                    }
                }
            }
        }
    }
}
