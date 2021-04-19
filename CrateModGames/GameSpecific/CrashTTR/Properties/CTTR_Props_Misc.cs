using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    [ModCategory(1)]
    public static class CTTR_Props_Misc
    {
        public static string[] TrackNames = Enum.GetNames(typeof(TrackID));

        public static ModPropNamedUIntArray RaceLaps = new ModPropNamedUIntArray(new uint[]
        {
            3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 9, 3, 3
        }, TrackNames);
    }
}
