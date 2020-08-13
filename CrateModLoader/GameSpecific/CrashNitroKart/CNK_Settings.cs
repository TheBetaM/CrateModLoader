using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    static class CNK_Settings
    {
        // to be deprecated in favor of generic ModProperty parsing
        public static void ParseSettings(string path_gob_extracted)
        {
            // example setting
            if (ModCrates.HasSetting("kart_AccelerationGainNormal"))
            {
                CNK_Data_KartStats.m_AccelerationGainNormal.Value = ModCrates.GetFloatSetting("kart_AccelerationGainNormal");

                string[] csv_kartphysicsbase = File.ReadAllLines(path_gob_extracted + "common/physics/kpbase.csv");

                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AccelerationGainNormal] = Modder_CNK.Float_To_CSV_Line(CNK_Data_KartStats.m_AccelerationGainNormal.Value);

                File.WriteAllLines(path_gob_extracted + "common/physics/kpbase.csv", csv_kartphysicsbase);
            }
        }

    }
}
