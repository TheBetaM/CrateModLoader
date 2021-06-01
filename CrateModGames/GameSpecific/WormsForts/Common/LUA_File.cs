using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class LUA_File
    {
        public string Name;
        public string FullName;
        public List<LUA_Func> Functions;

        public LUA_File(string file)
        {
            List<string> lines = new List<string>(File.ReadAllLines(file));
            Name = Path.GetFileName(file);
            FullName = file;
            Functions = new List<LUA_Func>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].TrimStart(' ').TrimStart(' ').StartsWith("function"))
                {
                    int EndPos = -1;
                    int NextFuncPos = -1;
                    for (int x = i + 1; x < lines.Count; x++)
                    {
                        if (lines[x].TrimStart(' ').TrimStart(' ').StartsWith("function"))
                        {
                            NextFuncPos = x;
                            break;
                        }
                    }

                    if (NextFuncPos != -1)
                    {
                        for (int x = NextFuncPos - 1; x > i; x--)
                        {
                            if (lines[x].TrimStart(' ').TrimStart(' ').StartsWith("end"))
                            {
                                EndPos = x;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int x = lines.Count - 1; x > i; x--)
                        {
                            if (lines[x].TrimStart(' ').TrimStart(' ').StartsWith("end"))
                            {
                                EndPos = x;
                                break;
                            }
                        }
                    }

                    if (EndPos == -1)
                    {
                        Console.WriteLine("End of function not found:" + lines[i] + " IN: " + FullName);
                        throw new Exception("End of function not found: " + FullName);
                    }

                    string TypeName = lines[i];
                    LUA_Func newObject = new LUA_Func(TypeName);
                    for (int x = i + 1; x < EndPos; x++)
                    {
                        if (!string.IsNullOrWhiteSpace(lines[x])) //trim empty lines for easier navigation
                        {
                            newObject.Content.Add(lines[x]);
                        }
                    }

                    Functions.Add(newObject);
                    i = EndPos;
                }
            }

        }

        public LUA_Func GetFunction(string typeName)
        {
            for (int i = 0; i < Functions.Count; i++)
            {
                if (Functions[i].TypeName.Contains(typeName))
                {
                    return Functions[i];
                }
            }
            return null;
        }
        public List<LUA_Func> GetFunctions(string typeName)
        {
            List<LUA_Func> objs = new List<LUA_Func>();
            for (int i = 0; i < Functions.Count; i++)
            {
                if (Functions[i].TypeName.Contains(typeName))
                {
                    objs.Add(Functions[i]);
                }
            }
            return objs;
        }

        public void Write(string path = "")
        {
            List<string> lines = new List<string>();

            foreach (LUA_Func obj in Functions)
            {
                lines.Add(obj.TypeName);
                foreach (string line in obj.Content)
                {
                    lines.Add(line);
                }
                lines.Add("end");
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

    public class LUA_Func
    {
        public string TypeName;
        public List<string> Content;

        public LUA_Func(string t)
        {
            TypeName = t;
            Content = new List<string>();
        }
    }
}
