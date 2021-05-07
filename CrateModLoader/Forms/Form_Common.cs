using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CrateModLoader.Forms
{
    public class Controller
    {
        public TreeNode Node { get; set; }

        public Controller()
        {
            Node = new TreeNode { Tag = this, };
        }
        public Controller(string name)
        {
            Node = new TreeNode { Text = name, Tag = this, };
        }
    }

    public class DirNode : Controller
    {
        public DirectoryInfo Dir;
        public bool isNew = false;

        public DirNode() : base()
        {
            Node.ImageKey = "folder";
            Node.SelectedImageKey = "folder";
        }
        public DirNode(string name, DirectoryInfo dir) : base(name)
        {
            Dir = dir;
            Node.ImageKey = "folder";
            Node.SelectedImageKey = "folder";
        }

        public void NewFolder()
        {
            Node.ImageKey = "file-add";
            Node.SelectedImageKey = "file-add";
            isNew = true;
        }
    }

    public class FileNode : Controller
    {
        public FileInfo File;
        public bool isReplaced = false;
        public string ExternalPath = string.Empty;
        private string origImage = string.Empty;

        internal static string iText = "file-text";
        internal static string iCode = "file-code";
        internal static string iImage = "photo";
        internal static string iWorld = "world";
        internal static string iMusic = "file-music";

        public static Dictionary<string, string> ExtIcons = new Dictionary<string, string>()
        {
            [".TXT"] = iText,
            [".XML"] = iCode,
            [".PNG"] = iImage,
            [".PSM"] = iImage,
            [".PSF"] = iImage,
            [".BMP"] = iImage,
            [".LEV"] = iWorld,
            [".RM2"] = iWorld,
            [".SM2"] = iWorld,
            [".RMX"] = iWorld,
            [".SMX"] = iWorld,
            [".RM"] = iWorld,
            [".SM"] = iWorld,
            [".MB"] = iMusic,
            [".MH"] = iMusic,
            [".WAV"] = iMusic,
            [".WMA"] = iMusic,
            [".NSF"] = iWorld,
            [".NSD"] = iWorld,
            [".GOD"] = iCode,
            [".RSD"] = iMusic,
        };

        public FileNode() : base() { }
        public FileNode(string name, FileInfo file) : base(name)
        {
            File = file;
            string ext = Path.GetExtension(name).ToUpper();
            if (!ExtIcons.ContainsKey(ext))
            {
                Node.ImageKey = "file";
                Node.SelectedImageKey = "file";
            }
            else
            {
                Node.ImageKey = ExtIcons[ext];
                Node.SelectedImageKey = ExtIcons[ext];
            }
        }

        public void Replace(string path)
        {
            origImage = Node.ImageKey;
            Node.ImageKey = "file-diff";
            Node.SelectedImageKey = "file-diff";
            isReplaced = true;
            ExternalPath = path;
            Node.Text = File.Name;
        }

        public void NewFile(string path)
        {
            Node.ImageKey = "file-plus";
            Node.SelectedImageKey = "file-plus";
            isReplaced = true;
            ExternalPath = path;
        }

        public void Restore()
        {
            if (isReplaced)
            {
                isReplaced = false;
                ExternalPath = string.Empty;
                Node.ImageKey = origImage;
                Node.SelectedImageKey = origImage;
            }
        }
    }
}
