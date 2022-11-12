
//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Collections.Generic;

namespace GTA.NaturalMotion
{
	public sealed class Euphoria
	{
		#region Fields
		readonly Ped _ped;
		readonly Dictionary<string, CustomHelper> _helperCache;
		#endregion

		internal Euphoria(Ped ped)
		{
			_ped = ped;
			_helperCache = new Dictionary<string, CustomHelper>();
		}
		T GetHelper<T>(string message) where T : CustomHelper
		{
			CustomHelper h;

			if (!_helperCache.TryGetValue(message, out h))
			{
				h = (CustomHelper)Activator.CreateInstance(typeof(T), _ped);

				_helperCache.Add(message, h);
			}

			return (T)h;
		}

		/// <summary>
		/// Gets a ActivePose Helper class for sending ActivePose <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ActivePoseHelper ActivePose => GetHelper<ActivePoseHelper>("activePose");

		/// <summary>
		/// Gets a ApplyImpulse Helper class for sending ApplyImpulse <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ApplyImpulseHelper ApplyImpulse => GetHelper<ApplyImpulseHelper>("applyImpulse");

		/// <summary>
		/// Gets a ApplyBulletImpulse Helper class for sending ApplyBulletImpulse <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ApplyBulletImpulseHelper ApplyBulletImpulse => GetHelper<ApplyBulletImpulseHelper>("applyBulletImpulse");

		/// <summary>
		/// Gets a BodyRelax Helper class for sending BodyRelax <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// Set the amount of relaxation across the whole body; Used to collapse the character into a rag-doll-like state.
		/// </remarks>
		public BodyRelaxHelper BodyRelax => GetHelper<BodyRelaxHelper>("bodyRelax");

		/// <summary>
		/// Gets a ConfigureBalance Helper class for sending ConfigureBalance <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// This single message allows you to configure various parameters used on any behavior that uses the dynamic balance.
		/// </remarks>
		public ConfigureBalanceHelper ConfigureBalance => GetHelper<ConfigureBalanceHelper>("configureBalance");

		/// <summary>
		/// Gets a ConfigureBalanceReset Helper class for sending ConfigureBalanceReset <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// reset the values configurable by the Configure Balance message to their defaults.
		/// </remarks>
		public ConfigureBalanceResetHelper ConfigureBalanceReset => GetHelper<ConfigureBalanceResetHelper>("configureBalanceReset");

		/// <summary>
		/// Gets a ConfigureSelfAvoidance Helper class for sending ConfigureSelfAvoidance <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// this single message allows to configure self avoidance for the character.BBDD Self avoidance tech.
		/// </remarks>
		public ConfigureSelfAvoidanceHelper ConfigureSelfAvoidance => GetHelper<ConfigureSelfAvoidanceHelper>("configureSelfAvoidance");

		/// <summary>
		/// Gets a ConfigureBullets Helper class for sending ConfigureBullets <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ConfigureBulletsHelper ConfigureBullets => GetHelper<ConfigureBulletsHelper>("configureBullets");

		/// <summary>
		/// Gets a ConfigureBulletsExtra Helper class for sending ConfigureBulletsExtra <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ConfigureBulletsExtraHelper ConfigureBulletsExtra => GetHelper<ConfigureBulletsExtraHelper>("configureBulletsExtra");

		/// <summary>
		/// Gets a ConfigureLimits Helper class for sending ConfigureLimits <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// Enable/disable/edit character limits in real time.  This adjusts limits in RAGE-native space and will *not* reorient the joint.
		/// </remarks>
		public ConfigureLimitsHelper ConfigureLimits => GetHelper<ConfigureLimitsHelper>("configureLimits");

		/// <summary>
		/// Gets a ConfigureSoftLimit Helper class for sending ConfigureSoftLimit <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ConfigureSoftLimitHelper ConfigureSoftLimit => GetHelper<ConfigureSoftLimitHelper>("configureSoftLimit");

		/// <summary>
		/// Gets a ConfigureShotInjuredArm Helper class for sending ConfigureShotInjuredArm <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// This single message allows you to configure the injured arm reaction during shot
		/// </remarks>
		public ConfigureShotInjuredArmHelper ConfigureShotInjuredArm => GetHelper<ConfigureShotInjuredArmHelper>("configureShotInjuredArm");

