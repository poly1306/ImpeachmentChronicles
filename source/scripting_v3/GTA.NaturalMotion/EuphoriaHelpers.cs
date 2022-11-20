
//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;

namespace GTA.NaturalMotion
{
	public enum ArmDirection
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
			set
			{
				SetArgument("mask", value);
			}
		}

		/// <summary>
		/// Apply gravity compensation.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool UseGravityCompensation
		{
			set
			{
				SetArgument("useGravityCompensation", value);
			}
		}

		/// <summary>
		/// Animation source.
		/// </summary>
		public AnimSource AnimSource
		{
			set
			{
				SetArgument("animSource", (int)value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 28;
				}

				if (value < -1)
				{
					value = -1;
				}

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
			set
			{
				SetArgument("impulse", Vector3.Clamp(value, new Vector3(-4500.0f, -4500.0f, -4500.0f), new Vector3(4500.0f, 4500.0f, 4500.0f)));
			}
		}

		/// <summary>
		/// Optional point on part where hit. If not supplied then the impulse is applied at the part center.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 HitPoint
		{
			set
			{
				SetArgument("hitPoint", value);
			}
		}

		/// <summary>
		/// Hit point in local coordinates of body part.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LocalHitPointInfo
		{
			set
			{
				SetArgument("localHitPointInfo", value);
			}
		}

		/// <summary>
		/// Impulse in local coordinates of body part.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LocalImpulseInfo
		{
			set
			{
				SetArgument("localImpulseInfo", value);
			}
		}

		/// <summary>
		/// Impulse should be considered an angular impulse.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool AngularImpulse
		{
			set
			{
				SetArgument("angularImpulse", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 28;
				}

				if (value < 0)
				{
					value = 0;
				}

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
			set
			{
				SetArgument("impulse", Vector3.Clamp(value, new Vector3(-1000.0f, -1000.0f, -1000.0f), new Vector3(1000.0f, 1000.0f, 1000.0f)));
			}
		}

		/// <summary>
		/// Optional point on part where hit.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 HitPoint
		{
			set
			{
				SetArgument("hitPoint", value);
			}
		}

		/// <summary>
		/// True = hitPoint is in local coordinates of bodyPart, false = hit point is in world coordinates.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LocalHitPointInfo
		{
			set
			{
				SetArgument("localHitPointInfo", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < -2.0f)
				{
					value = -2.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("mask", value);
			}
		}

		/// <summary>
		/// Automatically hold the current pose as the character relaxes - can be used to avoid relaxing into a t-pose.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool HoldPose
		{
			set
			{
				SetArgument("holdPose", value);
			}
		}

		/// <summary>
		/// Sets the drive state to free - this reduces drifting on the ground.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool DisableJointDriving
		{
			set
			{
				SetArgument("disableJointDriving", value);
			}
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
				{
					value = 0.4f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 0.4f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("taperKneeStrength", value);
			}
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
				{
					value = 16.0f;
				}

				if (value < 6.0f)
				{
					value = 6.0f;
				}

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
				{
					value = 4.0f;
				}

				if (value < 0.2f)
				{
					value = 0.2f;
				}

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
				{
					value = 4.0f;
				}

				if (value < 0.2f)
				{
					value = 0.2f;
				}

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
				{
					value = 4.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 4.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("footSlipCompOnMovingFloor", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.5f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1;
				}

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
				{
					value = 1.0f;
				}

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
				{
					value = -1;
				}

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
				{
					value = -1.0f;
				}

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
			set
			{
				SetArgument("fallType", (int)value);
			}
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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("fallReduceGravityComp", value);
			}
		}

		/// <summary>
		/// Bend over when falling after maxBalanceTime.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool RampHipPitchOnFail
		{
			set
			{
				SetArgument("rampHipPitchOnFail", value);
			}
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
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("failMustCollide", value);
			}
		}

		/// <summary>
		/// Ignore maxSteps and maxBalanceTime and try to balance forever.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool IgnoreFailure
		{
			set
			{
				SetArgument("ignoreFailure", value);
			}
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
				{
					value = 5.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
			set
			{
				SetArgument("balanceIndefinitely", value);
			}
		}

		/// <summary>
		/// Temporary variable to ignore movingFloor code that generally causes the character to fall over if the feet probe a moving object e.g. treading on a gun.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool MovingFloor
		{
			set
			{
				SetArgument("movingFloor", value);
			}
		}

		/// <summary>
		/// When airborne try to step.  Set to false for e.g. shotGun reaction.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool AirborneStep
		{
			set
			{
				SetArgument("airborneStep", value);
			}
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
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.5f;
				}

				if (value < -0.5f)
				{
					value = -0.5f;
				}

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
			set
			{
				SetArgument("flatterSwingFeet", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool FlatterStaticFeet
		{
			set
			{
				SetArgument("flatterStaticFeet", value);
			}
		}

		/// <summary>
		/// If true then balancer tries to avoid leg2leg collisions/avoid crossing legs. Avoid tries to not step across a line of the inside of the stance leg's foot.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool AvoidLeg
		{
			set
			{
				SetArgument("avoidLeg", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("stepIfInSupport", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool AlwaysStepWithFarthest
		{
			set
			{
				SetArgument("alwaysStepWithFarthest", value);
			}
		}

		/// <summary>
		/// Standup more with increased velocity.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool StandUp
		{
			set
			{
				SetArgument("standUp", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 40.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 40.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.5f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 10.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("useSelfAvoidance", value);
			}
		}

		/// <summary>
		/// Specify whether self avoidance tech should use original IK input target or the target that has been already modified by getStabilisedPos() tech i.e. function that compensates for rotational and linear velocity of shoulder/thigh.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool OverwriteDragReduction
		{
			set
			{
				SetArgument("overwriteDragReduction", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.6f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("selfAvoidIfInSpineBoundsOnly", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("overwriteTwist", value);
			}
		}

		/// <summary>
		/// Use the alternative self avoidance algorithm that is based on linear and polar target blending. WARNING: It only requires "radius" in terms of parametrization.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool UsePolarPathAlgorithm
		{
			set
			{
				SetArgument("usePolarPathAlgorithm", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("impulseSpreadOverParts", value);
			}
		}

		/// <summary>
		/// For weaker characters subsequent impulses remain strong.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ImpulseLeakageStrengthScaled
		{
			set
			{
				SetArgument("impulseLeakageStrengthScaled", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("loosenessFix", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 60.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("torqueMode", (int)value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueSpinMode.FromImpulse"/>.
		/// If <see cref="TorqueSpinMode.Flipping"/> a burst effect is achieved.
		/// </remarks>
		public TorqueSpinMode TorqueSpinMode
		{
			set
			{
				SetArgument("torqueSpinMode", (int)value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueFilterMode.ApplyEveryBullet"/>.
		/// </remarks>
		public TorqueFilterMode TorqueFilterMode
		{
			set
			{
				SetArgument("torqueFilterMode", (int)value);
			}
		}

		/// <summary>
		/// Always apply torques to spine3 instead of actual part hit.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool TorqueAlwaysSpine3
		{
			set
			{
				SetArgument("torqueAlwaysSpine3", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("counterAfterMagReached", value);
			}
		}

		/// <summary>
		/// Add a counter impulse to the pelvis.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool DoCounterImpulse
		{
			set
			{
				SetArgument("doCounterImpulse", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("impulseAirOn", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("impulseOneLegOn", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("rbTwistAxis", (int)value);
			}
		}

		/// <summary>
		/// If false pivot around COM always, if true change pivot depending on foot contact:  to feet center if both feet in contact, or foot position if 1 foot in contact or COM position if no feet in contact.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool RbPivot
		{
			set
			{
				SetArgument("rbPivot", value);
			}
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
			set
			{
				SetArgument("impulseSpreadOverParts", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("loosenessFix", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("torqueMode", (int)value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueSpinMode.FromImpulse"/>.
		/// If <see cref="TorqueSpinMode.Flipping"/> a burst effect is achieved.
		/// </remarks>
		public TorqueSpinMode TorqueSpinMode
		{
			set
			{
				SetArgument("torqueSpinMode", (int)value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="TorqueFilterMode.ApplyEveryBullet"/>.
		/// </remarks>
		public TorqueFilterMode TorqueFilterMode
		{
			set
			{
				SetArgument("torqueFilterMode", (int)value);
			}
		}

		/// <summary>
		/// Always apply torques to spine3 instead of actual part hit.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool TorqueAlwaysSpine3
		{
			set
			{
				SetArgument("torqueAlwaysSpine3", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("counterAfterMagReached", value);
			}
		}

		/// <summary>
		/// Add a counter impulse to the pelvis.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool DoCounterImpulse
		{
			set
			{
				SetArgument("doCounterImpulse", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 100.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("impulseAirOn", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("impulseOneLegOn", value);
			}
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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("rbTwistAxis", (int)value);
			}
		}

		/// <summary>
		/// If false pivot around COM always, if true change pivot depending on foot contact:  to feet center if both feet in contact, or foot position if 1 foot in contact or COM position if no feet in contact.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool RbPivot
		{
			set
			{
				SetArgument("rbPivot", value);
			}
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
			set
			{
				SetArgument("mask", value);
			}
		}

		/// <summary>
		/// If false, disable (set all to PI, -PI) limits.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool Enable
		{
			set
			{
				SetArgument("enable", value);
			}
		}

		/// <summary>
		/// If true, set limits to accommodate current desired angles.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ToDesired
		{
			set
			{
				SetArgument("toDesired", value);
			}
		}

		/// <summary>
		/// Return to cached defaults?.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool Restore
		{
			set
			{
				SetArgument("restore", value);
			}
		}

		/// <summary>
		/// If true, set limits to the current animated limits.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ToCurAnimation
		{
			set
			{
				SetArgument("toCurAnimation", value);
			}
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
				{
					value = -1;
				}

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
				{
					value = 3.1f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 3.1f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 3.1f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 3.1f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 3;
				}

				if (value < 0)
				{
					value = 0;
				}

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
				{
					value = 30.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1.1f;
				}

				if (value < 0.9f)
				{
					value = 0.9f;
				}

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
				{
					value = 6.3f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 1;
				}

				if (value < -1)
				{
					value = -1;
				}

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
			set
			{
				SetArgument("velocityScaled", value);
			}
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
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < -2.0f)
				{
					value = -2.0f;
				}

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
				{
					value = 2.0f;
				}

				if (value < -2.0f)
				{
					value = -2.0f;
				}

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
				{
					value = 0.7f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("forceStep", value);
			}
		}

		/// <summary>
		/// Turn the character using the balancer.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool StepTurn
		{
			set
			{
				SetArgument("stepTurn", value);
			}
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
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 40.0f;
				}

				if (value < 1.0f)
				{
					value = 1.0f;
				}

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
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

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
			set
			{
				SetArgument("velScales", value);
			}
		}
	}

	/// <summary>
	/// This single message allows you to configure the injured leg reaction during shot.
	/// </summary>
	public sealed class ConfigureShotInjuredLegHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureShotInjuredLegHelper for sending a ConfigureShotInjuredLeg <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureShotInjuredLeg <see cref="Message"/> to.</param>
		/// <remarks>
		/// This single message allows you to configure the injured leg reaction during shot.
		/// </remarks>
		public ConfigureShotInjuredLegHelper(Ped ped) : base(ped, "configureShotInjuredLeg")
		{
		}

		/// <summary>
		/// Time before a wounded leg is set to be weak and cause the character to collapse.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float TimeBeforeCollapseWoundLeg
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("timeBeforeCollapseWoundLeg", value);
			}
		}

		/// <summary>
		/// Leg injury duration (reaction to being shot in leg).
		/// </summary>
		/// <remarks>
		/// Default value = 0.4f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float LegInjuryTime
		{
			set
			{
				if (value > 2.0f)
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("legInjuryTime", value);
			}
		}

		/// <summary>
		/// Force a step to be taken whether pushed out of balance or not.
		/// </summary>
		/// <remarks>
		/// Default value = True.
		/// </remarks>
		public bool LegForceStep
		{
			set
			{
				SetArgument("legForceStep", value);
			}
		}

		/// <summary>
		/// Bend the legs via the balancer by this amount if stepping on the injured leg.
		/// 0.2 seems a good default.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LegLimpBend
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("legLimpBend", value);
			}
		}

		/// <summary>
		/// Leg lift duration (reaction to being shot in leg).
		/// (Lifting happens when not stepping with other leg).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float LegLiftTime
		{
			set
			{
				if (value > 2.0f)
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("legLiftTime", value);
			}
		}

		/// <summary>
		/// Leg injury - leg strength is reduced.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LegInjury
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("legInjury", value);
			}
		}

		/// <summary>
		/// Leg injury bend forwards amount when not lifting leg.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LegInjuryHipPitch
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("legInjuryHipPitch", value);
			}
		}

		/// <summary>
		/// Leg injury bend forwards amount when lifting leg.
		/// (Lifting happens when not stepping with other leg).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LegInjuryLiftHipPitch
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("legInjuryLiftHipPitch", value);
			}
		}

		/// <summary>
		/// Leg injury bend forwards amount when not lifting leg.
		/// </summary>
		/// <remarks>
		/// Default value = 0.1f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LegInjurySpineBend
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("legInjurySpineBend", value);
			}
		}

		/// <summary>
		/// Leg injury bend forwards amount when lifting leg.
		/// (Lifting happens when not stepping with other leg).
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LegInjuryLiftSpineBend
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("legInjuryLiftSpineBend", value);
			}
		}
	}

	public sealed class DefineAttachedObjectHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the DefineAttachedObjectHelper for sending a DefineAttachedObject <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the DefineAttachedObject <see cref="Message"/> to.</param>
		public DefineAttachedObjectHelper(Ped ped) : base(ped, "defineAttachedObject")
		{
		}

		/// <summary>
		/// Index of part to attach to.
		/// </summary>
		/// <remarks>
		/// Default value = -1.
		/// Min value = -1.
		/// Max value = 21.
		/// </remarks>
		public int PartIndex
		{
			set
			{
				if (value > 21)
				{
					value = 21;
				}

				if (value < -1)
				{
					value = -1;
				}

				SetArgument("partIndex", value);
			}
		}

		/// <summary>
		/// Mass of the attached object.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float ObjectMass
		{
			set
			{
				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("objectMass", value);
			}
		}

		/// <summary>
		/// World position of attached object's center of mass. Must be updated each frame.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 WorldPos
		{
			set
			{
				SetArgument("worldPos", value);
			}
		}
	}

	/// <summary>
	/// Apply an impulse to a named body part.
	/// </summary>
	public sealed class ForceToBodyPartHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ForceToBodyPartHelper for sending a ForceToBodyPart <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ForceToBodyPart <see cref="Message"/> to.</param>
		/// <remarks>
		/// Apply an impulse to a named body part.
		/// </remarks>
		public ForceToBodyPartHelper(Ped ped) : base(ped, "forceToBodyPart")
		{
		}

		/// <summary>
		/// Part or link or bound index.
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
				{
					value = 28;
				}

				if (value < 0)
				{
					value = 0;
				}

				SetArgument("partIndex", value);
			}
		}

		/// <summary>
		/// Force to apply.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, -50.0f, 0.0f).
		/// Min value = -100000.0f.
		/// Max value = 100000.0f.
		/// </remarks>
		public Vector3 Force
		{
			set
			{
				SetArgument("force",
					Vector3.Clamp(value, new Vector3(-100000.0f, -100000.0f, -100000.0f), new Vector3(100000.0f, 100000.0f, 100000.0f)));
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ForceDefinedInPartSpace
		{
			set
			{
				SetArgument("forceDefinedInPartSpace", value);
			}
		}
	}

	public sealed class LeanInDirectionHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the LeanInDirectionHelper for sending a LeanInDirection <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the LeanInDirection <see cref="Message"/> to.</param>
		public LeanInDirectionHelper(Ped ped) : base(ped, "leanInDirection")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Direction to lean in.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 1.0f).
		/// Min value = 0.0f.
		/// </remarks>
		public Vector3 Dir
		{
			set
			{
				SetArgument("dir", Vector3.Maximize(value, new Vector3(0.0f, 0.0f, 0.0f)));
			}
		}
	}

	public sealed class LeanRandomHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the LeanRandomHelper for sending a LeanRandom <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the LeanRandom <see cref="Message"/> to.</param>
		public LeanRandomHelper(Ped ped) : base(ped, "leanRandom")
		{
		}

		/// <summary>
		/// Minimum amount of lean.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmountMin
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("leanAmountMin", value);
			}
		}

		/// <summary>
		/// Maximum amount of lean.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmountMax
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("leanAmountMax", value);
			}
		}

		/// <summary>
		/// Minimum time until changing direction.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float ChangeTimeMin
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("changeTimeMin", value);
			}
		}

		/// <summary>
		/// Maximum time until changing direction.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float ChangeTimeMax
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("changeTimeMax", value);
			}
		}
	}

	public sealed class LeanToPositionHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the LeanToPositionHelper for sending a LeanToPosition <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the LeanToPosition <see cref="Message"/> to.</param>
		public LeanToPositionHelper(Ped ped) : base(ped, "leanToPosition")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -0.5f.
		/// Max value = 0.5f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 0.5f)
				{
					value = 0.5f;
				}

				if (value < -0.5f)
				{
					value = -0.5f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Position to head towards.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 Pos
		{
			set
			{
				SetArgument("pos", value);
			}
		}
	}

	public sealed class LeanTowardsObjectHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the LeanTowardsObjectHelper for sending a LeanTowardsObject <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the LeanTowardsObject <see cref="Message"/> to.</param>
		public LeanTowardsObjectHelper(Ped ped) : base(ped, "leanTowardsObject")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -0.5f.
		/// Max value = 0.5f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 0.5f)
				{
					value = 0.5f;
				}

				if (value < -0.5f)
				{
					value = -0.5f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Offset from instance position added when calculating position to lean to.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// Min value = -100.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public Vector3 Offset
		{
			set
			{
				SetArgument("offset",
					Vector3.Clamp(value, new Vector3(-100.0f, -100.0f, -100.0f), new Vector3(100.0f, 100.0f, 100.0f)));
			}
		}

		/// <summary>
		/// LevelIndex of object to lean towards.
		/// </summary>
		/// <remarks>
		/// Default value = -1.
		/// Min value = -1.
		/// </remarks>
		public int InstanceIndex
		{
			set
			{
				if (value < -1)
				{
					value = -1;
				}

				SetArgument("instanceIndex", value);
			}
		}

		/// <summary>
		/// BoundIndex of object to lean towards (0 = just use instance coordinates).
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// </remarks>
		public int BoundIndex
		{
			set
			{
				if (value < 0)
				{
					value = 0;
				}

				SetArgument("boundIndex", value);
			}
		}
	}

	public sealed class HipsLeanInDirectionHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the HipsLeanInDirectionHelper for sending a HipsLeanInDirection <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the HipsLeanInDirection <see cref="Message"/> to.</param>
		public HipsLeanInDirectionHelper(Ped ped) : base(ped, "hipsLeanInDirection")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Direction to lean in.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 1.0f).
		/// Min value = 0.0f.
		/// </remarks>
		public Vector3 Dir
		{
			set
			{
				SetArgument("dir", Vector3.Maximize(value, new Vector3(0.0f, 0.0f, 0.0f)));
			}
		}
	}

	public sealed class HipsLeanRandomHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the HipsLeanRandomHelper for sending a HipsLeanRandom <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the HipsLeanRandom <see cref="Message"/> to.</param>
		public HipsLeanRandomHelper(Ped ped) : base(ped, "hipsLeanRandom")
		{
		}

		/// <summary>
		/// Minimum amount of lean.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmountMin
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("leanAmountMin", value);
			}
		}

		/// <summary>
		/// Maximum amount of lean.
		/// </summary>
		/// <remarks>
		/// Default value = 0.4f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmountMax
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("leanAmountMax", value);
			}
		}

		/// <summary>
		/// Min time until changing direction.
		/// </summary>
		/// <remarks>
		/// Default value = 2.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float ChangeTimeMin
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("changeTimeMin", value);
			}
		}

		/// <summary>
		/// Maximum time until changing direction.
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float ChangeTimeMax
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("changeTimeMax", value);
			}
		}
	}

	public sealed class HipsLeanToPositionHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the HipsLeanToPositionHelper for sending a HipsLeanToPosition <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the HipsLeanToPosition <see cref="Message"/> to.</param>
		public HipsLeanToPositionHelper(Ped ped) : base(ped, "hipsLeanToPosition")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -0.5f.
		/// Max value = 0.5f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 0.5f)
				{
					value = 0.5f;
				}

				if (value < -0.5f)
				{
					value = -0.5f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Position to head towards.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 Pos
		{
			set
			{
				SetArgument("pos", value);
			}
		}
	}

	public sealed class HipsLeanTowardsObjectHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the HipsLeanTowardsObjectHelper for sending a HipsLeanTowardsObject <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the HipsLeanTowardsObject <see cref="Message"/> to.</param>
		public HipsLeanTowardsObjectHelper(Ped ped) : base(ped, "hipsLeanTowardsObject")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -0.5f.
		/// Max value = 0.5f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 0.5f)
				{
					value = 0.5f;
				}

				if (value < -0.5f)
				{
					value = -0.5f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Offset from instance position added when calculating position to lean to.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// Min value = -100.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public Vector3 Offset
		{
			set
			{
				SetArgument("offset",
					Vector3.Clamp(value, new Vector3(-100.0f, -100.0f, -100.0f), new Vector3(100.0f, 100.0f, 100.0f)));
			}
		}

		/// <summary>
		/// LevelIndex of object to lean hips towards.
		/// </summary>
		/// <remarks>
		/// Default value = -1.
		/// Min value = -1.
		/// </remarks>
		public int InstanceIndex
		{
			set
			{
				if (value < -1)
				{
					value = -1;
				}

				SetArgument("instanceIndex", value);
			}
		}

		/// <summary>
		/// BoundIndex of object to lean hips towards (0 = just use instance coordinates).
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// </remarks>
		public int BoundIndex
		{
			set
			{
				if (value < 0)
				{
					value = 0;
				}

				SetArgument("boundIndex", value);
			}
		}
	}

	public sealed class ForceLeanInDirectionHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ForceLeanInDirectionHelper for sending a ForceLeanInDirection <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ForceLeanInDirection <see cref="Message"/> to.</param>
		public ForceLeanInDirectionHelper(Ped ped) : base(ped, "forceLeanInDirection")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -1.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Direction to lean in.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 1.0f).
		/// Min value = 0.0f.
		/// </remarks>
		public Vector3 Dir
		{
			set
			{
				SetArgument("dir", Vector3.Maximize(value, new Vector3(0.0f, 0.0f, 0.0f)));
			}
		}

		/// <summary>
		/// Body part that the force is applied to.
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// Max value = 21.
		/// </remarks>
		public int BodyPart
		{
			set
			{
				if (value > 21)
				{
					value = 21;
				}

				if (value < 0)
				{
					value = 0;
				}

				SetArgument("bodyPart", value);
			}
		}
	}

	public sealed class ForceLeanRandomHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ForceLeanRandomHelper for sending a ForceLeanRandom <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ForceLeanRandom <see cref="Message"/> to.</param>
		public ForceLeanRandomHelper(Ped ped) : base(ped, "forceLeanRandom")
		{
		}

		/// <summary>
		/// Minimum amount of lean.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmountMin
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("leanAmountMin", value);
			}
		}

		/// <summary>
		/// Maximum amount of lean.
		/// </summary>
		/// <remarks>
		/// Default value = 0.4f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LeanAmountMax
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("leanAmountMax", value);
			}
		}

		/// <summary>
		/// Min time until changing direction.
		/// </summary>
		/// <remarks>
		/// Default value = 2.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float ChangeTimeMin
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("changeTimeMin", value);
			}
		}

		/// <summary>
		/// Maximum time until changing direction.
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float ChangeTimeMax
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("changeTimeMax", value);
			}
		}

		/// <summary>
		/// Body part that the force is applied to.
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// Max value = 21.
		/// </remarks>
		public int BodyPart
		{
			set
			{
				if (value > 21)
				{
					value = 21;
				}

				if (value < 0)
				{
					value = 0;
				}

				SetArgument("bodyPart", value);
			}
		}
	}

	public sealed class ForceLeanToPositionHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ForceLeanToPositionHelper for sending a ForceLeanToPosition <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ForceLeanToPosition <see cref="Message"/> to.</param>
		public ForceLeanToPositionHelper(Ped ped) : base(ped, "forceLeanToPosition")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -0.5f.
		/// Max value = 0.5f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 0.5f)
				{
					value = 0.5f;
				}

				if (value < -0.5f)
				{
					value = -0.5f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Position to head towards.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 Pos
		{
			set
			{
				SetArgument("pos", value);
			}
		}

		/// <summary>
		/// Body part that the force is applied to.
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// Max value = 21.
		/// </remarks>
		public int BodyPart
		{
			set
			{
				if (value > 21)
				{
					value = 21;
				}

				if (value < 0)
				{
					value = 0;
				}

				SetArgument("bodyPart", value);
			}
		}
	}

	public sealed class ForceLeanTowardsObjectHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ForceLeanTowardsObjectHelper for sending a ForceLeanTowardsObject <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ForceLeanTowardsObject <see cref="Message"/> to.</param>
		public ForceLeanTowardsObjectHelper(Ped ped) : base(ped, "forceLeanTowardsObject")
		{
		}

		/// <summary>
		/// Amount of lean, 0 to about 0.5. -ve will move away from the target.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = -0.5f.
		/// Max value = 0.5f.
		/// </remarks>
		public float LeanAmount
		{
			set
			{
				if (value > 0.5f)
				{
					value = 0.5f;
				}

				if (value < -0.5f)
				{
					value = -0.5f;
				}

				SetArgument("leanAmount", value);
			}
		}

		/// <summary>
		/// Offset from instance position added when calculating position to lean to.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// Min value = -100.0f.
		/// Max value = 100.0f.
		/// </remarks>
		public Vector3 Offset
		{
			set
			{
				SetArgument("offset",
					Vector3.Clamp(value, new Vector3(-100.0f, -100.0f, -100.0f), new Vector3(100.0f, 100.0f, 100.0f)));
			}
		}

		/// <summary>
		/// LevelIndex of object to move towards.
		/// </summary>
		/// <remarks>
		/// Default value = -1.
		/// Min value = -1.
		/// </remarks>
		public int InstanceIndex
		{
			set
			{
				if (value < -1)
				{
					value = -1;
				}

				SetArgument("instanceIndex", value);
			}
		}

		/// <summary>
		/// BoundIndex of object to move towards (0 = just use instance coordinates).
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// </remarks>
		public int BoundIndex
		{
			set
			{
				if (value < 0)
				{
					value = 0;
				}

				SetArgument("boundIndex", value);
			}
		}

		/// <summary>
		/// Body part that the force is applied to.
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// Max value = 21.
		/// </remarks>
		public int BodyPart
		{
			set
			{
				if (value > 21)
				{
					value = 21;
				}

				if (value < 0)
				{
					value = 0;
				}

				SetArgument("bodyPart", value);
			}
		}
	}

	/// <summary>
	/// Use this message to manually set the body stiffness values -before using Active Pose to drive to an animated pose, for example.
	/// </summary>
	public sealed class SetStiffnessHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the SetStiffnessHelper for sending a SetStiffness <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the SetStiffness <see cref="Message"/> to.</param>
		/// <remarks>
		/// Use this message to manually set the body stiffness values -before using Active Pose to drive to an animated pose, for example.
		/// </remarks>
		public SetStiffnessHelper(Ped ped) : base(ped, "setStiffness")
		{
		}

		/// <summary>
		/// Stiffness of whole character.
		/// </summary>
		/// <remarks>
		/// Default value = 12.0f.
		/// Min value = 2.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float BodyStiffness
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 2.0f)
				{
					value = 2.0f;
				}

				SetArgument("bodyStiffness", value);
			}
		}

		/// <summary>
		/// Damping amount, less is underdamped.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 3.0f.
		/// </remarks>
		public float Damping
		{
			set
			{
				if (value > 3.0f)
				{
					value = 3.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("damping", value);
			}
		}

		/// <summary>
		/// Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
		/// </summary>
		/// <remarks>
		/// Default value = "fb".
		/// </remarks>
		public string Mask
		{
			set
			{
				SetArgument("mask", value);
			}
		}
	}

	/// <summary>
	/// Use this message to manually set the muscle stiffness values -before using Active Pose to drive to an animated pose, for example.
	/// </summary>
	public sealed class SetMuscleStiffnessHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the SetMuscleStiffnessHelper for sending a SetMuscle stiffness <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the SetMuscle stiffness <see cref="Message"/> to.</param>
		/// <remarks>
		/// Use this message to manually set the muscle stiffness values -before using Active Pose to drive to an animated pose, for example.
		/// </remarks>
		public SetMuscleStiffnessHelper(Ped ped) : base(ped, "setMuscleStiffness")
		{
		}

		/// <summary>
		/// Muscle stiffness of joint/s.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float MuscleStiffness
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("muscleStiffness", value);
			}
		}

		/// <summary>
		/// Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
		/// </summary>
		/// <remarks>
		/// Default value = "fb".
		/// </remarks>
		public string Mask
		{
			set
			{
				SetArgument("mask", value);
			}
		}
	}

	/// <summary>
	/// Use this message to set the character's weapon mode.  This is an alternativeto the setWeaponMode public function.
	/// </summary>
	public sealed class SetWeaponModeHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the SetWeaponModeHelper for sending a SetWeaponMode <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the SetWeaponMode <see cref="Message"/> to.</param>
		/// <remarks>
		/// Use this message to set the character's weapon mode.  This is an alternativeto the setWeaponMode public function.
		/// </remarks>
		public SetWeaponModeHelper(Ped ped) : base(ped, "setWeaponMode")
		{
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="WeaponMode.PistolRight"/>.
		/// </remarks>.
		public WeaponMode WeaponMode
		{
			set
			{
				SetArgument("weaponMode", (int)value);
			}
		}
	}

	/// <summary>
	/// Use this message to register weapon.  This is an alternativeto the registerWeapon public function.
	/// </summary>
	public sealed class RegisterWeaponHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the RegisterWeaponHelper for sending a RegisterWeapon <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the RegisterWeapon <see cref="Message"/> to.</param>
		/// <remarks>
		/// Use this message to register weapon.  This is an alternativeto the registerWeapon public function.
		/// </remarks>
		public RegisterWeaponHelper(Ped ped) : base(ped, "registerWeapon")
		{
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="Hand.Right"/>.
		/// </remarks>
		public Hand Hand
		{
			set
			{
				SetArgument("hand", (int)value);
			}
		}

		/// <summary>
		/// Level index of the weapon.
		/// </summary>
		/// <remarks>
		/// Default value = -1.
		/// Min value = -1.
		/// </remarks>
		public int LevelIndex
		{
			set
			{
				if (value < -1)
				{
					value = -1;
				}

				SetArgument("levelIndex", value);
			}
		}

		/// <summary>
		/// Pointer to the hand-gun constraint handle.
		/// </summary>
		/// <remarks>
		/// Default value = -1.
		/// Min value = -1.
		/// </remarks>
		public int ConstraintHandle
		{
			set
			{
				if (value < -1)
				{
					value = -1;
				}

				SetArgument("constraintHandle", value);
			}
		}

		/// <summary>
		/// A vector of the gunToHand matrix.  The gunToHandMatrix is the desired gunToHandMatrix in the aimingPose. (The gunToHandMatrix when pointGun starts can be different so will be blended to this desired one).
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(1.0f, 0.0f, 0.0f).
		/// Min value = 0.0f.
		/// </remarks>
		public Vector3 GunToHandA
		{
			set
			{
				SetArgument("gunToHandA", Vector3.Maximize(value, new Vector3(0.0f, 0.0f, 0.0f)));
			}
		}

		/// <summary>
		/// B vector of the gunToHand matrix.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 1.0f, 0.0f).
		/// Min value = 0.0f.
		/// </remarks>
		public Vector3 GunToHandB
		{
			set
			{
				SetArgument("gunToHandB", Vector3.Maximize(value, new Vector3(0.0f, 0.0f, 0.0f)));
			}
		}

		/// <summary>
		/// C vector of the gunToHand matrix.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 1.0f).
		/// Min value = 0.0f.
		/// </remarks>
		public Vector3 GunToHandC
		{
			set
			{
				SetArgument("gunToHandC", Vector3.Maximize(value, new Vector3(0.0f, 0.0f, 0.0f)));
			}
		}

		/// <summary>
		/// D vector of the gunToHand matrix.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// Min value = 0.0f.
		/// </remarks>
		public Vector3 GunToHandD
		{
			set
			{
				SetArgument("gunToHandD", Vector3.Maximize(value, new Vector3(0.0f, 0.0f, 0.0f)));
			}
		}

		/// <summary>
		/// Gun center to muzzle expressed in gun co-ordinates.  To get the line of sight/barrel of the gun. Assumption: the muzzle direction is always along the same primary axis of the gun.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 GunToMuzzleInGun
		{
			set
			{
				SetArgument("gunToMuzzleInGun", value);
			}
		}

		/// <summary>
		/// Gun center to butt expressed in gun co-ordinates.  The gun pivots around this point when aiming.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 GunToButtInGun
		{
			set
			{
				SetArgument("gunToButtInGun", value);
			}
		}
	}

	public sealed class ShotRelaxHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ShotRelaxHelper for sending a ShotRelax <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ShotRelax <see cref="Message"/> to.</param>
		public ShotRelaxHelper(Ped ped) : base(ped, "shotRelax")
		{
		}

		/// <summary>
		/// Time over which to relax to full relaxation for upper body.
		/// </summary>
		/// <remarks>
		/// Default value = 2.0f.
		/// Min value = 0.0f.
		/// Max value = 40.0f.
		/// </remarks>
		public float RelaxPeriodUpper
		{
			set
			{
				if (value > 40.0f)
				{
					value = 40.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("relaxPeriodUpper", value);
			}
		}

		/// <summary>
		/// Time over which to relax to full relaxation for lower body.
		/// </summary>
		/// <remarks>
		/// Default value = 0.4f.
		/// Min value = 0.0f.
		/// Max value = 40.0f.
		/// </remarks>
		public float RelaxPeriodLower
		{
			set
			{
				if (value > 40.0f)
				{
					value = 40.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("relaxPeriodLower", value);
			}
		}
	}

	/// <summary>
	/// One shot message apply a force to the hand as we fire the gun that should be in this hand.
	/// </summary>
	public sealed class FireWeaponHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the FireWeaponHelper for sending a FireWeapon <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the FireWeapon <see cref="Message"/> to.</param>
		/// <remarks>
		/// One shot message apply a force to the hand as we fire the gun that should be in this hand.
		/// </remarks>
		public FireWeaponHelper(Ped ped) : base(ped, "fireWeapon")
		{
		}

		/// <summary>
		/// The force of the gun.
		/// </summary>
		/// <remarks>
		/// Default value = 1000.0f.
		/// Min value = 0.0f.
		/// Max value = 10000.0f.
		/// </remarks>
		public float FiredWeaponStrength
		{
			set
			{
				if (value > 10000.0f)
				{
					value = 10000.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("firedWeaponStrength", value);
			}
		}

		/// <summary>
		/// Which hand is the gun in.
		/// </summary>
		/// <remarks>
		/// Default value = <see cref="Hand.Left"/>.
		/// </remarks>
		public Hand GunHandEnum
		{
			set
			{
				SetArgument("gunHandEnum", (int)value);
			}
		}

		/// <summary>
		/// Should we apply some of the force at the shoulder. Force double handed weapons (Ak47 etc).
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ApplyFireGunForceAtClavicle
		{
			set
			{
				SetArgument("applyFireGunForceAtClavicle", value);
			}
		}

		/// <summary>
		/// Minimum time before next fire impulse.
		/// </summary>
		/// <remarks>
		/// Default value = 0.4f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float InhibitTime
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("inhibitTime", value);
			}
		}

		/// <summary>
		/// Direction of impulse in gun frame.
		/// </summary>
		/// <remarks>
		/// Default value = Vector3(0.0f, 0.0f, 0.0f).
		/// </remarks>
		public Vector3 Direction
		{
			set
			{
				SetArgument("direction", value);
			}
		}

		/// <summary>
		/// Split force between hand and clavicle when applyFireGunForceAtClavicle is true. 1 = all hand, 0 = all clavicle.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float Split
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("split", value);
			}
		}
	}

	/// <summary>
	/// One shot to give state of constraints on character and response to constraints.
	/// </summary>
	public sealed class ConfigureConstraintsHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the ConfigureConstraintsHelper for sending a ConfigureConstraints <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the ConfigureConstraints <see cref="Message"/> to.</param>
		/// <remarks>
		/// One shot to give state of constraints on character and response to constraints.
		/// </remarks>
		public ConfigureConstraintsHelper(Ped ped) : base(ped, "configureConstraints")
		{
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool HandCuffs
		{
			set
			{
				SetArgument("handCuffs", value);
			}
		}

		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool HandCuffsBehindBack
		{
			set
			{
				SetArgument("handCuffsBehindBack", value);
			}
		}

		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LegCuffs
		{
			set
			{
				SetArgument("legCuffs", value);
			}
		}

		/// <summary>
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool RightDominant
		{
			set
			{
				SetArgument("rightDominant", value);
			}
		}

		/// <summary>
		/// 0 setCurrent, 1= IK to dominant, (2=pointGunLikeIK //not implemented).
		/// </summary>
		/// <remarks>
		/// Default value = 0.
		/// Min value = 0.
		/// Max value = 5.
		/// </remarks>
		public int PassiveMode
		{
			set
			{
				if (value > 5)
				{
					value = 5;
				}

				if (value < 0)
				{
					value = 0;
				}

				SetArgument("passiveMode", value);
			}
		}

		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool BespokeBehavior
		{
			set
			{
				SetArgument("bespokeBehaviour", value);
			}
		}

		/// <summary>
		/// Blend Arms to zero pose.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float Blend2ZeroPose
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("blend2ZeroPose", value);
			}
		}
	}

	public sealed class StayUprightHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the StayUprightHelper for sending a StayUpright <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the StayUpright <see cref="Message"/> to.</param>
		public StayUprightHelper(Ped ped) : base(ped, "stayUpright")
		{
		}

		/// <summary>
		/// Enable force based constraint.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool UseForces
		{
			set
			{
				SetArgument("useForces", value);
			}
		}

		/// <summary>
		/// Enable torque based constraint.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool UseTorques
		{
			set
			{
				SetArgument("useTorques", value);
			}
		}

		/// <summary>
		/// Uses position/orientation control on the spine and drifts in the direction of bullets.  This ignores all other stayUpright settings.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool LastStandMode
		{
			set
			{
				SetArgument("lastStandMode", value);
			}
		}

		/// <summary>
		/// The sink rate (higher for a faster drop).
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LastStandSinkRate
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("lastStandSinkRate", value);
			}
		}

		/// <summary>
		/// Higher values for more damping.
		/// </summary>
		/// <remarks>
		/// Default value = 0.4f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float LastStandHorizDamping
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("lastStandHorizDamping", value);
			}
		}

		/// <summary>
		/// Max time allowed in last stand mode.
		/// </summary>
		/// <remarks>
		/// Default value = 0.4f.
		/// Min value = 0.0f.
		/// Max value = 5.0f.
		/// </remarks>
		public float LastStandMaxTime
		{
			set
			{
				if (value > 5.0f)
				{
					value = 5.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("lastStandMaxTime", value);
			}
		}

		/// <summary>
		/// Use cheat torques to face the direction of bullets if not facing too far away.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool TurnTowardsBullets
		{
			set
			{
				SetArgument("turnTowardsBullets", value);
			}
		}

		/// <summary>
		/// Make strength of constraint function of COM velocity.  Uses -1 for forceDamping if the damping is positive.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool VelocityBased
		{
			set
			{
				SetArgument("velocityBased", value);
			}
		}

		/// <summary>
		/// Only apply torque based constraint when airBorne.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool TorqueOnlyInAir
		{
			set
			{
				SetArgument("torqueOnlyInAir", value);
			}
		}

		/// <summary>
		/// Strength of constraint.
		/// </summary>
		/// <remarks>
		/// Default value = 3.0f.
		/// Min value = 0.0f.
		/// Max value = 16.0f.
		/// </remarks>
		public float ForceStrength
		{
			set
			{
				if (value > 16.0f)
				{
					value = 16.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("forceStrength", value);
			}
		}

		/// <summary>
		/// Damping in constraint: -1 makes it scale automagically with forceStrength.  Other negative values will scale this automagic damping.
		/// </summary>
		/// <remarks>
		/// Default value = -1.0f.
		/// Min value = -1.0f.
		/// Max value = 50.0f.
		/// </remarks>
		public float ForceDamping
		{
			set
			{
				if (value > 50.0f)
				{
					value = 50.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("forceDamping", value);
			}
		}

		/// <summary>
		/// Multiplier to the force applied to the feet.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ForceFeetMult
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("forceFeetMult", value);
			}
		}

		/// <summary>
		/// Share of pelvis force applied to spine3.
		/// </summary>
		/// <remarks>
		/// Default value = 0.3f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ForceSpine3Share
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("forceSpine3Share", value);
			}
		}

		/// <summary>
		/// How much the character lean is taken into account when reducing the force.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ForceLeanReduction
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("forceLeanReduction", value);
			}
		}

		/// <summary>
		/// Share of the feet force to the airborne foot.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ForceInAirShare
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("forceInAirShare", value);
			}
		}

		/// <summary>
		/// When min and max are greater than 0 the constraint strength is determined from character strength, scaled into the range given by min and max.
		/// </summary>
		/// <remarks>
		/// Default value = -1.0f.
		/// Min value = -1.0f.
		/// Max value = 16.0f.
		/// </remarks>
		public float ForceMin
		{
			set
			{
				if (value > 16.0f)
				{
					value = 16.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("forceMin", value);
			}
		}

		/// <summary>
		/// See above.
		/// </summary>
		/// <remarks>
		/// Default value = -1.0f.
		/// Min value = -1.0f.
		/// Max value = 16.0f.
		/// </remarks>
		public float ForceMax
		{
			set
			{
				if (value > 16.0f)
				{
					value = 16.0f;
				}

				if (value < -1.0f)
				{
					value = -1.0f;
				}

				SetArgument("forceMax", value);
			}
		}

		/// <summary>
		/// When in velocityBased mode, the COM velocity at which constraint reaches maximum strength (forceStrength).
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.1f.
		/// Max value = 10.0f.
		/// </remarks>
		public float ForceSaturationVel
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.1f)
				{
					value = 0.1f;
				}

				SetArgument("forceSaturationVel", value);
			}
		}

		/// <summary>
		/// When in velocityBased mode, the COM velocity above which constraint starts applying forces.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 5.0f.
		/// </remarks>
		public float ForceThresholdVel
		{
			set
			{
				if (value > 5.0f)
				{
					value = 5.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("forceThresholdVel", value);
			}
		}

		/// <summary>
		/// Strength of torque based constraint.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 16.0f.
		/// </remarks>
		public float TorqueStrength
		{
			set
			{
				if (value > 16.0f)
				{
					value = 16.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("torqueStrength", value);
			}
		}

		/// <summary>
		/// Damping of torque based constraint.
		/// </summary>
		/// <remarks>
		/// Default value = 0.5f.
		/// Min value = 0.0f.
		/// Max value = 16.0f.
		/// </remarks>
		public float TorqueDamping
		{
			set
			{
				if (value > 16.0f)
				{
					value = 16.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("torqueDamping", value);
			}
		}

		/// <summary>
		/// When in velocityBased mode, the COM velocity at which constraint reaches maximum strength (torqueStrength).
		/// </summary>
		/// <remarks>
		/// Default value = 4.0f.
		/// Min value = 0.1f.
		/// Max value = 10.0f.
		/// </remarks>
		public float TorqueSaturationVel
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.1f)
				{
					value = 0.1f;
				}

				SetArgument("torqueSaturationVel", value);
			}
		}

		/// <summary>
		/// When in velocityBased mode, the COM velocity above which constraint starts applying torques.
		/// </summary>
		/// <remarks>
		/// Default value = 2.5f.
		/// Min value = 0.0f.
		/// Max value = 5.0f.
		/// </remarks>
		public float TorqueThresholdVel
		{
			set
			{
				if (value > 5.0f)
				{
					value = 5.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("torqueThresholdVel", value);
			}
		}

		/// <summary>
		/// Distance the foot is behind Com projection that is still considered able to generate the support for the upright constraint.
		/// </summary>
		/// <remarks>
		/// Default value = 2.0f.
		/// Min value = -2.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float SupportPosition
		{
			set
			{
				if (value > 2.0f)
				{
					value = 2.0f;
				}

				if (value < -2.0f)
				{
					value = -2.0f;
				}

				SetArgument("supportPosition", value);
			}
		}

		/// <summary>
		/// Still apply this fraction of the upright constaint force if the foot is not in a position (defined by supportPosition) to generate the support for the upright constraint.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float NoSupportForceMult
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("noSupportForceMult", value);
			}
		}

		/// <summary>
		/// Strength of cheat force applied upwards to spine3 to help the character up steps/slopes.
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 16.0f.
		/// </remarks>
		public float StepUpHelp
		{
			set
			{
				if (value > 16.0f)
				{
					value = 16.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("stepUpHelp", value);
			}
		}

		/// <summary>
		/// How much the cheat force takes into account the acceleration of moving platforms.
		/// </summary>
		/// <remarks>
		/// Default value = 0.7f.
		/// Min value = 0.0f.
		/// Max value = 2.0f.
		/// </remarks>
		public float StayUpAcc
		{
			set
			{
				if (value > 2.0f)
				{
					value = 2.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("stayUpAcc", value);
			}
		}

		/// <summary>
		/// The maximum floorAcceleration (of a moving platform) that the cheat force takes into account.
		/// </summary>
		/// <remarks>
		/// Default value = 5.0f.
		/// Min value = 0.0f.
		/// Max value = 15.0f.
		/// </remarks>
		public float StayUpAccMax
		{
			set
			{
				if (value > 15.0f)
				{
					value = 15.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("stayUpAccMax", value);
			}
		}
	}

	/// <summary>
	/// Send this message to immediately stop all behaviors from executing.
	/// </summary>
	public sealed class StopAllBehaviorsHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the StopAllBehaviorsHelper for sending a StopAllBehaviors <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the StopAllBehaviors <see cref="Message"/> to.</param>
		/// <remarks>
		/// Send this message to immediately stop all behaviors from executing.
		/// </remarks>
		public StopAllBehaviorsHelper(Ped ped) : base(ped, "stopAllBehaviours")
		{
		}
	}

	/// <summary>
	/// Sets character's strength on the dead-granny-to-healthy-terminator scale: [0..1].
	/// </summary>
	public sealed class SetCharacterStrengthHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the SetCharacterStrengthHelper for sending a SetCharacterStrength <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the SetCharacterStrength <see cref="Message"/> to.</param>
		/// <remarks>
		/// Sets character's strength on the dead-granny-to-healthy-terminator scale: [0..1].
		/// </remarks>
		public SetCharacterStrengthHelper(Ped ped) : base(ped, "setCharacterStrength")
		{
		}

		/// <summary>
		/// Strength of character.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float CharacterStrength
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("characterStrength", value);
			}
		}
	}

	/// <summary>
	/// Sets character's health on the dead-to-alive scale: [0..1].
	/// </summary>
	public sealed class SetCharacterHealthHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the SetCharacterHealthHelper for sending a SetCharacterHealth <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the SetCharacterHealth <see cref="Message"/> to.</param>
		/// <remarks>
		/// Sets character's health on the dead-to-alive scale: [0..1].
		/// </remarks>
		public SetCharacterHealthHelper(Ped ped) : base(ped, "setCharacterHealth")
		{
		}

		/// <summary>
		/// Health of character.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 1.0f.
		/// </remarks>
		public float CharacterHealth
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("characterHealth", value);
			}
		}
	}

	/// <summary>
	/// Sets the type of reaction if catchFall is called.
	/// </summary>
	public sealed class SetFallingReactionHelper : CustomHelper
	{
		/// <summary>
		/// Creates a new Instance of the SetFallingReactionHelper for sending a SetFallingReaction <see cref="Message"/> to a given <see cref="Ped"/>.
		/// </summary>
		/// <param name="ped">The <see cref="Ped"/> to send the SetFallingReaction <see cref="Message"/> to.</param>
		/// <remarks>
		/// Sets the type of reaction if catchFall is called.
		/// </remarks>
		public SetFallingReactionHelper(Ped ped) : base(ped, "setFallingReaction")
		{
		}

		/// <summary>
		/// Set to true to get handsAndKnees catchFall if catchFall called. If true allows the dynBalancer to stay on during the catchfall and modifies the catch fall to give a more alive looking performance (hands and knees for front landing or sitting up for back landing).
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool HandsAndKnees
		{
			set
			{
				SetArgument("handsAndKnees", value);
			}
		}

		/// <summary>
		/// If true catchFall will call rollDownstairs if comVel GT comVelRDSThresh - prevents excessive sliding in catchFall.  Was previously only true for handsAndKnees.
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool CallRDS
		{
			set
			{
				SetArgument("callRDS", value);
			}
		}

		/// <summary>
		/// ComVel above which rollDownstairs will start - prevents excessive sliding in catchFall.
		/// </summary>
		/// <remarks>
		/// Default value = 2.0f.
		/// Min value = 0.0f.
		/// Max value = 20.0f.
		/// </remarks>
		public float ComVelRDSThresh
		{
			set
			{
				if (value > 20.0f)
				{
					value = 20.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("comVelRDSThresh", value);
			}
		}

		/// <summary>
		/// For rds catchFall only: True to resist rolling motion (rolling motion is set off by ub contact and a sliding velocity), false to allow more of a continuous rolling  (rolling motion is set off at a sliding velocity).
		/// </summary>
		/// <remarks>
		/// Default value = False.
		/// </remarks>
		public bool ResistRolling
		{
			set
			{
				SetArgument("resistRolling", value);
			}
		}

		/// <summary>
		/// Strength is reduced in the catchFall when the arms contact the ground.  0.2 is good for handsAndKnees.  2.5 is good for normal catchFall, anything lower than 1.0 for normal catchFall may lead to bad catchFall poses.
		/// </summary>
		/// <remarks>
		/// Default value = 2.5f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float ArmReduceSpeed
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("armReduceSpeed", value);
			}
		}

		/// <summary>
		/// Reach length multiplier that scales characters arm topological length, value in range from (0, 1 GT  where 1.0 means reach length is maximum.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.3f.
		/// Max value = 1.0f.
		/// </remarks>
		public float ReachLengthMultiplier
		{
			set
			{
				if (value > 1.0f)
				{
					value = 1.0f;
				}

				if (value < 0.3f)
				{
					value = 0.3f;
				}

				SetArgument("reachLengthMultiplier", value);
			}
		}

		/// <summary>
		/// Time after hitting ground that the catchFall can call rds.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float InhibitRollingTime
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("inhibitRollingTime", value);
			}
		}

		/// <summary>
		/// Time after hitting ground that the catchFall can change the friction of parts to inhibit sliding.
		/// </summary>
		/// <remarks>
		/// Default value = 0.2f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float ChangeFrictionTime
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("changeFrictionTime", value);
			}
		}

		/// <summary>
		/// 8.0 was used on yanked) Friction multiplier on body parts when on ground.  Character can look too slidy with groundFriction = 1.  Higher values give a more jerky reaction but this seems timestep dependent especially for dragged by the feet.
		/// </summary>
		/// <remarks>
		/// Default value = 1.0f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float GroundFriction
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("groundFriction", value);
			}
		}

		/// <summary>
		/// Min Friction of an impact with a body part (not head, hands or feet) - to increase friction of slippy environment to get character to roll better.  Applied in catchFall and rollUp(rollDownStairs).
		/// </summary>
		/// <remarks>
		/// Default value = 0.0f.
		/// Min value = 0.0f.
		/// Max value = 10.0f.
		/// </remarks>
		public float FrictionMin
		{
			set
			{
				if (value > 10.0f)
				{
					value = 10.0f;
				}

				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("frictionMin", value);
			}
		}

		/// <summary>
		/// Max Friction of an impact with a body part (not head, hands or feet) - to increase friction of slippy environment to get character to roll better.  Applied in catchFall and rollUp(rollDownStairs).
		/// </summary>
		/// <remarks>
		/// Default value = 9999.0f.
		/// Min value = 0.0f.
		/// </remarks>
		public float FrictionMax
		{
			set
			{
				if (value < 0.0f)
				{
					value = 0.0f;
				}

				SetArgument("frictionMax", value);
			}