using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Twinsanity;

namespace TwinsaityEditor.Workers
{
    public partial class TextureImport : Form
    {
        private ImageList _images = new ImageList();
        private Dictionary<string, string> _imagePaths = new Dictionary<string, string>();

        public TextureImport()
        {
            InitializeComponent();

            //Configure formats box
            cbFormats.SelectedIndex = 0;

            //Configure image listview
            _images.ImageSize = new Size(64, 64);
            _images.ColorDepth = ColorDepth.Depth32Bit;
            listImages.LargeImageList = _images;
            listImages.View = View.LargeIcon;

            pbImport.Step = 1;

            //Configure the texture import frame
            Layout += TextureImport_Layout;
            _prevSize = Size;
            MinimumSize = new Size(Size.Width, Size.Height);
        }

        //Adjust window layout
        private Size _prevSize;
        private void TextureImport_Layout(object sender, LayoutEventArgs e)
        {
            var wid_change = Size.Width - _prevSize.Width;
            var hei_change = Size.Height - _prevSize.Height;

            listImages.Width += wid_change;
            listImages.Height += hei_change;

            btnSelector.Top += hei_change;

            btnImport.Top += hei_change;
            btnImport.Left = listImages.Right - btnImport.Width;

            lbImageList.Left = listImages.Width / 2 - lbImageList.Width / 2;

            pbImport.Top += hei_change;
            pbImport.Width = listImages.Width;

            _prevSize = Size;
        }

        private void btnSelector_Click(object sender, EventArgs e)
        {
            if (ofdSelect.ShowDialog() == DialogResult.OK)
            {
                _imagePaths.Clear();
                _images.Images.Clear();
                listImages.Clear();

                foreach (var imageName in ofdSelect.FileNames)
                {
                    var formatStr = System.IO.Path.GetFileName(imageName);

                    _imagePaths.Add(formatStr, imageName);

                    _images.Images.Add(formatStr, Image.FromFile(imageName));

                    listImages.Items.Add(new ListViewItem(formatStr, formatStr));
                }

                pbImport.Value = 0;
                pbImport.Maximum = listImages.Items.Count;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Texture importing is currently not implemented.");
            if (listImages.Items.Count == 0)
            {
                MessageBox.Show("No images to import were selected!", "Import error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var fdbSave = new CommonOpenFileDialog();
                fdbSave.IsFolderPicker = true;

                pbImport.Show();
                if (fdbSave.ShowDialog(Handle) == CommonFileDialogResult.Ok)
                {
                    foreach (var image in listImages.Items)
                    {
                        ListViewItem item = (ListViewItem)image;
                        Bitmap bitmap = new Bitmap(_imagePaths[item.Text]);
                        byte[] rawData;

                        Texture tex = new Texture();
                        uint width = uint.Parse(cbFormats.Text.Split('x')[0]);
                        uint height = uint.Parse(cbFormats.Text.Split('x')[1].Split(' ')[0]);
                        bool mip = cbFormats.Text.Split('x')[1].Split(' ')[1].Equals("mip");

                        if (width > bitmap.Width || height > bitmap.Height)
                        {
                            MessageBox.Show("Attempting to import an image to a bigger size!", "Import error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        rawData = new byte[Math.Abs(data.Stride) * bitmap.Height];
                        
                        /*for (var i = 0; i < rawData.Length; ++i)
                        {
                            rawData[i] = bitmap.GetPixel(i % bitmap.Width, i / bitmap.Width);
                            pbRawData.PerformStep();
                        }*/
                        System.Runtime.InteropServices.Marshal.Copy(data.Scan0, rawData, 0, rawData.Length);


                        //Texture.BlockFormats bf = (Texture.BlockFormats)cbFormats.SelectedIndex;

                        /*BackgroundWorker worker = new BackgroundWorker();
                        worker.DoWork += (send,ev) => 
                        {
                            tex.Import(rawData, width, height, bf, mip);

                            //Save the image
                            FileStream file = new FileStream(fdbSave.FileName}\\{item.Text.Remove(item.Text.Length - 4, 4), FileMode.Create, FileAccess.Write);
                            BinaryWriter writer = new BinaryWriter(file);
                            writer.Write(tex.ByteStream.ToArray());

                            writer.Close();
                            file.Close();
                            writer.Dispose();
                            file.Dispose();
                        };*/

                        //tex.Import(rawData, width, height, bf, mip);
                        
                        //Save the image
                        FileStream file = new FileStream($"{fdbSave.FileName}\\{item.Text.Remove(item.Text.Length - 4, 4)}", FileMode.Create, FileAccess.Write);
                        BinaryWriter writer = new BinaryWriter(file);
                        //writer.Write(tex.Data);

                        writer.Close();
                        file.Close();
                        writer.Dispose();
                        file.Dispose();

                        //worker.RunWorkerAsync();


                        pbImport.PerformStep();

                        bitmap.UnlockBits(data);
                        //Clean up
                        bitmap.Dispose();
                    }

                    MessageBox.Show("Import finished! Imported {listImages.Items.Count.ToString()} images.", "Import finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                pbImport.Hide();
            }
        }
    }
}
