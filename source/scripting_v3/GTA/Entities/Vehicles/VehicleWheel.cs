//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using System;

namespace GTA
{
	public sealed class VehicleWheel
	{
		#region Fields
		IntPtr _cachedAddress;

		internal static readonly VehicleWheelBoneId[] vehicleWheelBoneIndexTableForNatives = {
			VehicleWheelBoneId.WheelLeftFront,
			VehicleWheelBoneId.WheelRightFront,
			VehicleWheelBoneId.WheelLeftMiddle1,
			VehicleWheelBoneId.WheelRightMiddle1,
			VehicleWheelBoneId.WheelLeftRear,
			VehicleWheelBoneId.WheelRightRear,
			VehicleWheelBoneId.WheelLeftFront,
			VehicleWheelBoneId.WheelLeftRear,
		};
		#endregion

		internal VehicleWheel(Vehicle owner, int index)
		{
			Vehicle = owner;

			#region Index Assignment
#pragma warning disable CS0618
			switch (index)
			{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					BoneId = vehicleWheelBoneIndexTableForNatives[index];
					Index = index;
					break;
				default:
					BoneId = VehicleWheelBoneId.Invalid;
					Index = -1;
					break;
			}
#pragma warning restore CS0618
			#endregion
		}

		internal VehicleWheel(Vehicle owner, VehicleWheelBoneId boneIndex)
		{
			Vehicle = owner;
			BoneId = boneIndex;

			#region Index Assignment
#pragma warning disable CS0618
			switch (boneIndex)
			{
				case VehicleWheelBoneId.WheelLeftFront:
				case VehicleWheelBoneId.WheelRightFront:
					Index = (int)boneIndex - 11;
					break;
				case VehicleWheelBoneId.WheelLeftRear:
				case VehicleWheelBoneId.WheelRightRear:
					Index = (int)boneIndex - 9;
					break;
				case VehicleWheelBoneId.WheelLeftMiddle1:
				case VehicleWheelBoneId.WheelRightMiddle1:
					Index = (int)boneIndex - 13;
					break;
				default:
					// Natives for vehicle wheels don't support the middle 2 wheels or middle 3 wheels
					// Can fetch some correct value even if any value outside 0 to 7 is passed as the wheel id to the natives, but it's kind of a undefined behavior because the array for wheel id has only 8 elements
					Index = -1;
					break;
			}
#pragma warning restore CS0618
			#endregion
		}

		internal VehicleWheel(Vehicle owner, VehicleWheelBoneId boneIndex, IntPtr wheelAddress) : this(owner, boneIndex)
		{
			_cachedAddress = wheelAddress;
		}

		/// <summary>
		/// Gets the <see cref="Vehicle"/>this <see cref="VehicleWheel"/> belongs to.
		/// </summary>
		public Vehicle Vehicle
		{
			get;
		}

		/// <summary>
		/// Gets the index for native functions.
		/// Obsoleted in v3 API because there is no legiminate ways to get value from or modify any of the 4 wheels <c>wheel_lm2</c>, <c>wheel_rm2</c>, <c>wheel_lm3</c>, or <c>wheel_lm3</c> in native functions.
		/// </summary>
		[Obsolete("VehicleWheel.Index does not support any of the wheels wheel_lm2, wheel_rm2, wheel_lm3, or wheel_lm3, but provided for legacy scripts compatibility in v3 API. Use VehicleWheel.BoneId instead.")]
		public int Index
		{
			get;
		}

		/// <summary>
		/// Gets the bone id this <see cref="VehicleWheel"/>.
		/// </summary>
		public VehicleWheelBoneId BoneId
		{
			get;
		}

		/// <summary>
		/// Gets the memory address where this <see cref="VehicleWheel"/> is stored in memory.
		/// </summary>
		public IntPtr MemoryAddress
		{
			get
			{
				if (!IsBoneIdValid(BoneId))
				{
					return IntPtr.Zero;
				}

				// Check if the vehicle is not boat, train, or submarine. This also checks if the vehicle exists (0xFFFFFFFF will be returned if doesn't exist)
				if (!CanVehicleHaveWheels(Vehicle))
				{
					return IntPtr.Zero;
				}

				if (_cachedAddress != IntPtr.Zero)
				{
					return _cachedAddress;
				}

				return GetMemoryAddressInit();
			}
		}

		/// <summary>
		/// Gets the last contact position.
		/// </summary>
		public Vector3 LastContactPosition
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return Vector3.Zero;
				}

				return new Vector3(SHVDN.NativeMemory.ReadVector3(address + 0x40));
			}
		}

		/// <summary>
		/// Gets or sets the limit multiplier that affects how much this <see cref="VehicleWheel"/> can turn.
		/// </summary>
		public float SteeringLimitMultiplier
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.VehicleWheelSteeringLimitMultiplierOffset == 0)
				{
					return 0f;
				}

				return SHVDN.NativeMemory.ReadFloat(address + SHVDN.NativeMemory.VehicleWheelSteeringLimitMultiplierOffset);
			}
			set
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.VehicleWheelSteeringLimitMultiplierOffset == 0)
				{
					return;
				}

				SHVDN.NativeMemory.WriteFloat(address + SHVDN.NativeMemory.VehicleWheelSteeringLimitMultiplierOffset, value);
			}
		}

		/// <summary>
		/// Gets or sets the temperature of <see cref="VehicleWheel"/>. This value rises when <see cref="Vehicle"/> is drifting, braking, or in burnout.
		/// If this value is kept at <c>59f</c> when <see cref="Vehicle"/> is on burnout for a short time, the tire will burst.
		/// </summary>
		public float Temperature
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.VehicleWheelTemperatureOffset == 0)
				{
					return 0f;
				}

				return SHVDN.NativeMemory.ReadFloat(address + SHVDN.NativeMemory.VehicleWheelTemperatureOffset);
			}
			set
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.VehicleWheelTemperatureOffset == 0)
				{
					return;
				}

				SHVDN.NativeMemory.WriteFloat(address + SHVDN.NativeMemory.VehicleWheelTemperatureOffset, value);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="VehicleWheel"/> is touching any surface.
		/// </summary>
		public bool IsTouchingSurface
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return false;
				}

				return SHVDN.NativeMemory.IsWheelTouchingSurface(address, Vehicle.MemoryAddress);
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="VehicleWheel"/>'s tire is on fire.
		/// </summary>
		public bool IsTireOnFire
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.VehicleWheelTouchingFlagsOffset == 0)
				{
					return false;
				}

				return SHVDN.NativeMemory.IsBitSet(address + SHVDN.NativeMemory.VehicleWheelTouchingFlagsOffset, 3);
