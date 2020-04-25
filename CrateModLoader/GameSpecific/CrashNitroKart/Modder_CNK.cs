using CrateModLoader.GameSpecific.CNK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//CNK Tools/API by BetaM, ManDude and eezstreet.
//Version number, seed and options are displayed in the Credits accessible from the main menu.
/* Mod settings available:
 * kart_AccelerationGainNormal - float - m_AccelerationGainNormal variable (example setting)
 * 
 * Mod Layers:
 * 1: ASSETS.GOB contents
 */

namespace CrateModLoader
{
    public sealed class Modder_CNK : Modder
    {
        // these values can be anything, as long as they're unique, otherwise an argument exception may be thrown when adding options
        // constants get transformed into their respective values at compile time
        internal const int RandomizeAdventure             = 0;
        internal const int RandomizeAdventureRequirements = 1;
        internal const int RandomizeCharacterStats        = 2;
        internal const int RandomizeKartStats             = 3;
        internal const int RandomizeAIKartStats           = 4;
        internal const int RandomizeSurfaceParameters     = 5;
        internal const int RandomizeWeaponPools           = 6;
        internal const int RandomizeWeapons               = 7;
        internal const int RandomizeCharacters            = 8;
        internal const int RandomizeKarts                 = 9;
        internal const int DisableFadeout                 = 10;
        internal const int DisablePopups                  = 11;
        internal const int SpeedUpMaskHints               = 12;
        internal const int RandomizeWumpaCrate            = 13;
        internal const int RandomizeObstacles             = 14;
        internal const int RandomizeCupPoints             = 15;
        internal const int RandomizeMusic                 = 16;
        internal const int NoMask                         = 17;
        internal const int NoAlchemyIntro                 = 18;

