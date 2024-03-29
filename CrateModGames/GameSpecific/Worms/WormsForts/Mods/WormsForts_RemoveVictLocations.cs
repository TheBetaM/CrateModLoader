﻿using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_RemoveVictLocations : ModStruct<XOM_File>
    {
        public override void ModPass(XOM_File file)
        {
            string level = Path.GetFileName(file.FileName);
            if (WormsForts_Common.Levels_Tutorial.Contains(level)) return;
            if (WormsForts_Common.Levels_Campaign.Contains(level)) return;

            XFortsExportedData cont = file.GetContainer<XFortsExportedData>();
            if (cont == null) return;

            for (int v = 0; v < cont.BuildPoints.Count; v++)
            {
                if (cont.BuildPoints[v].VictoryLocation.Value)
                {
                    cont.BuildPoints[v].VictoryLocation.Value = false;
                }
            }
        }
    }
}
