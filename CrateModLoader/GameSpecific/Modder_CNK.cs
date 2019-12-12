using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
//CNK Tools/API by BetaM, ManDude and eezstreet.

namespace CrateModLoader
{
    class Modder_CNK
    {
        public string gameName = "CNK";
        public string apiCredit = "Tools/API by BetaM, ManDude and eezstreet";
        public System.Drawing.Image gameIcon = Properties.Resources.icon_cnk;
        public string[] modOptions = {
            "Randomize Adventure Hub Warp Pads",
            "Randomize Adventure Requirements",
            "Randomize Adventure Rewards",
            "Randomize Character Stats",
            "Randomize Kart Stats",
            "Randomize AI Kart Stats",
            "Randomize Surface Parameters",
            "Randomize Weapon Pools",
            "Randomize Wumpa Crate",
            "Disable Fadeout Overlay",
            "Speed Up Mask Hints" };

        public bool Randomize_Hub_Pads = false;
        public bool Randomize_Hub_Requirements = false;
        public bool Randomize_Hub_Rewards = false;
        public bool Randomize_Character_Stats = false;
        public bool Randomize_Kart_Stats = false;
        public bool Randomize_AI_Kart_Stats = false; //TODO physics
        public bool Randomize_Wumpa_Crate = false; //TODO dda
        public bool Randomize_Surface_Parameters = false; //TODO physics
        public bool Randomize_Weapon_Pools = false; //TODO dda
        public bool Randomize_Obstacles = false; //TODO obstacles
        public bool Mod_SpeedUp_Mask_Hints = false; //TODO hints
        public bool Mod_Disable_Fadeout = false; //TODO hud
        public bool Mod_Mask_Hints_NoMask = false; //TODO, hinthistory.csv
        public bool Mod_Disable_Unlock_Popups = false; //TODO, unlockables.csv
        private string path_gob_extracted = "";

        public enum CNK_Options
        {
            RandomizeHubPads = 0,
            RandomizeHubRequirements = 1,
            RandomizeAdventureRewards = 2,
            RandomizeCharacterStats = 3,
            RandomizeKartStats = 4,
            RandomizeAIKartStats = 5,
            RandomizeSurfaceParameters = 6,
            RandomizeWeaponPools = 7,
            RandomizeWumpaCrate = 8,
            DisableFadeout = 9,
            SpeedUpMaskHints = 10,
        }

        public void OptionChanged(int option, bool value)
        {
            if (option == (int)CNK_Options.RandomizeHubPads)
            {
                Randomize_Hub_Pads = value;
            }
            else if (option == (int)CNK_Options.RandomizeCharacterStats)
            {
                Randomize_Character_Stats = value;
            }
            else if (option == (int)CNK_Options.DisableFadeout)
            {
                Mod_Disable_Fadeout = value;
            }
            else if (option == (int)CNK_Options.RandomizeKartStats)
            {
                Randomize_Kart_Stats = value;   
            }
            else if (option == (int)CNK_Options.RandomizeAIKartStats)
            {
                Randomize_AI_Kart_Stats = value;
            }
            else if (option == (int)CNK_Options.RandomizeWumpaCrate)
            {
                Randomize_Wumpa_Crate = value;
            }
            else if (option == (int)CNK_Options.SpeedUpMaskHints)
            {
                Mod_SpeedUp_Mask_Hints = value;
            }
            else if (option == (int)CNK_Options.RandomizeHubRequirements)
            {
                Randomize_Hub_Requirements = value;
            }
            else if (option == (int)CNK_Options.RandomizeAdventureRewards)
            {
                Randomize_Hub_Rewards = value;
            }
        }

