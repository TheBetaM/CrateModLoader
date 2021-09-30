using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TwinsaityEditor.Properties;
using Twinsanity.Items;
using WK.Libraries.BetterFolderBrowserNS;

namespace TwinsaityEditor
{
    public partial class BDExplorer : Form
    {
        private Data data = null;
        private string path;
        private string name;
        private PTCViewer viewer;

        public BDExplorer()
        {
            InitializeComponent();
            buttonExtractAll.Enabled = false;
            buttonExtractSelected.Enabled = false;
            Show();
        }

        internal void LoadData(string path, string name)
        {
            data = null;
            using (FileStream fileStream = new FileStream(string.Format(Path.Combine(path, name) + ".BH"), FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                data = new Data(reader);
            }
            if (data != null)
            {
                UpdateView();
            }
            else
            {
                throw new Exception("Error loading BH file.");
            }
        }

        internal void UpdateView()
        {
            archiveContentsTree.BeginUpdate();
            archiveContentsTree.Nodes.Clear();
            archiveContentsTree.Nodes.Add(new TreeNode(name));
            foreach (BH_Record record in data.FileList)
            {
                AddNode(record);
            }
            archiveContentsTree.EndUpdate();
        }

        internal void AddNode(BH_Record record)
        {
            string[] directories = Path.GetDirectoryName(record.Path).Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            string name = Path.GetFileName(record.Path);
            TreeNode node = archiveContentsTree.TopNode;
            foreach (string dir in directories)
            {
                if (node.Nodes.ContainsKey(dir))
                {
                    node = node.Nodes.Find(dir, false)[0];
                }
                else
                {
                    node = node.Nodes.Add(dir, dir);
                }
            }
            node = node.Nodes.Add(name);
            if (name.EndsWith("psm") || name.EndsWith("ptc") || name.EndsWith("psf"))
            {
                var viewMenu = new MenuItem("View");
                viewMenu.Click += PSViewer_OnClick;
                viewMenu.Tag = record;
                node.ContextMenu = new ContextMenu();
                node.ContextMenu.MenuItems.Add(viewMenu);
            }
            node.Tag = record;
        }

        internal void PSViewer_OnClick(object sender, EventArgs e)
        {
            if (viewer != null && !viewer.IsDisposed)
            {
                viewer.Close();
                viewer = null;
                GC.Collect(); // Collect all created references instantly just in case to avoid more memory leaks than we already have
            }
            viewer = new PTCViewer();
            var menu = (MenuItem)sender;
            var record = (BH_Record)menu.Tag;
            string name = Path.GetFileName(record.Path);
            var bdPath = $"{path}\\{this.name}.BD";
            using (FileStream fileStream = new FileStream(bdPath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                reader.BaseStream.Position = record.Offset;
                if (name.EndsWith("psf"))
                {
                    TwinsPSF psf = new TwinsPSF();
                    psf.Load(reader, record.Length);
                    viewer.PTCs = psf.FontPages;
                }
                else if (name.EndsWith("ptc"))
                {
                    TwinsPTC ptc = new TwinsPTC();
                    ptc.Load(reader, record.Length);
                    viewer.PTCs.Add(ptc);
                }
                else if (name.EndsWith("psm"))
                {
                    TwinsPSM psm = new TwinsPSM();
                    psm.Load(reader, record.Length);
                    viewer.PTCs = psm.PTCs;
                    viewer.EnablePSMCheckbox();
                }
            }
            viewer.SelectedPTC = viewer.PTCs[0];
            viewer.UpdateTextureLabel();
            viewer.Show();
        }

        internal class Data
        {
            public Data(BinaryReader reader)
            {
                Header = reader.ReadInt32();
                FileList = new List<BH_Record>();
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    FileList.Add(new BH_Record(reader));
                }
            }
            public Data(string folderToPack)
            {
                Header = 0x501;
                FileList = new List<BH_Record>();
                string[] files = Directory.GetFiles(folderToPack, "*.*", SearchOption.AllDirectories);
                int offset = 0;
                foreach (string file in files)
                {
                    BH_Record last = new BH_Record(folderToPack, file, offset);
                    offset += last.Length;
                    FileList.Add(last);
                }
            }

            public int Header { get; private set; }
            public List<BH_Record> FileList { get; private set; }

            public void WriteDataBH(BinaryWriter writer, Action<string> callback)
            {
                writer.Write(Header);
                foreach (BH_Record record in FileList)
                {
                    record.WriteDataBH(writer, callback);
                }
            }
            public void WriteDataBD(string source, BinaryWriter writer, Action<string> callback)
            {
                foreach (BH_Record record in FileList)
                {
                    record.WriteDataBD(source, writer, callback);
                }
            }
        }

        internal class BH_Record
        {
            public BH_Record(string root, string fileName, int offset)
            {
                if (!root.EndsWith("\\")) root += "\\";
                Path = fileName.Replace(root, "");
                FileInfo info = new FileInfo(fileName);
                Length = (int)info.Length;
                Offset = offset;
            }
            public BH_Record(BinaryReader reader)
            {
                int nameLength = reader.ReadInt32();
                Path = new string(reader.ReadChars(nameLength));
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }
            
            public string Path { get; private set; }
            public int Offset { get; private set; }
            public int Length { get; private set; }
            
            public void WriteDataBH(BinaryWriter writer, Action<string> callback)
            {
                callback.Invoke(string.Format("Writing Header: {0}", Path));
                writer.Write(Path.Length);
                writer.Write(Path.ToCharArray());
                writer.Write(Offset);
                writer.Write(Length);

            }
            public void WriteDataBD(string source, BinaryWriter writer, Action<string> callback)
            {
                callback.Invoke(string.Format("Writing Data: {0}", Path));
                using (FileStream fileStream = new FileStream(System.IO.Path.Combine(source,Path), FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    writer.BaseStream.Position = Offset;
                    writer.Write(reader.ReadBytes(Length));
                }
            }
        }

        internal void ExtractRecursively(BinaryReader source, TreeNode node, string extractionPath)
        {
            BH_Record record = (BH_Record)node.Tag;
            if (record != null)
            {
                ExtractRecord(source, record, extractionPath);
            }
            else
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    ExtractRecursively(source, childNode, extractionPath);
                }
            }
        }

