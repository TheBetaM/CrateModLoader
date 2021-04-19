using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: test
    public class TS_Rand_Music : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Rand_Music;
        public override string Description => Twins_Text.Rand_MusicDesc;

        internal List<uint> musicTypes = new List<uint>();
        internal List<uint> randMusicList = new List<uint>();

        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            List<uint> temp_musicList = new List<uint>();

            musicTypes.Add((uint)MusicID.Academy);
            musicTypes.Add((uint)MusicID.AcademyNoLaugh);
            musicTypes.Add((uint)MusicID.AltLab);
            musicTypes.Add((uint)MusicID.AntAgony);
            musicTypes.Add((uint)MusicID.BeeChase);
            musicTypes.Add((uint)MusicID.Boiler);
            musicTypes.Add((uint)MusicID.BoilerUnused);
            musicTypes.Add((uint)MusicID.BossAmberly);
            musicTypes.Add((uint)MusicID.BossDingodile);
            musicTypes.Add((uint)MusicID.BossNGin);
            musicTypes.Add((uint)MusicID.BossTikimon);
            musicTypes.Add((uint)MusicID.BossTwins);
            musicTypes.Add((uint)MusicID.BossUka);
            musicTypes.Add((uint)MusicID.BP);
            musicTypes.Add((uint)MusicID.Cavern);
            musicTypes.Add((uint)MusicID.ClassroomCortex);
            musicTypes.Add((uint)MusicID.ClassroomCrash);
            musicTypes.Add((uint)MusicID.Henchmania);
            musicTypes.Add((uint)MusicID.Hijinks);
            musicTypes.Add((uint)MusicID.IcebergLab);
            musicTypes.Add((uint)MusicID.IcebergLabFast);
            musicTypes.Add((uint)MusicID.IceClimb);
            musicTypes.Add((uint)MusicID.MechaBandicoot);
            musicTypes.Add((uint)MusicID.Rockslide);
            musicTypes.Add((uint)MusicID.Rooftop);
            musicTypes.Add((uint)MusicID.SlipSlide);
            musicTypes.Add((uint)MusicID.TitleTheme);
            musicTypes.Add((uint)MusicID.TotemRiver);
            musicTypes.Add((uint)MusicID.TwinsanityIsland);
            musicTypes.Add((uint)MusicID.WalrusChase);
            musicTypes.Add((uint)MusicID.WormChase);
            int targetPos = 0;

            for (int i = 0; i < musicTypes.Count; i++)
            {
                temp_musicList.Add(musicTypes[i]);
            }
            while (temp_musicList.Count > 0)
            {
                targetPos = randState.Next(0, temp_musicList.Count);
                randMusicList.Add(temp_musicList[targetPos]);
                temp_musicList.RemoveAt(targetPos);
            }
        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (ushort)ObjectID.DJ)
                        {
                            uint sourceMusic = instance.UnkI323[0];
                            uint targetMusic = (uint)MusicID.TitleTheme;
                            for (int m = 0; m < musicTypes.Count; m++)
                            {
                                if (musicTypes[m] == sourceMusic)
                                {
                                    targetMusic = randMusicList[m];
                                }
                            }
                            instance.UnkI323 = new List<uint>() { targetMusic, 255, instance.UnkI323[2] };

                            break;
                        }
                        else if (instance.ObjectID == (ushort)ObjectID.DJ_TRIGGERABLE)
                        {
                            uint sourceMusic = instance.UnkI323[0];
                            uint targetMusic = (uint)MusicID.TitleTheme;
                            for (int m = 0; m < musicTypes.Count; m++)
                            {
                                if (musicTypes[m] == sourceMusic)
                                {
                                    targetMusic = randMusicList[m];
                                }
                            }
                            instance.UnkI323 = new List<uint>() { targetMusic, 255 };

                            break;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }
    }
}
