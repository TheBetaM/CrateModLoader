using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTTR;
using Pure3D;
using Pure3D.Chunks;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_PlatformingCharacter : ModStruct<File, GOD_File>
    {
        public override bool NeedsCachePass => true;

        private Chunk targetIdleAnim = null;
        private List<int> randChars;
        private bool NonCrash = false;

        public override void BeforeCachePass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randChars = new List<int>();

            int maxPlayableCharacters = 2;

            List<int> possibleChars = new List<int>();

            for (int i = 0; i < 8; i++)
            {
                possibleChars.Add(i);
            }

            for (int i = 0; i < maxPlayableCharacters; i++)
            {
                int targetChar = possibleChars[randState.Next(0, possibleChars.Count)];
                randChars.Add(targetChar);
                possibleChars.Remove(targetChar);
            }

            NonCrash = randChars[0] != (int)CTTR_Data.DriverID.Crash;
        }

        public override void CachePass(File file)
        {
            if (targetIdleAnim == null)
            {
                if (file.FullName.Contains(CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d") ||
                    file.FullName.Contains(CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d"))
                {
                    if (file.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                    {
                        targetIdleAnim = file.RootChunk.GetChildByName<Animation>("onfoot_idle");
                    }
                    else if (file.RootChunk.GetChildByName<Animation>("onfoot_talk_bored") != null) // Nina doesn't have an idle animation
                    {
                        Animation targetIdleAnimAnim;
                        targetIdleAnimAnim = file.RootChunk.GetChildByName<Animation>("onfoot_talk_bored");
                        targetIdleAnimAnim.Name = "onfoot_idle";
                        targetIdleAnim = (Chunk)targetIdleAnimAnim;
                    }
                }
            }
        }

        public override void ModPass(GOD_File file)
        {
            /* TODO later, because it requires mission logic to unlock Crash/Cortex
           if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
           {
               string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
               List<string> LineList = new List<string>();
               for (int i = 0; i < startup_lines.Length; i++)
               {
                   LineList.Add(startup_lines[i]);
               }

               int characterList_Start = 0;
               int characterList_End = 0;
               List<string> DefaultUnlocks = new List<string>();
               if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "UnlockDefaults", ref characterList_Start, ref characterList_End, DefaultUnlocks))
               {
                   DefaultUnlocks.Clear();
                   DefaultUnlocks.Add("this.SetName(\"UnlockDefaults\")");
                   for (int i = 0; i < randChars.Count; i++)
                   {
                       DefaultUnlocks.Add("this.AddAction_UnlockCar(\"" + CTTR_Data.DriverNames[randChars[i]] + "\",1)");
                   }
               }
               CTTR_Data.LUA_SaveObject(LineList, "Objective", "UnlockDefaults", DefaultUnlocks);

               startup_lines = new string[LineList.Count];
               for (int i = 0; i < LineList.Count; i++)
               {
                   startup_lines[i] = LineList[i];
               }

               System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", startup_lines);

           }
           */

            if (!NonCrash)
            {
                return;
            }
            if (NonCrash && file.FullName.Contains(@"skins.god"))
            {
                LUA_Object obj = file.GetObject("Skin", "CrashDefault");
                if (obj != null)
                {
                    for (int i = 0; i < obj.Content.Count; i++)
                    {
                        if (obj.Content[i] == "this.SetOnfootSkinFilename(\"crash_onfoot_model\")")
                        {
                            obj.Content[i] = "this.SetOnfootSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                        else if (obj.Content[i] == "this.SetSpinSkinFilename(\"crash_spin_model\")")
                        {
                            obj.Content[i] = "this.SetSpinSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                    }
                }
            }
        }

        public override void ModPass(File file)
        {
            if (!NonCrash)
            {
                return;
            }

            // Swapping idle animation for platforming character
            if (targetIdleAnim != null)
            {
                if (file.FullName.Contains("crash_onfoot_animations.p3d") ||
                    file.FullName.Contains("crash_onfoot_midway_animations.p3d"))
                {
                    if (file.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                    {
                        int animIndex = file.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.GetChildIndexByName<Animation>("onfoot_idle");
                        file.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.Children[animIndex] = targetIdleAnim;
                    }
                    if (file.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                    {
                        int animIndex = file.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.GetChildIndexByName<Animation>("onfoot_idle");
                        file.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.Children[animIndex] = targetIdleAnim;
                    }
                }
            }
        }

    }
}
