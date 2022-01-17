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
					// Can fetch some correct value even if any value outside 0 to 7 is passed as the wheel id to the natives, but it's kind of