        public enum KartPhysicsBaseRows
        { // This enum's comments are from the original CNK files, not made for this tool!
            /// <summary> float | The minimum height we need to get without jumping before we set the in-air state (Note: This is from the center of the collision sphere!!!) </summary>
            m_MinHeightForAirNoJump = 1,
            /// <summary> float | The maximum we will allow our XY velocity to get. | 40 (Trying to limit aberrant behavior.) </summary>
            m_MaxLinearVelXY = 3,
            /// <summary> float | The maximum we will allow our Z velocity to get </summary>
            m_MaxLinearVelZ = 4,
            /// <summary> float | The collision sphere radius for the kart (m) </summary>
            m_CollisionRadius = 6,
            /// <summary> X, Y, Z | The collision sphere offset position from the kart (m) </summary>
            m_CollisionSphereOffset = 7,
            /// <summary> float | The NORMAL maximum FORWARD SPEED of the kart (m/sec) | 27 </summary>
            m_MaxForwardSpeedNormal = 9,
            /// <summary> float | The WUMPA maximum FORWARD SPEED of the kart (m/sec) | 30 </summary>
            m_MaxForwardSpeedWumpa = 10,
            /// <summary> float | The maximum REVERSE SPEED of the kart (m/sec) </summary>
            m_MaxReverseSpeed = 11,
            /// <summary> float | The NORMAL ACCELERATION GAIN of the kart (m/sec) | "18 (2.12s), 20 (1.82s), 22 (1.58s), 24 (1.40s), 26 (1.25s)" | 22 </summary>
            m_AccelerationGainNormal = 12,
            /// <summary> float | The WUMPA ACCELERATION GAIN of the kart (m/sec) | 25 </summary>
            m_AccelerationGainWumpa = 13,
            /// <summary> float | The REVERSE ACCELERATION GAIN of the kart (m/sec) | 37 </summary>
            m_ReverseGain = 14,
            /// <summary> float | The maximum REVERSE SPEED of the kart (m/sec) </summary>
            m_BrakeForce = 15,
            /// <summary> float | Speed to determine when we are in low speed driving model (m/sec) </summary>
            m_LowSpeed = 16,
            /// <summary> float | The amount of GRAVITY when in AIR (x times gravity) </summary>
            m_GravityAir = 17,
            /// <summary> float | The amount of GRAVITY when on GROUND (x times gravity) </summary>
            m_GravityGround = 18,
            /// <summary> float | The amount of DOWNFORCE when in MAGLEV (x times gravity) </summary>
            m_DownforceMagLev = 19,
            /// <summary> float | The amount of DOWNFORCE when in MAGLEV and IN AIR (x times gravity) (Note: This is ONLY applied after we have gained air for m_DownforceMagLevAirTime seconds) | "9, 12, 14" </summary>
            m_DownforceInAirMagLev = 20,
            /// <summary> float | The amount of DOWNFORCE when on GROUND (x times gravity)</summary>
            m_DownforceGround = 21,
            /// <summary> float | The time we allow in AIR before we apply m_DownforceMagLevInAir | 0.3 </summary>
            m_DownforceMagLevAirTime = 22,
            /// <summary> float | The minimum angle that this kicks in (r) </summary>
            m_SlopeMinAngle = 24,
            /// <summary> float | The maximum angle where we achieve full extra (r) </summary>
            m_SlopeMaxAngle = 25,
            /// <summary> float | The acceleration increase (percent) </summary>
            m_SlopeAccelExtra = 26,
            /// <summary> float | The NORMAL kart turn rate (r/sec) | 80 </summary>
            m_TurnRateNormal = 28,
            /// <summary> float | The WUMPA kart turn rate (r/sec) | 80 </summary>
            m_TurnRateWumpa = 29,
            /// <summary> float | The kart additional turn rate when brake is pressed (r/sec) | 90 </summary>
            m_TurnRateBrake = 30,
            /// <summary> float | The kart additional turn rate when accelerator and not brake is pressed (r/sec) </summary>
            m_TurnRateAccel = 31,
            //Todo: the rest
        }
        public enum KartPhysicsCharacterRows
        {// This enum's comments are from the original CNK files, not made for this tool!
            /// <summary> float </summary>
            m_MaxForwardSpeedNormal = 1,
            /// <summary> float </summary>
            m_MaxForwardSpeedWumpa = 2,
            /// <summary> float </summary>
            m_AccelerationGainNormal = 3,
            /// <summary> float </summary>
            m_AccelerationGainWumpa = 4,
            /// <summary> float </summary>
            m_BrakeForce = 5,
            /// <summary> float </summary>
            m_TurnRateNormal = 7,
            /// <summary> float </summary>
            m_TurnRateWumpa = 8,
            /// <summary> float </summary>
            m_TurnRateBrake = 9,
            /// <summary> float </summary>
            m_TurnRateAccel = 10,
            /// <summary> float </summary>
            m_HiTurnStartAngle = 12,
            /// <summary> lat / long / lat 2 long </summary>
            m_HiTurnFriction = 13,
            /// <summary> lat / long / lat 2 long </summary>
            m_NormalFriction = 14,
            /// <summary> float </summary>
            m_InAirTurnRateNormal = 16,
            /// <summary> float </summary>
            m_InAirTurnRateWumpa = 17,
            /// <summary> float </summary>
            m_TurnDecellSpeed = 19,
            /// <summary> float </summary>
            m_TurnDecellForce = 20,
            /// <summary> float </summary>
            m_TurnDecellForceMax = 21,
            /// <summary> float </summary>
            m_SlideMaxAngle = 23,
            /// <summary> float </summary>
            m_SlideMinAngle = 24,
            /// <summary> float </summary>
            m_SlideTurnRateInToSlide = 25,
            /// <summary> float </summary>
            m_SlideTurnRateAwayFromSlide = 26,
            /// <summary> lat / long / lat 2 long </summary>
            m_SlideFrictionLow = 28,
            /// <summary> lat / long / lat 2 long </summary>
            m_SlideFrictionNorm = 29,
            /// <summary> lat / long / lat 2 long </summary>
            m_SlideFrictionHigh = 30,
            /// <summary> float </summary>
            m_BoostMaxImpulsePerSecond = 32,
            /// <summary> float </summary>
            m_BoostSlidePushTime = 33,
            /// <summary> float </summary>
            m_BoostSlidePushAngle = 34,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_JUMP_SMALL = 35,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_JUMP_MEDIUM = 36,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_JUMP_LARGE = 37,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_SLIDE_1 = 38,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_SLIDE_2 = 39,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_SLIDE_3 = 40,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_PAD = 41,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_START = 42,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_AKU_DROP = 43,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_TURBOBOOST = 44,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_TURBOBOOST_JUICED = 45,
            /// <summary> speed / time / wheelie </summary>
            m_BoostInfo_eBOOST_SUPER_ENGINE = 46,
            /// <summary> float </summary>
            m_UIStats_Speed = 48,
            /// <summary> float </summary>
            m_UIStats_Acceleration = 49,
            /// <summary> float </summary>
            m_UIStats_Turn = 50,
            /// <summary> float </summary>
            m_UIStats_MaxValue = 51,
        }

