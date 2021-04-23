using System;
using System.Collections.Generic;
using Pure3D;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public enum TrackID
    {
        adventure1 = 0,
        adventure2 = 1,
        adventure3 = 2,
        fairy1 = 3,
        fairy2 = 4,
        fairy3 = 5,
        dino1 = 6,
        dino2 = 7,
        dino3 = 8,
        egypt1 = 9,
        egypt2 = 10,
        egypt3 = 11,
        solar1 = 12,
        solar2 = 13,
        solar3 = 14,
        adventure_arena = 15,
        fairy_arena = 16,
        dino_arena = 17,
        egypt_arena = 18,
        solar_arena = 19,
        bonus1_arena = 20,
    }

    static partial class CTTR_Data
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
        public enum HubID
        {
            Midway = 0,
            Adventure = 1,
            Fairy = 2,
            Dino = 3,
            Egypt = 4,
            Solar = 5,
            Tutorial = 6,
        }
        public static string[] HubNames = new string[]
        {
            "onfoot_midway",
            "onfoot_adventure",
            "onfoot_fairy",
            "onfoot_dino",
            "onfoot_egypt",
            "onfoot_solar",
            "onfoot_tutorial",
        };
        public static string[] HubNamesSimple = new string[]
        {
            "Midway",
            "Adventure",
            "Fairy",
            "Dino",
            "Egypt",
            "Solar",
            "Tutorial",
        };
        
        public static string[] TrackNames = new string[]
        {
            "adventure1",
            "adventure2",
            "adventure3",
            "fairy1",
            "fairy2",
            "fairy3",
            "dino1",
            "dino2",
            "dino3",
            "egypt1",
            "egypt2",
            "egypt3",
            "solar1",
            "solar2",
            "solar3",
            "adventure_arena",
            "fairy_arena",
            "dino_arena",
            "egypt_arena",
            "solar_arena",
            "bonus1_arena",
        };
        public static string[] TrackNamesSimple = new string[]
        {
            "Adventure1",
            "Adventure2",
            "Adventure3",
            "Fairy1",
            "Fairy2",
            "Fairy3",
            "Dino1",
            "Dino2",
            "Dino3",
            "Egypt1",
            "Egypt2",
            "Egypt3",
            "Solar1",
            "Solar2",
            "Solar3",
            "Adventure",
            "Fairy",
            "Dino",
            "Egypt",
            "Solar",
            "Dino",
        };
        public static string[] TrackGateNames = new string[]
        {
            "adventuregate1",
            "adventuregate2",
            "adventuregate3",
            "fairygate1",
            "fairygate2",
            "fairygate3",
            "dinogate1",
            "dinogate2",
            "dinogate3",
            "egyptgate1",
            "egyptgate2",
            "egyptgate3",
            "solargate1",
            "solargate2",
            "solargate3",
            "adventuregate4",
            "fairygate4",
            "dinogate4",
            "egyptgate4",
            "solargate4",
            "bonusgate",
        };
        public static string[] MissionObjectiveTypes = new string[]
        {
            "adventure",
            "dino",
            "egypt",
            "midway",
            "fairy",
            "solar",
        };
        public static string[] MissionObjectiveHubNamesSimple = new string[]
        {
            "Adventure",
            "Dino",
            "Egypt",
            "Midway",
            "Fairy",
            "Solar",
        };
        public static string[] MinigameObjectiveTypes = new string[]
        {
            "MiniGamesMidway1",
            "MiniGamesMidway2",
            "MiniGamesAdventure1",
            "MiniGamesFairy1",
            "MiniGamesDino1",
            "MiniGamesEgypt1",
            "MiniGamesSolar1",
            "MiniGamesSolar2",
        };
        public static string[] MinigameTypeNames = new string[]
        {
            "MiniGameBowling1",
            "MiniGameDucks1",
            "MiniGameSkeets1",
            "MiniGameFallingTargets1",
            "MiniGameFloatingTargets1",
            "MiniGameFallingTargets2",
            "MiniGameBowling2",
            "MiniGameFloatingTargets2",
        };
        public enum MissionID
        {
            Unlock_Hub_Adventure_Gate = 54,
            Unlock_Hub_Fairy_Gate = 55,
            Unlock_Hub_Dino_Gate = 56,
            Unlock_Hub_Egypt_Gate = 57,
            Unlock_Hub_Solar_Gate = 58,
        }
        public static string[] KeyMissionTypes = new string[] // This is because their names have typos
        {
            "Not Used",
            "Not Used",
            "FairyKeyMission",
            "DinotKeyMission",
            "EgyptKeyMission",
            "SolartKeuMission",
            "Not Used",
        };

        public static bool LUA_FindObject(List<string> lua_script, string object_type, string object_name, ref int object_start, ref int object_end)
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

        public static bool LUA_LoadObject(List<string> lua_script, string object_type, string object_name, ref int object_start, ref int object_end, List<string> object_content)
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
        public static bool LUA_SaveObject(List<string> lua_script, string object_type, string object_name, List<string> object_content)
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

        public static void PrintHierarchy(Chunk chunk, int indent)
        {
            Console.WriteLine("{1}{0}", chunk.ToString(), new string('\t', indent));

            foreach (var child in chunk.Children)
                PrintHierarchy(child, indent + 1);
        }

    }
}
