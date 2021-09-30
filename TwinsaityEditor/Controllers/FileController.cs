using Twinsanity;
using System.Collections.Generic;
using System.Windows.Forms;
using TwinsaityEditor.Controllers;

namespace TwinsaityEditor
{
    public class FileController : SectionController
    {
        public new TwinsFile Data { get; set; }
        public TwinsFile DataAux { get; set; }
        public TwinsFile DataDefault { get; set; }
        public FileController DefaultCont { get; set; }

        public string FileName { get => Data.FileName; }
        public string SafeFileName { get => Data.SafeFileName; }

        public SectionController MeshSection { get => GetMeshSection(); }
        public Dictionary<uint, string> ObjectNames { get; set; }
        public Dictionary<uint, string> MaterialNames { get; set; }

        public TwinsItem SelectedItem { get; set; } = null;
        public int SelectedItemArg { get; set; } = -1;

        //Editors
        private Form editChunkLinks;
        private Form editScripts;
        private Form editObjects;
        private Form editAnimations;
        private readonly Form[] editInstances = new Form[9], editPositions = new Form[9], editPaths = new Form[9], editTriggers = new Form[9];

        //Viewers
        private Form colForm;
        private SkydomeViewer skyViewer;
        private RMViewer rmViewer;
        private SMViewer smViewer;
        private TextureViewer texViewer;
        private Dictionary<uint, Form> MeshViewers { get; set; }
        private Dictionary<uint, Form> ModelViewers { get; set; }

        public FileController(MainForm topform, TwinsFile item) : base(topform, item)
        {
            Data = item;
            DataAux = null;
            DataDefault = null;
            ObjectNames = new Dictionary<uint, string>();
            MaterialNames = new Dictionary<uint, string>();
            MeshViewers = new Dictionary<uint, Form>();
            ModelViewers = new Dictionary<uint, Form>();
            LoadFileInfo();
        }

        protected override string GetName()
        {
            if (Data.Type == TwinsFile.FileType.MonkeyBallRM || Data.Type == TwinsFile.FileType.MonkeyBallSM)
            {
                if (Data.Console == TwinsFile.ConsoleType.PS2)
                {
                    return "Compressed File";
                }
            }
            return "File";
        }

        protected override void GenText()
        {
            TextPrev = new string[2];
            TextPrev[0] = $"Size: {Data.Size}";
            TextPrev[1] = $"ContentSize: {Data.ContentSize} Element Count: {Data.Records.Count}";
        }

        private void LoadFileInfo()
        {
            if ((Data.Type == TwinsFile.FileType.RM2 || Data.Type == TwinsFile.FileType.RMX || Data.Type == TwinsFile.FileType.DemoRM2) && Data.ContainsItem(10) && Data.GetItem<TwinsSection>(10).ContainsItem(0))
            {
                if (Data.Type == TwinsFile.FileType.DemoRM2)
                {
                    foreach (GameObjectDemo obj in Data.GetItem<TwinsSection>(10).GetItem<TwinsSection>(0).Records)
                    {
                        ObjectNames.Add(obj.ID, obj.Name);
                    }
                }
                else
                {
                    foreach (GameObject obj in Data.GetItem<TwinsSection>(10).GetItem<TwinsSection>(0).Records)
                    {
                        ObjectNames.Add(obj.ID, obj.Name);
                    }
                }
            }
            uint gfx_id = 11;
            if (Data.Type == TwinsFile.FileType.SM2 || Data.Type == TwinsFile.FileType.SMX || Data.Type == TwinsFile.FileType.DemoSM2)
            {
                gfx_id = 6;
            }
            if (Data.Type == TwinsFile.FileType.MonkeyBallRM || Data.Type == TwinsFile.FileType.MonkeyBallSM)
            {
                gfx_id = 99;
            }
            if (Data.ContainsItem(gfx_id) && Data.GetItem<TwinsSection>(gfx_id).ContainsItem(1))
            {
                if (Data.Type == TwinsFile.FileType.DemoRM2 || Data.Type == TwinsFile.FileType.DemoSM2)
                {
                    foreach (MaterialDemo mat in Data.GetItem<TwinsSection>(gfx_id).GetItem<TwinsSection>(1).Records)
                    {
                        MaterialNames.Add(mat.ID, mat.Name);
                    }
                }
                else
                {
                    foreach (Material mat in Data.GetItem<TwinsSection>(gfx_id).GetItem<TwinsSection>(1).Records)
                    {
                        MaterialNames.Add(mat.ID, mat.Name);
                    }
                }
                
            }
        }

