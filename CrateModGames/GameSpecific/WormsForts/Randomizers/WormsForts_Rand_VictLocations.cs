using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_Rand_VictLocations : ModStruct<XOM_File>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(XOM_File file)
        {
            string level = Path.GetFileName(file.FileName);
            if (WormsForts_Common.Levels_Tutorial.Contains(level)) return;
            if (WormsForts_Common.Levels_Campaign.Contains(level)) return;

            XFortsExportedData cont = file.GetContainer<XFortsExportedData>();
            if (cont == null) return;

            uint VictoryCount = 0;
            List<int> RandList = new List<int>();
            for (int v = 0; v < cont.BuildPoints.Count; v++)
            {
                RandList.Add(v);
                if (cont.BuildPoints[v].VictoryLocation.Value)
                {
                    VictoryCount++;
                    cont.BuildPoints[v].VictoryLocation.Value = false;
                }
            }

            List<int> ToRand = new List<int>();
            while (ToRand.Count < VictoryCount)
            {
                int r = rand.Next(RandList.Count);
                ToRand.Add(RandList[r]);
                RandList.RemoveAt(r);
            }

            for (int v = 0; v < ToRand.Count; v++)
            {
                cont.BuildPoints[ToRand[v]].VictoryLocation.Value = true;
            }
        }
    }
}
