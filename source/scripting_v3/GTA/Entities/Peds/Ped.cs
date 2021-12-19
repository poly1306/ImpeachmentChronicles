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
		/