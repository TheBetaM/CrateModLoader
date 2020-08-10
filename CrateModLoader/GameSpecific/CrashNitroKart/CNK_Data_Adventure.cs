using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    static partial class CNK_Data
    {
        public enum PadInfoNameID
        {
            Track_Earth1 = 0,
            Track_Earth2 = 1,
            Track_Earth3 = 2,
            Boss_EarthBoss = 3,
            Arena_EarthArena = 4,
            Track_Barin1 = 5,
            Track_Barin2 = 6,
            Track_Barin3 = 7,
            Boss_BarinBoss = 8,
            Arena_BarinArena = 9,
            Track_Fenomena1 = 10,
            Track_Fenomena2 = 11,
            Track_Fenomena3 = 12,
            Boss_FenomenaBoss = 13,
            Arena_FenomenaArena = 14,
            Track_Teknee1 = 15,
            Track_Teknee2 = 16,
            Track_Teknee3 = 17,
            Boss_TekneeBoss = 18,
            Arena_TekneeArena = 19,
            Boss_VeloBoss = 20,
            Warp_Earth_To_Citadel = 21,
            Warp_Barin_To_Citadel = 22,
            Warp_Fenomena_To_Citadel = 23,
            Warp_Teknee_To_Citadel = 24,
            Warp_Citadel_To_Earth = 25,
            Warp_Barin_To_Earth = 26,
            Warp_Teknee_To_Earth = 27,
            Warp_Citadel_To_Barin = 28,
            Warp_Earth_To_Barin = 29,
            Warp_Fenomena_To_Barin = 30,
            Warp_Citadel_To_Fenomena = 31,
            Warp_Barin_To_Fenomena = 32,
            Warp_Teknee_To_Fenomena = 33,
            Warp_Citadel_To_Teknee = 34,
            Warp_Fenomena_To_Teknee = 35,
            Warp_Earth_To_Teknee = 36,
            Warp_Vault_To_Citadel = 37,
            Warp_Citadel_To_Vault = 38,
            GemCup_Red = 39,
            GemCup_Green = 40,
            GemCup_Purple = 41,
            GemCup_Blue = 42,
            Track_VeloRace = 43,
            GemCup_Yellow = 44,
        }
        public static string[] PadInfoName = new string[]
        {
            "earth1",
            "earth2",
            "earth3",
            "earthboss",
            "eartharena",
            "barin1",
            "barin2",
            "barin3",
            "barinboss",
            "barinarena",
            "fenom1",
            "fenom2",
            "fenom3",
            "fenomboss",
            "fenomarena",
            "tek1",
            "tek2",
            "tek3",
            "tekboss",
            "tekarena",
            "velo",
            "citadela",
            "citadelb",
            "citadelf",
            "citadelt",
            "citadeleb",
            "earthb",
            "eartht",
            "citadelbb",
            "barina",
            "barinf",
            "citadelfb",
            "fenomb",
            "fenomt",
            "citadeltb",
            "tekneea",
            "tekf",
            "gemc",
            "citadelg",
            "redcup",
            "greencup",
            "purplecup",
            "bluecup",
            "velotime",
            "yellowcup",
        };

        public enum RewardID
        {
            Trophy = 0,
            Key = 1,
            Relic = 2,
            Relic_Sapphire = 2,
            Relic_Gold = 3,
            Relic_Platinum = 4,
            Token_Blue = 5,
            Token_Green = 6,
            Token_Red = 7,
            Token_Purple = 8,
            Token_Yellow = 9,
            Gem_Blue = 10,
            Gem_Green = 11,
            Gem_Red = 12,
            Gem_Purple = 13,
            Gem_Yellow = 14,
        }
        public static string[] RewardName = new string[]
        {
            "trophy", "key", "relic", "relic2", "relic3", "token_blue", "token_green", "token_red", "token_purple", "token_yellow", "gem_blue", "gem_green", "gem_red", "gem_purple", "gem_yellow"
        };

        public enum AdventureTracksManagerRows
        {
            Grid_Start_Row = 1,
            Number_Rows = 2,
        }
        public static int Adv_TracksManager_GridStartRow = 15;
        public static int Adv_TracksManager_NumberRows = 58;

        public static List<AdvTracksManagerEntry> Adv_TracksManager_EntryList = new List<AdvTracksManagerEntry>()
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

        public static List<GoalsToRewardsEntry> Adv_GoalsToRewards_EntryList = new List<GoalsToRewardsEntry>()
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

        public enum PadInfoEventID
        {
            Null = 0,
            One = 1,
            UsingWarpPad = 2,
            AccessTrack = 3,
            CrystalRequirements = 4,
            WinTrophy = 5,
            WinRelic = 6,
            WinToken = 7,
            CrystalArena = 8,
            WinKey = 9,
            WorldGreeting = 10,
            OpeningWorldGate = 11,
            MultiKeyWorldGate = 12,
            WinGem = 13,
            GemCup = 14,
            GemCupRequirements = 15,
            SecretTracks = 16,
            EarthBossGreeting = 17,
            EarthBossChallenge = 18,
            EarthBossWin = 19,
            BarinBossGreeting = 20,
            BarinBossChallenge = 21,
            BarinBossWin = 22,
            FenomBossGreeting = 23,
            FenomBossChallenge = 24,
            FenomBossWin = 25,
            TekneeBossGreeting = 26,
            TekneeBossChallenge = 27,
            TekneeBossWin = 28,
            VeloChallenge = 29,
            HangTimeBoost = 30,
            PowerSliding = 31,
            SlideBoost = 32,
            SlideBoostCombo = 33,
            BoostCounter = 34,
            ChooseDriver = 35,
            BoostGauge = 36,
            ResetBoost = 37,
            SlowSurfaces = 38,
            StartBoost = 39,
            BowlingBomb = 40,
            TNT = 41,
            BrakeSlide = 42,
            WumpaFruit = 43,
        }
        public static string[] PadInfoEventName =
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

        public enum PadInfoDescID
        {
            world_earth1 = 0,
            world_earth2 = 1,
            world_earth3 = 2,
            warp_kongo = 3,
            world_arena1 = 4,
            world_barin1 = 5,
            world_barin2 = 6,
            world_barin3 = 7,
            warp_nash = 8,
            world_arena2 = 9,
            world_fenom1 = 10,
            world_fenom2 = 11,
            world_fenom3 = 12,
            warp_norm = 13,
            world_arena3 = 14,
            world_teknee1 = 15,
            world_teknee2 = 16,
            world_teknee3 = 17,
            warp_otto = 18,
            world_arena4 = 19,
            velo_race_title = 20,
            world_citadel = 21,
            world_adv_hub_earth = 22,
            world_adv_hub_barin = 23,
            world_adv_hub_fenom = 24,
            world_adv_hub_teknee = 25,
            world_adv_hub_gem = 26,
            world_adv_gem_cup_red = 27,
            world_adv_gem_cup_green = 28,
            world_adv_gem_cup_purple = 29,
            world_adv_gem_cup_blue = 30,
            world_velo = 31,
            world_arena5 = 32,
        }
        public static string[] PadInfoDescTypes =
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

        public static List<WarpPadInfoEntry> Adv_WarpPadInfo_EntryList = new List<WarpPadInfoEntry>()
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

        public static TrackID[] GemCup_Red = new TrackID[] { TrackID.Earth_1, TrackID.Barin_1, TrackID.Teknee_1 };
        public static TrackID[] GemCup_Green = new TrackID[] { TrackID.Earth_2, TrackID.Barin_3, TrackID.Fenom_1 };
        public static TrackID[] GemCup_Blue = new TrackID[] { TrackID.Teknee_2, TrackID.Earth_3, TrackID.Fenom_3 };
        public static TrackID[] GemCup_Purple = new TrackID[] { TrackID.Barin_2, TrackID.Fenom_2, TrackID.Teknee_3 };
        public static TrackID[] GemCup_Yellow = new TrackID[] { TrackID.VeloRace, TrackID.VeloRace, TrackID.VeloRace }; // Unused gem cup with no default values

    }

    struct AdvTracksManagerEntry
    {
        public CNK_Data.PadInfoNameID PadName;
        public CNK_Data.SubModeID Submode;
        public CNK_Data.RewardID RewardNeeded;
        public int NumberNeeded;

        public AdvTracksManagerEntry(CNK_Data.PadInfoNameID pName, CNK_Data.SubModeID sMode, CNK_Data.RewardID rewNeed, int num)
        {
            PadName = pName;
            Submode = sMode;
            RewardNeeded = rewNeed;
            NumberNeeded = num;
        }
    }
    struct GoalsToRewardsEntry
    {
        public CNK_Data.TrackID Track;
        public CNK_Data.SubModeID Submode;
        public CNK_Data.RewardID Reward;

        public GoalsToRewardsEntry(CNK_Data.TrackID tck, CNK_Data.SubModeID sMode, CNK_Data.RewardID rew)
        {
            Track = tck;
            Submode = sMode;
            Reward = rew;
        }
    }
    struct WarpPadInfoEntry
    {
        public CNK_Data.PadInfoNameID PadName;
        public CNK_Data.PadInfoDescID PadDesc;
        public CNK_Data.TrackID Track;
        public CNK_Data.PadInfoEventID isWarpGate;
        public CNK_Data.PadInfoEventID PrimaryActEvent;
        public CNK_Data.PadInfoEventID SecondaryEvent;
        public CNK_Data.PadInfoEventID LockedEvent;
        public CNK_Data.PadInfoEventID LockedEvent2;
        public CNK_Data.PadInfoEventID[] BaseRewardEvent;
        public CNK_Data.PadInfoEventID RelicWonEvent;
        public CNK_Data.PadInfoEventID[] TokenWonEvent;

        public WarpPadInfoEntry(CNK_Data.PadInfoNameID pName, CNK_Data.PadInfoDescID pDesc, CNK_Data.TrackID tck, CNK_Data.PadInfoEventID WarpGate, CNK_Data.PadInfoEventID PrimEvent, CNK_Data.PadInfoEventID SecEvent, CNK_Data.PadInfoEventID LockEvent, CNK_Data.PadInfoEventID LockEvent2, CNK_Data.PadInfoEventID[] BaseRewEvent, CNK_Data.PadInfoEventID RelicEvent, CNK_Data.PadInfoEventID[] TokenEvent)
        {
            PadName = pName;
            PadDesc = pDesc;
            Track = tck;
            isWarpGate = WarpGate;
            PrimaryActEvent = PrimEvent;
            SecondaryEvent = SecEvent;
            LockedEvent = LockEvent;
            LockedEvent2 = LockEvent2;
            BaseRewardEvent = BaseRewEvent;
            RelicWonEvent = RelicEvent;
            TokenWonEvent = TokenEvent;
        }
    }
}