        public void StartModProcess()
        {
            // Fixes names for PS2, and moves the archive for convenience
            File.Move(Program.ModProgram.extractedPath + "/ASSETS.GFC;1", AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC");
            File.Move(Program.ModProgram.extractedPath + "/ASSETS.GOB;1", AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB");

            // Extract GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract_in.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            GobExtract.StartInfo.Arguments = "Tools/ASSETS.GOB" + " " + "Tools/cml_extr";
            GobExtract.Start();
            GobExtract.WaitForExit();
            path_gob_extracted = AppDomain.CurrentDomain.BaseDirectory + "/Tools/cml_extr/";

            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB");

            ModProcess();
        }

        void ModProcess()
        {
            // Proof of concept hiscores mod
            //string[] hiscores_lines = File.ReadAllLines(path_gob_extracted + "common/gameprogression/hiscores.csv");
            //hiscores_lines[5] = "Modded,crash,69,# Modded         ";
            //File.WriteAllLines(path_gob_extracted + "common/gameprogression/hiscores.csv", hiscores_lines);

            Random randState = new Random(Program.ModProgram.randoSeed);

            if (Randomize_Hub_Requirements)
            {
                string[] csv_advtracksmanager = File.ReadAllLines(path_gob_extracted + "common/gameprogression/adventuretracksmanager.csv");
                string[] cur_line_split;
                string[] requirement_types = new string[] { "trophy" ,"key", "token_red", "token_blue", "token_purple", "token_green", "gem_blue","gem_red","gem_green","gem_purple", "relic", "relic2", "relic3" };
                int target_req = 0;
                int target_amount = 0;

                for (int cur_row = 14; cur_row < 75; cur_row++)
                {
                    cur_line_split = csv_advtracksmanager[cur_row].Split(new string[] { ",," },StringSplitOptions.RemoveEmptyEntries);
                    target_req = randState.Next(0, requirement_types.Length);
                    target_amount = randState.Next(0, 4);
                    csv_advtracksmanager[cur_row] = cur_line_split[0] + ",," + cur_line_split[1] + ",," + requirement_types[target_req] + ",," + target_amount;
                }

                File.WriteAllLines(path_gob_extracted + "common/gameprogression/adventuretracksmanager.csv", csv_advtracksmanager);
            }
            if (Randomize_Hub_Rewards)
            {
                string[] csv_goalstorewards = File.ReadAllLines(path_gob_extracted + "common/gameprogression/goalstorewardsconverter.csv");
                string[] cur_line_split;
                string[] reward_types = new string[] { "trophy", "key", "relic", "token_red", "token_blue", "token_purple", "token_green", "gem_blue", "gem_red", "gem_green", "gem_purple" };
                int target_rew = 0;

                for (int cur_row = 6; cur_row < 73; cur_row++)
                {
                    if (csv_goalstorewards[cur_row] != "")
                    {
                        cur_line_split = csv_goalstorewards[cur_row].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        target_rew = randState.Next(0, reward_types.Length);
                        csv_goalstorewards[cur_row] = cur_line_split[0] + "," + cur_line_split[1] + "," + reward_types[target_rew];
                    }
                }

                File.WriteAllLines(path_gob_extracted + "common/gameprogression/goalstorewardsconverter.csv", csv_goalstorewards);
            }
            if (Randomize_Hub_Pads)
            {
                string[] csv_warppadinfo = File.ReadAllLines(path_gob_extracted + "common/gameprogression/warppadinfo.csv");
                string[] cur_line_split;
                string[] warp_types = new string[] { "earth1", "earth2", "earth3", "barin1", "barin2", "barin3", "fenom1", "fenom2", "fenom3", "teknee1", "teknee2", "teknee3", "velorace" };
                // TODO: add pad names too
                int target_warp = 0;
                int[] validrows = new int[] { 6,7,8,12,13,14,18,19,20,24,25,26,57}; //todo: cup entrances, hub warps, arenas, bosses (they aren't functional on non-boss-intended tracks)
                int cur_row = 0;

                for (int targetRow = 0; targetRow < validrows.Length; targetRow++)
                {
                    cur_row = validrows[targetRow];
                    if (csv_warppadinfo[cur_row] != "")
                    {
                        cur_line_split = csv_warppadinfo[cur_row].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        target_warp = randState.Next(0, warp_types.Length);
                        csv_warppadinfo[cur_row] = cur_line_split[0] + "," + cur_line_split[1] + "," + warp_types[target_warp];
                    }
                }

                File.WriteAllLines(path_gob_extracted + "common/gameprogression/warppadinfo.csv", csv_warppadinfo);
            }

            if (Randomize_Kart_Stats)
            {
                /* TODO
                string[] csv_kartphysicsbase = File.ReadAllLines(path_gob_extracted + "common/physics/kpbase.csv");
                string[] cur_line_split;
                float target_val = 0f;

                for (int cur_row = 0; cur_row < 158; cur_row++)
                {
                    if (csv_kartphysicsbase[cur_row] != "")
                    {
                        cur_line_split = csv_kartphysicsbase[cur_row].Split(new string[] { "," }, StringSplitOptions.None);
                        target_val = (float)randState.NextDouble();
                        csv_kartphysicsbase[cur_row] = target_val + "," + cur_line_split[1];
                    }
                }

                File.WriteAllLines(path_gob_extracted + "common/physics/kpbase.csv", csv_kartphysicsbase);
                */
            }
            if (Randomize_Character_Stats)
            {
                string[] csv_charactertypes = new string[] { "coco", "crash", "cortex", "crunch", "dingodile", "fakecrash", "ngin", "noxide", "ntrance", "ntropy", "polar", "pura", "realvelo", "tiny", "zam", "zem" };
                string[] csv_kartphysicscharacter;
                string[] cur_line_split;
                string cur_line;
                double target_val = 0;
                int m_Speed = 0;
                int m_Turn = 0;
                int m_Accel = 0;
                int m_MaxVal = 7;
                double temp_Speed = 0;
                double temp_Turn = 0;
                double temp_Accel = 0;

                for (int csv_pos = 0; csv_pos < csv_charactertypes.Length; csv_pos++)
                {
                    csv_kartphysicscharacter = File.ReadAllLines(path_gob_extracted + "common/physics/kp" + csv_charactertypes[csv_pos] + ".csv");

                    for (int cur_row = 0; cur_row < 48; cur_row++)
                    {
                        if (csv_kartphysicscharacter[cur_row] != "")
                        {
                            cur_line_split = csv_kartphysicscharacter[cur_row].Split(new string[] { "," }, StringSplitOptions.None);
                            target_val = randState.NextDouble() + 0.5;

                            if (cur_row == (int)KartPhysicsCharacterRows.m_MaxForwardSpeedNormal)
                            {
                                temp_Speed = target_val;
                            }
                            else if (cur_row == (int)KartPhysicsCharacterRows.m_AccelerationGainNormal)
                            {
                                temp_Accel = target_val;
                            }
                            else if (cur_row == (int)KartPhysicsCharacterRows.m_TurnRateNormal)
                            {
                                temp_Turn = target_val;
                            }

                            cur_line = String.Format("{0:0.#########}", target_val);
                            cur_line = cur_line.Replace(',','.'); // For some reason String.Format is still not enough
                            cur_line += ",";
                            for (int i = 1; i < cur_line_split.Length; i++)
                            {
                                cur_line += cur_line_split[i] + ",";
                            }
                            csv_kartphysicscharacter[cur_row] = cur_line;
                        }
                    }

                    m_Speed = (int)Math.Ceiling((temp_Speed/1.5) * m_MaxVal);
                    m_Accel = (int)Math.Ceiling((temp_Accel/1.5) * m_MaxVal);
                    m_Turn = (int)Math.Ceiling((temp_Turn/1.5) * m_MaxVal);

                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.m_UIStats_Acceleration] = m_Accel.ToString() + ",,,,,";
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.m_UIStats_Speed] = m_Speed.ToString() + ",,,,,";
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.m_UIStats_Turn] = m_Turn.ToString() + ",,,,,";
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.m_UIStats_MaxValue] = m_MaxVal.ToString() + ",,,,,";

                    File.WriteAllLines(path_gob_extracted + "common/physics/kp" + csv_charactertypes[csv_pos] + ".csv", csv_kartphysicscharacter);
                }

            }



            EndModProcess();
        }

        public void EndModProcess()
        {
            // Build GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract_out.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            GobExtract.StartInfo.Arguments = "Tools/ASSETS.GOB" + " " + "Tools/cml_extr" + " -create";
            GobExtract.Start();
            GobExtract.WaitForExit();

            // Fixes names for PS2, and moves the archive for convenience
            File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC", Program.ModProgram.extractedPath + "/ASSETS.GFC;1");
            File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB", Program.ModProgram.extractedPath + "/ASSETS.GOB;1");
            
            // Extraction cleanup
            if (Directory.Exists(path_gob_extracted))
            {
                DirectoryInfo di = new DirectoryInfo(path_gob_extracted);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_gob_extracted);
            }
            
        }
    }
}
