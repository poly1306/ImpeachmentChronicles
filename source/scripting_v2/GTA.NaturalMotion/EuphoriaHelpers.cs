
//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using GTA;
using GTA.Math;

namespace GTA.NaturalMotion
{
	public enum ArmDirections
	{
		Backwards = -1,
		Adaptive,
		Forwards
	}

	public enum AnimSource
	{
		CurrentItems,
		PreviousItems,
		AnimItems
	}

	public enum FallType
	{
		RampDownStiffness,
		DontChangeStep,
		ForceBalance,
		Slump
	}

	public enum Synchroisation
	{
		NotSynced,
		AlwaysSynced,
		SyncedAtStart
	}

	public enum TurnType
	{
		DontTurn,
		ToTarget,
		AwayFromTarget
	}

	public enum TorqueMode
	{
		Disabled,
		Proportional,
		Additive
	}

	public enum TorqueSpinMode
	{
		FromImpulse,
		Random,
		Flipping
	}

	public enum TorqueFilterMode
	{
		ApplyEveryBullet,
		ApplyIfLastFinished,
		ApplyIfSpinDifferent
	}

	public enum RbTwistAxis
	{
		WorldUp,
		CharacterComUp
	}

	public enum WeaponMode
	{
		None = -1,
		Pistol,
		Dual,
		Rifle,
		SideArm,
		PistolLeft,
		PistolRight
	}

	public enum Hand
	{
		Left,
		Right
	}

	public enum MirrorMode
	{
		Independant,
		Mirrored,
		Parallel
	}

	public enum AdaptiveMode
	{
		NotAdaptive,
		OnlyDirection,
		DirectionAndSpeed,
		DirectionSpeedAndStrength
	}

	public sealed class ActivePoseHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ActivePoseHelper for sending a ActivePose <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ActivePose <see cref="Message"/> to.</param>
		public ActivePoseHelper(Ped ped) : base(ped, "activePose")
		{
		}

		/// <summary>
		/// Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see notes for explanation).
		/// </summary>
		/// <remarks>
		/// Default value = "fb".
		/// </remarks>
		public string Mask
		{
			set => SetArgument("mask", value);
		}

		/// <summary>
		/// Apply gravity compensation.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool UseGravityCompensation
		{
			set => SetArgument("useGravityCompensation", value);
		}

