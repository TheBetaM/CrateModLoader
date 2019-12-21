using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader
{
    static class CTTR_Data
    {
        public enum CharacterID
        {
            BabyYeti = 0,
            Chick = 1,
            Chicken = 2,
            Coco = 3,
            Crash = 4,
            Crunch = 5,
            Generic = 6,
            Cortex = 7,
            NGin = 8,
            Nina = 9,
            npcFat = 10,
            npcFatB = 11,
            npcFatD = 12,
            npcFemaleA = 13,
            npcFemaleB = 14,
            npcFemaleC = 15,
            npcKid = 16,
            npcKidB = 17,
            npcKidgirlB = 18,
            npcMale = 19,
            npcMaleB = 20,
            npcMaleC = 21,
            Parkworker = 23,
            Pasadena = 24,
            Penguin = 25,
            Stew = 26,
            VonClutch = 27,
            Willie = 28,
            Yeti = 29,
        }
        public static string[] CharacterFileNames = new string[]
        {
            "babyyeti",
            "chick",
            "chicken",
            "coco",
            "crash",
            "crunch",
            "generic",
            "neocortex",
            "ngin",
            "nina",
            "npcFat",
            "npcFatB",
            "npcFatD",
            "npcFemaleA",
            "npcFemaleB",
            "npcFemaleC",
            "npcKid",
            "npcKidB",
            "npcKidgirlB",
            "npcMale",
            "npcMaleB",
            "npcMaleC",
            "parkworker",
            "pasadena",
            "penguin",
            "stew",
            "vonclutch",
            "willie",
            "yeti",
        };
        public static string[] CharacterNames = new string[]
        {
            "BabyYeti",
            "Chick",
            "Chicken",
            "Coco",
            "Crash",
            "Crunch",
            "generic",
            "NeoCortex",
            "Ngin",
            "Nina",
            "NPC_Fat",
            "NPC_FatB",
            "NPC_FatD",
            "NPC_FemaleA",
            "NPC_FemaleB",
            "NPC_FemaleC",
            "NPC_Kid",
            "NPC_KidB",
            "NPC_KidgirlB",
            "NPC_Male",
            "NPC_MaleB",
            "NPC_MaleC",
            "Parkworker",
            "Pasadena",
            "Penguin",
            "Stew",
            "VonClutch",
            "Willie",
            "Yeti",
        };
        public enum DriverID
        {
            Crash = 0,
            Cortex = 1,
            Pasadena = 2,
            Crunch = 3,
            Coco = 4,
            Nina = 5,
            NGin = 6,
            VonClutch = 7,
        }
        public static string[] DriverNames = new string[]
        {
            "crash",
            "neocortex",
            "pasadena",
            "crunch",
            "coco",
            "nina",
            "ngin",
            "vonclutch",
        };
        public static string[] DriverCameraNames = new string[]
        {
            "cameras/fe_cam_crashShape",
            "cameras/fe_cam_neoShape",
            "cameras/fe_cam_pasadenaShape",
            "cameras/fe_cam_crunchhape",
            "cameras/fe_cam_cocohape",
            "cameras/fe_cam_ninaShape",
            "cameras/fe_cam_nginShape",
            "cameras/fe_cam_vonShape",
        };
        public static string[] DriverAnimNames = new string[]
        {
            "crash_anim",
            "neocortex_anim",
            "pasadena_anim",
            "crunch_anim",
            "coco_anim",
            "nina_anim",
            "ngin_anim",
            "vonclutch_anim",
        };
        public enum LevelID
        {
            frontend = 0,
            adventure_Arena = 1,
            adventure1 = 2,
            adventure2 = 3,
            adventure3 = 4,
            Bonus1_arena = 5,
            Bonus11 = 6,
            Bonus2_arena = 7,
            dino_arena = 8,
            dino1 = 9,
            dino2 = 10,
            dino3 = 11,
            egypt_arena = 12,
            egypt1 = 13,
            egypt2 = 14,
            egypt3 = 15,
            fairy_arena = 16,
            fairy1 = 17,
            fairy2 = 18,
            fairy3 = 19,
            onfoot_adventure = 20,
            onfoot_dino = 21,
            onfoot_egypt = 22,
            onfoot_fairy = 23,
            onfoot_hub = 24,
            onfoot_midway = 25,
            onfoot_solar = 26,
            onfoot_tutorial = 27,
            sandbox = 28,
            solar_arena = 29,
            solar1 = 30,
            solar2 = 31,
            solar3 = 32,
            testbed1 = 33,
        }
        public static string[] LevelNames = new string[]
        {
            "frontend",
            "adventure_arena",
            "adventure1",
            "adventure2",
            "adventure3",
            "bonus1_arena",
            "bonus11",
            "bonus2_arena",
            "dino_arena",
            "dino1",
            "dino2",
            "dino3",
            "egypt_arena",
            "egypt1",
            "egypt2",
            "egypt3",
            "fairy_arena",
            "fairy1",
            "fairy2",
            "fairy3",
            "onfoot_adventure",
            "onfoot_dino",
            "onfoot_egypt",
            "onfoot_fairy",
            "onfoot_hub",
            "onfoot_midway",
            "onfoot_solar",
            "onfoot_tutorial",
            "sandbox",
            "solar_arena",
            "solar1",
            "solar2",
            "solar3",
            "testbed1",
        };

        public static bool LUA_FindObject(ref List<string> lua_script, string object_type, string object_name, ref int object_start, ref int object_end)
        {
            string pattern = "BeginObject " + object_type + " " + object_name;
            for (int i = 0; i < lua_script.Count; i++)
            {
                if (lua_script[i] == pattern)
                {
                    object_start = i;
                    for (int d = i; d < lua_script.Count; d++)
                    {
                        if (lua_script[d] == "EndObject")
                        {
                            object_end = d;
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public static bool LUA_LoadObject(ref List<string> lua_script, string object_type, string object_name, ref int object_start, ref int object_end, ref List<string> object_content)
        {
            string pattern = "BeginObject " + object_type + " " + object_name;
            for (int i = 0; i < lua_script.Count; i++)
            {
                if (lua_script[i] == pattern)
                {
                    object_start = i;
                    for (int d = i; d < lua_script.Count; d++)
                    {
                        if (lua_script[d] == "EndObject")
                        {
                            object_end = d;
                            for (int a = i + 1; a < d; a++)
                            {
                                object_content.Add(lua_script[a]);
                            }
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }
        public static bool LUA_SaveObject(ref List<string> lua_script, string object_type, string object_name, ref List<string> object_content)
        {
            string pattern = "BeginObject " + object_type + " " + object_name;
            for (int i = 0; i < lua_script.Count; i++)
            {
                if (lua_script[i] == pattern)
                {
                    for (int d = i; d < lua_script.Count; d++)
                    {
                        if (lua_script[d] == "EndObject")
                        {
                            for (int a = i + 1; a < d; a++)
                            {
                                lua_script.RemoveAt(i + 1);
                            }
                            for (int a = 0; a < object_content.Count; a++)
                            {
                                lua_script.Insert(i + a + 1, object_content[a]);
                            }
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

    }
}
