using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv.Mods
{
    public class SMBA_Metadata : ModStruct<GenericModStruct>
    {

        private List<string> Files = new List<string>()
        {
            "English.txt",
            "American.txt",
        };

        public override void ModPass(GenericModStruct mod)
        {
            string bdPath = SMBA_Common.GetDataPath(mod);

            List<string> CodeText_LineList = new List<string>();
            
            foreach (string file in Files)
            {
                string path = bdPath + "/Language/Code/" + file;
                if (File.Exists(path))
                {
                    CodeText_LineList = new List<string>(File.ReadAllLines(path, Encoding.Default));

                    for (int i = 0; i < CodeText_LineList.Count; i++)
                    {
                        if (CodeText_LineList[i].StartsWith("TRAVEL WITH THE SUPER~MONKEY BALL TEAM"))
                        {
                            CodeText_LineList[i] = "CRATE MOD LOADER " + ModLoaderGlobals.ProgramVersion.ToUpper() + "~" + "SEED: " + ModLoaderGlobals.RandomizerSeed + "";
                        }
                    }

                    File.WriteAllLines(path, CodeText_LineList.ToArray(), Encoding.Default);
                }
            }

        }
    }
}
