using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_SurfaceParams : ModStruct<CSV>
    {
        private Random randState;

        public override void BeforeModPass()
        {
            randState = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(CSV file)
        {
            if (file.Name.ToLower() == "surfparm.csv")
            {
                List<List<string>> table = file.Table;
                for (int i = 4; i < CNK_Data_Surfaces.Surface_m_MinSpeedPercent.Value.Length + 4; i++)
                {
                    table[i][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_MinSpeedPercent.Value[i - 4]);
                    table[i][1] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_SlowDownLongPercent.Value[i - 4]);
                    table[i][2] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_SlowDownAccelPercent.Value[i - 4]);
                    table[i][3] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_SlowDownBoostPercent.Value[i - 4]);
                    table[i][4] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_SpeedBoostIncreasePercent.Value[i - 4]);
                    table[i][5] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_BrakeLossPercent.Value[i - 4]);
                    table[i][6] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_LatFrictionLossPercent.Value[i - 4]);
                    table[i][7] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_LongFrictionLossPercent.Value[i - 4]);
                    table[i][8] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_SlideFrictionLossPercent.Value[i - 4]);
                    table[i][9] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_SpeedAccelIncreasePercent.Value[i - 4]);
                    table[i][10] = CNK_Common.Float_To_CSV_Word(CNK_Data_Surfaces.Surface_m_KartHeightOffset.Value[i - 4]);
                }
            }

        }

    }
}
