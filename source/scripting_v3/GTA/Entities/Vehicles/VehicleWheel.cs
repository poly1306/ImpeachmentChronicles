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
		