		/// <summary>
		/// Animation source.
		/// </summary>
		public AnimSource AnimSource
		{
			set => SetArgument("animSource", (int)value);
		}
	}

	public sealed class ApplyImpulseHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ApplyImpulseHelper for sending a ApplyImpulse <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ApplyImpulse <see cref="Message"/> to.</param>
		public ApplyImpulseHelper(Ped ped) : base(ped, "applyImpulse")
		{
		}

		/// <summary>
		/// 0 means straight impulse, 1 means multiply by the mass (change in velocity).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float EqualizeAmount
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("equalizeAmount", value);
			}
		}

		/// <summary>
		/// Index of part being hit. -1 apply impulse to COM.
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = -1.
		/// Max value = 28.
		/// </remarks>
		public int PartIndex
		{
			set
			{
				if (value > 28)
					value = 28;
				if (value < -1)
					value = -1;
				SetArgument("partIndex", value);
			}
		}

		/// <summary>
		/// Impulse vector (impulse is change in momentum).
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// Min value = -4500.0f.
		/// Max value = 4500.0f.
		/// </remarks>
		public Vector3 Impulse
		{
			set => SetArgument("impulse", Vector3.Clamp(value, new Vector3(-4500.0f, -4500.0f, -4500.0f), new Vector3(4500.0f, 4500.0f, 4500.0f)));
		}

		/// <summary>
		/// Optional point on part where hit. If not supplied then the impulse is applied at the part center.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 HitPoint
		{
			set => SetArgument("hitPoint", value);
		}

		/// <summary>
		/// Hit point in local coordinates of body part.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LocalHitPointInfo
		{
			set => SetArgument("localHitPointInfo", value);
		}

		/// <summary>
		/// Impulse in local coordinates of body part.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LocalImpulseInfo
		{
			set => SetArgument("localImpulseInfo", value);
		}

		/// <summary>
		/// Impulse should be considered an angular impulse.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool AngularImpulse
		{
			set => SetArgument("angularImpulse", value);
		}
	}

	public sealed class ApplyBulletImpulseHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ApplyBulletImpulseHelper for sending a ApplyBulletImpulse <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ApplyBulletImpulse <see cref="Message"/> to.</param>
		public ApplyBulletImpulseHelper(Ped ped) : base(ped, "applyBulletImpulse")
		{
		}

		/// <summary>
		/// 0 means straight impulse, 1 means multiply by the mass (change in velocity).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float EqualizeAmount
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("equalizeAmount", value);
			}
		}

		/// <summary>
		/// Index of part being hit.
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// Max value = 28.
		/// </remarks>
		public int PartIndex
		{
			set
			{
				if (value > 28)
					value = 28;
				if (value < 0)
					value = 0;
				SetArgument("partIndex", value);
			}
		}

		/// <summary>
		/// Impulse vector (impulse is change in momentum).
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// Min value = -1000.0f.
		/// Max value = 1000.0f.
		/// </remarks>
		public Vector3 Impulse
		{
			set => SetArgument("impulse", Vector3.Clamp(value, new Vector3(-1000.0f, -1000.0f, -1000.0f), new Vector3(1000.0f, 1000.0f, 1000.0f)));
		}

		/// <summary>
		/// Optional point on part where hit.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 HitPoint
		{
			set => SetArgument("hitPoint", value);
		}

		/// <summary>
		/// True = hitPoint is in local coordinates of bodyPart, false = hit point is in world coordinates.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LocalHitPointInfo
		{
			set => SetArgument("localHitPointInfo", value);
		}

		/// <summary>
		/// If not 0.0 then have an extra bullet applied to spine0 (approximates the COM).  Uses setup from configureBulletsExtra.  0-1 shared 0.0 = no extra bullet, 0.5 = impulse split equally between extra and bullet,  1.0 only extra bullet.  LT 0.0 then bullet + scaled extra bullet.  Eg.-0.5 = bullet + 0.5 impulse extra bullet.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = -2.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ExtraShare
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -2.0f)
					value = -2.0f;
				SetArgument("extraShare", value);
			}
		}
	}

	/// <summary>
	/// Set the amount of relaxation across the whole body; Used to collapse the character into a rag-doll-like state.
	/// </summary>
	public sealed class BodyRelaxHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the BodyRelaxHelper for sending a BodyRelax <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the BodyRelax <see cref="Message"/> to.</param>
		/// <remarks>
		/// Set the amount of relaxation across the whole body; Used to collapse the character into a rag-doll-like state.
		/// </remarks>
		public BodyRelaxHelper(Ped ped) : base(ped, "bodyRelax")
		{
		}

		/// <summary>
		/// How relaxed the body becomes, in percentage relaxed. 100 being totally rag-dolled, 0 being very stiff and rigid.
		/// </summary>
		/// <remarks>
		/// Default value = 50.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float Relaxation
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("relaxation", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float Damping
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("damping", value);
			}
		}

		/// <summary>
		/// Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
		/// </summary>
		/// <remarks>
		/// Default value = "fb.
		/// </remarks>
		public string Mask
		{
			set => SetArgument("mask", value);
		}

		/// <summary>
		/// Automatically hold the current pose as the character relaxes - can be used to avoid relaxing into a t-pose.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool HoldPose
		{
			set => SetArgument("holdPose", value);
		}

		/// <summary>
		/// Sets the drive state to free - this reduces drifting on the ground.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool DisableJointDriving
		{
			set => SetArgument("disableJointDriving", value);
		}
	}

	/// <summary>
	/// This single message allows you to configure various parameters used on any behavior that uses the dynamic balance.
	/// </summary>
	public sealed class ConfigureBalanceHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureBalanceHelper for sending a ConfigureBalance <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureBalance <see cref="Message"/> to.</param>
		/// <remarks>
		/// This single message allows you to configure various parameters used on any behavior that uses the dynamic balance.
		/// </remarks>
		public ConfigureBalanceHelper(Ped ped) : base(ped, "configureBalance")
		{
		}

		/// <summary>
		/// Maximum height that character steps vertically (above 0.2 is high ... But OK underwater).
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = 0.0f.
		/// Max value = 0.4f.
		/// </remarks>
		public float StepHeight
		{
			set
			{
				if (value > 0.4f)
					value = 0.4f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("stepHeight", value);
			}
		}

		/// <summary>
		/// Added to stepHeight if going up steps.
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = 0.0f.
		/// Max value = 0.4f.
		/// </remarks>
		public float StepHeightInc4Step
		{
			set
			{
				if (value > 0.4f)
					value = 0.4f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("stepHeightInc4Step", value);
			}
		}

		/// <summary>
		/// If the legs end up more than (legsApartRestep + hipwidth) apart even though balanced, take another step.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LegsApartRestep
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("legsApartRestep", value);
			}
		}

		/// <summary>
		/// Mmmm0.1 for drunk if the legs end up less than (hipwidth - legsTogetherRestep) apart even though balanced, take another step.  A value of 1 will turn off this feature and the max value is hipWidth = 0.23f by default but is model dependent.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LegsTogetherRestep
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("legsTogetherRestep", value);
			}
		}

		/// <summary>
		/// FRICTION WORKAROUND: if the legs end up more than (legsApartMax + hipwidth) apart when balanced, adjust the feet positions to slide back so they are legsApartMax + hipwidth apart.  Needs to be less than legsApartRestep to see any effect.
		/// </summary>
		/// <remarks>
		/// Default value = 2.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float LegsApartMax
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("legsApartMax", value);
			}
		}

		/// <summary>
		/// Does the knee strength reduce with angle.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool TaperKneeStrength
		{
			set => SetArgument("taperKneeStrength", value);
		}

		/// <summary>
		/// Stiffness of legs.
		/// </summary>
		/// <remarks>
		/// Default value = 12.0f.
		/// Min value = 6.0f.
		/// Max value = 16.0f.
		/// </remarks>
		public float LegStiffness
		{
			set
			{
				if (value > 16.0f)
					value = 16.0f;
				if (value < 6.0f)
					value = 6.0f;
				SetArgument("legStiffness", value);
			}
		}

		/// <summary>
		/// Damping of left leg during swing phase (mmmmDrunk used 1.25 to slow legs movement).
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.2f.
		/// Max value = 4.0f.
		/// </remarks>
		public float LeftLegSwingDamping
		{
			set
			{
				if (value > 4.0f)
					value = 4.0f;
				if (value < 0.2f)
					value = 0.2f;
				SetArgument("leftLegSwingDamping", value);
			}
		}

		/// <summary>
		/// Damping of right leg during swing phase (mmmmDrunk used 1.25 to slow legs movement).
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.2f.
		/// Max value = 4.0f.
		/// </remarks>
		public float RightLegSwingDamping
		{
			set
			{
				if (value > 4.0f)
					value = 4.0f;
				if (value < 0.2f)
					value = 0.2f;
				SetArgument("rightLegSwingDamping", value);
			}
		}

		/// <summary>
		/// Gravity opposition applied to hips and knees.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 4.0f.
		/// </remarks>
		public float OpposeGravityLegs
		{
			set
			{
				if (value > 4.0f)
					value = 4.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("opposeGravityLegs", value);
			}
		}

		/// <summary>
		/// Gravity opposition applied to ankles.  General balancer likes 1.0.  StaggerFall likes 0.1.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 4.0f.
		/// </remarks>
		public float OpposeGravityAnkles
		{
			set
			{
				if (value > 4.0f)
					value = 4.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("opposeGravityAnkles", value);
			}
		}

		/// <summary>
		/// Multiplier on the floorAcceleration added to the lean.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAcc
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("leanAcc", value);
			}
		}

		/// <summary>
		/// Multiplier on the floorAcceleration added to the leanHips.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float HipLeanAcc
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("hipLeanAcc", value);
			}
		}

		/// <summary>
		/// Max floorAcceleration allowed for lean and leanHips.
		/// </summary>
		/// <remarks>
		/// Default value = 5.0f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float LeanAccMax
		{
			set
			{
				if (value > 10.0f)
					value = 10.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("leanAccMax", value);
			}
		}

		/// <summary>
		/// Level of cheat force added to character to resist the effect of floorAcceleration (anti-Acceleration) - added to upperbody.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float ResistAcc
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("resistAcc", value);
			}
		}

		/// <summary>
		/// Max floorAcceleration allowed for anti-Acceleration. If  GT 20.0 then it is probably in a crash.
		/// </summary>
		/// <remarks>
		/// Default value = 3.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float ResistAccMax
		{
			set
			{
				if (value > 20.0f)
					value = 20.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("resistAccMax", value);
			}
		}

		/// <summary>
		/// This parameter will be removed when footSlipCompensation preserves the foot angle on a moving floor]. If the character detects a moving floor and footSlipCompOnMovingFloor is false then it will turn off footSlipCompensation - at footSlipCompensation preserves the global heading of the feet.  If footSlipCompensation is off then the character usually turns to the side in the end although when turning the vehicle turns it looks promising for a while.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool FootSlipCompOnMovingFloor
		{
			set => SetArgument("footSlipCompOnMovingFloor", value);
		}

		/// <summary>
		/// Ankle equilibrium angle used when static balancing.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float AnkleEquilibrium
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("ankleEquilibrium", value);
			}
		}

		/// <summary>
		/// Additional feet apart setting.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ExtraFeetApart
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("extraFeetApart", value);
			}
		}

		/// <summary>
		/// Amount of time at the start of a balance before the character is allowed to start stepping.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float DontStepTime
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("dontStepTime", value);
			}
		}

		/// <summary>
		/// When the character gives up and goes into a fall.  Larger values mean that the balancer can lean more before failing.
		/// </summary>
		/// <remarks>
		/// Default value = 0.6f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float BalanceAbortThreshold
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("balanceAbortThreshold", value);
			}
		}

		/// <summary>
		/// Height between lowest foot and COM below which balancer will give up.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.5f.
		/// </remarks>
		public float GiveUpHeight
		{
			set
			{
				if (value > 1.5f)
					value = 1.5f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("giveUpHeight", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float StepClampScale
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("stepClampScale", value);
			}
		}

		/// <summary>
		/// Variance in clamp scale every step. If negative only takes away from clampScale.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float StepClampScaleVariance
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("stepClampScaleVariance", value);
			}
		}

		/// <summary>
		/// Amount of time (seconds) into the future that the character tries to move hip to (kind of).  Will be controlled by balancer in future but can help recover spine quicker from bending forwards to much.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float PredictionTimeHip
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("predictionTimeHip", value);
			}
		}

		/// <summary>
		/// Amount of time (seconds) into the future that the character tries to step to. Bigger values try to recover with fewer, bigger steps. Smaller values recover with smaller steps, and generally recover less.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float PredictionTime
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("predictionTime", value);
			}
		}

		/// <summary>
		/// Variance in predictionTime every step. If negative only takes away from predictionTime.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float PredictionTimeVariance
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("predictionTimeVariance", value);
			}
		}

		/// <summary>
		/// Maximum number of steps that the balancer will take.
		/// </summary>
		/// <remarks>
		/// Default value = 100.
		/// Min value = 1.
		/// </remarks>
		public int MaxSteps
		{
			set
			{
				if (value < 1)
					value = 1;
				SetArgument("maxSteps", value);
			}
		}

		/// <summary>
		/// Maximum time(seconds) that the balancer will balance for.
		/// </summary>
		/// <remarks>
		/// Default value = 50.0f.
		/// Min value = 1.0f.
		/// </remarks>
		public float MaxBalanceTime
		{
			set
			{
				if (value < 1.0f)
					value = 1.0f;
				SetArgument("maxBalanceTime", value);
			}
		}

		/// <summary>
		/// Allow the balancer to take this many more steps before hitting maxSteps. If negative nothing happens(safe default).
		/// </summary>
		/// <remarks>
		/// Default value = -1.
		/// Min value = -1.
		/// </remarks>
		public int ExtraSteps
		{
			set
			{
				if (value < -1)
					value = -1;
				SetArgument("extraSteps", value);
			}
		}

		/// <summary>
		/// Allow the balancer to balance for this many more seconds before hitting maxBalanceTime.  If negative nothing happens(safe default).
		/// </summary>
		/// <remarks>
		/// Default value = -1.0f.
		/// Min value = -1.0f.
		/// </remarks>
		public float ExtraTime
		{
			set
			{
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("extraTime", value);
			}
		}

		/// <summary>
		/// How to fall after maxSteps or maxBalanceTime.
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="FallType.RampDownStiffness"/>.
		/// If <see cref="FallType.Slump"/> BCR has to be active.
		/// </remarks>
		public FallType FallType
		{
			set => SetArgument("fallType", (int)value);
		}

		/// <summary>
		/// Multiply the rampDown of stiffness on falling by this amount ( GT 1 fall quicker).
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float FallMult
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("fallMult", value);
			}
		}

		/// <summary>
		/// Reduce gravity compensation as the legs weaken on falling.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool FallReduceGravityComp
		{
			set => SetArgument("fallReduceGravityComp", value);
		}

		/// <summary>
		/// Bend over when falling after maxBalanceTime.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool RampHipPitchOnFail
		{
			set => SetArgument("rampHipPitchOnFail", value);
		}

		/// <summary>
		/// Linear speed threshold for successful balance.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float StableLinSpeedThresh
		{
			set
			{
				if (value > 10.0f)
					value = 10.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("stableLinSpeedThresh", value);
			}
		}

		/// <summary>
		/// Rotational speed threshold for successful balance.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float StableRotSpeedThresh
		{
			set
			{
				if (value > 10.0f)
					value = 10.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("stableRotSpeedThresh", value);
			}
		}

		/// <summary>
		/// The upper body of the character must be colliding and other failure conditions met to fail.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool FailMustCollide
		{
			set => SetArgument("failMustCollide", value);
		}

		/// <summary>
		/// Ignore maxSteps and maxBalanceTime and try to balance forever.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool IgnoreFailure
		{
			set => SetArgument("ignoreFailure", value);
		}

		/// <summary>
		/// Time not in contact (airborne) before step is changed. If -ve don't change step.
		/// </summary>
		/// <remarks>
		/// Default value = -1.0f.
		/// Min value = -1.0f.
		/// Max value = 5.0f.
		/// </remarks>
		public float ChangeStepTime
		{
			set
			{
				if (value > 5.0f)
					value = 5.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("changeStepTime", value);
			}
		}

		/// <summary>
		/// Ignore maxSteps and maxBalanceTime and try to balance forever.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool BalanceIndefinitely
		{
			set => SetArgument("balanceIndefinitely", value);
		}

		/// <summary>
		/// Temporary variable to ignore movingFloor code that generally causes the character to fall over if the feet probe a moving object e.g. treading on a gun.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool MovingFloor
		{
			set => SetArgument("movingFloor", value);
		}

		/// <summary>
		/// When airborne try to step.  Set to false for e.g. shotGun reaction.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool AirborneStep
		{
			set => SetArgument("airborneStep", value);
		}

		/// <summary>
		/// Velocity below which the balancer turns in the direction of the COM forward instead of the ComVel - for use with shot from running with high upright constraint use 1.9.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float UseComDirTurnVelThresh
		{
			set
			{
				if (value > 10.0f)
					value = 10.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("useComDirTurnVelThresh", value);
			}
		}

		/// <summary>
		/// Minimum knee angle (-ve value will mean this functionality is not applied).  0.4 seems a good value.
		/// </summary>
		/// <remarks>
		/// Default value = -0.5f.
		/// Min value = -0.5f.
		/// Max value = 1.5f.
		/// </remarks>
		public float MinKneeAngle
		{
			set
			{
				if (value > 1.5f)
					value = 1.5f;
				if (value < -0.5f)
					value = -0.5f;
				SetArgument("minKneeAngle", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool FlatterSwingFeet
		{
			set => SetArgument("flatterSwingFeet", value);
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool FlatterStaticFeet
		{
			set => SetArgument("flatterStaticFeet", value);
		}

		/// <summary>
		/// If true then balancer tries to avoid leg2leg collisions/avoid crossing legs. Avoid tries to not step across a line of the inside of the stance leg's foot.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool AvoidLeg
		{
			set => SetArgument("avoidLeg", value);
		}

		/// <summary>
		/// NB. Very sensitive. Avoid tries to not step across a line of the inside of the stance leg's foot. AvoidFootWidth = how much inwards from the ankle this line is in (m).
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float AvoidFootWidth
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("avoidFootWidth", value);
			}
		}

		/// <summary>
		/// NB. Very sensitive. Avoid tries to not step across a line of the inside of the stance leg's foot. Avoid doesn't allow the desired stepping foot to cross the line.  avoidFeedback = how much of the actual crossing of that line is fedback as an error.
		/// </summary>
		/// <remarks>
		/// Default value = 0.6f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float AvoidFeedback
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("avoidFeedback", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAgainstVelocity
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("leanAgainstVelocity", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float StepDecisionThreshold
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("stepDecisionThreshold", value);
			}
		}

		/// <summary>
		/// The balancer sometimes decides to step even if balanced.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool StepIfInSupport
		{
			set => SetArgument("stepIfInSupport", value);
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool AlwaysStepWithFarthest
		{
			set => SetArgument("alwaysStepWithFarthest", value);
		}

		/// <summary>
		/// Standup more with increased velocity.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool StandUp
		{
			set => SetArgument("standUp", value);
		}

		/// <summary>
		/// Supposed to increase foot friction: Impact depth of a collision with the foot is changed when the balancer is running - impact.SetDepth(impact.GetDepth() - depthFudge).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float DepthFudge
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("depthFudge", value);
			}
		}

		/// <summary>
		/// Supposed to increase foot friction: Impact depth of a collision with the foot is changed when staggerFall is running - impact.SetDepth(impact.GetDepth() - depthFudgeStagger).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float DepthFudgeStagger
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("depthFudgeStagger", value);
			}
		}

		/// <summary>
		/// Foot friction multiplier is multiplied by this amount if balancer is running.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 40.0f.
		/// </remarks>
		public float FootFriction
		{
			set
			{
				if (value > 40.0f)
					value = 40.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("footFriction", value);
			}
		}

		/// <summary>
		/// Foot friction multiplier is multiplied by this amount if staggerFall is running.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 40.0f.
		/// </remarks>
		public float FootFrictionStagger
		{
			set
			{
				if (value > 40.0f)
					value = 40.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("footFrictionStagger", value);
			}
		}

		/// <summary>
		/// Backwards lean threshold to cut off stay upright forces. 0.0 Vertical - 1.0 horizontal.  0.6 is a sensible value.  NB: the balancer does not fail in order to give stagger that extra step as it falls.  A backwards lean of GT 0.6 will generally mean the balancer will soon fail without stayUpright forces.
		/// </summary>
		/// <remarks>
		/// Default value = 1.1f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float BackwardsLeanCutoff
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("backwardsLeanCutoff", value);
			}
		}

		/// <summary>
		/// If this value is different from giveUpHeight, actual giveUpHeight will be ramped toward this value.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.5f.
		/// </remarks>
		public float GiveUpHeightEnd
		{
			set
			{
				if (value > 1.5f)
					value = 1.5f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("giveUpHeightEnd", value);
			}
		}

		/// <summary>
		/// If this value is different from balanceAbortThreshold, actual balanceAbortThreshold will be ramped toward this value.
		/// </summary>
		/// <remarks>
		/// Default value = 0.6f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float BalanceAbortThresholdEnd
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("balanceAbortThresholdEnd", value);
			}
		}

		/// <summary>
		/// Duration of ramp from start of behavior for above two parameters. If smaller than 0, no ramp is applied.
		/// </summary>
		/// <remarks>
		/// Default value = -1.0f.
		/// Min value = -1.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float GiveUpRampDuration
		{
			set
			{
				if (value > 10.0f)
					value = 10.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("giveUpRampDuration", value);
			}
		}

		/// <summary>
		/// Lean at which to send abort message when maxSteps or maxBalanceTime is reached.
		/// </summary>
		/// <remarks>
		/// Default value = 0.6f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanToAbort
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("leanToAbort", value);
			}
		}
	}

	/// <summary>
	/// Reset the values configurable by the Configure Balance message to their defaults.
	/// </summary>
	public sealed class ConfigureBalanceResetHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureBalanceResetHelper for sending a ConfigureBalanceReset <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureBalanceReset <see cref="Message"/> to.</param>
		/// <remarks>
		/// Reset the values configurable by the Configure Balance message to their defaults.
		/// </remarks>
		public ConfigureBalanceResetHelper(Ped ped) : base(ped, "configureBalanceReset")
		{
		}
	}

	/// <summary>
	/// This single message allows to configure self avoidance for the character.BBDD Self avoidance tech.
	/// </summary>
	public sealed class ConfigureSelfAvoidanceHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureSelfAvoidanceHelper for sending a ConfigureSelfAvoidance <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureSelfAvoidance <see cref="Message"/> to.</param>
		/// <remarks>
		/// This single message allows to configure self avoidance for the character.BBDD Self avoidance tech.
		/// </remarks>
		public ConfigureSelfAvoidanceHelper(Ped ped) : base(ped, "configureSelfAvoidance")
		{
		}

		/// <summary>
		/// Enable or disable self avoidance tech.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool UseSelfAvoidance
		{
			set => SetArgument("useSelfAvoidance", value);
		}

		/// <summary>
		/// Specify whether self avoidance tech should use original IK input target or the target that has been already modified by getStabilisedPos() tech i.e. function that compensates for rotational and linear velocity of shoulder/thigh.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool OverwriteDragReduction
		{
			set => SetArgument("overwriteDragReduction", value);
		}

		/// <summary>
		/// Place the adjusted target this much along the arc between effector (wrist) and target, value in range [0,1].
		/// </summary>
		/// <remarks>
		/// Default value = 0.8f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorsoSwingFraction
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torsoSwingFraction", value);
			}
		}

		/// <summary>
		/// Max value on the effector (wrist) to adjusted target offset.
		/// </summary>
		/// <remarks>
		/// Default value = 0.8f.
		/// Min value = 0.0f.
		/// Max value = 1.6f.
		/// </remarks>
		public float MaxTorsoSwingAngleRad
		{
			set
			{
				if (value > 1.6f)
					value = 1.6f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("maxTorsoSwingAngleRad", value);
			}
		}

		/// <summary>
		/// Restrict self avoidance to operate on targets that are within character torso bounds only.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool SelfAvoidIfInSpineBoundsOnly
		{
			set => SetArgument("selfAvoidIfInSpineBoundsOnly", value);
		}

		/// <summary>
		/// Amount of self avoidance offset applied when angle from effector (wrist) to target is greater then right angle i.e. when total offset is a blend between where effector currently is to value that is a product of total arm length and selfAvoidAmount. SelfAvoidAmount is in a range between [0, 1].
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float SelfAvoidAmount
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("selfAvoidAmount", value);
			}
		}

		/// <summary>
		/// Overwrite desired IK twist with self avoidance procedural twist.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool OverwriteTwist
		{
			set => SetArgument("overwriteTwist", value);
		}

		/// <summary>
		/// Use the alternative self avoidance algorithm that is based on linear and polar target blending. WARNING: It only requires "radius" in terms of parametrization.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool UsePolarPathAlgorithm
		{
			set => SetArgument("usePolarPathAlgorithm", value);
		}

		/// <summary>
		/// Self avoidance radius, measured out from the spine axis along the plane perpendicular to that axis. The closer is the proximity of reaching target to that radius, the more polar (curved) motion is used for offsetting the target. WARNING: Parameter only used by the alternative algorithm that is based on linear and polar target blending.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float Radius
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("radius", value);
			}
		}
	}

	public sealed class ConfigureBulletsHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureBulletsHelper for sending a ConfigureBullets <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureBullets <see cref="Message"/> to.</param>
		public ConfigureBulletsHelper(Ped ped) : base(ped, "configureBullets")
		{
		}

		/// <summary>
		/// Spreads impulse across parts. Currently only for spine parts, not limbs.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ImpulseSpreadOverParts
		{
			set => SetArgument("impulseSpreadOverParts", value);
		}

		/// <summary>
		/// For weaker characters subsequent impulses remain strong.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ImpulseLeakageStrengthScaled
		{
			set => SetArgument("impulseLeakageStrengthScaled", value);
		}

		/// <summary>
		/// Duration that impulse is spread over (triangular shaped).
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulsePeriod
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulsePeriod", value);
			}
		}

		/// <summary>
		/// An impulse applied at a point on a body equivalent to an impulse at the center of the body and a torque.  This parameter scales the torque component. (The torque component seems to be excite the rage looseness bug which sends the character in a sometimes wildly different direction to an applied impulse).
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseTorqueScale
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseTorqueScale", value);
			}
		}

		/// <summary>
		/// Fix the rage looseness bug by applying only the impulse at the center of the body unless it is a spine part then apply the twist component only of the torque as well.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LoosenessFix
		{
			set => SetArgument("loosenessFix", value);
		}

		/// <summary>
		/// Time from hit before impulses are being applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseDelay
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseDelay", value);
			}
		}

		/// <summary>
		/// By how much are subsequent impulses reduced (e.g. 0.0: no reduction, 0.1: 10% reduction each new hit).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseReductionPerShot
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseReductionPerShot", value);
			}
		}

		/// <summary>
		/// Recovery rate of impulse strength per second (impulse strength from 0.0:1.0).  At 60fps a impulseRecovery=60.0 will recover in 1 frame.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 60.0f.
		/// </remarks>
		public float ImpulseRecovery
		{
			set
			{
				if (value > 60.0f)
					value = 60.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseRecovery", value);
			}
		}

		/// <summary>
		/// The minimum amount of impulse leakage allowed.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseMinLeakage
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseMinLeakage", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueMode.Disabled"/>.
		/// If <see cref="TorqueMode.Proportional"/> - proportional to character strength, can reduce impulse amount.
		/// If <see cref="TorqueMode.Additive"/> - no reduction of impulse and not proportional to character strength.
		/// </remarks>
		public TorqueMode TorqueMode
		{
			set => SetArgument("torqueMode", (int)value);
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueSpinMode.FromImpulse"/>.
		/// If <see cref="TorqueSpinMode.Flipping"/> a burst effect is achieved.
		/// </remarks>
		public TorqueSpinMode TorqueSpinMode
		{
			set => SetArgument("torqueSpinMode", (int)value);
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueFilterMode.ApplyEveryBullet"/>.
		/// </remarks>
		public TorqueFilterMode TorqueFilterMode
		{
			set => SetArgument("torqueFilterMode", (int)value);
		}

		/// <summary>
		/// Always apply torques to spine3 instead of actual part hit.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool TorqueAlwaysSpine3
		{
			set => SetArgument("torqueAlwaysSpine3", value);
		}

		/// <summary>
		/// Time from hit before torques are being applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorqueDelay
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torqueDelay", value);
			}
		}

		/// <summary>
		/// Duration of torque.
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorquePeriod
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torquePeriod", value);
			}
		}

		/// <summary>
		/// Multiplies impulse magnitude to arrive at torque that is applied.
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float TorqueGain
		{
			set
			{
				if (value > 10.0f)
					value = 10.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torqueGain", value);
			}
		}

		/// <summary>
		/// Minimum ratio of impulse that remains after converting to torque (if in strength-proportional mode).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorqueCutoff
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torqueCutoff", value);
			}
		}

		/// <summary>
		/// Ratio of torque for next tick (e.g. 1.0: not reducing over time, 0.9: each tick torque is reduced by 10%).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorqueReductionPerTick
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torqueReductionPerTick", value);
			}
		}

		/// <summary>
		/// Amount of lift (directly multiplies torque axis to give lift force).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LiftGain
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("liftGain", value);
			}
		}

		/// <summary>
		/// Time after impulse is applied that counter impulse is applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float CounterImpulseDelay
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("counterImpulseDelay", value);
			}
		}

		/// <summary>
		/// Amount of the original impulse that is countered.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float CounterImpulseMag
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("counterImpulseMag", value);
			}
		}

		/// <summary>
		/// Applies the counter impulse counterImpulseDelay(secs) after counterImpulseMag of the Impulse has been applied.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool CounterAfterMagReached
		{
			set => SetArgument("counterAfterMagReached", value);
		}

		/// <summary>
		/// Add a counter impulse to the pelvis.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool DoCounterImpulse
		{
			set => SetArgument("doCounterImpulse", value);
		}

		/// <summary>
		/// Amount of the counter impulse applied to hips - the rest is applied to the part originally hit.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float CounterImpulse2Hips
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("counterImpulse2Hips", value);
			}
		}

		/// <summary>
		/// Amount to scale impulse by if the dynamicBalance is not OK.  1.0 means this functionality is not applied.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseNoBalMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseNoBalMult", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseBalStabMult*100% GT End. NB: Start LT End.
		/// </summary>
		/// <remarks>
		/// Default value = 3.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float ImpulseBalStabStart
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseBalStabStart", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseBalStabMult*100% GT End. NB: Start LT End.
		/// </summary>
		/// <remarks>
		/// Default value = 10.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float ImpulseBalStabEnd
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseBalStabEnd", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseBalStabMult*100% GT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseBalStabMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseBalStabMult", value);
			}
		}

		/// <summary>
		/// 100% GE Start to impulseSpineAngMult*100% LT End. NB: Start GT End.  This the dot of hip2Head with up.
		/// </summary>
		/// <remarks>
		/// Default value = 0.7f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseSpineAngStart
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("impulseSpineAngStart", value);
			}
		}

		/// <summary>
		/// 100% GE Start to impulseSpineAngMult*100% LT End. NB: Start GT End.  This the dot of hip2Head with up.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseSpineAngEnd
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("impulseSpineAngEnd", value);
			}
		}

		/// <summary>
		/// 100% GE Start to impulseSpineAngMult*100% LT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseSpineAngMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseSpineAngMult", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseVelMult*100% GT End. NB: Start LT End.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float ImpulseVelStart
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseVelStart", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseVelMult*100% GT End. NB: Start LT End.
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float ImpulseVelEnd
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseVelEnd", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseVelMult*100% GT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseVelMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseVelMult", value);
			}
		}

		/// <summary>
		/// Amount to scale impulse by if the character is airborne and dynamicBalance is OK and impulse is above impulseAirMultStart.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseAirMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseAirMult", value);
			}
		}

		/// <summary>
		/// If impulse is above this value scale it by impulseAirMult.
		/// </summary>
		/// <remarks>
		/// Default value = 100.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseAirMultStart
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseAirMultStart", value);
			}
		}

		/// <summary>
		/// Amount to clamp impulse to if character is airborne  and dynamicBalance is OK.
		/// </summary>
		/// <remarks>
		/// Default value = 100.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseAirMax
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseAirMax", value);
			}
		}

		/// <summary>
		/// If impulse is above this amount then do not scale/clamp just let it through as is - it's a shotgun or cannon.
		/// </summary>
		/// <remarks>
		/// Default value = 399.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseAirApplyAbove
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseAirApplyAbove", value);
			}
		}

		/// <summary>
		/// Scale and/or clamp impulse if the character is airborne and dynamicBalance is OK.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ImpulseAirOn
		{
			set => SetArgument("impulseAirOn", value);
		}

		/// <summary>
		/// Amount to scale impulse by if the character is contacting with one foot only and dynamicBalance is OK and impulse is above impulseAirMultStart.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseOneLegMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseOneLegMult", value);
			}
		}

		/// <summary>
		/// If impulse is above this value scale it by impulseOneLegMult.
		/// </summary>
		/// <remarks>
		/// Default value = 100.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseOneLegMultStart
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseOneLegMultStart", value);
			}
		}

		/// <summary>
		/// Amount to clamp impulse to if character is contacting with one foot only  and dynamicBalance is OK.
		/// </summary>
		/// <remarks>
		/// Default value = 100.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseOneLegMax
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseOneLegMax", value);
			}
		}

		/// <summary>
		/// If impulse is above this amount then do not scale/clamp just let it through as is - it's a shotgun or cannon.
		/// </summary>
		/// <remarks>
		/// Default value = 399.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseOneLegApplyAbove
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseOneLegApplyAbove", value);
			}
		}

		/// <summary>
		/// Scale and/or clamp impulse if the character is contacting with one leg only and dynamicBalance is OK.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ImpulseOneLegOn
		{
			set => SetArgument("impulseOneLegOn", value);
		}

		/// <summary>
		/// 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbRatio
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbRatio", value);
			}
		}

		/// <summary>
		/// Rigid body response is shared between the upper and lower body (rbUpperShare = 1-rbLowerShare). RbLowerShare=0.5 gives upper and lower share scaled by mass.  i.e. if 70% ub mass and 30% lower mass then rbLowerShare=0.5 gives actualrbShare of 0.7ub and 0.3lb. rbLowerShare GT 0.5 scales the ub share down from 0.7 and the lb up from 0.3.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbLowerShare
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbLowerShare", value);
			}
		}

		/// <summary>
		/// 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbMoment
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMoment", value);
			}
		}

		/// <summary>
		/// Maximum twist arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxTwistMomentArm
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxTwistMomentArm", value);
			}
		}

		/// <summary>
		/// Maximum broom((everything but the twist) arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxBroomMomentArm
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxBroomMomentArm", value);
			}
		}

		/// <summary>
		/// If Airborne: 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbRatioAirborne
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbRatioAirborne", value);
			}
		}

		/// <summary>
		/// If Airborne: 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbMomentAirborne
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMomentAirborne", value);
			}
		}

		/// <summary>
		/// If Airborne: Maximum twist arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxTwistMomentArmAirborne
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxTwistMomentArmAirborne", value);
			}
		}

		/// <summary>
		/// If Airborne: Maximum broom((everything but the twist) arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxBroomMomentArmAirborne
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxBroomMomentArmAirborne", value);
			}
		}

		/// <summary>
		/// If only one leg in contact: 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbRatioOneLeg
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbRatioOneLeg", value);
			}
		}

		/// <summary>
		/// If only one leg in contact: 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbMomentOneLeg
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMomentOneLeg", value);
			}
		}

		/// <summary>
		/// If only one leg in contact: Maximum twist arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxTwistMomentArmOneLeg
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxTwistMomentArmOneLeg", value);
			}
		}

		/// <summary>
		/// If only one leg in contact: Maximum broom((everything but the twist) arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxBroomMomentArmOneLeg
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxBroomMomentArmOneLeg", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="RbTwistAxis.WorldUp"/>.
		/// </remarks>.
		public RbTwistAxis RbTwistAxis
		{
			set => SetArgument("rbTwistAxis", (int)value);
		}

		/// <summary>
		/// If false pivot around COM always, if true change pivot depending on foot contact:  to feet center if both feet in contact, or foot position if 1 foot in contact or COM position if no feet in contact.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool RbPivot
		{
			set => SetArgument("rbPivot", value);
		}
	}

	public sealed class ConfigureBulletsExtraHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureBulletsExtraHelper for sending a ConfigureBulletsExtra <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureBulletsExtra <see cref="Message"/> to.</param>
		public ConfigureBulletsExtraHelper(Ped ped) : base(ped, "configureBulletsExtra")
		{
		}

		/// <summary>
		/// Spreads impulse across parts. Currently only for spine parts, not limbs.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ImpulseSpreadOverParts
		{
			set => SetArgument("impulseSpreadOverParts", value);
		}

		/// <summary>
		/// Duration that impulse is spread over (triangular shaped).
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulsePeriod
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulsePeriod", value);
			}
		}

		/// <summary>
		/// An impulse applied at a point on a body equivalent to an impulse at the center of the body and a torque.  This parameter scales the torque component. (The torque component seems to be excite the rage looseness bug which sends the character in a sometimes wildly different direction to an applied impulse).
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseTorqueScale
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseTorqueScale", value);
			}
		}

		/// <summary>
		/// Fix the rage looseness bug by applying only the impulse at the center of the body unless it is a spine part then apply the twist component only of the torque as well.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LoosenessFix
		{
			set => SetArgument("loosenessFix", value);
		}

		/// <summary>
		/// Time from hit before impulses are being applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseDelay
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseDelay", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueMode.Disabled"/>.
		/// If <see cref="TorqueMode.Proportional"/> - proportional to character strength, can reduce impulse amount.
		/// If <see cref="TorqueMode.Additive"/> - no reduction of impulse and not proportional to character strength.
		/// </remarks>
		public TorqueMode TorqueMode
		{
			set => SetArgument("torqueMode", (int)value);
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueSpinMode.FromImpulse"/>.
		/// If <see cref="TorqueSpinMode.Flipping"/> a burst effect is achieved.
		/// </remarks>
		public TorqueSpinMode TorqueSpinMode
		{
			set => SetArgument("torqueSpinMode", (int)value);
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueFilterMode.ApplyEveryBullet"/>.
		/// </remarks>
		public TorqueFilterMode TorqueFilterMode
		{
			set => SetArgument("torqueFilterMode", (int)value);
		}

		/// <summary>
		/// Always apply torques to spine3 instead of actual part hit.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool TorqueAlwaysSpine3
		{
			set => SetArgument("torqueAlwaysSpine3", value);
		}

		/// <summary>
		/// Time from hit before torques are being applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorqueDelay
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torqueDelay", value);
			}
		}

		/// <summary>
		/// Duration of torque.
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorquePeriod
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torquePeriod", value);
			}
		}

		/// <summary>
		/// Multiplies impulse magnitude to arrive at torque that is applied.
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float TorqueGain
		{
			set
			{
				if (value > 10.0f)
					value = 10.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torqueGain", value);
			}
		}

		/// <summary>
		/// Minimum ratio of impulse that remains after converting to torque (if in strength-proportional mode).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorqueCutoff
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torqueCutoff", value);
			}
		}

		/// <summary>
		/// Ratio of torque for next tick (e.g. 1.0: not reducing over time, 0.9: each tick torque is reduced by 10%).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float TorqueReductionPerTick
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("torqueReductionPerTick", value);
			}
		}

		/// <summary>
		/// Amount of lift (directly multiplies torque axis to give lift force).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LiftGain
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("liftGain", value);
			}
		}

		/// <summary>
		/// Time after impulse is applied that counter impulse is applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float CounterImpulseDelay
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("counterImpulseDelay", value);
			}
		}

		/// <summary>
		/// Amount of the original impulse that is countered.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float CounterImpulseMag
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("counterImpulseMag", value);
			}
		}

		/// <summary>
		/// Applies the counter impulse counterImpulseDelay(secs) after counterImpulseMag of the Impulse has been applied.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool CounterAfterMagReached
		{
			set => SetArgument("counterAfterMagReached", value);
		}

		/// <summary>
		/// Add a counter impulse to the pelvis.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool DoCounterImpulse
		{
			set => SetArgument("doCounterImpulse", value);
		}

		/// <summary>
		/// Amount of the counter impulse applied to hips - the rest is applied to the part originally hit.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float CounterImpulse2Hips
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("counterImpulse2Hips", value);
			}
		}

		/// <summary>
		/// Amount to scale impulse by if the dynamicBalance is not OK.  1.0 means this functionality is not applied.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseNoBalMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseNoBalMult", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseBalStabMult*100% GT End. NB: Start LT End.
		/// </summary>
		/// <remarks>
		/// Default value = 3.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float ImpulseBalStabStart
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseBalStabStart", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseBalStabMult*100% GT End. NB: Start LT End.
		/// </summary>
		/// <remarks>
		/// Default value = 10.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float ImpulseBalStabEnd
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseBalStabEnd", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseBalStabMult*100% GT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseBalStabMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseBalStabMult", value);
			}
		}

		/// <summary>
		/// 100% GE Start to impulseSpineAngMult*100% LT End. NB: Start GT End.  This the dot of hip2Head with up.
		/// </summary>
		/// <remarks>
		/// Default value = 0.7f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseSpineAngStart
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("impulseSpineAngStart", value);
			}
		}

		/// <summary>
		/// 100% GE Start to impulseSpineAngMult*100% LT End. NB: Start GT End.  This the dot of hip2Head with up.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseSpineAngEnd
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < -1.0f)
					value = -1.0f;
				SetArgument("impulseSpineAngEnd", value);
			}
		}

		/// <summary>
		/// 100% GE Start to impulseSpineAngMult*100% LT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseSpineAngMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseSpineAngMult", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseVelMult*100% GT End. NB: Start LT End.
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float ImpulseVelStart
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseVelStart", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseVelMult*100% GT End. NB: Start LT End.
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public float ImpulseVelEnd
		{
			set
			{
				if (value > 100.0f)
					value = 100.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseVelEnd", value);
			}
		}

		/// <summary>
		/// 100% LE Start to impulseVelMult*100% GT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseVelMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseVelMult", value);
			}
		}

		/// <summary>
		/// Amount to scale impulse by if the character is airborne and dynamicBalance is OK and impulse is above impulseAirMultStart.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseAirMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseAirMult", value);
			}
		}

		/// <summary>
		/// If impulse is above this value scale it by impulseAirMult.
		/// </summary>
		/// <remarks>
		/// Default value = 100.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseAirMultStart
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseAirMultStart", value);
			}
		}

		/// <summary>
		/// Amount to clamp impulse to if character is airborne  and dynamicBalance is OK.
		/// </summary>
		/// <remarks>
		/// Default value = 100.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseAirMax
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseAirMax", value);
			}
		}

		/// <summary>
		/// If impulse is above this amount then do not scale/clamp just let it through as is - it's a shotgun or cannon.
		/// </summary>
		/// <remarks>
		/// Default value = 399.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseAirApplyAbove
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseAirApplyAbove", value);
			}
		}

		/// <summary>
		/// Scale and/or clamp impulse if the character is airborne and dynamicBalance is OK.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ImpulseAirOn
		{
			set => SetArgument("impulseAirOn", value);
		}

		/// <summary>
		/// Amount to scale impulse by if the character is contacting with one foot only and dynamicBalance is OK and impulse is above impulseAirMultStart.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ImpulseOneLegMult
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseOneLegMult", value);
			}
		}

		/// <summary>
		/// If impulse is above this value scale it by impulseOneLegMult.
		/// </summary>
		/// <remarks>
		/// Default value = 100.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseOneLegMultStart
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseOneLegMultStart", value);
			}
		}

		/// <summary>
		/// Amount to clamp impulse to if character is contacting with one foot only  and dynamicBalance is OK.
		/// </summary>
		/// <remarks>
		/// Default value = 100.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseOneLegMax
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseOneLegMax", value);
			}
		}

		/// <summary>
		/// If impulse is above this amount then do not scale/clamp just let it through as is - it's a shotgun or cannon.
		/// </summary>
		/// <remarks>
		/// Default value = 399.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ImpulseOneLegApplyAbove
		{
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("impulseOneLegApplyAbove", value);
			}
		}

		/// <summary>
		/// Scale and/or clamp impulse if the character is contacting with one leg only and dynamicBalance is OK.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ImpulseOneLegOn
		{
			set => SetArgument("impulseOneLegOn", value);
		}

		/// <summary>
		/// 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbRatio
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbRatio", value);
			}
		}

		/// <summary>
		/// Rigid body response is shared between the upper and lower body (rbUpperShare = 1-rbLowerShare). RbLowerShare=0.5 gives upper and lower share scaled by mass.  i.e. if 70% ub mass and 30% lower mass then rbLowerShare=0.5 gives actualrbShare of 0.7ub and 0.3lb. rbLowerShare GT 0.5 scales the ub share down from 0.7 and the lb up from 0.3.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbLowerShare
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbLowerShare", value);
			}
		}

		/// <summary>
		/// 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbMoment
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMoment", value);
			}
		}

		/// <summary>
		/// Maximum twist arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxTwistMomentArm
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxTwistMomentArm", value);
			}
		}

		/// <summary>
		/// Maximum broom((everything but the twist) arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxBroomMomentArm
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxBroomMomentArm", value);
			}
		}

		/// <summary>
		/// If Airborne: 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbRatioAirborne
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbRatioAirborne", value);
			}
		}

		/// <summary>
		/// If Airborne: 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbMomentAirborne
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMomentAirborne", value);
			}
		}

		/// <summary>
		/// If Airborne: Maximum twist arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxTwistMomentArmAirborne
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxTwistMomentArmAirborne", value);
			}
		}

		/// <summary>
		/// If Airborne: Maximum broom((everything but the twist) arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxBroomMomentArmAirborne
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxBroomMomentArmAirborne", value);
			}
		}

		/// <summary>
		/// If only one leg in contact: 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbRatioOneLeg
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbRatioOneLeg", value);
			}
		}

		/// <summary>
		/// If only one leg in contact: 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float RbMomentOneLeg
		{
			set
			{
				if (value > 1.0f)
					value = 1.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMomentOneLeg", value);
			}
		}

		/// <summary>
		/// If only one leg in contact: Maximum twist arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxTwistMomentArmOneLeg
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxTwistMomentArmOneLeg", value);
			}
		}

		/// <summary>
		/// If only one leg in contact: Maximum broom((everything but the twist) arm moment of bullet applied.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float RbMaxBroomMomentArmOneLeg
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("rbMaxBroomMomentArmOneLeg", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="RbTwistAxis.WorldUp"/>.
		/// </remarks>.
		public RbTwistAxis RbTwistAxis
		{
			set => SetArgument("rbTwistAxis", (int)value);
		}

		/// <summary>
		/// If false pivot around COM always, if true change pivot depending on foot contact:  to feet center if both feet in contact, or foot position if 1 foot in contact or COM position if no feet in contact.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool RbPivot
		{
			set => SetArgument("rbPivot", value);
		}
	}

	/// <summary>
	/// Enable/disable/edit character limits in real time.  This adjusts limits in RAGE-native space and will *not* reorient the joint.
	/// </summary>
	public sealed class ConfigureLimitsHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureLimitsHelper for sending a ConfigureLimits <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureLimits <see cref="Message"/> to.</param>
		/// <remarks>
		/// Enable/disable/edit character limits in real time.  This adjusts limits in RAGE-native space and will *not* reorient the joint.
		/// </remarks>
		public ConfigureLimitsHelper(Ped ped) : base(ped, "configureLimits")
		{
		}

		/// <summary>
		/// Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  for joint limits to configure. Ignored if index != -1.
		/// </summary>
		/// <remarks>
		/// Default value = "fb".
		/// </remarks>
		public string Mask
		{
			set => SetArgument("mask", value);
		}

		/// <summary>
		/// If false, disable (set all to PI, -PI) limits.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool Enable
		{
			set => SetArgument("enable", value);
		}

		/// <summary>
		/// If true, set limits to accommodate current desired angles.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ToDesired
		{
			set => SetArgument("toDesired", value);
		}

		/// <summary>
		/// Return to cached defaults?.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool Restore
		{
			set => SetArgument("restore", value);
		}

		/// <summary>
		/// If true, set limits to the current animated limits.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ToCurAnimation
		{
			set => SetArgument("toCurAnimation", value);
		}

		/// <summary>
		/// Index of effector to configure.  Set to -1 to use mask.
		/// </summary>
		/// <remarks>
		/// Default value = -1.
		/// Min value = -1.
		/// </remarks>
		public int Index
		{
			set
			{
				if (value < -1)
					value = -1;
				SetArgument("index", value);
			}
		}

		/// <summary>
		/// Custom limit values to use if not setting limits to desired. Limits are RAGE-native, not NM-wrapper-native.
		/// </summary>
		/// <remarks>
		/// Default value = 1.6f.
		/// Min value = 0.0f.
		/// Max value = 3.1f.
		/// </remarks>
		public float Lean1
		{
			set
			{
				if (value > 3.1f)
					value = 3.1f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("lean1", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = 1.6f.
		/// Min value = 0.0f.
		/// Max value = 3.1f.
		/// </remarks>
		public float Lean2
		{
			set
			{
				if (value > 3.1f)
					value = 3.1f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("lean2", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = 1.6f.
		/// Min value = 0.0f.
		/// Max value = 3.1f.
		/// </remarks>
		public float Twist
		{
			set
			{
				if (value > 3.1f)
					value = 3.1f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("twist", value);
			}
		}

		/// <summary>
		/// Joint limit margin to add to current animation limits when using those to set runtime limits.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = 0.0f.
		/// Max value = 3.1f.
		/// </remarks>
		public float Margin
		{
			set
			{
				if (value > 3.1f)
					value = 3.1f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("margin", value);
			}
		}
	}

	public sealed class ConfigureSoftLimitHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureSoftLimitHelper for sending a ConfigureSoftLimit <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureSoftLimit <see cref="Message"/> to.</param>
		public ConfigureSoftLimitHelper(Ped ped) : base(ped, "configureSoftLimit")
		{
		}

		/// <summary>
		/// Select limb that the soft limit is going to be applied to.
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// Max value = 3.
		/// </remarks>
		public int Index
		{
			set
			{
				if (value > 3)
					value = 3;
				if (value < 0)
					value = 0;
				SetArgument("index", value);
			}
		}

		/// <summary>
		/// Stiffness of the soft limit.
		/// Parameter is used to calculate spring term that contributes to the desired acceleration.
		/// </summary>
		/// <remarks>
		/// Default value = 15.0f.
		/// Min value = 0.0f.
		/// Max value = 30.0f.
		/// </remarks>
		public float Stiffness
		{
			set
			{
				if (value > 30.0f)
					value = 30.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("stiffness", value);
			}
		}

		/// <summary>
		/// Damping of the soft limit.
		/// Parameter is used to calculate damper term that contributes to the desired acceleration.
		/// To have the system critically dampened set it to 1.0.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.9f.
		/// Max value = 1.1f.
		/// </remarks>
		public float Damping
		{
			set
			{
				if (value > 1.1f)
					value = 1.1f;
				if (value < 0.9f)
					value = 0.9f;
				SetArgument("damping", value);
			}
		}

		/// <summary>
		/// Soft limit angle.
		/// Positive angle in RAD, measured relatively either from hard limit maxAngle (approach direction = -1) or minAngle (approach direction = 1).
		/// This angle will be clamped if outside the joint hard limit range.
		/// </summary>
		/// <remarks>
		/// Default value = 0.4f.
		/// Min value = 0.0f.
		/// Max value = 6.3f.
		/// </remarks>
		public float LimitAngle
		{
			set
			{
				if (value > 6.3f)
					value = 6.3f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("limitAngle", value);
			}
		}

		/// <summary>
		/// Limit angle can be measured relatively to joints hard limit minAngle or maxAngle.
		/// Set it to +1 to measure soft limit angle relatively to hard limit minAngle that corresponds to the maximum stretch of the elbow.
		/// Set it to -1 to measure soft limit angle relatively to hard limit maxAngle that corresponds to the maximum stretch of the knee.
		/// </summary>
		/// <remarks>
		/// Default value = 1.
		/// Min value = -1.
		/// Max value = 1.
		/// </remarks>
		public int ApproachDirection
		{
			set
			{
				if (value > 1)
					value = 1;
				if (value < -1)
					value = -1;
				SetArgument("approachDirection", value);
			}
		}

		/// <summary>
		/// Scale stiffness based on character angular velocity.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool VelocityScaled
		{
			set => SetArgument("velocityScaled", value);
		}
	}

	/// <summary>
	/// This single message allows you to configure the injured arm reaction during shot.
	/// </summary>
	public sealed class ConfigureShotInjuredArmHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureShotInjuredArmHelper for sending a ConfigureShotInjuredArm <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureShotInjuredArm <see cref="Message"/> to.</param>
		/// <remarks>
		/// This single message allows you to configure the injured arm reaction during shot.
		/// </remarks>
		public ConfigureShotInjuredArmHelper(Ped ped) : base(ped, "configureShotInjuredArm")
		{
		}

		/// <summary>
		/// Length of the reaction.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float InjuredArmTime
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("injuredArmTime", value);
			}
		}

		/// <summary>
		/// Amount of hip twist.  (Negative values twist into bullet direction - probably not what is wanted).
		/// </summary>
		/// <remarks>
		/// Default value = 0.8f.
		/// Min value = -2.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float HipYaw
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < -2.0f)
					value = -2.0f;
				SetArgument("hipYaw", value);
			}
		}

		/// <summary>
		/// Amount of hip roll.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = -2.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float HipRoll
		{
			set
			{
				if (value > 2.0f)
					value = 2.0f;
				if (value < -2.0f)
					value = -2.0f;
				SetArgument("hipRoll", value);
			}
		}

		/// <summary>
		/// Additional height added to stepping foot.
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = 0.0f.
		/// Max value = 0.7f.
		/// </remarks>
		public float ForceStepExtraHeight
		{
			set
			{
				if (value > 0.7f)
					value = 0.7f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("forceStepExtraHeight", value);
			}
		}

		/// <summary>
		/// Force a step to be taken whether pushed out of balance or not.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool ForceStep
		{
			set => SetArgument("forceStep", value);
		}

		/// <summary>
		/// Turn the character using the balancer.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool StepTurn
		{
			set => SetArgument("stepTurn", value);
		}

		/// <summary>
		/// Start velocity where parameters begin to be ramped down to zero linearly.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float VelMultiplierStart
		{
			set
			{
				if (value > 20.0f)
					value = 20.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("velMultiplierStart", value);
			}
		}

		/// <summary>
		/// End velocity of ramp where parameters are scaled to zero.
		/// </summary>
		/// <remarks>
		/// Default value = 5.0f.
		/// Min value = 1.0f.
		/// Max value = 40.0f.
		/// </remarks>
		public float VelMultiplierEnd
		{
			set
			{
				if (value > 40.0f)
					value = 40.0f;
				if (value < 1.0f)
					value = 1.0f;
				SetArgument("velMultiplierEnd", value);
			}
		}

		/// <summary>
		/// Velocity above which a step is not forced.
		/// </summary>
		/// <remarks>
		/// Default value = 0.8f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float VelForceStep
		{
			set
			{
				if (value > 20.0f)
					value = 20.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("velForceStep", value);
			}
		}

		/// <summary>
		/// Velocity above which a stepTurn is not asked for.
		/// </summary>
		/// <remarks>
		/// Default value = 0.8f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float VelStepTurn
		{
			set
			{
				if (value > 20.0f)
					value = 20.0f;
				if (value < 0.0f)
					value = 0.0f;
				SetArgument("velStepTurn", value);
			}
		}

		/// <summary>
		/// Use the velocity scaling parameters.
		/// Tune for standing still then use velocity scaling to make sure a running character stays balanced (the turning tends to make the character fall over more at speed).
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool VelScales
		{
			set => SetArgument("velScales", value);
		}
	}

	/// <summary>
	/// This single message allows you to configure the injured leg reaction during shot.