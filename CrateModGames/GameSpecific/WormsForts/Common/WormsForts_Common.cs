using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class ColorSwizzleData
    {
        public int r_r;
        public int r_g;
        public int r_b;
        public int r_s;
        public int g_r;
        public int g_g;
        public int g_b;
        public int g_s;
        public int b_r;
        public int b_g;
        public int b_b;
        public int b_s;

        public ColorSwizzleData(Random rand)
        {
            r_r = rand.Next(2);
            r_g = rand.Next(2);
            r_b = rand.Next(2);
            r_s = r_r + r_g + r_b;
            g_r = rand.Next(2);
            g_g = rand.Next(2);
            g_b = rand.Next(2);
            g_s = g_r + g_g + g_b;
            b_r = rand.Next(2);
            b_g = rand.Next(2);
            b_b = rand.Next(2);
            b_s = b_r + b_g + b_b;

            if (r_s == 0) r_s = 1;
            if (g_s == 0) g_s = 1;
            if (b_s == 0) b_s = 1;
        }
    }

    public static class WormsForts_Common
    {
        public static List<string> Levels_All = new List<string>()
        {
            "T.xom",
            "T2.xom",
            "T3.xom",
            "DM01.xom",
            "DM02.xom",
            "DM03.xom",
            "DM04.xom",
            "DM05.xom",
            "DM06.xom",
            "DM07.xom",
            "DM08.xom",
            "DM09.xom",
            "DM10.xom",
            "E1.xom",
            "E2.xom",
            "E3.xom",
            "E4.xom",
            "E5.xom",
            "G1.xom",
            "G2.xom",
            "G3.xom",
            "G4.xom",
            "G5.xom",
            "M1.xom",
            "M2.xom",
            "M3.xom",
            "M4.xom",
            "M5.xom",
            "O1.xom",
            "O2.xom",
            "O3.xom",
            "O4.xom",
            "O5.xom",
            "ME1.xom",
            "ME2.xom",
            "ME3.xom",
            "MM1.xom",
            "MM2.xom",
            "MM3.xom",
            "MO1.xom",
            "MO2.xom",
            "MO3.xom",
            "MR1.xom",
            "MR2.xom",
            "MR3.xom",
        };

        public static List<string> Levels_Tutorial = new List<string>()
        {
            "T.xom",
            "T2.xom",
            "T3.xom",
        };

        public static List<string> Levels_Deathmatch = new List<string>()
        {
            "DM01.xom",
            "DM02.xom",
            "DM03.xom",
            "DM04.xom",
            "DM05.xom",
            "DM06.xom",
            "DM07.xom",
            "DM08.xom",
            "DM09.xom",
            "DM10.xom",
        };

        public static List<string> Levels_Campaign = new List<string>()
        {
            "E1.xom",
            "E2.xom",
            "E3.xom",
            "E4.xom",
            "E5.xom",
            "G1.xom",
            "G2.xom",
            "G3.xom",
            "G4.xom",
            "G5.xom",
            "M1.xom",
            "M2.xom",
            "M3.xom",
            "M4.xom",
            "M5.xom",
            "O1.xom",
            "O2.xom",
            "O3.xom",
            "O4.xom",
            "O5.xom",
        };

        public static List<string> Levels_Multiplayer = new List<string>()
        {
            "ME1.xom",
            "ME2.xom",
            "ME3.xom",
            "MM1.xom",
            "MM2.xom",
            "MM3.xom",
            "MO1.xom",
            "MO2.xom",
            "MO3.xom",
            "MR1.xom",
            "MR2.xom",
            "MR3.xom",
        };

    }
}
