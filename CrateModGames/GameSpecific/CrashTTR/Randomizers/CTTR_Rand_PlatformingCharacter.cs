using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashTTR;
using Pure3D;
using Pure3D.Chunks;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_PlatformingCharacter : ModStruct<string>
    {
        public override string Name => CTTR_Text.Rand_PlatformingCharacter;
        public override string Description => CTTR_Text.Rand_PlatformingCharacterDesc;

        public Pure3D.File targetCharAnim = null;
        public Pure3D.Chunk targetIdleAnim = null;
        private List<int> randChars;

        public override void BeforeModPass()
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
        }

        public override void ModPass(string path_extr)
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
            if (randChars[0] != (int)CTTR_Data.DriverID.Crash && System.IO.File.Exists(path_extr + @"design\permanent\skins.god"))
            {
                string[] skins_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\skins.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < skins_lines.Length; i++)
                {
                    LineList.Add(skins_lines[i]);
                }

                int skin_Start = 0;
                int skin_End = 0;
                List<string> SkinObj = new List<string>();
                if (CTTR_Data.LUA_LoadObject(LineList, "Skin", "CrashDefault", ref skin_Start, ref skin_End, SkinObj))
                {
                    for (int i = 0; i < SkinObj.Count; i++)
                    {
                        if (SkinObj[i] == "this.SetOnfootSkinFilename(\"crash_onfoot_model\")")
                        {
                            SkinObj[i] = "this.SetOnfootSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                        else if (SkinObj[i] == "this.SetSpinSkinFilename(\"crash_spin_model\")")
                        {
                            SkinObj[i] = "this.SetSpinSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                    }
                }
                CTTR_Data.LUA_SaveObject(LineList, "Skin", "CrashDefault", SkinObj);

                skins_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    skins_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\skins.god", skins_lines);

            }

            // Swapping idle animation for platforming character
            if (randChars[0] != (int)CTTR_Data.DriverID.Crash)
            {
                if (targetCharAnim == null || targetIdleAnim == null)
                {
                    targetCharAnim = new Pure3D.File();
                    if (System.IO.File.Exists(path_extr + @"art\animation\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d"))
                    {
                        targetCharAnim.Load(path_extr + @"art\animation\\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d");
                    }
                    else if (System.IO.File.Exists(path_extr + @"art\animation\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d"))
                    {
                        targetCharAnim.Load(path_extr + @"art\animation\\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d");
                    }
                    else
                    {
                        return;
                    }

                    if (targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                    {
                        targetIdleAnim = targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_idle");
                    }
                    else if (targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_talk_bored") != null) // Nina doesn't have an idle animation
                    {
                        Animation targetIdleAnimAnim;
                        targetIdleAnimAnim = targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_talk_bored");
                        targetIdleAnimAnim.Name = "onfoot_idle";
                        targetIdleAnim = (Pure3D.Chunk)targetIdleAnimAnim;
                    }
                    else
                    {
                        return;
                    }
                }

                if (targetCharAnim != null && targetIdleAnim != null)
                {
                    if (System.IO.File.Exists(path_extr + @"art\animation\crash_onfoot_animations.p3d"))
                    {
                        Pure3D.File CrashOnfootAnim = new Pure3D.File();
                        CrashOnfootAnim.Load(path_extr + @"art\animation\crash_onfoot_animations.p3d");

                        if (CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                        {
                            int animIndex = CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.GetChildIndexByName<Animation>("onfoot_idle");
                            CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.Children[animIndex] = targetIdleAnim;
                        }

                        CrashOnfootAnim.Save(path_extr + @"art\animation\crash_onfoot_animations1.p3d");
                        System.IO.File.Delete(path_extr + @"art\animation\crash_onfoot_animations.p3d");
                        System.IO.File.Move(path_extr + @"art\animation\crash_onfoot_animations1.p3d", path_extr + @"art\animation\crash_onfoot_animations.p3d");

                    }
                    if (System.IO.File.Exists(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d"))
                    {
                        Pure3D.File CrashOnfootMidwayAnim = new Pure3D.File();
                        CrashOnfootMidwayAnim.Load(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");

                        if (CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                        {
                            int animIndex = CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.GetChildIndexByName<Animation>("onfoot_idle");
                            CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.Children[animIndex] = targetIdleAnim;
                        }

                        CrashOnfootMidwayAnim.Save(path_extr + @"art\animation\crash_onfoot_midway_animations1.p3d");
                        System.IO.File.Delete(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");
                        System.IO.File.Move(path_extr + @"art\animation\crash_onfoot_midway_animations1.p3d", path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");

                    }
                }
            }
        }

    }
}
