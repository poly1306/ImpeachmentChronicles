//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using GTA.NaturalMotion;
using System;
using System.Linq;

namespace GTA
{
	public sealed class Ped : Entity
	{
		#region Fields
		TaskInvoker _tasks;
		Euphoria _euphoria;
		WeaponCollection _weapons;
		Style _style;
		PedBoneCollection _pedBones;

		internal static readonly string[] _speechModifierNames = {
			"SPEECH_PARAMS_STANDARD",
			"SPEECH_PARAMS_ALLOW_REPEAT",
			"SPEECH_PARAMS_BEAT",
			"SPEECH_PARAMS_FORCE",
			"SPEECH_PARAMS_FORCE_FRONTEND",
			"SPEECH_PARAMS_FORCE_NO_REPEAT_FRONTEND",
			"SPEECH_PARAMS_FORCE_NORMAL",
			"SPEECH_PARAMS_FORCE_NORMAL_CLEAR",
			"SPEECH_PARAMS_FORCE_NORMAL_CRITICAL",
			"SPEECH_PARAMS_FORCE_SHOUTED",
			"SPEECH_PARAMS_FORCE_SHOUTED_CLEAR",
			"SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL",
			"SPEECH_PARAMS_FORCE_PRELOAD_ONLY",
			"SPEECH_PARAMS_MEGAPHONE",
			"SPEECH_PARAMS_HELI",
			"SPEECH_PARAMS_FORCE_MEGAPHONE",
			"SPEECH_PARAMS_FORCE_HELI",
			"SPEECH_PARAMS_INTERRUPT",
			"SPEECH_PARAMS_INTERRUPT_SHOUTED",
			"SPEECH_PARAMS_INTERRUPT_SHOUTED_CLEAR",
			"SPEECH_PARAMS_INTERRUPT_SHOUTED_CRITICAL",
			"SPEECH_PARAMS_INTERRUPT_NO_FORCE",
			"SPEECH_PARAMS_INTERRUPT_FRONTEND",
			"SPEECH_PARAMS_INTERRUPT_NO_FORCE_FRONTEND",
			"SPEECH_PARAMS_ADD_BLIP",
			"SPEECH_PARAMS_ADD_BLIP_ALLOW_REPEAT",
			"SPEECH_PARAMS_ADD_BLIP_FORCE",
			"SPEECH_PARAMS_ADD_BLIP_SHOUTED",
			"SPEECH_PARAMS_ADD_BLIP_SHOUTED_FORCE",
			"SPEECH_PARAMS_ADD_BLIP_INTERRUPT",
			"SPEECH_PARAMS_ADD_BLIP_INTERRUPT_FORCE",
			"SPEECH_PARAMS_FORCE_PRELOAD_ONLY_SHOUTED",
			"SPEECH_PARAMS_FORCE_PRELOAD_ONLY_SHOUTED_CLEAR",
			"SPEECH_PARAMS_FORCE_PRELOAD_ONLY_SHOUTED_CRITICAL",
			"SPEECH_PARAMS_SHOUTED",
			"SPEECH_PARAMS_SHOUTED_CLEAR",
			"SPEECH_PARAMS_SHOUTED_CRITICAL",
		};
		#endregion

		internal Ped(int handle) : base(handle)
		{
		}

		/// <summary>
		/// Spawn an identical clone of this <see cref="Ped"/>.
		/// </summary>
		/// <param name="heading">The direction the clone should be facing.</param>
		public Ped Clone(float heading = 0.0f)
		{
			return new Ped(Function.Call<int>(Hash.CLONE_PED, Handle, heading, false, false));
		}

		/// <summary>
		/// Kills this <see cref="Ped"/> immediately.
		/// </summary>
		public void Kill()
		{
			Health = 0;
		}

		/// <summary>
		/// Resurrects this <see cref="Ped"/> from death.
		/// </summary>
		public void Resurrect()
		{
			int health = MaxHealth;
			bool isCollisionEnabled = IsCollisionEnabled;

			Function.Call(Hash.RESURRECT_PED, Handle);
			Health = MaxHealth = health;
			IsCollisionEnabled = isCollisionEnabled;
			Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Handle);
		}

		/// <summary>
		/// Determines if this <see cref="Ped"/> exists.
		/// You should ensure <see cref="Ped"/>s still exist before manipulating them or getting some values for them on every tick, since some native functions may crash the game if invalid entity handles are passed.
		/// </summary>
		/// <returns><see langword="true" /> if this <see cref="Ped"/> exists; otherwise, <see langword="false" /></returns>
		/// <seealso cref="Entity.IsDead"/>
		/// <seealso cref="IsInjured"/>
		public new bool Exists()
		{
			return EntityType == EntityType.Ped;
		}

