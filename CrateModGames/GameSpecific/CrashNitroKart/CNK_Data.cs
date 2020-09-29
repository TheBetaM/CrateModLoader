using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{

    public enum SubModeID
    {
        Hub = 0,
        Trophy = 1,
        CNK_Challenge = 2,
        Relic = 3,
        Boss = 4,
        Crystal = 5,
        Gem = 6,
    }

    public enum TrackID
    {
        Earth_1 = 0,
        Earth_2 = 1,
        Earth_3 = 2,
        Arena_1 = 3,
        Barin_1 = 4,
        Barin_2 = 5,
        Barin_3 = 6,
        Arena_2 = 7,
        Fenom_1 = 8,
        Fenom_2 = 9,
        Fenom_3 = 10,
        Arena_3 = 11,
        Teknee_1 = 12,
        Teknee_2 = 13,
        Teknee_3 = 14,
        Arena_4 = 15,
        Arena_5 = 16,
        VeloRace = 17,
        Citadel = 18,
        Hub_1 = 19,
        Hub_2 = 20,
        Hub_3 = 21,
        Hub_4 = 22,
        Secr = 23,
    }

    static class CNK_Data
    {

        public static string[] DriverTypes = new string[] { "coco", "cortex", "crash", "crunch", "dingodile", "fakecrash", "ngin", "noxide", "ntrance", "ntropy", "polar", "pura", "realvelo", "tiny", "zam", "zem" };
        public static string[] DriverModelTypes = new string[] { "coco", "ncortex", "crash", "crunch", "dingodile", "fakecrash", "ngin", "noxide", "ntrance", "ntropy", "polar", "pura", "realvelo", "tiny", "zam", "zem", "barinboss", "earthboss", "empvelo", "fenombigboss", "fenomlittleboss", "tekneeboss", "tekneeminion", "velominion" };
        public static string[] DriverAudioTypes = new string[] { "cob", "dnc", "crb", "cnb", "ddl", "fcb", "ngn", "oxd", "ntn", "ntp", "plr", "pur", "rvl", "tny", "zam", "zem", "nsh", "kgo", "vlo", "bnm", "lnm", "oto", "scr", "vlm" };

        public static string[] SubModeName = new string[]
        {
            "hub", "trophy", "ctr", "relic", "boss", "crystal", "gem"
        };

        public static string[] TrackName = new string[]
        {
            "earth1", "earth2", "earth3", "arena1", "barin1", "barin2", "barin3", "arena2", "fenom1", "fenom2", "fenom3", "arena3", "teknee1", "teknee2", "teknee3", "arena4", "arena5", "velorace" , "citadel" , "hub1" , "hub2" , "hub3" , "hub4" , "secr"
        };

    }

    
}