        internal void ExtractRecord(BinaryReader source, BH_Record record, string extractionPath)
        {
            string fullPath = Path.Combine(extractionPath, record.Path);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                CallBack(string.Format("Extracting: {0}", record.Path));
                source.BaseStream.Position = record.Offset;
                writer.Write(source.ReadBytes(record.Length));
            }
        }

        internal void ShowError(string msg)
        {
            MessageBox.Show(string.Format("Unhandled exception.\nMessage: {0}", msg), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal void PackArchive(string source, string destination, string name, Action callback = null)
        {
            data = new Data(source);
            string fullPathBH = Path.Combine(destination, string.Format("{0}.BH", name));
            string fullPathBD = Path.Combine(destination, string.Format("{0}.BD", name));
            using (FileStream fileStream = new FileStream(fullPathBH, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                data.WriteDataBH(writer, CallBack);
            }
            using (FileStream fileStream = new FileStream(fullPathBD, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                data.WriteDataBD(source, writer, CallBack);
            }
            if (callback != null)
            {
                callback.Invoke();
            }
        }

        internal void CallBack(string message)
        {
            statusBar.Text = message;
            Application.DoEvents();
        }

        internal bool PackBDArchives(string dest, Action callback = null)
        {
            string sourcePath = null;
            string name = "CRASH";
            using (BetterFolderBrowser ofd = new BetterFolderBrowser
            {
                Title = "Select BD/BH source folder",
                RootFolder = Settings.Default.BDSaveSrcPath
            })
            {
                if (DialogResult.OK == ofd.ShowDialog(this))
                {
                    Settings.Default.BDSaveSrcPath = ofd.SelectedPath;
                    sourcePath = ofd.SelectedPath;
                }
                else
                {
                    return false;
                }
            }
            PackArchive(sourcePath, dest, name, callback);
            return true;
        }

        internal void LockControls()
        {
            panel1.Enabled = false;
        }

        internal void UnlockControls()
        {
            panel1.Enabled = true;
        }
        
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.InitialDirectory = Settings.Default.BDFilePath;
                    ofd.Filter = "Bandicoot Header|*.BH";
                    if (DialogResult.OK == ofd.ShowDialog())
                    {
                        var file = ofd.FileName;
                        path = Path.GetDirectoryName(file);
                        name = Path.GetFileNameWithoutExtension(file);
                        Settings.Default.BDFilePath = file.Substring(0, file.LastIndexOf(Path.DirectorySeparatorChar));
                        LoadData(path, name);
                        buttonExtractAll.Enabled = true;
                        buttonExtractSelected.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void buttonExtractAll_Click(object sender, EventArgs e)
        {
            try
            {
                using (BetterFolderBrowser ofd = new BetterFolderBrowser
                {
                    Title = "Select destination folder",
                    RootFolder = Settings.Default.BDExtractPath
                })
                {
                    if (null != data)
                    {
                        if (DialogResult.OK == ofd.ShowDialog(this))
                        {
                            using (FileStream fileStream = new FileStream(Path.Combine(path, name) + ".BD", FileMode.Open, FileAccess.Read))
                            using (BinaryReader reader = new BinaryReader(fileStream))
                            {
                                Settings.Default.BDExtractPath = ofd.SelectedPath;
                                LockControls();
                                ExtractRecursively(reader, archiveContentsTree.TopNode, ofd.SelectedPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UnlockControls();
                ShowError(ex.Message);
            }
            UnlockControls();
            CallBack("Ready");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sourcePath = null;
                string destinationPath = null;
                string name = "CRASH"; //Hardcoded name
                using (BetterFolderBrowser ofd = new BetterFolderBrowser
                {
                    Title = "Select source folder",
                    RootFolder = Settings.Default.BDSaveSrcPath
                })
                {
                    if (DialogResult.OK == ofd.ShowDialog(this))
                    {
                        Settings.Default.BDSaveSrcPath = ofd.SelectedPath;
                        sourcePath = ofd.SelectedPath;
                    }
                    else
                    {
                        return;
                    }
                }
                using (BetterFolderBrowser ofd = new BetterFolderBrowser
                {
                    Title = "Select source folder",
                    RootFolder = Settings.Default.BDSaveSrcPath
                })
                {
                    ofd.Title = "Select destination folder";
                    ofd.RootFolder = Settings.Default.BDSaveDstPath;
                    if (DialogResult.OK == ofd.ShowDialog(this))
                    {
                        Settings.Default.BDSaveDstPath = ofd.SelectedPath;
                        destinationPath = ofd.SelectedPath;
                        if (
                            File.Exists(Path.Combine(destinationPath, string.Format("{0}.BH", name))) ||
                            File.Exists(Path.Combine(destinationPath, string.Format("{0}.BD", name)))
                            )
                        {
                            DialogResult result = MessageBox.Show(string.Format("Archive with name {0} already in destination folder. Overwrite?", name), "Attention",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (DialogResult.Yes != result)
                            {
                                destinationPath = null;
                            }
                        }

                    }
                }
                if (null != sourcePath && null != destinationPath)
                {
                    PackArchive(sourcePath, destinationPath, name);
                    UpdateView();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            CallBack("Ready");
        
        }

        private void buttonExtractSelected_Click(object sender, EventArgs e)
        {
            try
            {
                using (BetterFolderBrowser ofd = new BetterFolderBrowser
                {
                    Title = "Select destination folder",
                    RootFolder = Settings.Default.BDExtractPath
                })
                {
                    if (null != data && null != archiveContentsTree.SelectedNode)
                    {
                        using (FileStream fileStream = new FileStream(string.Format("{0}\\{1}.BD", path, name), FileMode.Open, FileAccess.Read))
                        using (BinaryReader reader = new BinaryReader(fileStream))
                        {
                            if (DialogResult.OK == ofd.ShowDialog(this))
                            {
                                Settings.Default.BDExtractPath = ofd.SelectedPath;
                                ExtractRecursively(reader, archiveContentsTree.SelectedNode, ofd.SelectedPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            CallBack("Ready");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
