using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_PowerupDistribution : ModStruct<string>
    {
        public override string Name => CNK_Text.Rand_Drivers;
        public override string Description => CNK_Text.Rand_DriversDesc;

        public override void BeforeModPass()
        {

        }

        public override void ModPass(string path_gob_extracted)
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);

            bool Editing_CSV_PlayerWeaponSelection = false;
            bool Editing_CSV_PlayerWeaponSelection_Boss = false;

            if (Editing_CSV_PlayerWeaponSelection)
            {
                string[] csv_PlayerWeaponSel = File.ReadAllLines(path_gob_extracted + "common/dda/playerweaponselection.csv");

                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Earth_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Earth_1, CNK_Data_Powerups.WeaponSelection_Track_Earth_1);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Earth_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Earth_2, CNK_Data_Powerups.WeaponSelection_Track_Earth_2);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Earth_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Earth_3, CNK_Data_Powerups.WeaponSelection_Track_Earth_3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Barin_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Barin_1, CNK_Data_Powerups.WeaponSelection_Track_Barin_1);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Barin_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Barin_2, CNK_Data_Powerups.WeaponSelection_Track_Barin_2);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Barin_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Barin_3, CNK_Data_Powerups.WeaponSelection_Track_Barin_3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Fenom_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Fenom_1, CNK_Data_Powerups.WeaponSelection_Track_Fenom_1);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Fenom_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Fenom_2, CNK_Data_Powerups.WeaponSelection_Track_Fenom_2);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Fenom_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Fenom_3, CNK_Data_Powerups.WeaponSelection_Track_Fenom_3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Teknee_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Teknee_1, CNK_Data_Powerups.WeaponSelection_Track_Teknee_1);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Teknee_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Teknee_2, CNK_Data_Powerups.WeaponSelection_Track_Teknee_2);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Teknee_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Teknee_3, CNK_Data_Powerups.WeaponSelection_Track_Teknee_3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_VeloRace] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_VeloRace, CNK_Data_Powerups.WeaponSelection_Track_VeloRace);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Arena_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_1, CNK_Data_Powerups.WeaponSelection_Track_Arena_1);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Arena_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_2, CNK_Data_Powerups.WeaponSelection_Track_Arena_2);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Arena_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_3, CNK_Data_Powerups.WeaponSelection_Track_Arena_3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Arena_4] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_4, CNK_Data_Powerups.WeaponSelection_Track_Arena_4);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Arena_5] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_5, CNK_Data_Powerups.WeaponSelection_Track_Arena_5);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Arena_6] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_6, CNK_Data_Powerups.WeaponSelection_Track_Arena_6);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Arena_7] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_7, CNK_Data_Powerups.WeaponSelection_Track_Arena_7);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Track_Lobby] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Lobby, CNK_Data_Powerups.WeaponSelection_Track_Lobby);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Adv_Trophy] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_Trophy, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Trophy);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Adv_CNK] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_CNK, CNK_Data_Powerups.WeaponSelection_Mode_Adv_CNK);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Adv_Gem] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_Gem, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Gem);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Adv_Boss] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_Boss, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Boss);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Adv_Crystal] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Crystal);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Arcade] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Arcade, CNK_Data_Powerups.WeaponSelection_Mode_Arcade);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Versus] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Versus, CNK_Data_Powerups.WeaponSelection_Mode_Versus);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_CrystalRace] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_CrystalRace, CNK_Data_Powerups.WeaponSelection_Mode_CrystalRace);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Battle_Point] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_Point, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Point);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Battle_Time] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_Time, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Time);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Battle_Domination] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_Domination, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Domination);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Battle_CTF] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_CTF, CNK_Data_Powerups.WeaponSelection_Mode_Battle_CTF);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Battle_KOTR] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_KOTR, CNK_Data_Powerups.WeaponSelection_Mode_Battle_KOTR);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Battle_Crystal] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Crystal);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Mode_Lobby] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Lobby, CNK_Data_Powerups.WeaponSelection_Mode_Lobby);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Rank_1st] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_1st, CNK_Data_Powerups.WeaponSelection_Rank_1st);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Rank_2nd] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_2nd, CNK_Data_Powerups.WeaponSelection_Rank_2nd);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Rank_3rd] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_3rd, CNK_Data_Powerups.WeaponSelection_Rank_3rd);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Rank_4th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_4th, CNK_Data_Powerups.WeaponSelection_Rank_4th);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Rank_5th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_5th, CNK_Data_Powerups.WeaponSelection_Rank_5th);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Rank_6th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_6th, CNK_Data_Powerups.WeaponSelection_Rank_6th);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Rank_7th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_7th, CNK_Data_Powerups.WeaponSelection_Rank_7th);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Rank_8th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_8th, CNK_Data_Powerups.WeaponSelection_Rank_8th);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_0] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_0, CNK_Data_Powerups.WeaponSelection_Progress_0);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_5] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_5, CNK_Data_Powerups.WeaponSelection_Progress_5);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_10] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_10, CNK_Data_Powerups.WeaponSelection_Progress_10);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_15] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_15, CNK_Data_Powerups.WeaponSelection_Progress_15);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_20] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_20, CNK_Data_Powerups.WeaponSelection_Progress_20);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_25] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_25, CNK_Data_Powerups.WeaponSelection_Progress_25);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_30] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_30, CNK_Data_Powerups.WeaponSelection_Progress_30);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_35] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_35, CNK_Data_Powerups.WeaponSelection_Progress_35);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_40] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_40, CNK_Data_Powerups.WeaponSelection_Progress_40);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_45] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_45, CNK_Data_Powerups.WeaponSelection_Progress_45);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_50] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_50, CNK_Data_Powerups.WeaponSelection_Progress_50);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_55] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_55, CNK_Data_Powerups.WeaponSelection_Progress_55);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_60] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_60, CNK_Data_Powerups.WeaponSelection_Progress_60);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_65] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_65, CNK_Data_Powerups.WeaponSelection_Progress_65);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_70] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_70, CNK_Data_Powerups.WeaponSelection_Progress_70);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_75] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_75, CNK_Data_Powerups.WeaponSelection_Progress_75);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_80] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_80, CNK_Data_Powerups.WeaponSelection_Progress_80);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_85] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_85, CNK_Data_Powerups.WeaponSelection_Progress_85);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_90] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_90, CNK_Data_Powerups.WeaponSelection_Progress_90);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Progress_95] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_95, CNK_Data_Powerups.WeaponSelection_Progress_95);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_CLEANINGFLUID, CNK_Data_Powerups.WeaponSelection_ActivePower_CLEANINGFLUID);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_CURSED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_CURSED, CNK_Data_Powerups.WeaponSelection_ActivePower_CURSED);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_GRACED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_GRACED, CNK_Data_Powerups.WeaponSelection_ActivePower_GRACED);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_ICED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_ICED, CNK_Data_Powerups.WeaponSelection_ActivePower_ICED);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_INVISIBILITY] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActivePower_INVISIBILITY);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_INVULNERABLE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_INVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_INVULNERABLE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_MIMECUBE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_MIMECUBE, CNK_Data_Powerups.WeaponSelection_ActivePower_MIMECUBE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, CNK_Data_Powerups.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_POWER_SHIELD] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActivePower_POWER_SHIELD);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_RESETTING] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_RESETTING, CNK_Data_Powerups.WeaponSelection_ActivePower_RESETTING);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_ROLLINGBRUSH, CNK_Data_Powerups.WeaponSelection_ActivePower_ROLLINGBRUSH);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_SPIKYFRUIT, CNK_Data_Powerups.WeaponSelection_ActivePower_SPIKYFRUIT);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_STATICSHOCKED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_STATICSHOCKED, CNK_Data_Powerups.WeaponSelection_ActivePower_STATICSHOCKED);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActivePower_SUPER_ENGINE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_TEETHSTRIP] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TEETHSTRIP, CNK_Data_Powerups.WeaponSelection_ActivePower_TEETHSTRIP);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TIMEBUBBLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TIMEBUBBLE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TROPY_CLOCKS, CNK_Data_Powerups.WeaponSelection_ActivePower_TROPY_CLOCKS);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActivePower_TURBO_BOOSTS);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActivePower_WINDUPJAW] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_WINDUPJAW, CNK_Data_Powerups.WeaponSelection_ActivePower_WINDUPJAW);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_BOWLING_BOMB, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_EXPCRATE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPCRATE_X3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_FREEZING_MINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZING_MINE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_HOMING_MISSLE, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_INVISIBILITY] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVISIBILITY);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActiveWep_POWER_SHIELD);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_REDEYE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_REDEYE, CNK_Data_Powerups.WeaponSelection_ActiveWep_REDEYE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_STATIC_SHOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATIC_SHOCK);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_SUPER_ENGINE);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_TORNADO] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_TORNADO, CNK_Data_Powerups.WeaponSelection_ActiveWep_TORNADO);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_TROPY_CLOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_TROPY_CLOCK);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_VOODOO_DOLL, CNK_Data_Powerups.WeaponSelection_ActiveWep_VOODOO_DOLL);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_0] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_0, CNK_Data_Powerups.WeaponSelection_KartsInFront_0);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_1, CNK_Data_Powerups.WeaponSelection_KartsInFront_1);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_2, CNK_Data_Powerups.WeaponSelection_KartsInFront_2);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_3, CNK_Data_Powerups.WeaponSelection_KartsInFront_3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_4] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_4, CNK_Data_Powerups.WeaponSelection_KartsInFront_4);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_5] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_5, CNK_Data_Powerups.WeaponSelection_KartsInFront_5);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_6] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_6, CNK_Data_Powerups.WeaponSelection_KartsInFront_6);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_7] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_7, CNK_Data_Powerups.WeaponSelection_KartsInFront_7);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsInFront_8] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_8, CNK_Data_Powerups.WeaponSelection_KartsInFront_8);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_0] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_0, CNK_Data_Powerups.WeaponSelection_KartsBehind_0);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_1, CNK_Data_Powerups.WeaponSelection_KartsBehind_1);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_2, CNK_Data_Powerups.WeaponSelection_KartsBehind_2);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_3, CNK_Data_Powerups.WeaponSelection_KartsBehind_3);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_4] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_4, CNK_Data_Powerups.WeaponSelection_KartsBehind_4);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_5] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_5, CNK_Data_Powerups.WeaponSelection_KartsBehind_5);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_6] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_6, CNK_Data_Powerups.WeaponSelection_KartsBehind_6);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_7] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_7, CNK_Data_Powerups.WeaponSelection_KartsBehind_7);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.KartsBehind_8] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_8, CNK_Data_Powerups.WeaponSelection_KartsBehind_8);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Difficulty_Easiest] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Difficulty_Easiest, CNK_Data_Powerups.WeaponSelection_Difficulty_Easiest);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Difficulty_Hardest] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Difficulty_Hardest, CNK_Data_Powerups.WeaponSelection_Difficulty_Hardest);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Buddy_Ahead] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Buddy_Ahead, CNK_Data_Powerups.WeaponSelection_Buddy_Ahead);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Buddy_Behind] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Buddy_Behind, CNK_Data_Powerups.WeaponSelection_Buddy_Behind);
                csv_PlayerWeaponSel[(int)WeaponSelectionRows.Buddy_InRange] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Buddy_InRange, CNK_Data_Powerups.WeaponSelection_Buddy_InRange);

                File.WriteAllLines(path_gob_extracted + "common/dda/playerweaponselection.csv", csv_PlayerWeaponSel);
            }
            if (Editing_CSV_PlayerWeaponSelection_Boss)
            {
                string[] csv_PlayerWeaponSelBoss = File.ReadAllLines(path_gob_extracted + "common/dda/playerweaponselection_boss.csv");

                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Earth_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Earth_1, CNK_Data_Powerups.WeaponSelection_Track_Earth_1);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Earth_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Earth_2, CNK_Data_Powerups.WeaponSelection_Track_Earth_2);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Earth_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Earth_3, CNK_Data_Powerups.WeaponSelection_Track_Earth_3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Barin_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Barin_1, CNK_Data_Powerups.WeaponSelection_Track_Barin_1);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Barin_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Barin_2, CNK_Data_Powerups.WeaponSelection_Track_Barin_2);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Barin_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Barin_3, CNK_Data_Powerups.WeaponSelection_Track_Barin_3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Fenom_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Fenom_1, CNK_Data_Powerups.WeaponSelection_Track_Fenom_1);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Fenom_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Fenom_2, CNK_Data_Powerups.WeaponSelection_Track_Fenom_2);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Fenom_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Fenom_3, CNK_Data_Powerups.WeaponSelection_Track_Fenom_3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Teknee_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Teknee_1, CNK_Data_Powerups.WeaponSelection_Track_Teknee_1);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Teknee_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Teknee_2, CNK_Data_Powerups.WeaponSelection_Track_Teknee_2);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Teknee_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Teknee_3, CNK_Data_Powerups.WeaponSelection_Track_Teknee_3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_VeloRace] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_VeloRace, CNK_Data_Powerups.WeaponSelection_Track_VeloRace);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Arena_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_1, CNK_Data_Powerups.WeaponSelection_Track_Arena_1);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Arena_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_2, CNK_Data_Powerups.WeaponSelection_Track_Arena_2);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Arena_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_3, CNK_Data_Powerups.WeaponSelection_Track_Arena_3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Arena_4] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_4, CNK_Data_Powerups.WeaponSelection_Track_Arena_4);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Arena_5] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_5, CNK_Data_Powerups.WeaponSelection_Track_Arena_5);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Arena_6] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_6, CNK_Data_Powerups.WeaponSelection_Track_Arena_6);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Arena_7] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Arena_7, CNK_Data_Powerups.WeaponSelection_Track_Arena_7);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Track_Lobby] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Track_Lobby, CNK_Data_Powerups.WeaponSelection_Track_Lobby);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Adv_Trophy] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_Trophy, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Trophy);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Adv_CNK] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_CNK, CNK_Data_Powerups.WeaponSelection_Mode_Adv_CNK);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Adv_Gem] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_Gem, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Gem);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Adv_Boss] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_Boss, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Boss);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Adv_Crystal] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Adv_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Adv_Crystal);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Arcade] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Arcade, CNK_Data_Powerups.WeaponSelection_Mode_Arcade);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Versus] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Versus, CNK_Data_Powerups.WeaponSelection_Mode_Versus);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_CrystalRace] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_CrystalRace, CNK_Data_Powerups.WeaponSelection_Mode_CrystalRace);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Battle_Point] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_Point, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Point);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Battle_Time] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_Time, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Time);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Battle_Domination] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_Domination, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Domination);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Battle_CTF] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_CTF, CNK_Data_Powerups.WeaponSelection_Mode_Battle_CTF);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Battle_KOTR] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_KOTR, CNK_Data_Powerups.WeaponSelection_Mode_Battle_KOTR);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Battle_Crystal] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Battle_Crystal, CNK_Data_Powerups.WeaponSelection_Mode_Battle_Crystal);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Mode_Lobby] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Mode_Lobby, CNK_Data_Powerups.WeaponSelection_Mode_Lobby);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Rank_1st] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_1st, CNK_Data_Powerups.WeaponSelection_Rank_1st);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Rank_2nd] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_2nd, CNK_Data_Powerups.WeaponSelection_Rank_2nd);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Rank_3rd] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_3rd, CNK_Data_Powerups.WeaponSelection_Rank_3rd);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Rank_4th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_4th, CNK_Data_Powerups.WeaponSelection_Rank_4th);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Rank_5th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_5th, CNK_Data_Powerups.WeaponSelection_Rank_5th);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Rank_6th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_6th, CNK_Data_Powerups.WeaponSelection_Rank_6th);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Rank_7th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_7th, CNK_Data_Powerups.WeaponSelection_Rank_7th);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Rank_8th] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Rank_8th, CNK_Data_Powerups.WeaponSelection_Rank_8th);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_0] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_0, CNK_Data_Powerups.WeaponSelection_Progress_0);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_5] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_5, CNK_Data_Powerups.WeaponSelection_Progress_5);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_10] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_10, CNK_Data_Powerups.WeaponSelection_Progress_10);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_15] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_15, CNK_Data_Powerups.WeaponSelection_Progress_15);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_20] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_20, CNK_Data_Powerups.WeaponSelection_Progress_20);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_25] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_25, CNK_Data_Powerups.WeaponSelection_Progress_25);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_30] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_30, CNK_Data_Powerups.WeaponSelection_Progress_30);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_35] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_35, CNK_Data_Powerups.WeaponSelection_Progress_35);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_40] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_40, CNK_Data_Powerups.WeaponSelection_Progress_40);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_45] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_45, CNK_Data_Powerups.WeaponSelection_Progress_45);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_50] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_50, CNK_Data_Powerups.WeaponSelection_Progress_50);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_55] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_55, CNK_Data_Powerups.WeaponSelection_Progress_55);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_60] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_60, CNK_Data_Powerups.WeaponSelection_Progress_60);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_65] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_65, CNK_Data_Powerups.WeaponSelection_Progress_65);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_70] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_70, CNK_Data_Powerups.WeaponSelection_Progress_70);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_75] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_75, CNK_Data_Powerups.WeaponSelection_Progress_75);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_80] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_80, CNK_Data_Powerups.WeaponSelection_Progress_80);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_85] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_85, CNK_Data_Powerups.WeaponSelection_Progress_85);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_90] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_90, CNK_Data_Powerups.WeaponSelection_Progress_90);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Progress_95] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Progress_95, CNK_Data_Powerups.WeaponSelection_Progress_95);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_CLEANINGFLUID] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_CLEANINGFLUID, CNK_Data_Powerups.WeaponSelection_ActivePower_CLEANINGFLUID);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_CURSED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_CURSED, CNK_Data_Powerups.WeaponSelection_ActivePower_CURSED);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActivePower_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_GRACED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_GRACED, CNK_Data_Powerups.WeaponSelection_ActivePower_GRACED);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_ICED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_ICED, CNK_Data_Powerups.WeaponSelection_ActivePower_ICED);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActivePower_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_INVISIBILITY] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActivePower_INVISIBILITY);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_INVULNERABLE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_INVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_INVULNERABLE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_MIMECUBE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_MIMECUBE, CNK_Data_Powerups.WeaponSelection_ActivePower_MIMECUBE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_POWERSHIELD_ZAPPED, CNK_Data_Powerups.WeaponSelection_ActivePower_POWERSHIELD_ZAPPED);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_POWER_SHIELD] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActivePower_POWER_SHIELD);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_RESETTING] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_RESETTING, CNK_Data_Powerups.WeaponSelection_ActivePower_RESETTING);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_ROLLINGBRUSH] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_ROLLINGBRUSH, CNK_Data_Powerups.WeaponSelection_ActivePower_ROLLINGBRUSH);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_SPIKYFRUIT] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_SPIKYFRUIT, CNK_Data_Powerups.WeaponSelection_ActivePower_SPIKYFRUIT);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_STATICSHOCKED] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_STATICSHOCKED, CNK_Data_Powerups.WeaponSelection_ActivePower_STATICSHOCKED);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_SUPER_ENGINE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActivePower_SUPER_ENGINE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_TEAMINVULNERABLE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TEAMINVULNERABLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TEAMINVULNERABLE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_TEETHSTRIP] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TEETHSTRIP, CNK_Data_Powerups.WeaponSelection_ActivePower_TEETHSTRIP);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_TIMEBUBBLE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TIMEBUBBLE, CNK_Data_Powerups.WeaponSelection_ActivePower_TIMEBUBBLE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_TROPY_CLOCKS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TROPY_CLOCKS, CNK_Data_Powerups.WeaponSelection_ActivePower_TROPY_CLOCKS);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_TURBO_BOOSTS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActivePower_TURBO_BOOSTS);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActivePower_WINDUPJAW] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActivePower_WINDUPJAW, CNK_Data_Powerups.WeaponSelection_ActivePower_WINDUPJAW);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_BOWLING_BOMB] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_BOWLING_BOMB, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_BOWLING_BOMB_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_BOWLING_BOMB_X3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_EXPCRATE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_EXPCRATE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPCRATE_X3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_EXPLOSIVE_CRATE, CNK_Data_Powerups.WeaponSelection_ActiveWep_EXPLOSIVE_CRATE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_FREEZEMINE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_FREEZEMINE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZEMINE_X3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_FREEZING_MINE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_FREEZING_MINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_FREEZING_MINE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_HOMING_MISSLE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_HOMING_MISSLE, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_HOMING_MISSLE_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_HOMING_MISSLE_X3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_INVINCIBILITY_MASKS, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVINCIBILITY_MASKS);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_INVISIBILITY] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_INVISIBILITY, CNK_Data_Powerups.WeaponSelection_ActiveWep_INVISIBILITY);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_POWER_SHIELD] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_POWER_SHIELD, CNK_Data_Powerups.WeaponSelection_ActiveWep_POWER_SHIELD);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_REDEYE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_REDEYE, CNK_Data_Powerups.WeaponSelection_ActiveWep_REDEYE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_STATICSHOCK_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_STATICSHOCK_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATICSHOCK_X3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_STATIC_SHOCK] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_STATIC_SHOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_STATIC_SHOCK);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_SUPER_ENGINE] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_SUPER_ENGINE, CNK_Data_Powerups.WeaponSelection_ActiveWep_SUPER_ENGINE);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_TORNADO] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_TORNADO, CNK_Data_Powerups.WeaponSelection_ActiveWep_TORNADO);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_TROPY_CLOCK] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_TROPY_CLOCK, CNK_Data_Powerups.WeaponSelection_ActiveWep_TROPY_CLOCK);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_TURBO_BOOSTS] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_TURBO_BOOSTS, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOSTS);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_TURBO_BOOST_X3, CNK_Data_Powerups.WeaponSelection_ActiveWep_TURBO_BOOST_X3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.ActiveWep_VOODOO_DOLL] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.ActiveWep_VOODOO_DOLL, CNK_Data_Powerups.WeaponSelection_ActiveWep_VOODOO_DOLL);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_0] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_0, CNK_Data_Powerups.WeaponSelection_KartsInFront_0);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_1, CNK_Data_Powerups.WeaponSelection_KartsInFront_1);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_2, CNK_Data_Powerups.WeaponSelection_KartsInFront_2);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_3, CNK_Data_Powerups.WeaponSelection_KartsInFront_3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_4] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_4, CNK_Data_Powerups.WeaponSelection_KartsInFront_4);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_5] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_5, CNK_Data_Powerups.WeaponSelection_KartsInFront_5);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_6] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_6, CNK_Data_Powerups.WeaponSelection_KartsInFront_6);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_7] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_7, CNK_Data_Powerups.WeaponSelection_KartsInFront_7);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsInFront_8] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsInFront_8, CNK_Data_Powerups.WeaponSelection_KartsInFront_8);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_0] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_0, CNK_Data_Powerups.WeaponSelection_KartsBehind_0);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_1] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_1, CNK_Data_Powerups.WeaponSelection_KartsBehind_1);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_2] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_2, CNK_Data_Powerups.WeaponSelection_KartsBehind_2);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_3] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_3, CNK_Data_Powerups.WeaponSelection_KartsBehind_3);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_4] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_4, CNK_Data_Powerups.WeaponSelection_KartsBehind_4);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_5] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_5, CNK_Data_Powerups.WeaponSelection_KartsBehind_5);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_6] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_6, CNK_Data_Powerups.WeaponSelection_KartsBehind_6);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_7] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_7, CNK_Data_Powerups.WeaponSelection_KartsBehind_7);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.KartsBehind_8] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.KartsBehind_8, CNK_Data_Powerups.WeaponSelection_KartsBehind_8);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Difficulty_Easiest] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Difficulty_Easiest, CNK_Data_Powerups.WeaponSelection_Difficulty_Easiest);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Difficulty_Hardest] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Difficulty_Hardest, CNK_Data_Powerups.WeaponSelection_Difficulty_Hardest);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Buddy_Ahead] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Buddy_Ahead, CNK_Data_Powerups.WeaponSelection_Buddy_Ahead);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Buddy_Behind] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Buddy_Behind, CNK_Data_Powerups.WeaponSelection_Buddy_Behind);
                csv_PlayerWeaponSelBoss[(int)WeaponSelectionRows.Buddy_InRange] = CNK_Common.CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows.Buddy_InRange, CNK_Data_Powerups.WeaponSelection_Buddy_InRange);

                File.WriteAllLines(path_gob_extracted + "common/dda/playerweaponselection_boss.csv", csv_PlayerWeaponSelBoss);
            }

        }

    }
}
