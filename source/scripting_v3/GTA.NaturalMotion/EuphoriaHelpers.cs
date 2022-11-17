
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