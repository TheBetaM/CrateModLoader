using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_PowerupDistribution : ModStruct<CSV>
    {
        private Random randState;

        public override void BeforeModPass()
        {
            randState = GetRandom();
        }

        public override void ModPass(CSV file)
        {
            if (file.Name.ToLower() == "playerweaponselection.csv" || file.Name.ToLower() == "playerweaponselection_boss.csv")
            {
                List<List<string>> table = file.Table;

                table[(int)WeaponSelectionRows.Track_Earth_1] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Earth_1, CNK_Data_Powerups.WeaponSelection_Track_Earth_1);
                table[(int)WeaponSelectionRows.Track_Earth_2] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Earth_2, CNK_Data_Powerups.WeaponSelection_Track_Earth_2);
                table[(int)WeaponSelectionRows.Track_Earth_3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Earth_3, CNK_Data_Powerups.WeaponSelection_Track_Earth_3);
                table[(int)WeaponSelectionRows.Track_Barin_1] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Barin_1, CNK_Data_Powerups.WeaponSelection_Track_Barin_1);
                table[(int)WeaponSelectionRows.Track_Barin_2] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Barin_2, CNK_Data_Powerups.WeaponSelection_Track_Barin_2);
                table[(int)WeaponSelectionRows.Track_Barin_3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Barin_3, CNK_Data_Powerups.WeaponSelection_Track_Barin_3);
                table[(int)WeaponSelectionRows.Track_Fenom_1] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Fenom_1, CNK_Data_Powerups.WeaponSelection_Track_Fenom_1);
                table[(int)WeaponSelectionRows.Track_Fenom_2] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Fenom_2, CNK_Data_Powerups.WeaponSelection_Track_Fenom_2);
                table[(int)WeaponSelectionRows.Track_Fenom_3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Fenom_3, CNK_Data_Powerups.WeaponSelection_Track_Fenom_3);
                table[(int)WeaponSelectionRows.Track_Teknee_1] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Teknee_1, CNK_Data_Powerups.WeaponSelection_Track_Teknee_1);
                table[(int)WeaponSelectionRows.Track_Teknee_2] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Teknee_2, CNK_Data_Powerups.WeaponSelection_Track_Teknee_2);
                table[(int)WeaponSelectionRows.Track_Teknee_3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Teknee_3, CNK_Data_Powerups.WeaponSelection_Track_Teknee_3);
                table[(int)WeaponSelectionRows.Track_VeloRace] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_VeloRace, CNK_Data_Powerups.WeaponSelection_Track_VeloRace);
                table[(int)WeaponSelectionRows.Track_Arena_1] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Arena_1, CNK_Data_Powerups.WeaponSelection_Track_Arena_1);
                table[(int)WeaponSelectionRows.Track_Arena_2] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Arena_2, CNK_Data_Powerups.WeaponSelection_Track_Arena_2);
                table[(int)WeaponSelectionRows.Track_Arena_3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Arena_3, CNK_Data_Powerups.WeaponSelection_Track_Arena_3);
                table[(int)WeaponSelectionRows.Track_Arena_4] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Arena_4, CNK_Data_Powerups.WeaponSelection_Track_Arena_4);
                table[(int)WeaponSelectionRows.Track_Arena_5] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Arena_5, CNK_Data_Powerups.WeaponSelection_Track_Arena_5);
                table[(int)WeaponSelectionRows.Track_Arena_6] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Arena_6, CNK_Data_Powerups.WeaponSelection_Track_Arena_6);
                table[(int)WeaponSelectionRows.Track_Arena_7] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Arena_7, CNK_Data_Powerups.WeaponSelection_Track_Arena_7);
                table[(int)WeaponSelectionRows.Track_Lobby] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Track_Lobby, CNK_Data_Powerups.WeaponSelection_Track_Lobby);
                table[(int)WeaponSelectionRows.Mode_Adv_Trophy] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Adv_Trophy, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Trophy);
                table[(int)WeaponSelectionRows.Mode_Adv_CNK] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Adv_CNK, CNK_Data_Powerups.WeaponSelection_Mode_Adv_CNK);
                table[(int)WeaponSelectionRows.Mode_Adv_Gem] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Adv_Gem, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Gem);
                table[(int)WeaponSelectionRows.Mode_Adv_Boss] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Adv_Boss, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Boss);
                table[(int)WeaponSelectionRows.Mode_Adv_Crystal] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Adv_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Crystal);
                table[(int)WeaponSelectionRows.Mode_Arcade] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Arcade, CNK_Data_Powerups.WeaponSelection_Mode_Arcade);
                table[(int)WeaponSelectionRows.Mode_Versus] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Versus, CNK_Data_Powerups.WeaponSelection_Mode_Versus);
                table[(int)WeaponSelectionRows.Mode_CrystalRace] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_CrystalRace, CNK_Data_Powerups.WeaponSelection_Mode_CrystalRace);
                table[(int)WeaponSelectionRows.Mode_Battle_Point] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Battle_Point, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Point);
                table[(int)WeaponSelectionRows.Mode_Battle_Time] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Battle_Time, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Time);
                table[(int)WeaponSelectionRows.Mode_Battle_Domination] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Battle_Domination, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Domination);
                table[(int)WeaponSelectionRows.Mode_Battle_CTF] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Battle_CTF, CNK_Data_Powerups.WeaponSelection_Mode_Battle_CTF);
                table[(int)WeaponSelectionRows.Mode_Battle_KOTR] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Battle_KOTR, CNK_Data_Powerups.WeaponSelection_Mode_Battle_KOTR);
                table[(int)WeaponSelectionRows.Mode_Battle_Crystal] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Battle_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Crystal);
                table[(int)WeaponSelectionRows.Mode_Lobby] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Mode_Lobby, CNK_Data_Powerups.WeaponSelection_Mode_Lobby);
                table[(int)WeaponSelectionRows.Rank_1st] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Rank_1st, CNK_Data_Powerups.WeaponSelection_Rank_1st);
                table[(int)WeaponSelectionRows.Rank_2nd] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Rank_2nd, CNK_Data_Powerups.WeaponSelection_Rank_2nd);
                table[(int)WeaponSelectionRows.Rank_3rd] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Rank_3rd, CNK_Data_Powerups.WeaponSelection_Rank_3rd);
                table[(int)WeaponSelectionRows.Rank_4th] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Rank_4th, CNK_Data_Powerups.WeaponSelection_Rank_4th);
                table[(int)WeaponSelectionRows.Rank_5th] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Rank_5th, CNK_Data_Powerups.WeaponSelection_Rank_5th);
                table[(int)WeaponSelectionRows.Rank_6th] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Rank_6th, CNK_Data_Powerups.WeaponSelection_Rank_6th);
                table[(int)WeaponSelectionRows.Rank_7th] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Rank_7th, CNK_Data_Powerups.WeaponSelection_Rank_7th);
                table[(int)WeaponSelectionRows.Rank_8th] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Rank_8th, CNK_Data_Powerups.WeaponSelection_Rank_8th);
                table[(int)WeaponSelectionRows.Progress_0] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_0, CNK_Data_Powerups.WeaponSelection_Progress_0);
                table[(int)WeaponSelectionRows.Progress_5] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_5, CNK_Data_Powerups.WeaponSelection_Progress_5);
                table[(int)WeaponSelectionRows.Progress_10] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_10, CNK_Data_Powerups.WeaponSelection_Progress_10);
                table[(int)WeaponSelectionRows.Progress_15] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_15, CNK_Data_Powerups.WeaponSelection_Progress_15);
                table[(int)WeaponSelectionRows.Progress_20] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_20, CNK_Data_Powerups.WeaponSelection_Progress_20);
                table[(int)WeaponSelectionRows.Progress_25] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_25, CNK_Data_Powerups.WeaponSelection_Progress_25);
                table[(int)WeaponSelectionRows.Progress_30] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_30, CNK_Data_Powerups.WeaponSelection_Progress_30);
                table[(int)WeaponSelectionRows.Progress_35] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_35, CNK_Data_Powerups.WeaponSelection_Progress_35);
                table[(int)WeaponSelectionRows.Progress_40] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_40, CNK_Data_Powerups.WeaponSelection_Progress_40);
                table[(int)WeaponSelectionRows.Progress_45] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_45, CNK_Data_Powerups.WeaponSelection_Progress_45);
                table[(int)WeaponSelectionRows.Progress_50] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_50, CNK_Data_Powerups.WeaponSelection_Progress_50);
                table[(int)WeaponSelectionRows.Progress_55] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_55, CNK_Data_Powerups.WeaponSelection_Progress_55);
                table[(int)WeaponSelectionRows.Progress_60] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_60, CNK_Data_Powerups.WeaponSelection_Progress_60);
                table[(int)WeaponSelectionRows.Progress_65] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_65, CNK_Data_Powerups.WeaponSelection_Progress_65);
                table[(int)WeaponSelectionRows.Progress_70] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_70, CNK_Data_Powerups.WeaponSelection_Progress_70);
                table[(int)WeaponSelectionRows.Progress_75] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_75, CNK_Data_Powerups.WeaponSelection_Progress_75);
                table[(int)WeaponSelectionRows.Progress_80] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_80, CNK_Data_Powerups.WeaponSelection_Progress_80);
                table[(int)WeaponSelectionRows.Progress_85] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_85, CNK_Data_Powerups.WeaponSelection_Progress_85);
                table[(int)WeaponSelectionRows.Progress_90] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_90, CNK_Data_Powerups.WeaponSelection_Progress_90);
                table[(int)WeaponSelectionRows.Progress_95] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Progress_95, CNK_Data_Powerups.WeaponSelection_Progress_95);
                table[(int)WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_CLEANINGFLUID, CNK_Data_Powerups.WeaponSelection_ActivePower_CLEANINGFLUID);
                table[(int)WeaponSelectionRows.ActivePower_CURSED] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_CURSED, CNK_Data_Powerups.WeaponSelection_ActivePower_CURSED);
                table[(int)WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                table[(int)WeaponSelectionRows.ActivePower_GRACED] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_GRACED, CNK_Data_Powerups.WeaponSelection_ActivePower_GRACED);
                table[(int)WeaponSelectionRows.ActivePower_ICED] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_ICED, CNK_Data_Powerups.WeaponSelection_ActivePower_ICED);
                table[(int)WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                table[(int)WeaponSelectionRows.ActivePower_INVISIBILITY] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActivePower_INVISIBILITY);
                table[(int)WeaponSelectionRows.ActivePower_INVULNERABLE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_INVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_INVULNERABLE);
                table[(int)WeaponSelectionRows.ActivePower_MIMECUBE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_MIMECUBE, CNK_Data_Powerups.WeaponSelection_ActivePower_MIMECUBE);
                table[(int)WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, CNK_Data_Powerups.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                table[(int)WeaponSelectionRows.ActivePower_POWER_SHIELD] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActivePower_POWER_SHIELD);
                table[(int)WeaponSelectionRows.ActivePower_RESETTING] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_RESETTING, CNK_Data_Powerups.WeaponSelection_ActivePower_RESETTING);
                table[(int)WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_ROLLINGBRUSH, CNK_Data_Powerups.WeaponSelection_ActivePower_ROLLINGBRUSH);
                table[(int)WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_SPIKYFRUIT, CNK_Data_Powerups.WeaponSelection_ActivePower_SPIKYFRUIT);
                table[(int)WeaponSelectionRows.ActivePower_STATICSHOCKED] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_STATICSHOCKED, CNK_Data_Powerups.WeaponSelection_ActivePower_STATICSHOCKED);
                table[(int)WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActivePower_SUPER_ENGINE);
                table[(int)WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                table[(int)WeaponSelectionRows.ActivePower_TEETHSTRIP] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_TEETHSTRIP, CNK_Data_Powerups.WeaponSelection_ActivePower_TEETHSTRIP);
                table[(int)WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_TIMEBUBBLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TIMEBUBBLE);
                table[(int)WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_TROPY_CLOCKS, CNK_Data_Powerups.WeaponSelection_ActivePower_TROPY_CLOCKS);
                table[(int)WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActivePower_TURBO_BOOSTS);
                table[(int)WeaponSelectionRows.ActivePower_WINDUPJAW] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActivePower_WINDUPJAW, CNK_Data_Powerups.WeaponSelection_ActivePower_WINDUPJAW);
                table[(int)WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_BOWLING_BOMB, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB);
                table[(int)WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                table[(int)WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_EXPCRATE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPCRATE_X3);
                table[(int)WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                table[(int)WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                table[(int)WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_FREEZING_MINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZING_MINE);
                table[(int)WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_HOMING_MISSLE, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE);
                table[(int)WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                table[(int)WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                table[(int)WeaponSelectionRows.ActiveWep_INVISIBILITY] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVISIBILITY);
                table[(int)WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActiveWep_POWER_SHIELD);
                table[(int)WeaponSelectionRows.ActiveWep_REDEYE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_REDEYE, CNK_Data_Powerups.WeaponSelection_ActiveWep_REDEYE);
                table[(int)WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                table[(int)WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_STATIC_SHOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATIC_SHOCK);
                table[(int)WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_SUPER_ENGINE);
                table[(int)WeaponSelectionRows.ActiveWep_TORNADO] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_TORNADO, CNK_Data_Powerups.WeaponSelection_ActiveWep_TORNADO);
                table[(int)WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_TROPY_CLOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_TROPY_CLOCK);
                table[(int)WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                table[(int)WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                table[(int)WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.ActiveWep_VOODOO_DOLL, CNK_Data_Powerups.WeaponSelection_ActiveWep_VOODOO_DOLL);
                table[(int)WeaponSelectionRows.KartsInFront_0] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_0, CNK_Data_Powerups.WeaponSelection_KartsInFront_0);
                table[(int)WeaponSelectionRows.KartsInFront_1] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_1, CNK_Data_Powerups.WeaponSelection_KartsInFront_1);
                table[(int)WeaponSelectionRows.KartsInFront_2] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_2, CNK_Data_Powerups.WeaponSelection_KartsInFront_2);
                table[(int)WeaponSelectionRows.KartsInFront_3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_3, CNK_Data_Powerups.WeaponSelection_KartsInFront_3);
                table[(int)WeaponSelectionRows.KartsInFront_4] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_4, CNK_Data_Powerups.WeaponSelection_KartsInFront_4);
                table[(int)WeaponSelectionRows.KartsInFront_5] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_5, CNK_Data_Powerups.WeaponSelection_KartsInFront_5);
                table[(int)WeaponSelectionRows.KartsInFront_6] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_6, CNK_Data_Powerups.WeaponSelection_KartsInFront_6);
                table[(int)WeaponSelectionRows.KartsInFront_7] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_7, CNK_Data_Powerups.WeaponSelection_KartsInFront_7);
                table[(int)WeaponSelectionRows.KartsInFront_8] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsInFront_8, CNK_Data_Powerups.WeaponSelection_KartsInFront_8);
                table[(int)WeaponSelectionRows.KartsBehind_0] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_0, CNK_Data_Powerups.WeaponSelection_KartsBehind_0);
                table[(int)WeaponSelectionRows.KartsBehind_1] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_1, CNK_Data_Powerups.WeaponSelection_KartsBehind_1);
                table[(int)WeaponSelectionRows.KartsBehind_2] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_2, CNK_Data_Powerups.WeaponSelection_KartsBehind_2);
                table[(int)WeaponSelectionRows.KartsBehind_3] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_3, CNK_Data_Powerups.WeaponSelection_KartsBehind_3);
                table[(int)WeaponSelectionRows.KartsBehind_4] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_4, CNK_Data_Powerups.WeaponSelection_KartsBehind_4);
                table[(int)WeaponSelectionRows.KartsBehind_5] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_5, CNK_Data_Powerups.WeaponSelection_KartsBehind_5);
                table[(int)WeaponSelectionRows.KartsBehind_6] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_6, CNK_Data_Powerups.WeaponSelection_KartsBehind_6);
                table[(int)WeaponSelectionRows.KartsBehind_7] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_7, CNK_Data_Powerups.WeaponSelection_KartsBehind_7);
                table[(int)WeaponSelectionRows.KartsBehind_8] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.KartsBehind_8, CNK_Data_Powerups.WeaponSelection_KartsBehind_8);
                table[(int)WeaponSelectionRows.Difficulty_Easiest] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Difficulty_Easiest, CNK_Data_Powerups.WeaponSelection_Difficulty_Easiest);
                table[(int)WeaponSelectionRows.Difficulty_Hardest] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Difficulty_Hardest, CNK_Data_Powerups.WeaponSelection_Difficulty_Hardest);
                table[(int)WeaponSelectionRows.Buddy_Ahead] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Buddy_Ahead, CNK_Data_Powerups.WeaponSelection_Buddy_Ahead);
                table[(int)WeaponSelectionRows.Buddy_Behind] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Buddy_Behind, CNK_Data_Powerups.WeaponSelection_Buddy_Behind);
                table[(int)WeaponSelectionRows.Buddy_InRange] = CNK_Common.CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows.Buddy_InRange, CNK_Data_Powerups.WeaponSelection_Buddy_InRange);
            }

        }

    }
}
