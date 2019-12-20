﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using CrateModLoader.GameSpecific.CNK;
//CNK Tools/API by BetaM, ManDude and eezstreet.
//Version number, seed and options are displayed in the Credits accessible from the main menu.

namespace CrateModLoader
{
    class Modder_CNK
    {

        public string[] modOptions = {
            "Randomize Adventure Hub Warp Pads & Cups",
            "Randomize Adventure Requirements & Rewards",
            "Randomize Character Stats",
            "Randomize Kart Stats",
            "Randomize AI Kart Stats",
            "Randomize Surface Parameters",
            "Randomize Powerup Distribution",
            "Randomize Powerup Effects",
            "Disable Fadeout Overlay",
            "Disable Unlock Popups",
            "Speed Up Mask Hints",
        };

        public bool Randomize_Hub_Pads = false;
        public bool Randomize_Hub_Requirements = false;
        public bool Randomize_Character_Stats = false;
        public bool Randomize_Kart_Stats = false;
        public bool Randomize_AI_Kart_Stats = false;
        //public bool Randomize_Wumpa_Crate = false; //TODO dda
        public bool Randomize_Surface_Parameters = false;
        public bool Randomize_Weapon_Pools = false;
        //public bool Randomize_Obstacles = false; //TODO obstacles
        //public bool Randomize_Cup_Points = false; // Maybe? gameprogression
        public bool Randomize_Weapons = false;
        public bool Mod_SpeedUp_Mask_Hints = false;
        public bool Mod_Disable_Fadeout = false;
        //public bool Mod_Mask_Hints_NoMask = false; //TODO, hinthistory.csv
        public bool Mod_Disable_Unlock_Popups = false;
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
            RandomizeWeapons = 7,
            DisableFadeout = 8,
            DisablePopups = 9,
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
            else if (option == (int)CNK_Options.RandomizeWeapons)
            {
                Randomize_Weapons = value;
            }
            else if (option == (int)CNK_Options.SpeedUpMaskHints)
            {
                Mod_SpeedUp_Mask_Hints = value;
            }
            else if (option == (int)CNK_Options.RandomizeAdventureRequirements)
            {
                Randomize_Hub_Requirements = value;
            }
            else if (option == (int)CNK_Options.RandomizeWeaponPools)
            {
                Randomize_Weapon_Pools = value;
            }
            else if (option == (int)CNK_Options.RandomizeSurfaceParameters)
            {
                Randomize_Surface_Parameters = value;
            }
            else if (option == (int)CNK_Options.DisablePopups)
            {
                Mod_Disable_Unlock_Popups = value;
            }
        }

