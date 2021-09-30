using OpenTK;
using System;
using System.Windows.Forms;
using TwinsaityEditor.Utils;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class ChunkLinksEditor : Form
    {
        private ChunkLinksController controller;
        ChunkLinks.ChunkLink link;

        private int pos_i, areap_i, u1_i, u2_i;
        private bool ignore_value_change;

        public ChunkLinksEditor(ChunkLinksController c)
        {
            controller = c;
            InitializeComponent();
            PopulateList();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            numericUpDown2.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown3.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown4.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown5.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown6.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown7.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown8.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown9.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown10.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown39.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown38.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown37.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown11.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown12.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown13.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown14.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown15.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown16.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown17.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown18.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown19.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown40.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown41.ValueChanged += numericUpDown20_ValueChanged;
            numericUpDown42.ValueChanged += numericUpDown20_ValueChanged;
        }

        private void PopulateList()
        {
            listBox1.Items.Clear();
            foreach (var i in controller.Data.Links)
            {
                listBox1.Items.Add(i.Path);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            ignore_value_change = true;

            link = controller.Data.Links[listBox1.SelectedIndex];
            groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = true;

            textBox1.Text = link.Path;

            comboBox1.SelectedIndex = link.Type;

            textBox2.Text = Convert.ToString(link.Flags, 16).ToUpper();

            numericUpDown4.Value = (decimal)link.ObjectMatrix[3].W;

            numericUpDown1.Value = (decimal)link.ObjectMatrix[3].X;
            numericUpDown2.Value = (decimal)link.ObjectMatrix[3].Y;
            numericUpDown3.Value = (decimal)link.ObjectMatrix[3].Z;

            numericUpDown7.Value = (decimal)link.ObjectMatrix[0].X;
            numericUpDown6.Value = (decimal)link.ObjectMatrix[0].Y;
            numericUpDown5.Value = (decimal)link.ObjectMatrix[0].Z;

            numericUpDown10.Value = (decimal)link.ObjectMatrix[1].X;
            numericUpDown9.Value = (decimal)link.ObjectMatrix[1].Y;
            numericUpDown8.Value = (decimal)link.ObjectMatrix[1].Z;

            numericUpDown39.Value = (decimal)link.ObjectMatrix[2].X;
            numericUpDown38.Value = (decimal)link.ObjectMatrix[2].Y;
            numericUpDown37.Value = (decimal)link.ObjectMatrix[2].Z;

            numericUpDown20.Value = (decimal)link.ChunkMatrix[3].W;

            numericUpDown42.Value = (decimal)link.ChunkMatrix[3].X;
            numericUpDown41.Value = (decimal)link.ChunkMatrix[3].Y;
            numericUpDown40.Value = (decimal)link.ChunkMatrix[3].Z;

            numericUpDown19.Value = (decimal)link.ChunkMatrix[0].X;
            numericUpDown18.Value = (decimal)link.ChunkMatrix[0].Y;
            numericUpDown17.Value = (decimal)link.ChunkMatrix[0].Z;

            numericUpDown16.Value = (decimal)link.ChunkMatrix[1].X;
            numericUpDown15.Value = (decimal)link.ChunkMatrix[1].Y;
            numericUpDown14.Value = (decimal)link.ChunkMatrix[1].Z;

            numericUpDown13.Value = (decimal)link.ChunkMatrix[2].X;
            numericUpDown12.Value = (decimal)link.ChunkMatrix[2].Y;
            numericUpDown11.Value = (decimal)link.ChunkMatrix[2].Z;

            if (groupBox6.Enabled = (link.Flags & 0x80000) != 0)
            {
                GetLoadWallPos();
            }

            if (groupBox9.Enabled = groupBox8.Enabled = groupBox7.Enabled = link.Type == 1 || link.Type == 3)
            {
                GetLoadAreaPos();
                GetAreaMatrix1Pos();
                GetAreaMatrix2Pos();
            }

            ignore_value_change = false;
            return;
            //broken
            //Vector3 pos = new Vector3(), rot = new Vector3(), sca = new Vector3();
            //float w = 1;
            //Matrix4 m = new Matrix4(
            //    link.ObjectMatrix[0].X, link.ObjectMatrix[1].X, link.ObjectMatrix[2].X, link.ObjectMatrix[3].X,
            //    link.ObjectMatrix[0].Y, link.ObjectMatrix[1].Y, link.ObjectMatrix[2].Y, link.ObjectMatrix[3].Y,
            //    link.ObjectMatrix[0].Z, link.ObjectMatrix[1].Z, link.ObjectMatrix[2].Z, link.ObjectMatrix[3].Z,
            //    link.ObjectMatrix[0].W, link.ObjectMatrix[1].W, link.ObjectMatrix[2].W, link.ObjectMatrix[3].W
            //    );
            //MatrixWrapper.DecomposeMatrix(ref m, ref pos, ref rot, ref sca, ref w);
            ////no touchy
            //numericUpDown4.Value = (decimal)w;

            ////position
            //numericUpDown1.Value = (decimal)pos.X;
            //numericUpDown2.Value = (decimal)pos.Y;
            //numericUpDown3.Value = (decimal)pos.Z;

            ////scale
            //numericUpDown10.Value = (decimal)sca.X;
            //numericUpDown9.Value = (decimal)sca.Y;
            //numericUpDown8.Value = (decimal)sca.Z;

            ////rotation
            //numericUpDown7.Value = (decimal)rot.X;
            //numericUpDown6.Value = (decimal)rot.Y;
            //numericUpDown5.Value = (decimal)rot.Z;

            //m = new Matrix4(link.ChunkMatrix[0].X, link.ChunkMatrix[1].X, link.ChunkMatrix[2].X, link.ChunkMatrix[3].X,
            //    link.ChunkMatrix[0].Y, link.ChunkMatrix[1].Y, link.ChunkMatrix[2].Y, link.ChunkMatrix[3].Y,
            //    link.ChunkMatrix[0].Z, link.ChunkMatrix[1].Z, link.ChunkMatrix[2].Z, link.ChunkMatrix[3].Z,
            //    link.ChunkMatrix[0].W, link.ChunkMatrix[1].W, link.ChunkMatrix[2].W, link.ChunkMatrix[3].W
            //    );
            //MatrixWrapper.DecomposeMatrix(ref m, ref pos, ref rot, ref sca, ref w);
            ////no touchy
            //numericUpDown17.Value = (decimal)w;

            ////position
            //numericUpDown20.Value = (decimal)pos.X;
            //numericUpDown19.Value = (decimal)pos.Y;
            //numericUpDown18.Value = (decimal)pos.Z;

            ////scale
            //numericUpDown13.Value = (decimal)sca.X;
            //numericUpDown12.Value = (decimal)sca.Y;
            //numericUpDown11.Value = (decimal)sca.Z;

            ////rotation
            //numericUpDown16.Value = (decimal)rot.X;
            //numericUpDown15.Value = (decimal)rot.Y;
            //numericUpDown14.Value = (decimal)rot.Z;
        }

        private void GetLoadWallPos()
        {
            numericUpDown27.Value = (decimal)link.LoadWall[pos_i].X;
            numericUpDown26.Value = (decimal)link.LoadWall[pos_i].Y;
            numericUpDown25.Value = (decimal)link.LoadWall[pos_i].Z;
            numericUpDown24.Value = (decimal)link.LoadWall[pos_i].W;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pos_i <= 0)
                return;
            label21.Text = $"{pos_i--} / 4";
            GetLoadWallPos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pos_i >= 3)
                return;
            label21.Text = $"{++pos_i+1} / 4";
            GetLoadWallPos();
        }

        private void GetLoadAreaPos()
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                numericUpDown21.Value = (decimal)Link.LoadArea[areap_i].X;
                numericUpDown28.Value = (decimal)Link.LoadArea[areap_i].Y;
                numericUpDown23.Value = (decimal)Link.LoadArea[areap_i].Z;
                numericUpDown22.Value = (decimal)Link.LoadArea[areap_i].W;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (areap_i <= 0)
                return;
            label22.Text = $"{areap_i--} / 8";
            GetLoadAreaPos();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (u1_i <= 0)
                return;
            label27.Text = $"{u1_i--} / 6";
            GetAreaMatrix1Pos();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (u1_i >= 5)
                return;
            label27.Text = $"{++u1_i + 1} / 6";
            GetAreaMatrix1Pos();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (u2_i <= 0)
                return;
            label32.Text = $"{u2_i--} / 6";
            GetAreaMatrix2Pos();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (u2_i >= 5)
                return;
            label32.Text = $"{++u2_i + 1} / 6";
            GetAreaMatrix2Pos();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdateObjectMatrix();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (areap_i >= 7)
                return;
            label22.Text = $"{++areap_i + 1} / 8";
            GetLoadAreaPos();
        }

        private void GetAreaMatrix1Pos()
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                numericUpDown29.Value = (decimal)Link.AreaMatrix[u1_i].X;
                numericUpDown32.Value = (decimal)Link.AreaMatrix[u1_i].Y;
                numericUpDown31.Value = (decimal)Link.AreaMatrix[u1_i].Z;
                numericUpDown30.Value = (decimal)Link.AreaMatrix[u1_i].W;
            }
        }

        private void numericUpDown20_ValueChanged(object sender, EventArgs e)
        {
            UpdateChunkMatrix();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("<new link>");
            ChunkLinks.ChunkLink link = new ChunkLinks.ChunkLink(0, "<new link>", 0x80102);
            controller.Data.Links.Add(link);
            controller.UpdateText();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sel_i = listBox1.SelectedIndex;
            if (sel_i == -1)
                return;
            controller.Data.Links.RemoveAt(sel_i);
            listBox1.Items.RemoveAt(sel_i);
            if (sel_i >= listBox1.Items.Count) sel_i = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = sel_i;
            if (listBox1.Items.Count == 0)
                groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = groupBox6.Enabled = groupBox9.Enabled = groupBox8.Enabled = groupBox7.Enabled = false;
            controller.UpdateText();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ignore_value_change) return;
            link.Path = textBox1.Text;
            controller.Data.Links[listBox1.SelectedIndex] = link;
            listBox1.Items[listBox1.SelectedIndex] = textBox1.Text;
            controller.UpdateText();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore_value_change) return;
            link.Type = comboBox1.SelectedIndex;
            controller.Data.Links[listBox1.SelectedIndex] = link;
            if (groupBox9.Enabled = groupBox8.Enabled = groupBox7.Enabled = link.Type == 1 || link.Type == 3)
            {
                if (link.TreeRoot == null) link.TreeRoot = new ChunkLinks.ChunkLink.LinkTree();
                GetLoadAreaPos();
                GetAreaMatrix1Pos();
                GetAreaMatrix2Pos();
            }
            else
            {
                if (link.TreeRoot != null) link.TreeRoot = null;
            }
            controller.UpdateText();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            uint o;
            if (uint.TryParse(textBox2.Text, System.Globalization.NumberStyles.HexNumber, null, out o))
                link.Flags = o;
            controller.Data.Links[listBox1.SelectedIndex] = link;
            if (groupBox6.Enabled = (link.Flags & 0x80000) != 0)
            {
                GetLoadWallPos();
            }
            controller.UpdateText();
        }

        private void numericUpDown27_ValueChanged(object sender, EventArgs e)
        {
            Pos pos = link.LoadWall[pos_i];
            pos.X = (float)numericUpDown27.Value;
            controller.Data.Links[listBox1.SelectedIndex].LoadWall[pos_i] = pos;
        }

        private void numericUpDown26_ValueChanged(object sender, EventArgs e)
        {
            Pos pos = link.LoadWall[pos_i];
            pos.Y = (float)numericUpDown26.Value;
            controller.Data.Links[listBox1.SelectedIndex].LoadWall[pos_i] = pos;
        }

        private void numericUpDown25_ValueChanged(object sender, EventArgs e)
        {
            Pos pos = link.LoadWall[pos_i];
            pos.Z = (float)numericUpDown25.Value;
            controller.Data.Links[listBox1.SelectedIndex].LoadWall[pos_i] = pos;
        }

        private void numericUpDown23_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.LoadArea[areap_i];
                pos.Z = (float)numericUpDown23.Value;
                Link.LoadArea[areap_i] = pos;
            }
        }

        private void numericUpDown28_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.LoadArea[areap_i];
                pos.Y = (float)numericUpDown28.Value;
                Link.LoadArea[areap_i] = pos;
            }
        }

        private void numericUpDown21_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.LoadArea[areap_i];
                pos.X = (float)numericUpDown21.Value;
                Link.LoadArea[areap_i] = pos;
            }
        }

        private void numericUpDown29_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.AreaMatrix[u1_i];
                pos.X = (float)numericUpDown29.Value;
                Link.AreaMatrix[u1_i] = pos;
            }
        }

        private void numericUpDown32_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.AreaMatrix[u1_i];
                pos.Y = (float)numericUpDown32.Value;
                Link.AreaMatrix[u1_i] = pos;
            }
        }

        private void numericUpDown31_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.AreaMatrix[u1_i];
                pos.Z = (float)numericUpDown31.Value;
                Link.AreaMatrix[u1_i] = pos;
            }
        }

        private void numericUpDown30_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.AreaMatrix[u1_i];
                pos.W = (float)numericUpDown30.Value;
                Link.AreaMatrix[u1_i] = pos;
            }
        }

        private void numericUpDown33_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.UnknownMatrix[u2_i];
                pos.X = (float)numericUpDown33.Value;
                Link.UnknownMatrix[u2_i] = pos;
            }
        }

        private void numericUpDown36_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.UnknownMatrix[u2_i];
                pos.Y = (float)numericUpDown36.Value;
                Link.UnknownMatrix[u2_i] = pos;
            }
        }

        private void numericUpDown35_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.UnknownMatrix[u2_i];
                pos.Z = (float)numericUpDown35.Value;
                Link.UnknownMatrix[u2_i] = pos;
            }
        }

        private void numericUpDown24_ValueChanged(object sender, EventArgs e)
        {
            Pos pos = link.LoadWall[pos_i];
            pos.W = (float)numericUpDown24.Value;
            controller.Data.Links[listBox1.SelectedIndex].LoadWall[pos_i] = pos;
        }

        private void numericUpDown22_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.LoadArea[areap_i];
                pos.W = (float)numericUpDown22.Value;
                Link.LoadArea[areap_i] = pos;
            }
        }

        private void numericUpDown34_ValueChanged(object sender, EventArgs e)
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                Pos pos = Link.UnknownMatrix[u2_i];
                pos.W = (float)numericUpDown34.Value;
                Link.UnknownMatrix[u2_i] = pos;
            }
        }

        private void GetAreaMatrix2Pos()
        {
            if (link.TreeRoot != null)
            {
                ChunkLinks.ChunkLink.LinkTree Link = (ChunkLinks.ChunkLink.LinkTree)link.TreeRoot;
                numericUpDown33.Value = (decimal)Link.UnknownMatrix[u2_i].X;
                numericUpDown36.Value = (decimal)Link.UnknownMatrix[u2_i].Y;
                numericUpDown35.Value = (decimal)Link.UnknownMatrix[u2_i].Z;
                numericUpDown34.Value = (decimal)Link.UnknownMatrix[u2_i].W;
            }
        }

        private void UpdateObjectMatrix()
        {
            if (ignore_value_change) return;
            
            link.ObjectMatrix[3].W = (float)numericUpDown4.Value;

            link.ObjectMatrix[3].X = (float)numericUpDown1.Value;
            link.ObjectMatrix[3].Y = (float)numericUpDown2.Value;
            link.ObjectMatrix[3].Z = (float)numericUpDown3.Value;

            link.ObjectMatrix[0].X = (float)numericUpDown7.Value;
            link.ObjectMatrix[0].Y = (float)numericUpDown6.Value;
            link.ObjectMatrix[0].Z = (float)numericUpDown5.Value;

            link.ObjectMatrix[1].X = (float)numericUpDown10.Value;
            link.ObjectMatrix[1].Y = (float)numericUpDown9.Value;
            link.ObjectMatrix[1].Z = (float)numericUpDown8.Value;

            link.ObjectMatrix[2].X = (float)numericUpDown39.Value;
            link.ObjectMatrix[2].Y = (float)numericUpDown38.Value;
            link.ObjectMatrix[2].Z = (float)numericUpDown37.Value;

            controller.Data.Links[listBox1.SelectedIndex] = link;
            return;
            //broken
            //Matrix4 rot_mat = MatrixWrapper.RotateMatrix4((float)numericUpDown7.Value, (float)numericUpDown6.Value, (float)numericUpDown5.Value);
            //Matrix4 sca_mat = Matrix4.CreateScale((float)numericUpDown10.Value, (float)numericUpDown9.Value, (float)numericUpDown8.Value);
            //Matrix4 pos_mat = Matrix4.CreateTranslation((float)numericUpDown1.Value, (float)numericUpDown2.Value, (float)numericUpDown3.Value);
            //Matrix4 new_mat = pos_mat * sca_mat * rot_mat;

            //link.ObjectMatrix[0] = new_mat.Row0.ToPos();
            //link.ObjectMatrix[1] = new_mat.Row1.ToPos();
            //link.ObjectMatrix[2] = new_mat.Row2.ToPos();
            //link.ObjectMatrix[3] = new_mat.Row3.ToPos();

            //listBox1_SelectedIndexChanged(null, null);
        }

        private void UpdateChunkMatrix()
        {
            if (ignore_value_change) return;
            
            link.ChunkMatrix[3].W = (float)numericUpDown20.Value;

            link.ChunkMatrix[3].X = (float)numericUpDown42.Value;
            link.ChunkMatrix[3].Y = (float)numericUpDown41.Value;
            link.ChunkMatrix[3].Z = (float)numericUpDown40.Value;

            link.ChunkMatrix[0].X = (float)numericUpDown19.Value;
            link.ChunkMatrix[0].Y = (float)numericUpDown18.Value;
            link.ChunkMatrix[0].Z = (float)numericUpDown17.Value;

            link.ChunkMatrix[1].X = (float)numericUpDown16.Value;
            link.ChunkMatrix[1].Y = (float)numericUpDown15.Value;
            link.ChunkMatrix[1].Z = (float)numericUpDown14.Value;

            link.ChunkMatrix[2].X = (float)numericUpDown13.Value;
            link.ChunkMatrix[2].Y = (float)numericUpDown12.Value;
            link.ChunkMatrix[2].Z = (float)numericUpDown11.Value;

            controller.Data.Links[listBox1.SelectedIndex] = link;
            return;
        }
    }
}
