using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using CrateModLoader.GameSpecific.CNK;
//CNK Tools/API by BetaM, ManDude and eezstreet.

namespace CrateModLoader
{
    class Modder_CNK
    {
        public string gameName = "CNK";
        public string apiCredit = "Tools/API by BetaM, ManDude and eezstreet";
        public System.Drawing.Image gameIcon = Properties.Resources.icon_cnk;
        public bool ModMenuEnabled = true;
        public bool ModCratesSupported = true;
        public string[] modOptions = {
            "Randomize Adventure Hub Warp Pads",
            "Randomize Adventure Requirements & Rewards",
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
            RandomizeAdventureRequirements = 1,
            RandomizeCharacterStats = 2,
            RandomizeKartStats = 3,
            RandomizeAIKartStats = 4,
            RandomizeSurfaceParameters = 5,
            RandomizeWeaponPools = 6,
            RandomizeWumpaCrate = 7,
            DisableFadeout = 8,
            SpeedUpMaskHints = 9,
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
            else if (option == (int)CNK_Options.RandomizeAdventureRequirements)
            {
                Randomize_Hub_Requirements = value;
            }
        }

        public void OpenModMenu()
        {
            GameSpecific.ModMenu_CNK modMenu = new GameSpecific.ModMenu_CNK();
            modMenu.Owner = Program.ModProgramForm;
            modMenu.Show();
        }

        public Random randState = new Random();
        