        private SectionController GetMeshSection()
        {
            uint gfx_id = 11;
            if (Data.Type == TwinsFile.FileType.SM2 || Data.Type == TwinsFile.FileType.SMX || Data.Type == TwinsFile.FileType.DemoSM2)
            {
                gfx_id = 6;
            }
            if (Data.ContainsItem(gfx_id) && Data.GetItem<TwinsSection>(gfx_id).ContainsItem(2))
            {
                return GetItem<SectionController>(gfx_id).GetItem<SectionController>(2);
            }
            else return null;
        }

        public void CloseFile()
        {
            CloseRMViewer();
            CloseSMViewer();
            CloseSkydomeViewer();
            CloseAllMeshViewers();
            CloseAllModelViewers();
            CloseTextureViewer();
            CloseEditor(Editors.ChunkLinks);
            CloseEditor(Editors.ColData);
            CloseEditor(Editors.Script);
            CloseEditor(Editors.Animation);
            for (int i = 0; i <= 7; ++i)
            {
                CloseEditor(Editors.Instance, i);
                CloseEditor(Editors.Position, i);
                CloseEditor(Editors.Path, i);
                CloseEditor(Editors.Trigger, i);
            }
            Data = DataAux = DataDefault = null;
        }

        public void OpenEditor(Controller c)
        {
            if (c is ChunkLinksController)
                OpenEditor(ref editChunkLinks, Editors.ChunkLinks, c);
            else if (c is ColDataController)
                OpenEditor(ref colForm, Editors.ColData, c);
            else if (c is PositionController)
                OpenEditor(ref editPositions[((PositionController)c).Data.Parent.Parent.ID], Editors.Position, (Controller)c.Node.Parent.Tag);
            else if (c is PathController)
                OpenEditor(ref editPaths[((PathController)c).Data.Parent.Parent.ID], Editors.Path, (Controller)c.Node.Parent.Tag);
            else if (c is InstanceController)
                OpenEditor(ref editInstances[((InstanceController)c).Data.Parent.Parent.ID], Editors.Instance, (Controller)c.Node.Parent.Tag);
            else if (c is InstanceMBController)
                OpenEditor(ref editInstances[((InstanceMBController)c).Data.Parent.Parent.ID], Editors.InstanceMB, (Controller)c.Node.Parent.Tag);
            else if (c is TriggerController)
                OpenEditor(ref editTriggers[((TriggerController)c).Data.Parent.Parent.ID], Editors.Trigger, (Controller)c.Node.Parent.Tag);
            else if (c is ScriptController)
                OpenEditor(ref editScripts, Editors.Script, (Controller)c.Node.Parent.Tag);
            else if (c is AnimationController)
                OpenEditor(ref editAnimations, Editors.Animation, (Controller)c.Node.Parent.Tag);
            else if (c is ObjectController)
                OpenEditor(ref editObjects, Editors.Object, (Controller)c.Node.Parent.Tag);
            else if (c is SectionController s)
            {
                if (s.Data.Type == SectionType.ObjectInstance)
                    OpenEditor(ref editInstances[s.Data.Parent.ID], Editors.Instance, c);
                else if (s.Data.Type == SectionType.ObjectInstanceMB)
                    OpenEditor(ref editInstances[s.Data.Parent.ID], Editors.InstanceMB, c);
                else if (s.Data.Type == SectionType.Position)
                    OpenEditor(ref editPositions[s.Data.Parent.ID], Editors.Position, c);
                else if (s.Data.Type == SectionType.Path)
                    OpenEditor(ref editPaths[s.Data.Parent.ID], Editors.Path, c);
                else if (s.Data.Type == SectionType.Trigger)
                    OpenEditor(ref editTriggers[s.Data.Parent.ID], Editors.Trigger, c);
                else if (s.Data.Type == SectionType.Script || s.Data.Type == SectionType.ScriptDemo || s.Data.Type == SectionType.ScriptX)
                    OpenEditor(ref editScripts, Editors.Script, c);
                else if (s.Data.Type == SectionType.Object)
                    OpenEditor(ref editObjects, Editors.Object, c);
                else if (s.Data.Type == SectionType.Animation)
                    OpenEditor(ref editAnimations, Editors.Animation, c);
            }
        }

