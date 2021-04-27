using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
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

    public static class CTR_Common
    {

        public static List<string> TrackFolderNames = new List<string>()
        {
            "arena2",
            "blimp1",
            "castle1",
            "cave1",
            "coco1",
            "desert2",
            "ice1",
            "island1",
            "labs1",
            "proto8",
            "proto9",
            "secret1",
            "secret2",
            "sewer1",
            "space",
            "temple1",
            "tube1",
        };

        public static string GetTrackName(string path)
        {
            foreach (string lev in TrackFolderNames)
            {
                if (path.Contains(lev))
                {
                    return lev;
                }
            }
            return string.Empty;
        }

    }
}
