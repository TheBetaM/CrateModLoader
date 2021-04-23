using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_SurfaceParams : ModStruct<string>
    {
        public override string Name => "Randomize Surface Parameters";

        public override void BeforeModPass()
        {

        }

        public override void ModPass(string path_gob_extracted)
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);

            string[] csv_surfparams = File.ReadAllLines(path_gob_extracted + "common/physics/surfparm.csv");

            List<string> csv_surfparams_LineList = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                csv_surfparams_LineList.Add(csv_surfparams[i]);
            }

            string line_m_MinSpeedPercent = "";
            string line_m_SlowDownLongPercent = "";
            string line_m_SlowDownAccelPercent = "";
            string line_m_SlowDownBoostPercent = "";
            string line_m_SpeedBoostIncreasePercent = "";
            string line_m_BrakeLossPercent = "";
            string line_m_LatFrictionLossPercent = "";
            string line_m_LongFrictionLossPercent = "";
            string line_m_SlideFrictionLossPercent = "";
            string line_m_SpeedAccelIncreasePercent = "";
            string line_m_KartHeightOffset = "";

            string cur_line = "";
            for (int i = 0; i < CNK_Data_Surfaces.Surface_m_MinSpeedPercent.Value.Length; i++)
            {
                line_m_MinSpeedPercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_MinSpeedPercent.Value[i]);
                line_m_SlowDownLongPercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SlowDownLongPercent.Value[i]);
                line_m_SlowDownAccelPercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SlowDownAccelPercent.Value[i]);
                line_m_SlowDownBoostPercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SlowDownBoostPercent.Value[i]);
                line_m_SpeedBoostIncreasePercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SpeedBoostIncreasePercent.Value[i]);
                line_m_BrakeLossPercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_BrakeLossPercent.Value[i]);
                line_m_LatFrictionLossPercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_LatFrictionLossPercent.Value[i]);
                line_m_LongFrictionLossPercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_LongFrictionLossPercent.Value[i]);
                line_m_SlideFrictionLossPercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SlideFrictionLossPercent.Value[i]);
                line_m_SpeedAccelIncreasePercent = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SpeedAccelIncreasePercent.Value[i]);
                line_m_KartHeightOffset = CNK_Common.Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_KartHeightOffset.Value[i]);

                cur_line += line_m_MinSpeedPercent;
                cur_line += line_m_SlowDownLongPercent;
                cur_line += line_m_SlowDownAccelPercent;
                cur_line += line_m_SlowDownBoostPercent;
                cur_line += line_m_SpeedBoostIncreasePercent;
                cur_line += line_m_BrakeLossPercent;
                cur_line += line_m_LatFrictionLossPercent;
                cur_line += line_m_LongFrictionLossPercent;
                cur_line += line_m_SlideFrictionLossPercent;
                cur_line += line_m_SpeedAccelIncreasePercent;
                cur_line += line_m_KartHeightOffset;

                csv_surfparams_LineList.Add(cur_line);

                cur_line = "";
            }
            csv_surfparams_LineList.Add("");

            csv_surfparams = new string[csv_surfparams_LineList.Count];
            for (int i = 0; i < csv_surfparams_LineList.Count; i++)
            {
                csv_surfparams[i] = csv_surfparams_LineList[i];
            }

            File.WriteAllLines(path_gob_extracted + "common/physics/surfparm.csv", csv_surfparams);
        }

    }
}
