using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    //unfinished
    public class Crash1_AddCavernLevel : ModStruct<NSF_Pair>
    {
        public override string Name => "Add Caved In";
        public override string Description => "Replaces Papu Papu with the unused cavern level.";

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC1 == Crash1_Levels.MapMainMenu)
            {
                GOOLEntry map = pair.nsf.GetEntry<GOOLEntry>("IsldC");
                if (map != null)
                {
                    map.Instructions[82].Value = 0x11804e51; //0x118 XX e51 - XX level ID

                    if (pair.region == RegionType.NTSC_U || pair.region == RegionType.PAL)
                    {
                        for (int i = map.Anims.Length - 10; i > 0; i--)
                        {
                            string s = System.Text.Encoding.Default.GetString(map.Anims, i, 9);
                            if (s.Contains("PAPU PAPU"))
                            {
                                CrashTri_Common.InsertStringsInByteArray(ref map.Anims, i, 9, new List<string>() {
                                "CAVED IN ",
                            });
                            }
                        }
                    }
                }
            }
            else if (pair.LevelC1 == Crash1_Levels.L29_Cavern)
            {
                OldEntity CrashEntity = null;
                OldZoneEntry CrashZone = null;

                for (int i = 0; i < pair.nsf.Chunks.Count; i++)
                {
                    if (pair.nsf.Chunks[i] is OldSoundChunk old)
                    {
                        List<Entry> sounds = new List<Entry>(old.Entries);
                        pair.nsf.Chunks[i] = new SoundChunk(sounds);
                    }
                }

                foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
                {
                    for (int i = 0; i < zone.Entities.Count; i++)
                    {
                        if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                        {
                            CrashEntity = zone.Entities[i];
                            CrashZone = zone;
                        }
                        else if (zone.Entities[i].Type == 3) // wumpa only
                        {
                            zone.Entities[i].Subtype = 16;
                        }
                    }
                }

                pair.oldnsd.Camera = 0;
                pair.oldnsd.StartZone = CrashZone.EID;
                pair.oldnsd.ID = 0x04;

            }
        }
    }
}
