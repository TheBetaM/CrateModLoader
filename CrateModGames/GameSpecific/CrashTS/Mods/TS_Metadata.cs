using System;
using System.Collections.Generic;
using Twinsanity;
using System.IO;
using System.Text;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    public class TS_Metadata : ModStruct<ExecutableInfo>
    {

        public override void ModPass(ExecutableInfo info)
        {
            string bdPath = info.BDpath;
            bool isNTSC = info.Index == ExecutableIndex.NTSCU || info.Index == ExecutableIndex.NTSCU2 || info.Index == ExecutableIndex.XBOX_NTSC;
            bool isPAL = info.Index == ExecutableIndex.PAL || info.Index == ExecutableIndex.XBOX_PAL;

            string[] CodeText;
            if (isNTSC)
            {
                CodeText = File.ReadAllLines(bdPath + "/Language/Code/American.txt", Encoding.Default);
            }
            else if (isPAL)
            {
                CodeText = File.ReadAllLines(bdPath + "/Language/Code/English.txt", Encoding.Default);
            }
            else
            {
                CodeText = File.ReadAllLines(bdPath + "/Language/Code/Japanese.txt", Encoding.Default);
            }

            List<string> CodeText_LineList = new List<string>();
            for (int i = 0; i < CodeText.Length; i++)
            {
                CodeText_LineList.Add(CodeText[i]);
            }

            for (int i = 0; i < CodeText_LineList.Count; i++)
            {
                if (CodeText_LineList[i] == "to enable autosave,~return to the pause menu~and re-save the game.")
                {
                    CodeText_LineList[i] = "to enable autosave,~return to the pause menu~and re-save the game.~crate mod loader " + ModLoaderGlobals.ProgramVersion.ToLower() + "~" + "seed: " + ModLoaderGlobals.RandomizerSeed + "";
                }
                else if (CodeText_LineList[i] == "autosave disabled")
                {
                    CodeText_LineList[i] = "autosave disabled~";
                }
                else if (i == 39 && !isNTSC && !isPAL)
                {
                    CodeText_LineList[i] += "~";
                }
                else if (i == 40 && !isNTSC && !isPAL)
                {
                    CodeText_LineList[i] += "~" + ModLoaderGlobals.ProgramVersion.ToLower() + "~" + "" + ModLoaderGlobals.RandomizerSeed + "";
                }
            }

            CodeText = new string[CodeText_LineList.Count];
            for (int i = 0; i < CodeText_LineList.Count; i++)
            {
                CodeText[i] = CodeText_LineList[i];
            }

            if (isNTSC)
            {
                File.WriteAllLines(bdPath + "/Language/Code/American.txt", CodeText, Encoding.Default);
            }
            else if (isPAL)
            {
                File.WriteAllLines(bdPath + "/Language/Code/English.txt", CodeText, Encoding.Default);
            }
            else
            {
                File.WriteAllLines(bdPath + "/Language/Code/Japanese.txt", CodeText, Encoding.Default);
            }
        }
    }
}