        private void OpenEditor(ref Form editor_var_ptr, Editors editor, Controller cont)
        {
            if (editor_var_ptr == null || editor_var_ptr.IsDisposed)
            {
                switch (editor)
                {
                    case Editors.ColData:
                        {
                            if (Data.ContainsItem(9)) editor_var_ptr = new ColDataEditor(Data.GetItem<ColData>(9)) { Tag = TopForm };
                            else return;
                        }
                        break;
                    case Editors.ChunkLinks: editor_var_ptr = new ChunkLinksEditor((ChunkLinksController)cont) { Tag = TopForm }; break;
                    case Editors.Position: editor_var_ptr = new PositionEditor((SectionController)cont) { Tag = TopForm }; break;
                    case Editors.Path: editor_var_ptr = new PathEditor((SectionController)cont) { Tag = TopForm }; break;
                    case Editors.Instance: editor_var_ptr = new InstanceEditor((SectionController)cont) { Tag = TopForm }; break;
                    case Editors.InstanceMB: editor_var_ptr = new InstanceMBEditor((SectionController)cont) { Tag = TopForm }; break;
                    case Editors.Trigger: editor_var_ptr = new TriggerEditor((SectionController)cont) { Tag = TopForm }; break;
                    case Editors.Script: editor_var_ptr = new ScriptEditor((SectionController)cont) { Tag = TopForm }; break;
                    case Editors.Object: editor_var_ptr = new ObjectEditor((SectionController)cont) { Tag = TopForm }; break;
                    case Editors.Animation: editor_var_ptr = new AnimationEditor((SectionController)cont) { Tag = TopForm }; break;
                }
                editor_var_ptr.Show();
            }
            else
                editor_var_ptr.Select();
        }

        public void CloseEditor(Editors editor, int arg = -1)
        {
            Form editorForm = null;
            switch (editor)
            {
                case Editors.ColData: editorForm = colForm; break;
                case Editors.ChunkLinks: editorForm = editChunkLinks; break;
                case Editors.Instance: editorForm = editInstances[arg]; break; //since arg is -1 by default, an exception will be thrown unless it is specified
                case Editors.Position: editorForm = editPositions[arg]; break;
                case Editors.Path: editorForm = editPaths[arg]; break;
                case Editors.Trigger: editorForm = editTriggers[arg]; break;
                case Editors.Script: editorForm = editScripts; break;
                case Editors.Animation: editorForm = editAnimations; break;
            }
            CloseForm(editorForm);
        }

        public void OpenMeshViewer(ModelController c)
        {
            var id = c.Data.ID;
            if (!MeshViewers.ContainsKey(id))
            {
                var f = new Form { Size = new System.Drawing.Size(480, 480), Text = "Initializing viewer..." };
                f.FormClosed += delegate
                {
                    MeshViewers.Remove(id);
                };
                f.Show();
                MeshViewer v = new MeshViewer(c, f) { Dock = DockStyle.Fill };
                f.Controls.Add(v);
                f.Text = "MeshViewer";
                MeshViewers.Add(id, f);
            }
            else
                MeshViewers[id].Select();
        }

        public void OpenTextureViewer(TextureController c)
        {
            if (texViewer == null || texViewer.IsDisposed)
            {
                texViewer = new TextureViewer();
                texViewer.SelectedTexture = c.Data;
                List<TwinsItem> textures = null;
                if (c.MainFile.FileName.EndsWith(".sm2"))
                {
                    textures = Data.GetItem<TwinsSection>(6).GetItem<TwinsSection>(0).Records;
                }
                else
                {
                    textures = Data.GetItem<TwinsSection>(11).GetItem<TwinsSection>(0).Records;
                }
                for (var i = 0; i < textures.Count; ++i)
                {
                    texViewer.Textures.Add((Texture)textures[i]);
                    if ((Texture)textures[i] == c.Data)
                    {
                        texViewer.TextureIndex = i;
                    }
                }
                texViewer.UpdateTextureLabel();
                texViewer.FormClosed += delegate
                {
                    texViewer = null;
                };
                texViewer.Show();
            }
            else
            {
                texViewer.Select();
            }
        }

        public void CloseMeshViewer(uint mesh_id)
        {
            var f = MeshViewers[mesh_id];
            CloseForm(f);
            MeshViewers.Remove(mesh_id);
        }

        public void CloseAllMeshViewers()
        {
            var a = new uint[MeshViewers.Count];
            MeshViewers.Keys.CopyTo(a, 0);
            foreach (var p in a)
            {
                CloseMeshViewer(p);
            }
        }

