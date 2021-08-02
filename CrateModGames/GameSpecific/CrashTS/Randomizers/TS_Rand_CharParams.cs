using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: test
    public class TS_Rand_CharParams : ModStruct<ChunkInfoRM>
    {
        internal List<uint> musicTypes = new List<uint>();
        internal List<uint> randMusicList = new List<uint>();

        private bool isSet = false;

        public TS_Rand_CharParams()
        {
            isSet = TS_Props_Main.Option_RandCharacterParams.Disabled;
        }

        public override void BeforeModPass()
        {
            if (!isSet)
            {
                Random randState = GetRandom();

                Twins_Randomize_Character((int)CharacterID.Crash, ref randState);
                Twins_Randomize_Character((int)CharacterID.Cortex, ref randState);
                Twins_Randomize_Character((int)CharacterID.Nina, ref randState);
            }
        }

        private void Twins_Randomize_Character(int charID, ref Random randState)
        {
            if (charID == (int)CharacterID.Mechabandicoot)
            {
                return;
            }

            Twins_Data_Characters.CharInts_SpawnHealth.Value[charID] = (uint)randState.Next(1, 4);
            //Twins_Data_Characters.CharFloats_AirGravity.Value[charID] = randState.Next(40, 60);
            Twins_Data_Characters.CharFloats_WalkSpeed.Value[charID] = randState.Next(20, 60) / 10f;
            Twins_Data_Characters.CharFloats_RunSpeed.Value[charID] = randState.Next(7, 14);
            Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[charID] = randState.Next(7, 15);
            Twins_Data_Characters.CharFloats_SpinLength.Value[charID] = randState.Next(25, 100) / 100f;
            Twins_Data_Characters.CharFloats_SpinDelay.Value[charID] = randState.Next(10, 20) / 100f;
            //Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[charID] = randState.Next(6, 10);
            //Twins_Data_Characters.CharFloats_JumpHeight.Value[charID] = randState.Next(13, 18);
            Twins_Data_Characters.CharFloats_CrawlSpeed.Value[charID] = randState.Next(125, 400) / 100f;
            Twins_Data_Characters.CharFloats_SlideSpeed.Value[charID] = randState.Next(10, 24);

            if (charID == (int)CharacterID.Crash)
            {
                //Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[charID] = CharFloats_JumpHeight.Value[charID] + randState.Next(1, 5);
                //Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[charID] = randState.Next(9, 16);
                Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[charID] = randState.Next(20, 60) / 100f;
            }
            else if (charID == (int)CharacterID.Cortex)
            {
                Twins_Data_Characters.CharFloats_StrafingSpeed.Value[charID] = randState.Next(3, 10);
                Twins_Data_Characters.CharFloats_GunChargeTime.Value[charID] = randState.Next(10, 100) / 100f;
                Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[charID] = randState.Next(10, 100) / 100f;
                Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[charID] = randState.Next(2, 10) / 100f;
            }
            else if (charID == (int)CharacterID.Nina)
            {

            }
            else if (charID == (int)CharacterID.Mechabandicoot)
            {
                Twins_Data_Characters.CharFloats_WalkSpeed.Value[charID] = randState.Next(120, 240) / 10f;
                Twins_Data_Characters.CharFloats_RunSpeed.Value[charID] = 0;
            }

        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (uint)ObjectID.CRASH)
                        {
                            // Crash mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data_Characters.CharFloats_AirGravity.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data_Characters.CharFloats_BaseGravity.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data_Characters.CharFloats_BodyslamGravityForce.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data_Characters.CharFloats_BodyslamUpwardForce.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data_Characters.CharFloats_CrawlSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data_Characters.CharFloats_CrawlTimeFromStand.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data_Characters.CharFloats_CrawlTimeToRun.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data_Characters.CharFloats_CrawlTimeToStand.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data_Characters.CharFloats_DoubleJumpArcUnk.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data_Characters.CharFloats_DoubleJumpUnk22.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data_Characters.CharFloats_FlyingKickForwardSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data_Characters.CharFloats_FlyingKickGravity.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data_Characters.CharFloats_FlyingKickHangTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data_Characters.CharFloats_GunButtonHoldTimeToStartCharging.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data_Characters.CharFloats_GunChargeTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data_Characters.CharFloats_JumpArcUnk18.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data_Characters.CharFloats_JumpArcUnk19.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data_Characters.CharFloats_JumpEdgeSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data_Characters.CharFloats_JumpHeight.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data_Characters.CharFloats_RadialBlastChargeTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data_Characters.CharFloats_RadialBlastTimeToStart.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data_Characters.CharFloats_RadialBlastUnk39.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data_Characters.CharFloats_RadialBlastUnk40.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data_Characters.CharFloats_RunSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data_Characters.CharFloats_SlideJumpUnk25.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data_Characters.CharFloats_SlideJumpUnk26.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data_Characters.CharFloats_SlideJumpUnk27.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data_Characters.CharFloats_SlideSlowdownTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data_Characters.CharFloats_SlideSlowdownTime2.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data_Characters.CharFloats_SlideSlowdownTime3.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data_Characters.CharFloats_SlideSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data_Characters.CharFloats_SlideUnk49.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data_Characters.CharFloats_SlideUnk50.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data_Characters.CharFloats_SpinDelay.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data_Characters.CharFloats_SpinLength.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data_Characters.CharFloats_StrafingSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data_Characters.CharFloats_WalkSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data_Characters.CharFloats_WalkSpeedPercentage.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data_Characters.CharFloats_Static1.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data_Characters.CharFloats_Static15.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data_Characters.CharFloats_Static6.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data_Characters.CharFloats_Unk13.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data_Characters.CharFloats_Unk14.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data_Characters.CharFloats_Unk28.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data_Characters.CharFloats_Unk29.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data_Characters.CharFloats_Unk3.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data_Characters.CharFloats_Unk30.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data_Characters.CharFloats_Unk31.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data_Characters.CharFloats_Unk55.Value[(int)CharacterID.Crash];
                            instance.UnkI323[2] = Twins_Data_Characters.CharInts_SpawnHealth.Value[(int)CharacterID.Crash];

                            instance.UnkI321[(int)CharacterInstanceFlags.Unk1] = Twins_Data_Characters.CharFlags_Unk1.Value[(int)CharacterID.Crash];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk2] = Twins_Data_Characters.CharFlags_Unk2.Value[(int)CharacterID.Crash];
                            instance.UnkI321[(int)CharacterInstanceFlags.GroundRotationSpeed] = Twins_Data_Characters.CharFlags_GroundRotationSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk4] = Twins_Data_Characters.CharFlags_Unk4.Value[(int)CharacterID.Crash];
                            instance.UnkI321[(int)CharacterInstanceFlags.CrawlRotationSpeed] = Twins_Data_Characters.CharFlags_CrawlRotationSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk6] = Twins_Data_Characters.CharFlags_Unk6.Value[(int)CharacterID.Crash];
                            instance.UnkI321[(int)CharacterInstanceFlags.JumpRotationSpeed] = Twins_Data_Characters.CharFlags_JumpRotationSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk8] = Twins_Data_Characters.CharFlags_Unk8.Value[(int)CharacterID.Crash];
                            instance.UnkI321[(int)CharacterInstanceFlags.SlideJumpRotationSpeed] = Twins_Data_Characters.CharFlags_SlideJumpRotationSpeed.Value[(int)CharacterID.Crash];

                            //instance.UnkI322[(int)CharacterInstanceFloats.Static1] = 0; // 1

                        }
                        else if (instance.ObjectID == (uint)ObjectID.CORTEX)
                        {
                            // Cortex mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data_Characters.CharFloats_AirGravity.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data_Characters.CharFloats_BaseGravity.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data_Characters.CharFloats_BodyslamGravityForce.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data_Characters.CharFloats_BodyslamUpwardForce.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data_Characters.CharFloats_CrawlSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data_Characters.CharFloats_CrawlTimeFromStand.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data_Characters.CharFloats_CrawlTimeToRun.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data_Characters.CharFloats_CrawlTimeToStand.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data_Characters.CharFloats_DoubleJumpArcUnk.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data_Characters.CharFloats_DoubleJumpUnk22.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data_Characters.CharFloats_FlyingKickForwardSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data_Characters.CharFloats_FlyingKickGravity.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data_Characters.CharFloats_FlyingKickHangTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data_Characters.CharFloats_GunButtonHoldTimeToStartCharging.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data_Characters.CharFloats_GunChargeTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data_Characters.CharFloats_JumpArcUnk18.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data_Characters.CharFloats_JumpArcUnk19.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data_Characters.CharFloats_JumpEdgeSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data_Characters.CharFloats_JumpHeight.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data_Characters.CharFloats_RadialBlastChargeTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data_Characters.CharFloats_RadialBlastTimeToStart.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data_Characters.CharFloats_RadialBlastUnk39.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data_Characters.CharFloats_RadialBlastUnk40.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data_Characters.CharFloats_RunSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data_Characters.CharFloats_SlideJumpUnk25.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data_Characters.CharFloats_SlideJumpUnk26.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data_Characters.CharFloats_SlideJumpUnk27.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data_Characters.CharFloats_SlideSlowdownTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data_Characters.CharFloats_SlideSlowdownTime2.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data_Characters.CharFloats_SlideSlowdownTime3.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data_Characters.CharFloats_SlideSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data_Characters.CharFloats_SlideUnk49.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data_Characters.CharFloats_SlideUnk50.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data_Characters.CharFloats_SpinDelay.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data_Characters.CharFloats_SpinLength.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data_Characters.CharFloats_StrafingSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data_Characters.CharFloats_WalkSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data_Characters.CharFloats_WalkSpeedPercentage.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data_Characters.CharFloats_Static1.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data_Characters.CharFloats_Static15.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data_Characters.CharFloats_Static6.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data_Characters.CharFloats_Unk13.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data_Characters.CharFloats_Unk14.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data_Characters.CharFloats_Unk28.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data_Characters.CharFloats_Unk29.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data_Characters.CharFloats_Unk3.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data_Characters.CharFloats_Unk30.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data_Characters.CharFloats_Unk31.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data_Characters.CharFloats_Unk55.Value[(int)CharacterID.Cortex];
                            instance.UnkI323[2] = Twins_Data_Characters.CharInts_SpawnHealth.Value[(int)CharacterID.Cortex];

                            instance.UnkI321[(int)CharacterInstanceFlags.Unk1] = Twins_Data_Characters.CharFlags_Unk1.Value[(int)CharacterID.Cortex];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk2] = Twins_Data_Characters.CharFlags_Unk2.Value[(int)CharacterID.Cortex];
                            instance.UnkI321[(int)CharacterInstanceFlags.GroundRotationSpeed] = Twins_Data_Characters.CharFlags_GroundRotationSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk4] = Twins_Data_Characters.CharFlags_Unk4.Value[(int)CharacterID.Cortex];
                            instance.UnkI321[(int)CharacterInstanceFlags.CrawlRotationSpeed] = Twins_Data_Characters.CharFlags_CrawlRotationSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk6] = Twins_Data_Characters.CharFlags_Unk6.Value[(int)CharacterID.Cortex];
                            instance.UnkI321[(int)CharacterInstanceFlags.JumpRotationSpeed] = Twins_Data_Characters.CharFlags_JumpRotationSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk8] = Twins_Data_Characters.CharFlags_Unk8.Value[(int)CharacterID.Cortex];
                            instance.UnkI321[(int)CharacterInstanceFlags.SlideJumpRotationSpeed] = Twins_Data_Characters.CharFlags_SlideJumpRotationSpeed.Value[(int)CharacterID.Cortex];

                            //instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = 0.4f;

                        }
                        else if (instance.ObjectID == (uint)ObjectID.NINA)
                        {
                            // Nina mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data_Characters.CharFloats_AirGravity.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data_Characters.CharFloats_BaseGravity.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data_Characters.CharFloats_BodyslamGravityForce.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data_Characters.CharFloats_BodyslamUpwardForce.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data_Characters.CharFloats_CrawlSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data_Characters.CharFloats_CrawlTimeFromStand.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data_Characters.CharFloats_CrawlTimeToRun.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data_Characters.CharFloats_CrawlTimeToStand.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data_Characters.CharFloats_DoubleJumpArcUnk.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data_Characters.CharFloats_DoubleJumpUnk22.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data_Characters.CharFloats_FlyingKickForwardSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data_Characters.CharFloats_FlyingKickGravity.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data_Characters.CharFloats_FlyingKickHangTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data_Characters.CharFloats_GunButtonHoldTimeToStartCharging.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data_Characters.CharFloats_GunChargeTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data_Characters.CharFloats_JumpArcUnk18.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data_Characters.CharFloats_JumpArcUnk19.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data_Characters.CharFloats_JumpEdgeSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data_Characters.CharFloats_JumpHeight.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data_Characters.CharFloats_RadialBlastChargeTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data_Characters.CharFloats_RadialBlastTimeToStart.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data_Characters.CharFloats_RadialBlastUnk39.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data_Characters.CharFloats_RadialBlastUnk40.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data_Characters.CharFloats_RunSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data_Characters.CharFloats_SlideJumpUnk25.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data_Characters.CharFloats_SlideJumpUnk26.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data_Characters.CharFloats_SlideJumpUnk27.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data_Characters.CharFloats_SlideSlowdownTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data_Characters.CharFloats_SlideSlowdownTime2.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data_Characters.CharFloats_SlideSlowdownTime3.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data_Characters.CharFloats_SlideSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data_Characters.CharFloats_SlideUnk49.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data_Characters.CharFloats_SlideUnk50.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data_Characters.CharFloats_SpinDelay.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data_Characters.CharFloats_SpinLength.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data_Characters.CharFloats_StrafingSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data_Characters.CharFloats_WalkSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data_Characters.CharFloats_WalkSpeedPercentage.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data_Characters.CharFloats_Static1.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data_Characters.CharFloats_Static15.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data_Characters.CharFloats_Static6.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data_Characters.CharFloats_Unk13.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data_Characters.CharFloats_Unk14.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data_Characters.CharFloats_Unk28.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data_Characters.CharFloats_Unk29.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data_Characters.CharFloats_Unk3.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data_Characters.CharFloats_Unk30.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data_Characters.CharFloats_Unk31.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data_Characters.CharFloats_Unk55.Value[(int)CharacterID.Nina];
                            instance.UnkI323[2] = Twins_Data_Characters.CharInts_SpawnHealth.Value[(int)CharacterID.Nina];

                            instance.UnkI321[(int)CharacterInstanceFlags.Unk1] = Twins_Data_Characters.CharFlags_Unk1.Value[(int)CharacterID.Nina];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk2] = Twins_Data_Characters.CharFlags_Unk2.Value[(int)CharacterID.Nina];
                            instance.UnkI321[(int)CharacterInstanceFlags.GroundRotationSpeed] = Twins_Data_Characters.CharFlags_GroundRotationSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk4] = Twins_Data_Characters.CharFlags_Unk4.Value[(int)CharacterID.Nina];
                            instance.UnkI321[(int)CharacterInstanceFlags.CrawlRotationSpeed] = Twins_Data_Characters.CharFlags_CrawlRotationSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk6] = Twins_Data_Characters.CharFlags_Unk6.Value[(int)CharacterID.Nina];
                            instance.UnkI321[(int)CharacterInstanceFlags.JumpRotationSpeed] = Twins_Data_Characters.CharFlags_JumpRotationSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk8] = Twins_Data_Characters.CharFlags_Unk8.Value[(int)CharacterID.Nina];
                            instance.UnkI321[(int)CharacterInstanceFlags.SlideJumpRotationSpeed] = Twins_Data_Characters.CharFlags_SlideJumpRotationSpeed.Value[(int)CharacterID.Nina];

                            //instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = 1.75f;

                        }
                        else if (instance.ObjectID == (uint)ObjectID.MECHABANDICOOT)
                        {
                            // Mechabandicoot mods
                            // Needs fixing
                            /*
                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data_Characters.CharFloats_AirGravity.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data_Characters.CharFloats_BaseGravity.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data_Characters.CharFloats_BodyslamGravityForce.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data_Characters.CharFloats_BodyslamUpwardForce.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data_Characters.CharFloats_CrawlSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data_Characters.CharFloats_CrawlTimeFromStand.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data_Characters.CharFloats_CrawlTimeToRun.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data_Characters.CharFloats_CrawlTimeToStand.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data_Characters.CharFloats_DoubleJumpArcUnk.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data_Characters.CharFloats_DoubleJumpUnk22.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data_Characters.CharFloats_FlyingKickForwardSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data_Characters.CharFloats_FlyingKickGravity.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data_Characters.CharFloats_FlyingKickHangTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data_Characters.CharFloats_GunButtonHoldTimeToStartCharging.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data_Characters.CharFloats_GunChargeTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data_Characters.CharFloats_JumpArcUnk18.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data_Characters.CharFloats_JumpArcUnk19.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data_Characters.CharFloats_JumpEdgeSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data_Characters.CharFloats_JumpHeight.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data_Characters.CharFloats_RadialBlastChargeTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data_Characters.CharFloats_RadialBlastTimeToStart.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data_Characters.CharFloats_RadialBlastUnk39.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data_Characters.CharFloats_RadialBlastUnk40.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data_Characters.CharFloats_RunSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data_Characters.CharFloats_SlideJumpUnk25.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data_Characters.CharFloats_SlideJumpUnk26.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data_Characters.CharFloats_SlideJumpUnk27.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data_Characters.CharFloats_SlideSlowdownTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data_Characters.CharFloats_SlideSlowdownTime2.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data_Characters.CharFloats_SlideSlowdownTime3.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data_Characters.CharFloats_SlideSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data_Characters.CharFloats_SlideUnk49.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data_Characters.CharFloats_SlideUnk50.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data_Characters.CharFloats_SpinDelay.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data_Characters.CharFloats_SpinLength.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data_Characters.CharFloats_StrafingSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data_Characters.CharFloats_WalkSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data_Characters.CharFloats_WalkSpeedPercentage.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data_Characters.CharFloats_Static1.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data_Characters.CharFloats_Static15.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data_Characters.CharFloats_Static6.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data_Characters.CharFloats_Unk13.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data_Characters.CharFloats_Unk14.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data_Characters.CharFloats_Unk28.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data_Characters.CharFloats_Unk29.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data_Characters.CharFloats_Unk3.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data_Characters.CharFloats_Unk30.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data_Characters.CharFloats_Unk31.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data_Characters.CharFloats_Unk55.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI323[2] = Twins_Data_Characters.CharInts_SpawnHealth.Value[(int)CharacterID.Mechabandicoot];

                            instance.UnkI321[(int)CharacterInstanceFlags.Unk1] = Twins_Data_Characters.CharFlags_Unk1.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk2] = Twins_Data_Characters.CharFlags_Unk2.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI321[(int)CharacterInstanceFlags.GroundRotationSpeed] = Twins_Data_Characters.CharFlags_GroundRotationSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk4] = Twins_Data_Characters.CharFlags_Unk4.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI321[(int)CharacterInstanceFlags.CrawlRotationSpeed] = Twins_Data_Characters.CharFlags_CrawlRotationSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk6] = Twins_Data_Characters.CharFlags_Unk6.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI321[(int)CharacterInstanceFlags.JumpRotationSpeed] = Twins_Data_Characters.CharFlags_JumpRotationSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI321[(int)CharacterInstanceFlags.Unk8] = Twins_Data_Characters.CharFlags_Unk8.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI321[(int)CharacterInstanceFlags.SlideJumpRotationSpeed] = Twins_Data_Characters.CharFlags_SlideJumpRotationSpeed.Value[(int)CharacterID.Mechabandicoot];
                            */

                            //instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = 10;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }
    }
}
