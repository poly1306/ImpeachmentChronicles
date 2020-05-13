using GTA;
using GTA.Math;
using GTA.NaturalMotion;
using System;
using System.Collections.Generic;
using System.Linq;

// Mimic electrocution with the stun gun by sending NM messages to the player ped.
// You can find similar config in physicstasks.ymt (search for "CTaskNMShot").
public class FakeElectrocutionShotDemo : Script
{
	public FakeElectrocutionShotDemo()
	{
		KeyDown += FakeElectrocutionShotDemo_KeyDown;
	}

	// Shot_Base (priority: 0)
	static private void StartShotBaseNmMessages(Ped ped)
	{
		var balancerCollisionsReactionNM = ped.Euphoria.BalancerCollisionsReaction;
		balancerCollisionsReactionNM.Start();

		var fallOverWallNM = ped.Euphoria.FallOverWall;
		fallOverWallNM.MoveLegs = true;
		fallOverWallNM.MoveArms = false;
		fallOverWallNM.BendSpine = true;
		fallOverWallNM.RollingPotential = 0.3f;
		fallOverWallNM.RollingBackThr = 0.5f;
		fallOverWallNM.ForceTimeOut = 2f;
		fallOverWallNM.MagOfForce = 0.5f;
		fallOverWallNM.BodyTwist = 0.54f;
		fallOverWallNM.Start();

		var shotNM = ped.Euphoria.Shot;
		shotNM.Start();

		var ShotConfigureArmsNM = ped.Euphoria.ShotConfigureArms;
		ShotConfigureArmsNM.PointGun = true;
		ShotConfigureArmsNM.Start();

		// You can send NM message to a ped without setting "start" parameter to true so this message can update parameters of the running behavior
		var ConfigureBalanceNM = ped.Euphoria.ConfigureBalance;
		ConfigureBalanceNM.FallMult = 40f;
		ConfigureBalanceNM.Update();
	}