        public void OpenModelViewer(RigidModelController c)
        {
            var id = c.Data.ID;
            if (!ModelViewers.ContainsKey(id))
            {
                var f = new Form { Size = new System.Drawing.Size(480, 480), Text = "Initializing viewer..." };
                f.FormClosed += delegate
                {
                    ModelViewers.Remove(id);
                };
                f.Show();
                ModelViewer v = new ModelViewer(c, f) { Dock = DockStyle.Fill };
                f.Controls.Add(v);
                f.Text = "ModelViewer";
                ModelViewers.Add(id, f);
            }
            else
                ModelViewers[id].Select();
        }
        public void OpenModelViewer(LodModelController spec)
        {
            SectionController model_sec = GetItem<SectionController>(6).GetItem<SectionController>(6);
            uint LODcount = spec.Data.ModelsAmount;
            int targetLOD = LODcount == 1 ? 0 : 1;
            RigidModelController c = model_sec.GetItem<RigidModelController>(spec.Data.LODModelIDs[targetLOD]);
            var id = c.Data.ID;
            if (!ModelViewers.ContainsKey(id))
            {
                var f = new Form { Size = new System.Drawing.Size(480, 480), Text = "Initializing viewer..." };
                f.FormClosed += delegate
                {
                    ModelViewers.Remove(id);
                };
                f.Show();
                ModelViewer v = new ModelViewer(c, f) { Dock = DockStyle.Fill };
                f.Controls.Add(v);
                f.Text = "ModelViewer";
                ModelViewers.Add(id, f);
            }
            else
                ModelViewers[id].Select();
        }

        public void CloseModelViewer(uint id)
        {
            var f = ModelViewers[id];
            CloseForm(f);
            ModelViewers.Remove(id);
        }

        public void OpenSkydomeViewer(SkydomeController c)
        {
            var id = c.Data.ID;
            if (skyViewer == null || skyViewer.Sky.Data.ID != c.Data.ID)
            {
                Form f = new Form { Size = new System.Drawing.Size(480, 480), Text = "Initializing viewer..." };
                f.FormClosed += delegate
                {
                    skyViewer = null;
                };
                f.Show();
                skyViewer = new SkydomeViewer(c, f) { Dock = DockStyle.Fill };
                f.Controls.Add(skyViewer);
                f.Text = "SkydomeViewer";
            }
            else
                skyViewer.ParentForm.Select();
        }

        public void CloseSkydomeViewer()
        {
            if (skyViewer == null) return;
            var f = skyViewer.ParentForm;
            CloseForm(f);
        }

        public void CloseAllModelViewers()
        {
            var a = new uint[ModelViewers.Count];
            ModelViewers.Keys.CopyTo(a, 0);
            foreach (var p in a)
            {
                CloseModelViewer(p);
            }
        }

        public void OpenRMViewer()
        {
            if (rmViewer == null)
            {
                Form f = new Form { Size = new System.Drawing.Size(480, 480), Text = "Initializing viewer..." };
                f.FormClosed += delegate
                {
                    rmViewer = null;
                };
                f.Show();
                rmViewer = new RMViewer(this, f) { Dock = DockStyle.Fill };
                f.Controls.Add(rmViewer);
                f.Text = "RMViewer";
            }
            else
                rmViewer.ParentForm.Select();
        }

        public void OpenSMViewer()
        {
            if (smViewer == null)
            {
                Form f = new Form { Size = new System.Drawing.Size(480, 480), Text = "Initializing viewer..." };
                f.FormClosed += delegate
                {
                    smViewer = null;
                };
                f.Show();
                smViewer = new SMViewer(this, f) { Dock = DockStyle.Fill };
                f.Controls.Add(smViewer);
                f.Text = "SMViewer";
            }
            else
                smViewer.ParentForm.Select();
        }

        public void RMViewer_LoadInstances()
        {
            if (rmViewer != null)
                rmViewer.LoadInstances();
        }

        public void RMViewer_LoadPositions()
        {
            if (rmViewer != null)
                rmViewer.LoadPositions();
        }

        public Pos RMViewer_GetPos(Pos pos_in)
        {
            Pos pos = new Pos(pos_in.X, pos_in.Y, pos_in.Z, 1);
            if (rmViewer != null)
            {
                OpenTK.Vector3 vec = rmViewer.GetViewerPos();
                pos.X = vec.X;
                pos.Y = vec.Y;
                pos.Z = vec.Z;
            }
            return pos;
        }

