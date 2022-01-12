
//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GTA
{
	public sealed class Vehicle : Entity
	{
		#region Fields
		VehicleDoorCollection _doors;
		VehicleModCollection _mods;
		VehicleWheelCollection _wheels;
		VehicleWindowCollection _windows;
		#endregion

		internal Vehicle(int handle) : base(handle)
		{
		}

		/// <summary>
		/// Repair all damage to this <see cref="Vehicle"/> instantaneously.
		/// </summary>
		public void Repair()
		{
			Function.Call(Hash.SET_VEHICLE_FIXED, Handle);
			IsConsideredDestroyed = false;
		}

		/// <summary>
		/// Explode this <see cref="Vehicle"/> instantaneously.
		/// </summary>
		public void Explode()
		{
			Function.Call(Hash.EXPLODE_VEHICLE, Handle, true, false);
		}

		/// <summary>
		/// Determines if this <see cref="Vehicle"/> exists.
		/// You should ensure <see cref="Vehicle"/>s still exist before manipulating them or getting some values for them on every tick, since some native functions may crash the game if invalid entity handles are passed.
		/// </summary>
		/// <returns><see langword="true" /> if this <see cref="Vehicle"/> exists; otherwise, <see langword="false" /></returns>
		public new bool Exists()
		{
			return EntityType == EntityType.Vehicle;
		}

		#region Styling

		public bool IsConvertible => Function.Call<bool>(Hash.IS_VEHICLE_A_CONVERTIBLE, Handle, 0);
		public bool IsBig => Function.Call<bool>(Hash.IS_BIG_VEHICLE, Handle);
		public bool HasBulletProofGlass => SHVDN.NativeMemory.HasVehicleFlag(Model.Hash, SHVDN.NativeMemory.VehicleFlag2.HasBulletProofGlass);
		public bool HasLowriderHydraulics => Game.Version >= GameVersion.v1_0_505_2_Steam && SHVDN.NativeMemory.HasVehicleFlag(Model.Hash, SHVDN.NativeMemory.VehicleFlag2.HasLowriderHydraulics);
		public bool HasDonkHydraulics => Game.Version >= GameVersion.v1_0_505_2_Steam && SHVDN.NativeMemory.HasVehicleFlag(Model.Hash, SHVDN.NativeMemory.VehicleFlag2.HasLowriderDonkHydraulics);
		public bool HasParachute => Game.Version >= GameVersion.v1_0_505_2_Steam && Function.Call<bool>(Hash.GET_VEHICLE_HAS_PARACHUTE, Handle);
		public bool HasRocketBoost => Game.Version >= GameVersion.v1_0_944_2_Steam && Function.Call<bool>(Hash.GET_HAS_ROCKET_BOOST, Handle);
		public bool IsParachuteDeployed => Game.Version >= GameVersion.v1_0_1011_1_Steam && Function.Call<bool>(Hash.IS_VEHICLE_PARACHUTE_DEPLOYED, Handle);
		public bool IsRocketBoostActive
		{
			get => Game.Version >= GameVersion.v1_0_944_2_Steam && Function.Call<bool>(Hash.IS_ROCKET_BOOST_ACTIVE, Handle);
			set
			{
				if (Game.Version < GameVersion.v1_0_944_2_Steam)
				{
					throw new GameVersionNotSupportedException(GameVersion.v1_0_944_2_Steam, nameof(Vehicle), nameof(IsRocketBoostActive));
				}

				Function.Call(Hash.SET_ROCKET_BOOST_ACTIVE, Handle, value);
			}
		}


		public float DirtLevel
		{
			get => Function.Call<float>(Hash.GET_VEHICLE_DIRT_LEVEL, Handle);
			set => Function.Call(Hash.SET_VEHICLE_DIRT_LEVEL, Handle, value);
		}

		public VehicleModCollection Mods => _mods ?? (_mods = new VehicleModCollection(this));

		public VehicleWheelCollection Wheels => _wheels ?? (_wheels = new VehicleWheelCollection(this));

		public VehicleWindowCollection Windows => _windows ?? (_windows = new VehicleWindowCollection(this));

		public void Wash()
		{
			DirtLevel = 0f;
		}

		public bool IsExtraOn(int extra)
		{
			return Function.Call<bool>(Hash.IS_VEHICLE_EXTRA_TURNED_ON, Handle, extra);
		}

		public bool ExtraExists(int extra)
		{
			return Function.Call<bool>(Hash.DOES_EXTRA_EXIST, Handle, extra);
		}

		public void ToggleExtra(int extra, bool toggle)
		{
			Function.Call(Hash.SET_VEHICLE_EXTRA, Handle, extra, !toggle);
		}

		#endregion

		#region Configuration

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a regular automobile.
		/// </summary>
		public bool IsRegularAutomobile => Type == VehicleType.Automobile;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is an amphibious automobile.
		/// </summary>
		public bool IsAmphibiousAutomobile => Type == VehicleType.AmphibiousAutomobile;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a submarine car.
		/// </summary>
		public bool IsSubmarineCar => Type == VehicleType.SubmarineCar;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is an automobile.
		/// </summary>
		public bool IsAutomobile
		{
			get
			{
				var vehicleType = Type;
				return (vehicleType == VehicleType.Automobile || vehicleType == VehicleType.AmphibiousAutomobile || vehicleType == VehicleType.SubmarineCar);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a regular quad bike.
		/// </summary>
		public bool IsRegularQuadBike => Type == VehicleType.QuadBike;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is an amphibious quad bike.
		/// </summary>
		public bool IsAmphibiousQuadBike => Type == VehicleType.AmphibiousQuadBike;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a quad bike.
		/// </summary>
		public bool IsQuadBike
		{
			get
			{
				var vehicleType = Type;
				return (vehicleType == VehicleType.QuadBike || vehicleType == VehicleType.AmphibiousQuadBike);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is an amphibious vehicle.
		/// </summary>
		public bool IsAmphibious
		{
			get
			{
				var vehicleType = Type;
				return (vehicleType == VehicleType.AmphibiousAutomobile || vehicleType == VehicleType.AmphibiousQuadBike);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a trailer.
		/// </summary>
		public bool IsTrailer => Type == VehicleType.Trailer;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a plane.
		/// </summary>
		public bool IsPlane => Type == VehicleType.Plane;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a helicopter.
		/// </summary>
		public bool IsHelicopter => Type == VehicleType.Helicopter;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a helicopter.
		/// </summary>
		public bool IsBlimp => Type == VehicleType.Blimp;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is an aircraft.
		/// </summary>
		public bool IsAircraft
		{
			get
			{
				var vehicleType = Type;
				return (vehicleType == VehicleType.Plane || vehicleType == VehicleType.Helicopter || vehicleType == VehicleType.Blimp);
			}
		}

		private bool IsHeliOrBlimp
		{
			get
			{
				var vehicleType = Type;
				return ((uint)vehicleType - 8) <= 1;
			}
		}

		private bool IsRotaryWingAircraft
		{
			get
			{
				var vehicleType = Type;
				return ((uint)vehicleType - 8) <= 2;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a motorcycle.
		/// </summary>
		public bool IsMotorcycle => Type == VehicleType.Motorcycle;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a bicycle.
		/// </summary>
		public bool IsBicycle => Type == VehicleType.Bicycle;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a bike.
		/// </summary>
		public bool IsBike
		{
			get
			{
				var vehicleType = Type;
				return (vehicleType == VehicleType.Motorcycle || vehicleType == VehicleType.Bicycle);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a boat.
		/// </summary>
		public bool IsBoat => Type == VehicleType.Motorcycle;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a train.
		/// </summary>
		public bool IsTrain => Type == VehicleType.Train;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Vehicle"/> is a submarine.
		/// </summary>
		public bool IsSubmarine => Type == VehicleType.Submarine;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Vehicle"/> can pretend it has the same <see cref="Ped"/>s.
		/// Set to <see langword="false"/> to prevent this <see cref="Vehicle"/> from creating new <see cref="Ped"/>s as its occupants.
		/// </summary>
		/// <remarks>
		/// <see cref="Vehicle"/>s do not pretend occupants regardless of this value if <see cref="Entity.PopulationType"/> is set to
		/// <see cref="EntityPopulationType.Permanent"/> or <see cref="EntityPopulationType.Mission"/>.
		/// </remarks>
		public bool CanPretendOccupants
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.DisablePretendOccupantOffset == 0)
				{
					return false;
				}

				return !SHVDN.NativeMemory.IsBitSet(address + SHVDN.NativeMemory.DisablePretendOccupantOffset, 7);
			}
			set
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.DisablePretendOccupantOffset == 0)
				{
					return;
				}

				// SET_DISABLE_PRETEND_OCCUPANTS changes the value only if the population type is set to 6 or 7, so change the value manually
				SHVDN.NativeMemory.SetBit(address + SHVDN.NativeMemory.DisablePretendOccupantOffset, 7, !value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Vehicle"/> was stolen.
		/// </summary>
		public bool IsStolen
		{