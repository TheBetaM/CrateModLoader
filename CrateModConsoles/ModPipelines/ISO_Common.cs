using System.IO;
using System.Collections.Generic;
using DiscUtils.Iso9660;

namespace CrateModLoader.Tools.ISO
{
    static class ISO_Common
    {

        public static void Recursive_AddDirs(CDBuilder isoBuild, DirectoryInfo di, string sName, HashSet<FileStream> files, bool extendedName)
        {
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                isoBuild.AddDirectory(sName + dir.Name);
                Recursive_AddDirs(isoBuild, dir, sName + dir.Name + @"\", files, extendedName);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                AddFile(isoBuild, file, sName, files, extendedName);
            }
        }

        public static void AddFile(CDBuilder isoBuild, FileInfo file, string sName, HashSet<FileStream> files, bool extendedName)
        {
            var fstream = file.Open(FileMode.Open);
            if (extendedName)
            {
                isoBuild.AddFile(sName + file.Name + ";1", fstream);
            }
            else
            {
                isoBuild.AddFile(sName + file.Name, fstream);
            }
            files.Add(fstream);
        }

    }
}
