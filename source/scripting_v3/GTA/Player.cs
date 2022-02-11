//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System.Drawing;

namespace GTA
{
	public sealed class Player : INativeValue
	{
		#region Fields
		Ped ped;
		#endregion

		internal Player(int handle)
		{
			Handle = handle;
		}

		public int Handle
		{
			get;
			private set;
		}

		public ulong NativeValue
		{
			get => (ulong)Handle;
			set => Handle = unchecked((int)value);
		}

		/// <summary>
		/// Gets the <see cref="Ped"/> this <see cref="Player"/> is controlling.
		/// </summary>
		public Ped Character
		{
			get
			{
				int handle = Function.Call<int>(Hash.GET_PLAYER_PED, Handle);

				if (ped == null || handle != ped.Handle)
				{
					ped = new Ped(handle);
				}

				return ped;
			}
		}

		/// <summary>
		/// Gets the Social Club name of this <see cref="Player"/>.
		/// </summary>
		public string Name => Function.Call<string>(Hash.GET_PLAYER_NAME, Handle);

		/// <summary>
		/// Gets or sets how much money this <see cref="Player"/> has.
		/// <remarks>Only works if current player is <see cref="PedHash.Michael"/>, <see cref="PedHash.Franklin"/> or <see cref="PedHash.Trevor"/></remarks>
		/// </summary>
		public int Money
		{
			get
			{
				int stat;

				switch ((PedHash)Character.Model.Hash)
				{
					case PedHash.Michael:
						stat = Game.GenerateHash("SP0_TOTAL_CASH");
						break;
					case PedHash.Franklin:
						stat = Game.GenerateHash("SP1_TOTAL_CASH");
						break;
					case PedHash.Trevor:
						stat = Game.GenerateHash("SP2_TOTAL_CASH");
						break;
					default:
						return 0;
				}

				int result;
				unsafe
				{
					Function.Call(Hash.STAT_GET_INT, stat, &result, -1);
				}

				return result;
			}
			set
			{
				int stat;

				switch ((PedHash)Character.Model.Hash)
				{
					case PedHash.Michael:
						stat = Game.GenerateHash("SP0_TOTAL_CASH");
						break;
					case PedHash.Franklin:
						stat = Game.GenerateHash("SP1_TOTAL_CASH");
						break;
					case PedHash.Trevor:
						stat = Game.GenerateHash("SP2_TOTAL_CASH");
						break;
					default:
						return;
				}

				Function.Call(Hash.STAT_SET_INT, stat, value, 1);
			}
		}

		/// <summary>
		/// Gets or sets the wanted level for this <see cref="Player"/>.
		/// </summary>
		public int WantedLevel
		{
			get => Function.Call<int>(Hash.GET_PLAYER_WANTED_LEVEL, Handle);
			set
			{
				Function.Call(Hash.SET_PLAYER_WANTED_LEVEL, Handle, value, false);
				Function.Call(Hash.SET_PLAYER_WANTED_LEVEL_NOW, Handle, false);
			}
		}

		/// <summary>
		/// Gets or sets the wanted center position for this <see cref="Player"/>.
		/// </summary>
		/// <value>
		/// The place in world coordinates where the police think this <see cref="Player"/> is.
		/// </value>
		public Vector3 WantedCenterPosition
		{
			get => Function.Call<Vector3>(Hash.GET_PLAYER_WANTED_CENTRE_POSITION, Handle);
			set => Function.Call(Hash.SET_PLAYER_WANTED_CENTRE_POSITION, Handle, value.X, value.Y, value.Z);
		}

		/// <summary>
		/// Gets or sets the maximum amount of armor this <see cref="Player"/> can carry.
		/// </summary>
		public int MaxArmor
		{
			get => Function.Call<int>(Hash.GET_PLAYER_MAX_ARMOUR, Handle);
			set => Function.Call(Hash.SET_PLAYER_MAX_ARMOUR, Handle, value);
		}

		/// <summary>
		/// Gets or sets the primary parachute tint for this <see cref="Player"/>.
		/// </summary>
		public ParachuteTint PrimaryParachuteTint
		{
			get
			{
				int result;
				unsafe
				{
					Function.Call(Hash.GET_PLAYER_PARACHUTE_TINT_INDEX, Handle, &result);
				}
				return (ParachuteTint)result;
			}
			set => Function.Call(Hash.SET_PLAYER_PARACHUTE_TINT_INDEX, Handle, value);
		}
		/// <summary>
		/// Gets or sets the reserve parachute tint for this <see cref="Player"/>.
		/// </summary>
		public ParachuteTint ReserveParachuteTint
		{
			get
			{
				int result;
				unsafe
				{
					Function.Call(Hash.GET_PLAYER_RESERVE_PARACHUTE_TINT_INDEX, Handle, &result);
				}
				return (ParachuteTint)result;
			}
			set => Function.Call(Hash.SET_PLAYER_RESERVE_PARACHUTE_TINT_INDEX, Handle, value);
		}

		/// <summary>
		/// Sets a value indicating whether this <see cref="Player"/> can leave a parachute smoke trail.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if this <see cref="Player"/> can leave a parachute smoke trail; otherwise, <see langword="false" />.
		/// </value>
		public bool CanLeaveParachuteSmokeTrail
		{
			set => Function.Call(Hash.SET_PLAYER_CAN_LEAVE_PARACHUTE_SMOKE_TRAIL, Handle, value);
		}

		/// <summary>
		/// Gets or sets the color of the parachute smoke trail for this <see cref="Player"/>.
		/// </summary>
		/// <value>
		/// The color of the parachute smoke trail for this <see cref="Player"/>.
		/// </value>
		public Color ParachuteSmokeTrailColor
		{
			get
			{
				int r, g, b;
				unsafe
				{
					Function.Call(Hash.GET_PLAYER_PARACHUTE_SMOKE_TRAIL_COLOR, Handle, &r, &g, &b);
				}
				return Color.FromArgb(r, g, b);
			}
			set => Function.Call(Hash.SET_PLAYER_PARACHUTE_SMOKE_TRAIL_COLOR, Handle, value.R, value.G, value.B);
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Player"/> is dead.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if this <see cref="Player"/> is dead; otherwise, <see langword="false" />.
		/// </value>
		public bool IsDead => Function.Call<bool>(Hash.IS_PLAYER_DEAD, Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Player"/> is alive.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if this <see cref="Player"/> is alive; otherwise, <see langword="false" />.
		/// </value>
		public bool IsAlive => !IsDead;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Player"/> is aiming.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if this <see cref="Player"/> is aiming; otherwise, <see lan