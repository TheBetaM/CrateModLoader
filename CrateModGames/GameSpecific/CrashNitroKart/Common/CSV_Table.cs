using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CSV
    {
        static char separator = ',';
        static char comment = '#';
        public List<List<string>> Table;
        public string Name;
        public string FullName;

        public CSV(FileInfo file)
        {
            string[] csv_lines = File.ReadAllLines(file.FullName);
            Name = file.Name;
            FullName = file.FullName;
            Table = new List<List<string>>();

            foreach (string line in csv_lines)
            {
                string[] split = line.Split(separator);
                Table.Add(new List<string>(split));
            }
        }

        public void Write(string path = "")
        {
            List<string> csv_lines = new List<string>();

            foreach(List<string> line in Table)
            {
                string ThisLine = "";
                for (int i = 0; i < line.Count; i++)
                {
                    ThisLine += line[i];
                    if (i != line.Count - 1)
                    {
                        ThisLine += separator;
                    }
                }
                csv_lines.Add(ThisLine);
            }

            if (path != "")
            {
                File.WriteAllLines(path, csv_lines);
            }
            else
            {
                File.WriteAllLines(FullName, csv_lines);
            }
        }
    }
}
