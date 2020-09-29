using System.IO;

namespace CrateModLoader.Tools.IO
{
    public static class IO_Common
    {
        public static void Recursive_RenameFiles(DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    file.MoveTo(file.FullName.Substring(0, file.FullName.Length - 2));
                }
                Recursive_RenameFiles(dir);
            }
        }

        public static void Recursive_CopyFiles(DirectoryInfo di, string pathparent)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Directory.CreateDirectory(pathparent + dir.Name);
                string pathchild = pathparent + dir.Name + @"\";
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    file.CopyTo(pathchild + file.Name);
                }
                Recursive_CopyFiles(dir, pathchild);
            }
        }

        public static bool PathIsFolder(string path)
        {
            if (path.EndsWith(@"\") || path.EndsWith(@"/"))
            {
                return true;
            }
            return false;
        }
    }
}
