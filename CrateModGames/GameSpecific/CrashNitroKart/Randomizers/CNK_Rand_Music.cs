using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    //unfinished, music.csv doesn't seem to affect the game?
    public class CNK_Rand_Music : ModStruct<CSV>
    {
        private Random randState;

        public override void BeforeModPass()
        {
            randState = GetRandom();
        }

        public override void ModPass(CSV file)
        {
            if (file.Name != "music.csv")
            {
                return;
            }

            List<TrackID> TrackList = new List<TrackID>()
                {
                    TrackID.Barin_1,
                    TrackID.Barin_2,
                    TrackID.Barin_3,
                    TrackID.Earth_1,
                    TrackID.Earth_2,
                    TrackID.Earth_3,
                    TrackID.Fenom_1,
                    TrackID.Fenom_2,
                    TrackID.Fenom_3,
                    TrackID.Teknee_1,
                    TrackID.Teknee_2,
                    TrackID.Teknee_3,
                    TrackID.VeloRace,
                };
            List<TrackID> HubList = new List<TrackID>()
                {
                    TrackID.Citadel,
                    TrackID.Hub_1,
                    TrackID.Hub_2,
                    TrackID.Hub_3,
                    TrackID.Hub_4,
                };

            List<TrackID> TempTracks = new List<TrackID>(TrackList);
            List<TrackID> TempHubs = new List<TrackID>(HubList);
            List<TrackID> RandTracks = new List<TrackID>();
            List<TrackID> RandHubs = new List<TrackID>();
            while (TempTracks.Count > 0)
            {
                int r = randState.Next(TempTracks.Count);
                //RandTracks.Add(TempTracks[r]);
                RandTracks.Add(TrackID.Fenom_2);
                TempTracks.RemoveAt(r);
            }
            while (TempHubs.Count > 0)
            {
                int r = randState.Next(TempHubs.Count);
                //RandHubs.Add(TempHubs[r]);
                RandHubs.Add(TrackID.Citadel);
                TempHubs.RemoveAt(r);
            }

            string extra = ",0,MusicBalance,0.8";
            string extraf = "f,0,MusicBalance,0.8";

            /*
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[0]] + "," + CNK_Common.TrackName[(int)RandTracks[0]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[0]] + "f," + CNK_Common.TrackName[(int)RandTracks[0]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[1]] + "," + CNK_Common.TrackName[(int)RandTracks[1]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[1]] + "f," + CNK_Common.TrackName[(int)RandTracks[1]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[2]] + "," + CNK_Common.TrackName[(int)RandTracks[2]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[2]] + "f," + CNK_Common.TrackName[(int)RandTracks[2]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)HubList[0]] + "," + CNK_Common.TrackName[(int)RandHubs[0]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[3]] + "," + CNK_Common.TrackName[(int)RandTracks[3]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[3]] + "f," + CNK_Common.TrackName[(int)RandTracks[3]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[4]] + "," + CNK_Common.TrackName[(int)RandTracks[4]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[4]] + "f," + CNK_Common.TrackName[(int)RandTracks[4]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[5]] + "," + CNK_Common.TrackName[(int)RandTracks[5]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[5]] + "f," + CNK_Common.TrackName[(int)RandTracks[5]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[6]] + "," + CNK_Common.TrackName[(int)RandTracks[6]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[6]] + "f," + CNK_Common.TrackName[(int)RandTracks[6]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[7]] + "," + CNK_Common.TrackName[(int)RandTracks[7]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[7]] + "f," + CNK_Common.TrackName[(int)RandTracks[7]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[8]] + "," + CNK_Common.TrackName[(int)RandTracks[8]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[8]] + "f," + CNK_Common.TrackName[(int)RandTracks[8]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)HubList[1]] + "," + CNK_Common.TrackName[(int)RandHubs[1]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)HubList[2]] + "," + CNK_Common.TrackName[(int)RandHubs[2]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)HubList[3]] + "," + CNK_Common.TrackName[(int)RandHubs[3]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)HubList[4]] + "," + CNK_Common.TrackName[(int)RandHubs[4]] + extra);

            for (int i = 35; i < 37; i++)
            {
                csv_Music_LineList.Add(csv_music[i]);
            }

            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[9]] + "," + CNK_Common.TrackName[(int)RandTracks[9]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[9]] + "f," + CNK_Common.TrackName[(int)RandTracks[9]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[10]] + "," + CNK_Common.TrackName[(int)RandTracks[10]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[10]] + "f," + CNK_Common.TrackName[(int)RandTracks[10]] + extraf);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[11]] + "," + CNK_Common.TrackName[(int)RandTracks[11]] + extra);
            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[11]] + "f," + CNK_Common.TrackName[(int)RandTracks[11]] + extraf);

            for (int i = 43; i < 45; i++)
            {
                csv_Music_LineList.Add(csv_music[i]);
            }

            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[12]] + "," + CNK_Common.TrackName[(int)RandTracks[12]] + extra);

            for (int i = 46; i < 48; i++)
            {
                csv_Music_LineList.Add(csv_music[i]);
            }

            csv_Music_LineList.Add(CNK_Common.TrackName[(int)TrackList[12]] + "f," + CNK_Common.TrackName[(int)RandTracks[12]] + extraf);

            csv_Music_LineList.Add("");
            */

        }

    }
}