		/// <summary>
		/// Gets a ConfigureShotInjuredLeg Helper class for sending ConfigureShotInjuredLeg <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// This single message allows you to configure the injured leg reaction during shot
		/// </remarks>
		public ConfigureShotInjuredLegHelper ConfigureShotInjuredLeg => GetHelper<ConfigureShotInjuredLegHelper>("configureShotInjuredLeg");

		/// <summary>
		/// Gets a DefineAttachedObject Helper class for sending DefineAttachedObject <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public DefineAttachedObjectHelper DefineAttachedObject => GetHelper<DefineAttachedObjectHelper>("defineAttachedObject");

		/// <summary>
		/// Gets a ForceToBodyPart Helper class for sending ForceToBodyPart <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// Apply an impulse to a named body part
		/// </remarks>
		public ForceToBodyPartHelper ForceToBodyPart => GetHelper<ForceToBodyPartHelper>("forceToBodyPart");

		/// <summary>
		/// Gets a LeanInDirection Helper class for sending LeanInDirection <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public LeanInDirectionHelper LeanInDirection => GetHelper<LeanInDirectionHelper>("leanInDirection");

		/// <summary>
		/// Gets a LeanRandom Helper class for sending LeanRandom <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public LeanRandomHelper LeanRandom => GetHelper<LeanRandomHelper>("leanRandom");

		/// <summary>
		/// Gets a LeanToPosition Helper class for sending LeanToPosition <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public LeanToPositionHelper LeanToPosition => GetHelper<LeanToPositionHelper>("leanToPosition");

		/// <summary>
		/// Gets a LeanTowardsObject Helper class for sending LeanTowardsObject <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public LeanTowardsObjectHelper LeanTowardsObject => GetHelper<LeanTowardsObjectHelper>("leanTowardsObject");

		/// <summary>
		/// Gets a HipsLeanInDirection Helper class for sending HipsLeanInDirection <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public HipsLeanInDirectionHelper HipsLeanInDirection => GetHelper<HipsLeanInDirectionHelper>("hipsLeanInDirection");

		/// <summary>
		/// Gets a HipsLeanRandom Helper class for sending HipsLeanRandom <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public HipsLeanRandomHelper HipsLeanRandom => GetHelper<HipsLeanRandomHelper>("hipsLeanRandom");

		/// <summary>
		/// Gets a HipsLeanToPosition Helper class for sending HipsLeanToPosition <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public HipsLeanToPositionHelper HipsLeanToPosition => GetHelper<HipsLeanToPositionHelper>("hipsLeanToPosition");

		/// <summary>
		/// Gets a HipsLeanTowardsObject Helper class for sending HipsLeanTowardsObject <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public HipsLeanTowardsObjectHelper HipsLeanTowardsObject => GetHelper<HipsLeanTowardsObjectHelper>("hipsLeanTowardsObject");

		/// <summary>
		/// Gets a ForceLeanInDirection Helper class for sending ForceLeanInDirection <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ForceLeanInDirectionHelper ForceLeanInDirection => GetHelper<ForceLeanInDirectionHelper>("forceLeanInDirection");

		/// <summary>
		/// Gets a ForceLeanRandom Helper class for sending ForceLeanRandom <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ForceLeanRandomHelper ForceLeanRandom => GetHelper<ForceLeanRandomHelper>("forceLeanRandom");

		/// <summary>
		/// Gets a ForceLeanToPosition Helper class for sending ForceLeanToPosition <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ForceLeanToPositionHelper ForceLeanToPosition => GetHelper<ForceLeanToPositionHelper>("forceLeanToPosition");

		/// <summary>
		/// Gets a ForceLeanTowardsObject Helper class for sending ForceLeanTowardsObject <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		public ForceLeanTowardsObjectHelper ForceLeanTowardsObject => GetHelper<ForceLeanTowardsObjectHelper>("forceLeanTowardsObject");

		/// <summary>
		/// Gets a SetStiffness Helper class for sending SetStiffness <see cref="Message"/> to this <see cref="Ped"/>.
		/// </summary>
		/// <remarks>
		/// Use this message to manually set the body stiffness values -before using Active Pose to drive to an animated pose, for example.
		/// </remarks>
		public SetStiffnessHelper SetStiffness => GetHelper<SetStiffnessHelper>("setStiffness");