	// normal in WeaponSets (priority: 5)
	// NmShotTuningSet is set to normal in WEAPON_STUNGUN config in weapons.meta.
	static private void StartNormalWeapomNmMessages(Ped ped)
	{
		var ConfigureBalanceNM = ped.Euphoria.ConfigureBalance;
		ConfigureBalanceNM.StableLinSpeedThresh = 0.7f;
		ConfigureBalanceNM.StableRotSpeedThresh = 0.85f;
		ConfigureBalanceNM.UseComDirTurnVelThresh = 0.6f;
		ConfigureBalanceNM.StepIfInSupport = true;
		ConfigureBalanceNM.BackwardsLeanCutoff = 0.3f;
		ConfigureBalanceNM.ExtraSteps = -1;
		ConfigureBalanceNM.ExtraTime = -1.0f;
		ConfigureBalanceNM.MaxBalanceTime = 2.0f;
		ConfigureBalanceNM.GiveUpHeight = 0.62f;
		ConfigureBalanceNM.LegStiffness = 12.0f;
		ConfigureBalanceNM.MaxSteps = 3;
		ConfigureBalanceNM.LegsTogetherRestep = 1.0f;
		ConfigureBalanceNM.LegsApartRestep = 0.2f;
		ConfigureBalanceNM.ExtraFeetApart = 0f;
		ConfigureBalanceNM.StepDecisionThreshold = 0f;
		ConfigureBalanceNM.DontStepTime = 0.2f;
		ConfigureBalanceNM.Start();

		var shotNM = ped.Euphoria.Shot;
		shotNM.InitialNeckStiffness = 5.0f;
		shotNM.InitialNeckDamping = 2.0f;
		shotNM.Looseness4Fall = 0.7f;
		shotNM.Looseness4Stagger = 0.4f;
		shotNM.MinArmsLooseness = 0.1f;
		shotNM.AlwaysResetNeckLooseness = true;
		shotNM.AngVelScale = 0f;
		shotNM.TimeBeforeReachForWound = 0.5f;
		shotNM.CpainSmooth2Time = 0f;
		shotNM.CpainMag = 0f;
		shotNM.CpainTwistMag = 0f;
		shotNM.CpainSmooth2Zero = 0f;
		shotNM.FallingReaction = 3;
		shotNM.InitialWeaknessZeroDuration = 0.1f;
		shotNM.InitialWeaknessRampDuration = 0.2f;
		shotNM.UseCStrModulation = false;
		shotNM.CStrUpperMin = 1.0f;
		shotNM.CStrLowerMin = 0.8f;
		shotNM.BodyStiffness = 10f;
		shotNM.SpineBlendExagCPain = true;
		shotNM.AlwaysResetLooseness = true;
		shotNM.Fling = true;
		shotNM.FlingWidth = 0.5f;
		shotNM.FlingTime = 0.05f;
		shotNM.Start();

		var shotSnapNM = ped.Euphoria.ShotSnap;
		shotSnapNM.Snap = true;
		shotSnapNM.SnapMag = 1f;
		shotSnapNM.SnapDirectionRandomness = 0f;
		shotSnapNM.SnapLeftLeg = false;
		shotSnapNM.SnapRightLeg = false;
		shotSnapNM.SnapNeck = true;
		shotSnapNM.UnSnapInterval = 0.05f;
		shotSnapNM.SnapMovingMult = 2f;
		shotSnapNM.SnapBalancingMult = 1f;
		shotSnapNM.SnapAirborneMult = 1.0f;
		shotSnapNM.SnapMovingThresh = 1.0f;
		shotSnapNM.SnapLeftArm = false;
		shotSnapNM.SnapRightArm = false;
		shotSnapNM.SnapSpine = true;
		shotSnapNM.SnapPhasedLegs = true;
		shotSnapNM.SnapHipType = 2;
		shotSnapNM.SnapUseBulletDir = true;
		shotSnapNM.SnapHitPart = false;
		shotSnapNM.UnSnapRatio = 0.3f;
		shotSnapNM.SnapUseTorques = true;
		shotSnapNM.Start();

		var shotShockSpinNM = ped.Euphoria.ShotShockSpin;
		shotShockSpinNM.AddShockSpin = false;
		shotShockSpinNM.AlwaysAddShockSpin = false;
		shotShockSpinNM.ShockSpinMin = 100f;
		shotShockSpinNM.ShockSpinMax = 100f;
		shotShockSpinNM.BracedSideSpinMult = 2f;
		shotShockSpinNM.Start();

		var configureBulletsNM = ped.Euphoria.ConfigureBullets;
		configureBulletsNM.LoosenessFix = true;
		configureBulletsNM.ImpulseReductionPerShot = 0.1f;
		configureBulletsNM.ImpulseRecovery = 0f;
		configureBulletsNM.TorqueAlwaysSpine3 = true;
		configureBulletsNM.RbRatio = 0f;
		configureBulletsNM.RbMaxTwistMomentArm = 1f;
		configureBulletsNM.RbMaxBroomMomentArm = 0f;
		configureBulletsNM.RbRatioAirborne = 0f;
		configureBulletsNM.RbMaxTwistMomentArmAirborne = 1f;
		configureBulletsNM.RbMaxBroomMomentArmAirborne = 0f;
		configureBulletsNM.RbRatioOneLeg = 0f;
		configureBulletsNM.RbMaxTwistMomentArmOneLeg = 1f;
		configureBulletsNM.RbMaxBroomMomentArmOneLeg = 0f;
		configureBulletsNM.RbPivot = false;
		configureBulletsNM.ImpulseAirOn = true;
		configureBulletsNM.ImpulseAirMax = 150f;
		configureBulletsNM.ImpulseAirMult = 1.0f;
		configureBulletsNM.Start();

		var configureShotInjuredLegNM = ped.Euphoria.ConfigureShotInjuredLeg;
		configureShotInjuredLegNM.TimeBeforeCollapseWoundLeg = 0f;
		configureShotInjuredLegNM.LegInjuryTime = 1.4f;
		configureShotInjuredLegNM.LegLimpBend = 0.2f;
		configureShotInjuredLegNM.LegInjury = 0f;
		configureShotInjuredLegNM.LegInjuryHipPitch = -0.2f;
		configure