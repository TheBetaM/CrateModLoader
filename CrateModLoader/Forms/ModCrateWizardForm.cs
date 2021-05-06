using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using CrateModAPI.Resources.Text;

namespace CrateModLoader.Forms
{
    public partial class ModCrateWizardForm : Form
    {
        public ModLoader ModProgram;
        private ModCrate crate;
        private uint LayerEditing = 0;
        private bool BaseLayer => (LayerEditing == 0);
        private bool ExportingFile = false;
        private List<TreeView> TreeViews;
        private List<int> LayerIDs;

        public ModCrateWizardForm(ModLoader program, ModCrate editCrate = null)
        {
            ModProgram = program;
            InitializeComponent();
            if (editCrate != null)
            {
                crate = editCrate;
            }
            else
            {
                crate = new ModCrate();
            }

            if (ModProgram.Game == null)
            {
                button_ModMenu.Enabled = false;
            }
            LayerEditing = 0;

            Modder Mod = ModProgram.Modder;
            TreeViews = new List<TreeView>();
            LayerIDs = new List<int>();
            comboBox_Layers.Items.Clear();
            comboBox_Layers.Items.Add("Layer 0: Extracted files");
            if (Mod != null)
            {
                for (int i = 0; i < Mod.Pipelines.Count; i++)
                {
                    ModPipeline pipe = Mod.Pipelines[i] as ModPipeline;
                    comboBox_Layers.Items.Add("Layer " + pipe.ModLayerID + ": " + pipe.Name);
                    TreeView NewTree = new TreeView()
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238))),
                        ImageIndex = 0,
                        ImageList = imageList1,
                        Margin = new Padding(0),
                        Name = "treeView_files" + i,
                        SelectedImageIndex = 0,
                    };
                    NewTree.Parent = tableLayoutPanel1;
                    NewTree.Visible = false;
                    NewTree.Enabled = false;
                    tableLayoutPanel1.SetRowSpan(NewTree, 8);
                    tableLayoutPanel1.SetRow(NewTree, 1);
                    tableLayoutPanel1.SetColumn(NewTree, 4);
                    TreeViews.Add(NewTree);
                    LayerIDs.Add(pipe.ModLayerID);
                }
            }

            button_AddFile.Enabled = button_AddFolder.Enabled = button_RemoveFile.Enabled = button_RemoveFolder.Enabled = ModProgram.Pipeline.Metadata.CanOnlyReplaceFiles;
            toolTip1.SetToolTip(button_ExportFile, "Export File/Folder");
            toolTip1.SetToolTip(button_EditInfo, "Edit name, description and other details about the Mod Crate.");
            toolTip1.SetToolTip(button_AddFile, "Add File in selected Folder");
            toolTip1.SetToolTip(button_AddFolder, "Add Folder in selected Folder");
            toolTip1.SetToolTip(button_ReplaceFile, "Replace selected File");
            toolTip1.SetToolTip(button_ModMenu, "Open Mod Menu of which setting will be applied in the Mod Crate.");
            toolTip1.SetToolTip(button_RemoveFile, "Remove selected File");
            toolTip1.SetToolTip(button_RemoveFolder, "Remove selected Folder");

            GenerateTree(0);
            for (int i = 0; i < TreeViews.Count; i++)
            {
                GenerateTree(i + 1);
            }
        }

        private void button_SaveAs_Click(object sender, EventArgs e)
        {
            //todo: export to folder
            ExportingFile = false;
            saveFileDialog1.FileName = $"{crate.Name} {crate.Version}.zip";
            saveFileDialog1.Filter = "Mod Crate (*.zip)|*.zip|" + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";
            saveFileDialog1.ShowDialog();
        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            SaveFileDialog s = sender as SaveFileDialog;
            TreeView tree = treeView_files;
            if (!BaseLayer) tree = TreeViews[(int)LayerEditing - 1];
            if (ExportingFile)
            {
                if (tree.SelectedNode.Tag == null) return;
                if (tree.SelectedNode.Tag is FileNode file)
                {
                    File.Copy(file.File.FullName, saveFileDialog1.FileName, true);
                }
                return;
            }

            ConsolePipeline Pipeline = ModProgram.Pipeline;
            Modder Mod = ModProgram.Modder;

            if (Mod != null)
                crate.TargetGame = ModProgram.Game.ShortName;
            else
                crate.TargetGame = ModCrates.UnsupportedGameShortName;
            crate.HasSettings = true;
            crate.IsFolder = false;

            string tempPath = ModLoaderGlobals.ModDirectory + @"\" + ModLoaderGlobals.TempName;
            string assetsPath = tempPath + @"\" + ModLoaderGlobals.ModAssetsFolderName + @"\";
            Directory.CreateDirectory(tempPath);
            Directory.CreateDirectory(assetsPath);

            List<string> LineList_Info = new List<string>();
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Name, ModCrates.Separator, crate.Name));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Desc, ModCrates.Separator, crate.Desc));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Author, ModCrates.Separator, crate.Author));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Version, ModCrates.Separator, crate.Version));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_CML_Version, ModCrates.Separator, crate.CML_Version));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Game, ModCrates.Separator, crate.TargetGame));
            File.WriteAllLines(Path.Combine(tempPath, ModCrates.InfoFileName), LineList_Info);

            if (crate.HasIcon)
            {
                File.Copy(crate.IconPath, Path.Combine(tempPath, ModCrates.IconFileName));
            }

            Dictionary<string, string> Assets = new Dictionary<string, string>();
            List<string> IgnoreDirs = new List<string>();
            Dictionary<string, string> Layer0 = new Dictionary<string, string>();
            List<string> Layer0_Folders = new List<string>();
            List<Dictionary<string, string>> Layers = new List<Dictionary<string, string>>();
            List<List<string>> LayerFolders = new List<List<string>>();
            if (Mod != null)
            {
                Assets = ModCrates.SaveSettingsToCrate(Mod, Path.Combine(tempPath, ModCrates.SettingsFileName), false);
                for (int i = 0; i < Mod.Pipelines.Count; i++)
                {
                    Layers.Add(new Dictionary<string, string>());
                    LayerFolders.Add(new List<string>());
                    ModPipeline pipe = Mod.Pipelines[i] as ModPipeline;
                    foreach (string ext in pipe.ExtractedPaths)
                    {
                        string testPath = ext;
                        if (!testPath.EndsWith("\\"))
                        {
                            testPath += "\\";
                        }
                        IgnoreDirs.Add(testPath);
                    }
                }
            }

            string layer0Name = ModCrates.LayerFolderName + @"0/";
            Recursive_FindFiles(layer0Name, (DirNode)treeView_files.Nodes[0].Tag, Layer0, Layer0_Folders);
            if (Mod != null)
            {
                for (int i = 0; i < Mod.Pipelines.Count; i++)
                {
                    string layerName = ModCrates.LayerFolderName + LayerIDs[i] + @"/";
                    Recursive_FindFiles(layerName, (DirNode)TreeViews[i].Nodes[0].Tag, Layers[i], LayerFolders[i]);
                }
            }

            string zipFolderName = ModLoaderGlobals.ModAssetsFolderName + @"/";
            using (FileStream fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create))
            {
                using (ZipArchive zip = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(Path.Combine(tempPath, ModCrates.InfoFileName), ModCrates.InfoFileName);
                    if (Mod != null)
                    {
                        zip.CreateEntryFromFile(Path.Combine(tempPath, ModCrates.SettingsFileName), ModCrates.SettingsFileName);
                    }
                    if (crate.HasIcon)
                    {
                        zip.CreateEntryFromFile(Path.Combine(tempPath, ModCrates.IconFileName), ModCrates.IconFileName);
                    }
                    if (Assets.Count > 0)
                    {
                        zip.CreateEntry(zipFolderName);
                        foreach (KeyValuePair<string, string> pair in Assets)
                        {
                            zip.CreateEntryFromFile(pair.Key, zipFolderName + pair.Value);
                        }
                    }

                    // Layer 0
                    if (Layer0.Count > 0)
                    {
                        zip.CreateEntry(layer0Name);
                        foreach(string dir in Layer0_Folders)
                        {
                            zip.CreateEntry(dir);
                        }
                        foreach (KeyValuePair<string, string> pair in Layer0)
                        {
                            zip.CreateEntryFromFile(pair.Key, pair.Value);
                        }
                    }
                    // Other layers
                    if (Layers.Count > 0)
                    {
                        for (int i = 0; i < Layers.Count; i++)
                        {
                            string layerName = ModCrates.LayerFolderName + LayerIDs[i] + @"/";
                            zip.CreateEntry(layerName);
                            foreach (string dir in LayerFolders[i])
                            {
                                zip.CreateEntry(dir);
                            }
                            foreach (KeyValuePair<string, string> pair in Layers[i])
                            {
                                zip.CreateEntryFromFile(pair.Key, pair.Value);
                            }
                        }
                    }

                }
            }

            //cleanup
            Directory.Delete(tempPath, true);

            Console.WriteLine("Mod crate saved at: " + saveFileDialog1.FileName);
            MessageBox.Show("Save complete!");
        }

        void Recursive_FindFiles(string rootPath, DirNode root, Dictionary<string, string> filemap, List<string> folders)
        {
            foreach (TreeNode node in root.Node.Nodes)
            {
                if (node.Tag is DirNode dir)
                {
                    string newPath = rootPath + dir.Node.Text + @"/";
                    Recursive_FindFiles(newPath, dir, filemap, folders);
                }
                else if (node.Tag is FileNode file)
                {
                    if (file.isReplaced)
                    {
                        string newPath = rootPath + file.Node.Text;
                        filemap.Add(file.ExternalPath, newPath);

                        /* 
                        //ehh, it doesn't care about folder entries...
                        string testPath = rootPath + "";
                        TreeNode thisNode = root.Node;
                        while (thisNode.Parent != null)
                        {
                            if (!folders.Contains(testPath))
                            {
                                //folders.Add(testPath);
                            }
                            string trim = thisNode.Text + @"\";
                            testPath = testPath.TrimEnd(trim.ToCharArray());
                            thisNode = thisNode.Parent;
                        }
                        */
                    }
                }
            }
        }

        private void button_ReplaceFile_Click(object sender, EventArgs e)
        {
            TreeView tree = treeView_files;
            if (!BaseLayer) tree = TreeViews[(int)LayerEditing - 1];
            if (tree.SelectedNode.Tag == null) return;
            if (tree.SelectedNode.Tag is DirNode) return; //todo: replace folder

            if (tree.SelectedNode.Tag is FileNode file)
            {
                openFileDialog1.Filter = ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //File.Copy(openFileDialog1.FileName, file.File.FullName, true);
                    file.Replace(openFileDialog1.FileName);
                }
            }
        }

        private void button_ExportFile_Click(object sender, EventArgs e)
        {
            TreeView tree = treeView_files;
            if (!BaseLayer) tree = TreeViews[(int)LayerEditing - 1];
            if (tree.SelectedNode.Tag == null) return;
            if (tree.SelectedNode.Tag is DirNode) return; //todo: extract folder

            if (tree.SelectedNode.Tag is FileNode file)
            {
                ExportingFile = true;
                saveFileDialog1.FileName = file.File.Name + Path.GetExtension(file.File.FullName);
                saveFileDialog1.Filter = ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";
                saveFileDialog1.ShowDialog();
            }
        }

        private void button_AddFile_Click(object sender, EventArgs e)
        {
            TreeView tree = treeView_files;
            if (!BaseLayer) tree = TreeViews[(int)LayerEditing - 1];
            if (tree.SelectedNode.Tag == null) return;

            if (tree.SelectedNode.Tag is FileNode file)
            {
                tree.SelectedNode = file.Node.Parent;
            }

            if (tree.SelectedNode.Tag is DirNode dir)
            {
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.Filter = ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //File.Copy(openFileDialog1.FileName, file.File.FullName, true);
                    string fName = Path.GetFileName(openFileDialog1.FileName);
                    FileNode fnode = new FileNode(fName, null);
                    fnode.NewFile(openFileDialog1.FileName);
                    tree.SelectedNode.Nodes.Add(fnode.Node);
                }
            }
        }

        private void button_AddFolder_Click(object sender, EventArgs e)
        {
            TreeView tree = treeView_files;
            if (!BaseLayer) tree = TreeViews[(int)LayerEditing - 1];
            if (tree.SelectedNode.Tag == null) return;

            if (tree.SelectedNode.Tag is FileNode file)
            {
                tree.SelectedNode = file.Node.Parent;
            }

            if (tree.SelectedNode.Tag is DirNode dir)
            {
                // todo: add folder if pipeline allows it
            }
        }

        private void button_ModMenu_Click(object sender, EventArgs e)
        {
            if (ModProgram.Game != null)
            {
                ModMenuForm modMenu = new ModMenuForm(null, ModProgram.Modder, ModProgram.Game);

                modMenu.Owner = this;
                modMenu.Show();
            }
        }

        private void button_EditInfo_Click(object sender, EventArgs e)
        {
            ModCrateMakerForm editMenu = new ModCrateMakerForm(crate);

            editMenu.Owner = this;
            editMenu.Show();
        }

        void GenerateTree(int id)
        {
            TreeView targetTree = treeView_files;
            if (id != 0) targetTree = TreeViews[id - 1];

            targetTree.Nodes.Clear();

            ConsolePipeline Pipeline = ModProgram.Pipeline;
            Modder Mod = ModProgram.Modder;

            List<string> IgnoreDirs = new List<string>();
            string TargetPath = Pipeline.ExtractedPath;
            
            if (Mod != null)
            {
                for (int i = 0; i < Mod.Pipelines.Count; i++)
                {
                    ModPipeline pipe = Mod.Pipelines[i] as ModPipeline;
                    foreach (string ext in pipe.ExtractedPaths)
                    {
                        string testPath = ext;
                        if (!testPath.EndsWith("\\"))
                        {
                            testPath += "\\";
                        }
                        IgnoreDirs.Add(testPath);
                    }
                }
            }

            DirNode root = new DirNode();
            root.Node.Text = "Root";

            if (id == 0)
            {
                DirectoryInfo dir = new DirectoryInfo(TargetPath);
                root.Dir = dir;
                Recursive_AddNodes(dir, root, IgnoreDirs);
            }
            else
            {
                for (int i = 0; i < Mod.Pipelines.Count; i++)
                {
                    ModPipeline pipe = Mod.Pipelines[i] as ModPipeline;
                    foreach (string ext in pipe.ExtractedPaths)
                    {
                        TargetPath = ext;
                        DirectoryInfo dir = new DirectoryInfo(TargetPath);
                        root.Dir = dir;
                        Recursive_AddNodes(dir, root, new List<string>());
                    }
                }
            }

            targetTree.Nodes.Add(root.Node);
            targetTree.Nodes[0].Expand();

        }

        void Recursive_AddNodes(DirectoryInfo di, DirNode root, List<string> ignoreDirs)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                if (!CheckNodeDupe(root, dir.Name, false))
                {
                    string testPath = dir.FullName;
                    if (!testPath.EndsWith("\\"))
                    {
                        testPath += "\\";
                    }
                    if (!ignoreDirs.Contains(testPath))
                    {
                        DirNode node = new DirNode(dir.Name, dir);
                        root.Node.Nodes.Add(node.Node);
                        Recursive_AddNodes(dir, node, ignoreDirs);
                    }
                }
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (!CheckNodeDupe(root, file.Name, true))
                {
                    FileNode fnode = new FileNode(file.Name, file);
                    root.Node.Nodes.Add(fnode.Node);
                }
            }
        }
        bool CheckNodeDupe(DirNode root, string Name, bool isFile)
        {
            // this is for something like .RCF where dozens of archives with dupe files represent one layer
            TreeNode node = root.Node;
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                if (node.Nodes[i].Text == Name)
                {
                    if (isFile && node.Nodes[i].Tag is FileNode)
                    {
                        return true;
                    }
                    else if (!isFile && node.Nodes[i].Tag is DirNode)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ModCrateWizardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }

        private void ModCrateWizardForm_Load(object sender, EventArgs e)
        {
            Owner.Enabled = false;
        }

        private void comboBox_Layers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = sender as ComboBox;
            if (c.SelectedIndex < 0) return;
            if (LayerEditing == c.SelectedIndex) return;

            LayerEditing = (uint)c.SelectedIndex;

            ConsolePipeline Pipeline = ModProgram.Pipeline;
            Modder Mod = ModProgram.Modder;
            if (!BaseLayer)
            {
                if (Mod != null)
                {
                    ModPipeline pipe = Mod.Pipelines[(int)LayerEditing - 1] as ModPipeline;
                    button_AddFile.Enabled = button_AddFolder.Enabled = button_RemoveFile.Enabled = button_RemoveFolder.Enabled = !pipe.ModLayerReplaceOnly;
                }
            }
            else
            {
                button_AddFile.Enabled = button_AddFolder.Enabled = button_RemoveFile.Enabled = button_RemoveFolder.Enabled = Pipeline.Metadata.CanOnlyReplaceFiles;
            }

            if (BaseLayer)
            {
                treeView_files.Visible = treeView_files.Enabled = true;
                for (int i = 0;i < TreeViews.Count; i++)
                {
                    TreeViews[i].Visible = TreeViews[i].Enabled = false;
                }
            }
            else
            {
                treeView_files.Visible = false;
                for (int i = 0; i < TreeViews.Count; i++)
                {
                    if (i == LayerEditing - 1)
                    {
                        TreeViews[i].Visible = TreeViews[i].Enabled = true;
                    }
                    else
                    {
                        TreeViews[i].Visible = TreeViews[i].Enabled = false;
                    }
                }
            }

            //GenerateTree();
        }

        private void button_RemoveFile_Click(object sender, EventArgs e)
        {
            //todo: remove added files, zero out existing files?
        }

        private void button_RemoveFolder_Click(object sender, EventArgs e)
        {
            //todo: remove added folders?
        }

        private void button_LevelEditor_Click(object sender, EventArgs e)
        {
            //tbd
        }
    }
}
