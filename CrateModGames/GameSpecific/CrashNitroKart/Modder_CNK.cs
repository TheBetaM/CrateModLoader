using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashNitroKart;
//CNK Tools/API by BetaM, ManDude and eezstreet.
/* 
 * Mod Layers:
 * 1: ASSETS.GOB contents
 */

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public enum ModProps : int
    {
        KartStats = 1,
        DriverStats = 2,
        Surfaces = 3,
        Powerups = 4,
        Adventure = 5,
        Textures = 6,
    }

    public sealed class Modder_CNK : Modder
    {

        public override Game Game => new Game()
        {
            Name = CNK_Text.GameTitle,
            ShortName = "CrashNK",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.XBOX
                },
            API_Credit = CNK_Text.API_Credit,
            API_Link = string.Empty,
            TextClass = typeof(CNK_Text),
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_206.49",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_206.49",
                    CodeName = "SLUS_20649", },
                    new RegionCode() {
                    Name = @"SLES_515.11",
                    Region = RegionType.PAL,
                    ExecName = "SLES_515.11",
                    CodeName = "SLES_51511", },
                    new RegionCode() {
                    Name = @"SLPM_660.67",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_660.67",
                    CodeName = "SLPM_66067", },
                },
                [ConsoleMode.GCN] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "GCNE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GCNP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "GC8JA4",
                    Region = RegionType.NTSC_J },
                },
                [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Crash Nitro Kart",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7, },
                    new RegionCode() {
                    Name = "Crash Nitro Kart",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [0] = "Options",
                [(int)ModProps.KartStats] = CNK_Text.PropCategory_KartStats,
                [(int)ModProps.DriverStats] = CNK_Text.PropCategory_DriverStats,
                [(int)ModProps.Surfaces] = CNK_Text.PropCategory_Surfaces,
                [(int)ModProps.Powerups] = CNK_Text.PropCategory_Powerups,
                [(int)ModProps.Adventure] = CNK_Text.PropCategory_Adventure,
                [(int)ModProps.Textures] = "Textures",
            }
        };
        public override bool CanPreloadGame => true;

        public static ModPropOption Option_RandAdventure = new ModPropOption(CNK_Text.Rand_Adventure, CNK_Text.Rand_AdventureDesc);
        public static ModPropOption Option_RandCharStats = new ModPropOption(CNK_Text.Rand_CharacterStats, CNK_Text.Rand_CharacterStatsDesc);
        public static ModPropOption Option_RandKartStats = new ModPropOption(CNK_Text.Rand_KartStats, CNK_Text.Rand_KartStatsDesc);
        
        public static ModPropOption Option_RandWeaponEffects = new ModPropOption(CNK_Text.Rand_PowerupEffects, CNK_Text.Rand_PowerupEffectsDesc);
        public static ModPropOption Option_RandCharacters = new ModPropOption(CNK_Text.Rand_Drivers, CNK_Text.Rand_DriversDesc); //TODO: later version: icon replacement, name replacement, main menu model replacement, adventure character select model
        public static ModPropOption Option_RandKarts = new ModPropOption(CNK_Text.Rand_Karts, CNK_Text.Rand_KartsDesc);

        public static ModPropOption Option_DisableFadeout = new ModPropOption(CNK_Text.Mod_DisableFadeout, CNK_Text.Mod_DisableFadeoutDesc);
        public static ModPropOption Option_DisablePopups = new ModPropOption(CNK_Text.Mod_DisableUnlockPopups, CNK_Text.Mod_DisableUnlockPopupsDesc);
        public static ModPropOption Option_SpeedUpMaskHints = new ModPropOption(CNK_Text.Mod_SpeedUpMaskHint, CNK_Text.Mod_SpeedUpMaskHintDesc);
        public static ModPropOption Option_NoIntro = new ModPropOption(1, CNK_Text.Mod_RemoveIntroVideos, CNK_Text.Mod_RemoveIntroVideosDesc);

        //unfinished
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "") { Hidden = true }; // audio.csv does NOTHING
        public static ModPropOption Option_RandWumpaCrate = new ModPropOption() { Hidden = true };  //TODO dda
        public static ModPropOption Option_RandObstacles = new ModPropOption() { Hidden = true };  //TODO obstacles
        public static ModPropOption Option_RandCupPoints = new ModPropOption() { Hidden = true };  //Maybe? gameprogression
        public static ModPropOption Option_RandSurfParams = new ModPropOption() { Hidden = true }; // TODO: later version
        public static ModPropOption Option_RandWeaponPools = new ModPropOption() { Hidden = true }; // TODO: later version
        public static ModPropOption Option_NoMaskHints = new ModPropOption() { Hidden = true }; //TODO, hinthistory.csv

        public Modder_CNK()
        {

        }

        internal string path_gob_extracted = "";

        public Random randState = new Random();

        public override void StartModProcess()
        {

            // Extract GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                GobExtract.StartInfo.Arguments = ConsolePipeline.ProcessPath + "assets.gob" + " " + ConsolePipeline.ProcessPath + "cml_extr";
            }
            else
            {
                GobExtract.StartInfo.Arguments = ConsolePipeline.ProcessPath + "ASSETS.GOB" + " " + ConsolePipeline.ProcessPath + "cml_extr";
            }
            GobExtract.Start();
            GobExtract.WaitForExit();

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                File.Delete(ConsolePipeline.ExtractedPath + "ASSETS.GFC");
                File.Delete(ConsolePipeline.ExtractedPath + "ASSETS.GOB");
            }
            else
            {
                File.Delete(ConsolePipeline.ExtractedPath + "assets.gfc");
                File.Delete(ConsolePipeline.ExtractedPath + "assets.gob");
            }
            path_gob_extracted = ConsolePipeline.ExtractedPath + @"\cml_extr\";

            ModProcess();
        }

        void ModProcess()
        {
            randState = new Random(ModLoaderGlobals.RandomizerSeed);

            ModCrates.InstallLayerMods(EnabledModCrates, path_gob_extracted, 1);

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
            bool Editing_CSV_Music = false;
            bool Editing_CSV_Credits = true;

            foreach (ModPropertyBase mod in Props)
            {
                if (mod.HasChanged)
                {
                    if (mod.Category == (int)ModProps.KartStats)
                    {
                        Editing_CSV_KartPhysicsBase = true;
                        Editing_CSV_AI_KartPhysicsBase = true;
                    }
                    else if (mod.Category == (int)ModProps.DriverStats)
                    {
                        Editing_CSV_CharacterPhysics = true;
                    }
                    else if (mod.Category == (int)ModProps.Adventure)
                    {
                        Editing_CSV_WarpPadInfo = true;
                        Editing_CSV_AdventureCup = true;
                        Editing_CSV_AdventureTracksManager = true;
                        Editing_CSV_GoalsToRewardsConverter = true;
                    }
                    else if (mod.Category == (int)ModProps.Powerups)
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
                    }
                    else if (mod.Category == (int)ModProps.Surfaces)
                    {
                        Editing_CSV_SurfaceParams = true;
                    }
                }
            }


            if (Option_NoIntro.Enabled)
            {
                if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
                {
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/ALCHEMY.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/ALCHEMY.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCO.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCO.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCODUT.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCODUT.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOENG.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOENG.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOFRE.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOFRE.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOGER.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOGER.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOITA.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOITA.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOSPA.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/FCOSPA.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCO.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCO.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCODUT.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCODUT.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOENG.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOENG.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOFRE.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOFRE.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOGER.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOGER.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOITA.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOITA.SFD;1");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOSPA.SFD;1"))
                        File.Delete(ConsolePipeline.ExtractedPath + "VIDEO/INTRO/SCOSPA.SFD;1");
                }
                else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
                {
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/alchemy.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/alchemy.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fco.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fco.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcodut.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcodut.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcoeng.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcoeng.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcofre.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcofre.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcoger.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcoger.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcoita.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcoita.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcospa.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcospa.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/sco.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/sco.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scodut.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scodut.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scoeng.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scoeng.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scofre.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scofre.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scoger.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scoger.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scoita.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scoita.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scospa.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scospa.sfd");
                }
                else
                {
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/alchemy.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/alchemy.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fco.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fco.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcodut.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcodut.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcoeng.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcoeng.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcofre.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcofre.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcoger.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcoger.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcoita.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcoita.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/fcospa.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/fcospa.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/sco.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/sco.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scodut.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scodut.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scoeng.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scoeng.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scofre.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scofre.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scoger.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scoger.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scoita.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scoita.sfd");
                    if (File.Exists(ConsolePipeline.ExtractedPath + "video/intro/scospa.sfd"))
                        File.Delete(ConsolePipeline.ExtractedPath + "video/intro/scospa.sfd");
                }
            }
            if (Option_RandCharacters.Enabled)
            {
                Mod_Randomize_Characters(randState);
            }
            if (Option_RandKarts.Enabled)
            {
                Mod_Randomize_Karts(randState);
            }
            if (Option_RandAdventure.Enabled)
            {
                Editing_CSV_WarpPadInfo = true;
                Editing_CSV_AdventureCup = true;
                Editing_CSV_AdventureTracksManager = true;
                Editing_CSV_GoalsToRewardsConverter = true;
                CNK_Data_Adventure.CNK_Randomize_WarpPads(randState);
                CNK_Data_Adventure.CNK_Randomize_ReqsRewards(randState);
            }
            if (Option_RandKartStats.Enabled)
            {
                Editing_CSV_KartPhysicsBase = true;
                CNK_Data_KartStats.CNK_Randomize_KartStats(randState);
            }
            if (Option_RandCharStats.Enabled)
            {
                Editing_CSV_CharacterPhysics = true;
                for (int i = 0; i < 16; i++)
                {
                    CNK_Data_DriverStats.CNK_Randomize_CharacterStats(randState, i);
                }
            }
            /*
            if (GetOption(RandomizeSurfaceParameters))
            {
                Editing_CSV_SurfaceParams = true;
                CNK_Data.CNK_Randomize_SufParams(randState);
            }
            */
            if (Option_RandWeaponEffects.Enabled)
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
                CNK_Data_Powerups.CNK_Randomize_PowerShield(randState);
                CNK_Data_Powerups.CNK_Randomize_BowlingBomb(randState);
                CNK_Data_Powerups.CNK_Randomize_FreezingMine(randState);
                CNK_Data_Powerups.CNK_Randomize_HomingMissle(randState);
                CNK_Data_Powerups.CNK_Randomize_InvincMask(randState);
                CNK_Data_Powerups.CNK_Randomize_RedEye(randState);
                CNK_Data_Powerups.CNK_Randomize_TNTCrate(randState);
                CNK_Data_Powerups.CNK_Randomize_Tornado(randState);
                CNK_Data_Powerups.CNK_Randomize_TurboBoost(randState);
                CNK_Data_Powerups.CNK_Randomize_StaticShock(randState);
            }
            /*
            if (GetOption(RandomizeWeaponPools))
            {
                Editing_CSV_PlayerWeaponSelection = true;
                Editing_CSV_PlayerWeaponSelection_Boss = true;
            }
            */
            if (Option_DisablePopups.Enabled)
            {
                Editing_CSV_Unlockables = true;
            }
            if (Option_SpeedUpMaskHints.Enabled)
            {
                Editing_CSV_HintsConfig = true;
            }
            if (Option_RandMusic.Enabled)
            {
                Editing_CSV_Music = true;
            }

            if (Editing_CSV_AdventureTracksManager)
            {
                string[] csv_advtracksmanager = File.ReadAllLines(path_gob_extracted + "common/gameprogression/adventuretracksmanager.csv");
                List<string> csv_AdvTracksManager_LineList = new List<string>();
                for (int i = 0; i < CNK_Data_Adventure.Adv_TracksManager_GridStartRow - 1; i++)
                {
                    csv_AdvTracksManager_LineList.Add(csv_advtracksmanager[i]);
                }

                string cur_line = "";
                for (int i = 0; i < CNK_Data_Adventure.Adv_TracksManager_EntryList.Count; i++)
                {
                    cur_line = CNK_Data_Adventure.PadInfoName[(int)CNK_Data_Adventure.Adv_TracksManager_EntryList[i].PadName] + ",," + CNK_Data.SubModeName[(int)CNK_Data_Adventure.Adv_TracksManager_EntryList[i].Submode] + ",," + CNK_Data_Adventure.RewardName[(int)CNK_Data_Adventure.Adv_TracksManager_EntryList[i].RewardNeeded] + ",," + CNK_Data_Adventure.Adv_TracksManager_EntryList[i].NumberNeeded.ToString();
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
                for (int i = 0; i < CNK_Data_Adventure.Adv_GoalsToRewards_EntryList.Count; i++)
                {
                    cur_line = CNK_Data.TrackName[(int)CNK_Data_Adventure.Adv_GoalsToRewards_EntryList[i].Track] + "," + CNK_Data.SubModeName[(int)CNK_Data_Adventure.Adv_GoalsToRewards_EntryList[i].Submode] + "," + CNK_Data_Adventure.RewardName[(int)CNK_Data_Adventure.Adv_GoalsToRewards_EntryList[i].Reward];
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
                for (int i = 0; i < CNK_Data_Adventure.Adv_WarpPadInfo_EntryList.Count; i++)
                {
                    cur_line = CNK_Data_Adventure.PadInfoName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].PadName] + ",";
                    cur_line += CNK_Data_Adventure.PadInfoDescTypes[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].PadDesc] + ",";
                    cur_line += CNK_Data.TrackName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].Track] + ",";
                    cur_line += CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].isWarpGate] + ",";
                    cur_line += CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].PrimaryActEvent] + ",";
                    cur_line += CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].SecondaryEvent] + ",";
                    cur_line += CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].LockedEvent] + ",";
                    cur_line += CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].LockedEvent2] + ",";
                    cur_line += CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[0]];
                    if (CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent.Length > 1)
                    {
                        cur_line += ";" + CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[1]];
                    }
                    if (CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent.Length > 2)
                    {
                        cur_line += ";" + CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[2]];
                    }
                    cur_line += ",";
                    cur_line += CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].RelicWonEvent] + ",";
                    cur_line += CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].TokenWonEvent[0]];
                    if (CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].TokenWonEvent.Length > 1)
                    {
                        cur_line += ";" + CNK_Data_Adventure.PadInfoEventName[(int)CNK_Data_Adventure.Adv_WarpPadInfo_EntryList[i].TokenWonEvent[1]];
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

                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AccelerationGainNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_AccelerationGainNormal.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AccelerationGainWumpa] = Float_To_CSV_Line(CNK_Data_KartStats.m_AccelerationGainWumpa.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropHeight] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropHeight.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_CancelMinPercent] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_CancelMinPercent.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_DecHoldTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_DecHoldTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_DecSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_DecSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_IncSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_IncSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_MaxHoldTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_MaxHoldTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_MaxRepressTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_MaxRepressTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_Quadratic] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_Quadratic.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInARowTimeTol] = Float_To_CSV_Line(CNK_Data_KartStats.m_BoostInARowTimeTol.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_AKU_DROP] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_AKU_DROP.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_LARGE] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_LARGE.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_MEDIUM] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_MEDIUM.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_SMALL] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_SMALL.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_PAD] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_PAD.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_1] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_1.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_2] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_2.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_3] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_3.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_START] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_START.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SUPER_ENGINE] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SUPER_ENGINE.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST_JUICED] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostMaxImpulsePerSecond] = Float_To_CSV_Line(CNK_Data_KartStats.m_BoostMaxImpulsePerSecond.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostMaxTimeCap] = Float_To_CSV_Line(CNK_Data_KartStats.m_BoostMaxTimeCap.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostSlidePushAngle] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostSlidePushAngle.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostSlidePushTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_BoostSlidePushTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BrakeForce] = Float_To_CSV_Line(CNK_Data_KartStats.m_BrakeForce.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_CollisionRadius] = Float_To_CSV_Line(CNK_Data_KartStats.m_CollisionRadius.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_CollisionSphereOffset] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CollisionSphereOffset.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_CtfFlagMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CtfFlagMaxForwardSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_CursedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CursedMaxForwardSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_DonutFriction.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutMinMaxSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_DonutMinMaxSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnRateMax] = Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnRateMax.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnRateMin] = Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnRateMin.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnTotal] = Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnTotal.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceGround] = Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceGround.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceInAirMagLev] = Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceInAirMagLev.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceMagLev] = Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceMagLev.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceMagLevAirTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceMagLevAirTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DragMaxStrength] = Float_To_CSV_Line(CNK_Data_KartStats.m_DragMaxStrength.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DragStrength] = Float_To_CSV_Line(CNK_Data_KartStats.m_DragStrength.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_GravityAir] = Float_To_CSV_Line(CNK_Data_KartStats.m_GravityAir.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_GravityGround] = Float_To_CSV_Line(CNK_Data_KartStats.m_GravityGround.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HeightForBigAir] = Float_To_CSV_Line(CNK_Data_KartStats.m_HeightForBigAir.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitByMissileFriction] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitByMissileFriction.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedForce] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedForce.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedForceRev] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedForceRev.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedMin] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedMin.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitStopAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitStopAngle.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitStopSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitStopSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitUpSlideTol] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitUpSlideTol.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HiTurnLatFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_HiTurnLatFriction.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HiTurnStartAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_HiTurnStartAngle.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatFricLoss] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatFricLoss.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatMaxAng] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatMaxAng.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatMinAng] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatMinAng.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_InAirFriction.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirMinSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_InAirMinSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirTurnRateNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_InAirTurnRateNormal.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirTurnRateWumpa] = Float_To_CSV_Line(CNK_Data_KartStats.m_InAirTurnRateWumpa.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InvincibiliyMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_InvincibiliyMaxForwardSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpAirTolerance] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpAirTolerance.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpBeforeAirTimeTol] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpBeforeAirTimeTol.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseBase] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseBase.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseBaseMagLev] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseBaseMagLev.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpMax] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpMax.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpMin] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpMin.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpPercent] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpPercent.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpMaxUpVelocity] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpMaxUpVelocity.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpTimeInAirBoost] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_JumpTimeInAirBoost.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_LowSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_LowSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxForwardSpeedNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxForwardSpeedNormal.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxForwardSpeedWumpa] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxForwardSpeedWumpa.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxLinearVelXY] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxLinearVelXY.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxLinearVelZ] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxLinearVelZ.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxReverseSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxReverseSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MinHeightForAirNoJump] = Float_To_CSV_Line(CNK_Data_KartStats.m_MinHeightForAirNoJump.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_NormalFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_NormalFriction.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetGravStrength] = Float_To_CSV_Line(CNK_Data_KartStats.m_ResetGravStrength.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetMaxTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_ResetMaxTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetWaitBeforeDrop] = Float_To_CSV_Line(CNK_Data_KartStats.m_ResetWaitBeforeDrop.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ReverseGain] = Float_To_CSV_Line(CNK_Data_KartStats.m_ReverseGain.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ShockedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_ShockedMaxForwardSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideBoostQuadratic] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideBoostQuadratic.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideBoostTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideBoostTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseInSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseInSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutPercentBetween] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutPercentBetween.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutRotVelSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutRotVelSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEndMaxTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEndMaxTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEndReduceTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEndReduceTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionHigh] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionHigh.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionLow] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionLow.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionNorm] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionNorm.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMaxAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMaxAngle.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMaxBoostCount] = Int_To_CSV_Line(CNK_Data_KartStats.m_SlideMaxBoostCount.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMinAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMinAngle.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMinimumSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMinimumSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideStartMinSteer] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideStartMinSteer.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideTurnRateAwayFromSlide] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideTurnRateAwayFromSlide.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideTurnRateInToSlide] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideTurnRateInToSlide.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeAccelExtra] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeAccelExtra.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeMaxAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeMaxAngle.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeMinAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeMinAngle.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpikeyFruitMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SpikeyFruitMaxForwardSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SpinOutFriction.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTotalLarge] = Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTotalLarge.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTotalNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTotalNormal.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTurnRateMax] = Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTurnRateMax.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTurnRateMin] = Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTurnRateMin.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SquashedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SquashedMaxForwardSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_CancelMinPercent] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_CancelMinPercent.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_DecHoldTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_DecHoldTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_DecSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_DecSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_IncSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_IncSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_MaxHoldTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_MaxHoldTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_MaxRepressTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_MaxRepressTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_Quadratic] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_Quadratic.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TimeBubbleMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_TimeBubbleMaxForwardSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TropyClocksMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_TropyClocksMaxForwardSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellForce] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellForce.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellForceMax] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellForceMax.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellSpeed.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateAccel] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateAccel.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateBrake] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateBrake.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateNormal.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateWumpa] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateWumpa.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_WaitBeforeBrakeReverses] = Float_To_CSV_Line(CNK_Data_KartStats.m_WaitBeforeBrakeReverses.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_WheelieMinTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_WheelieMinTime.Value);
                csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_WheelieSlideBoostMinPercent] = Float_To_CSV_Line(CNK_Data_KartStats.m_WheelieSlideBoostMinPercent.Value);

                File.WriteAllLines(path_gob_extracted + "common/physics/kpbase.csv", csv_kartphysicsbase);
                
            }

            if (Option_RandKartStats.Enabled)
            {
                Editing_CSV_AI_KartPhysicsBase = true;
                CNK_Data_KartStats.CNK_Randomize_KartStats(randState);
            }
            if (Editing_CSV_AI_KartPhysicsBase)
            {
                string[] csv_ai_kartphysicsbase = File.ReadAllLines(path_gob_extracted + "common/physics/kabase.csv");

                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AccelerationGainNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_AccelerationGainNormal.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AccelerationGainWumpa] = Float_To_CSV_Line(CNK_Data_KartStats.m_AccelerationGainWumpa.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropHeight] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropHeight.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_CancelMinPercent] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_CancelMinPercent.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_DecHoldTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_DecHoldTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_DecSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_DecSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_IncSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_IncSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_MaxHoldTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_MaxHoldTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_MaxRepressTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_MaxRepressTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_Quadratic] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_Quadratic.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInARowTimeTol] = Float_To_CSV_Line(CNK_Data_KartStats.m_BoostInARowTimeTol.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_AKU_DROP] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_AKU_DROP.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_LARGE] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_LARGE.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_MEDIUM] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_MEDIUM.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_SMALL] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_SMALL.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_PAD] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_PAD.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_1] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_1.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_2] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_2.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_3] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_3.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_START] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_START.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SUPER_ENGINE] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SUPER_ENGINE.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST_JUICED] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostMaxImpulsePerSecond] = Float_To_CSV_Line(CNK_Data_KartStats.m_BoostMaxImpulsePerSecond.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostMaxTimeCap] = Float_To_CSV_Line(CNK_Data_KartStats.m_BoostMaxTimeCap.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostSlidePushAngle] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostSlidePushAngle.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostSlidePushTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_BoostSlidePushTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_BrakeForce] = Float_To_CSV_Line(CNK_Data_KartStats.m_BrakeForce.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_CollisionRadius] = Float_To_CSV_Line(CNK_Data_KartStats.m_CollisionRadius.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_CollisionSphereOffset] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CollisionSphereOffset.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_CtfFlagMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CtfFlagMaxForwardSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_CursedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CursedMaxForwardSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_DonutFriction.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutMinMaxSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_DonutMinMaxSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnRateMax] = Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnRateMax.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnRateMin] = Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnRateMin.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnTotal] = Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnTotal.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceGround] = Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceGround.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceInAirMagLev] = Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceInAirMagLev.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceMagLev] = Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceMagLev.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceMagLevAirTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceMagLevAirTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DragMaxStrength] = Float_To_CSV_Line(CNK_Data_KartStats.m_DragMaxStrength.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_DragStrength] = Float_To_CSV_Line(CNK_Data_KartStats.m_DragStrength.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_GravityAir] = Float_To_CSV_Line(CNK_Data_KartStats.m_GravityAir.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_GravityGround] = Float_To_CSV_Line(CNK_Data_KartStats.m_GravityGround.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HeightForBigAir] = Float_To_CSV_Line(CNK_Data_KartStats.m_HeightForBigAir.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitByMissileFriction] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitByMissileFriction.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedForce] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedForce.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedForceRev] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedForceRev.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedMin] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedMin.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitStopAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitStopAngle.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitStopSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitStopSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitUpSlideTol] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitUpSlideTol.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HiTurnLatFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_HiTurnLatFriction.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HiTurnStartAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_HiTurnStartAngle.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatFricLoss] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatFricLoss.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatMaxAng] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatMaxAng.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatMinAng] = Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatMinAng.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_InAirFriction.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirMinSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_InAirMinSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirTurnRateNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_InAirTurnRateNormal.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirTurnRateWumpa] = Float_To_CSV_Line(CNK_Data_KartStats.m_InAirTurnRateWumpa.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_InvincibiliyMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_InvincibiliyMaxForwardSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpAirTolerance] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpAirTolerance.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpBeforeAirTimeTol] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpBeforeAirTimeTol.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseBase] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseBase.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseBaseMagLev] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseBaseMagLev.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpMax] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpMax.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpMin] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpMin.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpPercent] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpPercent.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpMaxUpVelocity] = Float_To_CSV_Line(CNK_Data_KartStats.m_JumpMaxUpVelocity.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpTimeInAirBoost] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_JumpTimeInAirBoost.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_LowSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_LowSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxForwardSpeedNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxForwardSpeedNormal.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxForwardSpeedWumpa] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxForwardSpeedWumpa.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxLinearVelXY] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxLinearVelXY.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxLinearVelZ] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxLinearVelZ.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxReverseSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_MaxReverseSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_MinHeightForAirNoJump] = Float_To_CSV_Line(CNK_Data_KartStats.m_MinHeightForAirNoJump.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_NormalFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_NormalFriction.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetGravStrength] = Float_To_CSV_Line(CNK_Data_KartStats.m_ResetGravStrength.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetMaxTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_ResetMaxTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetWaitBeforeDrop] = Float_To_CSV_Line(CNK_Data_KartStats.m_ResetWaitBeforeDrop.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_ReverseGain] = Float_To_CSV_Line(CNK_Data_KartStats.m_ReverseGain.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_ShockedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_ShockedMaxForwardSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideBoostQuadratic] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideBoostQuadratic.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideBoostTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideBoostTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseInSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseInSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutPercentBetween] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutPercentBetween.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutRotVelSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutRotVelSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEndMaxTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEndMaxTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEndReduceTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEndReduceTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionHigh] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionHigh.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionLow] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionLow.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionNorm] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionNorm.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMaxAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMaxAngle.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMaxBoostCount] = Int_To_CSV_Line(CNK_Data_KartStats.m_SlideMaxBoostCount.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMinAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMinAngle.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMinimumSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMinimumSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideStartMinSteer] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideStartMinSteer.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideTurnRateAwayFromSlide] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideTurnRateAwayFromSlide.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideTurnRateInToSlide] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlideTurnRateInToSlide.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeAccelExtra] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeAccelExtra.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeMaxAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeMaxAngle.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeMinAngle] = Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeMinAngle.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpikeyFruitMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SpikeyFruitMaxForwardSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutFriction] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SpinOutFriction.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTotalLarge] = Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTotalLarge.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTotalNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTotalNormal.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTurnRateMax] = Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTurnRateMax.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTurnRateMin] = Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTurnRateMin.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_SquashedMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SquashedMaxForwardSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_CancelMinPercent] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_CancelMinPercent.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_DecHoldTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_DecHoldTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_DecSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_DecSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_IncSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_IncSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_MaxHoldTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_MaxHoldTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_MaxRepressTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_MaxRepressTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_Quadratic] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_Quadratic.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TimeBubbleMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_TimeBubbleMaxForwardSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TropyClocksMaxForwardSpeed] = FloatArray_To_CSV_Line(CNK_Data_KartStats.m_TropyClocksMaxForwardSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellForce] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellForce.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellForceMax] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellForceMax.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellSpeed] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellSpeed.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateAccel] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateAccel.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateBrake] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateBrake.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateNormal] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateNormal.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateWumpa] = Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateWumpa.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_WaitBeforeBrakeReverses] = Float_To_CSV_Line(CNK_Data_KartStats.m_WaitBeforeBrakeReverses.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_WheelieMinTime] = Float_To_CSV_Line(CNK_Data_KartStats.m_WheelieMinTime.Value);
                csv_ai_kartphysicsbase[(int)KartPhysicsBaseRows.m_WheelieSlideBoostMinPercent] = Float_To_CSV_Line(CNK_Data_KartStats.m_WheelieSlideBoostMinPercent.Value);

                File.WriteAllLines(path_gob_extracted + "common/physics/kabase.csv", csv_ai_kartphysicsbase);
            }

            if (Editing_CSV_CharacterPhysics)
            {
                string[] csv_kartphysicscharacter;

                for (int csv_pos = 0; csv_pos < CNK_Data.DriverTypes.Length; csv_pos++)
                {
                    csv_kartphysicscharacter = File.ReadAllLines(path_gob_extracted + "common/physics/kp" + CNK_Data.DriverTypes[csv_pos] + ".csv");

                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_AccelerationGainNormal] = Float_To_CSV_Line(CNK_Data_DriverStats.c_AccelerationGainNormal.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_AccelerationGainWumpa] = Float_To_CSV_Line(CNK_Data_DriverStats.c_AccelerationGainWumpa.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_AKU_DROP] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_AKU_DROP.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_LARGE] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_LARGE.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_MEDIUM] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_MEDIUM.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_SMALL] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_SMALL.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_PAD] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_PAD.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_1] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_1.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_2] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_2.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_3] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_3.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_START] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_START.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SUPER_ENGINE] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_SUPER_ENGINE.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_TURBOBOOST] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_TURBOBOOST_JUICED] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostMaxImpulsePerSecond] = Float_To_CSV_Line(CNK_Data_DriverStats.c_BoostMaxImpulsePerSecond.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostSlidePushAngle] = Float_To_CSV_Line(CNK_Data_DriverStats.c_BoostSlidePushAngle.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BoostSlidePushTime] = Float_To_CSV_Line(CNK_Data_DriverStats.c_BoostSlidePushTime.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_BrakeForce] = Float_To_CSV_Line(CNK_Data_DriverStats.c_BrakeForce.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_HiTurnFriction] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_HiTurnFriction.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_HiTurnStartAngle] = Float_To_CSV_Line(CNK_Data_DriverStats.c_HiTurnStartAngle.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_InAirTurnRateNormal] = Float_To_CSV_Line(CNK_Data_DriverStats.c_InAirTurnRateNormal.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_InAirTurnRateWumpa] = Float_To_CSV_Line(CNK_Data_DriverStats.c_InAirTurnRateWumpa.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_MaxForwardSpeedNormal] = Float_To_CSV_Line(CNK_Data_DriverStats.c_MaxForwardSpeedNormal.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_MaxForwardSpeedWumpa] = Float_To_CSV_Line(CNK_Data_DriverStats.c_MaxForwardSpeedWumpa.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_NormalFriction] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_NormalFriction.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_SlideFrictionHigh] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_SlideFrictionHigh.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_SlideFrictionLow] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_SlideFrictionLow.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_SlideFrictionNorm] = FloatArray2_To_CSV_Line(CNK_Data_DriverStats.c_SlideFrictionNorm.Value, csv_pos);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_SlideMaxAngle] = Float_To_CSV_Line(CNK_Data_DriverStats.c_SlideMaxAngle.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_SlideMinAngle] = Float_To_CSV_Line(CNK_Data_DriverStats.c_SlideMinAngle.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_SlideTurnRateAwayFromSlide] = Float_To_CSV_Line(CNK_Data_DriverStats.c_SlideTurnRateAwayFromSlide.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_SlideTurnRateInToSlide] = Float_To_CSV_Line(CNK_Data_DriverStats.c_SlideTurnRateInToSlide.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_TurnDecellForce] = Float_To_CSV_Line(CNK_Data_DriverStats.c_TurnDecellForce.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_TurnDecellForceMax] = Float_To_CSV_Line(CNK_Data_DriverStats.c_TurnDecellForceMax.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_TurnDecellSpeed] = Float_To_CSV_Line(CNK_Data_DriverStats.c_TurnDecellSpeed.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_TurnRateAccel] = Float_To_CSV_Line(CNK_Data_DriverStats.c_TurnRateAccel.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_TurnRateBrake] = Float_To_CSV_Line(CNK_Data_DriverStats.c_TurnRateBrake.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_TurnRateNormal] = Float_To_CSV_Line(CNK_Data_DriverStats.c_TurnRateNormal.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_TurnRateWumpa] = Float_To_CSV_Line(CNK_Data_DriverStats.c_TurnRateWumpa.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_UIStats_Acceleration] = Float_To_CSV_Line(CNK_Data_DriverStats.c_UIStats_Acceleration.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_UIStats_Speed] = Float_To_CSV_Line(CNK_Data_DriverStats.c_UIStats_Speed.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_UIStats_Turn] = Float_To_CSV_Line(CNK_Data_DriverStats.c_UIStats_Turn.Value[csv_pos]);
                    csv_kartphysicscharacter[(int)KartPhysicsCharacterRows.c_UIStats_MaxValue] = Float_To_CSV_Line(CNK_Data_DriverStats.c_UIStats_MaxValue.Value[csv_pos]);

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
                for (int i = 0; i < CNK_Data_Surfaces.Surface_m_MinSpeedPercent.Value.Length; i++)
                {
                    line_m_MinSpeedPercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_MinSpeedPercent.Value[i]);
                    line_m_SlowDownLongPercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SlowDownLongPercent.Value[i]);
                    line_m_SlowDownAccelPercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SlowDownAccelPercent.Value[i]);
                    line_m_SlowDownBoostPercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SlowDownBoostPercent.Value[i]);
                    line_m_SpeedBoostIncreasePercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SpeedBoostIncreasePercent.Value[i]);
                    line_m_BrakeLossPercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_BrakeLossPercent.Value[i]);
                    line_m_LatFrictionLossPercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_LatFrictionLossPercent.Value[i]);
                    line_m_LongFrictionLossPercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_LongFrictionLossPercent.Value[i]);
                    line_m_SlideFrictionLossPercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SlideFrictionLossPercent.Value[i]);
                    line_m_SpeedAccelIncreasePercent = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_SpeedAccelIncreasePercent.Value[i]);
                    line_m_KartHeightOffset = Float_To_CSV_Line(CNK_Data_Surfaces.Surface_m_KartHeightOffset.Value[i]);

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

                csv_AdventureCup_LineList.Add(CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Red[0]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Green[0]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Blue[0]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Purple[0]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Yellow[0]] + ",");
                csv_AdventureCup_LineList.Add(CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Red[1]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Green[1]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Blue[1]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Purple[1]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Yellow[1]] + ",");
                csv_AdventureCup_LineList.Add(CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Red[2]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Green[2]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Blue[2]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Purple[2]] + "," + CNK_Data.TrackName[(int)CNK_Data_Adventure.GemCup_Yellow[2]] + ",");
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

                csv_PowerShield[1] = Float_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_Time.Value) + ",,,,# float ,# m_Time,";
                csv_PowerShield[2] = Float_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_RangeForZapping.Value) + ",,,,# float,# m_RangeForZapping,\"#15, 10\"";
                csv_PowerShield[3] = Float_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_ZapSpeed.Value) + ",,,,# float,# m_ZapSpeed,";
                csv_PowerShield[4] = FloatArray_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_ColorNonJuiced.Value) + ",,# vec3f,# m_ColorNonJuiced,";
                csv_PowerShield[5] = FloatArray_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_ColorJuiced.Value) + ",,# vec3f,# m_ColorJuiced,";
                csv_PowerShield[6] = Float_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_ColRadius.Value) + ",,,,# float,# m_ColRadius,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/powershield.csv", csv_PowerShield);
            }
            if (Editing_CSV_BowlingBomb)
            {
                string[] csv_BowlingBomb = File.ReadAllLines(path_gob_extracted + "common/weapons/bowlingbomb.csv");

                csv_BowlingBomb[1] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_Speed.Value) + ",,,,,,,";
                csv_BowlingBomb[2] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_Acceleration.Value) + ",,,,,,,";
                csv_BowlingBomb[3] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_AccelerationJuiced.Value) + ",,,,,,,";
                csv_BowlingBomb[4] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_Mass.Value) + ",,,,,,,";
                csv_BowlingBomb[5] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_Radius.Value) + ",,,,,,,";
                csv_BowlingBomb[6] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_AirGravity.Value) + ",,,,,,,";
                csv_BowlingBomb[7] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_GroundGravity.Value) + ",,,,,,,";
                csv_BowlingBomb[8] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_AirGravityMaglev.Value) + ",,,,,,,";
                csv_BowlingBomb[9] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_GroundGravityMaglev.Value) + ",,,,,,,";
                csv_BowlingBomb[10] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_TurnSpeed.Value) + ",,,,,,,";
                csv_BowlingBomb[11] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_TurnSpeedJuiced.Value) + ",,,,,,,";
                csv_BowlingBomb[12] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ViewRange.Value) + ",,,,,,,";
                csv_BowlingBomb[13] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_RangeInFront.Value) + ",,,,,,,";
                csv_BowlingBomb[14] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_BowlingBomb[15] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_BowlingBomb[16] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadius.Value) + ",,,,,,,";
                csv_BowlingBomb[17] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadiusJuiced.Value) + ",,,,,,,";
                csv_BowlingBomb[18] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_DragCoef.Value) + ",,,,,,,";
                csv_BowlingBomb[19] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_EasyLatFriction.Value) + ",,,,,,,";
                csv_BowlingBomb[20] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_EasyLongFriction.Value) + ",,,,,,,";
                csv_BowlingBomb[21] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_HardLatFriction.Value) + ",,,,,,,";
                csv_BowlingBomb[22] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_HardLongFriction.Value) + ",,,,,,,";
                csv_BowlingBomb[23] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_BackSpeed.Value) + ",,,,,,,";
                csv_BowlingBomb[24] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ExplScale.Value) + ",,,,,,,";
                csv_BowlingBomb[25] = Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ExplScaleJuiced.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/bowlingbomb.csv", csv_BowlingBomb);
            }
            if (Editing_CSV_FreezingMine)
            {
                string[] csv_FreezingMine = File.ReadAllLines(path_gob_extracted + "common/weapons/freezingmine.csv");

                csv_FreezingMine[1] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_NormalFreezeTime.Value) + ",,,,,,,";
                csv_FreezingMine[2] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_JuicedFreezeTime.Value) + ",,,,,,,";
                csv_FreezingMine[3] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_NormalWumpaFruitLost.Value) + ",,,,,,,";
                csv_FreezingMine[4] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_JuicedWumpaFruitLost.Value) + ",,,,,,,";
                csv_FreezingMine[5] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_ThrowDistance.Value) + ",,,,,,,";
                csv_FreezingMine[6] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_ThrowSpeedFactor.Value) + ",,,,,,,";
                csv_FreezingMine[7] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_BlastRadius.Value) + ",,,,,,,";
                csv_FreezingMine[8] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_BlastRadiusJuiced.Value) + ",,,,,,,";
                csv_FreezingMine[9] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_ExplScale.Value) + ",,,,,,,";
                csv_FreezingMine[10] = Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_ExplScaleJuiced.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/freezingmine.csv", csv_FreezingMine);
            }
            if (Editing_CSV_HomingMissle)
            {
                string[] csv_HomingMissle = File.ReadAllLines(path_gob_extracted + "common/weapons/homingmissile.csv");

                csv_HomingMissle[1] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_TrackingFrontDistance.Value) + ",,,,,,,";
                csv_HomingMissle[2] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_MaxSpeed.Value) + ",,,,,,,";
                csv_HomingMissle[3] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_MaxSpeedJuiced.Value) + ",,,,,,,";
                csv_HomingMissle[4] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_TimeLimit.Value) + ",,,,,,,";
                csv_HomingMissle[5] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_AirGravityNormal.Value) + ",,,,,,,";
                csv_HomingMissle[6] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_GroundGravityNormal.Value) + ",,,,,,,";
                csv_HomingMissle[7] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_AirGravityMaglev.Value) + ",,,,,,,";
                csv_HomingMissle[8] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_GroundGravityMaglev.Value) + ",,,,,,,";
                csv_HomingMissle[9] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_Acceleration.Value) + ",,,,,,,";
                csv_HomingMissle[10] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_AccelerationJuiced.Value) + ",,,,,,,";
                csv_HomingMissle[11] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_TurnSpeed.Value) + ",,,,,,,";
                csv_HomingMissle[12] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_TurnSpeedJuiced.Value) + ",,,,,,,";
                csv_HomingMissle[13] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_Mass.Value) + ",,,,,,,";
                csv_HomingMissle[14] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_Radius.Value) + ",,,,,,,";
                csv_HomingMissle[15] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DelayTrackingUpdate.Value) + ",,,,,,,";
                csv_HomingMissle[16] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ViewRange.Value) + ",,,,,,,";
                csv_HomingMissle[17] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_RangeInFront.Value) + ",,,,,,,";
                csv_HomingMissle[18] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_RangeInBack.Value) + ",,,,,,,";
                csv_HomingMissle[19] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_HomingMissle[20] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_HomingMissle[21] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadius.Value) + ",,,,,,,";
                csv_HomingMissle[22] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadiusJuiced.Value) + ",,,,,,,";
                csv_HomingMissle[23] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DragCoef.Value) + ",,,,,,,";
                csv_HomingMissle[24] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_EasyLatFriction.Value) + ",,,,,,,";
                csv_HomingMissle[25] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_EasyLongFriction.Value) + ",,,,,,,";
                csv_HomingMissle[26] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_HardLatFriction.Value) + ",,,,,,,";
                csv_HomingMissle[27] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_HardLongFriction.Value) + ",,,,,,,";
                csv_HomingMissle[28] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DecayTime.Value) + ",,,,,,,";
                csv_HomingMissle[29] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DecaySpeed.Value) + ",,,,,,,";
                csv_HomingMissle[30] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DecayMin.Value) + ",,,,,,,";
                csv_HomingMissle[31] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ExplScale.Value) + ",,,,,,,";
                csv_HomingMissle[32] = Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ExplScaleJuiced.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/homingmissile.csv", csv_HomingMissle);
            }
            if (Editing_CSV_InvincMask)
            {
                string[] csv_InvincMask = File.ReadAllLines(path_gob_extracted + "common/weapons/invincibilitymask.csv");

                csv_InvincMask[1] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_NormalTime.Value) + ",,,,,,,";
                csv_InvincMask[2] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_JuicedTime.Value) + ",,,,,,,";
                csv_InvincMask[3] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_NormalTimeTeamed.Value) + ",,,,,,,";
                csv_InvincMask[4] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_JuicedTimeTeamed.Value) + ",,,,,,,";
                csv_InvincMask[5] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_InvincMask[6] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_InvincMask[7] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_TeamSpeed.Value) + ",,,,,,,";
                csv_InvincMask[8] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_TeamBlastRange.Value) + ",,,,,,,";
                csv_InvincMask[9] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_TeamMeterFull.Value) + ",,,,,,,";
                csv_InvincMask[10] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_ExplScale.Value) + ",,,,,,,";
                csv_InvincMask[11] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_ExplScaleJuiced.Value) + ",,,,,,,";
                csv_InvincMask[12] = Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_ColRadius.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/invincibilitymask.csv", csv_InvincMask);
            }
            if (Editing_CSV_RedEye)
            {
                string[] csv_RedEye = File.ReadAllLines(path_gob_extracted + "common/weapons/redeye.csv");

                csv_RedEye[1] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Acceleration.Value) + ",,,,,,,";
                csv_RedEye[2] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Deceleration.Value) + ",,,,,,,";
                csv_RedEye[3] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_MaxSpeed.Value) + ",,,,,,,";
                csv_RedEye[4] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_MinSpeed.Value) + ",,,,,,,";
                csv_RedEye[5] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_TurnSpeed.Value) + ",,,,,,,";
                csv_RedEye[6] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Explosion_Radius.Value) + ",,,,,,,";
                csv_RedEye[7] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_TurnSpeedJuiced.Value) + ",,,,,,,";
                csv_RedEye[8] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Explosion_Radius_Juiced.Value) + ",,,,,,,";
                csv_RedEye[9] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Expl_Scale.Value) + ",,,,,,,";
                csv_RedEye[10] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Expl_Scale_Juiced.Value) + ",,,,,,,";
                csv_RedEye[11] = Float_To_CSV_Line(CNK_Data_Powerups.RedEye_FullSpeedTurnSlowdown.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/redeye.csv", csv_RedEye);
            }
            if (Editing_CSV_TNT)
            {
                string[] csv_TNT = File.ReadAllLines(path_gob_extracted + "common/weapons/tntcrate.csv");

                csv_TNT[1] = Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_Time.Value) + ",,,,,,,";
                csv_TNT[2] = Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_TimeBeforeHiddenChar.Value) + ",,,,,,,";
                csv_TNT[3] = Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_TimeHiddenChar.Value) + ",,,,,,,";
                csv_TNT[4] = Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_TNT[5] = Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_TNT[6] = Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_ExplosionBlastRadius.Value) + ",,,,,,,";
                csv_TNT[7] = Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_ExplScale.Value) + ",,,,,,,";
                csv_TNT[10] = Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_ExplScaleJuiced.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/tntcrate.csv", csv_TNT);
            }
            if (Editing_CSV_Tornado)
            {
                string[] csv_Tornado = File.ReadAllLines(path_gob_extracted + "common/weapons/tornado.csv");

                csv_Tornado[1] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TrackingFrontDistance.Value) + ",,,,,,,";
                csv_Tornado[2] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_MaxSpeed.Value) + ",,,,,,,";
                csv_Tornado[3] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_MaxSpeedJuiced.Value) + ",,,,,,,";
                csv_Tornado[4] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_MaxSpeedWithKart.Value) + ",,,,,,,";
                csv_Tornado[5] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TimeLimit.Value) + ",,,,,,,";
                csv_Tornado[6] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_AirGravity.Value) + ",,,,,,,";
                csv_Tornado[7] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_GroundGravity.Value) + ",,,,,,,";
                csv_Tornado[8] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_AirGravityMaglev.Value) + ",,,,,,,";
                csv_Tornado[9] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_GroundGravityMaglev.Value) + ",,,,,,,";
                csv_Tornado[10] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_Acceleration.Value) + ",,,,,,,";
                csv_Tornado[11] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_AccelerationJuiced.Value) + ",,,,,,,";
                csv_Tornado[12] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TurnSpeed.Value) + ",,,,,,,";
                csv_Tornado[13] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TurnSpeedJuiced.Value) + ",,,,,,,";
                csv_Tornado[14] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_Mass.Value) + ",,,,,,,";
                csv_Tornado[15] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_Radius.Value) + ",,,,,,,";
                csv_Tornado[16] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_DelayTrackingUpdate.Value) + ",,,,,,,";
                csv_Tornado[17] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_ViewRange.Value) + ",,,,,,,";
                csv_Tornado[18] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_RangeInFront.Value) + ",,,,,,,";
                csv_Tornado[19] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_RangeInBack.Value) + ",,,,,,,";
                csv_Tornado[20] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_LiftTime.Value) + ",,,,,,,";
                csv_Tornado[21] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_LiftForce.Value) + ",,,,,,,";
                csv_Tornado[22] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_FizzleTime.Value) + ",,,,,,,";
                csv_Tornado[23] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_Tornado[24] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_Tornado[25] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_DragCoef.Value) + ",,,,,,,";
                csv_Tornado[26] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_EasyLatFriction.Value) + ",,,,,,,";
                csv_Tornado[27] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_EasyLongFriction.Value) + ",,,,,,,";
                csv_Tornado[28] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_HardLatFriction.Value) + ",,,,,,,";
                csv_Tornado[29] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_HardLongFriction.Value) + ",,,,,,,";
                csv_Tornado[30] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TargetAllDistance.Value) + ",,,,,,,";
                csv_Tornado[31] = Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_ViewRangleOfTarget.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/tornado.csv", csv_Tornado);
            }
            if (Editing_CSV_TurboBoost)
            {
                string[] csv_TurboBoost = File.ReadAllLines(path_gob_extracted + "common/weapons/turboboost.csv");

                csv_TurboBoost[1] = Float_To_CSV_Line(CNK_Data_Powerups.TurboBoost_m_NormalTime.Value) + ",,,,,,,";
                csv_TurboBoost[2] = Float_To_CSV_Line(CNK_Data_Powerups.TurboBoost_m_JuicedTime.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/turboboost.csv", csv_TurboBoost);
            }
            if (Editing_CSV_StaticShock)
            {
                string[] csv_StaticShock = File.ReadAllLines(path_gob_extracted + "common/weapons/staticshock.csv");

                csv_StaticShock[1] = Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_NormalTime.Value) + ",,,,,,,";
                csv_StaticShock[2] = Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_JuicedTime.Value) + ",,,,,,,";
                csv_StaticShock[3] = Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_StaticShock[4] = Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_StaticShock[5] = Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_HomingSpeed.Value) + ",,,,,,,";
                csv_StaticShock[6] = Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_DistanceForHome.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/staticshock.csv", csv_StaticShock);
            }

            if (Editing_CSV_PlayerWeaponSelection)
            {
                string[] csv_PlayerWeaponSel = File.ReadAllLines(path_gob_extracted + "common/dda/playerweaponselection.csv");

                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_1, CNK_Data_Powerups.WeaponSelection_Track_Earth_1);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_2, CNK_Data_Powerups.WeaponSelection_Track_Earth_2);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_3, CNK_Data_Powerups.WeaponSelection_Track_Earth_3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_1, CNK_Data_Powerups.WeaponSelection_Track_Barin_1);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_2, CNK_Data_Powerups.WeaponSelection_Track_Barin_2);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_3, CNK_Data_Powerups.WeaponSelection_Track_Barin_3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_1, CNK_Data_Powerups.WeaponSelection_Track_Fenom_1);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_2, CNK_Data_Powerups.WeaponSelection_Track_Fenom_2);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_3, CNK_Data_Powerups.WeaponSelection_Track_Fenom_3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_1, CNK_Data_Powerups.WeaponSelection_Track_Teknee_1);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_2, CNK_Data_Powerups.WeaponSelection_Track_Teknee_2);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_3, CNK_Data_Powerups.WeaponSelection_Track_Teknee_3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_VeloRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_VeloRace, CNK_Data_Powerups.WeaponSelection_Track_VeloRace);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_1, CNK_Data_Powerups.WeaponSelection_Track_Arena_1);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_2, CNK_Data_Powerups.WeaponSelection_Track_Arena_2);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_3, CNK_Data_Powerups.WeaponSelection_Track_Arena_3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_4, CNK_Data_Powerups.WeaponSelection_Track_Arena_4);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_5, CNK_Data_Powerups.WeaponSelection_Track_Arena_5);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_6, CNK_Data_Powerups.WeaponSelection_Track_Arena_6);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_7, CNK_Data_Powerups.WeaponSelection_Track_Arena_7);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Lobby, CNK_Data_Powerups.WeaponSelection_Track_Lobby);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Trophy] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Trophy, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Trophy);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_CNK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_CNK, CNK_Data_Powerups.WeaponSelection_Mode_Adv_CNK);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Gem] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Gem, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Gem);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Boss] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Boss, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Boss);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Crystal);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Arcade] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Arcade, CNK_Data_Powerups.WeaponSelection_Mode_Arcade);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Versus] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Versus, CNK_Data_Powerups.WeaponSelection_Mode_Versus);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_CrystalRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_CrystalRace, CNK_Data_Powerups.WeaponSelection_Mode_CrystalRace);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Point] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Point, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Point);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Time] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Time, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Time);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Domination] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Domination, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Domination);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_CTF] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_CTF, CNK_Data_Powerups.WeaponSelection_Mode_Battle_CTF);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_KOTR] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_KOTR, CNK_Data_Powerups.WeaponSelection_Mode_Battle_KOTR);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Crystal);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Lobby, CNK_Data_Powerups.WeaponSelection_Mode_Lobby);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_1st] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_1st, CNK_Data_Powerups.WeaponSelection_Rank_1st);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_2nd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_2nd, CNK_Data_Powerups.WeaponSelection_Rank_2nd);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_3rd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_3rd, CNK_Data_Powerups.WeaponSelection_Rank_3rd);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_4th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_4th, CNK_Data_Powerups.WeaponSelection_Rank_4th);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_5th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_5th, CNK_Data_Powerups.WeaponSelection_Rank_5th);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_6th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_6th, CNK_Data_Powerups.WeaponSelection_Rank_6th);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_7th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_7th, CNK_Data_Powerups.WeaponSelection_Rank_7th);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_8th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_8th, CNK_Data_Powerups.WeaponSelection_Rank_8th);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_0, CNK_Data_Powerups.WeaponSelection_Progress_0);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_5, CNK_Data_Powerups.WeaponSelection_Progress_5);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_10] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_10, CNK_Data_Powerups.WeaponSelection_Progress_10);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_15] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_15, CNK_Data_Powerups.WeaponSelection_Progress_15);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_20] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_20, CNK_Data_Powerups.WeaponSelection_Progress_20);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_25] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_25, CNK_Data_Powerups.WeaponSelection_Progress_25);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_30] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_30, CNK_Data_Powerups.WeaponSelection_Progress_30);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_35] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_35, CNK_Data_Powerups.WeaponSelection_Progress_35);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_40] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_40, CNK_Data_Powerups.WeaponSelection_Progress_40);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_45] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_45, CNK_Data_Powerups.WeaponSelection_Progress_45);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_50] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_50, CNK_Data_Powerups.WeaponSelection_Progress_50);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_55] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_55, CNK_Data_Powerups.WeaponSelection_Progress_55);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_60] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_60, CNK_Data_Powerups.WeaponSelection_Progress_60);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_65] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_65, CNK_Data_Powerups.WeaponSelection_Progress_65);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_70] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_70, CNK_Data_Powerups.WeaponSelection_Progress_70);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_75] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_75, CNK_Data_Powerups.WeaponSelection_Progress_75);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_80] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_80, CNK_Data_Powerups.WeaponSelection_Progress_80);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_85] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_85, CNK_Data_Powerups.WeaponSelection_Progress_85);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_90] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_90, CNK_Data_Powerups.WeaponSelection_Progress_90);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_95] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_95, CNK_Data_Powerups.WeaponSelection_Progress_95);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_CLEANINGFLUID, CNK_Data_Powerups.WeaponSelection_ActivePower_CLEANINGFLUID);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_CURSED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_CURSED, CNK_Data_Powerups.WeaponSelection_ActivePower_CURSED);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_GRACED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_GRACED, CNK_Data_Powerups.WeaponSelection_ActivePower_GRACED);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_ICED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_ICED, CNK_Data_Powerups.WeaponSelection_ActivePower_ICED);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActivePower_INVISIBILITY);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_INVULNERABLE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_MIMECUBE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_MIMECUBE, CNK_Data_Powerups.WeaponSelection_ActivePower_MIMECUBE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, CNK_Data_Powerups.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActivePower_POWER_SHIELD);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_RESETTING] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_RESETTING, CNK_Data_Powerups.WeaponSelection_ActivePower_RESETTING);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_ROLLINGBRUSH, CNK_Data_Powerups.WeaponSelection_ActivePower_ROLLINGBRUSH);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_SPIKYFRUIT, CNK_Data_Powerups.WeaponSelection_ActivePower_SPIKYFRUIT);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_STATICSHOCKED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_STATICSHOCKED, CNK_Data_Powerups.WeaponSelection_ActivePower_STATICSHOCKED);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActivePower_SUPER_ENGINE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TEETHSTRIP] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TEETHSTRIP, CNK_Data_Powerups.WeaponSelection_ActivePower_TEETHSTRIP);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TIMEBUBBLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TIMEBUBBLE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TROPY_CLOCKS, CNK_Data_Powerups.WeaponSelection_ActivePower_TROPY_CLOCKS);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActivePower_TURBO_BOOSTS);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_WINDUPJAW] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_WINDUPJAW, CNK_Data_Powerups.WeaponSelection_ActivePower_WINDUPJAW);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_BOWLING_BOMB, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_EXPCRATE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPCRATE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_FREEZING_MINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZING_MINE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_HOMING_MISSLE, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVISIBILITY);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActiveWep_POWER_SHIELD);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_REDEYE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_REDEYE, CNK_Data_Powerups.WeaponSelection_ActiveWep_REDEYE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_STATIC_SHOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATIC_SHOCK);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_SUPER_ENGINE);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TORNADO] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TORNADO, CNK_Data_Powerups.WeaponSelection_ActiveWep_TORNADO);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TROPY_CLOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_TROPY_CLOCK);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_VOODOO_DOLL, CNK_Data_Powerups.WeaponSelection_ActiveWep_VOODOO_DOLL);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_0, CNK_Data_Powerups.WeaponSelection_KartsInFront_0);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_1, CNK_Data_Powerups.WeaponSelection_KartsInFront_1);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_2, CNK_Data_Powerups.WeaponSelection_KartsInFront_2);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_3, CNK_Data_Powerups.WeaponSelection_KartsInFront_3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_4, CNK_Data_Powerups.WeaponSelection_KartsInFront_4);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_5, CNK_Data_Powerups.WeaponSelection_KartsInFront_5);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_6, CNK_Data_Powerups.WeaponSelection_KartsInFront_6);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_7, CNK_Data_Powerups.WeaponSelection_KartsInFront_7);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_8, CNK_Data_Powerups.WeaponSelection_KartsInFront_8);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_0, CNK_Data_Powerups.WeaponSelection_KartsBehind_0);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_1, CNK_Data_Powerups.WeaponSelection_KartsBehind_1);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_2, CNK_Data_Powerups.WeaponSelection_KartsBehind_2);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_3, CNK_Data_Powerups.WeaponSelection_KartsBehind_3);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_4, CNK_Data_Powerups.WeaponSelection_KartsBehind_4);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_5, CNK_Data_Powerups.WeaponSelection_KartsBehind_5);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_6, CNK_Data_Powerups.WeaponSelection_KartsBehind_6);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_7, CNK_Data_Powerups.WeaponSelection_KartsBehind_7);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_8, CNK_Data_Powerups.WeaponSelection_KartsBehind_8);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Difficulty_Easiest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Difficulty_Easiest, CNK_Data_Powerups.WeaponSelection_Difficulty_Easiest);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Difficulty_Hardest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Difficulty_Hardest, CNK_Data_Powerups.WeaponSelection_Difficulty_Hardest);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Buddy_Ahead] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Buddy_Ahead, CNK_Data_Powerups.WeaponSelection_Buddy_Ahead);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Buddy_Behind] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Buddy_Behind, CNK_Data_Powerups.WeaponSelection_Buddy_Behind);
                csv_PlayerWeaponSel[(int)CNK_Data_Powerups.WeaponSelectionRows.Buddy_InRange] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Buddy_InRange, CNK_Data_Powerups.WeaponSelection_Buddy_InRange);

                File.WriteAllLines(path_gob_extracted + "common/dda/playerweaponselection.csv", csv_PlayerWeaponSel);
            }
            if (Editing_CSV_PlayerWeaponSelection_Boss)
            {
                string[] csv_PlayerWeaponSelBoss = File.ReadAllLines(path_gob_extracted + "common/dda/playerweaponselection_boss.csv");

                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_1, CNK_Data_Powerups.WeaponSelection_Track_Earth_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_2, CNK_Data_Powerups.WeaponSelection_Track_Earth_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Earth_3, CNK_Data_Powerups.WeaponSelection_Track_Earth_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_1, CNK_Data_Powerups.WeaponSelection_Track_Barin_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_2, CNK_Data_Powerups.WeaponSelection_Track_Barin_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Barin_3, CNK_Data_Powerups.WeaponSelection_Track_Barin_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_1, CNK_Data_Powerups.WeaponSelection_Track_Fenom_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_2, CNK_Data_Powerups.WeaponSelection_Track_Fenom_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Fenom_3, CNK_Data_Powerups.WeaponSelection_Track_Fenom_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_1, CNK_Data_Powerups.WeaponSelection_Track_Teknee_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_2, CNK_Data_Powerups.WeaponSelection_Track_Teknee_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Teknee_3, CNK_Data_Powerups.WeaponSelection_Track_Teknee_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_VeloRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_VeloRace, CNK_Data_Powerups.WeaponSelection_Track_VeloRace);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_1, CNK_Data_Powerups.WeaponSelection_Track_Arena_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_2, CNK_Data_Powerups.WeaponSelection_Track_Arena_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_3, CNK_Data_Powerups.WeaponSelection_Track_Arena_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_4, CNK_Data_Powerups.WeaponSelection_Track_Arena_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_5, CNK_Data_Powerups.WeaponSelection_Track_Arena_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_6, CNK_Data_Powerups.WeaponSelection_Track_Arena_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Arena_7, CNK_Data_Powerups.WeaponSelection_Track_Arena_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Track_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Track_Lobby, CNK_Data_Powerups.WeaponSelection_Track_Lobby);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Trophy] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Trophy, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Trophy);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_CNK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_CNK, CNK_Data_Powerups.WeaponSelection_Mode_Adv_CNK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Gem] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Gem, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Gem);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Boss] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Boss, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Boss);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Adv_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Crystal);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Arcade] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Arcade, CNK_Data_Powerups.WeaponSelection_Mode_Arcade);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Versus] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Versus, CNK_Data_Powerups.WeaponSelection_Mode_Versus);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_CrystalRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_CrystalRace, CNK_Data_Powerups.WeaponSelection_Mode_CrystalRace);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Point] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Point, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Point);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Time] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Time, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Time);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Domination] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Domination, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Domination);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_CTF] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_CTF, CNK_Data_Powerups.WeaponSelection_Mode_Battle_CTF);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_KOTR] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_KOTR, CNK_Data_Powerups.WeaponSelection_Mode_Battle_KOTR);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Battle_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Crystal);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Mode_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Mode_Lobby, CNK_Data_Powerups.WeaponSelection_Mode_Lobby);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_1st] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_1st, CNK_Data_Powerups.WeaponSelection_Rank_1st);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_2nd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_2nd, CNK_Data_Powerups.WeaponSelection_Rank_2nd);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_3rd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_3rd, CNK_Data_Powerups.WeaponSelection_Rank_3rd);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_4th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_4th, CNK_Data_Powerups.WeaponSelection_Rank_4th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_5th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_5th, CNK_Data_Powerups.WeaponSelection_Rank_5th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_6th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_6th, CNK_Data_Powerups.WeaponSelection_Rank_6th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_7th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_7th, CNK_Data_Powerups.WeaponSelection_Rank_7th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Rank_8th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Rank_8th, CNK_Data_Powerups.WeaponSelection_Rank_8th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_0, CNK_Data_Powerups.WeaponSelection_Progress_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_5, CNK_Data_Powerups.WeaponSelection_Progress_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_10] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_10, CNK_Data_Powerups.WeaponSelection_Progress_10);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_15] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_15, CNK_Data_Powerups.WeaponSelection_Progress_15);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_20] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_20, CNK_Data_Powerups.WeaponSelection_Progress_20);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_25] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_25, CNK_Data_Powerups.WeaponSelection_Progress_25);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_30] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_30, CNK_Data_Powerups.WeaponSelection_Progress_30);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_35] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_35, CNK_Data_Powerups.WeaponSelection_Progress_35);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_40] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_40, CNK_Data_Powerups.WeaponSelection_Progress_40);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_45] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_45, CNK_Data_Powerups.WeaponSelection_Progress_45);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_50] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_50, CNK_Data_Powerups.WeaponSelection_Progress_50);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_55] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_55, CNK_Data_Powerups.WeaponSelection_Progress_55);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_60] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_60, CNK_Data_Powerups.WeaponSelection_Progress_60);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_65] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_65, CNK_Data_Powerups.WeaponSelection_Progress_65);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_70] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_70, CNK_Data_Powerups.WeaponSelection_Progress_70);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_75] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_75, CNK_Data_Powerups.WeaponSelection_Progress_75);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_80] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_80, CNK_Data_Powerups.WeaponSelection_Progress_80);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_85] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_85, CNK_Data_Powerups.WeaponSelection_Progress_85);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_90] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_90, CNK_Data_Powerups.WeaponSelection_Progress_90);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Progress_95] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Progress_95, CNK_Data_Powerups.WeaponSelection_Progress_95);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_CLEANINGFLUID, CNK_Data_Powerups.WeaponSelection_ActivePower_CLEANINGFLUID);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_CURSED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_CURSED, CNK_Data_Powerups.WeaponSelection_ActivePower_CURSED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_GRACED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_GRACED, CNK_Data_Powerups.WeaponSelection_ActivePower_GRACED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_ICED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_ICED, CNK_Data_Powerups.WeaponSelection_ActivePower_ICED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActivePower_INVISIBILITY);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_INVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_INVULNERABLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_MIMECUBE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_MIMECUBE, CNK_Data_Powerups.WeaponSelection_ActivePower_MIMECUBE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, CNK_Data_Powerups.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActivePower_POWER_SHIELD);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_RESETTING] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_RESETTING, CNK_Data_Powerups.WeaponSelection_ActivePower_RESETTING);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_ROLLINGBRUSH, CNK_Data_Powerups.WeaponSelection_ActivePower_ROLLINGBRUSH);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_SPIKYFRUIT, CNK_Data_Powerups.WeaponSelection_ActivePower_SPIKYFRUIT);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_STATICSHOCKED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_STATICSHOCKED, CNK_Data_Powerups.WeaponSelection_ActivePower_STATICSHOCKED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActivePower_SUPER_ENGINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TEETHSTRIP] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TEETHSTRIP, CNK_Data_Powerups.WeaponSelection_ActivePower_TEETHSTRIP);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TIMEBUBBLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TIMEBUBBLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TROPY_CLOCKS, CNK_Data_Powerups.WeaponSelection_ActivePower_TROPY_CLOCKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActivePower_TURBO_BOOSTS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActivePower_WINDUPJAW] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActivePower_WINDUPJAW, CNK_Data_Powerups.WeaponSelection_ActivePower_WINDUPJAW);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_BOWLING_BOMB, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_EXPCRATE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPCRATE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_FREEZING_MINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZING_MINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_HOMING_MISSLE, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVISIBILITY);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActiveWep_POWER_SHIELD);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_REDEYE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_REDEYE, CNK_Data_Powerups.WeaponSelection_ActiveWep_REDEYE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_STATIC_SHOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATIC_SHOCK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_SUPER_ENGINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TORNADO] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TORNADO, CNK_Data_Powerups.WeaponSelection_ActiveWep_TORNADO);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TROPY_CLOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_TROPY_CLOCK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.ActiveWep_VOODOO_DOLL, CNK_Data_Powerups.WeaponSelection_ActiveWep_VOODOO_DOLL);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_0, CNK_Data_Powerups.WeaponSelection_KartsInFront_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_1, CNK_Data_Powerups.WeaponSelection_KartsInFront_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_2, CNK_Data_Powerups.WeaponSelection_KartsInFront_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_3, CNK_Data_Powerups.WeaponSelection_KartsInFront_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_4, CNK_Data_Powerups.WeaponSelection_KartsInFront_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_5, CNK_Data_Powerups.WeaponSelection_KartsInFront_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_6, CNK_Data_Powerups.WeaponSelection_KartsInFront_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_7, CNK_Data_Powerups.WeaponSelection_KartsInFront_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsInFront_8, CNK_Data_Powerups.WeaponSelection_KartsInFront_8);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_0, CNK_Data_Powerups.WeaponSelection_KartsBehind_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_1, CNK_Data_Powerups.WeaponSelection_KartsBehind_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_2, CNK_Data_Powerups.WeaponSelection_KartsBehind_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_3, CNK_Data_Powerups.WeaponSelection_KartsBehind_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_4, CNK_Data_Powerups.WeaponSelection_KartsBehind_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_5, CNK_Data_Powerups.WeaponSelection_KartsBehind_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_6, CNK_Data_Powerups.WeaponSelection_KartsBehind_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_7, CNK_Data_Powerups.WeaponSelection_KartsBehind_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.KartsBehind_8, CNK_Data_Powerups.WeaponSelection_KartsBehind_8);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Difficulty_Easiest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Difficulty_Easiest, CNK_Data_Powerups.WeaponSelection_Difficulty_Easiest);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Difficulty_Hardest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Difficulty_Hardest, CNK_Data_Powerups.WeaponSelection_Difficulty_Hardest);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Buddy_Ahead] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Buddy_Ahead, CNK_Data_Powerups.WeaponSelection_Buddy_Ahead);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Buddy_Behind] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Buddy_Behind, CNK_Data_Powerups.WeaponSelection_Buddy_Behind);
                csv_PlayerWeaponSelBoss[(int)CNK_Data_Powerups.WeaponSelectionRows.Buddy_InRange] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows.Buddy_InRange, CNK_Data_Powerups.WeaponSelection_Buddy_InRange);

                File.WriteAllLines(path_gob_extracted + "common/dda/playerweaponselection_boss.csv", csv_PlayerWeaponSelBoss);
            }

            if (Editing_CSV_Music)
            {
                string[] csv_music = File.ReadAllLines(path_gob_extracted + "common/audio/music.csv");

                List<string> csv_Music_LineList = new List<string>();
                for (int i = 0; i < 12; i++)
                {
                    csv_Music_LineList.Add(csv_music[i]);
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

                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[0]] + "," + CNK_Data.TrackName[(int)RandTracks[0]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[0]] + "f," + CNK_Data.TrackName[(int)RandTracks[0]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[1]] + "," + CNK_Data.TrackName[(int)RandTracks[1]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[1]] + "f," + CNK_Data.TrackName[(int)RandTracks[1]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[2]] + "," + CNK_Data.TrackName[(int)RandTracks[2]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[2]] + "f," + CNK_Data.TrackName[(int)RandTracks[2]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)HubList[0]] + "," + CNK_Data.TrackName[(int)RandHubs[0]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[3]] + "," + CNK_Data.TrackName[(int)RandTracks[3]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[3]] + "f," + CNK_Data.TrackName[(int)RandTracks[3]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[4]] + "," + CNK_Data.TrackName[(int)RandTracks[4]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[4]] + "f," + CNK_Data.TrackName[(int)RandTracks[4]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[5]] + "," + CNK_Data.TrackName[(int)RandTracks[5]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[5]] + "f," + CNK_Data.TrackName[(int)RandTracks[5]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[6]] + "," + CNK_Data.TrackName[(int)RandTracks[6]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[6]] + "f," + CNK_Data.TrackName[(int)RandTracks[6]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[7]] + "," + CNK_Data.TrackName[(int)RandTracks[7]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[7]] + "f," + CNK_Data.TrackName[(int)RandTracks[7]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[8]] + "," + CNK_Data.TrackName[(int)RandTracks[8]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[8]] + "f," + CNK_Data.TrackName[(int)RandTracks[8]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)HubList[1]] + "," + CNK_Data.TrackName[(int)RandHubs[1]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)HubList[2]] + "," + CNK_Data.TrackName[(int)RandHubs[2]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)HubList[3]] + "," + CNK_Data.TrackName[(int)RandHubs[3]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)HubList[4]] + "," + CNK_Data.TrackName[(int)RandHubs[4]] + extra);

                for (int i = 35; i < 37; i++)
                {
                    csv_Music_LineList.Add(csv_music[i]);
                }

                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[9]] + "," + CNK_Data.TrackName[(int)RandTracks[9]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[9]] + "f," + CNK_Data.TrackName[(int)RandTracks[9]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[10]] + "," + CNK_Data.TrackName[(int)RandTracks[10]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[10]] + "f," + CNK_Data.TrackName[(int)RandTracks[10]] + extraf);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[11]] + "," + CNK_Data.TrackName[(int)RandTracks[11]] + extra);
                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[11]] + "f," + CNK_Data.TrackName[(int)RandTracks[11]] + extraf);

                for (int i = 43; i < 45; i++)
                {
                    csv_Music_LineList.Add(csv_music[i]);
                }

                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[12]] + "," + CNK_Data.TrackName[(int)RandTracks[12]] + extra);

                for (int i = 46; i < 48; i++)
                {
                    csv_Music_LineList.Add(csv_music[i]);
                }

                csv_Music_LineList.Add(CNK_Data.TrackName[(int)TrackList[12]] + "f," + CNK_Data.TrackName[(int)RandTracks[12]] + extraf);

                csv_Music_LineList.Add("");

                csv_music = new string[csv_Music_LineList.Count];
                for (int i = 0; i < csv_Music_LineList.Count; i++)
                {
                    csv_music[i] = csv_Music_LineList[i];
                }

                File.WriteAllLines(path_gob_extracted + "common/audio/music.csv", csv_music);
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

            if (Option_DisableFadeout.Enabled)
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

                if (!csv_Credits[3].Contains(",0x") && !csv_Credits[3].Contains(", 0x") && !csv_Credits[3].Contains(",0x"))
                {
                    csv_Credits_LineList.Add("Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + ",AlphaDance,1.25,C,4294950912,0,40");
                    csv_Credits_LineList.Add("Seed: " + ModLoaderGlobals.RandomizerSeed + ",AlphaDance,1.25,C,4294950912,0,40");
                    csv_Credits_LineList.Add("Crash Nitro Kart,AlphaDance,1.25,C,4279304191,0,80");
                }
                else
                {
                    csv_Credits_LineList.Add("Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + ",AlphaDance,1.25,C,0xFF10FFFF,0,40");
                    csv_Credits_LineList.Add("Seed: " + ModLoaderGlobals.RandomizerSeed + ",AlphaDance,1.25,C,0xFF10FFFF,0,40");
                    csv_Credits_LineList.Add("Crash Nitro Kart,AlphaDance,1.25,C,0xFF10FFFF,0,80");
                }

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

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "ps2/gfx/hud/8-bit_hud.png");
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "gcn/gfx/hud/8-bit_hud.png");
            }
            else
            {
                CNK_Data_Textures.Texture_HudIcons.ResourceToFile(path_gob_extracted + "xbox/gfx/hud/8-bit_hud.png");
            }

            CNK_Data_Textures.Texture_Font.ResourceToFile(path_gob_extracted + "common/fonts/all_fonts.png");

            CNK_Data_Textures.Texture_Load_Arena1.ResourceToFile(path_gob_extracted + "common/load/arena1.png");
            CNK_Data_Textures.Texture_Load_Arena2.ResourceToFile(path_gob_extracted + "common/load/arena2.png");
            CNK_Data_Textures.Texture_Load_Arena3.ResourceToFile(path_gob_extracted + "common/load/arena3.png");
            CNK_Data_Textures.Texture_Load_Arena4.ResourceToFile(path_gob_extracted + "common/load/arena4.png");
            CNK_Data_Textures.Texture_Load_Arena5.ResourceToFile(path_gob_extracted + "common/load/arena5.png");
            CNK_Data_Textures.Texture_Load_Barin1.ResourceToFile(path_gob_extracted + "common/load/barin1.png");
            CNK_Data_Textures.Texture_Load_Barin2.ResourceToFile(path_gob_extracted + "common/load/barin2.png");
            CNK_Data_Textures.Texture_Load_Barin3.ResourceToFile(path_gob_extracted + "common/load/barin3.png");
            CNK_Data_Textures.Texture_Load_Citadel.ResourceToFile(path_gob_extracted + "common/load/citadel.png");
            CNK_Data_Textures.Texture_Load_Earth1.ResourceToFile(path_gob_extracted + "common/load/earth1.png");
            CNK_Data_Textures.Texture_Load_Earth2.ResourceToFile(path_gob_extracted + "common/load/earth2.png");
            CNK_Data_Textures.Texture_Load_Earth3.ResourceToFile(path_gob_extracted + "common/load/earth3.png");
            CNK_Data_Textures.Texture_Load_Fenom1.ResourceToFile(path_gob_extracted + "common/load/fenom1.png");
            CNK_Data_Textures.Texture_Load_Fenom2.ResourceToFile(path_gob_extracted + "common/load/fenom2.png");
            CNK_Data_Textures.Texture_Load_Fenom3.ResourceToFile(path_gob_extracted + "common/load/fenom3.png");
            CNK_Data_Textures.Texture_Load_Hub1.ResourceToFile(path_gob_extracted + "common/load/hub1.png");
            CNK_Data_Textures.Texture_Load_Hub2.ResourceToFile(path_gob_extracted + "common/load/hub2.png");
            CNK_Data_Textures.Texture_Load_Hub3.ResourceToFile(path_gob_extracted + "common/load/hub3.png");
            CNK_Data_Textures.Texture_Load_Hub4.ResourceToFile(path_gob_extracted + "common/load/hub4.png");
            CNK_Data_Textures.Texture_Load_Hub5.ResourceToFile(path_gob_extracted + "common/load/hub5.png");
            CNK_Data_Textures.Texture_Load_Teknee1.ResourceToFile(path_gob_extracted + "common/load/teknee1.png");
            CNK_Data_Textures.Texture_Load_Teknee2.ResourceToFile(path_gob_extracted + "common/load/teknee2.png");
            CNK_Data_Textures.Texture_Load_Teknee3.ResourceToFile(path_gob_extracted + "common/load/teknee3.png");
            CNK_Data_Textures.Texture_Load_Trophy.ResourceToFile(path_gob_extracted + "common/load/trophy.png");
            CNK_Data_Textures.Texture_Load_Velorace.ResourceToFile(path_gob_extracted + "common/load/velorace.png");
            CNK_Data_Textures.Texture_Load_MainMenu01.ResourceToFile(path_gob_extracted + "common/load/mainmenu01.png");
            CNK_Data_Textures.Texture_Load_MainMenu02.ResourceToFile(path_gob_extracted + "common/load/mainmenu02.png");
            CNK_Data_Textures.Texture_Load_MainMenu03.ResourceToFile(path_gob_extracted + "common/load/mainmenu03.png");
            CNK_Data_Textures.Texture_Load_MainMenu04.ResourceToFile(path_gob_extracted + "common/load/mainmenu04.png");
            CNK_Data_Textures.Texture_Load_MainMenu05.ResourceToFile(path_gob_extracted + "common/load/mainmenu05.png");
            CNK_Data_Textures.Texture_Load_MainMenu06.ResourceToFile(path_gob_extracted + "common/load/mainmenu06.png");
            CNK_Data_Textures.Texture_Load_MainMenu07.ResourceToFile(path_gob_extracted + "common/load/mainmenu07.png");
            CNK_Data_Textures.Texture_Load_MainMenu08.ResourceToFile(path_gob_extracted + "common/load/mainmenu08.png");
            CNK_Data_Textures.Texture_Load_MainMenu09.ResourceToFile(path_gob_extracted + "common/load/mainmenu09.png");
            CNK_Data_Textures.Texture_Load_MainMenu10.ResourceToFile(path_gob_extracted + "common/load/mainmenu10.png");
            CNK_Data_Textures.Texture_Load_MainMenu11.ResourceToFile(path_gob_extracted + "common/load/mainmenu11.png");
            CNK_Data_Textures.Texture_Load_MainMenu12.ResourceToFile(path_gob_extracted + "common/load/mainmenu12.png");
            CNK_Data_Textures.Texture_Load_MainMenu13.ResourceToFile(path_gob_extracted + "common/load/mainmenu13.png");


            EndModProcess();
        }

        void Mod_Randomize_Characters(Random randState)
        {
            //Replace model files
            string modelpath = path_gob_extracted;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                modelpath += "/ps2/gfx/chars/";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
            {
                modelpath += "/xbox/gfx/chars/";
            }
            else
            {
                modelpath += "/gcn/gfx/chars/";
            }

            List<int> charList = new List<int>();
            List<int> charList_rand = new List<int>();

            //Change 22 to 24 to add geary and velo minions, untested
            for (int i = 0; i < 22; i++)
            {
                charList.Add(i);
                File.Move(modelpath + CNK_Data.DriverModelTypes[i] + ".igb", modelpath + "Driver" + i + ".igb");
            }

            for (int i = 0; i < 22; i++)
            {
                int target_id = randState.Next(0, charList.Count);
                charList_rand.Add(charList[target_id]);
                charList.RemoveAt(target_id);
            }

            for (int i = 0; i < 22; i++)
            {
                File.Move(modelpath + "Driver" + charList_rand[i] + ".igb", modelpath + CNK_Data.DriverModelTypes[i] + ".igb");
            }

            //Replace voices (todo: some drivers have voiceline IDs that others don't)
            // Caused crashing on the Gamecube version
            /*
            string[] csv_voices = File.ReadAllLines(path_gob_extracted + "common/audio/voices.csv");

            List<string> csv_Voices_LineList = new List<string>();
            for (int i = 0; i < csv_voices.Length; i++)
            {
                csv_Voices_LineList.Add(csv_voices[i]);
            }

            string cur_line = "";
            int targetChar = 0;
            for (int i = 2; i < csv_Voices_LineList.Count; i++)
            {
                cur_line = csv_Voices_LineList[i];
                if (cur_line.Length > 2)
                {
                    for (int a = 0; a < charList_rand.Count; a++)
                    {
                        if (cur_line.Substring(0, 3) == CNK_Data.DriverAudioTypes[a])
                        {
                            targetChar = charList_rand[a];
                            cur_line = cur_line.Substring(0, 7) + CNK_Data.DriverAudioTypes[targetChar].Substring(0,1) + "/" + CNK_Data.DriverAudioTypes[targetChar] + cur_line.Substring(12);
                        }
                    }
                }
                csv_Voices_LineList[i] = cur_line;
            }

            csv_voices = new string[csv_Voices_LineList.Count];
            for (int i = 0; i < csv_Voices_LineList.Count; i++)
            {
                csv_voices[i] = csv_Voices_LineList[i];
            }

            File.WriteAllLines(path_gob_extracted + "common/audio/voices.csv", csv_voices);
            */
        }

        void Mod_Randomize_Karts(Random randState)
        {
            //Replace model files
            string modelpath = path_gob_extracted;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                modelpath += "/ps2/gfx/karts/";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
            {
                modelpath += "/xbox/gfx/karts/";
            }
            else
            {
                modelpath += "/gcn/gfx/karts/";
            }

            List<int> kartList = new List<int>();
            List<int> kartList_rand = new List<int>();
            string[] KartTypes = new string[] { "crash", "ncortex", "noxide", "ntrance", "boss_krunk", "boss_nash", "boss_norm_b", "boss_norm_l", "boss_geary", "boss_velo" , "boss" };

            //Boss karts crash the game
            for (int i = 0; i < 4; i++)
            {
                kartList.Add(i);
                File.Move(modelpath + KartTypes[i] + ".igb", modelpath + "Kart" + i + ".igb");
            }

            for (int i = 0; i < 4; i++)
            {
                int target_id = randState.Next(0, kartList.Count);
                kartList_rand.Add(kartList[target_id]);
                kartList.RemoveAt(target_id);
            }

            for (int i = 0; i < 4; i++)
            {
                File.Move(modelpath + "Kart" + kartList_rand[i] + ".igb", modelpath + KartTypes[i] + ".igb");
            }

        }

        void EndModProcess()
        {
            // Build GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                GobExtract.StartInfo.Arguments = ConsolePipeline.ProcessPath + "ASSETS.GOB" + " " + ConsolePipeline.ProcessPath + "cml_extr" + " -create 1";
            }
            else
            {
                GobExtract.StartInfo.Arguments = ConsolePipeline.ProcessPath + "assets.gob" + " " + ConsolePipeline.ProcessPath + "cml_extr" + " -create 1";
            }
            GobExtract.Start();
            GobExtract.WaitForExit();

            // Extraction cleanup
            if (Directory.Exists(path_gob_extracted))
            {
                DirectoryInfo di = new DirectoryInfo(path_gob_extracted);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                try
                {
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                    Directory.Delete(path_gob_extracted);
                }
                catch
                {

                }
                
            }
            
        }

        public override void StartPreload()
        {
            // Extract GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                GobExtract.StartInfo.Arguments = ConsolePipeline.ProcessPath + "assets.gob" + " " + ConsolePipeline.ProcessPath + "cml_extr";
            }
            else
            {
                GobExtract.StartInfo.Arguments = ConsolePipeline.ProcessPath + "ASSETS.GOB" + " " + ConsolePipeline.ProcessPath + "cml_extr";
            }
            GobExtract.Start();
            GobExtract.WaitForExit();

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                File.Delete(ConsolePipeline.ExtractedPath + "ASSETS.GFC");
                File.Delete(ConsolePipeline.ExtractedPath + "ASSETS.GOB");
            }
            else
            {
                File.Delete(ConsolePipeline.ExtractedPath + "assets.gfc");
                File.Delete(ConsolePipeline.ExtractedPath + "assets.gob");
            }
            path_gob_extracted = ConsolePipeline.ExtractedPath + @"\cml_extr\";

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "ps2/gfx/hud/8-bit_hud.png");
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "gcn/gfx/hud/8-bit_hud.png");
            }
            else
            {
                CNK_Data_Textures.Texture_HudIcons.FileToResource(path_gob_extracted + "xbox/gfx/hud/8-bit_hud.png");
            }

            CNK_Data_Textures.Texture_Font.FileToResource(path_gob_extracted + "common/fonts/all_fonts.png");

            CNK_Data_Textures.Texture_Load_Arena1.FileToResource(path_gob_extracted + "common/load/arena1.png");
            CNK_Data_Textures.Texture_Load_Arena2.FileToResource(path_gob_extracted + "common/load/arena2.png");
            CNK_Data_Textures.Texture_Load_Arena3.FileToResource(path_gob_extracted + "common/load/arena3.png");
            CNK_Data_Textures.Texture_Load_Arena4.FileToResource(path_gob_extracted + "common/load/arena4.png");
            CNK_Data_Textures.Texture_Load_Arena5.FileToResource(path_gob_extracted + "common/load/arena5.png");
            CNK_Data_Textures.Texture_Load_Barin1.FileToResource(path_gob_extracted + "common/load/barin1.png");
            CNK_Data_Textures.Texture_Load_Barin2.FileToResource(path_gob_extracted + "common/load/barin2.png");
            CNK_Data_Textures.Texture_Load_Barin3.FileToResource(path_gob_extracted + "common/load/barin3.png");
            CNK_Data_Textures.Texture_Load_Citadel.FileToResource(path_gob_extracted + "common/load/citadel.png");
            CNK_Data_Textures.Texture_Load_Earth1.FileToResource(path_gob_extracted + "common/load/earth1.png");
            CNK_Data_Textures.Texture_Load_Earth2.FileToResource(path_gob_extracted + "common/load/earth2.png");
            CNK_Data_Textures.Texture_Load_Earth3.FileToResource(path_gob_extracted + "common/load/earth3.png");
            CNK_Data_Textures.Texture_Load_Fenom1.FileToResource(path_gob_extracted + "common/load/fenom1.png");
            CNK_Data_Textures.Texture_Load_Fenom2.FileToResource(path_gob_extracted + "common/load/fenom2.png");
            CNK_Data_Textures.Texture_Load_Fenom3.FileToResource(path_gob_extracted + "common/load/fenom3.png");
            CNK_Data_Textures.Texture_Load_Hub1.FileToResource(path_gob_extracted + "common/load/hub1.png");
            CNK_Data_Textures.Texture_Load_Hub2.FileToResource(path_gob_extracted + "common/load/hub2.png");
            CNK_Data_Textures.Texture_Load_Hub3.FileToResource(path_gob_extracted + "common/load/hub3.png");
            CNK_Data_Textures.Texture_Load_Hub4.FileToResource(path_gob_extracted + "common/load/hub4.png");
            CNK_Data_Textures.Texture_Load_Hub5.FileToResource(path_gob_extracted + "common/load/hub5.png");
            CNK_Data_Textures.Texture_Load_Teknee1.FileToResource(path_gob_extracted + "common/load/teknee1.png");
            CNK_Data_Textures.Texture_Load_Teknee2.FileToResource(path_gob_extracted + "common/load/teknee2.png");
            CNK_Data_Textures.Texture_Load_Teknee3.FileToResource(path_gob_extracted + "common/load/teknee3.png");
            CNK_Data_Textures.Texture_Load_Trophy.FileToResource(path_gob_extracted + "common/load/trophy.png");
            CNK_Data_Textures.Texture_Load_Velorace.FileToResource(path_gob_extracted + "common/load/velorace.png");
            CNK_Data_Textures.Texture_Load_MainMenu01.FileToResource(path_gob_extracted + "common/load/mainmenu01.png");
            CNK_Data_Textures.Texture_Load_MainMenu02.FileToResource(path_gob_extracted + "common/load/mainmenu02.png");
            CNK_Data_Textures.Texture_Load_MainMenu03.FileToResource(path_gob_extracted + "common/load/mainmenu03.png");
            CNK_Data_Textures.Texture_Load_MainMenu04.FileToResource(path_gob_extracted + "common/load/mainmenu04.png");
            CNK_Data_Textures.Texture_Load_MainMenu05.FileToResource(path_gob_extracted + "common/load/mainmenu05.png");
            CNK_Data_Textures.Texture_Load_MainMenu06.FileToResource(path_gob_extracted + "common/load/mainmenu06.png");
            CNK_Data_Textures.Texture_Load_MainMenu07.FileToResource(path_gob_extracted + "common/load/mainmenu07.png");
            CNK_Data_Textures.Texture_Load_MainMenu08.FileToResource(path_gob_extracted + "common/load/mainmenu08.png");
            CNK_Data_Textures.Texture_Load_MainMenu09.FileToResource(path_gob_extracted + "common/load/mainmenu09.png");
            CNK_Data_Textures.Texture_Load_MainMenu10.FileToResource(path_gob_extracted + "common/load/mainmenu10.png");
            CNK_Data_Textures.Texture_Load_MainMenu11.FileToResource(path_gob_extracted + "common/load/mainmenu11.png");
            CNK_Data_Textures.Texture_Load_MainMenu12.FileToResource(path_gob_extracted + "common/load/mainmenu12.png");
            CNK_Data_Textures.Texture_Load_MainMenu13.FileToResource(path_gob_extracted + "common/load/mainmenu13.png");


        }

        public static string Float_To_CSV_Line(float targetfloat)
        {
            string cur_line = String.Format("{0:0.#########}", targetfloat);
            cur_line = cur_line.Replace(',', '.'); // For some reason String.Format is still not enough
            cur_line += ",";
            return cur_line;
        }
        public static string FloatArray_To_CSV_Line(float[] targetfloat)
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
        public static string FloatArray2_To_CSV_Line(float[,] targetfloat, int targetCharacter)
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
        public static string Int_To_CSV_Line(int targetInt)
        {
            string cur_line = targetInt.ToString();
            cur_line += ",";
            return cur_line;
        }
        static string CSV_WeaponSelection_RowID_To_RowText(CNK_Data_Powerups.WeaponSelectionRows RowID, float[] RowTable)
        {
            string row_text = "";
            row_text += ",,";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.EXPLOSIVE_CRATE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.FREEZING_MINE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.HOMING_MISSLE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.BOWLING_BOMB]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.TORNADO]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.STATIC_SHOCK]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.POWER_SHIELD]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.INVINCIBILITY_MASK]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.INVISIBILITY]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.VOODOO_DOLL]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.TROPY_CLOCKS]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.TURBO_BOOSTS]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.SUPER_ENGINE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.REDEYE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.HOMING_MISSLE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.BOWLING_BOMB_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.TURBO_BOOST_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.EXPCRATE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.FREEZEMINE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.STATICSHOCK_X3]);

            return row_text;
        }
    }
}
