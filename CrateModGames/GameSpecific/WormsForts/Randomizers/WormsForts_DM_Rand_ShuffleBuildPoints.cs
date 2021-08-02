using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_DM_Rand_ShuffleBuildPoints : ModStruct<XOM_File>
    {
        private List<string> DM_Names = new List<string>()
        {
            "Text.Level.Deathmatch1",
            "Text.Level.Deathmatch2",
            "Text.Level.Deathmatch3",
            "Text.Level.Deathmatch4",
            "Text.Level.Deathmatch5",
            "Text.Level.Deathmatch6",
            "Text.Level.Deathmatch7",
            "Text.Level.Deathmatch8",
            "Text.Level.Deathmatch9",
            "Text.Level.Deathmatch10",
        };
        private List<string> DM_Campaign_Names = new List<string>()
        {
            "FE.Trials.1",
            "FE.Trials.2",
            "FE.Trials.3",
            "FE.Trials.4",
            "FE.Trials.5",
            "FE.Trials.6",
            "FE.Trials.7",
            "FE.Trials.8",
            "FE.Trials.9",
            "FE.Trials.10",
        };

        private Random rand;

        public WF_DM_Rand_ShuffleBuildPoints()
        {
            rand = GetRandom();
        }

        public override void ModPass(XOM_File file)
        {
            string level = Path.GetFileName(file.FileName);
            if (!WormsForts_Common.Levels_Deathmatch.Contains(level)) return;

            XFortsExportedData cont = file.GetContainer<XFortsExportedData>();
            if (cont == null) return;

            int BuildCount = cont.BuildPoints.Count;
            List<uint> BP_Values = new List<uint>();
            List<int> RandList = new List<int>();
            for (int v = 0; v < BuildCount; v++)
            {
                RandList.Add(v);
                BP_Values.Add(cont.BuildPoints[v].NameID.Value);
            }

            List<int> ToRand = new List<int>();
            while (ToRand.Count < BuildCount)
            {
                int r = rand.Next(RandList.Count);
                ToRand.Add(RandList[r]);
                RandList.RemoveAt(r);
            }

            for (int v = 0; v < ToRand.Count; v++)
            {
                cont.BuildPoints[v].NameID.Value = BP_Values[ToRand[v]];
            }
        }
    }
}
