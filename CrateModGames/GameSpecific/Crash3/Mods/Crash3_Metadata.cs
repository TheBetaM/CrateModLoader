using Crash;
using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Crash3.Mods
{
    public class Crash3_Metadata : ModStruct<NSF_Pair>
    {
        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC3 != Crash3_Levels.WarpRoom)
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

                                            CrashTri_Common.InsertStringsInByteArray(ref gool.Anims, i, 27, new List<string>() {
                                            "CML " + ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            seed,
                                            "QUIT"
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
                                        if (s.Contains("GET ALL"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            InsertStringsInByteArray(ref gool.Anims, i - 103, 20, new List<string>() {
                                            ModLoaderGlobals.ProgramVersion.ToUpper(),
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
