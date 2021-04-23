using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    public class Crash1_AddStormyAscent : ModStruct<NSF_Pair>
    {
        public override string Name => "Add Stormy Ascent";
        public override string Description => "Replaces The Great Hall with Stormy Ascent. Works with all other features like Backwards Levels and Randomize Level Order. (Tokens removed from the level to ensure stability)";

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC1 == Crash1_Levels.MapMainMenu)
            {
                GOOLEntry map = pair.nsf.GetEntry<GOOLEntry>("IsldC");
                if (map != null)
                {
                    map.Instructions[298].Value = 0x11822e51; //0x118 XX e51 - XX level ID

                    if (pair.region == RegionType.NTSC_U || pair.region == RegionType.PAL)
                    {
                        for (int i = map.Anims.Length - 15; i > 0; i--)
                        {
                            string s = System.Text.Encoding.Default.GetString(map.Anims, i, 14);
                            if (s.Contains("THE GREAT HALL"))
                            {
                                CrashTri_Common.InsertStringsInByteArray(ref map.Anims, i, 14, new List<string>() {
                                "STORMY ASCENT ",
                            });
                            }
                        }
                    }
                }
            }
            else if (pair.LevelC1 == Crash1_Levels.L28_StormyAscent)
            {
                // remove bugged tokens from the level
                foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
                {
                    foreach (OldEntity ent in zone.Entities)
                    {
                        if (ent.Type == 34)
                        {
                            if (ent.VecX == (short)CrateContentTypes.Token_Cortex)
                            {
                                ent.VecX = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}
