using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    static class CNK_Settings
    {

        public static void ParseSettings(string path_gob_extracted)
        {
            // example setting
            if (ModCrates.HasSetting("kart_AccelerationGainNormal"))
            {
                CNK_Data.m_AccelerationGainNormal = ModCrates.GetFloatSetting("kart_AccelerationGainNormal");

                string[] csv_kartphysicsbase = File.ReadAllLines(path_gob_extracted + "common/physics/kpbase.csv");

                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AccelerationGainNormal] = Modder_CNK.Float_To_CSV_Line(CNK_Data.m_AccelerationGainNormal);

                File.WriteAllLines(path_gob_extracted + "common/physics/kpbase.csv", csv_kartphysicsbase);
            }
        }

    }
}
