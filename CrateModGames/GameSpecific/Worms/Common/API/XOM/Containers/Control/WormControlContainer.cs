using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("WormControlContainer")]
    public class WormControlContainer : Container
    {
        public float FallDamageDistance; //620f
        public float FallDamageFactor;//0.001f
        public float SlopeAngle; //45f
        public float WalkSpeed;  //1.55f
        public float MaxAcceleration;  //0.001f
        public float MaxDeceleration;  //-0.2f
        public float MaxTurn;   //10000f
        public float AimDirectionSpeed;  //1f
        public float AimTimeToMaxSpeed;  //0.5f
        public float AimWhenStoppingMultiply;  //0.9f
        public float StationaryTurnSpeed;  //1f
        public float StationaryTurnTimeToMaxSpeed;  //0.5f
        public float StationaryWhenStoppingMultiply;   //0.9f
        public float HeightOffset;  //8f
        public float MaxMoveDown;  //0.175f
        public float MaxMoveUp;   //0.175f
        public float FirstPersonTurnSpeedHoriz;  //1f
        public float FirstPersonTurnSpeedVert;   //1f
        public float MaxJetpackThrustUp;  //3f
        public float MaxJetpackThrustForward;  //5f
        public float GravityEffectOnJetpack;  //60f
        public float MaxHeightUsingJetpack;  //5000f
        public float InertiaOnJetpack;   //0.08f
        public float MaxJetpackTurn;   //1f
        public float WindEffectOnParachute;    //10f
        public float AGravityEffectOnParachute;   //6f
        public float MaxParachuteThrustForward;   //2f
        public float InertiaOnParachute;   //0.08f
        public float MaxParachuteTurn;   //0.9f
        public float MaxGirderDistance;  //320f
        public float MaxGirderMoveUpDown;   //3f
        public float MaxGirderMove;  //10f
        public float MaxGirderRotate;  //0.05f
        public float GirderMinScale;  //3f
        public float GirderMedScale;  //5f
        public float GirderMaxScale;   //7f
        public float JumpDelay;    //0.3f
        public float JumpVerticalHeight;     //250f
        public float JumpVerticalMinLaunchSpeed;      //0f
        public float JumpVerticalMaxLaunchSpeed;      //0.5f
        public float JumpBackflipHeight;      //300f
        public float JumpBackflipSecondHeight;      //200f
        public float JumpBackflipMinLaunchSpeed;      //-1f
        public float JumpBackflipMaxLaunchSpeed;      //-1.5f
        public float JumpForwardflipHeight;      //300f
        public float JumpForwardflipSecondHeight;      //200f
        public float JumpForwardflipMinLaunchSpeed;      //1f
        public float JumpForwardflipMaxLaunchSpeed;      //1.5f
        public float JumpForwardMinLaunchSpeed;      //1.5f
        public float JumpForwardMaxLaunchSpeed;      //2f
        public float JumpForwardHeight;      //100f
        public float JumpForwardMaxForwardsSpeed;      //0.9f
        public float JumpForwardMaxSidewaysSpeed;      //0.6f
        public float JumpForwardMaxAcceleration;      //0.04f
        public float JumpForwardMaxDeceleration;      //0.02f
        public float AutoHopForward;      //0.2f
        public float AutoHopHeight;      //0.32f
        public float WindFactor;      //0f
        public float CoefficientOfRestitution;      //0.1f
        public float AirResistance;      //0f
        public float ImpactFriction;      //0.025f
        public float MinWalkTurnAnimAngleRadians;      //0.5f
        public float WalkAnimSpeed;      //1.25f
        public float CreepAnimSpeed;      //1f
        public Vector3 FirstPersonOriginOffset = new Vector3();      //0f 35f 0f
        public Vector3 ThirdPersonOriginOffset = new Vector3();      //0f 24f 0f

        public override void Read(BinaryReader reader)
        {
            FallDamageDistance = reader.ReadSingle();
            FallDamageFactor = reader.ReadSingle();
            SlopeAngle = reader.ReadSingle();
            WalkSpeed = reader.ReadSingle();
            MaxAcceleration = reader.ReadSingle();
            MaxDeceleration = reader.ReadSingle();
            MaxTurn = reader.ReadSingle();
            AimDirectionSpeed = reader.ReadSingle();
            AimTimeToMaxSpeed = reader.ReadSingle();
            AimWhenStoppingMultiply = reader.ReadSingle();
            StationaryTurnSpeed = reader.ReadSingle();
            StationaryTurnTimeToMaxSpeed = reader.ReadSingle();
            StationaryWhenStoppingMultiply = reader.ReadSingle();
            HeightOffset = reader.ReadSingle();
            MaxMoveDown = reader.ReadSingle();
            MaxMoveUp = reader.ReadSingle();
            FirstPersonTurnSpeedHoriz = reader.ReadSingle();
            FirstPersonTurnSpeedVert = reader.ReadSingle();
            MaxJetpackThrustUp = reader.ReadSingle();
            MaxJetpackThrustForward = reader.ReadSingle();
            GravityEffectOnJetpack = reader.ReadSingle();
            MaxHeightUsingJetpack = reader.ReadSingle();
            InertiaOnJetpack = reader.ReadSingle();
            MaxJetpackTurn = reader.ReadSingle();
            WindEffectOnParachute = reader.ReadSingle();
            AGravityEffectOnParachute = reader.ReadSingle();
            MaxParachuteThrustForward = reader.ReadSingle();
            InertiaOnParachute = reader.ReadSingle();
            MaxParachuteTurn = reader.ReadSingle();
            MaxGirderDistance = reader.ReadSingle();
            MaxGirderMoveUpDown = reader.ReadSingle();
            MaxGirderMove = reader.ReadSingle();
            MaxGirderRotate = reader.ReadSingle();
            GirderMinScale = reader.ReadSingle();
            GirderMedScale = reader.ReadSingle();
            GirderMaxScale = reader.ReadSingle();
            JumpDelay = reader.ReadSingle();
            JumpVerticalHeight = reader.ReadSingle();
            JumpVerticalMinLaunchSpeed = reader.ReadSingle();
            JumpVerticalMaxLaunchSpeed = reader.ReadSingle();
            JumpBackflipHeight = reader.ReadSingle();
            JumpBackflipSecondHeight = reader.ReadSingle();
            JumpBackflipMinLaunchSpeed = reader.ReadSingle();
            JumpBackflipMaxLaunchSpeed = reader.ReadSingle();
            JumpForwardflipHeight = reader.ReadSingle();
            JumpForwardflipSecondHeight = reader.ReadSingle();
            JumpForwardflipMinLaunchSpeed = reader.ReadSingle();
            JumpForwardflipMaxLaunchSpeed = reader.ReadSingle();
            JumpForwardMinLaunchSpeed = reader.ReadSingle();
            JumpForwardMaxLaunchSpeed = reader.ReadSingle();
            JumpForwardHeight = reader.ReadSingle();
            JumpForwardMaxForwardsSpeed = reader.ReadSingle();
            JumpForwardMaxSidewaysSpeed = reader.ReadSingle();
            JumpForwardMaxAcceleration = reader.ReadSingle();
            JumpForwardMaxDeceleration = reader.ReadSingle();
            AutoHopForward = reader.ReadSingle();
            AutoHopHeight = reader.ReadSingle();
            WindFactor = reader.ReadSingle();
            CoefficientOfRestitution = reader.ReadSingle();
            AirResistance = reader.ReadSingle();
            ImpactFriction = reader.ReadSingle();
            MinWalkTurnAnimAngleRadians = reader.ReadSingle();
            WalkAnimSpeed = reader.ReadSingle();
            CreepAnimSpeed = reader.ReadSingle();
            FirstPersonOriginOffset.Read(reader);
            ThirdPersonOriginOffset.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(FallDamageDistance);
            writer.Write(FallDamageFactor);
            writer.Write(SlopeAngle);
            writer.Write(WalkSpeed);
            writer.Write(MaxAcceleration);
            writer.Write(MaxDeceleration);
            writer.Write(MaxTurn);
            writer.Write(AimDirectionSpeed);
            writer.Write(AimTimeToMaxSpeed);
            writer.Write(AimWhenStoppingMultiply);
            writer.Write(StationaryTurnSpeed);
            writer.Write(StationaryTurnTimeToMaxSpeed);
            writer.Write(StationaryWhenStoppingMultiply);
            writer.Write(HeightOffset);
            writer.Write(MaxMoveDown);
            writer.Write(MaxMoveUp);
            writer.Write(FirstPersonTurnSpeedHoriz);
            writer.Write(FirstPersonTurnSpeedVert);
            writer.Write(MaxJetpackThrustUp);
            writer.Write(MaxJetpackThrustForward);
            writer.Write(GravityEffectOnJetpack);
            writer.Write(MaxHeightUsingJetpack);
            writer.Write(InertiaOnJetpack);
            writer.Write(MaxJetpackTurn);
            writer.Write(WindEffectOnParachute);
            writer.Write(AGravityEffectOnParachute);
            writer.Write(MaxParachuteThrustForward);
            writer.Write(InertiaOnParachute);
            writer.Write(MaxParachuteTurn);
            writer.Write(MaxGirderDistance);
            writer.Write(MaxGirderMoveUpDown);
            writer.Write(MaxGirderMove);
            writer.Write(MaxGirderRotate);
            writer.Write(GirderMinScale);
            writer.Write(GirderMedScale);
            writer.Write(GirderMaxScale);
            writer.Write(JumpDelay);
            writer.Write(JumpVerticalHeight);
            writer.Write(JumpVerticalMinLaunchSpeed);
            writer.Write(JumpVerticalMaxLaunchSpeed);
            writer.Write(JumpBackflipHeight);
            writer.Write(JumpBackflipSecondHeight);
            writer.Write(JumpBackflipMinLaunchSpeed);
            writer.Write(JumpBackflipMaxLaunchSpeed);
            writer.Write(JumpForwardflipHeight);
            writer.Write(JumpForwardflipSecondHeight);
            writer.Write(JumpForwardflipMinLaunchSpeed);
            writer.Write(JumpForwardflipMaxLaunchSpeed);
            writer.Write(JumpForwardMinLaunchSpeed);
            writer.Write(JumpForwardMaxLaunchSpeed);
            writer.Write(JumpForwardHeight);
            writer.Write(JumpForwardMaxForwardsSpeed);
            writer.Write(JumpForwardMaxSidewaysSpeed);
            writer.Write(JumpForwardMaxAcceleration);
            writer.Write(JumpForwardMaxDeceleration);
            writer.Write(AutoHopForward);
            writer.Write(AutoHopHeight);
            writer.Write(WindFactor);
            writer.Write(CoefficientOfRestitution);
            writer.Write(AirResistance);
            writer.Write(ImpactFriction);
            writer.Write(MinWalkTurnAnimAngleRadians);
            writer.Write(WalkAnimSpeed);
            writer.Write(CreepAnimSpeed);
            FirstPersonOriginOffset.Write(writer);
            ThirdPersonOriginOffset.Write(writer);
        }
    }
}
