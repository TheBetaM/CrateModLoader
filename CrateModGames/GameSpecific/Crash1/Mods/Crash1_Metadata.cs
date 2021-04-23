using Crash;
using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    public class Crash1_Metadata : ModStruct<NSF_Pair>
    {
        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC1 != Crash1_Levels.MapMainMenu)
            {
                return;
            }

            foreach (GOOLEntry gool in pair.nsf.GetEntries<GOOLEntry>())
            {
                if (gool.EName == "GamOC" || gool.EName == "IsldC")
                {
                    if (pair.region == RegionType.NTSC_U || pair.region == RegionType.PAL)
                    {
                        for (int i = gool.Anims.Length - 11; i > 0; i--)
                        {
                            string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                            if (s.Contains("PASSWORD"))
                            {
                                string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                if (seed.Length < 10)
                                {
                                    while (seed.Length < 10)
                                    {
                                        seed += " ";
                                    }
                                }

                                CrashTri_Common.InsertStringsInByteArray(ref gool.Anims, i, 29, new List<string>() {
                                "CML " + ModLoaderGlobals.ProgramVersion.ToUpper(),
                                "SEED: " + seed,
                            });
                            }
                        }
                    }
                    else
                    {
                        for (int i = gool.Anims.Length - 11; i > 0; i--)
                        {
                            string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                            if (s.Contains("TEST SAVE"))
                            {
                                string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                if (seed.Length < 10)
                                {
                                    while (seed.Length < 10)
                                    {
                                        seed += " ";
                                    }
                                }

                                CrashTri_Common.InsertStringsInByteArray(ref gool.Anims, i - 15, 27, new List<string>() {
                                "CML " + ModLoaderGlobals.ProgramVersion.ToUpper(),
                                "SEED: " + seed,
                            });
                            }
                        }
                    }

                }

            }
        }
    }
}
