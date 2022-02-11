using Crash;
using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Crash2.Mods
{
    public class Crash2_Metadata : ModStruct<NSF_Pair>
    {
        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC2 != Crash2_Levels.WarpRoom && pair.LevelC2 != Crash2_Levels.WarpRoom2 && pair.LevelC2 != Crash2_Levels.WarpRoom3 && pair.LevelC2 != Crash2_Levels.WarpRoom4 && pair.LevelC2 != Crash2_Levels.WarpRoom5)
            {
                return;
            }

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is GOOLEntry gool)
                        {
                            if (gool.EName == "DispC")
                            {
                                if (pair.region == RegionType.NTSC_U)
                                {
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("RESUME"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            CrashTri_Common.InsertStringsInByteArray(ref gool.Anims, i, 17, new List<string>() {
                                            ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            seed,
                                        });
                                        }
                                    }
                                }
                                else if (pair.region == RegionType.PAL)
                                {
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("RESUME"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            CrashTri_Common.InsertStringsInByteArray(ref gool.Anims, i, 45, new List<string>() {
                                            ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            "OPTIONEN",
                                            "OPCIONES",
                                            "OPZIONI",
                                            seed,
                                        });
                                        }
                                    }
                                }
                                else
                                {
                                    /*
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("UNKNOWN"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            InsertStringsInByteArray(ref gool.Anims, i + 436, 11, new List<string>() {
                                            //ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            seed,
                                        });
                                        }
                                    }
                                    */
                                }

                            }

                        }
                    }
                }
            }
        }
    }
}