        public void CloseForm(Form form)
        {
            if (form != null && !form.IsDisposed)
            {
                form.Close();
                form = null;
            }
        }

        public void CloseRMViewer()
        {
            if (rmViewer == null) return;
            var f = rmViewer.ParentForm;
            CloseForm(f);
        }

        public void CloseSMViewer()
        {
            if (smViewer == null) return;
            var f = smViewer.ParentForm;
            CloseForm(f);
        }

        public void CloseTextureViewer()
        {
            if (texViewer == null) return;
            var f = texViewer.ParentForm;
            CloseForm(f);
        }

        public void SelectItem(TwinsItem item, int arg = -1)
        {
            var prev_item = SelectedItem;
            SelectedItem = item;
            SelectedItemArg = arg;
            if (rmViewer != null)
            {
                if (item == null && prev_item != null)
                {
                    if (prev_item is Instance)
                        rmViewer.LoadInstances();
                    else if (prev_item is InstanceMB)
                        rmViewer.LoadInstances();
                    else if (prev_item is Position)
                        rmViewer.LoadPositions();
                }
                else
                    rmViewer.UpdateSelected();
            }
        }

        public string GetMaterialName(uint id)
        {
            if (MaterialNames.ContainsKey(id))
                return MaterialNames[id].Replace('\0', ' ');
            else return string.Empty;
        }

        public string GetObjectName(uint id)
        {
            if (ObjectNames.ContainsKey(id))
                return ObjectNames[id];
            else return string.Empty;
        }

        public string GetScriptName(uint id)
        {
            try { return Data.GetItem<TwinsSection>(10).GetItem<TwinsSection>(1).GetItem<Script>(id).Name; }
            catch { return string.Empty; }
        }

        public ushort? GetInstanceID(uint sector, uint id)
        {
            if (Data.ContainsItem(sector) && Data.GetItem<TwinsSection>(sector).ContainsItem(6))
            {
                if (id < Data.GetItem<TwinsSection>(sector).GetItem<TwinsSection>(6).Records.Count)
                {
                    if (Data.GetItem<TwinsSection>(sector).GetItem<TwinsSection>(6).Records[(int)id] is Instance inst)
                    {
                        return inst.ObjectID;
                    }
                    else if (Data.GetItem<TwinsSection>(sector).GetItem<TwinsSection>(6).Records[(int)id] is InstanceDemo instdemo)
                    {
                        return instdemo.ObjectID;
                    }
                    return null;
                }
                else
                    return null;
            }
            else throw new System.ArgumentException("The requested section does not have an object instance section.");
        }
        public Pos GetInstancePos(uint sector, uint id)
        {
            if (Data.ContainsItem(sector) && Data.GetItem<TwinsSection>(sector).ContainsItem(6))
            {
                if (id < Data.GetItem<TwinsSection>(sector).GetItem<TwinsSection>(6).Records.Count)
                {
                    if (Data.GetItem<TwinsSection>(sector).GetItem<TwinsSection>(6).Records[(int)id] is Instance inst)
                    {
                        return new Pos(inst.Pos.X, inst.Pos.Y, inst.Pos.Z, inst.Pos.W);
                    }
                    else if (Data.GetItem<TwinsSection>(sector).GetItem<TwinsSection>(6).Records[(int)id] is InstanceDemo instdemo)
                    {
                        return new Pos(instdemo.Pos.X, instdemo.Pos.Y, instdemo.Pos.Z, instdemo.Pos.W);
                    }
                    return null;
                }
                else
                    return null;
            }
            else throw new System.ArgumentException("The requested section does not have an object instance section.");
        }

        public AIPosition GetAIPos(uint sector, uint id)
        {
            if (Data.ContainsItem(sector) && Data.GetItem<TwinsSection>(sector).ContainsItem(1))
            {
                //int i = 0;
                //foreach (Instance j in ((TwinsSection)((TwinsSection)Data.GetItem(sector)).GetItem(6)).Records)
                //{
                //    if (i++ == id)
                //        return j;
                //}
                //throw new System.ArgumentException("The requested section does not have an instance in the specified position.");
                if (id < Data.GetItem<TwinsSection>(sector).GetItem<TwinsSection>(1).Records.Count)
                    return (AIPosition)Data.GetItem<TwinsSection>(sector).GetItem<TwinsSection>(1).Records[(int)id];
                else
                    return null;
            }
            else throw new System.ArgumentException("The requested section does not have an object instance section.");
        }
    }
}
