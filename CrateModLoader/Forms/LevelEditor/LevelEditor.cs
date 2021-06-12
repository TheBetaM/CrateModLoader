using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using HelixToolkit.Wpf;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.Forms.LevelEditor
{
    public partial class LevelEditor : Form
    {
        public ModLoader ModProgram;
        private BindingSource bindingSource = new BindingSource();
        private LevelBase Level;
        private List<string> LevelList = new List<string>();
        private Dictionary<string, ModParserBase> LevelParsers = new Dictionary<string, ModParserBase>();

        public LevelEditor(ModLoader Program)
        {
            ModProgram = Program;
            InitializeComponent();

            comboBox1.Items.Clear();
            comboBox1.BeginUpdate();

            // parsers cannot be skipped for this to work!
            foreach (ModParserBase Parser in ModProgram.Modder.ModParsers)
            {
                if (Parser.IsLevelFile)
                {
                    foreach (KeyValuePair<string, List<FileInfo>> FileList in Parser.FoundFiles)
                    {
                        foreach (FileInfo file in FileList.Value)
                        {
                            string ItemName = file.FullName.Substring(ModProgram.Pipeline.ExtractedPath.Length);
                            comboBox1.Items.Add(ItemName);
                            LevelList.Add(file.FullName);
                            LevelParsers.Add(file.FullName, Parser);
                        }
                    }
                }
            }

            comboBox1.EndUpdate();



            mainViewport3D.ModProgram = Program;
            //LoadLevel(0);
            comboBox1.SelectedIndex = 0;
        }

        private void LevelEditor_Load(object sender, EventArgs e)
        {
            Owner.Enabled = false;
        }

        private void LevelEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }

        void LoadLevel(int LevelID)
        {
            propertyGrid1.SelectedObject = null;
            listBox_Objects.Items.Clear();
            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();

            string FileName = LevelList[LevelID];
            ModParserBase Parser = LevelParsers[FileName];

            Level = Parser.LoadLevel(FileName);
            if (Level == null)
            {
                Console.WriteLine("Failed to load level: " + FileName);
                treeView1.EndUpdate();
                return;
            }

            mainViewport3D.Create3DViewPort(Level);

            //Populate category tree
            TreeNode RootNode = new TreeNode();
            RootNode.Text = "Level";
            RootNode.Name = "RootNode";
            RootNode.Tag = Level;
            treeView1.Nodes.Add(RootNode);

            List<int> Categories = new List<int>();
            Dictionary<int, int> ItemCount = new Dictionary<int, int>();

            foreach (LevelObjectDataBase Data in Level.ObjectData)
            {
                if (!Categories.Contains(Data.ObjectCategory))
                {
                    int CatID = Data.ObjectCategory;
                    Categories.Add(CatID);
                    ItemCount.Add(CatID, 1);
                }
                else
                {
                    ItemCount[Data.ObjectCategory]++;
                }
            }
            foreach (int CatID in Categories)
            {
                TreeNode Node = new TreeNode();
                if (Level.CategoryNames.ContainsKey(CatID))
                {
                    Node.Text = Level.CategoryNames[CatID];
                }
                else
                {
                    Node.Text = "Section " + CatID;
                }
                Node.Text += " (" + ItemCount[CatID] + " items)";
                Node.Tag = CatID;
                RootNode.Nodes.Add(Node);
            }

            RootNode.Expand();

            treeView1.EndUpdate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) return;

            // Level Changed -> Unload (save?) old one, load new one
            //Level.Save();
            LoadLevel(comboBox1.SelectedIndex);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null) return;

            //todo: visibility toggle for category
            
            listBox_Objects.Items.Clear();
            listBox_Objects.BeginUpdate();

            if (treeView1.SelectedNode.Tag is LevelBase)
            {
                propertyGrid1.SelectedObject = Level;
            }
            else
            {
                propertyGrid1.SelectedObject = null;
                foreach (LevelObjectDataBase Data in Level.ObjectData)
                {
                    if (Data.ObjectCategory == (int)treeView1.SelectedNode.Tag)
                    {
                        listBox_Objects.Items.Add(Data);
                    }
                }
            }
            listBox_Objects.EndUpdate();


        }

        private void listBox_Objects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Objects.SelectedIndex < 0) return;

            propertyGrid1.SelectedObject = listBox_Objects.SelectedItem;
            if (listBox_Objects.SelectedItem is LevelObjectDataBase data)
            {
                mainViewport3D.MoveViewportToObject(data);
            }
        }

    }
}