		private IntPtr PedIntelligenceAddress
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}

				return SHVDN.NativeMemory.ReadAddress(address + SHVDN.NativeMemory.PedIntelligenceOffset);
			}
		}

		#region Styling

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is human.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if this <see cref="Ped"/> is human; otherwise, <see langword="false" />.
		/// </value>
		public bool IsHuman => Function.Call<bool>(Hash.IS_PED_HUMAN, Handle);

		public bool IsCuffed => Function.Call<bool>(Hash.IS_PED_CUFFED, Handle);

		public bool CanWearHelmet
		{
			set => Function.Call(Hash.SET_PED_HELMET, Handle, value);
		}

		public bool IsWearingHelmet => Function.Call<bool>(Hash.IS_PED_WEARING_HELMET, Handle);

		public void ClearBloodDamage()
		{
			Function.Call(Hash.CLEAR_PED_BLOOD_DAMAGE, Handle);
		}

		public void ClearVisibleDamage()
		{
			Function.Call(Hash.RESET_PED_VISIBLE_DAMAGE, Handle);
		}

		public void GiveHelmet(bool canBeRemovedByPed, Helmet helmetType, int textureIndex)
		{
			Function.Call(Hash.GIVE_PED_HELMET, Handle, !canBeRemovedByPed, helmetType, textureIndex);
		}

		public void RemoveHelmet(bool instantly)
		{
			Function.Call(Hash.REMOVE_PED_HELMET, Handle, instantly);
		}

		/// <summary>
		/// Opens a list of clothing and prop configurations that this <see cref="Ped"/> can wear.
		/// </summary>
		public Style Style => _style ?? (_style = new Style(this));

		/// <summary>
		/// Gets the gender of this <see cref="Ped"/>.
		/// </summary>
		public Gender Gender => Function.Call<bool>(Hash.IS_PED_MALE, Handle) ? Gender.Male : Gender.Female;

		/// <summary>
		/// Gets or sets the how much sweat should be rendered on this <see cref="Ped"/>.
		/// </summary>
		/// <value>
		/// The sweat from 0 to 100, 0 being no sweat, 100 being saturated.
		/// </value>
		public float Sweat
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.SweatOffset == 0)
				{
					return 0.0f;
				}

				return SHVDN.NativeMemory.ReadFloat(address + SHVDN.NativeMemory.SweatOffset);
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				if (value > 100)
				{
					value = 100;
				}

				Function.Call(Hash.SET_PED_SWEAT, Handle, value);
			}
		}

		/// <summary>
		/// Sets how high up on this <see cref="Ped"/>s body water should be visible.
		/// </summary>
		/// <value>
		/// The height ranges from 0.0f to 1.99f, 0.0f being no water visible, 1.99f being covered in water.
		/// </value>
		public float WetnessHeight
		{
			set
			{
				if (value == 0.0f)
				{
					Function.Call(Hash.CLEAR_PED_WETNESS, Handle);
				}
				else
				{
					Function.Call<float>(Hash.SET_PED_WETNESS_HEIGHT, Handle, value);
				}
			}
		}

		#endregion

		#region Configuration

		/// <summary>
		/// Gets or sets how much armor this <see cref="Ped"/> is wearing as an <see cref="int"/>.
		/// </summary>
		/// <remarks>if you need to get or set the value precisely, use <see cref="ArmorFloat"/> instead.</remarks>
		/// <value>
		/// The armor as an <see cref="int"/>.
		/// </value>
		public int Armor
		{
			get => Function.Call<int>(Hash.GET_PED_ARMOUR, Handle);
			set => Function.Call(Hash.SET_PED_ARMOUR, Handle, value);
		}

		/// <summary>
		/// Gets or sets how much Armor this <see cref="Ped"/> is wearing as a <see cref="float"/>.
		/// </summary>
		/// <value>
		/// The armor as a <see cref="float"/>.
		/// </value>
		public float ArmorFloat
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.ArmorOffset == 0)
				{
					return 0.0f;
				}

				return SHVDN.NativeMemory.ReadFloat(address + SHVDN.NativeMemory.ArmorOffset);
			}
			set
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.ArmorOffset == 0)
				{
					return;
				}

				SHVDN.NativeMemory.WriteFloat(address + SHVDN.NativeMemory.ArmorOffset, value);
			}
		}

		/// <summary>
		/// Gets or sets how much money this <see cref="Ped"/> is carrying.
		/// </summary>
		public int Money
		{
			get => Function.Call<int>(Hash.GET_PED_MONEY, Handle);
			set => Function.Call(Hash.SET_PED_MONEY, Handle, value);
		}

		/// <summary>
		/// Gets or sets the maximum health of this <see cref="Ped"/> as an <see cref="int"/>.
		/// </summary>
		/// <value>
		/// The maximum health as an <see cref="int"/>.
		/// </value>
		public override int MaxHealth
		{
			get => Function.Call<int>(Hash.GET_PED_MAX_HEALTH, Handle);
			set => Function.Call(Hash.SET_PED_MAX_HEALTH, Handle, value);
		}

		public bool IsPlayer => Function.Call<bool>(Hash.IS_PED_A_PLAYER, Handle);

		public bool GetConfigFlag(int flagID)
		{
			return Function.Call<bool>(Hash.GET_PED_CONFIG_FLAG, Handle, flagID, true);
		}

		public void SetConfigFlag(int flagID, bool value)
		{
			Function.Call(Hash.SET_PED_CONFIG_FLAG, Handle, flagID, value);
		}

		public void ResetConfigFlag(int flagID)
		{
			Function.Call(Hash.SET_PED_RESET_FLAG, Handle, flagID, true);
		}

		/// <summary>
		/// Sets a value indicating whether this <see cref="Entity"/> is persistent.
		/// Unlike <see cref="Entity.IsPersistent"/>, calling this method does not affect assigned tasks.
		/// </summary>
		public void SetIsPersistentNoClearTask(bool value)
		{
			if (value)
			{
				PopulationType = EntityPopulationType.Mission;
			}
			else
			{
				PopulationType = EntityPopulationType.RandomAmbient;
			}
		}

		/// <summary>
		/// Gets a collection of the <see cref="PedBone"/>s in this <see cref="Ped"/>.
		/// </summary>
		public new PedBoneCollection Bones => _pedBones ?? (_pedBones = new PedBoneCollection(this));

		#endregion

		#region Tasks

		public bool IsIdle => !IsInjured && !IsRagdoll && !IsInAir && !IsOnFire && !IsDucking && !IsGettingIntoVehicle && !IsInCombat && !IsInMeleeCombat && (!IsInVehicle() || IsSittingInVehicle());

		public bool IsProne => Function.Call<bool>(Hash.IS_PED_PRONE, Handle);

		public bool IsGettingUp => Function.Call<bool>(Hash.IS_PED_GETTING_UP, Handle);

		public bool IsDiving => Function.Call<bool>(Hash.IS_PED_DIVING, Handle);

		public bool IsJumping => Function.Call<bool>(Hash.IS_PED_JUMPING, Handle);

		public bool IsFalling => Function.Call<bool>(Hash.IS_PED_FALLING, Handle);

		public bool IsVaulting => Function.Call<bool>(Hash.IS_PED_VAULTING, Handle);

		public bool IsClimbing => Function.Call<bool>(Hash.IS_PED_CLIMBING, Handle);

		public bool IsClimbingLadder => Function.Call<bool>(Hash.GET_IS_TASK_ACTIVE, Handle, 1 /* CTaskClimbLadder */);

		public bool IsWalking => Function.Call<bool>(Hash.IS_PED_WALKING, Handle);

		public bool IsRunning => Function.Call<bool>(Hash.IS_PED_RUNNING, Handle);

		public bool IsSprinting => Function.Call<bool>(Hash.IS_PED_SPRINTING, Handle);

		public bool IsStopped => Function.Call<bool>(Hash.IS_PED_STOPPED, Handle);

		public bool IsSwimming => Function.Call<bool>(Hash.IS_PED_SWIMMING, Handle);

		public bool IsSwimmingUnderWater => Function.Call<bool>(Hash.IS_PED_SWIMMING_UNDER_WATER, Handle);

		public bool IsDucking
		{
			get => Function.Call<bool>(Hash.IS_PED_DUCKING, Handle);
			set => Function.Call(Hash.SET_PED_DUCKING, Handle, value);
		}

		public bool IsHeadtracking(Entity entity)
		{
			return Function.Call<bool>(Hash.IS_PED_HEADTRACKING_ENTITY, Handle, entity.Handle);
		}

		/// <summary>
		/// Sets whether this <see cref="Ped"/> keeps their tasks when they are marked as no longer needed by <see cref="Entity.MarkAsNoLongerNeeded"/>.
		/// </summary>
		/// <value>
		/// <para>
		/// If set to <see langword="false" />, this <see cref="Ped"/>'s task will be immediately cleared and start some ambient tasks
		/// (most likely start wandering) when they are marked as no longer needed.
		/// </para>
		/// <para>
		/// If set to <see langword="true" />, this <see cref="Ped"/> will keep their scripted task.
		/// Once this <see cref="Ped"/> has no script tasks, their task will clear and they'll start some ambient tasks (one-time-only).
		/// </para>
		/// </value>
		public bool KeepTaskWhenMarkedAsNoLongerNeeded
		{
			set => Function.Call(Hash.SET_PED_KEEP_TASK, Handle, value);
		}

		/// <summary>
		/// Sets whether this <see cref="Ped"/> keeps their tasks when they are marked as no longer needed by <see cref="Entity.MarkAsNoLongerNeeded"/>.
		/// Despite the property name, this property does not determine whether permanent events can interrupt the <see cref="Ped"/>'s tasks (e.g. seeing hated peds or getting shot at).
		/// </summary>
		/// <inheritdoc cref="KeepTaskWhenMarkedAsNoLongerNeeded"/>
		[Obsolete("Ped.AlwaysKeepTask is obsolete because it does not indicate it only affects when the ped is marked as no longer needed. Use Ped.KeepTaskWhenMarkedAsNoLongerNeeded instead.")]

		public bool AlwaysKeepTask
		{
			set => KeepTaskWhenMarkedAsNoLongerNeeded = value;
		}

		/// <summary>
		/// Opens a list of <see cref="TaskInvoker"/> that this <see cref="Ped"/> can carry out.
		/// </summary>
		public TaskInvoker Task => _tasks ?? (_tasks = new TaskInvoker(this));

		/// <summary>
		/// Gets the stage of the <see cref="TaskSequence"/> this <see cref="Ped"/> is currently executing.
		/// </summary>
		public int TaskSequenceProgress => Function.Call<int>(Hash.GET_SEQUENCE_PROGRESS, Handle);

		/// <summary>
		/// Gets the task status of specified scripted task on this <see cref="Ped"/>.
		/// </summary>
		public ScriptTaskStatus GetTaskStatus(ScriptTaskNameHash taskNameHash) => Function.Call<ScriptTaskStatus>(Hash.GET_SCRIPT_TASK_STATUS, Handle, taskNameHash);

		#endregion

		#region Events

		/// <summary>
		/// Gets or sets the decision maker of this <see cref="Ped"/>, which determines what and how this <see cref="Ped"/> should response to events.
		/// Events can cause <see cref="Ped"/>s to start certain tasks. You can see how decision makers are configured in <c>events.meta</c>.
		/// </summary>
		public DecisionMaker DecisionMaker
		{
			get
			{
				if (PedIntelligenceAddress == IntPtr.Zero || SHVDN.NativeMemory.PedIntelligenceDecisionMakerHashOffset == 0)
				{
					return default;
				}

				return new DecisionMaker(SHVDN.NativeMemory.ReadInt32(PedIntelligenceAddress + SHVDN.NativeMemory.PedIntelligenceDecisionMakerHashOffset));
			}
			set => Function.Call(Hash.SET_DECISION_MAKER, Handle, value.Hash);
		}

		/// <summary>
		/// Sets whether permanent events are blocked for this <see cref="Ped"/>.
		/// <para>
		/// If set to <see langword="true" />, this <see cref="Ped"/> will no longer react to permanent events and will only do as they're told.
		/// For example, the <see cref="Ped"/> will not flee when get shot at and they will not begin combat even if <see cref="DecisionMaker"/> specifies that seeing a hated ped should.
		/// However, the <see cref="Ped"/> will still respond to temporary events like walking around other peds or vehicles even if this property is set to <see langword="true" />.
		/// </para>
		/// </summary>
		/// <value>
		///   <see langword="true" /> if permanent events are blocked; otherwise, <see langword="false" />.
		/// </value>
		public bool BlockPermanentEvents
		{
			set => Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, Handle, value);
		}

		/// <summary>
		/// Indicates whether this <see cref="Ped"/> has received the event of <paramref name="eventType"/>.
		/// <see cref="EventType.Invalid"/> can be used to test if the <see cref="Ped"/> has received any event.
		/// </summary>
		/// <value>
		///   <see langword="true"/> if the <see cref="Ped"/> has received the event  of <paramref name="eventType"/>; otherwise, <see langword="false" />.
		/// </value>
		/// <remarks>This is similar to <see cref="IsRespondingToEvent(EventType)"/>, but will work with blocking of non-temporary events with <see cref="BlockPermanentEvents"/>.</remarks>

		public bool HasReceivedEvent(EventType eventType)
		{
			if ((int)Game.Version < (int)GameVersion.v1_0_1868_0_Steam)
			{
				return Function.Call<bool>(Hash.HAS_PED_RECEIVED_EVENT, Handle, GetEventTypeIndexForB1737OrOlder(eventType));
			}

			return Function.Call<bool>(Hash.HAS_PED_RECEIVED_EVENT, Handle, eventType);
		}

		/// <summary>
		/// Indicates whether this <see cref="Ped"/> is responding to an event of <paramref name="eventType"/>.
		/// <see cref="EventType.Invalid"/> can be used to test if the <see cref="Ped"/> is responding to any event.
		/// </summary>
		/// <value>
		///   <see langword="true"/> if this <see cref="Ped"/> is responding to an event of <paramref name="eventType"/> and subsequent tasks are running; otherwise, <see langword="false" />.
		/// </value>
		public bool IsRespondingToEvent(EventType eventType)
		{
			if ((int)Game.Version < (int)GameVersion.v1_0_1868_0_Steam)
			{
				return Function.Call<bool>(Hash.IS_PED_RESPONDING_TO_EVENT, Handle, GetEventTypeIndexForB1737OrOlder(eventType));
			}

			return Function.Call<bool>(Hash.IS_PED_RESPONDING_TO_EVENT, Handle, eventType);
		}

		private int GetEventTypeIndexForB1737OrOlder(EventType eventType)
		{
			if (eventType == EventType.Incapacitated)
			{
				throw new ArgumentException("EventType.Incapacitated is not available in the game versions prior to v1.0.1868.0.", nameof(eventType));
			}
			if (eventType == EventType.ShockingBrokenGlass)
			{
				throw new ArgumentException("EventType.ShockingBrokenGlass is not available in the game versions prior to v1.0.1868.0.", nameof(eventType));
			}

			int eventTypeCorrected = (int)eventType;
			if (eventTypeCorrected >= (int)EventType.ShockingCarAlarm)
			{
				eventTypeCorrected -= 2;
			}
			else if (eventTypeCorrected >= (int)EventType.LeaderEnteredCarAsDriver)
			{
				--eventTypeCorrected;
			}
			return eventTypeCorrected;
		}

		#endregion

		#region Euphoria & Ragdoll

		/// <inheritdoc cref="Ragdoll(int, RagdollType, bool)"/>
		public void Ragdoll(int duration = -1, RagdollType ragdollType = RagdollType.Relax)
		{
			Ragdoll(duration, ragdollType, false);
		}
		/// <summary>
		/// Switches this <see cref="Ped"/> to a ragdoll by starting a ragdoll task and applying to this <see cref="Ped"/>.
		/// If <paramref name="ragdollType"/> is not set to <see cref="RagdollType.Relax"/> or <see cref="RagdollType.ScriptControl"/>, the ragdoll behavior for <see cref="RagdollType.Balance"/> will be used.
		/// </summary>
		/// <param name="duration">The duration how long the ragdoll task will run in milliseconds.</param>
		/// <param name="ragdollType">The ragdoll type.</param>
		/// <param name="forceScriptControl">
		/// Specifies whether this <see cref="Ped"/> will not get injured or killed by being lower health than <see cref="InjuryHealthThreshold"/> or <see cref="FatalInjuryHealthThreshold"/>.
		/// If ped's health goes lower than <see cref="InjuryHealthThreshold"/>, the ragdoll task will keep their health to <see cref="InjuryHealthThreshold"/> plus 5.0 until the task ends.
		/// </param>
		public void Ragdoll(int duration, RagdollType ragdollType, bool forceScriptControl = false)
		{
			CanRagdoll = true;
			// Looks like 4th and 5th parameter are completely unused
			Function.Call(Hash.SET_PED_TO_RAGDOLL, Handle, duration, duration, ragdollType, false, false, forceScriptControl);
		}

		public void CancelRagdoll()
		{
			Function.Call(Hash.SET_PED_TO_RAGDOLL, Handle, 1, 1, 1, false, false, false);
		}


		/// <summary>
		/// Indicates whether this <see cref="Ped"/> is ragdolling.
		/// Will return <see langword="false"/> when the <see cref="Ped"/> is getting up or writhing as a part of a ragdoll task.
		/// </summary>
		public bool IsRagdoll => Function.Call<bool>(Hash.IS_PED_RAGDOLL, Handle);

		/// <summary>
		/// Indicates whether this <see cref="Ped"/> is running a ragdoll task which manages its ragdoll.
		/// Will return <see langword="true"/> when <see cref="IsRagdoll"/> returns <see langword="true"/> or the <see cref="Ped"/> is getting up or writhing as a part of a ragdoll task.
		/// </summary>
		public bool IsRunningRagdollTask => Function.Call<bool>(Hash.IS_PED_RUNNING_RAGDOLL_TASK, Handle);

		public bool CanRagdoll
		{
			get => Function.Call<bool>(Hash.CAN_PED_RAGDOLL, Handle);
			set => Function.Call(Hash.SET_PED_CAN_RAGDOLL, Handle, value);
		}

		/// <summary>
		/// Opens a list of <see cref="GTA.NaturalMotion.Euphoria"/> Helpers which can be applied to this <see cref="Ped"/>.
		/// </summary>
		public Euphoria Euphoria => _euphoria ?? (_euphoria = new Euphoria(this));

		#endregion

		#region Weapon Interaction

		/// <summary>
		/// Gets or sets how accurate this <see cref="Ped"/>s shooting ability is.
		///  The higher the value of this property is, the more likely it is that this <see cref="Ped"/> will shoot at exactly where they are aiming at.
		/// </summary>
		/// <value>
		/// The accuracy from 0 to 100, 0 being very inaccurate, which means this <see cref="Ped"/> cannot shoot at exactly where they are aiming at,
		///  100 being perfectly accurate.
		/// </value>
		public int Accuracy
		{
			get => Function.Call<int>(Hash.GET_PED_ACCURACY, Handle);
			set => Function.Call(Hash.SET_PED_ACCURACY, Handle, value);
		}

		/// <summary>
		/// Sets the rate this <see cref="Ped"/> will shoot at.
		/// </summary>
		/// <value>
		/// The shoot rate from 0.0f to 1000.0f, 100.0f is the default value.
		/// </value>
		public int ShootRate
		{
			set => Function.Call(Hash.SET_PED_SHOOT_RATE, Handle, value);
		}

		/// <summary>
		/// Gets of sets the pattern this <see cref="Ped"/> uses to fire weapons.
		/// </summary>
		public FiringPattern FiringPattern
		{
			get
			{
				if (SHVDN.NativeMemory.FiringPatternOffset == 0)
				{
					return 0;
				}

				var address = PedIntelligenceAddress;
				if (address == IntPtr.Zero)
				{
					return 0;
				}

				return (FiringPattern)SHVDN.NativeMemory.ReadInt32(address + SHVDN.NativeMemory.FiringPatternOffset);
			}
			set => Function.Call(Hash.SET_PED_FIRING_PATTERN, Handle, value);
		}

		/// <summary>
		/// Gets a collection of all this <see cref="Ped"/>s <see cref="Weapon"/>s.
		/// </summary>
		public WeaponCollection Weapons => _weapons ?? (_weapons = new WeaponCollection(this));

		/// <summary>
		/// Gets the vehicle weapon this <see cref="Ped"/> is using.
		/// <remarks>The vehicle weapon, returns <see cref="VehicleWeaponHash.Invalid"/> if this <see cref="Ped"/> isnt using a vehicle weapon.</remarks>
		/// </summary>
		public VehicleWeaponHash VehicleWeapon
		{
			get
			{
				unsafe
				{
					int hash;
					return Function.Call<bool>(Hash.GET_CURRENT_PED_VEHICLE_WEAPON, Handle, &hash) ?
						(VehicleWeaponHash)hash : VehicleWeaponHash.Invalid;
				}
			}
			set
			{
				Function.Call(Hash.SET_CURRENT_PED_VEHICLE_WEAPON, Handle, value);
			}
		}

		/// <summary>
		/// Sets if this <see cref="Ped"/> can switch between different weapons.
		/// </summary>
		public bool CanSwitchWeapons
		{
			set => Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, Handle, value);
		}

		#endregion

		#region Vehicle Interaction

		public bool IsOnBike => Function.Call<bool>(Hash.IS_PED_ON_ANY_BIKE, Handle);

		public bool IsOnFoot => Function.Call<bool>(Hash.IS_PED_ON_FOOT, Handle);

		public bool IsInSub => Function.Call<bool>(Hash.IS_PED_IN_ANY_SUB, Handle);

		public bool IsInTaxi => Function.Call<bool>(Hash.IS_PED_IN_ANY_TAXI, Handle);

		public bool IsInTrain => Function.Call<bool>(Hash.IS_PED_IN_ANY_TRAIN, Handle);

		public bool IsInHeli => Function.Call<bool>(Hash.IS_PED_IN_ANY_HELI, Handle);

		public bool IsInPlane => Function.Call<bool>(Hash.IS_PED_IN_ANY_PLANE, Handle);

		public bool IsInFlyingVehicle => Function.Call<bool>(Hash.IS_PED_IN_FLYING_VEHICLE, Handle);

		public bool IsInBoat => Function.Call<bool>(Hash.IS_PED_IN_ANY_BOAT, Handle);

		public bool IsInPoliceVehicle => Function.Call<bool>(Hash.IS_PED_IN_ANY_POLICE_VEHICLE, Handle);

		public bool IsGettingIntoVehicle => Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, Handle);

		public bool IsExitingVehicle => Function.Call<bool>(Hash.GET_IS_TASK_ACTIVE, Handle, 2 /* CTaskExitVehicle */);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is jumping out of their vehicle.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if this <see cref="Ped"/> is jumping out of their vehicle; otherwise, <see langword="false" />.
		/// </value>
		public bool IsJumpingOutOfVehicle => Function.Call<bool>(Hash.IS_PED_JUMPING_OUT_OF_VEHICLE, Handle);

		public bool IsTryingToEnterALockedVehicle => Function.Call<bool>(Hash.IS_PED_TRYING_TO_ENTER_A_LOCKED_VEHICLE, Handle);

		public bool CanBeDraggedOutOfVehicle
		{
			set => Function.Call(Hash.SET_PED_CAN_BE_DRAGGED_OUT, Handle, value);
		}

		public bool CanBeKnockedOffBike
		{
			set => Function.Call(Hash.SET_PED_CAN_BE_KNOCKED_OFF_VEHICLE, Handle, !value);
		}

		public bool CanFlyThroughWindscreen
		{
			get => Function.Call<bool>(Hash.GET_PED_CONFIG_FLAG, Handle, 32, true);
			set => Function.Call(Hash.SET_PED_CONFIG_FLAG, Handle, 32, value);
		}

		public bool IsInVehicle()
		{
			return Function.Call<bool>(Hash.IS_PED_IN_ANY_VEHICLE, Handle, 0);
		}
		public bool IsInVehicle(Vehicle vehicle)
		{
			return Function.Call<bool>(Hash.IS_PED_IN_VEHICLE, Handle, vehicle.Handle, 0);
		}

		public bool IsSittingInVehicle()
		{
			return Function.Call<bool>(Hash.IS_PED_SITTING_IN_ANY_VEHICLE, Handle);
		}
		public bool IsSittingInVehicle(Vehicle vehicle)
		{
			return Function.Call<bool>(Hash.IS_PED_SITTING_IN_VEHICLE, Handle, vehicle.Handle);
		}

		public void SetIntoVehicle(Vehicle vehicle, VehicleSeat seat)
		{
			Function.Call(Hash.SET_PED_INTO_VEHICLE, Handle, vehicle.Handle, seat);
		}

		/// <summary>
		/// Gets the last <see cref="Vehicle"/> this <see cref="Ped"/> used.
		/// </summary>
		/// <remarks>returns <see langword="null" /> if the last vehicle doesn't exist.</remarks>
		public Vehicle LastVehicle
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return null;
				}

				// GET_VEHICLE_PED_IS_IN isn't reliable at getting last vehicle since it returns 0 when the ped is going to a door of some vehicle or opening one.
				// Also, the native returns the vehicle's handle the ped is getting in when ped is getting in it (which is not the last vehicle), though the 2nd parameter name is supposed to be "ConsiderEnteringAsInVehicle" as a leaked header suggests.
				var vehicleHandle = SHVDN.NativeMemory.GetLastVehicleHandleOfPed(address);
				return vehicleHandle != 0 ? new Vehicle(vehicleHandle) : null;
			}
		}

		/// <summary>
		/// Gets the current <see cref="Vehicle"/> this <see cref="Ped"/> is using.
		/// </summary>
		/// <remarks>returns <see langword="null" /> if this <see cref="Ped"/> isn't in a <see cref="Vehicle"/>.</remarks>
		public Vehicle CurrentVehicle
		{
			get
			{
				// In b2699, GET_VEHICLE_PED_IS_IN always returns the last vehicle without checking the driving flag even when the 2nd argument is set to false.
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return null;
				}

				var vehicleHandle = SHVDN.NativeMemory.GetVehicleHandlePedIsIn(address);
				return vehicleHandle != 0 ? new Vehicle(vehicleHandle) : null;
			}
		}

		/// <summary>
		/// Gets the <see cref="Vehicle"/> this <see cref="Ped"/> is trying to enter.
		/// </summary>
		/// <remarks>returns <see langword="null" /> if this <see cref="Ped"/> isn't trying to enter a <see cref="Vehicle"/>.</remarks>
		public Vehicle VehicleTryingToEnter
		{
			get
			{
				var veh = new Vehicle(Function.Call<int>(Hash.GET_VEHICLE_PED_IS_TRYING_TO_ENTER, Handle));
				return veh.Exists() ? veh : null;
			}
		}

		/// <summary>
		/// Gets the <see cref="VehicleSeat"/> this <see cref="Ped"/> is in.
		/// </summary>
		/// <value>
		/// The <see cref="VehicleSeat"/> this <see cref="Ped"/> is in if this <see cref="Ped"/> is in a <see cref="Vehicle"/>; otherwise, <see cref="VehicleSeat.None"/>.
		/// </value>
		public VehicleSeat SeatIndex
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.SeatIndexOffset == 0)
				{
					return VehicleSeat.None;
				}

				int seatIndex = (sbyte)SHVDN.NativeMemory.ReadByte(address + SHVDN.NativeMemory.SeatIndexOffset);
				return (seatIndex >= 0 && IsInVehicle()) ? (VehicleSeat)(seatIndex - 1) : VehicleSeat.None;
			}
		}

		#endregion

		#region Driving

		public float DrivingSpeed
		{
			set => Function.Call(Hash.SET_DRIVE_TASK_CRUISE_SPEED, Handle, value);
		}

		/// <summary>
		/// Sets the maximum driving speed this <see cref="Ped"/> can drive at.
		/// </summary>
		public float MaxDrivingSpeed
		{
			set => Function.Call(Hash.SET_DRIVE_TASK_MAX_CRUISE_SPEED, Handle, value);
		}

		public DrivingStyle DrivingStyle
		{
			set => Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, Handle, value);
		}

		public VehicleDrivingFlags VehicleDrivingFlags
		{
			set => Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, Handle, value);
		}

		#endregion

		#region Jacking

		public bool IsJacking => Function.Call<bool>(Hash.IS_PED_JACKING, Handle);

		public bool IsBeingJacked => Function.Call<bool>(Hash.IS_PED_BEING_JACKED, Handle);

		/// <summary>
		/// Sets a value indicating whether this <see cref="Ped"/> will stay in the vehicle when the driver gets jacked.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if <see cref="Ped"/> stays in vehicle when jacked; otherwise, <see langword="false" />.
		/// </value>
		public bool StaysInVehicleWhenJacked
		{
			set => Function.Call(Hash.SET_PED_STAY_IN_VEHICLE_WHEN_JACKED, Handle, value);
		}

		public Ped Jacker
		{
			get
			{
				var ped = new Ped(Function.Call<int>(Hash.GET_PEDS_JACKER, Handle));
				return ped.Exists() ? ped : null;
			}
		}

		public Ped JackTarget
		{
			get
			{
				var ped = new Ped(Function.Call<int>(Hash.GET_JACK_TARGET, Handle));
				return ped.Exists() ? ped : null;
			}
		}

		#endregion

		#region Parachuting

		public bool IsInParachuteFreeFall => Function.Call<bool>(Hash.IS_PED_IN_PARACHUTE_FREE_FALL, Handle);

		public void OpenParachute()
		{
			Function.Call(Hash.FORCE_PED_TO_OPEN_PARACHUTE, Handle);
		}

		public ParachuteState ParachuteState => Function.Call<ParachuteState>(Hash.GET_PED_PARACHUTE_STATE, Handle);

		public ParachuteLandingType ParachuteLandingType => Function.Call<ParachuteLandingType>(Hash.GET_PED_PARACHUTE_LANDING_TYPE, Handle);

		#endregion

		#region Combat

		public bool IsEnemy
		{
			set => Function.Call(Hash.SET_PED_AS_ENEMY, Handle, value);
		}

		public bool IsPriorityTargetForEnemies
		{
			set => Function.Call(Hash.SET_ENTITY_IS_TARGET_PRIORITY, Handle, value, 0);
		}

		public bool IsFleeing => Function.Call<bool>(Hash.IS_PED_FLEEING, Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is injured (<see cref="Entity.Health"/> of the <see cref="Ped"/> is lower than <see cref="InjuryHealthThreshold"/>) or does not exist.
		/// Can be called safely to check if <see cref="Ped"/>s exist and are not injured without calling <see cref="Exists"/>.
		/// </summary>
		/// <value>
		///   <see langword="true" /> this <see cref="Ped"/> is injured or does not exist; otherwise, <see langword="false" />.
		/// </value>
		/// <seealso cref="Entity.IsDead"/>
		/// <seealso cref="Exists"/>
		public bool IsInjured => Function.Call<bool>(Hash.IS_PED_INJURED, Handle);

		public bool IsInStealthMode => Function.Call<bool>(Hash.GET_PED_STEALTH_MOVEMENT, Handle);

		public bool IsInCombat => Function.Call<bool>(Hash.IS_PED_IN_COMBAT, Handle);

		public bool IsInMeleeCombat => Function.Call<bool>(Hash.IS_PED_IN_MELEE_COMBAT, Handle);

		public bool IsAiming => GetConfigFlag(78);

		public bool IsPlantingBomb => Function.Call<bool>(Hash.IS_PED_PLANTING_BOMB, Handle);

		public bool IsShooting => Function.Call<bool>(Hash.IS_PED_SHOOTING, Handle);

		public bool IsReloading => Function.Call<bool>(Hash.IS_PED_RELOADING, Handle);

		public bool IsDoingDriveBy => Function.Call<bool>(Hash.IS_PED_DOING_DRIVEBY, Handle);

		public bool IsGoingIntoCover => Function.Call<bool>(Hash.IS_PED_GOING_INTO_COVER, Handle);

		public bool IsAimingFromCover => Function.Call<bool>(Hash.IS_PED_AIMING_FROM_COVER, Handle);

		public bool IsBeingStunned => Function.Call<bool>(Hash.IS_PED_BEING_STUNNED, Handle);

		public bool IsBeingStealthKilled => Function.Call<bool>(Hash.IS_PED_BEING_STEALTH_KILLED