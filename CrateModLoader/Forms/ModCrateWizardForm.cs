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
//using Octodiff.Core;
//using Octodiff.Diagnostics;

namespace CrateModLoader.Forms
{
    public partial class ModCrateWizardForm : Form
    {
        public ModLoader ModProgram;
        private ModCrate crate;
        private uint LayerEditing = 0;
        private bool BaseLayer => (LayerEditing == 0);
        private bool ExportingFile = false;
        private bool EditMode = false;
        private List<TreeView> TreeViews;
        private List<int> LayerIDs;

        public ModCrateWizardForm(ModLoader program, ModCrate editCrate = null)
        {
            ModProgram = program;
            InitializeComponent();
            crate = new ModCrate();
            if (editCrate != null)
            {
                editCrate.CopyTo(crate);
                EditMode = true;
            }

            if (ModProgram.Game == null)
            {
                button_ModMenu.Enabled = false;
                button_LevelEditor.Enabled = false;
            }
            else
            {
                button_LevelEditor.Enabled = ModProgram.Game.EnableLevelEditor;
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
            toolTip1.SetToolTip(button_RestoreFile, "Restore selected File if it has been replaced");
            toolTip1.SetToolTip(button_ReplaceFileDelta, "Replace selected File by creating a delta patch (smaller size, incompatible between different versions)");

            GenerateTree(0);
            for (int i = 0; i < TreeViews.Count; i++)
            {
                GenerateTree(i + 1);
            }

            if (EditMode)
            {
                LoadCrate();
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

            if (EditMode && File.Exists(saveFileDialog1.FileName))
            {
                Console.WriteLine("Cannot overwrite editing crate: " + saveFileDialog1.FileName);
                MessageBox.Show("Save failed! Cannot overwrite Mod Crates in Edit mode.");
                return;
            }

            try
            {
                SaveCrate(saveFileDialog1.FileName);
                Console.WriteLine("Mod Crate saved at: " + saveFileDialog1.FileName);
                MessageBox.Show("Save complete!");
            }
            catch
            {
                Console.WriteLine("Failed to save Mod Crate: " + saveFileDialog1.FileName);
                MessageBox.Show("Save failed!");
            }
        }

        void SaveCrate(string SavePath)
        {
            bool restorefolder = crate.IsFolder;
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
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
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
                File.Copy(crate.IconPath, Path.Combine(tempPath, ModCrates.IconFileName), true);
            }

            Dictionary<string, string> Assets = new Dictionary<string, string>();
            Dictionary<string, string> Layer0 = new Dictionary<string, string>();
            List<Dictionary<string, string>> Layers = new List<Dictionary<string, string>>();
            if (Mod != null)
            {
                Assets = ModCrates.SaveSettingsToCrate(Mod, Path.Combine(tempPath, ModCrates.SettingsFileName), false);
                for (int i = 0; i < Mod.Pipelines.Count; i++)
                {
                    Layers.Add(new Dictionary<string, string>());
                }
            }

            string layer0Name = ModCrates.LayerFolderName + @"0/";
            Recursive_FindFiles(layer0Name, (DirNode)treeView_files.Nodes[0].Tag, Layer0);
            if (Mod != null)
            {
                for (int i = 0; i < Mod.Pipelines.Count; i++)
                {
                    string layerName = ModCrates.LayerFolderName + LayerIDs[i] + @"/";
                    Recursive_FindFiles(layerName, (DirNode)TreeViews[i].Nodes[0].Tag, Layers[i]);
                }
            }

            string zipFolderName = ModLoaderGlobals.ModAssetsFolderName + @"/";
            using (FileStream fileStream = new FileStream(SavePath, FileMode.Create))
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

            foreach(KeyValuePair<string, string> pair in Layer0)
            {
                if (Path.GetExtension(pair.Key).EndsWith("octodelta"))
                {
                    File.Delete(pair.Key);
                }
            }
            for (int i = 0; i < Layers.Count; i++)
            {
                foreach (KeyValuePair<string, string> pair in Layers[i])
                {
                    if (Path.GetExtension(pair.Key).EndsWith("octodelta"))
                    {
                        File.Delete(pair.Key);
                    }
                }
            }

            crate.IsFolder = restorefolder;
        }

        void LoadCrate()
        {
            Modder Mod = ModProgram.Modder;
            if (Mod != null)
            {
                ModCrates.InstallCrateSettings(crate, ModProgram.Modder);
            }
            string cratePath = crate.Path;

            if (!crate.IsFolder)
            {
                cratePath = ModLoaderGlobals.ModDirectory + "tempext";
                Directory.CreateDirectory(cratePath);
                using (ZipArchive archive = ZipFile.OpenRead(crate.Path))
                {
                    archive.ExtractToDirectory(cratePath);
                }
            }

            if (crate.HasIcon)
            {
                crate.IconPath = cratePath + @"\" + ModCrates.IconFileName;
            }
            if (crate.LayersModded.Length == 1 && !crate.LayersModded[0])
            {
                // Nothing else to do
                return;
            }

            for (int i = 0; i < crate.LayersModded.Length; i++)
            {
                if (crate.LayersModded[i])
                {
                    int LayerID = 0;
                    if (i != 0)
                    {
                        for (int id = 0; id < LayerIDs.Count; id++)
                        {
                            if (LayerIDs[id] == i)
                            {
                                LayerID = id;
                            }
                        }
                        if (LayerID > TreeViews.Count)
                        {
                            throw new Exception("Invalid layer!!");
                        }
                    }

                    TreeView tree = treeView_files;
                    if (i != 0) tree = TreeViews[LayerID];

                    DirectoryInfo dir = new DirectoryInfo(cratePath + @"\" + ModCrates.LayerFolderName + i + @"\");
                    Recursive_CompareLayers((DirNode)tree.Nodes[0].Tag, dir);
                }
            }

        }

        void Recursive_CompareLayers(DirNode root, DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                TreeNode node = NodeContainsName(root.Node, dir.Name);
                DirNode dnode = null;
                if (node != null)
                {
                    dnode = (DirNode)node.Tag;
                }
                else
                {
                    dnode = new DirNode(dir.Name, dir);
                    dnode.NewFolder();
                    root.Node.Nodes.Add(dnode.Node);
                }
                Recursive_CompareLayers(dnode, dir);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                TreeNode node = null;
                if (Path.GetExtension(file.Name).EndsWith("octodelta"))
                {
                    node = NodeContainsName(root.Node, file.Name.TrimEnd(".octodelta".ToCharArray()));
                    if (node != null)
                    {
                        FileNode fnode = (FileNode)node.Tag;
                        fnode.ReplaceDelta(file.FullName);
                        //todo: test if the delta patch can even be applied?
                    }
                    else
                    {
                        // there's no file that the delta patch is for so... do nothing? error/warning?
                    }
                }
                else
                {
                    node = NodeContainsName(root.Node, file.Name);
                    if (node != null)
                    {
                        FileNode fnode = (FileNode)node.Tag;
                        fnode.Replace(file.FullName);
                    }
                    else
                    {
                        FileNode fnode = new FileNode(file.Name, file);
                        fnode.NewFile(file.FullName);
                        root.Node.Nodes.Add(fnode.Node);
                    }
                }
            }
        }

        TreeNode NodeContainsName(TreeNode tree, string name)
        {
            foreach(TreeNode node in tree.Nodes)
            {
                if (node.Text == name)
                {
                    return node;
                }
            }
            return null;
        }

        void Recursive_FindFiles(string rootPath, DirNode root, Dictionary<string, string> filemap)
        {
            foreach (TreeNode node in root.Node.Nodes)
            {
                if (node.Tag is DirNode dir)
                {
                    string newPath = rootPath + dir.Node.Text + @"/";
                    Recursive_FindFiles(newPath, dir, filemap);
                }
                else if (node.Tag is FileNode file)
                {
                    if (file.isReplaced)
                    {
                        if (file.isDelta)
                        {
                            //Create octodiff delta patch and and it to the filemap
                            var signatureBaseFilePath = file.File.FullName;
                            var signatureFilePath = file.ExternalPath + @".octosig";
                            var newFilePath = file.ExternalPath;
                            var deltaFilePath = file.ExternalPath + @".octodelta";
                            /*
                            try
                            {
                                // Create signature file
                                var signatureBuilder = new SignatureBuilder();
                                using (var basisStream = new FileStream(signatureBaseFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                                using (var signatureStream = new FileStream(signatureFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                {
                                    signatureBuilder.Build(basisStream, new SignatureWriter(signatureStream));
                                }

                                // Create delta file
                                var deltaBuilder = new DeltaBuilder();
                                using (var newFileStream = new FileStream(newFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                                using (var signatureFileStream = new FileStream(signatureFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                                using (var deltaStream = new FileStream(deltaFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                {
                                    deltaBuilder.BuildDelta(newFileStream, new SignatureReader(signatureFileStream, new ConsoleProgressReporter()), new AggregateCopyOperationsDecorator(new BinaryDeltaWriter(deltaStream)));
                                }

                                string newPath = rootPath + file.Node.Text + ".octodelta";
                                filemap.Add(deltaFilePath, newPath);

                                //Delete signature file
                                File.Delete(signatureFilePath);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Octodiff error: " + ex.Message);
                            }
                            */
                        }
                        else
                        {
                            string newPath = rootPath + file.Node.Text;
                            filemap.Add(file.ExternalPath, newPath);
                        }
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
        private void button_ReplaceFileDelta_Click(object sender, EventArgs e)
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
                    file.ReplaceDelta(openFileDialog1.FileName);
                }
            }
        }
        private void button_RestoreFile_Click(object sender, EventArgs e)
        {
            TreeView tree = treeView_files;
            if (!BaseLayer) tree = TreeViews[(int)LayerEditing - 1];
            if (tree.SelectedNode.Tag == null) return;
            if (tree.SelectedNode.Tag is DirNode) return; //todo: restore folder

            if (tree.SelectedNode.Tag is FileNode file)
            {
                file.Restore();
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
                saveFileDialog1.FileName = file.File.Name; //+ Path.GetExtension(file.File.FullName);
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
                    FileNode fnode = new FileNode(fName, new FileInfo(openFileDialog1.FileName));
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
                string testPath = dir.FullName;
                if (!testPath.EndsWith("\\"))
                {
                    testPath += "\\";
                }
                if (!ignoreDirs.Contains(testPath))
                {
                    DirNode node = CheckNodeDupe(root, dir.Name, false);
                    if (node == null)
                    {
                        node = new DirNode(dir.Name, dir);
                        root.Node.Nodes.Add(node.Node);
                    }
                    Recursive_AddNodes(dir, node, ignoreDirs);
                }
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (!CheckNodeDupeFile(root, file.Name, true))
                {
                    FileNode fnode = new FileNode(file.Name, file);
                    root.Node.Nodes.Add(fnode.Node);
                }
            }
        }
        DirNode CheckNodeDupe(DirNode root, string Name, bool isFile)
        {
            // this is for something like .RCF where dozens of archives with dupe files represent one layer
            TreeNode node = root.Node;
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                if (node.Nodes[i].Text == Name)
                {
                    if (!isFile && node.Nodes[i].Tag is DirNode dir)
                    {
                        return dir;
                    }
                }
            }
            return null;
        }
        bool CheckNodeDupeFile(DirNode root, string Name, bool isFile)
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
            if (!crate.IsFolder && Directory.Exists(ModLoaderGlobals.ModDirectory + "tempext"))
            {
                crate = null;
                Directory.Delete(ModLoaderGlobals.ModDirectory + "tempext", true);
            }
            crate = null;

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
            //LevelEditor.LevelEditor Editor = new LevelEditor.LevelEditor(ModProgram);
            //Editor.Owner = this;
            //Editor.Show();
        }
    }
}