        public Modder_CNK()
        {
            Game = new Game()
            {
                Name = "Crash Nitro Kart",
                ShortName = "CrashNK",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.XBOX
                },
                API_Credit = "Tools/API by BetaM, ManDude and eezstreet",
                API_Link = string.Empty,
                Icon = Properties.Resources.icon_cnk,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
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
                RegionID_GCN = new RegionCode[] {
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
                RegionID_XBOX = new RegionCode[] {
                    new RegionCode() {
                    Name = "Crash Nitro Kart",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7, },
                    new RegionCode() {
                    Name = "Crash Nitro Kart",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
            };

            Options.Add(RandomizeAdventure, new ModOption("Randomize Adventure"));
            Options.Add(RandomizeCharacterStats, new ModOption("Randomize Character Stats"));
            Options.Add(RandomizeKartStats, new ModOption("Randomize Kart Stats")); // TODO
            //Options.Add(RandomizeWumpaCrate, new ModOption()); //TODO dda
            //Options.Add(RandomizeObstacles, new ModOption()); //TODO obstacles
            //Options.Add(RandomizeCupPoints, new ModOption()); // Maybe? gameprogression
            //Options.Add(RandomizeSurfaceParameters, new ModOption("Randomize Surface Parameters")); // TODO: later version
            //Options.Add(RandomizeWeaponPools, new ModOption("Randomize Powerup Distribution")); // TODO: later version
            Options.Add(RandomizeWeapons, new ModOption("Randomize Powerup Effects"));
            Options.Add(RandomizeCharacters, new ModOption("Randomize Drivers")); //TODO: later version: icon replacement, name replacement, main menu model replacement, adventure character select model
            Options.Add(RandomizeKarts, new ModOption("Randomize Karts"));
            //Options.Add(RandomizeMusic, new ModOption()); //TODO music.csv
            //Options.Add(NoMask, new ModOption()); //TODO, hinthistory.csv
            Options.Add(DisableFadeout, new ModOption("Disable Fadeout/Flash Overlay"));
            Options.Add(DisablePopups, new ModOption("Disable Unlock Popups"));
            Options.Add(SpeedUpMaskHints, new ModOption("Speed Up Mask Hint Appearance"));
            Options.Add(NoAlchemyIntro, new ModOption("Remove Intro Videos", true));
        }

        internal string path_gob_extracted = "";

        public override void OpenModMenu()
        {
            GameSpecific.ModMenu_CNK modMenu = new GameSpecific.ModMenu_CNK();
            modMenu.Show(Program.ModProgramForm);
        }

        public Random randState = new Random();

        public override void StartModProcess()
        {

            // Extract GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //GobExtract.StartInfo.Arguments = "Tools/ASSETS.GOB" + " " + "Tools/cml_extr";
            if (Program.ModProgram.isoType == ConsoleMode.GCN)
            {
                GobExtract.StartInfo.Arguments = "temp/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/assets.gob" + " " + "temp/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/cml_extr";
            }
            else
            {
                GobExtract.StartInfo.Arguments = "temp/ASSETS.GOB" + " " + "temp/cml_extr";
            }
            GobExtract.Start();
            GobExtract.WaitForExit();

            if (Program.ModProgram.isoType == ConsoleMode.GCN)
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/assets.gfc");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/assets.gob");
                path_gob_extracted = AppDomain.CurrentDomain.BaseDirectory + "/temp/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/cml_extr/";
            }
            else if (Program.ModProgram.isoType == ConsoleMode.PS2)
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/ASSETS.GFC");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/ASSETS.GOB");
                path_gob_extracted = AppDomain.CurrentDomain.BaseDirectory + "/temp/cml_extr/";
            }
            else
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/assets.gfc");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/assets.gob");
                path_gob_extracted = AppDomain.CurrentDomain.BaseDirectory + "/temp/cml_extr/";
            }

            ModProcess();
        }

        protected override void ModProcess()
        {
            randState = new Random(Program.ModProgram.randoSeed);

            ModCrates.InstallLayerMods(path_gob_extracted, 1);

            CNK_Settings.ParseSettings(path_gob_extracted);

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

            if (Options[NoAlchemyIntro].Enabled)
            {
                if (Program.ModProgram.isoType == ConsoleMode.PS2)
                {
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/ALCHEMY.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/ALCHEMY.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCO.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCO.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCODUT.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCODUT.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOENG.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOENG.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOFRE.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOFRE.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOGER.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOGER.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOITA.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOITA.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOSPA.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/FCOSPA.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCO.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCO.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCODUT.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCODUT.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOENG.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOENG.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOFRE.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOFRE.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOGER.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOGER.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOITA.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOITA.SFD;1");
                    if (File.Exists(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOSPA.SFD;1"))
                        File.Delete(Program.ModProgram.extractedPath + "/VIDEO/INTRO/SCOSPA.SFD;1");
                }
                else if (Program.ModProgram.isoType == ConsoleMode.GCN)
                {
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/alchemy.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/alchemy.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fco.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fco.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcodut.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcodut.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcoeng.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcoeng.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcofre.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcofre.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcoger.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcoger.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcoita.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcoita.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcospa.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/fcospa.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/sco.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/sco.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scodut.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scodut.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scoeng.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scoeng.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scofre.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scofre.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scoger.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scoger.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scoita.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scoita.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scospa.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/video/intro/scospa.sfd");
                }
                else
                {
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/alchemy.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/alchemy.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/fco.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/fco.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/fcodut.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/fcodut.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/fcoeng.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/fcoeng.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/fcofre.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/fcofre.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/fcoger.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/fcoger.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/fcoita.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/fcoita.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/fcospa.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/fcospa.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/sco.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/sco.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/scodut.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/scodut.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/scoeng.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/scoeng.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/scofre.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/scofre.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/scoger.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/scoger.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/scoita.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/scoita.sfd");
                    if (File.Exists(Program.ModProgram.extractedPath + "/video/intro/scospa.sfd"))
                        File.Delete(Program.ModProgram.extractedPath + "/video/intro/scospa.sfd");
                }
            }
            if (Options[RandomizeCharacters].Enabled)
            {
                Mod_Randomize_Characters(randState);
            }
            if (Options[RandomizeKarts].Enabled)
            {
                Mod_Randomize_Karts(randState);
            }
            if (Options[RandomizeAdventure].Enabled)
            {
                Editing_CSV_WarpPadInfo = true;
                Editing_CSV_AdventureCup = true;
                Editing_CSV_AdventureTracksManager = true;
                Editing_CSV_GoalsToRewardsConverter = true;
                CNK_Data.CNK_Randomize_WarpPads(randState);
                CNK_Data.CNK_Randomize_ReqsRewards(randState);
            }
            if (Options[RandomizeKartStats].Enabled)
            {
                Editing_CSV_KartPhysicsBase = true;
                CNK_Data.CNK_Randomize_KartStats(randState);
            }
            if (Options[RandomizeCharacterStats].Enabled)
            {
                Editing_CSV_CharacterPhysics = true;
                for (int i = 0; i < 16; i++)
                {
                    CNK_Data.CNK_Randomize_CharacterStats(randState, i);
                }
            }
            /*
            if (Options[RandomizeSurfaceParameters].Enabled)
            {
                Editing_CSV_SurfaceParams = true;
                CNK_Data.CNK_Randomize_SufParams(randState);
            }
            */
            if (Options[RandomizeWeapons].Enabled)
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
                CNK_Data.CNK_Randomize_PowerShield(randState);
                CNK_Data.CNK_Randomize_BowlingBomb(randState);
                CNK_Data.CNK_Randomize_FreezingMine(randState);
                CNK_Data.CNK_Randomize_HomingMissle(randState);
                CNK_Data.CNK_Randomize_InvincMask(randState);
                CNK_Data.CNK_Randomize_RedEye(randState);
                CNK_Data.CNK_Randomize_TNTCrate(randState);
                CNK_Data.CNK_Randomize_Tornado(randState);
                CNK_Data.CNK_Randomize_TurboBoost(randState);
                CNK_Data.CNK_Randomize_StaticShock(randState);
            }
            /*
            if (Options[RandomizeWeaponPools].Enabled)
            {
                Editing_CSV_PlayerWeaponSelection = true;
                Editing_CSV_PlayerWeaponSelection_Boss = true;
            }
            */
            if (Options[DisablePopups].Enabled)
            {
                Editing_CSV_Unlockables = true;
            }
            if (Options[SpeedUpMaskHints].Enabled)
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

            if (Options[RandomizeKartStats].Enabled)
            {
                Editing_CSV_AI_KartPhysicsBase = true;
                CNK_Data.CNK_Randomize_KartStats(randState);
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

                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Earth_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_1, CNK_Data.WeaponSelection_Track_Earth_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Earth_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_2, CNK_Data.WeaponSelection_Track_Earth_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Earth_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_3, CNK_Data.WeaponSelection_Track_Earth_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Barin_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_1, CNK_Data.WeaponSelection_Track_Barin_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Barin_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_2, CNK_Data.WeaponSelection_Track_Barin_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Barin_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_3, CNK_Data.WeaponSelection_Track_Barin_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_1, CNK_Data.WeaponSelection_Track_Fenom_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_2, CNK_Data.WeaponSelection_Track_Fenom_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_3, CNK_Data.WeaponSelection_Track_Fenom_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_1, CNK_Data.WeaponSelection_Track_Teknee_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_2, CNK_Data.WeaponSelection_Track_Teknee_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_3, CNK_Data.WeaponSelection_Track_Teknee_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_VeloRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_VeloRace, CNK_Data.WeaponSelection_Track_VeloRace);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_1, CNK_Data.WeaponSelection_Track_Arena_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_2, CNK_Data.WeaponSelection_Track_Arena_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_3, CNK_Data.WeaponSelection_Track_Arena_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_4, CNK_Data.WeaponSelection_Track_Arena_4);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_5, CNK_Data.WeaponSelection_Track_Arena_5);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_6, CNK_Data.WeaponSelection_Track_Arena_6);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Arena_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_7, CNK_Data.WeaponSelection_Track_Arena_7);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Track_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Lobby, CNK_Data.WeaponSelection_Track_Lobby);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Trophy] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Trophy, CNK_Data.WeaponSelection_Mode_Adv_Trophy);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_CNK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_CNK, CNK_Data.WeaponSelection_Mode_Adv_CNK);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Gem] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Gem, CNK_Data.WeaponSelection_Mode_Adv_Gem);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Boss] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Boss, CNK_Data.WeaponSelection_Mode_Adv_Boss);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Crystal, CNK_Data.WeaponSelection_Mode_Adv_Crystal);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Arcade] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Arcade, CNK_Data.WeaponSelection_Mode_Arcade);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Versus] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Versus, CNK_Data.WeaponSelection_Mode_Versus);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_CrystalRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_CrystalRace, CNK_Data.WeaponSelection_Mode_CrystalRace);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Point] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Point, CNK_Data.WeaponSelection_Mode_Battle_Point);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Time] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Time, CNK_Data.WeaponSelection_Mode_Battle_Time);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Domination] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Domination, CNK_Data.WeaponSelection_Mode_Battle_Domination);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_CTF] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_CTF, CNK_Data.WeaponSelection_Mode_Battle_CTF);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_KOTR] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_KOTR, CNK_Data.WeaponSelection_Mode_Battle_KOTR);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Crystal, CNK_Data.WeaponSelection_Mode_Battle_Crystal);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Mode_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Lobby, CNK_Data.WeaponSelection_Mode_Lobby);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_1st] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_1st, CNK_Data.WeaponSelection_Rank_1st);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_2nd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_2nd, CNK_Data.WeaponSelection_Rank_2nd);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_3rd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_3rd, CNK_Data.WeaponSelection_Rank_3rd);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_4th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_4th, CNK_Data.WeaponSelection_Rank_4th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_5th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_5th, CNK_Data.WeaponSelection_Rank_5th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_6th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_6th, CNK_Data.WeaponSelection_Rank_6th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_7th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_7th, CNK_Data.WeaponSelection_Rank_7th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Rank_8th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_8th, CNK_Data.WeaponSelection_Rank_8th);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_0, CNK_Data.WeaponSelection_Progress_0);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_5, CNK_Data.WeaponSelection_Progress_5);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_10] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_10, CNK_Data.WeaponSelection_Progress_10);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_15] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_15, CNK_Data.WeaponSelection_Progress_15);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_20] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_20, CNK_Data.WeaponSelection_Progress_20);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_25] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_25, CNK_Data.WeaponSelection_Progress_25);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_30] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_30, CNK_Data.WeaponSelection_Progress_30);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_35] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_35, CNK_Data.WeaponSelection_Progress_35);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_40] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_40, CNK_Data.WeaponSelection_Progress_40);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_45] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_45, CNK_Data.WeaponSelection_Progress_45);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_50] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_50, CNK_Data.WeaponSelection_Progress_50);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_55] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_55, CNK_Data.WeaponSelection_Progress_55);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_60] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_60, CNK_Data.WeaponSelection_Progress_60);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_65] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_65, CNK_Data.WeaponSelection_Progress_65);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_70] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_70, CNK_Data.WeaponSelection_Progress_70);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_75] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_75, CNK_Data.WeaponSelection_Progress_75);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_80] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_80, CNK_Data.WeaponSelection_Progress_80);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_85] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_85, CNK_Data.WeaponSelection_Progress_85);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_90] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_90, CNK_Data.WeaponSelection_Progress_90);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Progress_95] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_95, CNK_Data.WeaponSelection_Progress_95);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_CLEANINGFLUID, CNK_Data.WeaponSelection_ActivePower_CLEANINGFLUID);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_CURSED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_CURSED, CNK_Data.WeaponSelection_ActivePower_CURSED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, CNK_Data.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_GRACED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_GRACED, CNK_Data.WeaponSelection_ActivePower_GRACED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_ICED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_ICED, CNK_Data.WeaponSelection_ActivePower_ICED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, CNK_Data.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVISIBILITY, CNK_Data.WeaponSelection_ActivePower_INVISIBILITY);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVULNERABLE, CNK_Data.WeaponSelection_ActivePower_INVULNERABLE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_MIMECUBE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_MIMECUBE, CNK_Data.WeaponSelection_ActivePower_MIMECUBE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, CNK_Data.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_POWER_SHIELD, CNK_Data.WeaponSelection_ActivePower_POWER_SHIELD);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_RESETTING] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_RESETTING, CNK_Data.WeaponSelection_ActivePower_RESETTING);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_ROLLINGBRUSH, CNK_Data.WeaponSelection_ActivePower_ROLLINGBRUSH);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_SPIKYFRUIT, CNK_Data.WeaponSelection_ActivePower_SPIKYFRUIT);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_STATICSHOCKED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_STATICSHOCKED, CNK_Data.WeaponSelection_ActivePower_STATICSHOCKED);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_SUPER_ENGINE, CNK_Data.WeaponSelection_ActivePower_SUPER_ENGINE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, CNK_Data.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TEETHSTRIP] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TEETHSTRIP, CNK_Data.WeaponSelection_ActivePower_TEETHSTRIP);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TIMEBUBBLE, CNK_Data.WeaponSelection_ActivePower_TIMEBUBBLE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TROPY_CLOCKS, CNK_Data.WeaponSelection_ActivePower_TROPY_CLOCKS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TURBO_BOOSTS, CNK_Data.WeaponSelection_ActivePower_TURBO_BOOSTS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActivePower_WINDUPJAW] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_WINDUPJAW, CNK_Data.WeaponSelection_ActivePower_WINDUPJAW);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB, CNK_Data.WeaponSelection_ActiveWep_BOWLING_BOMB);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, CNK_Data.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_EXPCRATE_X3, CNK_Data.WeaponSelection_ActiveWep_EXPCRATE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, CNK_Data.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, CNK_Data.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_FREEZING_MINE, CNK_Data.WeaponSelection_ActiveWep_FREEZING_MINE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE, CNK_Data.WeaponSelection_ActiveWep_HOMING_MISSLE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, CNK_Data.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, CNK_Data.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_INVISIBILITY, CNK_Data.WeaponSelection_ActiveWep_INVISIBILITY);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_POWER_SHIELD, CNK_Data.WeaponSelection_ActiveWep_POWER_SHIELD);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_REDEYE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_REDEYE, CNK_Data.WeaponSelection_ActiveWep_REDEYE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, CNK_Data.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_STATIC_SHOCK, CNK_Data.WeaponSelection_ActiveWep_STATIC_SHOCK);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_SUPER_ENGINE, CNK_Data.WeaponSelection_ActiveWep_SUPER_ENGINE);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TORNADO] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TORNADO, CNK_Data.WeaponSelection_ActiveWep_TORNADO);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TROPY_CLOCK, CNK_Data.WeaponSelection_ActiveWep_TROPY_CLOCK);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, CNK_Data.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, CNK_Data.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_VOODOO_DOLL, CNK_Data.WeaponSelection_ActiveWep_VOODOO_DOLL);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_0, CNK_Data.WeaponSelection_KartsInFront_0);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_1, CNK_Data.WeaponSelection_KartsInFront_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_2, CNK_Data.WeaponSelection_KartsInFront_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_3, CNK_Data.WeaponSelection_KartsInFront_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_4, CNK_Data.WeaponSelection_KartsInFront_4);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_5, CNK_Data.WeaponSelection_KartsInFront_5);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_6, CNK_Data.WeaponSelection_KartsInFront_6);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_7, CNK_Data.WeaponSelection_KartsInFront_7);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsInFront_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_8, CNK_Data.WeaponSelection_KartsInFront_8);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_0, CNK_Data.WeaponSelection_KartsBehind_0);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_1, CNK_Data.WeaponSelection_KartsBehind_1);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_2, CNK_Data.WeaponSelection_KartsBehind_2);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_3, CNK_Data.WeaponSelection_KartsBehind_3);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_4, CNK_Data.WeaponSelection_KartsBehind_4);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_5, CNK_Data.WeaponSelection_KartsBehind_5);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_6, CNK_Data.WeaponSelection_KartsBehind_6);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_7, CNK_Data.WeaponSelection_KartsBehind_7);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.KartsBehind_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_8, CNK_Data.WeaponSelection_KartsBehind_8);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Difficulty_Easiest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Difficulty_Easiest, CNK_Data.WeaponSelection_Difficulty_Easiest);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Difficulty_Hardest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Difficulty_Hardest, CNK_Data.WeaponSelection_Difficulty_Hardest);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Buddy_Ahead] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_Ahead, CNK_Data.WeaponSelection_Buddy_Ahead);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Buddy_Behind] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_Behind, CNK_Data.WeaponSelection_Buddy_Behind);
                csv_PlayerWeaponSel[(int)CNK_Data.WeaponSelectionRows.Buddy_InRange] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_InRange, CNK_Data.WeaponSelection_Buddy_InRange);

                File.WriteAllLines(path_gob_extracted + "common/dda/playerweaponselection.csv", csv_PlayerWeaponSel);
            }
            if (Editing_CSV_PlayerWeaponSelection_Boss)
            {
                string[] csv_PlayerWeaponSelBoss = File.ReadAllLines(path_gob_extracted + "common/dda/playerweaponselection_boss.csv");

                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Earth_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_1, CNK_Data.WeaponSelection_Track_Earth_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Earth_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_2, CNK_Data.WeaponSelection_Track_Earth_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Earth_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Earth_3, CNK_Data.WeaponSelection_Track_Earth_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Barin_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_1, CNK_Data.WeaponSelection_Track_Barin_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Barin_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_2, CNK_Data.WeaponSelection_Track_Barin_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Barin_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Barin_3, CNK_Data.WeaponSelection_Track_Barin_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_1, CNK_Data.WeaponSelection_Track_Fenom_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_2, CNK_Data.WeaponSelection_Track_Fenom_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Fenom_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Fenom_3, CNK_Data.WeaponSelection_Track_Fenom_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_1, CNK_Data.WeaponSelection_Track_Teknee_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_2, CNK_Data.WeaponSelection_Track_Teknee_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Teknee_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Teknee_3, CNK_Data.WeaponSelection_Track_Teknee_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_VeloRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_VeloRace, CNK_Data.WeaponSelection_Track_VeloRace);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_1, CNK_Data.WeaponSelection_Track_Arena_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_2, CNK_Data.WeaponSelection_Track_Arena_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_3, CNK_Data.WeaponSelection_Track_Arena_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_4, CNK_Data.WeaponSelection_Track_Arena_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_5, CNK_Data.WeaponSelection_Track_Arena_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_6, CNK_Data.WeaponSelection_Track_Arena_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Arena_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Arena_7, CNK_Data.WeaponSelection_Track_Arena_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Track_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Track_Lobby, CNK_Data.WeaponSelection_Track_Lobby);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Trophy] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Trophy, CNK_Data.WeaponSelection_Mode_Adv_Trophy);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_CNK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_CNK, CNK_Data.WeaponSelection_Mode_Adv_CNK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Gem] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Gem, CNK_Data.WeaponSelection_Mode_Adv_Gem);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Boss] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Boss, CNK_Data.WeaponSelection_Mode_Adv_Boss);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Adv_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Adv_Crystal, CNK_Data.WeaponSelection_Mode_Adv_Crystal);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Arcade] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Arcade, CNK_Data.WeaponSelection_Mode_Arcade);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Versus] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Versus, CNK_Data.WeaponSelection_Mode_Versus);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_CrystalRace] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_CrystalRace, CNK_Data.WeaponSelection_Mode_CrystalRace);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Point] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Point, CNK_Data.WeaponSelection_Mode_Battle_Point);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Time] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Time, CNK_Data.WeaponSelection_Mode_Battle_Time);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Domination] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Domination, CNK_Data.WeaponSelection_Mode_Battle_Domination);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_CTF] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_CTF, CNK_Data.WeaponSelection_Mode_Battle_CTF);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_KOTR] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_KOTR, CNK_Data.WeaponSelection_Mode_Battle_KOTR);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Battle_Crystal] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Battle_Crystal, CNK_Data.WeaponSelection_Mode_Battle_Crystal);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Mode_Lobby] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Mode_Lobby, CNK_Data.WeaponSelection_Mode_Lobby);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_1st] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_1st, CNK_Data.WeaponSelection_Rank_1st);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_2nd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_2nd, CNK_Data.WeaponSelection_Rank_2nd);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_3rd] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_3rd, CNK_Data.WeaponSelection_Rank_3rd);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_4th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_4th, CNK_Data.WeaponSelection_Rank_4th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_5th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_5th, CNK_Data.WeaponSelection_Rank_5th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_6th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_6th, CNK_Data.WeaponSelection_Rank_6th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_7th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_7th, CNK_Data.WeaponSelection_Rank_7th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Rank_8th] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Rank_8th, CNK_Data.WeaponSelection_Rank_8th);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_0, CNK_Data.WeaponSelection_Progress_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_5, CNK_Data.WeaponSelection_Progress_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_10] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_10, CNK_Data.WeaponSelection_Progress_10);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_15] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_15, CNK_Data.WeaponSelection_Progress_15);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_20] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_20, CNK_Data.WeaponSelection_Progress_20);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_25] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_25, CNK_Data.WeaponSelection_Progress_25);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_30] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_30, CNK_Data.WeaponSelection_Progress_30);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_35] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_35, CNK_Data.WeaponSelection_Progress_35);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_40] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_40, CNK_Data.WeaponSelection_Progress_40);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_45] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_45, CNK_Data.WeaponSelection_Progress_45);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_50] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_50, CNK_Data.WeaponSelection_Progress_50);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_55] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_55, CNK_Data.WeaponSelection_Progress_55);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_60] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_60, CNK_Data.WeaponSelection_Progress_60);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_65] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_65, CNK_Data.WeaponSelection_Progress_65);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_70] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_70, CNK_Data.WeaponSelection_Progress_70);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_75] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_75, CNK_Data.WeaponSelection_Progress_75);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_80] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_80, CNK_Data.WeaponSelection_Progress_80);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_85] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_85, CNK_Data.WeaponSelection_Progress_85);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_90] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_90, CNK_Data.WeaponSelection_Progress_90);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Progress_95] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Progress_95, CNK_Data.WeaponSelection_Progress_95);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_CLEANINGFLUID, CNK_Data.WeaponSelection_ActivePower_CLEANINGFLUID);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_CURSED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_CURSED, CNK_Data.WeaponSelection_ActivePower_CURSED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, CNK_Data.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_GRACED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_GRACED, CNK_Data.WeaponSelection_ActivePower_GRACED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_ICED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_ICED, CNK_Data.WeaponSelection_ActivePower_ICED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, CNK_Data.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVISIBILITY, CNK_Data.WeaponSelection_ActivePower_INVISIBILITY);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_INVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_INVULNERABLE, CNK_Data.WeaponSelection_ActivePower_INVULNERABLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_MIMECUBE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_MIMECUBE, CNK_Data.WeaponSelection_ActivePower_MIMECUBE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, CNK_Data.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_POWER_SHIELD, CNK_Data.WeaponSelection_ActivePower_POWER_SHIELD);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_RESETTING] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_RESETTING, CNK_Data.WeaponSelection_ActivePower_RESETTING);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_ROLLINGBRUSH, CNK_Data.WeaponSelection_ActivePower_ROLLINGBRUSH);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_SPIKYFRUIT, CNK_Data.WeaponSelection_ActivePower_SPIKYFRUIT);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_STATICSHOCKED] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_STATICSHOCKED, CNK_Data.WeaponSelection_ActivePower_STATICSHOCKED);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_SUPER_ENGINE, CNK_Data.WeaponSelection_ActivePower_SUPER_ENGINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, CNK_Data.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TEETHSTRIP] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TEETHSTRIP, CNK_Data.WeaponSelection_ActivePower_TEETHSTRIP);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TIMEBUBBLE, CNK_Data.WeaponSelection_ActivePower_TIMEBUBBLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TROPY_CLOCKS, CNK_Data.WeaponSelection_ActivePower_TROPY_CLOCKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_TURBO_BOOSTS, CNK_Data.WeaponSelection_ActivePower_TURBO_BOOSTS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActivePower_WINDUPJAW] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActivePower_WINDUPJAW, CNK_Data.WeaponSelection_ActivePower_WINDUPJAW);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB, CNK_Data.WeaponSelection_ActiveWep_BOWLING_BOMB);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, CNK_Data.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_EXPCRATE_X3, CNK_Data.WeaponSelection_ActiveWep_EXPCRATE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, CNK_Data.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, CNK_Data.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_FREEZING_MINE, CNK_Data.WeaponSelection_ActiveWep_FREEZING_MINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE, CNK_Data.WeaponSelection_ActiveWep_HOMING_MISSLE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, CNK_Data.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, CNK_Data.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_INVISIBILITY] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_INVISIBILITY, CNK_Data.WeaponSelection_ActiveWep_INVISIBILITY);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_POWER_SHIELD, CNK_Data.WeaponSelection_ActiveWep_POWER_SHIELD);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_REDEYE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_REDEYE, CNK_Data.WeaponSelection_ActiveWep_REDEYE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, CNK_Data.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_STATIC_SHOCK, CNK_Data.WeaponSelection_ActiveWep_STATIC_SHOCK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_SUPER_ENGINE, CNK_Data.WeaponSelection_ActiveWep_SUPER_ENGINE);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TORNADO] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TORNADO, CNK_Data.WeaponSelection_ActiveWep_TORNADO);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TROPY_CLOCK, CNK_Data.WeaponSelection_ActiveWep_TROPY_CLOCK);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, CNK_Data.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, CNK_Data.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.ActiveWep_VOODOO_DOLL, CNK_Data.WeaponSelection_ActiveWep_VOODOO_DOLL);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_0, CNK_Data.WeaponSelection_KartsInFront_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_1, CNK_Data.WeaponSelection_KartsInFront_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_2, CNK_Data.WeaponSelection_KartsInFront_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_3, CNK_Data.WeaponSelection_KartsInFront_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_4, CNK_Data.WeaponSelection_KartsInFront_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_5, CNK_Data.WeaponSelection_KartsInFront_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_6, CNK_Data.WeaponSelection_KartsInFront_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_7, CNK_Data.WeaponSelection_KartsInFront_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsInFront_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsInFront_8, CNK_Data.WeaponSelection_KartsInFront_8);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_0] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_0, CNK_Data.WeaponSelection_KartsBehind_0);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_1] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_1, CNK_Data.WeaponSelection_KartsBehind_1);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_2] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_2, CNK_Data.WeaponSelection_KartsBehind_2);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_3] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_3, CNK_Data.WeaponSelection_KartsBehind_3);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_4] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_4, CNK_Data.WeaponSelection_KartsBehind_4);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_5] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_5, CNK_Data.WeaponSelection_KartsBehind_5);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_6] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_6, CNK_Data.WeaponSelection_KartsBehind_6);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_7] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_7, CNK_Data.WeaponSelection_KartsBehind_7);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.KartsBehind_8] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.KartsBehind_8, CNK_Data.WeaponSelection_KartsBehind_8);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Difficulty_Easiest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Difficulty_Easiest, CNK_Data.WeaponSelection_Difficulty_Easiest);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Difficulty_Hardest] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Difficulty_Hardest, CNK_Data.WeaponSelection_Difficulty_Hardest);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Buddy_Ahead] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_Ahead, CNK_Data.WeaponSelection_Buddy_Ahead);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Buddy_Behind] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_Behind, CNK_Data.WeaponSelection_Buddy_Behind);
                csv_PlayerWeaponSelBoss[(int)CNK_Data.WeaponSelectionRows.Buddy_InRange] = CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows.Buddy_InRange, CNK_Data.WeaponSelection_Buddy_InRange);

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

            if (Options[DisableFadeout].Enabled)
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
                    csv_Credits_LineList.Add("Crate Mod Loader " + Program.ModProgram.releaseVersionString + ",AlphaDance,1.25,C,4294950912,0,40");
                    csv_Credits_LineList.Add("Seed: " + Program.ModProgram.randoSeed + ",AlphaDance,1.25,C,4294950912,0,40");
                    csv_Credits_LineList.Add("Options: " + Program.ModProgram.optionsSelectedString + ",AlphaDance,1.25,C,4294950912,0,40");
                    csv_Credits_LineList.Add("Crash Nitro Kart,AlphaDance,1.25,C,4279304191,0,80");
                }
                else
                {
                    csv_Credits_LineList.Add("Crate Mod Loader " + Program.ModProgram.releaseVersionString + ",AlphaDance,1.25,C,0xFF10FFFF,0,40");
                    csv_Credits_LineList.Add("Seed: " + Program.ModProgram.randoSeed + ",AlphaDance,1.25,C,0xFF10FFFF,0,40");
                    csv_Credits_LineList.Add("Options: " + Program.ModProgram.optionsSelectedString + ",AlphaDance,1.25,C,0xFF10FFFF,0,40");
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


            EndModProcess();
        }

        void Mod_Randomize_Characters(Random randState)
        {
            //Replace model files
            string modelpath = path_gob_extracted;
            if (Program.ModProgram.isoType == ConsoleMode.PS2)
            {
                modelpath += "/ps2/gfx/chars/";
            }
            else if (Program.ModProgram.isoType == ConsoleMode.XBOX)
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
            if (Program.ModProgram.isoType == ConsoleMode.PS2)
            {
                modelpath += "/ps2/gfx/karts/";
            }
            else if (Program.ModProgram.isoType == ConsoleMode.XBOX)
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

        protected override void EndModProcess()
        {
            // Build GOB
            Process GobExtract = new Process();
            GobExtract.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract.exe";
            GobExtract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //GobExtract.StartInfo.Arguments = "Tools/ASSETS.GOB" + " " + "Tools/cml_extr" + " -create 1";
            if (Program.ModProgram.isoType == ConsoleMode.GCN)
            {
                GobExtract.StartInfo.Arguments = "temp/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/assets.gob" + " " + "temp/P-" + Program.ModProgram.ProductCode.Substring(0, 4) + "/files/cml_extr" + " -create 1";
            }
            else if (Program.ModProgram.isoType == ConsoleMode.PS2)
            {
                GobExtract.StartInfo.Arguments = "temp/ASSETS.GOB" + " " + "temp/cml_extr" + " -create 1";
            }
            else
            {
                GobExtract.StartInfo.Arguments = "temp/assets.gob" + " " + "temp/cml_extr" + " -create 1";
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
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_gob_extracted);
            }
            
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
        static string CSV_WeaponSelection_RowID_To_RowText(CNK_Data.WeaponSelectionRows RowID, float[] RowTable)
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