        public void StartModProcess()
        {
            // Fixes names for PS2, and moves the archive for convenience
            //File.Move(Program.ModProgram.extractedPath + "/ASSETS.GFC;1", AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC");
            //File.Move(Program.ModProgram.extractedPath + "/ASSETS.GOB;1", AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB");
            File.Move(Program.ModProgram.extractedPath + "/ASSETS.GFC", AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC");
            File.Move(Program.ModProgram.extractedPath + "/ASSETS.GOB", AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB");

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

            randState = new Random(Program.ModProgram.randoSeed);

            bool Editing_CSV_AdventureTracksManager = false;
            bool Editing_CSV_GoalsToRewardsConverter = false;
            bool Editing_CSV_WarpPadInfo = false;
            bool Editing_CSV_KartPhysicsBase = false;
            bool Editing_CSV_CharacterPhysics = false;

            if (Randomize_Hub_Requirements)
            {
                Editing_CSV_AdventureTracksManager = true;
                Editing_CSV_GoalsToRewardsConverter = true;
                CNK_Data.CNK_Randomize_ReqsRewards();
            }
            if (Randomize_Hub_Pads)
            {
                Editing_CSV_WarpPadInfo = true;
                CNK_Data.CNK_Randomize_WarpPads();
            }
            if (Randomize_Kart_Stats)
            {
                Editing_CSV_KartPhysicsBase = true;
                CNK_Data.CNK_Randomize_KartStats(ref randState);
            }
            if (Randomize_Character_Stats)
            {
                Editing_CSV_CharacterPhysics = true;
                for (int i = 0; i < CNK_Data.DriverAmount; i++)
                {
                    CNK_Data.CNK_Randomize_CharacterStats(ref randState, i);
                }
            }

            

            if (Editing_CSV_AdventureTracksManager)
            {
                string[] csv_advtracksmanager = File.ReadAllLines(path_gob_extracted + "common/gameprogression/adventuretracksmanager.csv");
                List<string> csv_AdvTracksManager_LineList = new List<string>();
                for (int i = 0; i < CNK_Data.Adv_TracksManager_GridStartRow - 1; i++)
                {
                    csv_AdvTracksManager_LineList.Add(csv_advtracksmanager[i]);
                }

                string cur_line = "";
                for (int i = 0; i < CNK_Data.Adv_TracksManager_EntryList.Count; i++)
                {
                    cur_line = CNK_Data.PadInfoName[(int)CNK_Data.Adv_TracksManager_EntryList[i].PadName] + ",," + CNK_Data.SubModeName[(int)CNK_Data.Adv_TracksManager_EntryList[i].Submode] + ",," + CNK_Data.RewardName[(int)CNK_Data.Adv_TracksManager_EntryList[i].RewardNeeded] + ",," + CNK_Data.Adv_TracksManager_EntryList[i].NumberNeeded.ToString();
                    csv_AdvTracksManager_LineList.Add(cur_line);
                }
                csv_AdvTracksManager_LineList.Add("");

                csv_advtracksmanager = new string[csv_AdvTracksManager_LineList.Count];
                for (int i = 0; i < csv_AdvTracksManager_LineList.Count; i++)
                {
                    csv_advtracksmanager[i] = csv_AdvTracksManager_LineList[i];
                }

                File.WriteAllLines(path_gob_extracted + "common/gameprogression/adventuretracksmanager.csv", csv_advtracksmanager);
            }
            if (Editing_CSV_GoalsToRewardsConverter)
            {
                string[] csv_goalstorewards = File.ReadAllLines(path_gob_extracted + "common/gameprogression/goalstorewardsconverter.csv");

                List<string> csv_GoalsToRewards_LineList = new List<string>();
                for (int i = 0; i < 6; i++)
                {
                    csv_GoalsToRewards_LineList.Add(csv_goalstorewards[i]);
                }

                string cur_line = "";
                for (int i = 0; i < CNK_Data.Adv_GoalsToRewards_EntryList.Count; i++)
                {
                    cur_line = CNK_Data.TrackName[(int)CNK_Data.Adv_GoalsToRewards_EntryList[i].Track] + "," + CNK_Data.SubModeName[(int)CNK_Data.Adv_GoalsToRewards_EntryList[i].Submode] + "," + CNK_Data.RewardName[(int)CNK_Data.Adv_GoalsToRewards_EntryList[i].Reward];
                    csv_GoalsToRewards_LineList.Add(cur_line);
                }
                csv_GoalsToRewards_LineList.Add("end_rewards,,");
                csv_GoalsToRewards_LineList.Add("");

                csv_goalstorewards = new string[csv_GoalsToRewards_LineList.Count];
                for (int i = 0; i < csv_GoalsToRewards_LineList.Count; i++)
                {
                    csv_goalstorewards[i] = csv_GoalsToRewards_LineList[i];
                }

                File.WriteAllLines(path_gob_extracted + "common/gameprogression/goalstorewardsconverter.csv", csv_goalstorewards);
            }
            if (Editing_CSV_WarpPadInfo)
            {
                string[] csv_warppadinfo = File.ReadAllLines(path_gob_extracted + "common/gameprogression/warppadinfo.csv");

                List<string> csv_WarpPadInfo_LineList = new List<string>();
                for (int i = 0; i < 6; i++)
                {
                    csv_WarpPadInfo_LineList.Add(csv_warppadinfo[i]);
                }

                string cur_line = "";
                for (int i = 0; i < CNK_Data.Adv_WarpPadInfo_EntryList.Count; i++)
                {
                    cur_line = CNK_Data.PadInfoName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].PadName] + ",";
                    cur_line += CNK_Data.PadInfoDescTypes[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].PadDesc] + ",";
                    cur_line += CNK_Data.TrackName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].Track] + ",";
                    cur_line += CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].isWarpGate] + ",";
                    cur_line += CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].PrimaryActEvent] + ",";
                    cur_line += CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].SecondaryEvent] + ",";
                    cur_line += CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].LockedEvent] + ",";
                    cur_line += CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].LockedEvent2] + ",";
                    cur_line += CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[0]];
                    if (CNK_Data.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent.Length > 1)
                    {
                        cur_line += ";" + CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[1]];
                    }
                    if (CNK_Data.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent.Length > 2)
                    {
                        cur_line += ";" + CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[2]];
                    }
                    cur_line += ",";
                    cur_line += CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].RelicWonEvent] + ",";
                    cur_line += CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].TokenWonEvent[0]];
                    if (CNK_Data.Adv_WarpPadInfo_EntryList[i].TokenWonEvent.Length > 1)
                    {
                        cur_line += ";" + CNK_Data.PadInfoEventName[(int)CNK_Data.Adv_WarpPadInfo_EntryList[i].TokenWonEvent[1]];
                    }
                    csv_WarpPadInfo_LineList.Add(cur_line);
                }
                csv_WarpPadInfo_LineList.Add("end_padinfo,,,,,,,,,,");
                csv_WarpPadInfo_LineList.Add("");

                for (int i = 62; i < 79; i++)
                {
                    csv_WarpPadInfo_LineList.Add(csv_warppadinfo[i]);
                }

                csv_WarpPadInfo_LineList.Add("");

                csv_warppadinfo = new string[csv_WarpPadInfo_LineList.Count];
                for (int i = 0; i < csv_WarpPadInfo_LineList.Count; i++)
                {
                    csv_warppadinfo[i] = csv_WarpPadInfo_LineList[i];
                }

                File.WriteAllLines(path_gob_extracted + "common/gameprogression/warppadinfo.csv", csv_warppadinfo);
            }

            if (Editing_CSV_KartPhysicsBase)
            {

                string[] csv_kartphysicsbase = File.ReadAllLines(path_gob_extracted + "common/physics/kpbase.csv");

                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AccelerationGainNormal] = Float_To_CSV_Line(CNK_Data.m_AccelerationGainNormal);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AccelerationGainWumpa] = Float_To_CSV_Line(CNK_Data.m_AccelerationGainWumpa);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropHeight] = Float_To_CSV_Line(CNK_Data.m_AkuDropHeight);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropSpeed] = Float_To_CSV_Line(CNK_Data.m_AkuDropSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTime] = Float_To_CSV_Line(CNK_Data.m_AkuDropTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_CancelMinPercent] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_CancelMinPercent);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_DecHoldTime] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_DecHoldTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_DecSpeed] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_DecSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_IncSpeed] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_IncSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_MaxHoldTime] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_MaxHoldTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_MaxRepressTime] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_MaxRepressTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_Quadratic] = FloatArray_To_CSV_Line(CNK_Data.m_AkuDropTS_m_Quadratic);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInARowTimeTol] = Float_To_CSV_Line(CNK_Data.m_BoostInARowTimeTol);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_AKU_DROP] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_AKU_DROP);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_LARGE] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_JUMP_LARGE);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_MEDIUM] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_JUMP_MEDIUM);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_SMALL] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_JUMP_SMALL);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_PAD] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_PAD);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_1] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_SLIDE_1);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_2] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_SLIDE_2);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_3] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_SLIDE_3);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_START] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_START);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_SUPER_ENGINE] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_SUPER_ENGINE);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_TURBOBOOST);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST_JUICED] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_TURBOBOOST_JUICED);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostMaxImpulsePerSecond] = Float_To_CSV_Line(CNK_Data.m_BoostMaxImpulsePerSecond);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostMaxTimeCap] = Float_To_CSV_Line(CNK_Data.m_BoostMaxTimeCap);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostSlidePushAngle] = FloatArray_To_CSV_Line(CNK_Data.m_BoostSlidePushAngle);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostSlidePushTime] = Float_To_CSV_Line(CNK_Data.m_BoostSlidePushTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BrakeForce] = Float_To_CSV_Line(CNK_Data.m_BrakeForce);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_CollisionRadius] = Float_To_CSV_Line(CNK_Data.m_CollisionRadius);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_CollisionSphereOffset] = FloatArray_To_CSV_Line(CNK_Data.m_CollisionSphereOffset);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_CtfFlagMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_CtfFlagMaxForwardSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_CursedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_CursedMaxForwardSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutFriction] = FloatArray_To_CSV_Line(CNK_Data.m_DonutFriction);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutMinMaxSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_DonutMinMaxSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutTurnRateMax] = Float_To_CSV_Line(CNK_Data.m_DonutTurnRateMax);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutTurnRateMin] = Float_To_CSV_Line(CNK_Data.m_DonutTurnRateMin);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutTurnTotal] = Float_To_CSV_Line(CNK_Data.m_DonutTurnTotal);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DownforceGround] = Float_To_CSV_Line(CNK_Data.m_DownforceGround);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DownforceInAirMagLev] = Float_To_CSV_Line(CNK_Data.m_DownforceInAirMagLev);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DownforceMagLev] = Float_To_CSV_Line(CNK_Data.m_DownforceMagLev);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DownforceMagLevAirTime] = Float_To_CSV_Line(CNK_Data.m_DownforceMagLevAirTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DragMaxStrength] = Float_To_CSV_Line(CNK_Data.m_DragMaxStrength);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DragStrength] = Float_To_CSV_Line(CNK_Data.m_DragStrength);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_GravityAir] = Float_To_CSV_Line(CNK_Data.m_GravityAir);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_GravityGround] = Float_To_CSV_Line(CNK_Data.m_GravityGround);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HeightForBigAir] = Float_To_CSV_Line(CNK_Data.m_HeightForBigAir);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitByMissileFriction] = Float_To_CSV_Line(CNK_Data.m_HitByMissileFriction);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitSlowdownSpeedForce] = Float_To_CSV_Line(CNK_Data.m_HitSlowdownSpeedForce);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitSlowdownSpeedForceRev] = Float_To_CSV_Line(CNK_Data.m_HitSlowdownSpeedForceRev);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitSlowdownSpeedMin] = Float_To_CSV_Line(CNK_Data.m_HitSlowdownSpeedMin);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitStopAngle] = Float_To_CSV_Line(CNK_Data.m_HitStopAngle);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitStopSpeed] = Float_To_CSV_Line(CNK_Data.m_HitStopSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitUpSlideTol] = Float_To_CSV_Line(CNK_Data.m_HitUpSlideTol);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HiTurnLatFriction] = FloatArray_To_CSV_Line(CNK_Data.m_HiTurnLatFriction);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HiTurnStartAngle] = Float_To_CSV_Line(CNK_Data.m_HiTurnStartAngle);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitWallLatFricLoss] = Float_To_CSV_Line(CNK_Data.m_HitWallLatFricLoss);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitWallLatMaxAng] = Float_To_CSV_Line(CNK_Data.m_HitWallLatMaxAng);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitWallLatMinAng] = Float_To_CSV_Line(CNK_Data.m_HitWallLatMinAng);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InAirFriction] = FloatArray_To_CSV_Line(CNK_Data.m_InAirFriction);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InAirMinSpeed] = Float_To_CSV_Line(CNK_Data.m_InAirMinSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InAirTurnRateNormal] = Float_To_CSV_Line(CNK_Data.m_InAirTurnRateNormal);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InAirTurnRateWumpa] = Float_To_CSV_Line(CNK_Data.m_InAirTurnRateWumpa);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InvincibiliyMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_InvincibiliyMaxForwardSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpAirTolerance] = Float_To_CSV_Line(CNK_Data.m_JumpAirTolerance);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpBeforeAirTimeTol] = Float_To_CSV_Line(CNK_Data.m_JumpBeforeAirTimeTol);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseBase] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseBase);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseBaseMagLev] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseBaseMagLev);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseUpMax] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseUpMax);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseUpMin] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseUpMin);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseUpPercent] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseUpPercent);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpMaxUpVelocity] = Float_To_CSV_Line(CNK_Data.m_JumpMaxUpVelocity);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpTimeInAirBoost] = FloatArray_To_CSV_Line(CNK_Data.m_JumpTimeInAirBoost);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_LowSpeed] = Float_To_CSV_Line(CNK_Data.m_LowSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxForwardSpeedNormal] = Float_To_CSV_Line(CNK_Data.m_MaxForwardSpeedNormal);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxForwardSpeedWumpa] = Float_To_CSV_Line(CNK_Data.m_MaxForwardSpeedWumpa);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxLinearVelXY] = Float_To_CSV_Line(CNK_Data.m_MaxLinearVelXY);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxLinearVelZ] = Float_To_CSV_Line(CNK_Data.m_MaxLinearVelZ);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxReverseSpeed] = Float_To_CSV_Line(CNK_Data.m_MaxReverseSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MinHeightForAirNoJump] = Float_To_CSV_Line(CNK_Data.m_MinHeightForAirNoJump);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_NormalFriction] = FloatArray_To_CSV_Line(CNK_Data.m_NormalFriction);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ResetGravStrength] = Float_To_CSV_Line(CNK_Data.m_ResetGravStrength);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ResetMaxTime] = Float_To_CSV_Line(CNK_Data.m_ResetMaxTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ResetWaitBeforeDrop] = Float_To_CSV_Line(CNK_Data.m_ResetWaitBeforeDrop);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ReverseGain] = Float_To_CSV_Line(CNK_Data.m_ReverseGain);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ShockedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_ShockedMaxForwardSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideBoostQuadratic] = FloatArray_To_CSV_Line(CNK_Data.m_SlideBoostQuadratic);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideBoostTime] = Float_To_CSV_Line(CNK_Data.m_SlideBoostTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEaseInSpeed] = Float_To_CSV_Line(CNK_Data.m_SlideEaseInSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEaseOutPercentBetween] = FloatArray_To_CSV_Line(CNK_Data.m_SlideEaseOutPercentBetween);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEaseOutRotVelSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_SlideEaseOutRotVelSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEaseOutSpeed] = Float_To_CSV_Line(CNK_Data.m_SlideEaseOutSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEndMaxTime] = Float_To_CSV_Line(CNK_Data.m_SlideEndMaxTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEndReduceTime] = Float_To_CSV_Line(CNK_Data.m_SlideEndReduceTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideFrictionHigh] = FloatArray_To_CSV_Line(CNK_Data.m_SlideFrictionHigh);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideFrictionLow] = FloatArray_To_CSV_Line(CNK_Data.m_SlideFrictionLow);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideFrictionNorm] = FloatArray_To_CSV_Line(CNK_Data.m_SlideFrictionNorm);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideMaxAngle] = Float_To_CSV_Line(CNK_Data.m_SlideMaxAngle);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideMaxBoostCount] = Int_To_CSV_Line(CNK_Data.m_SlideMaxBoostCount);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideMinAngle] = Float_To_CSV_Line(CNK_Data.m_SlideMinAngle);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideMinimumSpeed] = Float_To_CSV_Line(CNK_Data.m_SlideMinimumSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideStartMinSteer] = Float_To_CSV_Line(CNK_Data.m_SlideStartMinSteer);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideTurnRateAwayFromSlide] = Float_To_CSV_Line(CNK_Data.m_SlideTurnRateAwayFromSlide);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideTurnRateInToSlide] = Float_To_CSV_Line(CNK_Data.m_SlideTurnRateInToSlide);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlopeAccelExtra] = Float_To_CSV_Line(CNK_Data.m_SlopeAccelExtra);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlopeMaxAngle] = Float_To_CSV_Line(CNK_Data.m_SlopeMaxAngle);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlopeMinAngle] = Float_To_CSV_Line(CNK_Data.m_SlopeMinAngle);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpikeyFruitMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_SpikeyFruitMaxForwardSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutFriction] = FloatArray_To_CSV_Line(CNK_Data.m_SpinOutFriction);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutTotalLarge] = Float_To_CSV_Line(CNK_Data.m_SpinOutTotalLarge);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutTotalNormal] = Float_To_CSV_Line(CNK_Data.m_SpinOutTotalNormal);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutTurnRateMax] = Float_To_CSV_Line(CNK_Data.m_SpinOutTurnRateMax);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutTurnRateMin] = Float_To_CSV_Line(CNK_Data.m_SpinOutTurnRateMin);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SquashedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_SquashedMaxForwardSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_CancelMinPercent] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_CancelMinPercent);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_DecHoldTime] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_DecHoldTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_DecSpeed] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_DecSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_IncSpeed] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_IncSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_MaxHoldTime] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_MaxHoldTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_MaxRepressTime] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_MaxRepressTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_Quadratic] = FloatArray_To_CSV_Line(CNK_Data.m_StartLineTS_m_Quadratic);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TimeBubbleMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_TimeBubbleMaxForwardSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TropyClocksMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_TropyClocksMaxForwardSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnDecellForce] = Float_To_CSV_Line(CNK_Data.m_TurnDecellForce);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnDecellForceMax] = Float_To_CSV_Line(CNK_Data.m_TurnDecellForceMax);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnDecellSpeed] = Float_To_CSV_Line(CNK_Data.m_TurnDecellSpeed);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnRateAccel] = Float_To_CSV_Line(CNK_Data.m_TurnRateAccel);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnRateBrake] = Float_To_CSV_Line(CNK_Data.m_TurnRateBrake);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnRateNormal] = Float_To_CSV_Line(CNK_Data.m_TurnRateNormal);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnRateWumpa] = Float_To_CSV_Line(CNK_Data.m_TurnRateWumpa);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_WaitBeforeBrakeReverses] = Float_To_CSV_Line(CNK_Data.m_WaitBeforeBrakeReverses);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_WheelieMinTime] = Float_To_CSV_Line(CNK_Data.m_WheelieMinTime);
                csv_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_WheelieSlideBoostMinPercent] = Float_To_CSV_Line(CNK_Data.m_WheelieSlideBoostMinPercent);

                File.WriteAllLines(path_gob_extracted + "common/physics/kpbase.csv", csv_kartphysicsbase);
                
            }
            if (Editing_CSV_CharacterPhysics)
            {
                string[] csv_kartphysicscharacter;

                for (int csv_pos = 0; csv_pos < CNK_Data.DriverTypes.Length; csv_pos++)
                {
                    csv_kartphysicscharacter = File.ReadAllLines(path_gob_extracted + "common/physics/kp" + CNK_Data.DriverTypes[csv_pos] + ".csv");

                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_AccelerationGainNormal] = Float_To_CSV_Line(CNK_Data.c_AccelerationGainNormal[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_AccelerationGainWumpa] = Float_To_CSV_Line(CNK_Data.c_AccelerationGainWumpa[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_AKU_DROP] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_AKU_DROP, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_LARGE] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_JUMP_LARGE, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_MEDIUM] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_JUMP_MEDIUM, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_SMALL] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_JUMP_SMALL, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_PAD] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_PAD, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_1] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_SLIDE_1, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_2] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_SLIDE_2, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_3] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_SLIDE_3, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_START] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_START, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SUPER_ENGINE] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_SUPER_ENGINE, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_TURBOBOOST] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_TURBOBOOST, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostInfo_eBOOST_TURBOBOOST_JUICED] = FloatArray2_To_CSV_Line(CNK_Data.c_BoostInfo_eBOOST_TURBOBOOST_JUICED, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostMaxImpulsePerSecond] = Float_To_CSV_Line(CNK_Data.c_BoostMaxImpulsePerSecond[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostSlidePushAngle] = Float_To_CSV_Line(CNK_Data.c_BoostSlidePushAngle[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BoostSlidePushTime] = Float_To_CSV_Line(CNK_Data.c_BoostSlidePushTime[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_BrakeForce] = Float_To_CSV_Line(CNK_Data.c_BrakeForce[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_HiTurnFriction] = FloatArray2_To_CSV_Line(CNK_Data.c_HiTurnFriction, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_HiTurnStartAngle] = Float_To_CSV_Line(CNK_Data.c_HiTurnStartAngle[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_InAirTurnRateNormal] = Float_To_CSV_Line(CNK_Data.c_InAirTurnRateNormal[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_InAirTurnRateWumpa] = Float_To_CSV_Line(CNK_Data.c_InAirTurnRateWumpa[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_MaxForwardSpeedNormal] = Float_To_CSV_Line(CNK_Data.c_MaxForwardSpeedNormal[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_MaxForwardSpeedWumpa] = Float_To_CSV_Line(CNK_Data.c_MaxForwardSpeedWumpa[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_NormalFriction] = FloatArray2_To_CSV_Line(CNK_Data.c_NormalFriction, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_SlideFrictionHigh] = FloatArray2_To_CSV_Line(CNK_Data.c_SlideFrictionHigh, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_SlideFrictionLow] = FloatArray2_To_CSV_Line(CNK_Data.c_SlideFrictionLow, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_SlideFrictionNorm] = FloatArray2_To_CSV_Line(CNK_Data.c_SlideFrictionNorm, csv_pos);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_SlideMaxAngle] = Float_To_CSV_Line(CNK_Data.c_SlideMaxAngle[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_SlideMinAngle] = Float_To_CSV_Line(CNK_Data.c_SlideMinAngle[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_SlideTurnRateAwayFromSlide] = Float_To_CSV_Line(CNK_Data.c_SlideTurnRateAwayFromSlide[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_SlideTurnRateInToSlide] = Float_To_CSV_Line(CNK_Data.c_SlideTurnRateInToSlide[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_TurnDecellForce] = Float_To_CSV_Line(CNK_Data.c_TurnDecellForce[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_TurnDecellForceMax] = Float_To_CSV_Line(CNK_Data.c_TurnDecellForceMax[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_TurnDecellSpeed] = Float_To_CSV_Line(CNK_Data.c_TurnDecellSpeed[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_TurnRateAccel] = Float_To_CSV_Line(CNK_Data.c_TurnRateAccel[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_TurnRateBrake] = Float_To_CSV_Line(CNK_Data.c_TurnRateBrake[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_TurnRateNormal] = Float_To_CSV_Line(CNK_Data.c_TurnRateNormal[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_TurnRateWumpa] = Float_To_CSV_Line(CNK_Data.c_TurnRateWumpa[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_UIStats_Acceleration] = Float_To_CSV_Line(CNK_Data.c_UIStats_Acceleration[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_UIStats_Speed] = Float_To_CSV_Line(CNK_Data.c_UIStats_Speed[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_UIStats_Turn] = Float_To_CSV_Line(CNK_Data.c_UIStats_Turn[csv_pos]);
                    csv_kartphysicscharacter[(int)CNK_Data.KartPhysicsCharacterRows.c_UIStats_MaxValue] = Float_To_CSV_Line(CNK_Data.c_UIStats_MaxValue[csv_pos]);

                    File.WriteAllLines(path_gob_extracted + "common/physics/kp" + CNK_Data.DriverTypes[csv_pos] + ".csv", csv_kartphysicscharacter);
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
            //File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC", Program.ModProgram.extractedPath + "/ASSETS.GFC;1");
            //File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB", Program.ModProgram.extractedPath + "/ASSETS.GOB;1");
            File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GFC", Program.ModProgram.extractedPath + "/ASSETS.GFC");
            File.Move(AppDomain.CurrentDomain.BaseDirectory + "/Tools/ASSETS.GOB", Program.ModProgram.extractedPath + "/ASSETS.GOB");

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

        string Float_To_CSV_Line(float targetfloat)
        {
            string cur_line = String.Format("{0:0.#########}", targetfloat);
            cur_line = cur_line.Replace(',', '.'); // For some reason String.Format is still not enough
            cur_line += ",";
            return cur_line;
        }
        string FloatArray_To_CSV_Line(float[] targetfloat)
        {
            string cur_line = "";
            string[] line_vars = new string[targetfloat.Length];
            for (int i = 0; i < targetfloat.Length; i++)
            {
                line_vars[i] = String.Format("{0:0.#########}", targetfloat[i]);
                line_vars[i] = line_vars[i].Replace(',', '.'); // For some reason String.Format is still not enough
            }
            for (int i = 0; i < targetfloat.Length; i++)
            {
                cur_line += line_vars[i];
                cur_line += ",";
            }
            cur_line += ",";
            return cur_line;
        }
        string FloatArray2_To_CSV_Line(float[,] targetfloat, int targetCharacter)
        {
            string cur_line = "";
            string[] line_vars = new string[targetfloat.GetLength(1)];
            for (int i = 0; i < targetfloat.GetLength(1); i++)
            {
                line_vars[i] = String.Format("{0:0.#########}", targetfloat[targetCharacter, i]);
                line_vars[i] = line_vars[i].Replace(',', '.'); // For some reason String.Format is still not enough
            }
            for (int i = 0; i < targetfloat.GetLength(1); i++)
            {
                cur_line += line_vars[i];
                cur_line += ",";
            }
            cur_line += ",";
            return cur_line;
        }
        string Int_To_CSV_Line(int targetInt)
        {
            string cur_line = targetInt.ToString();
            cur_line += ",";
            return cur_line;
        }
    }
}
