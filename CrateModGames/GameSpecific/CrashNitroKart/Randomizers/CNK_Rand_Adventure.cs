using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_Adventure : ModStruct<CSV>
    {
        public string[] PadInfoEventName =
        {
            "0",
            "1",
            "UsingWarpPad",
            "AccessTrack",
            "CrystalRequirements",
            "WinTrophy",
            "WinRelic",
            "WinToken",
            "CrystalArena",
            "WinKey",
            "WorldGreeting",
            "OpeningWorldGate",
            "MultiKeyWorldGate",
            "WinGem",
            "GemCup",
            "GemCupRequirements",
            "SecretTracks",
            "EarthBossGreeting",
            "EarthBossChallenge",
            "EarthBossWin",
            "BarinBossGreeting",
            "BarinBossChallenge",
            "BarinBossWin",
            "FenomBossGreeting",
            "FenomBossChallenge",
            "FenomBossWin",
            "TekneeBossGreeting",
            "TekneeBossChallenge",
            "TekneeBossWin",
            "VeloChallenge",
            "HangTimeBoost",
            "PowerSliding",
            "SlideBoost",
            "SlideBoostCombo",
            "BoostCounter",
            "ChooseDriver",
            "BoostGauge",
            "ResetBoost",
            "SlowSurfaces",
            "StartBoost",
            "BowlingBomb",
            "TNT",
            "BrakeSlide",
            "WumpaFruit",
        };

        public string[] PadInfoDescTypes =
        {
            "world_earth1",
            "world_earth2",
            "world_earth3",
            "warp_kongo",
            "world_arena1",
            "world_barin1",
            "world_barin2",
            "world_barin3",
            "warp_nash",
            "world_arena2",
            "world_fenom1",
            "world_fenom2",
            "world_fenom3",
            "warp_norm",
            "world_arena3",
            "world_teknee1",
            "world_teknee2",
            "world_teknee3",
            "warp_otto",
            "world_arena4",
            "velo_race_title",
            "world_citadel",
            "world_adv_hub_earth",
            "world_adv_hub_barin",
            "world_adv_hub_fenom",
            "world_adv_hub_teknee",
            "world_adv_hub_gem",
            "world_adv_gem_cup_red",
            "world_adv_gem_cup_green",
            "world_adv_gem_cup_purple",
            "world_adv_gem_cup_blue",
            "world_velo",
            "world_arena5",
        };

        public List<AdvTracksManagerEntry> Adv_TracksManager_EntryList = new List<AdvTracksManagerEntry>()
        {
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Trophy, RewardID.Trophy, 0),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.CNK_Challenge, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Relic, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Trophy, RewardID.Trophy, 0),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.CNK_Challenge, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Relic, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Trophy, RewardID.Trophy, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.CNK_Challenge, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Relic, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_EarthBoss, SubModeID.Boss, RewardID.Trophy, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Arena_EarthArena, SubModeID.Crystal, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Trophy, RewardID.Trophy, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.CNK_Challenge, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Relic, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Trophy, RewardID.Trophy, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.CNK_Challenge, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Relic, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Trophy, RewardID.Trophy, 5),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.CNK_Challenge, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Relic, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_BarinBoss, SubModeID.Boss, RewardID.Trophy, 6),
            new AdvTracksManagerEntry(PadInfoNameID.Arena_BarinArena, SubModeID.Crystal, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Trophy, RewardID.Trophy, 6),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.CNK_Challenge, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Relic, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Trophy, RewardID.Trophy, 7),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.CNK_Challenge, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Relic, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Trophy, RewardID.Trophy, 8),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.CNK_Challenge, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Relic, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_FenomenaBoss, SubModeID.Boss, RewardID.Trophy, 9),
            new AdvTracksManagerEntry(PadInfoNameID.Arena_FenomenaArena, SubModeID.Crystal, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Trophy, RewardID.Trophy, 9),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.CNK_Challenge, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Relic, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Trophy, RewardID.Trophy, 10),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.CNK_Challenge, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Relic, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Trophy, RewardID.Trophy, 11),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.CNK_Challenge, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Relic, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_TekneeBoss, SubModeID.Boss, RewardID.Trophy, 12),
            new AdvTracksManagerEntry(PadInfoNameID.Arena_TekneeArena, SubModeID.Crystal, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_VeloBoss, SubModeID.Boss, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Barin, SubModeID.Trophy, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Fenomena, SubModeID.Trophy, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Teknee, SubModeID.Trophy, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Barin, SubModeID.Trophy, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Fenomena_To_Teknee, SubModeID.Trophy, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Barin_To_Fenomena, SubModeID.Trophy, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Teknee, SubModeID.Trophy, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.GemCup_Red, SubModeID.Gem, RewardID.Token_Red, 4),
            new AdvTracksManagerEntry(PadInfoNameID.GemCup_Blue, SubModeID.Gem, RewardID.Token_Blue, 4),
            new AdvTracksManagerEntry(PadInfoNameID.GemCup_Green, SubModeID.Gem, RewardID.Token_Green, 4),
            new AdvTracksManagerEntry(PadInfoNameID.GemCup_Purple, SubModeID.Gem, RewardID.Token_Purple, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Vault, SubModeID.Trophy, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Blue, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Red, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Green, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Purple, 1),
        };

        public List<GoalsToRewardsEntry> Adv_GoalsToRewards_EntryList = new List<GoalsToRewardsEntry>()
        {
            new GoalsToRewardsEntry(TrackID.Earth_1, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Earth_1, SubModeID.CNK_Challenge, RewardID.Token_Red),
            new GoalsToRewardsEntry(TrackID.Earth_1, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.CNK_Challenge, RewardID.Token_Green),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.Boss, RewardID.Key),
            new GoalsToRewardsEntry(TrackID.Earth_3, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Earth_3, SubModeID.CNK_Challenge, RewardID.Token_Blue),
            new GoalsToRewardsEntry(TrackID.Earth_3, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Arena_1, SubModeID.Crystal, RewardID.Token_Purple),
            new GoalsToRewardsEntry(TrackID.Barin_1, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Barin_1, SubModeID.CNK_Challenge, RewardID.Token_Red),
            new GoalsToRewardsEntry(TrackID.Barin_1, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Barin_2, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Barin_2, SubModeID.CNK_Challenge, RewardID.Token_Green),
            new GoalsToRewardsEntry(TrackID.Barin_2, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Barin_3, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Barin_3, SubModeID.CNK_Challenge, RewardID.Token_Blue),
            new GoalsToRewardsEntry(TrackID.Barin_3, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Barin_3, SubModeID.Boss, RewardID.Key),
            new GoalsToRewardsEntry(TrackID.Arena_2, SubModeID.Crystal, RewardID.Token_Purple),
            new GoalsToRewardsEntry(TrackID.Fenom_1, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Fenom_1, SubModeID.CNK_Challenge, RewardID.Token_Red),
            new GoalsToRewardsEntry(TrackID.Fenom_1, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Fenom_1, SubModeID.Boss, RewardID.Key),
            new GoalsToRewardsEntry(TrackID.Fenom_2, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Fenom_2, SubModeID.CNK_Challenge, RewardID.Token_Green),
            new GoalsToRewardsEntry(TrackID.Fenom_2, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Fenom_3, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Fenom_3, SubModeID.CNK_Challenge, RewardID.Token_Blue),
            new GoalsToRewardsEntry(TrackID.Fenom_3, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Arena_3, SubModeID.Crystal, RewardID.Token_Purple),
            new GoalsToRewardsEntry(TrackID.Teknee_1, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Teknee_1, SubModeID.CNK_Challenge, RewardID.Token_Red),
            new GoalsToRewardsEntry(TrackID.Teknee_1, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Teknee_2, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Teknee_2, SubModeID.CNK_Challenge, RewardID.Token_Green),
            new GoalsToRewardsEntry(TrackID.Teknee_2, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Teknee_2, SubModeID.Boss, RewardID.Key),
            new GoalsToRewardsEntry(TrackID.Teknee_3, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Teknee_3, SubModeID.CNK_Challenge, RewardID.Token_Blue),
            new GoalsToRewardsEntry(TrackID.Teknee_3, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Arena_4, SubModeID.Crystal, RewardID.Token_Purple),
            new GoalsToRewardsEntry(TrackID.Earth_1, SubModeID.Gem, RewardID.Gem_Red),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.Gem, RewardID.Gem_Green),
            new GoalsToRewardsEntry(TrackID.Earth_3, SubModeID.Gem, RewardID.Gem_Purple),
            new GoalsToRewardsEntry(TrackID.Barin_1, SubModeID.Gem, RewardID.Gem_Blue),
            new GoalsToRewardsEntry(TrackID.VeloRace, SubModeID.Relic, RewardID.Relic),
        };

        public List<WarpPadInfoEntry> Adv_WarpPadInfo_EntryList = new List<WarpPadInfoEntry>()
        {
            new WarpPadInfoEntry(PadInfoNameID.Track_Earth1,PadInfoDescID.world_earth1,TrackID.Earth_1,PadInfoEventID.Null,PadInfoEventID.UsingWarpPad,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.HangTimeBoost},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Earth2,PadInfoDescID.world_earth2,TrackID.Earth_2,PadInfoEventID.Null,PadInfoEventID.UsingWarpPad,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.PowerSliding},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Earth3,PadInfoDescID.world_earth3,TrackID.Earth_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.SlideBoost, PadInfoEventID.EarthBossGreeting},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_EarthBoss,PadInfoDescID.warp_kongo,TrackID.Earth_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.EarthBossChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinKey, PadInfoEventID.EarthBossWin},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Arena_EarthArena,PadInfoDescID.world_arena1,TrackID.Arena_1,PadInfoEventID.Null,PadInfoEventID.CrystalArena,PadInfoEventID.Null,PadInfoEventID.CrystalRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Barin1,PadInfoDescID.world_barin1,TrackID.Barin_1,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.SlideBoostCombo},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Barin2,PadInfoDescID.world_barin2,TrackID.Barin_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.BoostCounter},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Barin3,PadInfoDescID.world_barin3,TrackID.Barin_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.ChooseDriver, PadInfoEventID.BarinBossGreeting},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_BarinBoss,PadInfoDescID.warp_nash,TrackID.Barin_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.BarinBossChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinKey, PadInfoEventID.BarinBossWin},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Arena_BarinArena,PadInfoDescID.world_arena2,TrackID.Arena_2,PadInfoEventID.Null,PadInfoEventID.CrystalArena,PadInfoEventID.Null,PadInfoEventID.CrystalRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinToken , PadInfoEventID.StartBoost }),
            new WarpPadInfoEntry(PadInfoNameID.Track_Fenomena1,PadInfoDescID.world_fenom1,TrackID.Fenom_1,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.BoostGauge},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Fenomena2,PadInfoDescID.world_fenom2,TrackID.Fenom_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.ResetBoost},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Fenomena3,PadInfoDescID.world_fenom3,TrackID.Fenom_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.SlowSurfaces, PadInfoEventID.FenomBossGreeting},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_FenomenaBoss,PadInfoDescID.warp_norm,TrackID.Fenom_1,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.FenomBossChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinKey, PadInfoEventID.FenomBossWin},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Arena_FenomenaArena,PadInfoDescID.world_arena3,TrackID.Arena_3,PadInfoEventID.Null,PadInfoEventID.CrystalArena,PadInfoEventID.Null,PadInfoEventID.CrystalRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinToken, PadInfoEventID.BowlingBomb }),
            new WarpPadInfoEntry(PadInfoNameID.Track_Teknee1,PadInfoDescID.world_teknee1,TrackID.Teknee_1,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.TNT},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Teknee2,PadInfoDescID.world_teknee2,TrackID.Teknee_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.BrakeSlide},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Teknee3,PadInfoDescID.world_teknee3,TrackID.Teknee_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.WumpaFruit, PadInfoEventID.TekneeBossGreeting},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_TekneeBoss,PadInfoDescID.warp_otto,TrackID.Teknee_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.TekneeBossChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinKey, PadInfoEventID.TekneeBossWin},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Arena_TekneeArena,PadInfoDescID.world_arena4,TrackID.Arena_4,PadInfoEventID.Null,PadInfoEventID.CrystalArena,PadInfoEventID.Null,PadInfoEventID.CrystalRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_VeloBoss,PadInfoDescID.velo_race_title,TrackID.VeloRace,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.VeloChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Earth_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Barin_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Fenomena_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Teknee_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Earth,PadInfoDescID.world_adv_hub_earth,TrackID.Hub_1,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Barin_To_Earth,PadInfoDescID.world_adv_hub_earth,TrackID.Hub_1,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Teknee_To_Earth,PadInfoDescID.world_adv_hub_earth,TrackID.Hub_1,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Barin,PadInfoDescID.world_adv_hub_barin,TrackID.Hub_2,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.OpeningWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Earth_To_Barin,PadInfoDescID.world_adv_hub_barin,TrackID.Hub_2,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.OpeningWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Fenomena_To_Barin,PadInfoDescID.world_adv_hub_barin,TrackID.Hub_2,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Fenomena,PadInfoDescID.world_adv_hub_fenom,TrackID.Hub_3,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Barin_To_Fenomena,PadInfoDescID.world_adv_hub_fenom,TrackID.Hub_3,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Teknee_To_Fenomena,PadInfoDescID.world_adv_hub_fenom,TrackID.Hub_3,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Teknee,PadInfoDescID.world_adv_hub_teknee,TrackID.Hub_4,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Earth_To_Teknee,PadInfoDescID.world_adv_hub_teknee,TrackID.Hub_4,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Fenomena_To_Teknee,PadInfoDescID.world_adv_hub_teknee,TrackID.Hub_4,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Vault_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Vault,PadInfoDescID.world_adv_hub_gem,TrackID.Secr,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.GemCup_Red,PadInfoDescID.world_adv_gem_cup_red,TrackID.Earth_1,PadInfoEventID.Null,PadInfoEventID.GemCup,PadInfoEventID.Null,PadInfoEventID.GemCupRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinGem},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.GemCup_Green,PadInfoDescID.world_adv_gem_cup_green,TrackID.Earth_2,PadInfoEventID.Null,PadInfoEventID.GemCup,PadInfoEventID.Null,PadInfoEventID.GemCupRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinGem},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.GemCup_Purple,PadInfoDescID.world_adv_gem_cup_purple,TrackID.Earth_3,PadInfoEventID.Null,PadInfoEventID.GemCup,PadInfoEventID.Null,PadInfoEventID.GemCupRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinGem},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.GemCup_Blue,PadInfoDescID.world_adv_gem_cup_blue,TrackID.Barin_1,PadInfoEventID.Null,PadInfoEventID.GemCup,PadInfoEventID.Null,PadInfoEventID.GemCupRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinGem},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Track_VeloRace,PadInfoDescID.world_velo,TrackID.VeloRace,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.SecretTracks,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
        };

        public TrackID[] GemCup_Red = new TrackID[] { TrackID.Earth_1, TrackID.Barin_1, TrackID.Teknee_1 };
        public TrackID[] GemCup_Green = new TrackID[] { TrackID.Earth_2, TrackID.Barin_3, TrackID.Fenom_1 };
        public TrackID[] GemCup_Blue = new TrackID[] { TrackID.Teknee_2, TrackID.Earth_3, TrackID.Fenom_3 };
        public TrackID[] GemCup_Purple = new TrackID[] { TrackID.Barin_2, TrackID.Fenom_2, TrackID.Teknee_3 };
        public TrackID[] GemCup_Yellow = new TrackID[] { TrackID.VeloRace, TrackID.VeloRace, TrackID.VeloRace }; // Unused gem cup with no default values

        public override void BeforeModPass()
        {
            Random randState = GetRandom();

            CNK_Randomize_WarpPads(randState);
            CNK_Randomize_ReqsRewards(randState);
        }

        public override void ModPass(CSV file)
        {
            if (file.Name.ToLower() == "adventuretracksmanager.csv")
            {
                List<List<string>> table = file.Table;

                for (int i = 0; i < Adv_TracksManager_EntryList.Count; i++)
                {
                    List<string> cur_line = new List<string>() {
                        CNK_Common.PadInfoName[(int)Adv_TracksManager_EntryList[i].PadName],
                        "",
                        CNK_Common.SubModeName[(int)Adv_TracksManager_EntryList[i].Submode],
                        "",
                        CNK_Common.RewardName[(int)Adv_TracksManager_EntryList[i].RewardNeeded],
                        "",
                        Adv_TracksManager_EntryList[i].NumberNeeded.ToString(),
                    };

                    int x = i + 14;
                    table[x] = cur_line;
                }
                
            }
            else if (file.Name.ToLower() == "goalstorewardsconverter.csv")
            {
                List<List<string>> table = file.Table;

                int start_rewards = 0;
                int end_rewards = 0;
                for (int i = 0; i < table.Count; i++)
                {
                    if (table[i][0] == "start_rewards")
                    {
                        start_rewards = i;
                    }
                    if (table[i][0] == "end_rewards")
                    {
                        end_rewards = i;
                    }
                }
                for (int i = 1; i < end_rewards - start_rewards; i++)
                {
                    table.RemoveAt(start_rewards + 1);
                }

                for (int i = 0; i < Adv_GoalsToRewards_EntryList.Count; i++)
                {
                    List<string> cur_line = new List<string>() {
                        CNK_Common.TrackName[(int)Adv_GoalsToRewards_EntryList[i].Track],
                        CNK_Common.SubModeName[(int)Adv_GoalsToRewards_EntryList[i].Submode],
                        CNK_Common.RewardName[(int)Adv_GoalsToRewards_EntryList[i].Reward],
                    };
                    table.Insert(start_rewards + 1, cur_line);
                }

            }
            else if (file.Name.ToLower() == "warppadinfo.csv")
            {
                List<List<string>> table = file.Table;

                int start_padinfo = 0;
                int end_padinfo = 0;
                for (int i = 0; i < table.Count; i++)
                {
                    if (table[i][0] == "start_padinfo")
                    {
                        start_padinfo = i;
                    }
                    if (table[i][0] == "end_padinfo")
                    {
                        end_padinfo = i;
                    }
                }
                for (int i = 1; i < end_padinfo - start_padinfo; i++)
                {
                    table.RemoveAt(start_padinfo + 1);
                }

                for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
                {
                    List<string> cur_line = new List<string>() {
                        CNK_Common.PadInfoName[(int)Adv_WarpPadInfo_EntryList[i].PadName],
                        PadInfoDescTypes[(int)Adv_WarpPadInfo_EntryList[i].PadDesc],
                        CNK_Common.TrackName[(int)Adv_WarpPadInfo_EntryList[i].Track],
                        PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].isWarpGate],
                        PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].PrimaryActEvent],
                        PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].SecondaryEvent],
                        PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].LockedEvent],
                        PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].LockedEvent2],
                    };
                    if (Adv_WarpPadInfo_EntryList[i].BaseRewardEvent.Length > 2)
                    {
                        cur_line.Add(PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[0]] + ";" + 
                            PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[1]] + ";" + 
                            PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[2]]);
                    }
                    else if (Adv_WarpPadInfo_EntryList[i].BaseRewardEvent.Length > 1)
                    {
                        cur_line.Add(PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[0]] + ";" +
                            PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[1]] + ";");
                    }
                    else
                    {
                        cur_line.Add(PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].BaseRewardEvent[0]]);
                    }
                    
                    cur_line.Add(PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].RelicWonEvent]);
                    
                    if (Adv_WarpPadInfo_EntryList[i].TokenWonEvent.Length > 1)
                    {
                        cur_line.Add(PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].TokenWonEvent[0]] +
                            ";" + PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].TokenWonEvent[1]]);
                    }
                    else
                    {
                        cur_line.Add(PadInfoEventName[(int)Adv_WarpPadInfo_EntryList[i].TokenWonEvent[0]]);
                    }

                    table.Insert(start_padinfo + 1, cur_line);
                }
                
            }
            else if (file.Name.ToLower() == "adventurecup.csv")
            {
                List<List<string>> table = file.Table;

                table[10] = new List<string>()
                {
                    CNK_Common.TrackName[(int)GemCup_Red[0]],
                    CNK_Common.TrackName[(int)GemCup_Green[0]],
                    CNK_Common.TrackName[(int)GemCup_Blue[0]],
                    CNK_Common.TrackName[(int)GemCup_Purple[0]],
                    CNK_Common.TrackName[(int)GemCup_Yellow[0]],
                    "",
                    "",
                };

                table[11] = new List<string>()
                {
                    CNK_Common.TrackName[(int)GemCup_Red[1]],
                    CNK_Common.TrackName[(int)GemCup_Green[1]],
                    CNK_Common.TrackName[(int)GemCup_Blue[1]],
                    CNK_Common.TrackName[(int)GemCup_Purple[1]],
                    CNK_Common.TrackName[(int)GemCup_Yellow[1]],
                    "",
                    "",
                };

                table[12] = new List<string>()
                {
                    CNK_Common.TrackName[(int)GemCup_Red[2]],
                    CNK_Common.TrackName[(int)GemCup_Green[2]],
                    CNK_Common.TrackName[(int)GemCup_Blue[2]],
                    CNK_Common.TrackName[(int)GemCup_Purple[2]],
                    CNK_Common.TrackName[(int)GemCup_Yellow[2]],
                    "",
                    "",
                };

            }
        }

        public void CNK_Randomize_ReqsRewards(Random randState)
        {
            Adv_TracksManager_EntryList.Clear();
            //todo
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.CNK_Challenge, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Relic, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.CNK_Challenge, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Relic, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Trophy, RewardID.Trophy, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.CNK_Challenge, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Relic, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_EarthBoss, SubModeID.Boss, RewardID.Trophy, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_EarthArena, SubModeID.Crystal, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Trophy, RewardID.Trophy, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.CNK_Challenge, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Relic, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Trophy, RewardID.Trophy, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.CNK_Challenge, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Relic, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Trophy, RewardID.Trophy, 5));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.CNK_Challenge, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Relic, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_BarinBoss, SubModeID.Boss, RewardID.Trophy, 6));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_BarinArena, SubModeID.Crystal, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Trophy, RewardID.Trophy, 6));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.CNK_Challenge, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Relic, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Trophy, RewardID.Trophy, 7));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.CNK_Challenge, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Relic, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Trophy, RewardID.Trophy, 8));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.CNK_Challenge, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Relic, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_FenomenaBoss, SubModeID.Boss, RewardID.Trophy, 9));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_FenomenaArena, SubModeID.Crystal, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Trophy, RewardID.Trophy, 9));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.CNK_Challenge, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Relic, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Trophy, RewardID.Trophy, 10));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.CNK_Challenge, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Relic, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Trophy, RewardID.Trophy, 11));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.CNK_Challenge, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Relic, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_TekneeBoss, SubModeID.Boss, RewardID.Trophy, 12));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_TekneeArena, SubModeID.Crystal, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_VeloBoss, SubModeID.Boss, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Barin, SubModeID.Trophy, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Fenomena, SubModeID.Trophy, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Teknee, SubModeID.Trophy, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Barin, SubModeID.Trophy, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Fenomena_To_Teknee, SubModeID.Trophy, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Barin_To_Fenomena, SubModeID.Trophy, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Teknee, SubModeID.Trophy, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Red, SubModeID.Gem, RewardID.Token_Red, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Blue, SubModeID.Gem, RewardID.Token_Blue, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Green, SubModeID.Gem, RewardID.Token_Green, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Purple, SubModeID.Gem, RewardID.Token_Purple, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Vault, SubModeID.Trophy, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Blue, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Red, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Green, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Purple, 1));

            Adv_GoalsToRewards_EntryList.Add(new GoalsToRewardsEntry(TrackID.Arena_5, SubModeID.Crystal, RewardID.Token_Purple)); // Adds unused Terra Drome crystal challenge
            for (int i = 0; i < Adv_GoalsToRewards_EntryList.Count; i++)
            {
                Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(Adv_GoalsToRewards_EntryList[i].Track, Adv_GoalsToRewards_EntryList[i].Submode, Adv_GoalsToRewards_EntryList[i].Reward);
            }
        }

        public void CNK_Randomize_WarpPads(Random randState)
        {
            //todo: hubs? boss cutscenes, velorace testing

            //Tracks
            List<TrackID> ValidTracks = new List<TrackID>();
            List<PadInfoDescID> ValidDesc = new List<PadInfoDescID>();
            ValidTracks.Add(TrackID.Earth_1);
            ValidDesc.Add(PadInfoDescID.world_earth1);
            ValidTracks.Add(TrackID.Earth_2);
            ValidDesc.Add(PadInfoDescID.world_earth2);
            ValidTracks.Add(TrackID.Earth_3);
            ValidDesc.Add(PadInfoDescID.world_earth3);
            ValidTracks.Add(TrackID.Barin_1);
            ValidDesc.Add(PadInfoDescID.world_barin1);
            ValidTracks.Add(TrackID.Barin_2);
            ValidDesc.Add(PadInfoDescID.world_barin2);
            ValidTracks.Add(TrackID.Barin_3);
            ValidDesc.Add(PadInfoDescID.world_barin3);
            ValidTracks.Add(TrackID.Fenom_1);
            ValidDesc.Add(PadInfoDescID.world_fenom1);
            ValidTracks.Add(TrackID.Fenom_2);
            ValidDesc.Add(PadInfoDescID.world_fenom2);
            ValidTracks.Add(TrackID.Fenom_3);
            ValidDesc.Add(PadInfoDescID.world_fenom3);
            ValidTracks.Add(TrackID.Teknee_1);
            ValidDesc.Add(PadInfoDescID.world_teknee1);
            ValidTracks.Add(TrackID.Teknee_2);
            ValidDesc.Add(PadInfoDescID.world_teknee2);
            ValidTracks.Add(TrackID.Teknee_3);
            ValidDesc.Add(PadInfoDescID.world_teknee3);
            //ValidTracks.Add(TrackID.VeloRace); testing required
            //ValidDesc.Add(PadInfoDescID.world_velo);

            List<PadInfoNameID> ValidReplacements = new List<PadInfoNameID>()
            {
                PadInfoNameID.Track_Earth1,
                PadInfoNameID.Track_Earth2,
                PadInfoNameID.Track_Earth3,
                PadInfoNameID.Track_Barin1,
                PadInfoNameID.Track_Barin2,
                PadInfoNameID.Track_Barin3,
                PadInfoNameID.Track_Fenomena1,
                PadInfoNameID.Track_Fenomena2,
                PadInfoNameID.Track_Fenomena3,
                PadInfoNameID.Track_Teknee1,
                PadInfoNameID.Track_Teknee2,
                PadInfoNameID.Track_Teknee3,
            };

            int targetPos;
            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                if (ValidReplacements.Contains(Adv_WarpPadInfo_EntryList[i].PadName))
                {
                    targetPos = randState.Next(0, ValidTracks.Count);
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, ValidDesc[targetPos], ValidTracks[targetPos], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                    ValidTracks.RemoveAt(targetPos);
                    ValidDesc.RemoveAt(targetPos);
                }
            }

            //Crystal Arenas
            List<TrackID> ValidArenas = new List<TrackID>();
            List<PadInfoDescID> ValidDescArena = new List<PadInfoDescID>();
            ValidArenas.Add(TrackID.Arena_1);
            ValidArenas.Add(TrackID.Arena_2);
            ValidArenas.Add(TrackID.Arena_3);
            ValidArenas.Add(TrackID.Arena_4);
            ValidArenas.Add(TrackID.Arena_5);
            ValidDescArena.Add(PadInfoDescID.world_arena1);
            ValidDescArena.Add(PadInfoDescID.world_arena2);
            ValidDescArena.Add(PadInfoDescID.world_arena3);
            ValidDescArena.Add(PadInfoDescID.world_arena4);
            ValidDescArena.Add(PadInfoDescID.world_arena5);

            List<PadInfoNameID> ValidReplacementsArena = new List<PadInfoNameID>()
            {
                PadInfoNameID.Arena_EarthArena,
                PadInfoNameID.Arena_BarinArena,
                PadInfoNameID.Arena_FenomenaArena,
                PadInfoNameID.Arena_TekneeArena,
            };

            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                if (ValidReplacementsArena.Contains(Adv_WarpPadInfo_EntryList[i].PadName))
                {
                    targetPos = randState.Next(0, ValidArenas.Count);
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, ValidDescArena[targetPos], ValidArenas[targetPos], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                    ValidArenas.RemoveAt(targetPos);
                    ValidDescArena.RemoveAt(targetPos);
                }
            }

            //Bosses
            List<TrackID> ValidBosses = new List<TrackID>();
            List<PadInfoDescID> ValidDescBoss = new List<PadInfoDescID>();
            ValidBosses.Add(TrackID.Earth_2);
            ValidBosses.Add(TrackID.Barin_3);
            ValidBosses.Add(TrackID.Fenom_1);
            ValidBosses.Add(TrackID.Teknee_2);
            ValidDescBoss.Add(PadInfoDescID.warp_kongo);
            ValidDescBoss.Add(PadInfoDescID.warp_nash);
            ValidDescBoss.Add(PadInfoDescID.warp_norm);
            ValidDescBoss.Add(PadInfoDescID.warp_otto);
            //ValidBosses.Add(TrackID.VeloRace);
            //ValidDescBoss.Add(PadInfoDescID.velo_race_title);

            List<PadInfoNameID> ValidReplacementsBoss = new List<PadInfoNameID>()
            {
                PadInfoNameID.Boss_EarthBoss,
                PadInfoNameID.Boss_BarinBoss,
                PadInfoNameID.Boss_FenomenaBoss,
                PadInfoNameID.Boss_TekneeBoss,
            };

            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                if (ValidReplacementsBoss.Contains(Adv_WarpPadInfo_EntryList[i].PadName))
                {
                    targetPos = randState.Next(0, ValidBosses.Count);
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, ValidDescBoss[targetPos], ValidBosses[targetPos], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                    ValidBosses.RemoveAt(targetPos);
                    ValidDescBoss.RemoveAt(targetPos);
                }
            }

            //Gem Cups
            List<TrackID> GemTracks = new List<TrackID>();
            GemTracks.Add(TrackID.Earth_1);
            GemTracks.Add(TrackID.Earth_2);
            GemTracks.Add(TrackID.Earth_3);
            GemTracks.Add(TrackID.Barin_1);
            GemTracks.Add(TrackID.Barin_2);
            GemTracks.Add(TrackID.Barin_3);
            GemTracks.Add(TrackID.Fenom_1);
            GemTracks.Add(TrackID.Fenom_2);
            GemTracks.Add(TrackID.Fenom_3);
            GemTracks.Add(TrackID.Teknee_1);
            GemTracks.Add(TrackID.Teknee_2);
            GemTracks.Add(TrackID.Teknee_3);
            //GemTracks.Add(TrackID.VeloRace); testing required

            for (int i = 0; i < GemCup_Red.Length; i++)
            {
                targetPos = randState.Next(0, GemTracks.Count);
                GemCup_Red[i] = GemTracks[targetPos];
                GemTracks.RemoveAt(targetPos);
            }
            for (int i = 0; i < GemCup_Blue.Length; i++)
            {
                targetPos = randState.Next(0, GemTracks.Count);
                GemCup_Blue[i] = GemTracks[targetPos];
                GemTracks.RemoveAt(targetPos);
            }
            for (int i = 0; i < GemCup_Green.Length; i++)
            {
                targetPos = randState.Next(0, GemTracks.Count);
                GemCup_Green[i] = GemTracks[targetPos];
                GemTracks.RemoveAt(targetPos);
            }
            for (int i = 0; i < GemCup_Purple.Length; i++)
            {
                targetPos = randState.Next(0, GemTracks.Count);
                GemCup_Purple[i] = GemTracks[targetPos];
                GemTracks.RemoveAt(targetPos);
            }

            for (int i = 0; i < Adv_GoalsToRewards_EntryList.Count; i++)
            {
                if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Blue)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Blue[0], SubModeID.Gem, RewardID.Gem_Blue);
                }
                else if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Green)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Green[0], SubModeID.Gem, RewardID.Gem_Green);
                }
                else if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Purple)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Purple[0], SubModeID.Gem, RewardID.Gem_Purple);
                }
                else if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Red)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Red[0], SubModeID.Gem, RewardID.Gem_Red);
                }
                else if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Yellow)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Yellow[0], SubModeID.Gem, RewardID.Gem_Yellow);
                }
            }
            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Red)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Red[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
                else if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Blue)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Blue[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
                else if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Green)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Green[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
                else if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Purple)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Purple[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
                else if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Yellow)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Yellow[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
            }
        }


    }
}
