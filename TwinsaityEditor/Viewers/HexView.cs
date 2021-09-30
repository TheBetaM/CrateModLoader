using System.Windows.Forms;
using Microsoft.VisualBasic;
using System;

namespace TwinsaityEditor
{
    public partial class HexView
    {
        public void LoadHEX(byte[] File, int endPos, int startPos)
        {
            int lines = (endPos - startPos - 1) / 16 + 1;
            string[] line = new string[lines - 1 + 1];
            uint Pos = (uint)startPos;
            for (int i = 0; i <= lines - 1; i++)
            {
                string str = Convert.ToString(i * 16, 16);
                while (str.Length < 8)
                    str = "0" + str;
                str += " | ";
                if (endPos - Pos >= 16)
                {
                    byte[] Bytes = new byte[16];
                    for (int a = 0; a <= 15; a++)
                    {
                        Bytes[a] = File[Pos];
                        Pos += 1;
                        string s = Convert.ToString(Bytes[a], 16);
                        if (s.Length < 2)
                            s = "0" + s;
                        if ((a + 1) % 4 == 0)
                            str += s + "  ";
                        else
                            str += s + " ";
                    }
                    str += "| ";
                    for (int a = 0; a <= 15; a++)
                    {
                        if (Bytes[a] >= 32)
                            str += Strings.Chr(Bytes[a]);
                        else
                            str += " ";
                    }
                }
                else
                {
                    byte l = (byte)(endPos - Pos);
                    byte[] Bytes = new byte[l - 1 + 1];
                    for (int a = 0; a <= endPos - Pos - 1; a++)
                    {
                        Bytes[a] = File[Pos];
                        Pos += 1;
                        string s = Convert.ToString(Bytes[a], 16);
                        if (s.Length < 2)
                            s = "0" + s;
                        if ((a + 1) % 4 == 0)
                            str += s + "  ";
                        else
                            str += s + " ";
                    }
                    for (int a = l; a <= 15; a++)
                    {
                        string s = "..";
                        if ((a + 1) % 4 == 0)
                            str += s + "  ";
                        else
                            str += s + " ";
                    }
                    str += "| ";
                    for (int a = 0; a <= l - 1; a++)
                    {
                        if (Bytes[a] >= 32)
                            str += Strings.Chr(Bytes[a]);
                        else
                            str += " ";
                    }
                }
                line[i] = str;
                Application.DoEvents();
            }
            HEX.Lines = line;
        }
    }
}
