using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class GOD_File
    {
        public string Name;
        public string FullName;
        public List<LUA_Object> Objects;

        public GOD_File(string file)
        {
            List<string> lines = new List<string>(File.ReadAllLines(file));
            Name = Path.GetFileName(file);
            FullName = file;
            Objects = new List<LUA_Object>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("BeginObject"))
                {
                    int EndPos = -1;
                    for (int x = i + 1; x < lines.Count; x++)
                    {
                        if (lines[x].StartsWith("EndObject"))
                        {
                            EndPos = x;
                            break;
                        }
                    }
                    if (EndPos == -1)
                        throw new Exception("End of object not found: " + FullName);

                    string TypeName = lines[i].Split(' ')[1];
                    string ObjectName = lines[i].Split(' ')[2];
                    LUA_Object newObject = new LUA_Object(TypeName, ObjectName);
                    for (int x = i + 1; x < EndPos; x++)
                    {
                        newObject.Content.Add(lines[x]);
                    }

                    Objects.Add(newObject);
                    i = EndPos;
                }
            }
            
        }

        public LUA_Object GetObject(string typeName, string objName)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].TypeName == typeName && Objects[i].ObjectName == objName)
                {
                    return Objects[i];
                }
            }
            return null;
        }
        public List<LUA_Object> GetObjects(string typeName)
        {
            List<LUA_Object> objs = new List<LUA_Object>();
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].TypeName == typeName)
                {
                   objs.Add(Objects[i]);
                }
            }
            return objs;
        }

        public void Write(string path = "")
        {
            List<string> lines = new List<string>();

            foreach (LUA_Object obj in Objects)
            {
                lines.Add("BeginObject " + obj.TypeName + " " + obj.ObjectName);
                foreach (string line in obj.Content)
                {
                    lines.Add(line);
                }
                lines.Add("EndObject");
            }

            if (path != "")
            {
                File.WriteAllLines(path, lines);
            }
            else
            {
                File.WriteAllLines(FullName, lines);
            }
        }
    }

    public class LUA_Object
    {
        public string TypeName;
        public string ObjectName;
        public List<string> Content;

        public LUA_Object(string t, string o)
        {
            TypeName = t;
            ObjectName = o;
            Content = new List<string>();
        }
    }
}