        public void UpdateModOptions()
        {
            Program.ModProgram.PrepareOptionsList(modOptions);
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
            GobExtract.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract.exe";
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
            bool Editing_CSV_AI_KartPhysicsBase = false;
            bool Editing_CSV_CharacterPhysics = false;
            bool Editing_CSV_SurfaceParams = false;
            bool Editing_CSV_AdventureCup = false;
            bool Editing_CSV_Unlockables = false;
            bool Editing_CSV_HintsConfig = false;
            bool Editing_CSV_PowerShield = false;
            bool Editing_CSV_BowlingBomb = false;
            bool Editing_CSV_FreezingMine = false;
            bool Editing_CSV_HomingMissle = false;
            bool Editing_CSV_InvincMask = false;
            bool Editing_CSV_RedEye = false;
            bool Editing_CSV_TNT = false;
            bool Editing_CSV_Tornado = false;
            bool Editing_CSV_TurboBoost = false;
            bool Editing_CSV_StaticShock = false;
            bool Editing_CSV_PlayerWeaponSelection = false;
            //bool Editing_CSV_PlayerWeaponSelection_Battle = false;
            bool Editing_CSV_PlayerWeaponSelection_Boss = false;
            //bool Editing_CSV_AI_WeaponSelection = false;
            bool Editing_CSV_Credits = true;

            if (Randomize_Hub_Requirements)
            {
                Editing_CSV_AdventureTracksManager = true;
                Editing_CSV_GoalsToRewardsConverter = true;
                CNK_Data.CNK_Randomize_ReqsRewards();
            }
            if (Randomize_Hub_Pads)
            {
                Editing_CSV_WarpPadInfo = true;
                Editing_CSV_AdventureCup = true;
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
            if (Randomize_Surface_Parameters)
            {
                Editing_CSV_SurfaceParams = true;
                CNK_Data.CNK_Randomize_SufParams(ref randState);
            }
            if (Randomize_Weapons)
            {
                Editing_CSV_PowerShield = true;
                Editing_CSV_FreezingMine = true;
                Editing_CSV_HomingMissle = true;
                Editing_CSV_InvincMask = true;
                Editing_CSV_RedEye = true;
                Editing_CSV_TNT = true;
                Editing_CSV_Tornado = true;
                Editing_CSV_TurboBoost = true;
                Editing_CSV_BowlingBomb = true;
                Editing_CSV_StaticShock = true;
                CNK_Data.CNK_Randomize_PowerShield(ref randState);
                CNK_Data.CNK_Randomize_BowlingBomb(ref randState);
                CNK_Data.CNK_Randomize_FreezingMine(ref randState);
                CNK_Data.CNK_Randomize_HomingMissle(ref randState);
                CNK_Data.CNK_Randomize_InvincMask(ref randState);
                CNK_Data.CNK_Randomize_RedEye(ref randState);
                CNK_Data.CNK_Randomize_TNTCrate(ref randState);
                CNK_Data.CNK_Randomize_Tornado(ref randState);
                CNK_Data.CNK_Randomize_TurboBoost(ref randState);
                CNK_Data.CNK_Randomize_StaticShock(ref randState);
            }
            if (Randomize_Weapon_Pools)
            {
                Editing_CSV_PlayerWeaponSelection = true;
                Editing_CSV_PlayerWeaponSelection_Boss = true;
            }
            if (Mod_Disable_Unlock_Popups)
            {
                Editing_CSV_Unlockables = true;
            }
            if (Mod_SpeedUp_Mask_Hints)
            {
                Editing_CSV_HintsConfig = true;
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

            if (Randomize_AI_Kart_Stats)
            {
                Editing_CSV_AI_KartPhysicsBase = true;
                CNK_Data.CNK_Randomize_KartStats(ref randState);
            }
            if (Editing_CSV_AI_KartPhysicsBase)
            {
                string[] csv_ai_kartphysicsbase = File.ReadAllLines(path_gob_extracted + "common/physics/kabase.csv");

                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AccelerationGainNormal] = Float_To_CSV_Line(CNK_Data.m_AccelerationGainNormal);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AccelerationGainWumpa] = Float_To_CSV_Line(CNK_Data.m_AccelerationGainWumpa);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropHeight] = Float_To_CSV_Line(CNK_Data.m_AkuDropHeight);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropSpeed] = Float_To_CSV_Line(CNK_Data.m_AkuDropSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTime] = Float_To_CSV_Line(CNK_Data.m_AkuDropTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_CancelMinPercent] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_CancelMinPercent);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_DecHoldTime] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_DecHoldTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_DecSpeed] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_DecSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_IncSpeed] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_IncSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_MaxHoldTime] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_MaxHoldTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_MaxRepressTime] = Float_To_CSV_Line(CNK_Data.m_AkuDropTS_m_MaxRepressTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_AkuDropTS_m_Quadratic] = FloatArray_To_CSV_Line(CNK_Data.m_AkuDropTS_m_Quadratic);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInARowTimeTol] = Float_To_CSV_Line(CNK_Data.m_BoostInARowTimeTol);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_AKU_DROP] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_AKU_DROP);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_LARGE] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_JUMP_LARGE);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_MEDIUM] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_JUMP_MEDIUM);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_SMALL] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_JUMP_SMALL);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_PAD] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_PAD);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_1] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_SLIDE_1);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_2] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_SLIDE_2);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_3] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_SLIDE_3);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_START] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_START);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_SUPER_ENGINE] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_SUPER_ENGINE);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_TURBOBOOST);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST_JUICED] = FloatArray_To_CSV_Line(CNK_Data.m_BoostInfo_eBOOST_TURBOBOOST_JUICED);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostMaxImpulsePerSecond] = Float_To_CSV_Line(CNK_Data.m_BoostMaxImpulsePerSecond);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostMaxTimeCap] = Float_To_CSV_Line(CNK_Data.m_BoostMaxTimeCap);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostSlidePushAngle] = FloatArray_To_CSV_Line(CNK_Data.m_BoostSlidePushAngle);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BoostSlidePushTime] = Float_To_CSV_Line(CNK_Data.m_BoostSlidePushTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_BrakeForce] = Float_To_CSV_Line(CNK_Data.m_BrakeForce);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_CollisionRadius] = Float_To_CSV_Line(CNK_Data.m_CollisionRadius);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_CollisionSphereOffset] = FloatArray_To_CSV_Line(CNK_Data.m_CollisionSphereOffset);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_CtfFlagMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_CtfFlagMaxForwardSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_CursedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_CursedMaxForwardSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutFriction] = FloatArray_To_CSV_Line(CNK_Data.m_DonutFriction);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutMinMaxSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_DonutMinMaxSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutTurnRateMax] = Float_To_CSV_Line(CNK_Data.m_DonutTurnRateMax);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutTurnRateMin] = Float_To_CSV_Line(CNK_Data.m_DonutTurnRateMin);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DonutTurnTotal] = Float_To_CSV_Line(CNK_Data.m_DonutTurnTotal);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DownforceGround] = Float_To_CSV_Line(CNK_Data.m_DownforceGround);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DownforceInAirMagLev] = Float_To_CSV_Line(CNK_Data.m_DownforceInAirMagLev);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DownforceMagLev] = Float_To_CSV_Line(CNK_Data.m_DownforceMagLev);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DownforceMagLevAirTime] = Float_To_CSV_Line(CNK_Data.m_DownforceMagLevAirTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DragMaxStrength] = Float_To_CSV_Line(CNK_Data.m_DragMaxStrength);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_DragStrength] = Float_To_CSV_Line(CNK_Data.m_DragStrength);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_GravityAir] = Float_To_CSV_Line(CNK_Data.m_GravityAir);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_GravityGround] = Float_To_CSV_Line(CNK_Data.m_GravityGround);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HeightForBigAir] = Float_To_CSV_Line(CNK_Data.m_HeightForBigAir);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitByMissileFriction] = Float_To_CSV_Line(CNK_Data.m_HitByMissileFriction);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitSlowdownSpeedForce] = Float_To_CSV_Line(CNK_Data.m_HitSlowdownSpeedForce);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitSlowdownSpeedForceRev] = Float_To_CSV_Line(CNK_Data.m_HitSlowdownSpeedForceRev);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitSlowdownSpeedMin] = Float_To_CSV_Line(CNK_Data.m_HitSlowdownSpeedMin);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitStopAngle] = Float_To_CSV_Line(CNK_Data.m_HitStopAngle);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitStopSpeed] = Float_To_CSV_Line(CNK_Data.m_HitStopSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitUpSlideTol] = Float_To_CSV_Line(CNK_Data.m_HitUpSlideTol);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HiTurnLatFriction] = FloatArray_To_CSV_Line(CNK_Data.m_HiTurnLatFriction);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HiTurnStartAngle] = Float_To_CSV_Line(CNK_Data.m_HiTurnStartAngle);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitWallLatFricLoss] = Float_To_CSV_Line(CNK_Data.m_HitWallLatFricLoss);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitWallLatMaxAng] = Float_To_CSV_Line(CNK_Data.m_HitWallLatMaxAng);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_HitWallLatMinAng] = Float_To_CSV_Line(CNK_Data.m_HitWallLatMinAng);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InAirFriction] = FloatArray_To_CSV_Line(CNK_Data.m_InAirFriction);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InAirMinSpeed] = Float_To_CSV_Line(CNK_Data.m_InAirMinSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InAirTurnRateNormal] = Float_To_CSV_Line(CNK_Data.m_InAirTurnRateNormal);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InAirTurnRateWumpa] = Float_To_CSV_Line(CNK_Data.m_InAirTurnRateWumpa);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_InvincibiliyMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_InvincibiliyMaxForwardSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpAirTolerance] = Float_To_CSV_Line(CNK_Data.m_JumpAirTolerance);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpBeforeAirTimeTol] = Float_To_CSV_Line(CNK_Data.m_JumpBeforeAirTimeTol);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseBase] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseBase);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseBaseMagLev] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseBaseMagLev);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseUpMax] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseUpMax);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseUpMin] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseUpMin);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpImpulseUpPercent] = Float_To_CSV_Line(CNK_Data.m_JumpImpulseUpPercent);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpMaxUpVelocity] = Float_To_CSV_Line(CNK_Data.m_JumpMaxUpVelocity);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_JumpTimeInAirBoost] = FloatArray_To_CSV_Line(CNK_Data.m_JumpTimeInAirBoost);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_LowSpeed] = Float_To_CSV_Line(CNK_Data.m_LowSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxForwardSpeedNormal] = Float_To_CSV_Line(CNK_Data.m_MaxForwardSpeedNormal);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxForwardSpeedWumpa] = Float_To_CSV_Line(CNK_Data.m_MaxForwardSpeedWumpa);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxLinearVelXY] = Float_To_CSV_Line(CNK_Data.m_MaxLinearVelXY);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxLinearVelZ] = Float_To_CSV_Line(CNK_Data.m_MaxLinearVelZ);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MaxReverseSpeed] = Float_To_CSV_Line(CNK_Data.m_MaxReverseSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_MinHeightForAirNoJump] = Float_To_CSV_Line(CNK_Data.m_MinHeightForAirNoJump);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_NormalFriction] = FloatArray_To_CSV_Line(CNK_Data.m_NormalFriction);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ResetGravStrength] = Float_To_CSV_Line(CNK_Data.m_ResetGravStrength);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ResetMaxTime] = Float_To_CSV_Line(CNK_Data.m_ResetMaxTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ResetWaitBeforeDrop] = Float_To_CSV_Line(CNK_Data.m_ResetWaitBeforeDrop);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ReverseGain] = Float_To_CSV_Line(CNK_Data.m_ReverseGain);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_ShockedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_ShockedMaxForwardSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideBoostQuadratic] = FloatArray_To_CSV_Line(CNK_Data.m_SlideBoostQuadratic);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideBoostTime] = Float_To_CSV_Line(CNK_Data.m_SlideBoostTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEaseInSpeed] = Float_To_CSV_Line(CNK_Data.m_SlideEaseInSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEaseOutPercentBetween] = FloatArray_To_CSV_Line(CNK_Data.m_SlideEaseOutPercentBetween);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEaseOutRotVelSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_SlideEaseOutRotVelSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEaseOutSpeed] = Float_To_CSV_Line(CNK_Data.m_SlideEaseOutSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEndMaxTime] = Float_To_CSV_Line(CNK_Data.m_SlideEndMaxTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideEndReduceTime] = Float_To_CSV_Line(CNK_Data.m_SlideEndReduceTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideFrictionHigh] = FloatArray_To_CSV_Line(CNK_Data.m_SlideFrictionHigh);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideFrictionLow] = FloatArray_To_CSV_Line(CNK_Data.m_SlideFrictionLow);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideFrictionNorm] = FloatArray_To_CSV_Line(CNK_Data.m_SlideFrictionNorm);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideMaxAngle] = Float_To_CSV_Line(CNK_Data.m_SlideMaxAngle);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideMaxBoostCount] = Int_To_CSV_Line(CNK_Data.m_SlideMaxBoostCount);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideMinAngle] = Float_To_CSV_Line(CNK_Data.m_SlideMinAngle);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideMinimumSpeed] = Float_To_CSV_Line(CNK_Data.m_SlideMinimumSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideStartMinSteer] = Float_To_CSV_Line(CNK_Data.m_SlideStartMinSteer);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideTurnRateAwayFromSlide] = Float_To_CSV_Line(CNK_Data.m_SlideTurnRateAwayFromSlide);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlideTurnRateInToSlide] = Float_To_CSV_Line(CNK_Data.m_SlideTurnRateInToSlide);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlopeAccelExtra] = Float_To_CSV_Line(CNK_Data.m_SlopeAccelExtra);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlopeMaxAngle] = Float_To_CSV_Line(CNK_Data.m_SlopeMaxAngle);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SlopeMinAngle] = Float_To_CSV_Line(CNK_Data.m_SlopeMinAngle);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpikeyFruitMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_SpikeyFruitMaxForwardSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutFriction] = FloatArray_To_CSV_Line(CNK_Data.m_SpinOutFriction);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutTotalLarge] = Float_To_CSV_Line(CNK_Data.m_SpinOutTotalLarge);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutTotalNormal] = Float_To_CSV_Line(CNK_Data.m_SpinOutTotalNormal);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutTurnRateMax] = Float_To_CSV_Line(CNK_Data.m_SpinOutTurnRateMax);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SpinOutTurnRateMin] = Float_To_CSV_Line(CNK_Data.m_SpinOutTurnRateMin);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_SquashedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_SquashedMaxForwardSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_CancelMinPercent] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_CancelMinPercent);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_DecHoldTime] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_DecHoldTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_DecSpeed] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_DecSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_IncSpeed] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_IncSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_MaxHoldTime] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_MaxHoldTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_MaxRepressTime] = Float_To_CSV_Line(CNK_Data.m_StartLineTS_m_MaxRepressTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_StartLineTS_m_Quadratic] = FloatArray_To_CSV_Line(CNK_Data.m_StartLineTS_m_Quadratic);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TimeBubbleMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_TimeBubbleMaxForwardSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TropyClocksMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data.m_TropyClocksMaxForwardSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnDecellForce] = Float_To_CSV_Line(CNK_Data.m_TurnDecellForce);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnDecellForceMax] = Float_To_CSV_Line(CNK_Data.m_TurnDecellForceMax);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnDecellSpeed] = Float_To_CSV_Line(CNK_Data.m_TurnDecellSpeed);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnRateAccel] = Float_To_CSV_Line(CNK_Data.m_TurnRateAccel);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnRateBrake] = Float_To_CSV_Line(CNK_Data.m_TurnRateBrake);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnRateNormal] = Float_To_CSV_Line(CNK_Data.m_TurnRateNormal);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_TurnRateWumpa] = Float_To_CSV_Line(CNK_Data.m_TurnRateWumpa);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_WaitBeforeBrakeReverses] = Float_To_CSV_Line(CNK_Data.m_WaitBeforeBrakeReverses);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_WheelieMinTime] = Float_To_CSV_Line(CNK_Data.m_WheelieMinTime);
                csv_ai_kartphysicsbase[(int)CNK_Data.KartPhysicsBaseRows.m_WheelieSlideBoostMinPercent] = Float_To_CSV_Line(CNK_Data.m_WheelieSlideBoostMinPercent);

                File.WriteAllLines(path_gob_extracted + "common/physics/kabase.csv", csv_ai_kartphysicsbase);
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

            if (Editing_CSV_SurfaceParams)
            {
                string[] csv_surfparams = File.ReadAllLines(path_gob_extracted + "common/physics/surfparm.csv");

                List<string> csv_surfparams_LineList = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    csv_surfparams_LineList.Add(csv_surfparams[i]);
                }

                string line_m_MinSpeedPercent = "";
                string line_m_SlowDownLongPercent = "";
                string line_m_SlowDownAccelPercent = "";
                string line_m_SlowDownBoostPercent = "";
                string line_m_SpeedBoostIncreasePercent = "";
                string line_m_BrakeLossPercent = "";
                string line_m_LatFrictionLossPercent = "";
                string line_m_LongFrictionLossPercent = "";
                string line_m_SlideFrictionLossPercent = "";
                string line_m_SpeedAccelIncreasePercent = "";
                string line_m_KartHeightOffset = "";

                string cur_line = "";
                for (int i = 0; i < CNK_Data.Surface_m_MinSpeedPercent.Length; i++)
                {
                    line_m_MinSpeedPercent = Float_To_CSV_Line(CNK_Data.Surface_m_MinSpeedPercent[i]);
                    line_m_SlowDownLongPercent = Float_To_CSV_Line(CNK_Data.Surface_m_SlowDownLongPercent[i]);
                    line_m_SlowDownAccelPercent = Float_To_CSV_Line(CNK_Data.Surface_m_SlowDownAccelPercent[i]);
                    line_m_SlowDownBoostPercent = Float_To_CSV_Line(CNK_Data.Surface_m_SlowDownBoostPercent[i]);
                    line_m_SpeedBoostIncreasePercent = Float_To_CSV_Line(CNK_Data.Surface_m_SpeedBoostIncreasePercent[i]);
                    line_m_BrakeLossPercent = Float_To_CSV_Line(CNK_Data.Surface_m_BrakeLossPercent[i]);
                    line_m_LatFrictionLossPercent = Float_To_CSV_Line(CNK_Data.Surface_m_LatFrictionLossPercent[i]);
                    line_m_LongFrictionLossPercent = Float_To_CSV_Line(CNK_Data.Surface_m_LongFrictionLossPercent[i]);
                    line_m_SlideFrictionLossPercent = Float_To_CSV_Line(CNK_Data.Surface_m_SlideFrictionLossPercent[i]);
                    line_m_SpeedAccelIncreasePercent = Float_To_CSV_Line(CNK_Data.Surface_m_SpeedAccelIncreasePercent[i]);
                    line_m_KartHeightOffset = Float_To_CSV_Line(CNK_Data.Surface_m_KartHeightOffset[i]);

                    cur_line += line_m_MinSpeedPercent;
                    cur_line += line_m_SlowDownLongPercent;
                    cur_line += line_m_SlowDownAccelPercent;
                    cur_line += line_m_SlowDownBoostPercent;
                    cur_line += line_m_SpeedBoostIncreasePercent;
                    cur_line += line_m_BrakeLossPercent;
                    cur_line += line_m_LatFrictionLossPercent;
                    cur_line += line_m_LongFrictionLossPercent;
                    cur_line += line_m_SlideFrictionLossPercent;
                    cur_line += line_m_SpeedAccelIncreasePercent;
                    cur_line += line_m_KartHeightOffset;

                    csv_surfparams_LineList.Add(cur_line);

                    cur_line = "";
                }
                csv_surfparams_LineList.Add("");

                csv_surfparams = new string[csv_surfparams_LineList.Count];
                for (int i = 0; i < csv_surfparams_LineList.Count; i++)
                {
                    csv_surfparams[i] = csv_surfparams_LineList[i];
                }

                File.WriteAllLines(path_gob_extracted + "common/physics/surfparm.csv", csv_surfparams);
            }

            if (Editing_CSV_AdventureCup)
            {
                string[] csv_AdventureCup = File.ReadAllLines(path_gob_extracted + "common/gameprogression/adventurecup.csv");

                List<string> csv_AdventureCup_LineList = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    csv_AdventureCup_LineList.Add(csv_AdventureCup[i]);
                }

                csv_AdventureCup_LineList.Add(CNK_Data.TrackName[(int)CNK_Data.GemCup_Red[0]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Green[0]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Blue[0]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Purple[0]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Yellow[0]] + ",");
                csv_AdventureCup_LineList.Add(CNK_Data.TrackName[(int)CNK_Data.GemCup_Red[1]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Green[1]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Blue[1]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Purple[1]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Yellow[1]] + ",");
                csv_AdventureCup_LineList.Add(CNK_Data.TrackName[(int)CNK_Data.GemCup_Red[2]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Green[2]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Blue[2]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Purple[2]] + "," + CNK_Data.TrackName[(int)CNK_Data.GemCup_Yellow[2]] + ",");
                csv_AdventureCup_LineList.Add("");

                csv_AdventureCup = new string[csv_AdventureCup_LineList.Count];
                for (int i = 0; i < csv_AdventureCup_LineList.Count; i++)
                {
                    csv_AdventureCup[i] = csv_AdventureCup_LineList[i];
                }

                File.WriteAllLines(path_gob_extracted + "common/gameprogression/adventurecup.csv", csv_AdventureCup);
            }

            if (Editing_CSV_PowerShield)
            {
                string[] csv_PowerShield = File.ReadAllLines(path_gob_extracted + "common/weapons/powershield.csv");

                csv_PowerShield[1] = Float_To_CSV_Line(CNK_Data.PowerShield_m_Time) + ",,,,# float ,# m_Time,";
                csv_PowerShield[2] = Float_To_CSV_Line(CNK_Data.PowerShield_m_RangeForZapping) + ",,,,# float,# m_RangeForZapping,\"#15, 10\"";
                csv_PowerShield[3] = Float_To_CSV_Line(CNK_Data.PowerShield_m_ZapSpeed) + ",,,,# float,# m_ZapSpeed,";
                csv_PowerShield[4] = FloatArray_To_CSV_Line(CNK_Data.PowerShield_m_ColorNonJuiced) + ",,# vec3f,# m_ColorNonJuiced,";
                csv_PowerShield[5] = FloatArray_To_CSV_Line(CNK_Data.PowerShield_m_ColorJuiced) + ",,# vec3f,# m_ColorJuiced,";
                csv_PowerShield[6] = Float_To_CSV_Line(CNK_Data.PowerShield_m_ColRadius) + ",,,,# float,# m_ColRadius,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/powershield.csv", csv_PowerShield);
            }
            if (Editing_CSV_BowlingBomb)
            {
                string[] csv_BowlingBomb = File.ReadAllLines(path_gob_extracted + "common/weapons/bowlingbomb.csv");

                csv_BowlingBomb[1] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_Speed) + ",,,,,,,";
                csv_BowlingBomb[2] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_Acceleration) + ",,,,,,,";
                csv_BowlingBomb[3] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_AccelerationJuiced) + ",,,,,,,";
                csv_BowlingBomb[4] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_Mass) + ",,,,,,,";
                csv_BowlingBomb[5] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_Radius) + ",,,,,,,";
                csv_BowlingBomb[6] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_AirGravity) + ",,,,,,,";
                csv_BowlingBomb[7] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_GroundGravity) + ",,,,,,,";
                csv_BowlingBomb[8] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_AirGravityMaglev) + ",,,,,,,";
                csv_BowlingBomb[9] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_GroundGravityMaglev) + ",,,,,,,";
                csv_BowlingBomb[10] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_TurnSpeed) + ",,,,,,,";
                csv_BowlingBomb[11] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_TurnSpeedJuiced) + ",,,,,,,";
                csv_BowlingBomb[12] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_ViewRange) + ",,,,,,,";
                csv_BowlingBomb[13] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_RangeInFront) + ",,,,,,,";
                csv_BowlingBomb[14] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_NormalWumpaLoss) + ",,,,,,,";
                csv_BowlingBomb[15] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_JuicedWumpaLoss) + ",,,,,,,";
                csv_BowlingBomb[16] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_ExplosionBlastRadius) + ",,,,,,,";
                csv_BowlingBomb[17] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_ExplosionBlastRadiusJuiced) + ",,,,,,,";
                csv_BowlingBomb[18] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_DragCoef) + ",,,,,,,";
                csv_BowlingBomb[19] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_EasyLatFriction) + ",,,,,,,";
                csv_BowlingBomb[20] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_EasyLongFriction) + ",,,,,,,";
                csv_BowlingBomb[21] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_HardLatFriction) + ",,,,,,,";
                csv_BowlingBomb[22] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_HardLongFriction) + ",,,,,,,";
                csv_BowlingBomb[23] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_BackSpeed) + ",,,,,,,";
                csv_BowlingBomb[24] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_ExplScale) + ",,,,,,,";
                csv_BowlingBomb[25] = Float_To_CSV_Line(CNK_Data.BowlingBomb_m_ExplScaleJuiced) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/bowlingbomb.csv", csv_BowlingBomb);
            }
            if (Editing_CSV_FreezingMine)
            {
                string[] csv_FreezingMine = File.ReadAllLines(path_gob_extracted + "common/weapons/freezingmine.csv");

                csv_FreezingMine[1] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_NormalFreezeTime) + ",,,,,,,";
                csv_FreezingMine[2] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_JuicedFreezeTime) + ",,,,,,,";
                csv_FreezingMine[3] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_NormalWumpaFruitLost) + ",,,,,,,";
                csv_FreezingMine[4] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_JuicedWumpaFruitLost) + ",,,,,,,";
                csv_FreezingMine[5] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_ThrowDistance) + ",,,,,,,";
                csv_FreezingMine[6] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_ThrowSpeedFactor) + ",,,,,,,";
                csv_FreezingMine[7] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_BlastRadius) + ",,,,,,,";
                csv_FreezingMine[8] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_BlastRadiusJuiced) + ",,,,,,,";
                csv_FreezingMine[9] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_ExplScale) + ",,,,,,,";
                csv_FreezingMine[10] = Float_To_CSV_Line(CNK_Data.FreezingMine_m_ExplScaleJuiced) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/freezingmine.csv", csv_FreezingMine);
            }
            if (Editing_CSV_HomingMissle)
            {
                string[] csv_HomingMissle = File.ReadAllLines(path_gob_extracted + "common/weapons/homingmissile.csv");

                csv_HomingMissle[1] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_TrackingFrontDistance) + ",,,,,,,";
                csv_HomingMissle[2] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_MaxSpeed) + ",,,,,,,";
                csv_HomingMissle[3] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_MaxSpeedJuiced) + ",,,,,,,";
                csv_HomingMissle[4] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_TimeLimit) + ",,,,,,,";
                csv_HomingMissle[5] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_AirGravityNormal) + ",,,,,,,";
                csv_HomingMissle[6] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_GroundGravityNormal) + ",,,,,,,";
                csv_HomingMissle[7] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_AirGravityMaglev) + ",,,,,,,";
                csv_HomingMissle[8] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_GroundGravityMaglev) + ",,,,,,,";
                csv_HomingMissle[9] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_Acceleration) + ",,,,,,,";
                csv_HomingMissle[10] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_AccelerationJuiced) + ",,,,,,,";
                csv_HomingMissle[11] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_TurnSpeed) + ",,,,,,,";
                csv_HomingMissle[12] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_TurnSpeedJuiced) + ",,,,,,,";
                csv_HomingMissle[13] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_Mass) + ",,,,,,,";
                csv_HomingMissle[14] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_Radius) + ",,,,,,,";
                csv_HomingMissle[15] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_DelayTrackingUpdate) + ",,,,,,,";
                csv_HomingMissle[16] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_ViewRange) + ",,,,,,,";
                csv_HomingMissle[17] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_RangeInFront) + ",,,,,,,";
                csv_HomingMissle[18] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_RangeInBack) + ",,,,,,,";
                csv_HomingMissle[19] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_NormalWumpaLoss) + ",,,,,,,";
                csv_HomingMissle[20] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_JuicedWumpaLoss) + ",,,,,,,";
                csv_HomingMissle[21] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_ExplosionBlastRadius) + ",,,,,,,";
                csv_HomingMissle[22] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_ExplosionBlastRadiusJuiced) + ",,,,,,,";
                csv_HomingMissle[23] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_DragCoef) + ",,,,,,,";
                csv_HomingMissle[24] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_EasyLatFriction) + ",,,,,,,";
                csv_HomingMissle[25] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_EasyLongFriction) + ",,,,,,,";
                csv_HomingMissle[26] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_HardLatFriction) + ",,,,,,,";
                csv_HomingMissle[27] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_HardLongFriction) + ",,,,,,,";
                csv_HomingMissle[28] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_DecayTime) + ",,,,,,,";
                csv_HomingMissle[29] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_DecaySpeed) + ",,,,,,,";
                csv_HomingMissle[30] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_DecayMin) + ",,,,,,,";
                csv_HomingMissle[31] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_ExplScale) + ",,,,,,,";
                csv_HomingMissle[32] = Float_To_CSV_Line(CNK_Data.HomingMissle_m_ExplScaleJuiced) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/homingmissile.csv", csv_HomingMissle);
            }
            if (Editing_CSV_InvincMask)
            {
                string[] csv_InvincMask = File.ReadAllLines(path_gob_extracted + "common/weapons/invincibilitymask.csv");

                csv_InvincMask[1] = Float_To_CSV_Line(CNK_Data.InvincMask_m_NormalTime) + ",,,,,,,";
                csv_InvincMask[2] = Float_To_CSV_Line(CNK_Data.InvincMask_m_JuicedTime) + ",,,,,,,";
                csv_InvincMask[3] = Float_To_CSV_Line(CNK_Data.InvincMask_m_NormalTimeTeamed) + ",,,,,,,";
                csv_InvincMask[4] = Float_To_CSV_Line(CNK_Data.InvincMask_m_JuicedTimeTeamed) + ",,,,,,,";
                csv_InvincMask[5] = Float_To_CSV_Line(CNK_Data.InvincMask_m_NormalWumpaLoss) + ",,,,,,,";
                csv_InvincMask[6] = Float_To_CSV_Line(CNK_Data.InvincMask_m_JuicedWumpaLoss) + ",,,,,,,";
                csv_InvincMask[7] = Float_To_CSV_Line(CNK_Data.InvincMask_m_TeamSpeed) + ",,,,,,,";
                csv_InvincMask[8] = Float_To_CSV_Line(CNK_Data.InvincMask_m_TeamBlastRange) + ",,,,,,,";
                csv_InvincMask[9] = Float_To_CSV_Line(CNK_Data.InvincMask_m_TeamMeterFull) + ",,,,,,,";
                csv_InvincMask[10] = Float_To_CSV_Line(CNK_Data.InvincMask_m_ExplScale) + ",,,,,,,";
                csv_InvincMask[11] = Float_To_CSV_Line(CNK_Data.InvincMask_m_ExplScaleJuiced) + ",,,,,,,";
                csv_InvincMask[12] = Float_To_CSV_Line(CNK_Data.InvincMask_m_ColRadius) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/invincibilitymask.csv", csv_InvincMask);
            }
            if (Editing_CSV_RedEye)
            {
                string[] csv_RedEye = File.ReadAllLines(path_gob_extracted + "common/weapons/redeye.csv");

                csv_RedEye[1] = Float_To_CSV_Line(CNK_Data.RedEye_Acceleration) + ",,,,,,,";
                csv_RedEye[2] = Float_To_CSV_Line(CNK_Data.RedEye_Deceleration) + ",,,,,,,";
                csv_RedEye[3] = Float_To_CSV_Line(CNK_Data.RedEye_MaxSpeed) + ",,,,,,,";
                csv_RedEye[4] = Float_To_CSV_Line(CNK_Data.RedEye_MinSpeed) + ",,,,,,,";
                csv_RedEye[5] = Float_To_CSV_Line(CNK_Data.RedEye_TurnSpeed) + ",,,,,,,";
                csv_RedEye[6] = Float_To_CSV_Line(CNK_Data.RedEye_Explosion_Radius) + ",,,,,,,";
                csv_RedEye[7] = Float_To_CSV_Line(CNK_Data.RedEye_TurnSpeedJuiced) + ",,,,,,,";
                csv_RedEye[8] = Float_To_CSV_Line(CNK_Data.RedEye_Explosion_Radius_Juiced) + ",,,,,,,";
                csv_RedEye[9] = Float_To_CSV_Line(CNK_Data.RedEye_Expl_Scale) + ",,,,,,,";
                csv_RedEye[10] = Float_To_CSV_Line(CNK_Data.RedEye_Expl_Scale_Juiced) + ",,,,,,,";
                csv_RedEye[11] = Float_To_CSV_Line(CNK_Data.RedEye_FullSpeedTurnSlowdown) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/redeye.csv", csv_RedEye);
            }
            if (Editing_CSV_TNT)
            {
                string[] csv_TNT = File.ReadAllLines(path_gob_extracted + "common/weapons/tntcrate.csv");

                csv_TNT[1] = Float_To_CSV_Line(CNK_Data.TNT_m_Time) + ",,,,,,,";
                csv_TNT[2] = Float_To_CSV_Line(CNK_Data.TNT_m_TimeBeforeHiddenChar) + ",,,,,,,";
                csv_TNT[3] = Float_To_CSV_Line(CNK_Data.TNT_m_TimeHiddenChar) + ",,,,,,,";
                csv_TNT[4] = Float_To_CSV_Line(CNK_Data.TNT_m_NormalWumpaLoss) + ",,,,,,,";
                csv_TNT[5] = Float_To_CSV_Line(CNK_Data.TNT_m_JuicedWumpaLoss) + ",,,,,,,";
                csv_TNT[6] = Float_To_CSV_Line(CNK_Data.TNT_m_ExplosionBlastRadius) + ",,,,,,,";
                csv_TNT[7] = Float_To_CSV_Line(CNK_Data.TNT_m_ExplScale) + ",,,,,,,";
                csv_TNT[10] = Float_To_CSV_Line(CNK_Data.TNT_m_ExplScaleJuiced) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/tntcrate.csv", csv_TNT);
            }
            if (Editing_CSV_Tornado)
            {
                string[] csv_Tornado = File.ReadAllLines(path_gob_extracted + "common/weapons/tornado.csv");

                csv_Tornado[1] = Float_To_CSV_Line(CNK_Data.Tornado_m_TrackingFrontDistance) + ",,,,,,,";
                csv_Tornado[2] = Float_To_CSV_Line(CNK_Data.Tornado_m_MaxSpeed) + ",,,,,,,";
                csv_Tornado[3] = Float_To_CSV_Line(CNK_Data.Tornado_m_MaxSpeedJuiced) + ",,,,,,,";
                csv_Tornado[4] = Float_To_CSV_Line(CNK_Data.Tornado_m_MaxSpeedWithKart) + ",,,,,,,";
                csv_Tornado[5] = Float_To_CSV_Line(CNK_Data.Tornado_m_TimeLimit) + ",,,,,,,";
                csv_Tornado[6] = Float_To_CSV_Line(CNK_Data.Tornado_m_AirGravity) + ",,,,,,,";
                csv_Tornado[7] = Float_To_CSV_Line(CNK_Data.Tornado_m_GroundGravity) + ",,,,,,,";
                csv_Tornado[8] = Float_To_CSV_Line(CNK_Data.Tornado_m_AirGravityMaglev) + ",,,,,,,";
                csv_Tornado[9] = Float_To_CSV_Line(CNK_Data.Tornado_m_GroundGravityMaglev) + ",,,,,,,";
                csv_Tornado[10] = Float_To_CSV_Line(CNK_Data.Tornado_m_Acceleration) + ",,,,,,,";
                csv_Tornado[11] = Float_To_CSV_Line(CNK_Data.Tornado_m_AccelerationJuiced) + ",,,,,,,";
                csv_Tornado[12] = Float_To_CSV_Line(CNK_Data.Tornado_m_TurnSpeed) + ",,,,,,,";
                csv_Tornado[13] = Float_To_CSV_Line(CNK_Data.Tornado_m_TurnSpeedJuiced) + ",,,,,,,";
                csv_Tornado[14] = Float_To_CSV_Line(CNK_Data.Tornado_m_Mass) + ",,,,,,,";
                csv_Tornado[15] = Float_To_CSV_Line(CNK_Data.Tornado_m_Radius) + ",,,,,,,";
                csv_Tornado[16] = Float_To_CSV_Line(CNK_Data.Tornado_m_DelayTrackingUpdate) + ",,,,,,,";
                csv_Tornado[17] = Float_To_CSV_Line(CNK_Data.Tornado_m_ViewRange) + ",,,,,,,";
                csv_Tornado[18] = Float_To_CSV_Line(CNK_Data.Tornado_m_RangeInFront) + ",,,,,,,";
                csv_Tornado[19] = Float_To_CSV_Line(CNK_Data.Tornado_m_RangeInBack) + ",,,,,,,";
                csv_Tornado[20] = Float_To_CSV_Line(CNK_Data.Tornado_m_LiftTime) + ",,,,,,,";
                csv_Tornado[21] = Float_To_CSV_Line(CNK_Data.Tornado_m_LiftForce) + ",,,,,,,";
                csv_Tornado[22] = Float_To_CSV_Line(CNK_Data.Tornado_m_FizzleTime) + ",,,,,,,";
                csv_Tornado[23] = Float_To_CSV_Line(CNK_Data.Tornado_m_NormalWumpaLoss) + ",,,,,,,";
                csv_Tornado[24] = Float_To_CSV_Line(CNK_Data.Tornado_m_JuicedWumpaLoss) + ",,,,,,,";
                csv_Tornado[25] = Float_To_CSV_Line(CNK_Data.Tornado_m_DragCoef) + ",,,,,,,";
                csv_Tornado[26] = Float_To_CSV_Line(CNK_Data.Tornado_m_EasyLatFriction) + ",,,,,,,";
                csv_Tornado[27] = Float_To_CSV_Line(CNK_Data.Tornado_m_EasyLongFriction) + ",,,,,,,";
                csv_Tornado[28] = Float_To_CSV_Line(CNK_Data.Tornado_m_HardLatFriction) + ",,,,,,,";
                csv_Tornado[29] = Float_To_CSV_Line(CNK_Data.Tornado_m_HardLongFriction) + ",,,,,,,";
                csv_Tornado[30] = Float_To_CSV_Line(CNK_Data.Tornado_m_TargetAllDistance) + ",,,,,,,";
                csv_Tornado[31] = Float_To_CSV_Line(CNK_Data.Tornado_m_ViewRangleOfTarget) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/tornado.csv", csv_Tornado);
            }
            if (Editing_CSV_TurboBoost)
            {
                string[] csv_TurboBoost = File.ReadAllLines(path_gob_extracted + "common/weapons/turboboost.csv");

                csv_TurboBoost[1] = Float_To_CSV_Line(CNK_Data.TurboBoost_m_NormalTime) + ",,,,,,,";
                csv_TurboBoost[2] = Float_To_CSV_Line(CNK_Data.TurboBoost_m_JuicedTime) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/turboboost.csv", csv_TurboBoost);
            }
            if (Editing_CSV_StaticShock)
            {
                string[] csv_StaticShock = File.ReadAllLines(path_gob_extracted + "common/weapons/staticshock.csv");

                csv_StaticShock[1] = Float_To_CSV_Line(CNK_Data.StaticShock_m_NormalTime) + ",,,,,,,";
                csv_StaticShock[2] = Float_To_CSV_Line(CNK_Data.StaticShock_m_JuicedTime) + ",,,,,,,";
                csv_StaticShock[3] = Float_To_CSV_Line(CNK_Data.StaticShock_m_NormalWumpaLoss) + ",,,,,,,";
                csv_StaticShock[4] = Float_To_CSV_Line(CNK_Data.StaticShock_m_JuicedWumpaLoss) + ",,,,,,,";
                csv_StaticShock[5] = Float_To_CSV_Line(CNK_Data.StaticShock_m_HomingSpeed) + ",,,,,,,";
                csv_StaticShock[6] = Float_To_CSV_Line(CNK_Data.StaticShock_m_DistanceForHome) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/staticshock.csv", csv_StaticShock);
            }

            if (Editing_CSV_PlayerWeaponSelection)
            {
                string[] csv_PlayerWeaponSel = File.ReadAllLines(path_gob_extracted + "common/dda/playerweaponselection.csv");

                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Earth_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_1, ref CNK_Data.WeaponSelection_Track_Earth_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Earth_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_2, ref CNK_Data.WeaponSelection_Track_Earth_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Earth_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_3, ref CNK_Data.WeaponSelection_Track_Earth_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Barin_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_1, ref CNK_Data.WeaponSelection_Track_Barin_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Barin_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_2, ref CNK_Data.WeaponSelection_Track_Barin_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Barin_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_3, ref CNK_Data.WeaponSelection_Track_Barin_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_1, ref CNK_Data.WeaponSelection_Track_Fenom_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_2, ref CNK_Data.WeaponSelection_Track_Fenom_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_3, ref CNK_Data.WeaponSelection_Track_Fenom_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_1, ref CNK_Data.WeaponSelection_Track_Teknee_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_2, ref CNK_Data.WeaponSelection_Track_Teknee_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_3, ref CNK_Data.WeaponSelection_Track_Teknee_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_VeloRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_VeloRace, ref CNK_Data.WeaponSelection_Track_VeloRace);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_1, ref CNK_Data.WeaponSelection_Track_Arena_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_2, ref CNK_Data.WeaponSelection_Track_Arena_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_3, ref CNK_Data.WeaponSelection_Track_Arena_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_4, ref CNK_Data.WeaponSelection_Track_Arena_4);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_5, ref CNK_Data.WeaponSelection_Track_Arena_5);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_6, ref CNK_Data.WeaponSelection_Track_Arena_6);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_7, ref CNK_Data.WeaponSelection_Track_Arena_7);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Lobby, ref CNK_Data.WeaponSelection_Track_Lobby);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Trophy] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Trophy, ref CNK_Data.WeaponSelection_Mode_Adv_Trophy);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_CNK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_CNK, ref CNK_Data.WeaponSelection_Mode_Adv_CNK);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Gem] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Gem, ref CNK_Data.WeaponSelection_Mode_Adv_Gem);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Boss] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Boss, ref CNK_Data.WeaponSelection_Mode_Adv_Boss);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Crystal, ref CNK_Data.WeaponSelection_Mode_Adv_Crystal);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Arcade] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Arcade, ref CNK_Data.WeaponSelection_Mode_Arcade);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Versus] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Versus, ref CNK_Data.WeaponSelection_Mode_Versus);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_CrystalRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_CrystalRace, ref CNK_Data.WeaponSelection_Mode_CrystalRace);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Point] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Point, ref CNK_Data.WeaponSelection_Mode_Battle_Point);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Time] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Time, ref CNK_Data.WeaponSelection_Mode_Battle_Time);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Domination] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Domination, ref CNK_Data.WeaponSelection_Mode_Battle_Domination);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_CTF] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_CTF, ref CNK_Data.WeaponSelection_Mode_Battle_CTF);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_KOTR] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_KOTR, ref CNK_Data.WeaponSelection_Mode_Battle_KOTR);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Crystal, ref CNK_Data.WeaponSelection_Mode_Battle_Crystal);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Lobby, ref CNK_Data.WeaponSelection_Mode_Lobby);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_1st] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_1st, ref CNK_Data.WeaponSelection_Rank_1st);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_2nd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_2nd, ref CNK_Data.WeaponSelection_Rank_2nd);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_3rd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_3rd, ref CNK_Data.WeaponSelection_Rank_3rd);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_4th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_4th, ref CNK_Data.WeaponSelection_Rank_4th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_5th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_5th, ref CNK_Data.WeaponSelection_Rank_5th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_6th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_6th, ref CNK_Data.WeaponSelection_Rank_6th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_7th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_7th, ref CNK_Data.WeaponSelection_Rank_7th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_8th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_8th, ref CNK_Data.WeaponSelection_Rank_8th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_0, ref CNK_Data.WeaponSelection_Progress_0);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_5, ref CNK_Data.WeaponSelection_Progress_5);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_10] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_10, ref CNK_Data.WeaponSelection_Progress_10);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_15] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_15, ref CNK_Data.WeaponSelection_Progress_15);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_20] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_20, ref CNK_Data.WeaponSelection_Progress_20);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_25] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_25, ref CNK_Data.WeaponSelection_Progress_25);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_30] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_30, ref CNK_Data.WeaponSelection_Progress_30);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_35] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_35, ref CNK_Data.WeaponSelection_Progress_35);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_40] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_40, ref CNK_Data.WeaponSelection_Progress_40);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_45] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_45, ref CNK_Data.WeaponSelection_Progress_45);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_50] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_50, ref CNK_Data.WeaponSelection_Progress_50);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_55] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_55, ref CNK_Data.WeaponSelection_Progress_55);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_60] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_60, ref CNK_Data.WeaponSelection_Progress_60);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_65] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_65, ref CNK_Data.WeaponSelection_Progress_65);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_70] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_70, ref CNK_Data.WeaponSelection_Progress_70);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_75] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_75, ref CNK_Data.WeaponSelection_Progress_75);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_80] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_80, ref CNK_Data.WeaponSelection_Progress_80);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_85] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_85, ref CNK_Data.WeaponSelection_Progress_85);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_90] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_90, ref CNK_Data.WeaponSelection_Progress_90);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_95] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_95, ref CNK_Data.WeaponSelection_Progress_95);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_CLEANINGFLUID, ref CNK_Data.WeaponSelection_ActivePower_CLEANINGFLUID);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_CURSED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_CURSED, ref CNK_Data.WeaponSelection_ActivePower_CURSED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, ref CNK_Data.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_GRACED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_GRACED, ref CNK_Data.WeaponSelection_ActivePower_GRACED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_ICED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_ICED, ref CNK_Data.WeaponSelection_ActivePower_ICED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, ref CNK_Data.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVISIBILITY, ref CNK_Data.WeaponSelection_ActivePower_INVISIBILITY);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVULNERABLE, ref CNK_Data.WeaponSelection_ActivePower_INVULNERABLE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_MIMECUBE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_MIMECUBE, ref CNK_Data.WeaponSelection_ActivePower_MIMECUBE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, ref CNK_Data.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_POWER_SHIELD, ref CNK_Data.WeaponSelection_ActivePower_POWER_SHIELD);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_RESETTING] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_RESETTING, ref CNK_Data.WeaponSelection_ActivePower_RESETTING);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_ROLLINGBRUSH, ref CNK_Data.WeaponSelection_ActivePower_ROLLINGBRUSH);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_SPIKYFRUIT, ref CNK_Data.WeaponSelection_ActivePower_SPIKYFRUIT);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_STATICSHOCKED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_STATICSHOCKED, ref CNK_Data.WeaponSelection_ActivePower_STATICSHOCKED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_SUPER_ENGINE, ref CNK_Data.WeaponSelection_ActivePower_SUPER_ENGINE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, ref CNK_Data.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TEETHSTRIP] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TEETHSTRIP, ref CNK_Data.WeaponSelection_ActivePower_TEETHSTRIP);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TIMEBUBBLE, ref CNK_Data.WeaponSelection_ActivePower_TIMEBUBBLE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TROPY_CLOCKS, ref CNK_Data.WeaponSelection_ActivePower_TROPY_CLOCKS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TURBO_BOOSTS, ref CNK_Data.WeaponSelection_ActivePower_TURBO_BOOSTS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_WINDUPJAW] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_WINDUPJAW, ref CNK_Data.WeaponSelection_ActivePower_WINDUPJAW);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB, ref CNK_Data.WeaponSelection_ActiveWep_BOWLING_BOMB);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, ref CNK_Data.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_EXPCRATE_X3, ref CNK_Data.WeaponSelection_ActiveWep_EXPCRATE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, ref CNK_Data.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, ref CNK_Data.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_FREEZING_MINE, ref CNK_Data.WeaponSelection_ActiveWep_FREEZING_MINE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE, ref CNK_Data.WeaponSelection_ActiveWep_HOMING_MISSLE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, ref CNK_Data.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, ref CNK_Data.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_INVISIBILITY, ref CNK_Data.WeaponSelection_ActiveWep_INVISIBILITY);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_POWER_SHIELD, ref CNK_Data.WeaponSelection_ActiveWep_POWER_SHIELD);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_REDEYE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_REDEYE, ref CNK_Data.WeaponSelection_ActiveWep_REDEYE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, ref CNK_Data.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_STATIC_SHOCK, ref CNK_Data.WeaponSelection_ActiveWep_STATIC_SHOCK);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_SUPER_ENGINE, ref CNK_Data.WeaponSelection_ActiveWep_SUPER_ENGINE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TORNADO] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TORNADO, ref CNK_Data.WeaponSelection_ActiveWep_TORNADO);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TROPY_CLOCK, ref CNK_Data.WeaponSelection_ActiveWep_TROPY_CLOCK);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, ref CNK_Data.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, ref CNK_Data.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_VOODOO_DOLL, ref CNK_Data.WeaponSelection_ActiveWep_VOODOO_DOLL);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_0, ref CNK_Data.WeaponSelection_KartsInFront_0);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_1, ref CNK_Data.WeaponSelection_KartsInFront_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_2, ref CNK_Data.WeaponSelection_KartsInFront_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_3, ref CNK_Data.WeaponSelection_KartsInFront_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_4, ref CNK_Data.WeaponSelection_KartsInFront_4);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_5, ref CNK_Data.WeaponSelection_KartsInFront_5);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_6, ref CNK_Data.WeaponSelection_KartsInFront_6);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_7, ref CNK_Data.WeaponSelection_KartsInFront_7);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_8, ref CNK_Data.WeaponSelection_KartsInFront_8);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_0, ref CNK_Data.WeaponSelection_KartsBehind_0);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_1, ref CNK_Data.WeaponSelection_KartsBehind_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_2, ref CNK_Data.WeaponSelection_KartsBehind_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_3, ref CNK_Data.WeaponSelection_KartsBehind_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_4, ref CNK_Data.WeaponSelection_KartsBehind_4);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_5, ref CNK_Data.WeaponSelection_KartsBehind_5);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_6, ref CNK_Data.WeaponSelection_KartsBehind_6);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_7, ref CNK_Data.WeaponSelection_KartsBehind_7);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_8, ref CNK_Data.WeaponSelection_KartsBehind_8);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Difficulty_Easiest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Difficulty_Easiest, ref CNK_Data.WeaponSelection_Difficulty_Easiest);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Difficulty_Hardest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Difficulty_Hardest, ref CNK_Data.WeaponSelection_Difficulty_Hardest);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Buddy_Ahead] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_Ahead, ref CNK_Data.WeaponSelection_Buddy_Ahead);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Buddy_Behind] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_Behind, ref CNK_Data.WeaponSelection_Buddy_Behind);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Buddy_InRange] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_InRange, ref CNK_Data.WeaponSelection_Buddy_InRange);

                File.WriteAllLines(path_gob_extracted + "common/dda/playerweaponselection.csv", csv_PlayerWeaponSel);
            }
            if (Editing_CSV_PlayerWeaponSelection_Boss)
            {
                string[] csv_PlayerWeaponSelBoss = File.ReadAllLines(path_gob_extracted + "common/dda/playerweaponselection_boss.csv");

                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Earth_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_1, ref CNK_Data.WeaponSelection_Track_Earth_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Earth_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_2, ref CNK_Data.WeaponSelection_Track_Earth_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Earth_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_3, ref CNK_Data.WeaponSelection_Track_Earth_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Barin_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_1, ref CNK_Data.WeaponSelection_Track_Barin_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Barin_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_2, ref CNK_Data.WeaponSelection_Track_Barin_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Barin_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_3, ref CNK_Data.WeaponSelection_Track_Barin_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_1, ref CNK_Data.WeaponSelection_Track_Fenom_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_2, ref CNK_Data.WeaponSelection_Track_Fenom_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_3, ref CNK_Data.WeaponSelection_Track_Fenom_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_1, ref CNK_Data.WeaponSelection_Track_Teknee_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_2, ref CNK_Data.WeaponSelection_Track_Teknee_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_3, ref CNK_Data.WeaponSelection_Track_Teknee_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_VeloRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_VeloRace, ref CNK_Data.WeaponSelection_Track_VeloRace);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_1, ref CNK_Data.WeaponSelection_Track_Arena_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_2, ref CNK_Data.WeaponSelection_Track_Arena_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_3, ref CNK_Data.WeaponSelection_Track_Arena_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_4, ref CNK_Data.WeaponSelection_Track_Arena_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_5, ref CNK_Data.WeaponSelection_Track_Arena_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_6, ref CNK_Data.WeaponSelection_Track_Arena_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_7, ref CNK_Data.WeaponSelection_Track_Arena_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Lobby, ref CNK_Data.WeaponSelection_Track_Lobby);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Trophy] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Trophy, ref CNK_Data.WeaponSelection_Mode_Adv_Trophy);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_CNK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_CNK, ref CNK_Data.WeaponSelection_Mode_Adv_CNK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Gem] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Gem, ref CNK_Data.WeaponSelection_Mode_Adv_Gem);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Boss] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Boss, ref CNK_Data.WeaponSelection_Mode_Adv_Boss);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Crystal, ref CNK_Data.WeaponSelection_Mode_Adv_Crystal);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Arcade] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Arcade, ref CNK_Data.WeaponSelection_Mode_Arcade);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Versus] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Versus, ref CNK_Data.WeaponSelection_Mode_Versus);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_CrystalRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_CrystalRace, ref CNK_Data.WeaponSelection_Mode_CrystalRace);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Point] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Point, ref CNK_Data.WeaponSelection_Mode_Battle_Point);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Time] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Time, ref CNK_Data.WeaponSelection_Mode_Battle_Time);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Domination] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Domination, ref CNK_Data.WeaponSelection_Mode_Battle_Domination);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_CTF] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_CTF, ref CNK_Data.WeaponSelection_Mode_Battle_CTF);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_KOTR] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_KOTR, ref CNK_Data.WeaponSelection_Mode_Battle_KOTR);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Crystal, ref CNK_Data.WeaponSelection_Mode_Battle_Crystal);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Lobby, ref CNK_Data.WeaponSelection_Mode_Lobby);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_1st] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_1st, ref CNK_Data.WeaponSelection_Rank_1st);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_2nd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_2nd, ref CNK_Data.WeaponSelection_Rank_2nd);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_3rd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_3rd, ref CNK_Data.WeaponSelection_Rank_3rd);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_4th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_4th, ref CNK_Data.WeaponSelection_Rank_4th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_5th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_5th, ref CNK_Data.WeaponSelection_Rank_5th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_6th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_6th, ref CNK_Data.WeaponSelection_Rank_6th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_7th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_7th, ref CNK_Data.WeaponSelection_Rank_7th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_8th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_8th, ref CNK_Data.WeaponSelection_Rank_8th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_0, ref CNK_Data.WeaponSelection_Progress_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_5, ref CNK_Data.WeaponSelection_Progress_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_10] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_10, ref CNK_Data.WeaponSelection_Progress_10);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_15] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_15, ref CNK_Data.WeaponSelection_Progress_15);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_20] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_20, ref CNK_Data.WeaponSelection_Progress_20);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_25] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_25, ref CNK_Data.WeaponSelection_Progress_25);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_30] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_30, ref CNK_Data.WeaponSelection_Progress_30);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_35] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_35, ref CNK_Data.WeaponSelection_Progress_35);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_40] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_40, ref CNK_Data.WeaponSelection_Progress_40);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_45] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_45, ref CNK_Data.WeaponSelection_Progress_45);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_50] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_50, ref CNK_Data.WeaponSelection_Progress_50);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_55] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_55, ref CNK_Data.WeaponSelection_Progress_55);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_60] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_60, ref CNK_Data.WeaponSelection_Progress_60);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_65] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_65, ref CNK_Data.WeaponSelection_Progress_65);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_70] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_70, ref CNK_Data.WeaponSelection_Progress_70);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_75] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_75, ref CNK_Data.WeaponSelection_Progress_75);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_80] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_80, ref CNK_Data.WeaponSelection_Progress_80);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_85] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_85, ref CNK_Data.WeaponSelection_Progress_85);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_90] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_90, ref CNK_Data.WeaponSelection_Progress_90);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_95] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_95, ref CNK_Data.WeaponSelection_Progress_95);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_CLEANINGFLUID, ref CNK_Data.WeaponSelection_ActivePower_CLEANINGFLUID);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_CURSED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_CURSED, ref CNK_Data.WeaponSelection_ActivePower_CURSED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, ref CNK_Data.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_GRACED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_GRACED, ref CNK_Data.WeaponSelection_ActivePower_GRACED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_ICED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_ICED, ref CNK_Data.WeaponSelection_ActivePower_ICED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, ref CNK_Data.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVISIBILITY, ref CNK_Data.WeaponSelection_ActivePower_INVISIBILITY);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVULNERABLE, ref CNK_Data.WeaponSelection_ActivePower_INVULNERABLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_MIMECUBE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_MIMECUBE, ref CNK_Data.WeaponSelection_ActivePower_MIMECUBE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, ref CNK_Data.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_POWER_SHIELD, ref CNK_Data.WeaponSelection_ActivePower_POWER_SHIELD);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_RESETTING] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_RESETTING, ref CNK_Data.WeaponSelection_ActivePower_RESETTING);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_ROLLINGBRUSH, ref CNK_Data.WeaponSelection_ActivePower_ROLLINGBRUSH);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_SPIKYFRUIT, ref CNK_Data.WeaponSelection_ActivePower_SPIKYFRUIT);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_STATICSHOCKED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_STATICSHOCKED, ref CNK_Data.WeaponSelection_ActivePower_STATICSHOCKED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_SUPER_ENGINE, ref CNK_Data.WeaponSelection_ActivePower_SUPER_ENGINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, ref CNK_Data.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TEETHSTRIP] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TEETHSTRIP, ref CNK_Data.WeaponSelection_ActivePower_TEETHSTRIP);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TIMEBUBBLE, ref CNK_Data.WeaponSelection_ActivePower_TIMEBUBBLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TROPY_CLOCKS, ref CNK_Data.WeaponSelection_ActivePower_TROPY_CLOCKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TURBO_BOOSTS, ref CNK_Data.WeaponSelection_ActivePower_TURBO_BOOSTS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_WINDUPJAW] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_WINDUPJAW, ref CNK_Data.WeaponSelection_ActivePower_WINDUPJAW);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB, ref CNK_Data.WeaponSelection_ActiveWep_BOWLING_BOMB);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, ref CNK_Data.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_EXPCRATE_X3, ref CNK_Data.WeaponSelection_ActiveWep_EXPCRATE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, ref CNK_Data.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, ref CNK_Data.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_FREEZING_MINE, ref CNK_Data.WeaponSelection_ActiveWep_FREEZING_MINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE, ref CNK_Data.WeaponSelection_ActiveWep_HOMING_MISSLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, ref CNK_Data.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, ref CNK_Data.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_INVISIBILITY, ref CNK_Data.WeaponSelection_ActiveWep_INVISIBILITY);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_POWER_SHIELD, ref CNK_Data.WeaponSelection_ActiveWep_POWER_SHIELD);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_REDEYE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_REDEYE, ref CNK_Data.WeaponSelection_ActiveWep_REDEYE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, ref CNK_Data.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_STATIC_SHOCK, ref CNK_Data.WeaponSelection_ActiveWep_STATIC_SHOCK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_SUPER_ENGINE, ref CNK_Data.WeaponSelection_ActiveWep_SUPER_ENGINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TORNADO] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TORNADO, ref CNK_Data.WeaponSelection_ActiveWep_TORNADO);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TROPY_CLOCK, ref CNK_Data.WeaponSelection_ActiveWep_TROPY_CLOCK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, ref CNK_Data.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, ref CNK_Data.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_VOODOO_DOLL, ref CNK_Data.WeaponSelection_ActiveWep_VOODOO_DOLL);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_0, ref CNK_Data.WeaponSelection_KartsInFront_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_1, ref CNK_Data.WeaponSelection_KartsInFront_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_2, ref CNK_Data.WeaponSelection_KartsInFront_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_3, ref CNK_Data.WeaponSelection_KartsInFront_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_4, ref CNK_Data.WeaponSelection_KartsInFront_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_5, ref CNK_Data.WeaponSelection_KartsInFront_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_6, ref CNK_Data.WeaponSelection_KartsInFront_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_7, ref CNK_Data.WeaponSelection_KartsInFront_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_8, ref CNK_Data.WeaponSelection_KartsInFront_8);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_0, ref CNK_Data.WeaponSelection_KartsBehind_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_1, ref CNK_Data.WeaponSelection_KartsBehind_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_2, ref CNK_Data.WeaponSelection_KartsBehind_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_3, ref CNK_Data.WeaponSelection_KartsBehind_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_4, ref CNK_Data.WeaponSelection_KartsBehind_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_5, ref CNK_Data.WeaponSelection_KartsBehind_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_6, ref CNK_Data.WeaponSelection_KartsBehind_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_7, ref CNK_Data.WeaponSelection_KartsBehind_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_8, ref CNK_Data.WeaponSelection_KartsBehind_8);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Difficulty_Easiest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Difficulty_Easiest, ref CNK_Data.WeaponSelection_Difficulty_Easiest);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Difficulty_Hardest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Difficulty_Hardest, ref CNK_Data.WeaponSelection_Difficulty_Hardest);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Buddy_Ahead] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_Ahead, ref CNK_Data.WeaponSelection_Buddy_Ahead);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Buddy_Behind] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_Behind, ref CNK_Data.WeaponSelection_Buddy_Behind);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Buddy_InRange] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_InRange, ref CNK_Data.WeaponSelection_Buddy_InRange);

                File.WriteAllLines(path_gob_extracted + "common/dda/playerweaponselection_boss.csv", csv_PlayerWeaponSelBoss);
            }


            if (Editing_CSV_Unlockables)
            {
                string[] csv_Unlockables = File.ReadAllLines(path_gob_extracted + "common/gameprogression/unlockables.csv");

                string[] cur_line_split;
                for (int i = 0; i < csv_Unlockables.Length; i++)
                {
                    cur_line_split = csv_Unlockables[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (cur_line_split.Length == 3 && cur_line_split[2] == "1")
                    {
                        cur_line_split[2] = "0";
                        csv_Unlockables[i] = cur_line_split[0] + "," + cur_line_split[1] + "," + cur_line_split[2];
                    }
                }

                File.WriteAllLines(path_gob_extracted + "common/gameprogression/unlockables.csv", csv_Unlockables);
            }

            if (Editing_CSV_HintsConfig)
            {
                string[] csv_HintsConfig = File.ReadAllLines(path_gob_extracted + "common/hints/config.csv");

                csv_HintsConfig[5] = "500,# Boom duration";
                csv_HintsConfig[6] = "250,# Rotation duration";
                csv_HintsConfig[7] = "0.12,# Rotation speed";
                csv_HintsConfig[9] = "500,# Wait for end of smoke delay";
                csv_HintsConfig[10] = "250,# Camera move time";

                File.WriteAllLines(path_gob_extracted + "common/hints/config.csv", csv_HintsConfig);
            }

            if (Mod_Disable_Fadeout)
            {
                DirectoryInfo dir_hud = new DirectoryInfo(path_gob_extracted + "common/hud/");
                foreach (FileInfo file in dir_hud.EnumerateFiles())
                {
                    string[] csv_HUD_Config = File.ReadAllLines(file.FullName);

                    if (csv_HUD_Config[0] == "[FaderStuff],Fader")
                    {
                        csv_HUD_Config[0] = "";
                    }
                    if (csv_HUD_Config[1] == "player,0")
                    {
                        csv_HUD_Config[1] = "";
                    }
                    if (csv_HUD_Config[1] == "player,0 0")
                    {
                        csv_HUD_Config[1] = "";
                    }
                    if (csv_HUD_Config[0] == "[FadeStuff],Fader")
                    {
                        csv_HUD_Config[0] = "";
                    }

                    File.WriteAllLines(file.FullName, csv_HUD_Config);
                }

                if (Directory.Exists(path_gob_extracted + "hud/"))
                {
                    dir_hud = new DirectoryInfo(path_gob_extracted + "hud/");
                    foreach (FileInfo file in dir_hud.EnumerateFiles())
                    {
                        string[] csv_HUD_Config = File.ReadAllLines(file.FullName);

                        if (csv_HUD_Config[0] == "[FaderStuff],Fader")
                        {
                            csv_HUD_Config[0] = "";
                        }
                        if (csv_HUD_Config[1] == "player,0")
                        {
                            csv_HUD_Config[1] = "";
                        }
                        if (csv_HUD_Config[1] == "player,0 0")
                        {
                            csv_HUD_Config[1] = "";
                        }
                        if (csv_HUD_Config[0] == "[FadeStuff],Fader")
                        {
                            csv_HUD_Config[0] = "";
                        }

                        File.WriteAllLines(file.FullName, csv_HUD_Config);
                    }
                }
                
            }

            if (Editing_CSV_Credits)
            {
                string[] csv_Credits = File.ReadAllLines(path_gob_extracted + "common/ui/eng/credits.csv");

                List<string> csv_Credits_LineList = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    csv_Credits_LineList.Add(csv_Credits[i]);
                }

                csv_Credits_LineList.Add("Crate Mod Loader " + Program.ModProgram.releaseVersionString + ",AlphaDance,1.25,C,4294950912,0,40");
                csv_Credits_LineList.Add("Seed: " + Program.ModProgram.randoSeed + ",AlphaDance,1.25,C,4294950912,0,40");
                csv_Credits_LineList.Add("Options: " + Program.ModProgram.optionsSelectedString + ",AlphaDance,1.25,C,4294950912,0,40");
                csv_Credits_LineList.Add("Crash Nitro Kart,AlphaDance,1.25,C,4279304191,0,80");

                for (int i = 4; i < csv_Credits.Length; i++)
                {
                    csv_Credits_LineList.Add(csv_Credits[i]);
                }

                csv_Credits = new string[csv_Credits_LineList.Count];
                for (int i = 0; i < csv_Credits_LineList.Count; i++)
                {
                    csv_Credits[i] = csv_Credits_LineList[i];
                }

                File.WriteAllLines(path_gob_extracted + "common/ui/eng/credits.csv", csv_Credits);
            }


            EndModProcess();
        }

        public void EndModProcess()
        {
            // Build GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            GobExtract.StartInfo.Arguments = "Tools/ASSETS.GOB" + " " + "Tools/cml_extr" + " -create 1";
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
        string CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows RowID, ref float[] RowTable)
        {
            string row_text = "";
            row_text += ",,";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.EXPLOSIVE_CRATE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.FREEZING_MINE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.HOMING_MISSLE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.BOWLING_BOMB]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.TORNADO]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.STATIC_SHOCK]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.POWER_SHIELD]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.INVINCIBILITY_MASK]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.INVISIBILITY]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.VOODOO_DOLL]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.TROPY_CLOCKS]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.TURBO_BOOSTS]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.SUPER_ENGINE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.REDEYE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.HOMING_MISSLE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.BOWLING_BOMB_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.TURBO_BOOST_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.EXPCRATE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.FREEZEMINE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)CNK_Data.PowerupTypes.STATICSHOCK_X3]);

            return row_text;
        }
    }
}